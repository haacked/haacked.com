---
title: Finding Bad Controllers
tags: [aspnetmvc]
redirect_from: "/archive/2012/07/24/finding-bad-controllers.aspx/"
---

In one mailing list I’m on, someone ran into a problem where they
renamed a controller, but ASP.NET MVC could not for the life of it find
it. They double checked everything. But ASP.NET MVC simply reported a
404.

This is usually where I tell folks to run the following NuGet command:

```csharp
Install-Package RouteDebugger
```

[RouteDebugger](https://haacked.com/archive/2011/04/13/routedebugger-2.aspx "RouteDebugger")
is a great way to find out why a route isn’t matching a controller.

In this particular case, the culprit was that the person renamed the
controller and forgot to append the “Controller” suffix. So from
something like `HomeController` to `Default`.

In this post, I’ll talk a little bit about what makes a controller a
controller and a potential solution so this mistake is caught
automatically.

What’s with the convention anyways?
-----------------------------------

A question that often comes up is why require a name based convention
for controllers in the first place? After all, controllers also must
implement the `IController` interface and that should be enough to
indicate the class is a controller.

Great question! That decision was made before I joined Microsoft and I
never bothered to ask why. What I did do was make up my own retroactive
reasoning, which is as follows.

Suppose we simply took the class name to be the controller name. Also
suppose you’re building a page to display a category and you want the
URL to be something like `/category/unicorns`. So you write a controller
named `Category`. Chances are, you also have an entity class named
`Category` too that you want to use within the `Category` controller. So
now, a common default situation becomes painful.

If I could get in a time machine and revisit this decision, would I?
Hell no! As my former co-worker Eilon always says; if you have a time
machine, there’s probably a lot better things you can do than fix bad
design decisions in ASP.NET MVC.

But if I were to do this again, I’m not so sure I’d require the
“Controller” suffix. Instead, I’d suggest using plural controllers (and
URLs) to better avoid conflicts. So the controller would be `Categories`
and the URL would be `/categories/unicorns`. And perhaps I’d make the
“Controller” suffix allowed as a way to resolve conflicts. So
`CategoryController` would still work fine (as a conroller named
“category”) if that’s the way you roll.

How do I detect Controller Problems?
------------------------------------

Since I didn’t use my time machine to fix this issue (*I would rather
use it to go back in time and fix line endings text encodings*), what
can we do about it?

The simplest solution is to do nothing. How often will you make this
mistake?

Then again, when you do, it’s kind of maddening because there’s no error
message. The controller just isn’t there. Also, there’s other mistakes
you could make, though many are unlikley. All the following look like
they were intended to be controllers, but aren’t.

```csharp
public class Category : Controller
{}

public abstract class CategoryContoller : Controller
{}

public class Foo 
{
    public class CategoryController : Controller 
    { }
}

class BarController : Controller
{}

internal class BarController : Controller
{}

public class BuzzController
{}
```

Controllers in ASP.NET MVC must be public, non-abstract, non-nested, and
implement `IController`, or derive from a class that does.

So the list of possible mistakes you might make are:

-   Forget to add the “Controller” suffix (*happens more than you
    think*)
-   Make the class abstract (*probably not likely*)
-   Nest the class (*could happen by accident like you thought you were
    pasting in a namespace, but very very unlikely*)
-   Forget to make the class public (*not likely if you use the Add
    Controller dialog. But if you use the Add Class dialog, could
    happen*)
-   Forget to derive from `Controller` or `ControllerBase` or implement
    `IController`. (Again, probably not very likely)

How to detect these cases
-------------------------

As I mentioned before, it might not be that important to do this, but
one thing you could consider is writing a unit test that detects these
various conditions. Well how would you do that?

You’re in luck. Whether this is super useful or not, I still found this
to be an interesting problem to solve. I wrote a `ControllerValidator`
class with some methods for finding all controllers that match one of
these conditions.

It builds on the [extension method to retrieve all
types](https://haacked.com/archive/2012/07/23/get-all-types-in-an-assembly.aspx "Get all types in an assembly")
I blogged about the other day. First, I wrote extension methods for the
various controller conditions:

```csharp
static bool IsPublicClass(this Type type)
{
    return (type != null && type.IsPublic && type.IsClass && !type.IsAbstract);
}

static bool IsControllerType(this Type t)
{
    return typeof (IController).IsAssignableFrom(t);
}

static bool MeetsConvention(this Type t)
{
    return t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase);
}
```

With these methods, it became a simple case of writing methods that
checked for two out of these three conditions.

For example, to get all the controllers that don’t have the “Controller”
suffix:

```csharp
public static IEnumerable<Type> GetUnconventionalControllers
  (this Assembly assembly)
{
  return from t in assembly.GetLoadableTypes()
      where t.IsPublicClass() && t.IsControllerType() && !t.MeetsConvention()
      select t;
}
```

With these methods, it’s a simple matter to write automated tests that
look for these mistakes.

Source code and Demo
--------------------

I added these methods as part of the [ControllerInspector
library](https://haacked.com/archive/2011/08/10/writing-an-asp-net-mvc-controller-inspector.aspx "ControllerInspector")
which is available on NuGet. You can also grab the source code from my
[CodeHaacks repository on
GitHub](https://github.com/Haacked/CodeHaacks "CodeHaacks") (*click the
Clone in Windows button!*).

If you get the source code, check out the following projects:

-   **ControllerInspectorTests.csproj** – Unit tests of these new
    methods show you how you might write your own unit tests.
-   **MvcHaack.ControllerInspector.csproj** – Contains the
    `ControllerValidator` class.
-   **MvcHaack.ControllerInspector.DemoWeb.csproj** – Has a website that
    demonstrates this class too.

The demo website’s homepage uses these methods to show a list of bad
controllers.

[![bad-controllers](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/b185f9b53b5b_11FC8/bad-controllers_thumb.png "bad-controllers")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/b185f9b53b5b_11FC8/bad-controllers_2.png)

The code I wrote is based on looking at how ASP.NET MVC locates and
determines controllers. It turns out, because of the performance
optimizations, it takes a bit of digging to find the right code.

If you’re interested in looking at the source, check out
[TypeCacheUtil.cs on
CodePlex](http://aspnetwebstack.codeplex.com/SourceControl/changeset/view/eecfe803d31d#src%2fSystem.Web.Mvc%2fTypeCacheUtil.cs "TypeCacheUtil.cs").
It’s really nice that ASP.NET MVC is not only open source, but also
accepts contributions. I highly recommend digging through the source as
there’s a lot of interesting useful code in there, especially around
reflection.

If you don’t find this useful, I hope you at least found it
illuminating.

