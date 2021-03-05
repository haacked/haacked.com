---
title: A Look Back at Mix 11
tags: [code,oss,aspnetmvc,aspnet,nuget]
redirect_from: "/archive/2011/04/15/a-look-back-at-mix-11.aspx/"
---

Another Spring approaches and once again, another Mix is over. This year
at Mix, my team announced the release of the ASP.NET MVC 3 Tools Update
at Mix, which I blogged about recently.

Working on this release as well as NuGet has kept me intensely busy
since we released ASP.NET MVC 3 RTM only this past January. Hopefully
now, my team and I can take a moment to breath as we start making big
plans for ASP.NET MVC 4. It’s interesting to me to think that the
version number for ASP.NET MVC is quickly catching up to the version of
ASP.NET proper.
![Smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/A-Look-Back-at-Mix-11_D597/wlEmoticon-smile_2.png)

Once again, Mix has continued to be one of my favorite conferences due
to the eclectic mix of folks who attend.

[![trouble-inc](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/A-Look-Back-at-Mix-11_D597/trouble-inc_thumb.jpg "trouble-inc")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/A-Look-Back-at-Mix-11_D597/trouble-inc_2.jpg)

The previous photo was taken from [Joey De Villa’s Blog
post](http://www.globalnerdy.com/2011/04/13/trouble-inc/ "Trouble, Inc").

[![Me-and-elvis](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/A-Look-Back-at-Mix-11_D597/Me-and-elvis_thumb.jpg "Me-and-elvis")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/A-Look-Back-at-Mix-11_D597/Me-and-elvis_2.jpg)

It’s not just a conference where you’ll run into Scott Guthrie and
Hanselman, but you’ll also run into [Douglas
Crockford](http://www.crockford.com/ "Crockford's Website"), [Miguel De
Icaza](http://tirania.org/blog/ "Miguel's Blog") or even Elvis!

I was involved with two talks at Mix which are now available on Channel9
and embedded here.

[**ASP.NET MVC 3 @:The Time Is
Now**](http://channel9.msdn.com/events/MIX/MIX11/FRM03 "ASP.NET MVC 3 The Time Is Now")

In this talk, I cover the new features of ASP.NET MVC 3 and the ASP.NET
MVC 3 Tools Update while building an application that allows me to ask
the audience survey questions. The application is hosted at
[http://mix11.haacked.com/](http://mix11.haacked.com/).

**Errata:** I ran into a few problems during this talk, which I will
cover [in a follow-up blog post about speaking
tips](https://haacked.com/archive/2011/04/18/presentation-tips.aspx "Presentation Tips From My Mistakes")
I learned due to mistakes I’ve made.

If you attended the talk (or watched it), I learned at the end that the
failure to publish was due to a proxy issue in the room’s network that I
didn’t have in my hotel room or the main conference area.

I plan to follow up on various topics I covered in the talk with blog
posts. For example, I wrote a helper method during the talk that allows
you to pass in a Razor snippet as a template for a loop. That’s now
covered in this blog post, [A Better Razor Foreach
Loop](https://haacked.com/archive/2011/04/14/a-better-razor-foreach-loop.aspx).

[**NuGet in Depth: Empowering Open Source on the .NET
Platform**](http://channel9.msdn.com/events/MIX/MIX11/FRM09 "NuGet In Depth")

In this talk, Scott and I perform what we call our “HaaHa” show, which
is a name derived from a combination of our last names, Phil **Haa**ck
and Scott **Ha**nselman but pronounced like our aliases Phil**Ha**and
Scott**Ha**.

We spent the entire talk attempting to one-up each other with demos of
NuGet. Each demo built on the last and showed more and more what you can
do with NuGet.

**Errata:** During the demo, there was one point where I expected a
License Agreement to pop up, but it didn’t. I gave a misleading
explanation for why that happened. We should have seen the pop-up
because we do not install SqlServerCompact by default.

Turns out I ran into an edge case potential bug in NuGet. Usually, when
I create a project, I make sure to create a folder for the solution so
that the solution is isolated in its own folder. For some reason, I
didn’t have that checked and the solution was being created in my temp
directory. Thus the packages folder was being shared with every project
I’ve created in that folder which made NuGet think that SqlServerCompact
was already installed.

If you’ve never accepted that agreement, it will pop up.

The second mistake I made was in describing *install.ps1*, which indeed
runs every time you install it into a project, not once per solution. To
get the correct definition, read our documentation page on [Creating a
Package](http://nuget.codeplex.com/wikipage?title=Creating%20a%20Package "Creating a Package").

Another minor mistake I made was in describing the Magic 8-Ball, I said
it had a dodecahedron inside. I meant to say icosahedron which is a
twenty-sided polyhedron.

During the talk, we randomly start talking about a ringtone. That was
due to someone’s phone going off in the audience. You can’t hear it in
the recording.
![Smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/A-Look-Back-at-Mix-11_D597/wlEmoticon-smile_2.png)

Oh, I just pushed *MoodSwings* to the main feed so you can try it out.

### Summary

This was the first time I stayed till the following day of a conference
rather than hopping on a cab to the airport immediately after my last
talk.

I highly recommend that approach. It was nice to have time to relax
after my last talk. A few of us went to ride the rollercoaster at NY NY,
walk around the strip, and take in a show
[JabbaWockeez](http://en.wikipedia.org/wiki/JabbaWockeeZ "JabbaWockeez").

[![IMG\_1183](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/A-Look-Back-at-Mix-11_D597/IMG_1183_thumb.jpg "IMG_1183")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/A-Look-Back-at-Mix-11_D597/IMG_1183.jpg)[![IMG\_1188](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/A-Look-Back-at-Mix-11_D597/IMG_1188_thumb.jpg "IMG_1188")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/A-Look-Back-at-Mix-11_D597/IMG_1188.jpg)

