---
title: Developers Guide To Open Source Software Licensing
tags: [oss,legal,licensing]
redirect_from: "/archive/2006/01/23/DevelopersGuideToOpenSourceSoftwareLicensing.aspx/"
---

This is part 2 in my three-part series on copyright law and software
licensing. [Part 1
covered](https://haacked.com/archive/2006/01/24/TheDevelopersGuideToCopyrightLaw-Part1.aspx)
the basics of copyright law. With the background knowledge from that
post, we are ready to tackle software licensing in more depth. After
this, continue onto [Part
3](https://haacked.com/archive/2006/01/25/WhoOwnstheCopyrightforAnOpenSourceProject.aspx)
of the series.

## Licensing In General

A license is permission granted by a copyright holder to others to
reproduce or distribute a work. It is a means to allow others to have
some rights when it comes to using a work without assigning the
copyright to others.

For example, although I own exclusive copyright to this very blog post,
at the bottom of every page in my [blog](https://haacked.com/) I provide
a license to freely copy, distribute, display, and perform the work. I
also allow making derivative works and commercial use of the work under
a [Creative Commons
license](http://creativecommons.org/licenses/by/2.0/). Gee what a nice
guy I am! However I do stipulate one restriction. Anyone who wishes to
exercise one of the listed rights within the license must attribute the
work to me and make clear to others the license terms for the work. As
the copyright holder, I am free to grant others these rights, but also
to add restrictions as well.

## Proprietary and Closed Source Licenses

Copyright law and licensing applies to software every bit as much as it
does to writing. Most of the software that the average person uses day
to day falls under a proprietary license. That is, the user is not free
to distribute the software to others. This is often called “closed
source” software, but that term may be slightly misleading as software
can have its source code visible, but still not allow open distribution.
Likewise, it is possible for closed-source software to allow others to
freely distribute it as in the case of many free utilities.

## Open Source and Free Software Licenses

This leads us finally to “Open Source Software” and “Free Software”. The
two terms are often used interchangeably, but there is a slight
distinction. The term “Free Software” tends to apply to software
licensed in such a way that any code that makes use of the free software
code must itself be freely available. The “free” in “Free Software”
applies to the freedom to view the code.

Whereas “Open Source” is a more blanket term that merely applies to
software in which the source code is visible and freely distributed.
Open Source software does not necessarily require that its usage also be
Open Source. Thus Free Software is Open Source, but Open Source is not
necessarily Free Software.

## Types of Open Source Licenses

When starting an open source project, the copyright owner is free to
license the source code to others in any manner he or she sees fit. But
the cost to draft a custom open source license is prohibitive. And to do
so oneself is often a big mistake, especially given the fact that there
are many [well established
licenses](http://www.opensource.org/licenses/) in existence that have
stood the test of time.

## Choosing A License

The Fogel book does a decent job of providing insight into how to choose
a license, so I won’t delve into it too deeply.

### GPL

Despite the plethora of licenses, in general the one you choose will be
a result of your philosophical disposition towards open source software.
If you fall under the free software camp and believe that all software
should be free, then you may gravitate towards [The GNU General Public
License (GPL)](http://www.opensource.org/licenses/gpl-license.php).

The GPL is designed to guarantee the user’s freedom to share and change
the software licensed under its terms. When using GPL code, no
additional restrictions may be applied to resulting product. In this
way, the GPL is similar to the Borg. If you wish to use GPL code within
your own project, then your own project must be licensed in a compatible
manner with GPL. Thus GPL code tends to begat more GPL code. It is not
permissible under the GPL to use GPL in proprietary software while
keeping that software closed source.

### MIT and BSD Licenses

For others with no philosophical objection to using open source software
within proprietary software, the
[MIT](http://www.opensource.org/licenses/mit-license.php) license or the
new [BSD](http://www.opensource.org/licenses/bsd-license.php) license
may be more appropriate.

In essence, these licenses do not provide any restrictions on how the
software may be copied, modified, or incorporated into other projects
apart from attribution. Thus you can take code from a BSD licensed
project and incorporate it into your proprietary software. You can even
try to sell BSD licensed software as is (technically you can do this
with GPL too), but this is as difficult as selling ice to Eskimos.
Because you cannot restrict others from simply obtaining the source
code, selling open source licensed software as is makes for a difficult
proposition. You had better add a lot of value to be successful. Popular
.NET projects such as [Subtext](http://subtextproject.com/),
[DasBlog](http://www.dasblog.net/), and [RSS
Bandit](http://www.rssbandit.org/) are all licensed under the BSD
license.

UPDATE 2013/07/17: GitHub recently created a site to help folks
unfamiliar with licensing make an informed choice. It’s called
[http://choosealicense.com/](http://choosealicense.com/) and worth
checking out.

Stay Tuned for [Part
3](https://haacked.com/archive/2006/01/26/WhoOwnstheCopyrightforAnOpenSourceProject.aspx "Part 3").
