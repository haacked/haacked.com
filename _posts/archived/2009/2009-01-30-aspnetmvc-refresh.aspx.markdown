---
title: ASP.NET MVC RC Refresh
tags: [aspnetmvc,aspnet]
redirect_from: "/archive/2009/01/29/aspnetmvc-refresh.aspx/"
---

Hello there. :)

On Tuesday, we [announced the release
candidate](https://haacked.com/archive/2009/01/27/aspnetmvc-release-candidate.aspx "ASP.NET MVC RC")
for [ASP.NET MVC](http://asp.net/mvc "ASP.NET MVC Website"). While there
is much new in there to be excited about and many many bug fixes, there
were two changes introduced in the RC that broke some scenarios which
previously worked in the Beta, as reported by customers.

We’ve updated the Release Candidate with a refresh that addresses these
two issues. You can use the recently released [**Microsoft Web Platform
Installer
1.0**](http://www.microsoft.com/web/channel/products/WebPlatformInstaller.aspx "Microsoft Web Platform Installer")
to install ASP.NET MVC RC Refresh. It happens to be a handy tool for
installing not just ASP.NET MVC, but everything you might need to use
ASP.NET MVC such as Visual Web Developer Express 2008 SP1.

The link on the [official download
page](http://go.microsoft.com/fwlink/?LinkID=140768&clcid=0x409 "ASP.NET MVC Release Candidate")
appears to be updated with the new MSI (we pushed it out yesterday), but
we’ve experienced some odd proxy caching issues etc where some people
were still getting the old MSI.

In order to be safe, you can get the download directly from [**this
download
link**](http://go.microsoft.com/fwlink/?LinkID=141184&clcid=0x409 "ASP.NET MVC RC Download Link")**.**I
don’t anticipate any problems with that link, but being paranoid, the
way to fully ensure you have the refresh is to right click on the
downloaded file, select the Digital Signatures tab, and make sure the
Time Stamp says Wednesday, January 28 and not Friday, January 23.

![msi-props](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/RegressionsinASP.NETMVCRC_CB6C/msi-props_5.png "msi-props")

**Don’t forget to fully uninstall the previous RC before installing this
one.**

Also note that this refresh does not address the [Controls Collection
Cannot Be Modified
issue](https://haacked.com/archive/2009/01/27/controls-collection-cannot-be-modified-issue-with-asp.net-mvc-rc1.aspx "Controls Collection Cannot Be Modified")
I reported recently. We will address that soon. The following describes
the issues that this refresh does fix.

The first change was that we changed our helper methods that generate
URLs and links to now generate relative paths instead of absolute paths.
This caused problems with several AJAX scenarios.

The fix to this in the refresh was to roll back that check-in so the
behavior went back to the way it was in the Beta. We will not use
relative paths, and we have no intention of re-introducing the
generation of relative paths. Because we rolled back this change to a
known good state, we feel very confident in the current behavior. URL
generation should work the same way it did back in the Beta.

The other regression is that in some cases, the `RouteUrl` (and thus
`RouteLink`) methods return an empty string when you specify a route
name, but the route has default parameters which are not parameters in
the URL.

For example, if you have the following route:

```csharp
routes.MapRoute("route-name", 
  "foo/bar", 
  new {controller="Home", action="index"});
```

Notice that *controller* has default value, but is not part of the URL.
If you then specify:

```aspx-cs
<%= Url.RouteUrl("route-name") %>
```

You might expect that it would use that route to render the URL, but it
doesn’t. This bug was introduced when we refactored all our url
generating helpers to call into a common method. It turns out, however,
that our `RouteUrl` methods (aka non-MVC specific) should have subtly
different behavior than the MVC specific methods (such as `Action`). We
added a flag to the common method so that this difference is taken into
consideration. This was a fix that did not have a large surface area.

Another thing we did was update all our test cases (both unit tests and
automated functional tests) with scenarios reported to us by customers.
So now, we have these types of cases well covered.

If you downloaded the Release Candidate before today, you should check
the digital signature timestamp as I described earlier and if it’s the
old one, I recommend you go and download the refresh via the [**Web
Platform
Installer**](http://www.microsoft.com/web/channel/products/WebPlatformInstaller.aspx "Web Platform Installer")
or directly at the **[URL I mentioned
earlier](http://go.microsoft.com/fwlink/?LinkID=141184&clcid=0x409 "ASP.NET MVC Release Candidate 1 Refresh")**.

As I mentioned before, we are very excited about this release and hope
that you are enjoying writing code with it. :)

