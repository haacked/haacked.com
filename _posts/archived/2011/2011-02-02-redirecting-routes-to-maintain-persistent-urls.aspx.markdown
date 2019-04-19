---
title: Redirecting Routes To Maintain Persistent URLs
date: 2011-02-02 -0800 9:00 AM
tags: [aspnet,nuget,oss]
redirect_from: "/archive/2011/02/01/redirecting-routes-to-maintain-persistent-urls.aspx/"
---

Over a decade ago, Tim Berners-Lee, creator of the World Wide Web
instructed the world know that [cool URIs don’t
change](http://www.w3.org/Provider/Style/URI "Cool URIs don't change.")
with what appears to be a poem, but it doesn’t rhyme and it’s not haiku.

> What makes a cool URI? \
> A cool URI is one which does not change. \
> What sorts of URI change? \
> *URIs don't change: people change them.*

In a related article, [URL as
UI](http://www.useit.com/alertbox/990321.html "URL as UI"), usability
expert [Jakob Nielsen](http://www.useit.com/jakob/ "Jakob Nielsen")
lists the following criteria for a usable site:

-   a **domain name** that is easy to remember and easy to spell
-   **short** URLs
-   **easy-to-type** URLs
-   URLs that visualize the **site structure**
-   URLs that are "**hackable**" to allow users to move to higher levels
    of the information architecture by hacking off the end of the URL
-   **persistent URLs** that don't change

The permanence of URLs is a fundamental trait of the web that seems to
run counter to one of the benefits of using a feature like ASP.NET
Routing. For example, one benefit of routing is you can change a route
from `{controller}/{action}/{id}` to `{controller}/{id}/{action}` and
have every URL in your site corresponding to that route automatically be
updated.

This is very nice during development when you’re still fleshing out your
URLs and haven’t committed to anything, but once you’ve published your
site, changing a route URL violates the sacred trait of URL permanence.

This is exactly where I find myself with
[Subtext](http://subtextproject.com/ "Subtext Blog Engine Project Homepage").
All of our existing URLs end with the .aspx extension, a practice which
[Jon
Udell](http://blog.jonudell.net/2008/01/17/aspx-considered-harmful/ "http://blog.jonudell.net/")
convincingly [argued is
harmful](http://blog.jonudell.net/2008/01/17/aspx-considered-harmful/ ".aspx considered harmful").
In the upcoming version of Subtext, we’re moving to extensionless URLs
by building upon the great support built into ASP.NET 4 and Routing.

I could simply change our routes to remove the .aspx extension, but that
would break nearly every existing URL in every blog running on Subtext.
So much for URL permanence, right?

There’s a Better Way
--------------------

Rather than changing routes, what I really want is a way to simply
redirect the existing route to a new route. This is pretty easy, but
there are a few caveats to keep in mind that make it non-trivial.

1.  Since you don’t want to generate URLs for the old route, the legacy
    route should never be selected for URL generation. It’s only for
    matching incoming requests.
2.  The legacy route should be registered after the new URL to ensure it
    doesn’t accidentally match and supersede the new URL.

I wrote a library that provides a `RedirectRoute` and a simple extension
method for registering a `RedirectRoute` that satisfies these
conditions. Let’s look at an example of how it would be used.

Let’s suppose we have the following route defined and the site has been
published to the web..

```csharp
routes.MapRoute("old", "foo/{controller}/{action}/{id}");
```

But later, we decide we want all such URLs to start with */bar* instead
and we want to re-order the *id* and *action* segments of the URL.

Here’s an example of how we can do that using this new library.

```csharp
var route = routes.MapRoute("new", "bar/{controller}/{id}/{action}");
routes.Redirect(r => r.MapRoute("old", "foo/{controller}/{action}/{id}"))  .To(route);
```

This snippet registers the new route and passes that route to the
`RedirectRoute` that was returned by a call to the `Redirect` extension
method. The `RedirectRoute` delegates to the old route to match incoming
requests. With this in place, every request matching the old route will
be redirected to the new route.

Thus a request for */foo/home/index/123* will be redirected to
*/bar/home/123/index*.

Why The Lambda Expression?
--------------------------

To fully understand what’s going on under the hood, I need to explain
why the API takes in a lambda expression rather than simply taking in
two routes, old route and new route.

Let’s suppose that the API did just that, simply accepted two routes.
Here’s what a naïve attempt to use the method might look like.

```csharp
var new = routes.MapRoute("new", "bar/{controller}/{id}/{action}");
var old = routes.MapRoute("new", "foo/{controller}/{action}/{id}");
routes.Redirect(old).To(new);
```

Hopefully it’s immediately apparent why this is not good. The old route
is mapped *before* the redirect route. So the redirect route will never
be matched. 

The `MapRoute` extension method not only creates a route, but it adds it
to the route collection. So we could have manually created the route,
but that’s a pain if you’re already using the `MapRoute` method to
create the route. Or, we could have done this:

```csharp
var new = routes.MapRoute("new", "bar/{controller}/{id}/{action}");
var throwAway = new RouteCollection();
var old = throwAway.MapRoute("new", "foo/{controller}/{action}/{id}");
routes.Redirect(old).To(new);
```

Requiring the user of the API to create a throwaway route collection is
ugly when the API itself could do it for you. Hence the lambda
expression argument to `Redirect`. Internally, the method creates a
throwaway route collection and calls the expression against that instead
of against the main route collection.

Implementation Details
----------------------

I won’t post the full source here, but the implementation details are
pretty simple. Here’s the implementation of `GetRouteData` which is the
method called when matching incoming requests.

```csharp
public override RouteData GetRouteData(HttpContextBase httpContext) {
    // Use the original route to match
    var routeData = SourceRoute.GetRouteData(httpContext);
    if (routeData == null) {
        return null;
    }
    // But swap its route handler with our own
    routeData.RouteHandler = this;
    return routeData;
}
```

Notice that I use the source route, which is the old route passed into
the redirect route, to match the request, but I swap the route handler
with the redirect route. `RedirectRoute` also implements
`IRouteHandler`. It was a little implementation shortcut I took which
happens to work fine in this case.

The implementation of `GetVirtualPath` is even simpler.

```csharp
public override VirtualPathData GetVirtualPath(RequestContext requestContext  , RouteValueDictionary values) {
    // Redirect routes never generate an URL.
    return null;
}
```

We never want to generate a URL to the old route, so this method always
returns null.

As mentioned, `RedirectRoute` implements `IRouteHandler`, so we should
look at its implementation.

```csharp
public IHttpHandler GetHttpHandler(RequestContext requestContext) {
  var requestRouteValues = requestContext.RouteData.Values;

  var routeValues = AdditionalRouteValues.Merge(requestRouteValues);

  var vpd = TargetRoute.GetVirtualPath(requestContext, routeValues);
  string targetUrl = null;
  if (vpd != null) {
    targetUrl = "~/" + vpd.VirtualPath;
    return new RedirectHttpHandler(targetUrl, Permanent, isReusable: false);
  }
  return new DelegateHttpHandler(    httpContext => httpContext.Response.StatusCode = 404, false);
}
```

Notice that we make use of the `DelegateHttpHandler` which is something
[I wrote about a while
ago](https://haacked.com/archive/2008/12/15/redirect-routes-and-other-fun-with-routing-and-lambdas.aspx "Redirect Routes and Lambda").

Where to get it?
----------------

All the code I showed here is now part of the
*[RouteMagic](https://github.com/haacked/routemagic "RouteMagic")*
library [I blogged about
recently](https://haacked.com/archive/2011/01/30/introducing-routemagic.aspx "Introducing Route Magic").
I’ve updated the package so all you need to do is *Install-Package
RouteMagic*within [NuGet](http://nuget.codeplex.com/ "NuGet").

