---
title: Getting The Route Name For A Route
date: 2010-11-28 -0800
disqus_identifier: 18742
tags:
- aspnet
- aspnetmvc
- code
redirect_from: "/archive/2010/11/27/getting-the-route-name-for-a-route.aspx/"
---

A question I often receive via my blog and email goes like this:

> Hi, I just got an email from a Nigerian prince asking me to hold some
> money in a bank account for him after which I’ll get a cut. Is this a
> scam?

The answer is yes. But that’s not the question I wanted to write about.
Rather, a question that I often see on
[StackOverflow](http://stackoverflow.com/ "Programming Q&A Site") and
our [ASP.NET MVC
forums](http://forums.asp.net/1146.aspx "ASP.NET MVC Forums") is more
interesting to me and it goes something like this:

> How do I get the route name for the current route?

My answer is “You can’t”. Bam! End of blog post, short and sweet.

Joking aside, I admit that’s not a satisfying answer and ending it there
wouldn’t make for much of a blog post. Not that continuing to expound on
this question necessarily will make a good blog post, but expound I
will.

It's not possible to get the route name of the route because the name is
not a property of the `Route`. When adding a route to a
`RouteCollection`, the name is used as an internal *unique* index for
the route so that lookup for the route is extremely fast. This index is
never exposed.

The reason why the route name can’t be a property becomes more apparent
when you consider that it’s possible to add a route to multiple route
collections.

```csharp
var routeCollection1 = new RouteCollection();
var routeCollection2 = new RouteCollection();

var route = new Route("{controller}/{action}", new MvcRouteHandler());

routeCollection1.Add("route-name1", route);
routeCollection2.Add("route-name2", route);
```

So in this example, we add the same route to two different route
collections using two different route names when we added the route. So
we can’t really talk about the name of the route here because what would
it be? Would it be “route-name1” or “route-name2”? I call this the
“Route Name Uncertainty Principle” but trust me, I’m alone in this.

Some of you might be thinking that ASP.NET Routing didn’t have to be
designed this way. I address that at the end of this blog post. For now,
this is the world we live in, so let’s deal with it.

Let’s do it anyways
-------------------

I’m not one to let logic and an irrefutable mathematical proof stand in
the way of me and getting what I want. I want a route’s name, and golly
gee wilickers, I’m damn well going to get it.

After all, while in theory I can add a route to multiple route
collections, I rarely do that in real life. If I promise to behave and
not do that, maybe I can have my route name with my route. How do we
accomplish this?

It’s simple really. When we add a route to the route collection, we need
to tell the route what the route name is so it can store it in its
[`DataTokens` dictionary
property](http://msdn.microsoft.com/en-us/library/system.web.routing.route.datatokens.aspx "DataTokens property of Route").
That’s exactly what that property of `Route` was designed for. Well not
for storing the name of the route, but for storing additional metadata
about the route that doesn’t affect route matching or URL generation.
Any time you need some information stored with a route so that you can
retrieve it later, `DataTokens` is the way to do it.

I wrote some simple extension methods for setting and retrieving the
name of a route.

```csharp
public static string GetRouteName(this Route route) {
    if (route == null) {
        return null;
    }
    return route.DataTokens.GetRouteName();
}

public static string GetRouteName(this RouteData routeData) {
    if (routeData == null) {
        return null;
    }
    return routeData.DataTokens.GetRouteName();
}

public static string GetRouteName(this RouteValueDictionary routeValues) {
    if (routeValues == null) {
        return null;
    }
    object routeName = null;
    routeValues.TryGetValue("__RouteName", out routeName);
    return routeName as string;
}

public static Route SetRouteName(this Route route, string routeName) {
    if (route == null) {
        throw new ArgumentNullException("route");
    }
    if (route.DataTokens == null) {
        route.DataTokens = new RouteValueDictionary();
    }
    route.DataTokens["__RouteName"] = routeName;
    return route;
}
```

Yeah, besides changing diapers, this is what I do on the weekends.
Pretty sad isn’t it?

So now, when I register routes, I just need to remember to call
`SetRouteName`.

```csharp
routes.MapRoute("rName", "{controller}/{action}").SetRouteName("rName");
```

BTW, did you know that `MapRoute` returns a `Route`? Well now you do. I
think we made that change in v2 after I begged for it like a little
toddler. But I digress.

Like eating a Turducken, that code doesn’t sit well with me. We’re
repeating the route name twice here which is prone to error. Ideally,
`MapRoute` would do it for us, but it doesn’t. So we need some new and
improved extension methods for mapping routes.

```csharp
public static Route Map(this RouteCollection routes, string name, 
    string url) {
  return routes.Map(name, url, null, null, null);
}

public static Route Map(this RouteCollection routes, string name, 
    string url, object defaults) {
  return routes.Map(name, url, defaults, null, null);
}

public static Route Map(this RouteCollection routes, string name, 
    string url, object defaults, object constraints) {
  return routes.Map(name, url, defaults, constraints, null);
}

public static Route Map(this RouteCollection routes, string name, 
    string url, object defaults, object constraints, string[] namespaces) {
  return routes.MapRoute(name, url, defaults, constraints, namespaces)
    .SetRouteName(name);
}
```

These methods correspond to some (but not all, because I’m lazy) of the
`MapRoute` extension methods in the `System.Web.Mvc` namespace. I called
them `Map` simply because I didn’t want to conflict with the existing
`MapRoute` extension methods.

With these set of methods, I can easily create routes for which I can
retrieve the route name.

```csharp
var route = routes.Map("rName", "url");
route.GetRouteName();

// within a controller
string routeName = RouteData.GetRouteName();
```

With these methods, you can now grab the route name from the route
should you need it.

Of course, one question to ask yourself is why do you need to know the
route name in the first place? Many times, when people ask this
question, what they really are doing is making the route name do double
duty. They want it to act as an index for route lookup as well as be a
label applied to the route so they can take some custom action based on
the name.

In this second case though, the “label” doesn’t have to be the route
name. It could be anything stored in data tokens. In a future blog post,
I’ll show you an example of a situation where I really do need to know
the route name.

Alternate Design Aside
----------------------

As an aside, why is routing designed this way? I wasn’t there when this
particular decision was made, but I believe it has to do with
performance and safety. With the current API, once a route name has been
added to a route collection with a name, internally, the route
collection can safely use the route name as a dictionary key for the
route knowing full well that the route name cannot change.

But imagine instead that `RouteBase` (the base class for all routes) had
a `Name` property and the `RouteCollection.Add` method used that as the
key for route lookup. Well it’s quite possible that the value of the
route’s name could change for some reason due to a poor implementation.
In that case, the index would be out of sync with the route’s name.

While I agree that the current design is safer, in retrospect I doubt
many will screw up  a read-only name property which should never change.
We could have documented that the contract for the `Name` property of
`Route` is that it should never change during the lifetime of the route.
But then again, who reads the documentation? After all, I offered
\$1,000 to the first person who emailed me a hidden message embedded in
our [ASP.NET MVC 3 release
notes](http://www.asp.net/learn/whitepapers/mvc3-release-notes "ASP.NET MVC 3 Release Notes")
and haven’t received one email yet. Also, you’d be surprised how many
people screw up `GetHashCode()`, which effectively would have the same
purpose as a route’s `Name` property.

And by the way, there are no hidden messages in the release notes. Did I
make you look?

