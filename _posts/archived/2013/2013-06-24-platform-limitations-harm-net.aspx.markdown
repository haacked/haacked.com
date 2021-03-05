---
title: Platform Limitations Harm .NET
tags: [code]
redirect_from: "/archive/2013/06/23/platform-limitations-harm-net.aspx/"
---

UPDATE: The .NET team [removed the platform
limitations](http://blogs.msdn.com/b/dotnet/archive/2013/11/13/pcl-and-net-nuget-libraries-are-now-enabled-for-xamarin.aspx).

Let me start by giving some kudos to the Microsoft BCL (Base Class
Library) team. They’ve been doing a great job of shipping useful
libraries lately. Here’s a small sampling on Nuget:

-   [Microsoft.Net.Http](https://nuget.org/packages/Microsoft.Net.Http)
    (HttpClient)
-   [Microsoft.Bcl.Compression](https://nuget.org/packages/Microsoft.Bcl.Compression/ "Microsoft BCL Compression")
-   [Microsoft.Bcl.Immutable](https://nuget.org/packages/Microsoft.Bcl.Immutable/ "Immutable collections")
    (Immutable Collections)

However, one trend I’ve noticed is that the released versions of most of
these packages have a platform limitation in the EULA (*the pre-release
versions have an “eval-only” license which do not limit platform, but do
limit deployment for production use*). At this point I should remind
everyone I’m not a lawyer and this is not legal advice blah blah blah.

Here’s an excerpt from section 2. c. in the released HttpClient license,
emphasis mine:

> **a. Distribution Restrictions. You may not**
>
> -   alter any copyright, trademark or patent notice in the
>     Distributable Code;
> -   use Microsoft’s trademarks in your programs’ names or in a way
>     that suggests your programs come from or are endorsed by
>     Microsoft;
> -   **distribute Distributable Code to run on a platform other than
>     the Windows platform;**

I think this last bullet point is problematic and should be removed.

Why should they?
----------------

I recently wrote the following tweet in response to this trend:

> Dear Microsoft BCL team. Please remove the platform limitation on your
> very cool libraries. Love, cross-platform .NET devs.

And a [Richard Burte tweeted
back](https://twitter.com/arebee/status/349214825802506240 "Tweet"):

> And that pays the rent how exactly?

Great question!

There is this sentiment among many that the only reason to make .NET
libraries cross platform or open source is just to appease us long
haired open source hippies.

Well first, let me make it crystal clear that I plan to get a haircut
very soon. Second, the focus of this particular discussion is the
platform limitation on the *compiled binaries*. I’ll get to the extra
hippie open source part later.

There are several reasons why **removing the platform limitation
benefits Microsoft and the .NET team**.

## It benefits Microsoft’s enterprise customers

Let’s start with Microsoft’s bread and butter, the enterprise. There’s a
growing trend of enterprises that support employees who bring their own
devices (BYOD) to work. As [Wikipedia points
out](http://en.wikipedia.org/wiki/Bring_your_own_device "BYOD on Wikipedia"):

> BYOD is making significant inroads in the business world, with about
> 75% of employees in high growth markets such as Brazil and Russia and
> 44% in developed markets already using their own technology at work.

Heck, at the time I was an employee, even Microsoft supported employees
with iPhones connecting to Exchange to get email. I assume they still
do, [Ballmer pretending to break an iPhone
notwithstanding](http://gizmodo.com/5357235/ballmer-busts-microsoft-staffer-taking-his-photo-with-an-iphoneuh-oh "Ballmer pretends to break iPhone").

Microsoft’s own software supports cross-platform usage. Keeping platform
limitations on their .NET code hamstrings enterprise developers who want
to either target the enterprise market or want to make internal tools
for their companies that work on all devices.

## It’s a long play benefit to Windows 8 Phone and Tablet

While developing Windows 8, Microsoft put a ton of energy and focus into
a new HTML and JavaScript based development model for Windows 8
applications, at the cost of focus on .NET and C# in that time period.

The end result? From several sources I’ve heard that something like 85%
of apps in the Windows app store are C# apps.

Now, I don’t think we’re going to see a bunch of iOS developers suddenly
pick up C# in droves and start porting their apps to work on Windows.
But there is the *next generation* to think of. If Windows 8 devices can
get enough share to make it worthwhile, it may be easier to convince
this next generation of developers to consider C# for their iOS
development and port to Windows cheaply. Already, with Xamarin tools,
using C# to target iOS is a worlds better environment than Objective-C.
I believe iOS developers today tolerate Objective-C because it’s been so
successful for them and it was the only game in town. As Xamarin tools
get more notice, I don’t think the next generation will tolerate the
clumsiness of the Objective-C tools.

## There’s no good reason not to

Ok, this isn’t strictly speaking a benefit. But it speaks to a benefit.

The benefit here is that when Microsoft restricts developers without
good reason, it makes them unhappy.

If you recall, Ballmer is the one who once went on stage to affirm
Microsoft’s focus on developers! developers! developers! through
interpretive dance.

[![ballmer-developers-dance](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/PlatformLimitations_ACF1/ballmer-developers-dance_thumb.gif "ballmer-developers-dance")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/PlatformLimitations_ACF1/ballmer-developers-dance_2.gif)

Unless there’s something I’m missing (and feel free to enlighten me!),
there’s no good reason to keep the platform restriction on most of these
libraries. In such cases, focus on the developers!

At a recent Outercurve conference, [Scott
Guthrie](http://weblogs.asp.net/scottgu/ "Scott Guthrie's blog"), a
corporate VP at Microsoft in change of the Azure Development platform
told the audience that his team’s rule of thumb with new frameworks is
to default it to open source unless they have a good reason not to.

The Azure team recognizes that a strategy that requires total Windows
hegemony will only lead to tears. Microsoft can succeed without having
Windows on every machine. Hence Azure supports Linux, and PHP, and other
non-Microsoft technologies.

I think the entire .NET team should look to what the Azure team is doing
in deciding what their strategy regarding licensing should be moving
forward. It makes more developers happy and costs very little to remove
that one bullet point from the EULA. I know, I’ve been a part of a team
that did it. We worked to destroy that bullet with fire (among others)
in every ASP.NET MVC EULA.

***Update:** It looks like I may have overstated this. Licenses for
products are based on templates. Typically a product team’s lawyer will
grab a template and then modify it. So with ASP.NET MVC 1 and 2, we
removed the platform restriction in the EULA. But it looks like the
legal team switched to a different license template in ASP.NET MVC 3 and
we forgot to remove the restriction. That was never the intention. Shame
on past Phil. Present Phil is disappointed.*

*At least in this case, the actual source code is licensed under the
Apache 2.0 license developers have the option to compile and
redistribute, making this a big inconvenience but not a showstopper.*

Next Steps
----------

I recently [commented on a BCL blog
post](http://blogs.msdn.com/b/dotnet/archive/2013/06/24/please-welcome-immutablearray.aspx "Immutable collections")
suggesting that the team remove the platform limitation on a library.
Immo Landwerth, a BCL Program Manager responded with a good clarifying
question:

> Thanks for sharing your concerns and the candid feedback. You post
> raised two very different items:
>
> ​(1) Windows only restriction on the license of the binaries
>
> ​(2) Open sourcing immutable collections
>
> From what I read, it sounds you are more interested in (1), is this
> correct?

The post he refers to is actually one that [Miguel de
Icaza](http://tirania.org/blog/ "Miguel's Blog") wrote when MEF came out
with a license that had a platform restriction entitled [Avoid the
Managed Extensibility
Framework](http://tirania.org/blog/archive/2008/Sep-07.html "Miguel's Blog Post").
Fortunately, that was quickly corrected in that case.

But now we seem to be in a similar situation again.

Here was my response:

> @Immo, well I'm interested in both. But I also understand how
> Microsoft works enough to know that (1) is much easier than (2). :P
>
> So ultimately, I think both would be great, but for now, (1) is a good
> first step and a minimal requirement for us to use it in ReactiveUI
> etc.

So while I’d love to see these libraries be open source, I think a
minimal next step would be to remove the platform limitation on the
compiled library and all future libraries.

And not just to make us long haired (but soon to have a haircut) open
source hippies happy, but to make us enterprise developers happy. To
make us cross-platform mobile developers happy.

