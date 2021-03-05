---
title: "Editable Routes Using App_Code"
tags: [aspnet,aspnetmvc,routing]
---
UPDATE: THIS POST IS DEPRECATED!!! I’ve updated the [original post](https://haacked.com/archive/2010/01/17/editable-routes.aspx "Editable Routes")
for editable routes to work in medium trust and not require a full app
domain reload like this approach does. I think that approach may
supersede this approach until I learn otherwise. :)

Yesterday I wrote about a technique [using dynamic compilation to allow
editing
routes](https://haacked.com/archive/2010/01/17/editable-routes.aspx "Editable Routes")
after you’ve deployed an application without having to manually
recompile your application.

I made use of a FileSystemWatcher to monitor a Config directory and
dynamically recompiled code when the code file changed. This has one
advantage over using the App\_Code directory in that the whole App
Domain doesn’t need to get recycled when you make changes to your
routes.

Today, my co-worker [David
Ebbo](http://blogs.msdn.com/davidebb/ "Angle Bracket Percent") (who’s a
master at ASP.NET Build and Compilation system) pointed out one gaping
flaw with my approach. **It doesn’t work in Medium Trust because the
`FileSystemWatcher class` demands full trust**. Doh!

For many business systems, that may not be a concern. But for my blog
engine, it’s a huge concern. There are workarounds to the
`FileSystemWatcher` issues, but I decided to take the easy way out and
use the *App\_Code* directory approach, since it handles all that crufty
logic for watching the file system for me.

In this case, I simply added a new folder named *App\_Code* to my
project and copied *Routes.cs* to that folder.

![routes-in-app\_code](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/EditableRoutesInMediumTrust_9C13/routes-in-app_code_3.png "routes-in-app_code")

I then added a new method to `RouteRegistrationExtensions`. (Note that
the actual code has some null reference checking which I omitted here.)

```csharp
public static void RegisterAppCodeRoutes(this RouteCollection routes) {
  var type = BuildManager.GetType("Routes", false/*throwOnError*/);
  var registrar = Activator.CreateInstance(type) as IRouteRegistrar;
  registrar.RegisterRoutes(RouteTable.Routes);
}
```

So instead of the method I wrote in my previous post, I call this
method. The nice thing here is that this method doesn’t have to worry
about attaching a `FileSystemWatcher` or handling events and reloading
routes.

Any time the *Routes.cs file* is changed, the entire App Domain is
restarted and `Application_Start` is called again.

I want to also point out that a long while ago, I showed a [different
approach for editable routes using
IronRuby](https://haacked.com/archive/2008/04/22/defining-asp.net-mvc-routes-and-views-in-ironruby.aspx "Defining ASP.NET MVC Routes and Views in IronRuby")
that you might be interested in.

You can **[download the sample project
here](http://code.haacked.com/mvc-2/EditableRoutesDemo-MediumTrust.zip "Editable (Medium Trust) Routes Sample")**
which includes both methods for doing editable routes.