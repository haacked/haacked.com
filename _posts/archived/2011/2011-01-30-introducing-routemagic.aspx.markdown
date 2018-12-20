---
title: Introducing RouteMagic
date: 2011-01-30 -0800
tags:
- aspnet
- code
- aspnetmvc
- oss
redirect_from: "/archive/2011/01/29/introducing-routemagic.aspx/"
---

Over the past couple of years, I’ve written [several blog posts on
ASP.NET
Routing](https://haacked.com/tags/Routing/default.aspx "Blog Posts Tagged with 'Routing'")
where I provided various extensions to routing. Typically such blog
posts included a zip download of the binaries and source code to allow
readers to easily try out the code.

But that’s always been a real pain and most people don’t bother. But
now, there’s [a better
way](http://nuget.codeplex.com/ "A Better Way To Share Code") to share
such code. Moving forward, I’ll be using NuGet packages as a means of
sharing my code samples.

In the case of my routing extensions, I’ve compiled them into a solution
I call *RouteMagic* ([source is available on
GitHub](https://github.com/haacked/routemagic "RouteMagic")). This
solution includes two packages, *RouteMagic.Mvc* (extensions specific to
ASP.NET MVC Routing) and *RouteMagic* (more general ASP.NET Routing
extensions). The RouteMagic.Mvc package depends on the RouteMagic
package.

These packages are available in the
[NuGet](http://nuget.codeplex.com/ "NuGet feed") feed!

After installing the *RouteMagic.Mvc* package, you’ll have the
following  features available to you.

-   [HttpHandler
    Routing](https://haacked.com/archive/2009/11/04/routehandler-for-http-handlers.aspx "Route Handler for IHttpHandler")
-   [Delegate
    routing](https://haacked.com/archive/2008/12/15/redirect-routes-and-other-fun-with-routing-and-lambdas.aspx "Delegate Routing")
-   [Group
    Routing](https://haacked.com/archive/2010/12/02/grouping-routes-part-1.aspx "Group Routing")
-   [Editable
    Routes](https://haacked.com/archive/2010/01/17/editable-routes.aspx "Editable Routes")

The source code for the solution contains the following projects:

-   RouteMagic
-   RouteMagic.Mvc
-   RouteMagic.Demo.Web (ASP.NET MVC Web application used to demo these
    features)
-   UnitTests

This is just a pet project I put together based on various blog posts
I’ve written. I’d love to see some of these ideas eventually make it
into the Framework. But until then, you’ll probably see these things
make it into
Subtext for
sure!

