---
title: Is Community Server Open Source?
date: 2006-07-25 -0800
tags: [oss,licensing]
redirect_from: "/archive/2006/07/24/IsCommunityServerOpenSource.aspx/"
---

[Dave Burke](http://dbvt.com/blog/ "Dave Burke") makes the interesting
claim that [Community Server is an open source
application](http://dbvt.com/blog/archive/2006/07/25/4963.aspx "Community Server and Open Source").
Whether this is true or not of course depends on your definition of the
term *Open Source*. Here is Dave’s definition.

> To talk about Community Server and Open Source we should start with a
> baseline definition of an Open Source application: All of the source
> code is available. For free.

But is that all there is to *Open Source*, access to the code? Is mere
access to the code the fairy dust that has inspired such a passionate
movement in the software community?

Certainly the term Open Source has had a history of ambiguity, so that
definition might contain some validity. But I do not think that is the
commonly agreed upon minimal criteria for something to be considered
Open Source.

**Open source isn’t just about whether the source code is available, it
is all about the *license* to the source code.**

My favorite definition of open source software is [The Open Source
Definition](http://www.opensource.org/docs/definition.php "OSD Defined")
(or OSD for short) on the [Open Source
Initiative](http://www.opensource.org/index.php "OSI") website.

The definition starts with the following introduction and then lists
serveral criteria for open source software.

> Open source doesn’t just mean access to the source code. The
> distribution terms of open-source software must comply with the
> following criteria:

The first criteria listed is **Free Redistribution** which states...

> The license shall not restrict any party from selling or giving away
> the software as a component of an aggregate software distribution
> containing programs from several different sources. The license shall
> not require a royalty or other fee for such sale.

Contrast this to the Community Server license agreement 2.0 which
states...

> 3g. **Distribution.** You may not distribute this product, or any
> portion thereof, or any derived work thereof, to anyone outside your
> organization. You are not allowed to combine or distribute the
> Software with other software that is licensed under terms that seek to
> require that the Software (or any intellectual propertyin it) be
> provided in source code form, licensed to others to allow the creation
> or distribution of derivative works, or distributed without charge.

For many people, the terms of the Community Server license might not be
a problem. They are not terribly restrictive. If you plan to use
Community Server [under the community
license](http://communityserver.org/i/licensing.aspx#Community "Community License")
your only requirement is to display the *Powered By Community Server*
logo on every page of the site that uses Community Server.

However for many others, these terms are restrictive enough. For
example, suppose you don’t like the way development is progressing on
Community Server. **You cannot fork the code base and start a new
project based on the source code**. Although a fork may seem like a bad
thing, Karl Fogel points out in his book *"[Producing Open Source
Software - How to Run a Successful Free Software
Project](http://producingoss.com/ "Book")* that the threat of a fork is
what keeps the leader(s) of an open source project from being
tyrannical. It is this threat of a fork that motivates and requires open
source projects to be well run.

Not every open source license is created equal as I pointed out in my
guide to [Open Source Software
Licensing](https://haacked.com/archive/2006/01/24/DevelopersGuideToOpenSourceSoftwareLicensing.aspx "OSS Licensing").
For example, under the [BSD
license](http://www.opensource.org/licenses/bsd-license.php "BSD") in
which Subtext is licensed, you and I are free to create a commercial
derivative version of Subtext and keep your changes to the code closed
source and proprietary. That’s right. If you wanted to (and had the
ability to), you could package up the Subtext source code in its
entirety and start selling it as a packaged product.

Note that you can’t turn around and claim that you have the copyright to
the Subtext code. You would only have copyright to your changes to the
code. Pretty much the only restriction is that the original license must
be retained with with the code, but it does not have to be publicly
visible in your site (such as in an about box).

In contrast, with a [GPL
Licensed](http://www.opensource.org/licenses/gpl-license.php "GPL")
project, you could start selling it, but you couldn’t keep your changes
closed source without violating the terms of the license.

In the end, I think we need to agree on a term for unique products such
as Community Server in which the source code is freely available, but
does not fit the definition of an open source product. I suggest the
term *Source Available*.

Please do not misconstrue this as an attack on [Community
Server](http://communityserver.org/ "CS") or its licensing. I have met
both [Scott
Watermasysk](http://scottwater.com/blog/ "Scott Watermasysk") and [Rob
Howard](http://weblogs.asp.net/rhoward/ "Rob Howard") and they are both
very smart and capable leaders of a strong company. Community Server is
a great product and deserves the recognition it gets. I am not a zealot
and have no beef with closed source products. Certainly my livelihood
depends on many such products.

At the same time, I am passionate about Open Source software and it is
important to me to help keep the distinctions clear and educate others
on what open source software is and the value it provides.

