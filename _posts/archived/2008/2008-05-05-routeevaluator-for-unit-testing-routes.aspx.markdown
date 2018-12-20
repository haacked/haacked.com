---
title: RouteEvaluator For Unit Testing Routes
date: 2008-05-05 -0800
disqus_identifier: 18482
tags: [routing,tdd,aspnetmvc]
redirect_from: "/archive/2008/05/04/routeevaluator-for-unit-testing-routes.aspx/"
---

A while back I wrote a [routing
debugger](https://haacked.com/archive/2008/03/13/url-routing-debugger.aspx "URL Routing Debugger")
which is useful for testing your routes and seeing which routes would
match a given URL. [Rob](http://blog.wekeroad.com/ "Rob Conery")
suggested we have something like this for unit tests, so I whipped
something simple up.

This is a class that allows you to test multiple different URLs quickly.
You simply create the `RouteEvaluator` giving it a collection of routes
and then `GetMatches` which returns a `List<RouteData>` containing a
`RouteData` instance for every route that matches, not just the first
one.

Here's a sample of usage.

```csharp
[Test]
public void CanMatchUsingRouteEvaluator()
{
  var routes = new RouteCollection();
  GlobalApplication.RegisterRoutes(routes);

  var evaluator = new RouteEvaluator(routes);
  var matchingRouteData = evaluator.GetMatches("~/foo/bar");
  Assert.IsTrue(matchingRouteData.Count > 0);
  matchingRouteData = evaluator.GetMatches("~/foo/bar/baz/billy");
  Assert.AreEqual(0, matchingRouteData.Count);
}
```

And here’s the code. Note that my implementation relies on
[Moq](http://code.google.com/p/moq/ "Moq on Google Code"), but you could
easily implement it without using Moq if you wanted to.

```csharp
public class RouteEvaluator
{
  RouteCollection routes;
    
  public RouteEvaluator(RouteCollection routes)
  {
    this.routes = routes;
  }

  public IList<RouteData> GetMatches(string virtualPath)
  {
    return GetMatches(virtualPath, "GET");
  }

  public IList<RouteData> GetMatches(string virtualPath, string httpMethod)
  {
    List<RouteData> matchingRouteData = new List<RouteData>();

    foreach (var route in this.routes)
    {
      var context = new Mock<HttpContextBase>();
      var request = new Mock<HttpRequestBase>();

      context.Expect(ctx => ctx.Request).Returns(request.Object);
      request.Expect(req => req.PathInfo).Returns(string.Empty);
      request.Expect(req => 
req.AppRelativeCurrentExecutionFilePath).Returns(virtualPath);
      if (!string.IsNullOrEmpty(httpMethod))
      {
        request.Expect(req => req.HttpMethod).Returns(httpMethod);
      }

      RouteData routeData = this.routes.GetRouteData(context.Object);
      if (routeData != null) {
        matchingRouteData.Add(routeData);
      }
    }
    return matchingRouteData;
  }
}
```

Let me know if this ends up being useful to you.

