---
title: Open Source and Open Source Software Are Not The Same Things
date: 2012-02-16 -0800
disqus_identifier: 18847
categories:
- code
- asp.net mvc
- oss
- community
redirect_from: "/archive/2012/02/15/open-source-and-open-source-software-are-not-the-same.aspx/"
---

UPDATE: I have a [follow-up
post](https://haacked.com/archive/2012/02/22/spirit-of-open-source.aspx "Spirit of OS")
that addresses a few criticisms of this post.

It all started with an innocent tweet asking whether ASP.NET MVC 3 is
“open source” or not? I jumped in with my usual answer, “of course it
is!” The source code is released under the
[Ms-PL](http://www.opensource.org/licenses/MS-PL "MS-PL"), a license
recognized that the OSI legally reviewed to ensure it meets the [Open
Source Definition
(OSD)](http://www.opensource.org/osd.html "Open Source Definition (annotated)").
The Free Software Foundation (FSF) recognizes it as a “[free software
license](http://en.wikipedia.org/wiki/Free_software_licence "Free Software License")”^1^making
it not only OSS, but FOSS ([Free and open source
software](http://en.wikipedia.org/wiki/Free_and_Open_Source_Software "Free and Open Source Software"))
by that definition.

Afterwards, a healthy debate ensued on Twitter. No seriously! “Healthy”
debates on Twitter do happen sometimes. And many times, I learn
something.

Many countered with objections. How can the project be open source if
development is done in private and the code is “tossed over the wall” at
the end of the product cycle under an open source license? How can it be
open source if it doesn’t accept contributions?

This is when it occurred to me:

1.  I’ve been using these terms interchangeably and imprecisely.
2.  Many people have different concepts of what “open source” is.

**“Open source” is not the same thing as “open source software”.** The
first defines an approach to building software. The second is the end
product. Saying they are the same thing is like saying that a car that
Toyota makes and Kanban are the same thing. Obvious, isn’t it?

The Importance of Definitions
-----------------------------

I’m a big fan of open source software and the open source model of
developing software. I don’t necessarily think all software should be
open source, but I do find a lot of benefits to this approach. I’m also
not arguing that writing OSS in a closed off manner is a good or bad
thing. What I care about is having clear definitions so we’re talking
about the same things when we use these terms. Or at least making it
clear what *I mean* when I use these terms.

Definitions are important! If you allow for any code to define itself as
“open source”, you get the monstrosities like the MS-LPL. If you don’t
remember this license, it looked a lot like an open source license, but
it had a nasty platform restriction.

Microsoft started the term “[Shared
Source](http://en.wikipedia.org/wiki/Shared_source "Shared source")” to
describe these licenses which allow you to look at the code, but aren’t
open source by most common definitions of the term.

Open Source Software
--------------------

Going back to my realization, when I talk about “open source” I often
really mean “open source software”. In my mind, open source software is
**source code** that is licensed under a license that meets the [Open
Source
Definition](http://www.opensource.org/osd.html "Open Source Definition.").

Thus this phrase is completely defined in terms of properties of the
source code. More specifically, it’s defined in terms of the source
code’s license and what that license allows.

So when we discuss what it means for software to be open source
software, I try to frame things in terms of the software and not who or
how the software is made.

This is why I believe ASP.NET MVC is OSS despite the fact that the team
doesn’t currently accept outside contributions. After all, source code
can’t accept contributions. People do. In fact, open source licenses
have nothing to say about contributions at all.

What defines OSS is the right to modify and freely redistribute the
code. It doesn’t give anyone the right to force the authors to accept
contributions.

Open Source
-----------

So that’s the definition that’s often in my mind, when I’ve use the term
“open source” in the past. From now on, I’ll try to use “open source
software” for that.

When most people I talk to use the term “open source”, they’re talking
about something different. They are usually talking about the culture,
process, and philosophy that surrounds building open source products
such as open source software. Characteristics typically include:

-   Developed in the open with community involvement
-   The team accepts contributions that meet its standards
-   The end product has an open source license. This encompasses open
    source software, open source hardware, etc.

There’s probably others I’m forgetting, but those three stand out to me.
So while I completely believe that a team can develop open source
software in private without accepting contributions, I do believe that
they’re not complying with the culture and spirit of open source. Or as
my co-worker Paul puts it (slightly modified by me)

> I would put a more positive spin, that **they're losing out on many of
> the benefits of open source.**

And as [Bertrand](http://weblogs.asp.net/bleroy/ "Bertrand's Blog")
points out in my comments, “open source” applies to many more domains
than just software, such as open source hardware, recipes, etc.

Why does it matter?
-------------------

So am I simply quibbling over semantics? Why does it matter? If a
project does it in private and doesn’t accept contributions, why does
anyone care if the product is licensed as open source software?

I think it’s very important. In fact, I think the most important part is
the freedom that an open source license allows. As important and
wonderful as I think developing in the open and accepting contributions
is, I think the freedom the license gives you is even more important.

Why? Maybe a contrived, but not too far fetched, example will help
illustrate.

Imagine a project building a JavaScript library that develops completely
in the open and accepts contributions. They’ve built up a nice healthy
ecosystem and community around their project. Some of the code is really
whiz bang and would make my website super great. I can submit all the
contributions I want. Awesome, right?

But there’s one teensy problem. The license for the code has a platform
restriction. Let’s say the code may only run on Windows. That’d be
pretty horrible, right? The software is useless to me. I can’t even use
a tiny part of it in my own website because I know browsers running on
macs will visit my site and thus I’d be distributing it to non-Windows
machines in violation of the license.

But if it were a library developed in private, and once in a while they
produce an open source licensed release, so many doors are opened. I can
choose to fork it and create a separate open community around the fork.
I can distribute it via my website however I like. Others can
redistribute it.

Again, just to clarify. In the best case, I’d have it both ways. An open
source license and the ability to submit contributions and see the
software developed in the open. My main point is that the license of a
piece of useful software, despite its origins, is very important.

In the end?
-----------

So in the end, is ASP.NET MVC 3 “open source”? As Drew Miller aptly
wrote on Twitter,

> How I like to describe it: ASP.NET MVC 3 is open source software, but
> not an open source project.

At least not yet anyways.

UPDATE: After some discussion, I’m not so sure even this last statement
is correct. Read my follow-up post, [What is the spirit of open
source?](https://haacked.com/archive/2012/02/22/spirit-of-open-source.aspx "Spirit of OS")

*^1^Although the Free Software Foundation*[*recognizes the MS-PL (and
MS-RL) as a free software
license*](http://www.gnu.org/licenses/license-list.html#ms-pl "FSF comments on MS-PL")*,
they don’t recommend it because of its incompatibility with GNU GPL.*

