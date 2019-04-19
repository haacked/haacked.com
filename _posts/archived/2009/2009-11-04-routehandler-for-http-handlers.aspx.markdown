---
title: A RouteHandler for IHttpHandlers
date: 2009-11-04 -0800
tags: [aspnet,code]
redirect_from: "/archive/2009/11/03/routehandler-for-http-handlers.aspx/"
---

This code has been incorporated into a new
[RouteMagic](https://haacked.com/archive/2011/01/30/introducing-routemagic.aspx)
library I wrote which includes Source Code on CodePlex.com as well as a
NuGet package!

I saw a [bug on
Connect](https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=507180&wa=wsignin1.0 "Change PageRouteHandler to handle IHttpHandler as well as Page")
today in which someone offers the suggestion that the `PageRouteHandler`
(new in ASP.NET 4) should handle `IHttpHandler` as well as `Page`.

I don’t really agree with the suggestion because while a `Page` is an
`IHttpHandler`, an `IHttpHandler` is not a `Page`. What I this person
really wants is a new handler specifically for http handlers. Let’s give
it the tongue twisting name: `IHttpHandlerRouteHandler`.

Unfortunately, it’s too late to add this for ASP.NET 4, but it turns out
such a thing is trivially easy to write. In fact, here it is.

```csharp
public class HttpHandlerRouteHandler<THandler> 
    : IRouteHandler where THandler : IHttpHandler, new() {
  public IHttpHandler GetHttpHandler(RequestContext requestContext) {
    return new THandler();
  }
}
```

Of course, by itself it’s not all that useful. We need extension methods
to make it really easy to register routes for http handlers. I wrote a
set of those, but will only post two examples here on my blog. To get
the full set download the sample project at the very end of this post.

```csharp
public static class HttpHandlerExtensions {
  public static void MapHttpHandler<THandler>(this RouteCollection routes,     string url) where THandler : IHttpHandler, new() {
    routes.MapHttpHandler<THandler>(null, url, null, null);
  }
  //...
  public static void MapHttpHandler<THandler>(this RouteCollection routes, 
      string name, string url, object defaults, object constraints) 
      where THandler : IHttpHandler, new() { 
    var route = new Route(url, new HttpHandlerRouteHandler<THandler>());
    route.Defaults = new RouteValueDictionary(defaults);
    route.Constraints = new RouteValueDictionary(constraints);
    routes.Add(name, route);
  }
}
```

This now allows me to register a route which is handled by an
`IHttpHandler` very easily. In this case, I’m registering a route that
will use my `SimpleHttpHandler` to handle any two segment URL.

```csharp
public static void RegisterRoutes(RouteCollection routes) {
    routes.MapHttpHandler<SampleHttpHandler>("{foo}/{bar}");
}
```

And here’s the code for `SampleHttpHandler` for completeness. All it
does is print out the route values.

```csharp
public class SampleHttpHandler : IHttpHandler {
  public bool IsReusable {
    get { return false; }
  }

  public void ProcessRequest(HttpContext context) {
    var routeValues = context.Request.RequestContext.RouteData.Values;
    string message = "I saw foo='{0}' and bar='{1}'";
    message = string.Format(message, routeValues["foo"], routeValues["bar"]);

    context.Response.Write(message);
  }
}
```

When I make a request for `/testing/yo` I’ll see the message

> I saw foo='testing' and bar='yo'

in my browser. Very cool.

### Limitation

One limitation here is that my http handler has to have a parameterless
constructor. That’s not really that bad of a limitation since to
register an HTTP Handler in the old way you had to make sure that the
handler had an empty constructor.

However, this code that I wrote for this blog post is based on code that
I added to [Subtext](http://subtextproject.com/ "Subtext"). In [that
code](http://code.google.com/p/subtext/source/browse/trunk/SubtextSolution/Subtext.Framework/Routing/HttpRouteHandler.cs?spec=svn3553&r=3553 "HttpRouteHandler"),
I am passing an `IKernel` (I’m using
[Ninject](http://ninject.org/ "Ninject")) to my `HttpRouteHandler`. That
way, my route handler will use Ninject to instantiate the http handler
and thus my http handlers aren’t required to have a parameterless
constructor.

### Try it out!

The [RouteMagic](https://github.com/Haacked/RouteMagic/) solution
includes a sample project that demonstrates all this.

