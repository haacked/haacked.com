---
title: Writing an ASP.NET MVC Controller Inspector
tags: [aspnetmvc,aspnet]
redirect_from: "/archive/2011/08/09/writing-an-asp-net-mvc-controller-inspector.aspx/"
---

99.99999% of the time (yes, I measured it), a controller in ASP.NET MVC
is a type, and an action is a method — with reflection as the glue that
holds it all together. For most folks, that’s the best way to view how
ASP.NET MVC works.

But some folks like to dig deeper and get their hands dirty a bit by
taking a peek under the hood. Doing so reveals that while the reflection
based mapping of controllers types and actions to methods is true by
default, it can be easily changed to something else entirely.

ASP.NET MVC contains powerful abstractions for the controllers and
actions via the `ControllerDescriptor` and `ActionDescriptor` classes.
These abstractions make it possible to implement completely different
underlying implementations of a controller and action. For example, one
could implement a version of ASP.NET MVC on top of a dynamic language
using the DLR such as the [IronRuby ASP.NET
MVC](https://haacked.com/archive/2009/02/17/aspnetmvc-ironruby-with-filters.aspx "IronRuby ASP.NET MVC")
I wrote about a long time ago.

Using these abstractions, we can implement something useful like a
**Controller Inspector**, a nice complement to the **[Route
Debugger](https://haacked.com/archive/2008/03/13/url-routing-debugger.aspx)**
I wrote a while back.

Installing the Controller Inspector
-----------------------------------

![Inspector-120x120](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/0137111d4432_E43D/Inspector-120x120_3.png "Inspector-120x120")The
Controller Inspector is available as a NuGet package with the package id
*MvcHaack.ControllerInspector* (my Paint.net skills are top notch!).

`   `

`Install-Package MvcHaack.ControllerInspector`

After installing the package, visit any URL in your application rendered
by a controller action. For example, here’s a standard request for a
boring action.

![index-action](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/0137111d4432_E43D/index-action_5.png "index-action")

With the package installed and while running the site on localhost (it
won’t work when the site is deployed), append the query string parameter
**?inspect**. For example, in my sample, I just visit:
*http://localhost:38249/?inspect* and voila!

[![controller-inspector](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/0137111d4432_E43D/controller-inspector_thumb.png "controller-inspector")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/0137111d4432_E43D/controller-inspector_2.png)

I nicely formatted page that displays information about the controller
and each of its actions. *If you’re wondering, “hey, isn’t this like
Glimpse!” please skip to the end of this blog post where I address
that.*

Here’s a look at an action method.

![contorller-inspector-action-view](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/0137111d4432_E43D/contorller-inspector-action-view_3.png "contorller-inspector-action-view")

Notice that it conveniently shows the HTTP verbs next to the name to
differentiate action methods of the same name. If an action method
accepts a complex type as a parameter, the inspector displays details
about that type (though not recursively yet).

![action-with-model](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/0137111d4432_E43D/action-with-model_3.png "action-with-model")

Accessing controller metadata
-----------------------------

The `ControllerDescriptor` class provides ways to get at metadata about
the controller. The interesting members include:

-   `ControllerName`
-   `ControllerType`
-   `GetCanonicalActions`
-   `GetCustomAttributes`

The first two properties are self evident. The method
`GetCanonicalActions` returns an enumeration of `ActionDescriptor`
instances, each of which describes an action. `GetCustomAttributes`
returns attributes applied to the controller. These are typically the
filters applied to the controller itself.

In the case of the default controller descriptor,
`ReflectedControllerDescriptor`, the filters returned by
`GetCustomAttributes` are retrieved via reflection. But a custom
descriptor could load those filters from elsewhere (as is the case with
the IronRuby implementation).

The `ActionDescriptor` also has a few interesting properties.

-   `ActionName`
-   `ControllerDescriptor`
-   `GetCustomAttributes`
-   `GetFilters`
-   `GetParameters`
-   `GetSelectors`

Despite what you might expect, you can’t obtain everything you’d want to
know from an `ActionDescriptor`. For example, if you’re interested in
the return type of an action method, the `ActionDescriptor` won’t help?
Why not? Well, it may be impossible to tell you that. For example, the
type might not be known ahead of time because it requires the action
method to be invoked first, as would be the case in a dynamically typed
language.

So these abstractions were carefully designed not to assume too much
about the underlying implementation of an action/controller.

But as we learned before, 99.99999% of the time we’re dealing with the
default reflection based approach. So what the Controller Inspector does
is to try and cast the `ActionDescriptor` to `ReflectedActionDescriptor`
and if that succeeds, it can reflect over the action method normally to
provide a lot more details.

Hooking itself up
-----------------

To hook the code up that outputs all this information, I made use of
David Ebbo’s [WebActivator
package](http://blogs.msdn.com/b/davidebb/archive/2010/10/11/light-up-your-nupacks-with-startup-code-and-webactivator.aspx "WebActivator").
This allows me to run a bit of code at startup that replaces the current
controller factory with an `InspectorControllerFactory`.

```csharp
[assembly: PostApplicationStartMethod(typeof(AppStart), "Start")]
namespace MvcHaack.ControllerInspector {
  public static class AppStart {
    public static void Start() {
      var factory = ControllerBuilder.Current.GetControllerFactory();
      ControllerBuilder.Current.SetControllerFactory(
        new InspectorControllerFactory(factory));
      }
  }
}
```

The `InspectorControllerFactory` wraps the existing controller factory.
All it does is call into the existing factory to create a controller,
and if the request is a local request with the proper “inspect” query
string parameter, it sets its invoker to be an `InspectorActionInvoker`.
This way, for normal requests, there is pretty much no overhead.

```csharp
public IController CreateController(RequestContext requestContext, 
    string controllerName) {
  var controller = _controllerFactory.CreateController(requestContext,
    controllerName);

  if (IsInspectorRequest(requestContext.HttpContext.Request)) {
    var normalController = controller as Controller;
    var invoker = normalController.ActionInvoker;
    normalController.ActionInvoker = new InspectorActionInvoker(invoker);
  }
  return controller;
}

private static bool IsInspectorRequest(HttpRequestBase httpRequest) {
  return httpRequest.IsLocal
    && httpRequest.QueryString.Keys.Count > 0
    && httpRequest.QueryString.GetValues(null).Contains("inspect");
}
```

What the `InspectorActionInvoker` does is build up a model of all the
information we want to display and passes it to a precompiled Razor
template using the approach I wrote about recently in [Text Templating
using Razor the easy
way](https://haacked.com/archive/2011/08/01/text-templating-using-razor-the-easy-way.aspx "Text Templating with Razor blog post").
I simply build up a huge anonymous class and pass it to a dynamic model
in the template. Note that this probably won’t work in medium trust, but
I can easily fix that later by either using an
[ExpandoObject](http://msdn.microsoft.com/en-us/library/system.dynamic.expandoobject(VS.100).aspx "ExpandoObject in MSDN")
or by using a strongly typed model. I was just being lazy as this is a
proof of concept.

What about Glimpse?
-------------------

As soon as I showed this to some co-workers they asked me why I was
trying to re-implement Glimpse. If you haven’t heard of Glimpse, stop
and go [read this blog
post](http://www.hanselman.com/blog/NuGetPackageOfTheWeek5DebuggingASPNETMVCApplicationsWithGlimpse.aspx "Debugging MVC with Glimpse").
Glimpse is like a server-side Firebug for ASP.NET MVC applications.
However, when I last checked, it didn’t have something exactly like
this.

The point in writing this was to teach folks about the
`ControllerDescriptor` and `ActionDescriptor`. I’ll make the code
available later when I finish part three of this informal series and
perhaps someone can help me turn this into a Glimpse plugin if that
makes sense. In the meanwhile, I built the package [with
symbols](http://docs.nuget.org/docs/creating-packages/creating-and-publishing-a-symbol-package "Creating a symbols package")
so you can debug into it to see the source.

UPDATE: The code is [available on
Github!](https://github.com/Haacked/CodeHaacks "CodeHaacks")

In the third part, I blog about [what originally lead me down this
path](https://haacked.com/archive/2011/08/18/calling-asp-net-mvc-action-methods-from-javascript.aspx)
to write about the descriptors, which in turn lead me down the path to
write about the Razor Generator. Yes, I’m easily sidetracked!

As a reminder, to try it out, install the
**MvcHaack.ControllerInspector** package then use the *?inspect* query
string parameter when viewing a page rendered by a controller.



