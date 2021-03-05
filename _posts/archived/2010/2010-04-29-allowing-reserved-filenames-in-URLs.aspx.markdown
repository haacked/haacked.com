---
title: Putting the Con (COM1, LPT1, NUL, etc.) Back in your URLs
tags: [aspnet,aspnetmvc,code]
redirect_from: "/archive/2010/04/28/allowing-reserved-filenames-in-URLs.aspx/"
---

One annoyance that some developers have run into with ASP.NET MVC is
that certain reserved filenames are not allowed in URLs. Often, this is
manifested as a Bad Request error or a File Not Found (404) error.

The specifics of this restriction are accounted for in an interesting
blog post entitled [Zombie Operating Systems and ASP.NET
MVC](http://blog.bitquabit.com/2009/06/12/zombie-operating-systems-and-aspnet-mvc/).
This actually wasn’t a restriction on ASP.NET MVC but was built into the
core of ASP.NET itself.

Fortunately, ASP.NET 4 fixes this issue with a new setting. In
web.config, simply add
`<httpRuntime relaxedUrlToFileSystemMapping="true"/>` to the system.web
node. Here’s a snippet from my web.config.

```csharp
<configuration>
  <system.web>
    <httpRuntime relaxedUrlToFileSystemMapping="true"/>

    <!-- ... your other settings ... -->
  </system.web>
</configuration>
```

Here is a screenshot of it working on my machine.

![con-in-my-url](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/PuttingtheConCOM1LPT1NULe.BackinyourURLs_8819/con-in-my-url_6.png "con-in-my-url")

Now you are free to use `COM1-9, LPT1-9, AUX, PRT, NUL, CON` in your
URLs. I know you were dying to do so. :)

### What about `web.config`?

So the question comes up from time to time, “what if I want to have
`web.config` in my URL?” Why would you want that? Well if you are
[StackOverflow.com](http://stackoverflow.com/ "StackOverflow"), this
makes sense because of the tagging system which places a tag (such as
the “web.config” tag, into the URL. I’m not sure why anyone *else* would
want it. ;)

The answer is **yes, it works**.

![web.config-in-my-url](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/PuttingtheConCOM1LPT1NULe.BackinyourURLs_8819/web.config-in-my-url_5.png "web.config-in-my-url")

Please note, that you still can’t request `/web.config` because that
would try to request `web.config` in the root of your web application
and ASP.NET won’t allow that for good reason!

In fact, any request for a \**.config* file that doesn’t match a route
will fail.

While I think the vast majority of developers really won’t encounter
this issue, it’s a really improvement included in ASP.NET 4 for those
that do care.

Keep in mind that this isn’t restricted to just these special names. For
example, a URL segment ending with a dot such as the following URL
`http://example.com/version/1.0./something` will not work unless you set
the this web.config value.

