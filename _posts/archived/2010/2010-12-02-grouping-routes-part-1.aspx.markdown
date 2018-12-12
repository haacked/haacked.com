---
title: Grouping Routes Part 1
date: 2010-12-02 -0800
disqus_identifier: 18743
categories:
- asp.net
- asp.net mvc
- code
redirect_from: "/archive/2010/12/01/grouping-routes-part-1.aspx/"
---

UPDATE: 2011/02/13: This code is now included in the
[RouteMagic](https://haacked.com/archive/2011/01/30/introducing-routemagic.aspx "Introducing RouteMagic")
NuGet package! To use this code, simply run `Install-Package RouteMagic`
within the NuGet Package Manager Console.

One thing ASP.NET Routing doesn’t support is washing and detailing my
car. I really pushed for that feature, but my coworkers felt it was out
of scope. Kill joys.

Another thing Routing doesn’t support out of the box is a way to group a
set of routes within another route. For example, suppose I want a set of
routes to all live under the same URL path. Today, I’d need to make sure
all the routes started with the same URL segment. For example, here’s a
set of routes that all live under the “/blog” URL path.

```csharp
RouteTable.Routes.MapRoute("r1", "blog/posts");
RouteTable.Routes.MapRoute("r2", "blog/posts/{id}");
RouteTable.Routes.MapRoute("r3", "blog/archives");
```

If I decide I want all these routes to live under something other than
“blog” such as in the root or under a completely different name such as
“archives”, I have to change each route. Not such a big deal with only
three routes, but with a large system with multiple groupings, this can
be a hassle.

I suppose one easy way to solve this is to do the following:

```csharp
string baseUrl = "blog/";
RouteTable.Routes.MapRoute("r1", baseUrl + "posts");
RouteTable.Routes.MapRoute("r2", baseUrl + "posts/{id}");
RouteTable.Routes.MapRoute("r3", baseUrl + "archives");
```

Bam! Done! Call it a night Frank.

This is actually a very simple and great solution to the problem I
stated. In fact, it probably works better than the alternative I’m about
to show you. If this works so well, why am I showing you the
alternative?

Well, there’s something unsatisfying about that answer. Suppose a
request comes in for */not-blog*. Every one of those routes is going to
be evaluated even though we already know none of them will match. If we
could group them, we could reduce the check to just one check. Also,
it’s just not as much fun as what I’m about to show you.

What I would like to be able to do is the following.

```csharp
var blogRoutes = new RouteCollection();
blogRoutes.MapRoute("r1", "posts");
blogRoutes.MapRoute("r2", "posts/{id}");
blogRoutes.MapRoute("r3", "archives");

RouteTable.Routes.Add("blog-routes", new GroupRoute("~/blog", blogRoutes));
```

In this code snippet, I’ve declared a set of routes and added them to a
proposed `GroupRoute` instance. That group route is then added to the
route table. Note that the child routes are not themselves added to the
route table and they have no idea what parent path they’ll end up
responding to.

With this proposed route, I these child routes would then handle
requests to */blog/posts* and */blog/archives*. But if I decide to place
them under a different path, I can simply change a single route, the
group route, and I don’t need to change each child route.

Implementation
--------------

In this section, I’ll describe the implementation of such a group route
in broad brush strokes. The goal here is to provide an under the hood
look at how routing works and how it can be extended.

Implementing such a grouping route is not trivial. Routes in general
work directly off of the current http request in order to determine if
they match a request or not.

By themselves, those child routes I defined earlier would not match a
request for */blog/posts*. Note that the URL for the child routes don’t
start with “blog”. Fortunately though, the request that is supplied to
each route is an instance of `HttpRequestBase`, an abstract base class.

What this means is we can muck around with the request and even change
it so that the child routes don’t even know the actual requests starts
with */blog*. That way, when a request comes in for */blog/posts*, the
group route matches it, but then rewrites the request only for its child
routes so that they think they’re trying to match */posts.*

**Please note that what I’m about to show you here is based on internal
knowledge of routing and is unsupported and may cause you to lose hair,
get a rash, and suffer much grief if you depend on it. Use this approach
at your own risk.**

The first thing I did was implement my own wrapper classes for the http
context class.

```csharp
public class ChildHttpContextWrapper : HttpContextBase {
  private HttpContextBase _httpContext;
  private HttpRequestBase _request;

  public ChildHttpContextWrapper(HttpContextBase httpContext, 
      string parentVirtualPath, string parentPath) {
    _httpContext = httpContext;
    _request = new ChildHttpRequestWrapper(httpContext.Request, 
      parentVirtualPath, parentPath);
  }

  public override HttpRequestBase Request {
    get {
      return _request;
    }
 }

  // ... All other properties/methods delegate to _httpContext
}
```

Note that all this does is delegate every method and property to the
supplied `HttpContextBase` instance that it wraps except for the Request
property, which returns an instance of my next wrapper class.

```csharp
public class ChildHttpRequestWrapper : HttpRequestBase {
  HttpRequestBase _httpRequest;
  string _path;
  string _appRelativeCurrentExecutionFilePath;

  public ChildHttpRequestWrapper(HttpRequestBase httpRequest, 
    string parentVirtualPath, string parentPath) {
    if (!parentVirtualPath.StartsWith("~/")) {
      throw new InvalidOperationException("parentVirtualPath 
        must start with ~/");
    }

    if (!httpRequest.AppRelativeCurrentExecutionFilePath
        .StartsWith(parentVirtualPath, StringComparison.OrdinalIgnoreCase)) {
      throw new InvalidOperationException("This request is not valid 
        for the current path.");
    }

    _path = httpRequest.Path.Remove(0, parentPath.Length);
    _appRelativeCurrentExecutionFilePath = httpRequest.
      AppRelativeCurrentExecutionFilePath.Remove(1,       parentVirtualPath.Length - 1);
    _httpRequest = httpRequest;
  }

  public override string Path { get { return _path; } }

  public override string AppRelativeCurrentExecutionFilePath {
    get { return _appRelativeCurrentExecutionFilePath; }
  }

  // all other properties/method delegate to_httpRequest
}
```

What this child request does is strip off the portion of the request
path that corresponds to its parent’s virtual path. That’s the “\~/blog”
part supplied by the group route.

It then makes sure that the `Path` and the
`AppRelativeCurrentExecutionFilePath` properties return this updated
URL. Current implementations of routing look at these two properties
when matching an incoming request. However, that’s an internal
implementation detail of routing that could change, hence my admonition
earlier that this is voodoo magic.

The implementation of request matching for `GroupRoute` is fairly
straightforward then.

```csharp
public override RouteData GetRouteData(HttpContextBase httpContext) {
  if (!httpContext.Request.AppRelativeCurrentExecutionFilePath.
      StartsWith(VirtualPath, StringComparison.OrdinalIgnoreCase)) {
    return null;
  }

  HttpContextBase childHttpContext = VirtualPath != ApplicationRootPath ? 
    new ChildHttpContextWrapper(httpContext, VirtualPath, _path) : null;

  return ChildRoutes.GetRouteData(childHttpContext ?? httpContext);
}
```

All we did here is to make sure that the group route matches the current
request. If so, we then created a child http context which as we saw
earlier, looks just like the current http context, only the /blog
portion of the request is stripped off. We then pass that to our
internal route collection to see if any child route matches. If so, we
return the route data from that match and we’re done.

In [Part 2 of this
series](https://haacked.com/archive/2011/01/09/grouping-routes-part-2.aspx "Grouping Routes part 2"),
we’ll look at implementing URL generation. That’s where things get
really tricky.

