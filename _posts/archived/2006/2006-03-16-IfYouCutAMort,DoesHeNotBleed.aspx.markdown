---
title: If You Cut A Mort, Does He Not Bleed?
date: 2006-03-16 -0800 9:00 AM
tags: [developers,commentary]
redirect_from: "/archive/2006/03/15/IfYouCutAMort,DoesHeNotBleed.aspx/"
---

[Lazy Coder](http://www.lazycoder.com/weblog/ "Scott's Blog") Scott
[snickers when he reads blog](http://www.lazycoder.com/weblog/index.php/archives/2006/03/17/we-are-all-mort/)
entries decrying the existence of “Mort”. As he points out, we are all
Morts to some degree or another.

> I snicker when I read these posts because they dont get it. The entire
> POINT of writing code is to abstract away the difficulty that is
> inherent in using computers.

Which is true. The history of software development has been all about
heaping one layer of abstraction on top of another.

However, I don’t see many blog entries decrying the *existence* of Mort.
What I see are blog entries from “Not-So-Mort” upset about what happens
to their programming tools and languages when tool and language
providers accomodate Mort. Perhaps we see these tools as being made for
the “Not-So-Mort” set, when the reality appears that these tools are
being built for Mort. Perhaps the “Not-So-Mort” set would like separate
tools.

Consider this, [all abstractions are
leaky](http://www.joelonsoftware.com/articles/LeakyAbstractions.html "The Law Of Leaky Abstractions").
However, when an abstraction is well implemented, it hardly matters for
the majority of the population. I believe I drive just as well in an
automatic transmission car as I would in a manual transmission car,
though my car has reduced the leak in the abstraction a bit via its
sequential shift so I can switch to a manual-like mode, but that’s
beside the point.

But many times, an abstraction is created in haste and causes problems
for those of us who need finer grained control. A classic example is
WebForms designer in Visual Studio.NET 1.1. I'm fine with the webforms
designer. It is a great productivity tool and makes it quick and easy
for Mort to build web pages using RAD techniques.

But now, take a Not-So-Mort who wants to use something like Microformats
for example, which requires clean markup. He marks up his pages just
right, but the pages get all FUBAR because the designer decides to
rewrite his code. That's problematic.

**The problem here is we’ve exchanged long-term productivity gains (the
maintenance cycle) in exchange for short-term gains in initial
productivity.**

Because these abstractions are leaky and poorly implemented, they
convolute the implementation details they are meant to hide and make
long term maintenance that much more difficult.

Whereas well implemented abstractions tend to promote good code. I’ve
read several people state that a developer would have a difficult time
writing more optimized Assembler than a good C++ compiler generates in
this day and age, especially on a grand scale. It can still be done, but
the fact that it is so close shows that C++ is a great abstraction on
top of Assembly.

I’ve [written about Mort
too](/archive/2005/08/03/9210.aspx "Does Mort Know We're Talking About Him"),
but I am not hating on Mort. As Scott says, we are all to some degree a
Mort. However one characteristic of a Mort as I have seen commonly
defined is that Mort does not care to constantly learn. Mort isn’t
striving to improve.

I do still think we should expect more from Mort. Understand that though
the tools we have at our disposal make computing easier every day,
computing at its core is a complex problem. Be sure to gain some small
understanding of what these tools are doing for you and a general idea
of what happens under the hood.

Back to my car analogy, I couldn’t take a wrench and fix my timing belt
for the life of me. But I do have a general idea of what an automatic
transmission is doing and what limitations it causes on my driving (I
can’t seem to redline!). I would never ask Mort to understand assembly,
but do take the time to understand some general principles.

