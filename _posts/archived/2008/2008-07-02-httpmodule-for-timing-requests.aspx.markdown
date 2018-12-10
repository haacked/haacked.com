---
layout: post
title: HttpModule For Timing Requests
date: 2008-07-02 -0800
comments: true
disqus_identifier: 18501
categories: [aspnetmvc]
redirect_from: "/archive/2008/07/01/httpmodule-for-timing-requests.aspx/"
---

Yesterday, I wrote a quick and dirty ASP.NET `HttpModule` for displaying
the time that a request takes to process. Note that by turning on [trace
output for a
page](http://msdn.microsoft.com/en-us/library/94c55d08.aspx "Enable tracing for an ASP.NET page"),
you can get timing information for that page. But as far as I
understand, and I need to double check this, this only applies to the
page lifecycle, which might not have all the information you want in the
context of ASP.NET MVC.

Not to mention, I just wanted to see a simple number at the end of the
page and not have to wade through all that trace output. Also keep in
mind that this number only applies to the time spent in the ASP.NET
pipeline. It obviously doesn’t tell you the full time of the request
from browser sending the request to the browser rendering the response.
For that I would use something like Firebug in Firefox.

Here’s the code for the module. Note that it only works from local
requests in Debug mode. That’s a safety precaution so that if someone
accidentally deploys this to a production machine, they won’t see this
number at the bottom.

```csharp
using System;
using System.Diagnostics;
using System.Web;

public class TimingModule : IHttpModule {
  public void Dispose() {
  }

  public void Init(HttpApplication context) {
    context.BeginRequest += OnBeginRequest;
    context.EndRequest += OnEndRequest;
  }

  void OnBeginRequest(object sender, System.EventArgs e) {
    if (HttpContext.Current.Request.IsLocal 
        && HttpContext.Current.IsDebuggingEnabled) {
      var stopwatch = new Stopwatch();
      HttpContext.Current.Items["Stopwatch"] = stopwatch;
      stopwatch.Start();
    }
  }

  void OnEndRequest(object sender, System.EventArgs e) {
    if (HttpContext.Current.Request.IsLocal 
        && HttpContext.Current.IsDebuggingEnabled) {
      Stopwatch stopwatch = 
        (Stopwatch)HttpContext.Current.Items["Stopwatch"];
      stopwatch.Stop();

      TimeSpan ts = stopwatch.Elapsed;
      string elapsedTime = String.Format("{0}ms", ts.TotalMilliseconds);

      HttpContext.Current.Response.Write("<p>" + elapsedTime + "</p>");
    }
  }
}
```

Notice that I made use of the
`System.Diagnostics.Stopwatch`[](http://msdn.microsoft.com/en-us/library/system.diagnostics.stopwatch.aspx "MSDN Documentation for Stopwatch Class")
class, which provides more accuracy than simply calling taking the
difference between two calls to `DateTime.Now`.

In my web.config, I just needed to add the following to the httpModules
section (replacing *Namespace* and *AssemblyName* with their appropriate
values):

```csharp
<httpModules>
  <!-- ...other modules... -->
  <add name="TimingModule" type="Namespace.TimingModule, AssemblyName" />
</httpModules>
```

For IIS 7, this configuration would go in the \<modules /\> section.

Lastly, don’t forget to set your website to debug mode by adding the
following in your Web.config. If you want to test your perf in release
mode, just remove the `IsDebuggingEnabled` clause in the module and you
don’t need to make the following change.

```csharp
<compilation debug="true">
<!-- ... -->
</compilation>
```

Hope you find this useful.

