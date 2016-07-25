---
layout: post
title: Make Routing Ignore Requests For A File Extension
date: 2008-07-14 -0800
comments: true
disqus_identifier: 18503
categories:
- asp.net mvc
- asp.net
redirect_from: "/archive/2008/07/13/make-routing-ignore-requests-for-a-file-extension.aspx/"
---

By default, ASP.NET Routing ignores requests for files that do ~~not~~
exist on disk. I explained the reason for this in a [previous post on
upcoming routing
changes](http://haacked.com/archive/2008/04/10/upcoming-changes-in-routing.aspx "Upcoming Routing Changes").

Long story short, we didn’t want routing to attempt to route requests
for static files such as images. Unfortunately, this caused us a
headache when we remembered that many features of ASP.NET make requests
for .axd files which do not exist on disk.

To fix this, we included a new extension method on `RouteCollection`,
`IgnoreRoute`, that creates a Route mapped to the
`StopRoutingHandler `route handler (class that implements
`IRouteHandler`). Effectively, any request that matches an “ignore
route” will be ignored by routing and normal ASP.NET handling will occur
based on existing http handler mappings.

Hence in our default template, you’ll notice we have the following route
defined.

```csharp
routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
```

This handles the standard *.axd* requests.

However, there are other cases where you might have requests for files
that don’t exist on disk. For example, if you register an HTTP Handler
directly to a type that implements `IHttpHandler`. Not to mention
requests for *favicon.ico* that the browser makes automatically. ASP.NET
Routing attempts to route these requests to a controller.

One solution to this is to add an appropriate ignore route to indicate
that routing should ignore these requests. Unfortunately, we can’t do
something like this: `{*path}.aspx/{*pathinfo}`

We only allow one catch-all route and it must happen at the end of the
URL. However, you can take the following approach. In this example, I
added the following two routes.

```csharp
routes.IgnoreRoute("{*allaspx}", new {allaspx=@".*\.aspx(/.*)?"});
routes.IgnoreRoute("{*favicon}", new {favicon=@"(.*/)?favicon.ico(/.*)?"});
```

What I’m doing here is a technique
[Eilon](http://weblogs.asp.net/leftslipper/ "Eilon Lipton's Blog")
showed me which is to map all URLs to these routes, but then restrict
which routes to ignore via the constraints dictionary. So in this case,
these routes will match (and thus *ignore*) all requests for
*favicon.ico* (no matter which directory) as well as requests for a
*.aspx* file. Since we told routing to ignore these requests, normal
ASP.NET processing of these requests will occur.

Technorati Tags:
[aspnetmvc](http://technorati.com/tags/aspnetmvc),[routing](http://technorati.com/tags/routing)

