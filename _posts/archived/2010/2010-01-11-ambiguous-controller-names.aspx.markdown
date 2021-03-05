---
title: "Ambiguous Controller Names With Areas"
tags: [aspnet,aspnetmvc]
---
*Note: This describes the behavior of ASP.NET MVC 2 as of the release
candidate. It’s possible things might change for the RTM.*

When using areas in ASP.NET MVC 2, a common problem you might encounter
is this exception message.

> The controller name 'Home' is ambiguous between the following types: \
>  AreasDemoWeb.Controllers.HomeController \
>  AreasDemoWeb.Areas.Blogs.Controllers.HomeController

This message is telling you that the controller factory found two types
that match the route data for the current request. Typically this
happens when you have a controller of the same name in an area and in
the main project.

For example, in the screenshot below, notice that we have a
`HomeController` in the main *Controllers* folder as well as in the
*Blogs* area.

![area-with-ambiguous-controller](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/AmbiguousControllerNames_8573/area-with-ambiguous-controller_3.png "area-with-ambiguous-controller")

If you make a request for the area such as */Blogs/Home*, you’ll find
that everything works
[hunky-dory](http://www.merriam-webster.com/dictionary/hunky-dory "Definition of 'hunky-dory' from Merriam-Webster").
However, if you make a request for the root HomeController, such as
*/Home*, you’ll get the ambiguous controller exception.

### Why is that?

When you register routes for an area, they get a namespace associated
with each route. That ensures that only controllers within the namespace
associated with that area can fulfill the request. Thus the request that
matches an area will have that namespace and the namespace is used to
disambiguate controllers.

But by default, the routes in the main application don’t have a
namespace associated with them. That means the controller factory will
scan all types looking for a match, and in this case finding two types
which match the controller name “Home”.

### The Fix

There are two very simple workarounds. The simplest falls in the “If it
hurts, stop doing that” camp which is to simply avoid naming two
controllers the same name.

For many situations, this is not a satisfactory answer. The other
workaround, as you might guess from my explanation of why this happens,
is to give the route in the main application a specific namespace.
Here’s an example of the default route in *Global.asax.cs* which has the
fix.

```csharp
public static void RegisterRoutes(RouteCollection routes)
{
  routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

  routes.MapRoute(
    "Default",                                              // Route name
    "{controller}/{action}/{id}",                           // URL
    new { controller = "Home", action = "Index", id = "" }, // Defaults
    new[]{"AreasDemoWeb.Controllers"}                       // Namespaces
  );
}
```

In the code above, I added a fourth parameter which is an array of
namespaces. The controllers for my project live in a namespace called
`AreasDemoWeb.Controllers`.

### Follow Up

In a follow-up post, I’ll walk through more details about areas and how
namespaces play into routing and controller lookup. For now, I hope this
gets you unstuck if you’ve run into this problem before.