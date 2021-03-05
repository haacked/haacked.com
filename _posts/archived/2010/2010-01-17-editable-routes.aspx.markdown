---
title: "Editable Routes"
tags: [aspnetmvc,aspnet,code,routing]
---
UPDATE: 2011/02/13: This code is now included in the
[RouteMagic](https://haacked.com/archive/2011/01/30/introducing-routemagic.aspx "Introducing RouteMagic")
NuGet package! To use this code, simply run `Install-Package RouteMagic`
within the NuGet Package Manager Console.

In general, once you deploy your ASP.NET MVC application, you can’t
change the routes for your application without recompiling the
application and redeploying the assembly where your routes are defined.

[![routes](https://haacked.com/images/haacked_com/WindowsLiveWriter/EditableRoutes_12F42/routes_3.jpg "routes")](http://www.sxc.hu/photo/732646 "Vias 2 by L Avi at sxc.hu")This
is partly by design as routes are generally considered application
*code*, and should have associated unit tests to verify that the routes
are correct. A misconfigured route could seriously tank your
application.

Having said that, there are many situations in which the ability to
change an application’s routes without having to recompile the
application comes in very handy. This is the situation I find myself in
as I build a [blog
engine](http://subtextproject.com/ "Subtext Project Website") where the
folks who will install may want to tweak the routes without having to
recompile the blog’s source code.

In this post, I’ll demonstrate an approach that’ll allow you to define
your routes in a content file *as code*(no XML here!) which you deploy
with your application as in the screenshot.

![Routes File In
Soultion](https://haacked.com/images/haacked_com/WindowsLiveWriter/EditableRoutes_12F42/solution-explorer_3.png "Routes File In Soultion")

In my implementation, you need to place the routes in a *Config* folder
in your web root. Note that I used Visual Studio’s Properties dialog to
mark the file’s *Build Action* as “Content” so that it’s not compiled
into my application.

![Properties](https://haacked.com/images/haacked_com/WindowsLiveWriter/EditableRoutes_9EFD/Properties_3.png "Properties")

What this means is that the code in the *Routes.cs* file is *not*
compiled with the application. Instead, we will dynamically compile it.
First, let’s look at the contents of that file. It shouldn’t be too
surprising.

```csharp
using System.Web.Mvc;
using System.Web.Routing;
using EditableRoutesWeb;

public class Routes : IRouteRegistrar
{
  public void RegisterRoutes(RouteCollection routes)
  {
    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

    routes.MapRoute(
      "Default",
      "{controller}/{action}/{id}",
      new { controller = "Home", action = "Index", id = "" }
    );
  }
}
```

One thing you’ll notice is that this class implements an interface named
`IRouteRegistrar`. This is an interface I created and added to my web
application (though it could be defined in another assembly).

The code in *Global.asax.cs* for this application simply calls an
extension method I wrote.

```csharp
protected void Application_Start()
{
  AreaRegistration.RegisterAllAreas();
  RouteTable.Routes.RegisterRoutes("~/Config/Routes.cs");
}
```

It’s the code in this extension method where the real magic happens.

Before I show the code, there are two concepts at work here that make
this work. The first is using the `BuildManager` to dynamically create
an assembly from the *Routes.cs* file. From that assembly, we can create
an instance of the type `Routes` and cast it to `IRouteHandler`.

```csharp
var assembly = BuildManager.GetCompiledAssembly("~/Config/Routes.cs");
var registrar = assembly.CreateInstance("Routes") as IRouteRegistrar;
```

Once we have an instance of a route registrar, we can call
`RegisterRoutes` on that instance.

The second concept is being able to get notification when the
*Routes.cs* file changes. The clever trick that David told me about is
using the ASP.NET Cache object to do that. When you add an item to the
cache, you can give it a cache dependency pointing to a file *and* a
method to call when the cache is invalidated.

With those two pieces, we can add a cache dependency pointing to
*Routes.cs* and a callback method which will reload the routes when
*Routes.cs* is changed.

Here’s the full listing for `RouteRegistrationExtensions`.

```csharp
public static class RouteRegistrationExtensions
{
  public static void RegisterRoutes(this RouteCollection routes, 
      string virtualPath)
  {
    routes.ReloadRoutes(virtualPath);
    ConfigFileChangeNotifier.Listen(virtualPath, 
      vp => routes.ReloadRoutes(vp));
  }

  static void ReloadRoutes(this RouteCollection routes, string virtualPath)
  {
    var assembly = BuildManager.GetCompiledAssembly(virtualPath);
    var registrar = assembly.CreateInstance("Routes") as IRouteRegistrar;
    using(routes.GetWriteLock())
    {
      routes.Clear();
      registrar.RegisterRoutes(routes);
    }
  }
}
```

This uses a class called `ConfigFileChangeNotifier` which is based on
some code David wrote for Dynamic Data.

```csharp
public class ConfigFileChangeNotifier
{
  private ConfigFileChangeNotifier(Action<string> changeCallback)
    : this(HostingEnvironment.VirtualPathProvider, changeCallback)
  { 
  }

  private ConfigFileChangeNotifier(VirtualPathProvider vpp, 
      Action<string> changeCallback) {
    _vpp = vpp;
    _changeCallback = changeCallback;
  }

  VirtualPathProvider _vpp;
  Action<string> _changeCallback;

  // When the file at the given path changes, 
  // we'll call the supplied action.
  public static void Listen(string virtualPath, Action<string> action) {
    var notifier = new ConfigFileChangeNotifier(action);
    notifier.ListenForChanges(virtualPath);
  }

  void ListenForChanges(string virtualPath) {
    // Get a CacheDependency from the BuildProvider, 
    // so that we know anytime something changes
    var virtualPathDependencies = new List<string>();
    virtualPathDependencies.Add(virtualPath);
    CacheDependency cacheDependency = _vpp.GetCacheDependency(
      virtualPath, virtualPathDependencies, DateTime.UtcNow);
      HttpRuntime.Cache.Insert(virtualPath /*key*/,
        virtualPath /*value*/,
        cacheDependency,
        Cache.NoAbsoluteExpiration,
        Cache.NoSlidingExpiration,
        CacheItemPriority.NotRemovable,
        new CacheItemRemovedCallback(OnConfigFileChanged));
  }

  void OnConfigFileChanged(string key, object value, 
    CacheItemRemovedReason reason) {
    // We only care about dependency changes
    if (reason != CacheItemRemovedReason.DependencyChanged)
      return;

    _changeCallback(key);

    // Need to listen for the next change
    ListenForChanges(key);
  }
}
```

With this in place, you can now change routes within the *Routes.cs*
file in the *Config* directory after you’ve deployed the application.
Note that technically, a recompilation is happening, but it’s happening
dynamically at runtime when the file changes and there’s no need to
restart your entire App Domain, which is one benefit of this approach
over using the code in *App\_Code*.

If you want to try this code out, you can **[download a sample project
here](http://code.haacked.com/mvc-2/EditableRoutesDemo.zip "Editable Routes Demo")**.
The sample app is compiled against ASP.NET MVC 2 RC, but the same
principles and code can be used with an ASP.NET MVC 1.0 application. In
fact, it can also be used in an ASP.NET 4 Web Forms application since we
now support page routing.

*Note, if you want to see the old version of this code, [I’ve archived
it
here](http://code.haacked.com/mvc-2/EditableRoutesDemo-FileSystemWatcher.zip "Old FileSystemWatcher version of this code").*
