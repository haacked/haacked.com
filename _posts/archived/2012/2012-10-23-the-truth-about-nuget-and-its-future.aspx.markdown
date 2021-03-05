---
title: The Truth about NuGet and its Future
tags: [oss,nuget,community,code]
redirect_from: "/archive/2012/10/22/the-truth-about-nuget-and-its-future.aspx/"
---

In [my last
post](https://haacked.com/archive/2012/10/21/monkeyspace-dotnet-oss.aspx "MonkeySpace shines a light on .NET open source"),
I talked about the [MonkeySpace
conference](http://monkeyspace.org/ "MonkeySpace Conference") conference
and how it reflects positive trends in the future of open source in
.NET. But getting to a better future is going to take some work on our
part. And a key component of that is making
[NuGet](http://nuget.org/ "NuGet") better.

Several discussions at MonkeySpace made it clear to me that there is
some pervasive confusion and misconceptions about
[NuGet](http://nuget.org/ "NuGet"). It also made it clear that there are
some dramatic changes needed for NuGet to continue to grow into a great
open source project. In this post, I’ll cover some of these
misconceptions and draw an outline of what I hope to see NuGet grow
into.

Myth: NuGet is tied to Visual Studio and Windows
------------------------------------------------

This is only partially true. The most popular NuGet client is clearly
the one that ships in Visual Studio. Also, NuGet packages may contain
PowerShell scripts. PowerShell is not currently available on any other
operating system other than Windows.

However, the architecture of NuGet is such that there’s a core assembly,
`NuGet.Core.dll`, that has no specific ties to Visual Studio. The proof
of this is in the fact that ASP.NET Web Pages and Web Matrix both have
NuGet clients. In these cases, the PowerShell scripts are ignored. Most
packages do not contain PowerShell scripts, and those that do, the
changes the scripts make are often optional or easily done manually.

In fact, there’s a `NuGet.exe` which is a wrapper of `NuGet.Core.dll`
that runs on Mono. Well sometimes it does; **and this is where we need
your help!** So far, Mono support for `NuGet.exe` has been low priority
for the NuGet team. But as I see the growth of Mono, I think this is
something we want to improve. My co-worker, [Drew
Miller](http://twitter.com/halfogre "Drew Miller on Twitter") (also a
former member of the NuGet and ASP.NET MVC team) is keen to make better
Mono support a reality. Initially, it could be as simple as adding a
Mono CI server to make sure `NuGet.exe` builds and runs on Mono.
Ultimately, we would like to build a MonoDevelop plugin.

Initially, it will probably simply ignore PowerShell scripts. There’s an
existing [CodePlex work
item](http://nuget.codeplex.com/workitem/720 ".NET equivalent of install.ps1, uninstall.ps1 and init.ps1")
to provide .NET equivalents to `Install.ps1` and the other scripts.

I created a personal fork of the NuGet project under my GitHub account
at [http://github.com/haacked/nuget](http://github.com/haacked/nuget).
This’ll be our playground for experimenting with these new features with
the clear goal of getting these changes back into the official NuGet
repository.

Myth: NuGet isn’t truly Open Source
-----------------------------------

This is an easy myth to dispel. Here’s the [license file for
NuGet](http://nuget.codeplex.com/SourceControl/changeset/view/767d123c4d2a#LICENSE.txt "License").
NuGet is licensed under the [Apache version 2
license](http://www.apache.org/licenses/LICENSE-2.0.html "Apache v2 License"),
and meets [the Open Source
Definition](http://opensource.org/docs/osd "Open Source Definition")
defined by the Open Source Initiative. The NuGet team accepts external
contributions as well, so it’s not just open source, but it’s an open
and collaborative project.

But maybe it’s not as collaborative as it could be. I’ll address that in
a moment.

Myth: NuGet is a Microsoft Project
----------------------------------

On paper, NuGet is a fully independent project of the [Outercurve
Foundation](http://www.outercurve.org/ "Outercurve Foundation"). If you
look at the
[COPYRIGHT.txt](http://nuget.codeplex.com/SourceControl/changeset/view/767d123c4d2a#COPYRIGHT.txt "NuGet copyright")
file in the NuGet source tree, you’ll see this:

> Copyright 2010 Outercurve Foundation

Which makes me realize, we need to update that file with the current
year, but I digress! That’s right, Microsoft assigned the copyright over
to the Outercurve foundation. Contributors are asked to assign copyright
for their contribution to the foundation as well. So clearly this is not
a Microsoft project, right?

Well if you look at the entry in the Visual Studio Extension Manager (or
[the
gallery](http://visualstudiogallery.msdn.microsoft.com/27077b70-9dad-4c64-adcf-c7cf6bc9970c?SRC=Home "NuGet in VS Extension Gallery")),
you’ll see this:

[![NuGet-Package-Manager](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/The-Truth-about-NuGet_B15C/NuGet-Package-Manager_thumb.png "NuGet-Package-Manager")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/The-Truth-about-NuGet_B15C/NuGet-Package-Manager_2.png)

Huh? What gives? Well, it’s time for some REAL TALK™.

There’s nothing shady going on here. In the same way that Google Chrome
is a Google product [with its own
EULA](https://www.google.com/intl/en/chrome/browser/eula.html "Google Chrome EULA")
that incorporates the open source [Chromium
project](http://www.chromium.org/ "The Chromium Projects"), and Safari
is an Apple product [with its own
EULA](http://images.apple.com/legal/sla/docs/SafariWindows.pdf "Safari EULA")
that incorporates the open source [WebKit
project](http://www.webkit.org/ "WebKit"), the version of NuGet included
in Visual Studio 2012 is officially named the *NuGet-Based Microsoft
Package Manager* and is a Microsoft product with [its own
EULA](http://visualstudiogallery.msdn.microsoft.com/site/27077b70-9dad-4c64-adcf-c7cf6bc9970c/eula?licenseType=None "EULA")
that incorporates the open source NuGet project. This is a common
practice among companies well known for shipping “open source” and all
complies with the terms of the license. You are always free to build and
install the Outercurve version of NuGet into Visual Studio should you
choose.

Of course, unlike the other two examples, NuGet is a bit confusing
because both the proprietary version and the open source version contain
the word “NuGet.” This is because we liked the name so much and because
it had established its identity that we felt not including “NuGet” in
the name of the Microsoft product would cause even more confusion. I
almost wish we had named the open source version “NuGetium” following
the Chromium/Chrome example.

This explains why NuGet is included in the Visual Studio Express
editions when it’s well known that third party extensions are not
allowed. It’s because NuGet is not included, it’s *NuGet-Based Microsoft
Package Manager* that’s included.

NuGet is not a Community Project
--------------------------------

Ok, this claim is a toss-up. As I pointed out before, NuGet is truly an
open source project that accepts external community contributions. But
is it really a “community project”

As the originator of the project, the sole provider of full time
contributors, and a huge benefactor of the Outercurve Foundation;
Microsoft clearly wields enormous influence on the NuGet project. Also,
more and more parts of Microsoft are realizing the enormous potential of
NuGet and getting on board with shipping packages. NuGet is integrated
into Visual Studio 2012. These are all great developments! But it also
means lessens the incentive for Microsoft to give up any control of the
project to the community at large.

So while I still maintain it is a community project, in its current
state the community’s influence is marginalized. But this isn’t entirely
Microsoft’s intention or fault. Some of it has to do with the lack of
outside contributors. Especially from those who have built products and
even businesses on top of NuGet.

My Role With NuGet
------------------

Before I talk about what I hope to see in NuGet’s future, let me give
you a brief rundown of my role. From the Outercurve perspective, I’m
still the [project lead of
NuGet](http://www.outercurve.org/Galleries/ASPNETOpenSourceGallery/NuGet "NuGet Project Page"),
the open source project. Microsoft of course has a developer lead, [Jeff
Handley](http://jeffhandley.com/ "Jeff Handley's Blog"), and a Program
Manager, [Howard
Dierking](http://codebetter.com/howarddierking/ "Howard Dierking's Blog"),
who run the day to day operations of NuGet and manage Microsoft’s
extensive contributions to NuGet.

Of course, since NuGet is no longer a large part of my day job, it’s
been challenging to stay involved. I recently met with Howard and Jeff
to figure out how my role fits in with theirs and we all agreed that I
should stay involved, but focus on the high level aspects of the
project. So while they run the day to day operations such as triage,
feature planning, code reviews, etc. I’ll still be involved in the
direction of NuGet as an independent open source project. I recently sat
in on the feature review for the next couple of versions of NuGet and
will periodically visit my old stomping grounds for these product
reviews.

The Future of NuGet
-------------------

Over time, I would like to see NuGet grow into a true community driven
project. This will require buy-in from Microsoft at many levels as well
as participation from the NuGet community.

In this regard, I think the governance model of the [Orchard
Project](http://www.orchardproject.net/ "Orchard Project Website") is a
great example of the direction that NuGet could head in. In September of
2011, [Microsoft transferred control of the Orchard project to the
community](http://weblogs.asp.net/bleroy/archive/2012/01/30/about-orchard-governance-and-microsoft.aspx "Orchard Governance and Microsoft").
As [Bertrand Le Roy](http://weblogs.asp.net/bleroy/ "Bertrand Le Roy")
writes:

> Back in September, we did something with Orchard that is kind of a big
> deal: we transferred control over the Orchard project to the
> community.
>
> Most Open Source projects that were initiated by corporations such as
> Microsoft are nowadays still governed by that corporation. They may
> have an open license, they may take patches and contributions, they
> may have given the copyright to some non-profit foundation, but for
> all practical purposes, it’s still that corporation that controls the
> project and makes the big decisions.
>
> That wasn’t what we wanted for Orchard. We wanted to trust the
> community completely to do what’s best for the project.

Why didn’t NuGet follow this model already? It’s complicated.

With something so integrated into so many of areas of Microsoft now, I
think this is a pretty bold step for Microsoft to take. It’ll take time
to reach this goal and it’ll take us, the community, demonstrating to
Microsoft and others who are invested in NuGet’s future that we’re fit
and ready to take on this responsibility.

As part of that, I would love to see more corporate sponsors of NuGet
supplying contributors. Especially those that profit from NuGet. For
example, while [GitHub](https://github.com/ "GitHub") doesn’t directly
profit from NuGet, we feel anything that encourages open source is
valuable to us. So Drew and I will spend some of our time on NuGet in
the upcoming months. The reason I don’t spend more time on NuGet today
is really a personal choice and prioritization, not because I’m not
given work time to do it since I pretty much define my own schedule.

If you are a company that benefits from NuGet, consider allotting time
for some of your developers to contribute back (or become a sponsor of
the Outercurve Foundation). Consider it an investment in having more of
a say in the future of NuGet should Microsoft transfer control over to
the community. NuGet belongs to us all, but we have to do our part to
own it.

