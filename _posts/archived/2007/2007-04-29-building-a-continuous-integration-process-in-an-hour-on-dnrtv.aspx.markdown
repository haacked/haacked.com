---
title: Building a Continuous Integration Process In An Hour On DNRTV
date: 2007-04-29 -0800
tags: [ci]
redirect_from: "/archive/2007/04/28/building-a-continuous-integration-process-in-an-hour-on-dnrtv.aspx/"
---

If you’ve read my blog at all, you know I’m a big proponent of
[Continuous
Integration](http://www.martinfowler.com/articles/continuousIntegration.html "Continuous Integration")
(CI). For the [Subtext
project](http://subtextproject.com/ "Subtext Project Website"), we use
[CruiseControl.NET](http://confluence.public.thoughtworks.org/display/CCNET/Welcome+to+CruiseControl.NET "CruiseControl.NET").
I’ve [written about our build
process](https://haacked.com/archive/2006/05/03/SubtextCruisingInCruiseControl.NET.aspx "Subtext Cruising in CruiseControl.NET")
in the past.

Given the usefulness of having a build server, you can understand my
frustration and sadness when our build server recently [took a
dive](https://haacked.com/archive/2007/04/24/the-death-of-the-subtext-build-server.aspx "Death of the subtext build server").
I bought a replacement hard drive, but it was the wrong kind (a rookie
mistake on my part, accidentally getting an IDE drive rather than SATA).

Members of the Subtext team such as
[Simo](http://codeclimber.net.nz/ "CodeClimber"), Myself, and [Scott
Dorman](http://geekswithblogs.net/sdorman/Default.aspx "Scott Dorman")
have put in countless hours into perfecting the build server. If only we
had [CI Factory](http://cifactory.org/ "CI Factory Website") in our
toolbelt before we started.

CI Factory is just that, a factory for creating CruiseControl.NET
scripts. [Scott
Hanselman](http://www.hanselman.com/blog/ "Scott Hanselman") calls it a
Continuous Integration accelerator. It bundles just about everything you
need for a complete CI setup such as CCNET,
[NUnit](http://nunit.com/ "NUnit") or
[MbUnit](http://mbunit.com/ "MbUnit"),
[NCover](http://ncover.org/site/ "NCover"), etc...

[In the latest dnrTV
episode](http://www.dnrtv.com/default.aspx?showID=64 "dnrTV Episode 64"),
[Jay Flowers](http://jayflowers.com/joomla/ "Jay Flowers"), the creator
of CI Factory, joins hosts Scott Hanselman and [Carl
Franklin](http://www.intellectualhedonism.com/ "Carl Franklin") to
create a Continuous Integration setup using CI Factory in around an
hour.

The project they chose to use as a demonstration is none other than
Subtext! Given the number of hours we’ve taken to setup the Subtext
build server, this is quite an ambituous undertaking to take, especially
while being recorded.

**Can you imagine having to write code while two guys provide color
commentary?** I’d probably wilt under that pressure, but Jay handles it
with aplomb.

The video runs a bit long, but is worth watching if you plan to setup CI
for your own project. The amount of XML configuration with CIFactory
might seem daunting at first, but trust me when I say that it’s much
worse for CCNET by itself. CIFactory reduces the amount of configuration
by a lot, and Jay is constantly making it easier and easier to setup.

As an aside, Jay Flowers scores big points with me for also being a
member of the MbUnit team, my favorite unit testing framework. Kudos to
Jay, Scott, and Carl for a great show.

