---
title: NuGet 1.5 Released!
date: 2011-08-30 -0800
disqus_identifier: 18811
categories:
- nuget
- code
redirect_from: "/archive/2011/08/29/nuget-1-5-released.aspx/"
---

UPDATE: We found [an issue](http://nuget.codeplex.com/workitem/1419)
with 1.5 when running behind some proxies that caused an “Arithmetic
operation resulted in an overflow” exception message and another issue
with [signed PS1
scripts](http://nuget.codeplex.com/workitem/1473 "NuGet Types issue").
We’ve now posted an update (NuGet 1.5.20902.9023) that fixes the issues.

I’m happy to announce the release of NuGet 1.5 just in time to make sure
our roadmap isn’t a liar. I won’t bore you by repeating the details of
the release, but instead direct you to the [NuGet 1.5 release
notes](http://docs.nuget.org/docs/release-notes/nuget-1.5 "NuGet 1.5 released.").

If you are running a private NuGet.Server repository, you’ll need to
update that repository the latest version of NuGet.Server to connect to
it using NuGet 1.5.

I’ve updated our roadmap to reflect what we’re [thinking about
next](http://nuget.codeplex.com/). The next release is going to focus
more on continuous integration scenarios that we’ve heard from
customers, pre-release “beta” packages and multiple UI improvements such
as allowing folks to disable package sources.

Also, on occasion, the NuGet feature team hangs out in a home-grown IRC
style web based [chat application](http://chatapp.apphb.com "Chat")
written by [David
Fowler](http://weblogs.asp.net/davidfowler/ "David Fowler") in his spare
time as a test app for
[SignalR](https://github.com/SignalR/SignalR "SignalR on Github"). We’re
contemplating the idea of making it a more regular thing if possible.

Tags: [nuget](https://haacked.com/tags/nuget/default.aspx),
[oss](https://haacked.com/tags/oss/default.aspx), [open
source](https://haacked.com/tags/open+source/default.aspx), [package
manager](https://haacked.com/tags/package+manager/default.aspx)

