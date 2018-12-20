---
title: Testing Routes In ASP.NET MVC
date: 2007-12-17 -0800
tags:
- aspnet
- code
- aspnetmvc
redirect_from:
- "/archive/2007/12/15/testing-routes-in-asp.net-mvc.aspx/"
- "/archive/2007/12/16/testing-routes-in-asp.net-mvc.aspx/"
---

The ASP.NET Routing engine used by ASP.NET MVC plays a very important
role. Routes map incoming requests for URLs to a Controller and Action.
They also are used to construct an URL to a Controller/Action. In this
way, they provide a two-way mapping between URLs and controller actions.

When building routes, it may be useful to write unit tests for the
routes to ensure that you’ve set up the proper mappings you intend.

[ScottGu](http://weblogs.asp.net/scottgu/ "Scott Guthrie") touched a bit
on unit testing routes in [part
2](http://weblogs.asp.net/scottgu/archive/2007/12/03/asp-net-mvc-framework-part-2-url-routing.aspx "URL Routing")
of his series on MVC in which he covers URL Routing. In this post, we’ll
go into a little more depth with testing routes.

To keep it interesting, let me show you what the final unit test looks
like for testing a route. That way, if you don’t care about the details,
you can skip all this discussion and just download the code.

```csharp
[TestMethod]
public void RouteHasDefaultActionWhenUrlWithoutAction()
{
  RouteCollection routes = new RouteCollection();
  GlobalApplication.RegisterRoutes(routes);

  TestHelper.AssertRoute(routes, "~/product"
    , new { controller = "product", action = "Index" });
}
```

The first part of the test simply populates a collection with routes
from your Global application class defined in `Global.asax.cs`. The
second part is a call to another helper method that attempts to map a
route to the specified virtual URL and compares the route data to a
dictionary of name/value pairs. The dictionary is passed in as an
anonymous type using a technique that my coworker [Eilon
Lipton](http://weblogs.asp.net/leftslipper/ "Eilon") wrote [about on his
blog](http://weblogs.asp.net/leftslipper/archive/2007/09/24/using-c-3-0-anonymous-types-as-dictionaries.aspx "Anonymous Types as dictionaries").

Let’s take a quick look at the `RegisterRoutes` method so you can see
which routes I am testing.

```csharp
public static void RegisterRoutes(RouteCollection routes) {
    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

    routes.MapRoute("blog-route", "blog/{year}/{month}/{day}",
        new { controller = "Blog", action = "Index", id = "" },
        new { year = @"\d{4}", month = @"\d{2}", day = @"\d{2}" }
    );

    routes.MapRoute(
        "Default",
        "{controller}/{action}/{id}",
        new { controller = "Home", action = "Index", id = "" }
    );
}
```

Looks like your standard set of routes, no? I threw one in there that
looks like one you might use with a blog engine.

Next, I’ll show you how I would write a test the long way using MoQ.

```csharp
[TestMethod]
public void CanMapNormalControllerActionRoute() {
    RouteCollection routes = new RouteCollection();
    GlobalApplication.RegisterRoutes(routes);

    var httpContextMock = new Mock<HttpContextBase>();
    httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
        .Returns("~/product/list");

    RouteData routeData = routes.GetRouteData(httpContextMock.Object);
    Assert.IsNotNull(routeData, "Should have found the route");
    Assert.AreEqual("product", routeData.Values["Controller"]
        , "Expected a different controller");
    Assert.AreEqual("list", routeData.Values["action"]
        , "Expected a different action");
}
```

Ok, that isn’t all that bad. It may seem like a lot of code, but it’s
pretty straightforward, assuming you understand the general pattern for
using a mock framework.

However, we can shorten a lot of this code by using an extension method
I wrote in a [previous
post](https://haacked.com/archive/2007/11/05/rhino-mocks-extension-methods-mvc-crazy-delicious.aspx "Rhino Mock + Extension Methods + MVC = Crazy Delicious").
I actually wrote an overload that makes it easier to mock a request for
a specific URL.

That gets us further, but we can do so much more. Here is the code I
wrote for my `AssertRoute` method.

```csharp
public static void AssertRoute(RouteCollection routes, string url, 
    object expectations) 
{
    var httpContextMock = new Mock<HttpContextBase>();
    httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
        .Returns(url);

    RouteData routeData = routes.GetRouteData(httpContextMock.Object);
    Assert.IsNotNull(routeData, "Should have found the route");

    foreach (PropertyValue property in GetProperties(expectations)) {
        Assert.IsTrue(string.Equals(property.Value.ToString(), 
            routeData.Values[property.Name].ToString(), 
            StringComparison.OrdinalIgnoreCase)
            , string.Format("Expected '{0}', not '{1}' for '{2}'.", 
            property.Value, routeData.Values[property.Name], property.Name));
    }
}
```

This code makes use of the `GetProperties` method I lifted from Eilon’s
blog post, [Using C\# 3.0 Anonymous types as
Dictionaries](http://weblogs.asp.net/leftslipper/archive/2007/09/24/using-c-3-0-anonymous-types-as-dictionaries.aspx "Using Anonymous Types as Dictionaries").

The *expectations* passed to this method are the name/value pairs you
expect to see in the `RouteData.Values` dictionary.

I hope you find this useful. The code (along with other unit test
examples) are in solution [**ready for
download**](http://code.haacked.com/mvc-3/TddDemo.zip "TDD Demo Solution").

UPDATE: I updated the blog post and solution (7/27 1:38 PM PST) to
account for all the changes to routing made since I wrote this post
originally.

UPDATE AGAIN: Updated the post to use [MoQ
3.0](http://code.google.com/p/moq/downloads/list "MoQ 3") and cleaned up
the code a slight bit.

UPDATE: 03/11/2012 - Fixed the broken download link. Rewrote the tests
to use xUnit.NET.

