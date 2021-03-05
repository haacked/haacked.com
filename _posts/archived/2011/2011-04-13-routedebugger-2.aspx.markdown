---
title: RouteDebugger 2.0
tags: [aspnet,aspnetmvc,routing]
redirect_from: "/archive/2011/04/12/routedebugger-2.aspx/"
---

I’m at Mix11 all week and this past Monday, I attended the [Open Source
Fest](http://johnpapa.net/silverlight/opensourcefestannounce/ "Open Source Fest")
where multiple tables were set up for open source project owners to show
off their projects.

One of  my favorite projects is also a NuGet package named [Glimpse Web
Debugger](http://nuget.org/List/Packages/Glimpse "Glimpse"). It adds a
FireBug like experience for grabbing server-side diagnostics from an
ASP.NET MVC application while looking at it in your browser. It provides
a browser plug-in like experience without the plug-in.

One of the features of their plug-in is a route debugger inspired by my
route debugger. Over time, as Glimpse catches on, I’ll probably be able
to simply retire mine.

But in the meanwhile, inspired by their route debugger, I’ve updated my
route debugger so that it acts like tracing and puts the debug
information at the bottom of the page (*click to enlarge*).

[![About Us - Windows Internet
Explorer](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/RouteDebugger-2.0_6BC4/About%20Us%20-%20Windows%20Internet%20Explorer_thumb.png "About Us - Windows Internet Explorer")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/RouteDebugger-2.0_6BC4/About%20Us%20-%20Windows%20Internet%20Explorer_2.png)

Note that this new feature requires that you’re running against .NET 4
and that you have the Microsoft.Web.Infrastructure assembly available
(which you would in an ASP.NET MVC 3 application).

The RouteDebugger NuGet package includes the older version of
RouteDebug.dll for those still running against .NET 3.5.

This takes advantage of a new feature included in the
Microsoft.Web.Infrastructure assembly that allows you to [register an
HttpModule
dynamically](http://blog.davidebbo.com/2011/02/register-your-http-modules-at-runtime.html "Register Http Modules Dynamically").
That allows me to easily append this route debug information to the end
of every request.

By the way, RouteDebugger is now part of the [RouteMagic
project](http://routemagic.codeplex.com/ "RouteMagic") if you want to
see the source code.

To try it out, `Install-Package RouteDebugger`.
