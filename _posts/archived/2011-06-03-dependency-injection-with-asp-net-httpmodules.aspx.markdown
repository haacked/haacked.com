---
layout: post
title: Dependency Injection With ASP.NET HttpModules
date: 2011-06-03 -0800
comments: true
disqus_identifier: 18793
categories:
- asp.net
- asp.net mvc
- nuget
- code
redirect_from: "/archive/2011/06/02/dependency-injection-with-asp-net-httpmodules.aspx/"
---

At the risk of getting [punched in the
face](http://twitter.com/#!/migueldeicaza/status/28312478630805504 "Miguel doesn't like DI")
by my friend Miguel, I’m not afraid to admit I’m a fan of responsible
use of dependency injection. However, for many folks, attempting to use
DI runs into a roadblock when it comes to ASP.NET HttpModule.

In the past, I typically used “[Poor man’s
DI](http://lostechies.com/jimmybogard/2009/07/03/how-not-to-do-dependency-injection-in-nerddinner/ "Poor Man's DI")”
for this. I wasn’t raised in an affluent family, so I guess I don’t have
as much of a problem with this approach that others do.

However, when the opportunity for something better comes along, I’ll
take it [Daddy
Warbucks](http://en.wikipedia.org/wiki/Oliver_%22Daddy%22_Warbucks "Daddy Warkbucs on Wikipedia").
I was refactoring some code in Subtext when it occurred to me that the
new ability to [register HttpModules
dynamically](http://blog.davidebbo.com/2011/02/register-your-http-modules-at-runtime.html "Register HttpModules")
using the `PreApplicationStartMethodAttribute` could come in very handy.

Unfortunately, the API only allows for registering a module by type,
which means the module requires a default constructor. However, as with
many problems in computer science, the solution is another layer of
redirection.

In this case, I wrote a container `HttpModule` that itself calls into
the  [the DependencyResolver feature of ASP.NET MVC
3](http://bradwilson.typepad.com/blog/2010/10/service-location-pt5-idependencyresolver.html "Dependency Resolver")
in order to find and initialize the http modules registered via your
IoC/DI container. The approach I took happens to be very much similar to
one that Mauricio Scheffer [blogged
about](http://bugsquash.blogspot.com/2009/11/windsor-managed-httpmodules.html "Windsor-Managed HttpModules")
a while ago.

```csharp
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using HttpModuleMagic;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: PreApplicationStartMethod(typeof(ContainerHttpModule), "Start")]
namespace HttpModuleMagic
{
  public class ContainerHttpModule : IHttpModule
  {
    public static void Start()
    {
      DynamicModuleUtility.RegisterModule(typeof(ContainerHttpModule));
    }

    Lazy<IEnumerable<IHttpModule>> _modules 
      = new Lazy<IEnumerable<IHttpModule>>(RetrieveModules);

    private static IEnumerable<IHttpModule> RetrieveModules()
    {
      return DependencyResolver.Current.GetServices<IHttpModule>();
    }

    public void Dispose()
    {
      var modules = _modules.Value;
      foreach (var module in modules)
      {
        var disposableModule = module as IDisposable;
        if (disposableModule != null)
        {
          disposableModule.Dispose();
        }
      }
    }

    public void Init(HttpApplication context)
    {
      var modules = _modules.Value;
      foreach (var module in modules)
      {
        module.Init(context);
      }
    }
  }
}
```

The code is pretty straightforward, though there’s a lot going on here.
At the top of the class we use the `PreApplicationStartMethodAttribute`
which allows the http module to register itself! Just reference the
assembly containing this code and you’re all set to go. No mucking
around with web.config!

Note that this code does require that you’re application has the
following two assemblies in bin:

1.  System.Web.Mvc.dll 3.0
2.  Microsoft.Web.Infrastructure.dll 1.0

The nice part about this is after referencing this assembly, I can
simply register the Http Modules using my favorite DI container and I’m
good to go. For example, I installed the *Ninject.Mvc3* package and
added the following Subtext http module bindings:

```csharp
kernel.Bind<IHttpModule>().To<BlogRequestModule>();
kernel.Bind<IHttpModule>().To<FormToBasicAuthenticationModule>();
kernel.Bind<IHttpModule>().To<AuthenticationModule>();
kernel.Bind<IHttpModule>().To<InstallationCheckModule>();
kernel.Bind<IHttpModule>().To<CompressionModule>();
```

There is one caveat I should point out. You’ll notice that when the
container http module is disposed, `Dispose` is called on each of the
registered http modules.

This could be problematic if you happen to register them in singleton
scope. In my case, all of my modules are stateless and the `Dispose`
method is a no-op, which in general is a good idea unless you absolutely
need to hold onto state.

If your modules do hold onto state and need to be disposed of, you’ll
have to be careful to scope your http modules appropriately. It’s
possible for multiple instances of your http module to be created in an
ASP.NET application.

DI for a Single Http Module
---------------------------

Just in case your DI container doesn’t support the ability to register
multiple instances of a type (in other words, it doesn’t support the
`DependencyResolver.GetServices` call), or it can’t handle the scoping
properly and your http module holds onto state that needs to be disposed
at the right time, I did write another class for registering an
individual module, while still allowing your DI container to hook into
creation of that one module.

In this case, you won’t be using DI to register the set of http modules.
But you will be using it to create instances of the modules that you
register.

Here’s the class.

```csharp
using System;
using System.Web;
using System.Web.Mvc;

namespace HttpModuleMagic
{
  public class ContainerHttpModule<TModule> 
    : IHttpModule where TModule : IHttpModule
  {
    Lazy<IHttpModule> _module = new Lazy<IHttpModule>(RetrieveModule);

    private static IHttpModule RetrieveModule()
    {
      return DependencyResolver.Current.GetService<IHttpModule>();
    }

    public void Dispose()
    {
      _module.Value.Dispose();
    }

    public void Init(HttpApplication context)
    {
      _module.Value.Init(context);
    }
  }
}
```

This module is much like the other container one, but it only wraps a
single http module. You would register it like so:

```csharp
DynamicModuleUtility.RegisterModule(typeof(ContainerHttpModule<MyHttpModule>));
```

In this case, you’d need to set up your own `PreApplicationStartMethod`
attribute or use the
[WebActivator](http://blogs.msdn.com/b/davidebb/archive/2010/10/11/light-up-your-nupacks-with-startup-code-and-webactivator.aspx "Light up your NuGets with WebActivator").

And of course, I created a little NuGet package for this.

`   `

Install-Package HttpModuleMagic

Note that this requires that you install it into an application with the
[ASP.NET MVC 3
assemblies](http://haacked.com/archive/2011/05/25/bin-deploying-asp-net-mvc-3.aspx "Bin deploying ASP.NET MVC 3").

