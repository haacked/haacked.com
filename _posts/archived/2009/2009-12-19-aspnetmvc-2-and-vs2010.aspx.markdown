---
title: ASP.NET MVC 2 and Visual Studio 2010
tags: [aspnet,aspnetmvc,code]
redirect_from: "/archive/2009/12/18/aspnetmvc-2-and-vs2010.aspx/"
---

When we [released ASP.NET MVC 2
Beta](https://haacked.com/archive/2009/11/17/asp.net-mvc-2-beta-released.aspx "ASP.NET MVC 2 Beta Released")
back in November, I addressed the issue of support for Visual Studio
2010 Beta 2.

> Unfortunately, because Visual Studio 2010 Beta 2 and ASP.NET MVC 2
> Beta share components which are currently not in sync, running ASP.NET
> MVC 2 Beta on VS10 Beta 2 is not supported.

The [release candidate for ASP.NET MVC
2](https://haacked.com/archive/2009/12/16/aspnetmvc-2-rc.aspx "ASP.NET MVC 2 RC")
does not change the situation, but I wasn’t as clear as I could have
been about what the situation is exactly. In this post, I hope to clear
up the confusion (and hopefully not add any more new confusion) and
explain what is and isn’t supported and why that’s the case.

[![why](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ASP.NETMVC2andVisualStudio2010_113C5/why_3.jpg "why")](http://www.flickr.com/photos/emagic/56206868/ "Good Question, by e-magic: CC license by attribution some rights reserved")

Part of the confusion may lie in the fact that ASP.NET MVC 2 consists of
two components, the runtime and what we call “Tooling”. The runtime is
simply the `System.Web.Mvc.dll` which contains the framework code which
you would reference and deploy as part of your ASP.NET MVC application.

The Tooling consists of the installer, the project templates, and all
the features in Visual Studio such as the *Add View* and *Add Area*
dialogs. Much as ASP.NET 4 is different from Visual Studio 2010, the
ASP.NET MVC 2 tooling is different from the runtime. The difference is
that we primarily release both components in one package.

The reason I bring this up is to point out that when I said that ASP.NET
MVC 2 RC is not supported on machines with Visual Studio 2010 Beta 2,
I’m really referring to the *tooling*.

**The ASP.NET MVC 2 RC runtime is fully supported with the ASP.NET 4
runtime.** As I [mentioned
before](https://haacked.com/archive/2009/11/03/html-encoding-nuggets-aspnetmvc2.aspx "Html Encoding Nuggets with ASP.NET MVC 2"),
we are not compiling a different version of the runtime for ASP.NET 4.
It’s the same runtime. So you can create an ASP.NET empty web
application project, for example, add in the RC of `System.Web.Mvc.dll`
as a reference, and go to town.

The problem of course is that you won’t have the full tooling experience
at your disposal in VS2010 such as project templates and dialogs. This
is definitely a pain point and very unfortunate.

### Why don’t we ship updated installers for Visual Studio 2010 Beta 2?

This is a fair question. What it comes down to is that this would add a
lot of extra work for our small team, and we’re already working hard to
release the core features we have planned for this release.

Add this extra work and something would have to give. It would have to
come at the cost of feature work and bug fixes and we felt those were a
higher priority than temporary support for interim releases of VS2010.

Why would this add overhead? [Eilon
Lipton](http://weblogs.asp.net/leftslipper/ "Eilon Lipton's Blog"), lead
developer on the ASP.NET MVC feature team, covers this well in [his
comment on my last
post](https://haacked.com/archive/2009/12/16/aspnetmvc-2-rc.aspx#75341 "Eilon's Comment").

> Regarding Visual Studio 2010 and .NET 4 support, that is unfortunately
> not a feasible option. The most recent public release of VS2010 and
> .NET 4 is Beta 2. However, our internal builds of MVC 2 for VS2010 and
> .NET 4 depend on features that were available only after Beta 2. In
> other words, if we released what we have right now for VS2010 and .NET
> 4 then it wouldn't even run.

We are constantly syncing our internal builds with the latest builds of
Visual Studio 2010. As Eilon points out, to support VS 10 Beta 2, we’d
have to have two separate builds for VS10, one for Beta 2 and one for
the latest internal build. Keep in mind, this is on top of the build for
Visual Studio 2008 we’re doing.

Trying to sync our tooling against two different versions of Visual
Studio is hard enough. Doing it against three makes it much more
difficult.

As I mentioned before, the ASP.NET MVC 2 project schedule isn’t aligned
with the Visual Studio 2010 schedule exactly. Heck, when ASP.NET MVC 1.0
was shipped, work on VS 10 was already underway, so we were playing
catch-up to catch the VS 10 ship train. Thus when the Beta 2 was code
complete, we weren’t done with our Beta. When we were done with Beta, we
were already building our tools against a newer build of VS10. The same
thing applies to the RC.

### What about Visual Studio 2010 RC?

Funny thing is, since I’ve been on leave, I pretty much found out that
we were even having a public Release Candidate for Visual Studio 2010
the same time you probably did via [ScottGu’s Blog post on the
subject](http://weblogs.asp.net/scottgu/archive/2009/12/17/visual-studio-2010-and-net-4-0-update.aspx "VS2010 and .NET 4 update").

**The good news is that the Visual Studio 2010 Release Candidate will
include a newer version of ASP.NET MVC 2.** We’re still working out the
details of which exact version we will include, though I’d really like
it to be the RC of ASP.NET MVC, assuming the logistics and schedule line
up properly.

And of course, the Visual Studio 2010 RTM will include the ASP.NET MVC 2
RTM and at that point, all will be well with the world as installing
tooling for ASP.NET MVC 2 will be supported on both VS 2008 and VS 2010
at the same time.

So again, we do understand this is an unfortunate situation and
apologize for the inconvenience this may cause some of you. Aftor all, I
feel the same pain! I want to install both versions on my machine just
like you do. Fortunately, it’s only a temporary situation and will all
be a bad memory when VS2010 RTM is released to the world. Thanks for
listening.

