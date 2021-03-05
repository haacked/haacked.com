---
title: Named Routes To The Rescue
tags: [aspnet,aspnetmvc,code]
redirect_from: "/archive/2010/11/20/named-routes-to-the-rescue.aspx/"
---

> The beginning of wisdom is to call things by their right names –
> Chinese Proverb

Routing in ASP.NET doesn’t require that you name your routes, and in
many cases it works out great. When you want to generate an URL, you
grab this bag of values you have lying around, hand it to the routing
engine, and let it sort it all out.

[![nameless-routes](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Named-Routes-Are-A-Good-Thing_12C7A/nameless-routes_3.jpg "nameless-routes")](http://www.sxc.hu/photo/626043 "Traffic sign in London (blank) by José A. Warletta from sxc.hu")

For example, suppose an application has the following two routes defined

```csharp
routes.MapRoute(
    name: "Test",
    url: "code/p/{action}/{id}",
    defaults: new { controller = "Section", action = "Index", id = "" }
);

routes.MapRoute(
    name: "Default",
    url: "{controller}/{action}/{id}",
    defaults: new { controller = "Home", action = "Index", id = "" }
);
```

To generate a hyperlink to each route, you’d write the following code.

```csharp
@Html.RouteLink("Test", new {controller="section", action="Index", id=123})

@Html.RouteLink("Default", new {controller="Home", action="Index", id=123})
```

Notice that these two method calls don’t specify which route to use to
generate the links. They simply supply some route values and let ASP.NET
Routing figure it out.

In this example, The first one generates a link to the URL
*/code/p/Index/123 a*nd the second to */Home/Index/123*which should
match your expectations.

This is fine for these simple cases, but there are situations where this
can bite you. ASP.NET 4 introduced the ability to use routing to [route
to a Web Form
page](http://weblogs.asp.net/scottgu/archive/2009/10/13/url-routing-with-asp-net-4-web-forms-vs-2010-and-net-4-0-series.aspx "Page Routing"). 
Let’s suppose I add the following page route at the beginning of my list
of routes so that the URL */static/url* is handled by the page
*/aspx/SomePage.aspx*.

```csharp
routes.MapPageRoute("new", "static/url", "~/aspx/SomePage.aspx"); 
```

Note that I can’t put this route at the end of my list of routes because
it would never match incoming requests since */static/url* would match
the default route. Adding it to the beginning of the list seems like the
right thing to do here.

If you’re not using Web Forms, you still might run into a case like this
if you use routing with a custom route handler, such as the one I
[blogged about a while ago (with source
code)](https://haacked.com/archive/2009/11/04/routehandler-for-http-handlers.aspx "Route Handler for Http Handlers").
In that blog post, I showed how to use routing to route to standard
`IHttpHandler` instances.

Seems like an innocent enough change, right? For incoming requests, this
route will only match requests that exactly matches */static/url* but no
others, which is great. But if I look at my page, I’ll find that the two
URLs I generated earlier are broken.

Now, the two URLs are*/url?controller=section&action=Index&id=123*and
*/static/url?controller=Home&action=Index&id=123*.

WTF?!

This is running into a subtle behavior of routing which is admittedly
somewhat of an edge case, but is something that people run into from
time to time. In fact, I had to help [Scott
Hanselman](http://hanselman.com/ "Scott Hanselman") with such an issue
when he was preparing his [Metaweblog example for his fantastic PDC
talk](http://player.microsoftpdc.com/Session/e0c3ce51-9869-456c-a197-63dc0283f57e "ASP.NET + Packaging + Open Source = Crazy Delicious")
([HD quality
MP4](http://videoaz.microsoftpdc.com/vod/downloads/FT01_High.mp4 "Download the HD MP4 file")).

Typically, when you generate a URL using routing, the route values you
supply are used to “fill in” the URL parameters. In case you don’t
remember, URL parameters are those placeholders within a route’s URL
with the curly braces such as *{controller}* and *{action}*.

So when you have a route with the URL *{controller}*/*{action}/{Id}*,
you’re expected to supply values for *controller*, *action*, and *Id*
when generating a URL. During URL generation, you need to supply a route
value for each URL parameter so that an URL can be generated. If every
route parameter is supplied with a value, that route is considered a
match for the purposes of URL generation. If you supply extra parameters
above and beyond the URL parameters for the route, those extra values
are appended to the generated URL as query string parameters.

In this case, since the new route I mapped doesn’t have any URL
parameters, that route matches *every* URL generation attempt since
technically, “a route value is supplied for each URL parameter.” It just
so happens in this case there are no URL parameters. That’s why all my
existing URLs are broken because every attempt to generate a URL now
matches this new route.

There’s even more details I’ve glossed over having to do with how a
route’s default values figure into URL generation. That’s a topic for
another time, but it explains why you don’t run into this problem with
routes to controller actions which have an URL without parameters.

This might seem like a big problem, but the fix is actually very simple.
**Use names for all your routes and always use the route name when
generating URLs**. Most of the time, letting routing sort out which
route you want to use to generate an URL is really leaving it to chance.
When generating an URL, you generally know exactly which route you want
to link to, so you might as well specify it by name.

Also, by specifying the name of the route, you avoid ambiguities and may
even get a bit of a performance improvement since the routing engine can
go directly to the named route and attempt to use it for URL generation.

So in the sample above where I have code to generate the two links, the
following change fixes the issue (I changed the code to use named
parameters to make it clear what the change was).

```csharp
@Html.RouteLink(
    linkText: "route: Test", 
    routeName: "test", 
    routeValues: new {controller="section", action="Index", id=123}
)

@Html.RouteLink(
    linkText: "route: Default", 
    routeName: "default", 
    routeValues: new {controller="Home", action="Index", id=123}
)
```

> People's fates are simplified by their names.  \~Elias Canetti

And the same goes for routing.
![Smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Named-Routes-Are-A-Good-Thing_12C7A/wlEmoticon-smile_2.png)

