---
layout: post
title: "Announcing Subtext, A Fork Of .TEXT For Your Blogging Pleasure"
date: 2005-05-04 -0800
comments: true
disqus_identifier: 2953
categories: [subtext,open source]
---
> **sub text**\
>  Function: *noun*\
>  1. The implicit or metaphorical meaning (as of a literary text)\
>  2. A story within the story.

What is .TEXT?
--------------

.TEXT is a popular (among .NET loving geeks), scalable, and feature rich
blogging engine started by [Scott
Watermasysk](http://scottwater.com/blog) and released as an open source
project under a [BSD license](http://scottwater.com/license). Scott did
a wonderful job with .TEXT as evidenced by its widespread use among
bloggers and being the blogging engine for
[http://blogs.msdn.com/](http://blogs.msdn.com/) among others.

Sounds great. So why fork it?
-----------------------------

There are several reasons I think a fork is waranted.

### .TEXT is dead as an open source product.

.TEXT is dead as a BSD licensed open source project. Out of its ashes
has risen [Community Server](http://communityserver.org/) which
integrates a new version of the .TEXT source code with Forums and Photo
Galleries. Community Server is now a [product being
sold](https://store.telligentsystems.com/FamilyProducts.aspx?id=1) by
[Telligent Systems](https://www.telligentsystems.com/). There is a
non-commercial license available, but it requires displaying the
telligent logo and restricts usage to non-commercial purposes. I'd
prefer to use a blogging engine with an [OSI approved
license](http://www.opensource.org/licenses/index.php), in particular
the [BSD license](http://www.opensource.org/licenses/bsd-license.php)
works well for me.

> As an aside, if you're wondering how they can take an open source
> project and turn it into a commercial product, it's quite easy
> actually. Here's [the story of another commercially acquired open
> source project](http://blogs.zdnet.com/BTL/index.php?p=1306).

### Community Server Targets A Different Market

Another reason is that Community Server has become sort of the Team
System of blogging engines. By virtue of it going commercial, it's being
targetted to a different market than your average hobbyist and blogger.
While I'm sure many are looking forward to the tight integration with
forums and photo gallery, that's just not something I personally need.
This integration project was quite ambitious, but it resulted, in my
opinion, a rushed 1.0 release as evidenced by [this list of
bugs](http://jaysonknight.com/blog/archive/2005/04/06/1322.aspx). Bugs
are fine, but many of these are regressions of RSS functionality that
worked fine in .TEXT. I've helped
[Jayson](http://jaysonknight.com/blog/) with fixing some of these bugs
in CS. As a developer on the [RSS Bandit](http://www.rssbandit.org/)
team, you can guess that proper RSS support is very near and dear to me.
Starting this project will enable me to have a hand in both ends of the
blogging spectrum.

Ok, So Who Does Subtext Target?
-------------------------------

Subtext is the name I've chosen for this fork of .TEXT. Subtext targets
the blogging enthusiast who wants a usable and tightly focused blogging
engine. If you've ever caught yourself throwing your hands in the air
and declaring that you're going to write your own blogging engine from
scratch, Subtext is going to be for you. My first and primary task is to
streamline the installation and configuration process (hence [my recent
fascination with WiX](http://haacked.com/archive/2005/05/03/2930.aspx)).

What are the Subtext Guiding Principles
---------------------------------------

There are several principles that will serve to guide development on
Subtext.

-   Usability
-   Badass Quality
-   Documentation
-   Focused
-   Easy to install and configure

One of the difficulties of many open source projects is their typical
lack of documentation. Working with
[Dare](http://www.25hoursaday.com/weblog/) and
[Torsten](http://www.rendelmann.info/blog/), I helped improve their
already impressive documentation of RSS Bandit. I'd like to do the same
for Subtext.

Likewise, I want to make setting up Subtext a pleasure, not a royal
headache. That's my first task and highest priority at this point.

Where's Subtext At Now?
-----------------------

Currently I'm in the planning stage for Subtext. I've uploaded the code
to the [Subtext SourceForge
project](http://sourceforge.net/projects/subtext/) and am currently
recruiting a few core members to help out. I've started with the .TEXT
0.95 code base, so if you have patches to submit, by all means please
do. I've already added some small changes to make it more XHTML
compliant. In the beginning, I plan to recruit a small core team of
developers with write access to help me review and apply code patches,
as well as do some of the development. Over time I hope the team will
grow as we find developers who are making meaningful contributions.

In the meanwhile, I'll be drawing up some project guidelines and a
roadmap so stay tuned.

UPDATE: Ken Robertson [points out some
inaccuracies](http://www.qgyen.net/blog/archive/2005/05/05/1024.aspx)
with this announcement in his blog. I went ahead and made some
corrections.

One thing he mentions is that I'm slightly off when I say that the
target market for CS is larger institutions. I agree that CS may work
well for small fries like me, but I defended my assertion with a comment
in his blog.

> As for the target market, I see your point that I may be slightly off.
> My point is that by commercializing Community Server, you've created
> an incentive to target the needs of larger paying corporations. You're
> a business and you need the cash inflow. Nothing wrong with that.

Starting this project is not an attack of Telligent or Community Server
by any means. I do wish them well. I just think there's still room for a
tightly focused Open Source blogging engine targeted to individuals with
no restrictions. It's very likely that Community Server 1.1 or 1.2 will
blow our socks off and have us questioning whether Subtext is worth the
time. But until then, I think Subtext will ride the wave of backlash at
the perceived hastiness in which CS 1.0 was released and hopefully turn
into a compelling product in its own right. We're carving out a niche
here.

Tags: [Subtext](http://haacked.com/tags/subtext/default.aspx)

