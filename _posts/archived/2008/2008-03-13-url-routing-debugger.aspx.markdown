---
title: ASP.NET Routing Debugger
tags: [aspnet,aspnetmvc,routing]
redirect_from: "/archive/2008/03/12/url-routing-debugger.aspx/"
---

*UPDATE: I’ve added a [NuGet
package](https://haacked.com/archive/2010/10/06/introducing-nupack-package-manager.aspx)
named "routedebugger" to the NuGet feed, which will make it much easier
to install.*

*UPDATE 2: In newer versions of the NuGet package you don't need to add code to `global.asax` as described below. An appSetting `<add key="RouteDebugger:Enabled" value="true" />` in `web.config` suffices.*

In [Scott Hanselman’s](http://www.hanselman.com/blog/ "Scott Hanselman")
wonderful [talk at
Mix](http://sessions.visitmix.com/?selectedSearch=T22 "Developing ASP.NET MVC Applications Screencast"),
he demonstrated a simple little route tester I quickly put together.

[![Route Debugger
Screenshot](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UrlRoutingDebugger_EEBA/route-debugger-screenshot.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UrlRoutingDebugger_EEBA/route-debugger-screenshot.png)

This utility displays the route data pulled from the request of the
current request in the address bar. So you can type in various URLs in
the address bar to see which route matches. At the bottom, it shows a
list of all defined routes in your application. This allows you to see
which of your routes would match the current URL.

The reason this is useful is sometimes you expect one route to match,
but another higher up the stack matches instead. This will show you that
is happening. However, it doesn’t provide any information *why* that is
happening. Hopefully we can do more to help that situation in the
future.

To use this, simply download the [following zip
file](http://code.haacked.com/mvc-1.0/RouteDebug-Binary.zip "RouteDebug.dll")
and place the assembly inside of it into your bin folder. Then in your
Global.asax.cs file add one line to the `Application_Start` method (in
bold).

```csharp
protected void Application_Start(object sender, EventArgs e)
{
  RegisterRoutes(RouteTable.Routes);
  RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
}
```

This will update the route handler (`IRouteHandler`) of all your routes
to use a `DebugRouteHandler `instead of whichever route handler you had
previously specified for the route. It also adds a catch-all route to
the end to make sure that the debugger always matches any request for
the application.

I’m also making available the [**full
source**](http://code.haacked.com/mvc-1.0/RouteTesterDemo.zip "Route Tester Demo")
(using the word *full* makes it sound like there’s a lot, but there’s
not all that much) and a demo app that makes use of this route tester.
Let me know if this ends up being useful or not for you.
