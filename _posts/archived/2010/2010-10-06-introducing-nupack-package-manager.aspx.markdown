---
title: Introducing NuGet Package Manager
tags: [aspnetmvc,code,oss,nuget]
redirect_from: "/archive/2010/10/05/introducing-nupack-package-manager.aspx/"
---

[NuGet](http://nuget.codeplex.com/ "NuGet Project on CodePlex")
([recently renamed from
NuPack](https://haacked.com/archive/2010/10/29/nupack-is-now-nuget.aspx "NuPack Renamed to NuGet"))
is a free open source developer focused package manager intent on
simplifying the process of incorporating third party libraries into a
.NET application during development.

After several months of work, the [Outercurve
Foundation](http://www.codeplex.org/ "CodePlex Foundation") (formerly
CodePlex Foundation) today announced the acceptance of the NuGet project
to the ASP.NET Open Source Gallery. This is another contribution to the
foundation by the Web Platform and Tools (WPT) team at Microsoft.

Also be sure to read [Scott Guthrie’s announcement
post](http://weblogs.asp.net/scottgu/archive/2010/10/06/announcing-nupack-asp-net-mvc-3-beta-and-webmatrix-beta-2.aspx "MVC 3")
and [Scott Hanselman’s NuGet
walkthrough.](http://www.hanselman.com/blog/IntroducingNuPackPackageManagementForNETAnotherPieceOfTheWebStack.aspx "NuPack")
There’s also a [video interview with
me](http://channel9.msdn.com/Shows/Web+Camps+TV/Web-Camps-TV-8-NuPack-with-Phil-Haack "Video Interview With Me on NuPack")
on Web Camps TV where I talk about NuGet.

![nuget-229x64](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IntroducingNuPackPackageManager_8229/nuget-229x64_556266e8-afb7-4f84-912b-c99a3f9bf742.png "nuget-229x64")Just
to warn you, the rest of this blog post is full of blah blah blah about
NuGet so if you’re a person of action, feel free to go:

- [**Download**](http://nupack.codeplex.com/releases) the latest build right away.
- Read the **[Getting Started](http://nupack.codeplex.com/wikipage?title=Getting%20Started)** page to learn how to use it.

Now back to my blabbing. I have to tell you, I’m really excited to
finally be able to talk about this in public as we’ve been incubating
this for several months now. During that time, we collaborated with
various influential members of the .NET open source community including
the Nu team in order to gather feedback on delivering the right project.

## What Does NuGet Solve?

The .NET open source community has churned out a huge catalog of useful
libraries. But what has been lacking is a widely available easy to use
manner of discovering and incorporating these libraries into a project.

Take
[ELMAH](http://code.google.com/p/elmah/ "ELMAH project page on Google Code"),
for example. For the most part, this is a very simple library to use.
Even so, it may take the following steps to get started:

1.  You first need to discover ELMAH somehow.
2.  The download page for ELMAH includes multiple zip files. You need to
    make sure you choose the correct one.
3.  After downloading the zip file, don’t forget to unblock it.
4.  If you’re really careful, you’ll verify the hash of the downloaded
    file against the hash provided by the download page.
5.  The package needs to be unzipped, typically into a lib folder within
    the solution.
6.  You’ll then add an assembly reference to the assembly from within
    the Visual Studio solution explorer.
7.  Finally, you need to figure out the correct configuration settings
    and apply them to the *web.config* file.

That’s a lot of steps for a simple library, and it doesn’t even take
into account what you might do if the library itself depends on multiple
other libraries.

NuGet automates all of these common and tedious tasks, allowing you to
spend more time using the library than getting it set up in your
project.

## NuGet Guiding Principles

I remember several months ago, Hot on the heels of shipping ASP.NET MVC
2, I was in a meeting with [Scott
Guthrie](weblogs.asp.net/scottgu "Scott Guthrie's Blog") (aka “The Gu”)
reviewing plans for ASP.NET MVC 3 when he laid the gauntlet down and
said it was time to ship a package manager for .NET developers. The
truth was, it was long overdue.

I set about doing some research looking at existing package management
systems on other platforms for inspiration such as Ruby Gems, Apt-Get,
and Maven. Package Management is well trodden ground and we have a lot
to learn from what’s come before.

After this research, I came up with a set of guiding principles for the
design of NuGet that I felt specifically addressed the needs of .NET
developers.

1.  **Works with your source code.** This is an important principle
    which serves to meet two goals: The changes that NuGet makes can be
    committed to source control and the changes that NuGet makes can be
    x-copy deployed. This allows you to install a set of packages and
    commit the changes so that when your co-worker gets latest, her
    development environment is in the same state as yours. This is why
    NuGet packages do not install assemblies into the GAC as that would
    make it difficult to meet these two goals. **NuGet doesn’t touch
    anything outside of your solution folder.** It doesn’t install
    programs onto your computer. It doesn’t install extensions into
    Visual studio. It leaves those tasks to other package managers such
    as the Visual Studio Extension manager and the Web Platform
    Installer.
2.  **Works against a well known central feed.**As part of this project,
    we plan to host a central feed that contains (or points to) NuGet
    packages. Package authors will be able to create an account and
    start adding packages to the feed. The NuGet client tools will know
    about this feed by default.
3.  **No central approval process for adding packages.** When you upload
    a package to the NuGet Package Gallery (which doesn’t exist yet),
    you won’t have to wait around for days or weeks waiting for someone
    to review it and approve it. Instead, we’ll rely on the community to
    moderate and police itself when it comes to the feed. This is in the
    spirit of how
    [CodePlex.com](http://codeplex.com/ "CodePlex.com Open Source Hosting Site")
    and [RubyGems.org](http://rubygems.org "RubyGems") work.
4.  **Anyone can host a feed.** While we will host a central feed, we
    wanted to make sure that anyone who wants to can also host a feed. I
    would imagine that some companies might want to host an internal
    feed of approved open source libraries, for example. Or you may want
    to host a feed containing your curated list of the best open source
    libraries. Who knows! The important part is that the NuGet tools are
    not hard-coded to a single feed but support pointing them to
    multiple feeds.
5.  **Command Line and GUI based user interfaces.** It was important to
    us to support the productivity of a command line based console
    interface. Thus NuGet ships with the PowerShell based Package
    Manager Console which I believe will appeal to power users.
    Likewise, NuGet also includes an easy to use GUI dialog for adding
    packages.

## NuGet’s Primary Goal

In my mind, the primary goal of NuGet is to help foster a vibrant open
source community on the .NET platform by providing a means for .NET
developers to easily share and make use of open source libraries.

As an open source developer myself, this goal is something that is near
and dear to my heart. It also reflects the evolution of open source in
DevDiv (the division I work in) as this is a product that will ship with
other Microsoft products, **but also accepts contributions**. Given the
primary goal that I stated, it only makes sense that NuGet itself would
be released as a truly open source product.

There’s one feature in particular I want to call out that’s particularly
helpful to me as an open source developer. I run an open source blog
engine called
[Subtext](http://subtextproject.com/ "Subtext Blog Engine") that makes
use of around ten to fifteen other open source libraries. Before every
release, I go through the painful process of looking at each of these
libraries looking for new updates and incorporating them into our
codebase.

With NuGet, this is one simple command: `List-Package –updates`. The
dialog also displays which packages have updates available. Nice!

And keep in mind, while the focus is on open source, NuGet works just
fine with any kind of package. So you can create a network share at
work, put all your internal packages in there, and tell your co-workers
to point NuGet to that directory. No need to set up a NuGet server.

## Get Involved!

So in the fashion of all true open source projects, this is the part
where I beg for your help. ;)

It is still early in the development cycle for NuGet. For example, the
*Add Package Dialog* is really just a prototype intended to be rewritten
from scratch. We kept it in the codebase so people can try out the user
interface workflow and provide feedback.

We have yet to release our first official preview (though it’s coming
soon). What we have today is closer in spirit to a nightly build (we’re
working on getting a Continuous Integration (CI) server in place).

So go over to the
[NuGet](http://nuget.codeplex.com/ "NuGet on CodePlex.com") website on
CodePlex and check out our guide to [contributing to
NuGet](http://nuget.codeplex.com/documentation?title=Contributing%20to%20NuPack "Contributing to NuGet").
I’ve been working hard to try and get documentation in place, but I
could sure use some help.

With your help, I hope that NuGet becomes a wildly successful example of
how building products in collaboration with the open source community
benefits our business and the community.

