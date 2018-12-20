---
title: ASP.NET MVC Release Candidate
date: 2009-01-27 -0800
disqus_identifier: 18579
tags:
- asp.net mvc
- asp.net
redirect_from: "/archive/2009/01/26/aspnetmvc-release-candidate.aspx/"
---

At long last I am happy, relieved, excited to announce the release
candidate for [ASP.NET MVC](http://asp.net/mvc "ASP.NET MVC Website").
Feel free to **[go download it
now](http://go.microsoft.com/fwlink/?LinkID=140768&clcid=0x409 "ASP.NET MVC RC Download Page")**.
I’ll wait right here patiently.

There have been a lot of improvements made since the Beta release so be
sure to **[read the release
notes](http://go.microsoft.com/fwlink/?LinkID=137661&clcid=0x409 "ASP.NET MVC RC Release Notes")**.
I’ve tried very hard to be thorough in the notes so do let me know if
anything is lacking. We are also pushing new tutorials up to the
[ASP.NET MVC Website](http://asp.net/mvc "ASP.NET MVC Website") as I
write this.

Also, don’t miss
[ScottGu](http://weblogs.asp.net/scottgu/ "Scott Guthrie")’s usual
**[epic blog
post](http://weblogs.asp.net/scottgu/archive/2009/01/27/asp-net-mvc-1-0-release-candidate-now-available.aspx "Release Candidate Now Available")**
describing the many improvements. There’s also a [post on the Web Tools
team
blog](http://blogs.msdn.com/webdevtools/archive/2009/01/27/overview-of-mvc-tools-features.aspx "Web Tools Team Blog")
covering tooling aspects of this release in detail. As I mentioned when
[we released the
Beta](https://haacked.com/archive/2008/10/16/aspnetmvc-beta-release.aspx "ASP.NET MVC Beta Released"),
we didn’t have plans for many new features in the runtime, but we did
have a lot of tooling improvements to add. I’ve already described some
of these changes in a [previous blog
post](https://haacked.com/archive/2008/12/19/a-little-holiday-love-from-the-asp.net-mvc-team.aspx "Holiday Love"),
as did ScottGu in his [detailed
look](http://weblogs.asp.net/scottgu/archive/2008/12/19/asp-net-mvc-design-gallery-and-upcoming-view-improvements-with-the-asp-net-mvc-release-candidate.aspx "Upcoming View Improvements").

Our goal with this release was to fix all outstanding bugs which we felt
were real showstoppers and try to address specific customer concerns. We
worked hard to add a bit of spit and polish to this release.

Unfortunately, a few minor bugs did crop up at the last second, but we
decided we could continue with this RC and fix the bugs afterwards as
the impact appear to be relatively small and they all have workarounds.
I wrote about [one particular
bug](https://haacked.com/archive/2009/01/27/controls-collection-cannot-be-modified-issue-with-asp.net-mvc-rc1.aspx "Controls Collection Modified")
so that you’re aware of it.

For now, I want to share a few highlights. This is not an exhaustive
list at all. For that, check out Scott’s post and the release notes.

Ajax
----

Yes, I do know that jQuery released a [new version
(1.3.1)](http://docs.jquery.com/Release:jQuery_1.3.1 "jQuery 1.3.1"),
and no, it is not in this release. :) We just didn’t have time to
include it due to the timing of its release. However, we are performing
due diligence right now and plan to include it with the RTM.

We did make some changes to our Ajax helpers to recognize the standard
`X-Requested-With` HTTP header used by the major JavaScript libraries
such as Prototype.js, jQuery, and Dojo. Thus the `IsMvcAjaxRequest`
method was renamed to `IsAjaxRequest` and looks for this header rather
than our custom one.

ControllerContext
-----------------

`ControllerContext` no longer inherits from `RequestContext`, which will
improve testing and extensibility scenarios. We would have liked to make
changes to `RequestContext`, but it was introduced as part of the .NET
Framework in ASP.NET 3.5 SP1. Thus we can’t change it in our out-of-band
release.

Anti Forgery Helpers
--------------------

These helpers were previously released in our “futures” assembly, but
we’ve fixed a few bugs and moved them into the core ASP.NET MVC
assembly.

These are helpers which help mitigate [Cross-Site Request
Forgery](http://en.wikipedia.org/wiki/Cross-site_request_forgery "Cross-site request forgery on Wikipedia")
(CSRF) attacks. For a great description of these helpers, check out
[Steve Sanderson’s blog post on the
topic](http://blog.codeville.net/2008/09/01/prevent-cross-site-request-forgery-csrf-using-aspnet-mvcs-antiforgerytoken-helper/ "Prevent Cross-Site Request Forgery").

MVC Futures
-----------

I added a couple of expression based helpers to the [ASP.NET MVC futures
assembly](http://www.codeplex.com/aspnet/Release/ProjectReleases.aspx?ReleaseId=22359 "ASP.NET MVC RC 1 Futures"),
`Microsoft.Web.Mvc.dll`. These are just samples to demonstrate how one
could write such helpers. I’ll add a few more by the time we ship the
RTM. Note, if you’re using the old futures assembly, it won’t work with
the new RC. You’ll need to update to the new Futures assembly.

**In case you missed it the first time,**[**click here for the
Download**](http://go.microsoft.com/fwlink/?LinkID=140768&clcid=0x409 "ASP.NET MVC RC Download Page").

