---
title: NuGet 1.4 Released
tags: [oss,nuget]
redirect_from: "/archive/2011/06/16/nuget-1-4-released.aspx/"
---

The moon goes around the earth and when it comes up on the other side,
Hark! There’s a new release of NuGet! Well, this time it was more like
one and a half revolutions, but I’m happy nonetheless to announce the
**release of NuGet 1.4**.

A **big thank you** goes out to the [many external
contributors](http://www.ohloh.net/p/nuget/contributors "NuGet Contributors")
who submitted patches to this release! Your enhancements are much
appreciated!

I’ve written up much more details about what’s in this release in the
[NuGet 1.4 Release
Notes](http://docs.nuget.org/docs/release-notes/nuget-1.4 "NuGet 1.4 Release Notes"),
but I’ll highlight a few choice things in this blog post.

NuGet Self-Update Notification Check
------------------------------------

One thing you may notice immediately if you’re running NuGet 1.3 today
is that the NuGet dialog notifies you itself that there’s a new version
of NuGet.

[![NuGet Update Check
Notification](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/NuGet-1.4-Released_C2D2/manage-nuget-packages-update-notification_thumb.png "NuGet Update Check Notification")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/NuGet-1.4-Released_C2D2/manage-nuget-packages-update-notification_2.png)

***Note**: The check is only made if the **Online** tab has been
selected in the current session*.

This feature was actually added in NuGet 1.3, but obviously would not be
visible until today, now that NuGet 1.4 is available.

Managing Packages Across The Solution
-------------------------------------

A lot of work in this release went into managing packages across the
solution. If you’re a command-line junky, the Package Manager Console
Update-Package commands now support updating all packages in all
projects as well as a single package across all projects.

The NuGet dialog can also be launched at the solution level which makes
it easy to choose a set of projects to install a package into, rather
than installing a package into a project one at a time. This was a
common request for those working on a large multi-project solution.

![NuGet Project
Selection](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/NuGet-1.4-Released_C2D2/manage-nuget-packages-update-project-selection_b63a6356-8b22-4ed2-acd1-a37d6526ddea.png "NuGet Project Selection")

What’s Next?
------------

This blog post is just a tiny sampling of what’s new. Again, check out
[the release
notes](http://docs.nuget.org/docs/release-notes/nuget-1.4 "Release Notes")
for more details.

We’re going to try better to have a roadmap of the next couple of
releases hosted on the front page here:
[http://nuget.codeplex.com/](http://nuget.codeplex.com/). For now, it’s
very high level and general because we really only fully plan one
iteration ahead.

However, we do have an idea of some of the big themes we want to focus
on:

-   **Simple package creation:** We constantly want to lower the bar for
    creating and sharing code from inside and outside of Visual Studio.
-   **NuGet in the Enterprise:**This includes CI scenarios outside of
    Visual Studio, authenticated feeds, etc.
-   **Flexible packaging:** Includes things like including assemblies
    that are not referenced but deployed and vice versa.
-   **Developer Workflow:** We’re looking at common workflows that don’t
    match our own expectations and how we can support them. This also
    includes workflows we do know about such as the use of pre-release
    packages etc.

In general though, I think we can sum up all of themes in one big theme:
**Make NuGet Better!**

Get Involved!
-------------

If you have great ideas for NuGet, please get involved [in the
discussions](http://nuget.codeplex.com/discussions "NuGet Discussions").
We try to be very responsive and we do accept external contributions as
Joshua Flanagan learned and wrote about in his blog post, An opportunity
for a [viable .NET open source
ecosystem](http://lostechies.com/joshuaflanagan/2011/05/27/an-opportunity-for-a-viable-net-open-source-ecosystem/ ".NET Ecosystem").

> Then, remembering my last experience, I figured I would at least
> [start a discussion](http://nuget.codeplex.com/discussions/258338)
> before giving up for the night. To my surprise, the next day it was
> [turned into an issue](http://nuget.codeplex.com/workitem/1089) – this
> isn’t just another [Microsoft Connect black
> hole](http://ayende.com/blog/2667/how-to-kill-the-community-feedback-or-the-uselessness-of-microsoft-connect).
> After hashing out a few details, I went to work on a solution and
> submitted a [pull
> request](http://nuget.codeplex.com/SourceControl/changeset/changes/2e7df0e9ae42).
> It was accepted within a few days. Aha! This is open source. This is
> how its supposed to work. This works.

Onward to NuGet 1.5!

