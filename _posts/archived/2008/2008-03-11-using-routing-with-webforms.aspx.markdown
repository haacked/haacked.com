---
layout: post
title: Using Routing With WebForms
date: 2008-03-11 -0800
comments: true
disqus_identifier: 18463
categories: [routing aspnet aspnetmvc]
redirect_from: "/archive/2008/03/10/using-routing-with-webforms.aspx/"
---

UPDATE: I updated the sample to work with the final version of ASP.NET
Routing included with ASP.NET 3.5 SP1. This sample is now being hosted
on CodePlex.

**[Download the demo
here](http://www.codeplex.com/aspnet/Release/ProjectReleases.aspx?ReleaseId=13576 "WebFormRoutingDemo")**

In [my last
post](https://haacked.com/archive/2008/03/10/thoughts-on-asp.net-mvc-preview-2-and-beyond.aspx "My Last Post")
I described how Routing no longer has any dependency on MVC. The natural
question I’ve been asked upon hearing that is “*Can I use it with Web
Forms?*” to which I answer “*You sure can, but very carefully*.”

Being on the inside, I’ve had a working example of this for a while now
based on early access to the bits. Even so, [Chris
Cavanagh](http://chriscavanagh.wordpress.com/ "Chris Cavanagh")
impressively [beats me to the
punch](http://chriscavanagh.wordpress.com/2008/03/11/aspnet-routing-goodbye-url-rewriting/ "Goodby Url Rewriting")
in blogging his own implementation of routing for Web Forms. Nice!

> One of the obvious uses for the new routing mechanism is as a “clean”
> alternative to [URL
> rewriting](http://weblogs.asp.net/scottgu/archive/2007/02/26/tip-trick-url-rewriting-with-asp-net.aspx)
> (and possibly [custom
> VirtualPathProviders](http://weblogs.asp.net/scottgu/archive/2005/11/27/431650.aspx)
> for simple scenarios) for traditional / postback-based ASP.NET sites. 
> After a little experimentation I found some minimal steps that work
> pretty well:
>
> -   Create a custom
>     [IRouteHandler](http://weblogs.asp.net/fredriknormen/archive/2007/11/18/asp-net-mvc-framework-create-your-own-iroutehandler.aspx)
>     that instantiates your pages
> -   Register new Routes associated with your IRouteHandler
> -   That’s it!

He took advantage of the extensibility model by implementing the
`IRouteHandler` interface with his own `WebFormRouteHandler` class (not
surprisingly my implementation uses the same name) ;)

There is one subtle potential security issue to be aware of when using
routing with URL Authorization. Let me give an example.

Suppose you have a website and you wish to block unauthenticated access
to the *admin* folder. With a standard site, one way to do so would be
to drop the following web.config file in the *admin* folder...

```csharp
<?xml version="1.0"?>
<configuration>
    <system.web>
        
        <authorization>
            <deny users="*" />
        </authorization>

    </system.web>
</configuration>
```

Ok, I am a bit draconian. I decided to block access to the *admin*
directory for *all*users. Attempt to navigate to the *admin* directory
and you get an access denied error. However, suppose you use a naive
implementation of `WebFormRouteHandler` to map the URL *fizzbucket* to
the *admin* dir like so...

```csharp
RouteTable.Routes.Add(new Route("fizzbucket"
  , new WebFormRouteHandler("~/admin/secretpage.aspx"));
```

Now, a request for the URL */fizzbucket* will display *secretpage.aspx*
in the *admin* directory. This might be what you want all along. Then
again, it might not be.

In general, I believe that users of routing and Web Form will want to
secure the physical directory structure in which Web Forms are placed
using *UrlAuthorization*. One way to do this is to call
`UrlAuthorizationModule.CheckUrlAccessForPrincipal` on the actual
physical virtual path for the Web Form.

This is one key difference between Routing and URL Rewriting, routing
doesn’t actually rewrite the URL. Another key difference is that routing
provides a mean to generate URLs as well and is thus bidirectional.

The following code is my implementation of `WebFormRouteHandler `which
addresses this security issue. This class has a boolean property on it
that allows you to not apply URL authorization to the physical path if
you’d like (in following the principal of *secure by default* the
default value for this property is *true* which means it will always
apply URL authorization).

```csharp
public class WebFormRouteHandler : IRouteHandler
{
  public WebFormRouteHandler(string virtualPath) : this(virtualPath, true)
  {
  }

  public WebFormRouteHandler(string virtualPath, bool checkPhysicalUrlAccess)
  {
    this.VirtualPath = virtualPath;
    this.CheckPhysicalUrlAccess = checkPhysicalUrlAccess;
  }

  public string VirtualPath { get; private set; }

  public bool CheckPhysicalUrlAccess { get; set; }

  public IHttpHandler GetHttpHandler(RequestContext requestContext)
  {
    if (this.CheckPhysicalUrlAccess 
      && !UrlAuthorizationModule.CheckUrlAccessForPrincipal(this.VirtualPath
              ,  requestContext.HttpContext.User
              , requestContext.HttpContext.Request.HttpMethod))
      throw new SecurityException();

    var page = BuildManager
      .CreateInstanceFromVirtualPath(this.VirtualPath
        , typeof(Page)) as IHttpHandler;
      
    if (page != null)
    {
      var routablePage = page as IRoutablePage;
      if (routablePage != null)
        routablePage.RequestContext = requestContext;
    }
    return page;
  }
}
```

You’ll notice the code here checks to see if the page implements an
`IRoutablePage` interface. If your Web Form Page implements this
interface, the `WebFromRouteHandler` class can pass it the
`RequestContext`. In the MVC world, you generally get the
`RequestContext` via the `ControllerContext` property of `Controller`,
which itself inherits from `RequestContext`.

The `RequestContext` is important for calling into API methods for URL
generation. Along with the `IRoutablePage`, I provide a `RoutablePage`
abstract base class that inherits from `Page`. The code for this
interface and the abstract base class that implements it is in the
download at the end of this post.

One other thing I did for fun was to play around with fluent interfaces
and extension methods for defining simple routes for Web Forms. Since
routes with Web Forms tend to be simple, I thought this syntax would
work nicely.

```csharp
public static void RegisterRoutes(RouteCollection routes)
{
  //first one is a named route.
  routes.Map("General", "haha/{filename}.aspx").To("~/forms/haha.aspx");
  routes.Map("backdoor").To("~/admin/secret.aspx");
}
```

The general idea is that the route url on the left maps to the webform
virtual path to the right.

I’ve packaged all this up into a solution you can download and try out.
The solution contains three projects:

-   **WebFormRouting** - The class library with the
    `WebFormRouteHandler` and helpers...
-   **WebFormRoutingDemoWebApp** - A website that demonstrates how to
    use WebFormRouting and also shows off url generation.
-   **WebFormRoutingTests**- a few non comprehensive unit tests of the
    WebFormRouting library.

**WARNING**: This is prototype code I put together for educational
purposes. Use it at your own risk. It is by no means comprehensive, but
is a useful start to understanding how to use routing with Web Forms
should you wish. **[Download the demo
here](http://www.codeplex.com/aspnet/Release/ProjectReleases.aspx?ReleaseId=13576 "WebFormRoutingDemo")**.

