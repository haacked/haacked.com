---
layout: post
title: "The ABCs of Alpha, Beta, CTP"
date: 2008-08-15 -0800
comments: true
disqus_identifier: 18524
categories: [code,asp.net mvc,asp.net]
---
A commenter to [my last
post](http://haacked.com/archive/2008/08/14/aspnetmvc-not-in-sp1.aspx)
asks the following question,

> What is the difference between a beta, a CTP, a fully-supported out of
> band release, an RTM feature, and a service pack?

The answer you get will differ based on who you ask, but I’ll give you
my two cents on what these terms mean.

### Beta

Let’s start with Beta. A great starting point is this post by Jeff
Atwood entitled [Alpha, Beta, and Sometimes
Gamma](http://www.codinghorror.com/blog/archives/001159.html "Alpha, Beta, and Sometimes Gamma").

> The software is complete enough for external testing -- that is, by
> groups outside the organization or community that developed the
> software. Beta software is usually feature complete, but may have
> known limitations or bugs. Betas are either closed (private) and
> limited to a specific set of users, or they can be open to the general
> public.

With the ASP.NET MVC project, all features we plan to implement for RTM
should be complete for our Beta. However, the Beta period can influence
this and if it seems extremely important, we may take on small DCRs
(Design Change Requests).

### CTP

CTP stands for Community Technology Preview. It's generally an
*incomplete* preview of a new technology in progress. These usually come
out before beta and are a way to gather feedback from the community
during the development of a product. This is similar to an Alpha release
per Jeff’s hierarchy, except that at Microsoft, we generally do put CTPs
in a public location.

With the ASP.NET MVC project, we no longer use the term CTP and simply
use the term “Preview”. I think this is due to running out of our TLA
(Three Letter Acronym) budget for the year. Our previews do still
undergo a QA test pass and are released to the [ASP.NET
website](http://asp.net/).

### Daily Builds / Interim Releases

The commenter didn’t ask about this, but I thought I would mention it.
In many open source projects, you can get a daily build of the software
directly from their source code repository. For example, with Subtext,
if you want to grab the most recent build, you can go to our [builds
archive](http://build.subtextproject.com/builds/archive/ "Builds"). A
daily build is really for those who like to play with fire, as they
usually are not tested, and could represent work in progress that is not
even working at all.

The closest thing the ASP.NET MVC team has to this is with our periodic
“Interim releas”, a term we just made up, that is pushed out to
[CodePlex](http://codeplex.com/aspnet) and not placed on the ASP.NET
website, because of the more mainstream nature of that site.

As much as these CodePlex releases are for the cutting edge audience,
being Microsoft, we can’t simply put daily builds out there and say
you’re on your own. At least not yet. So these CodePlex builds are
sanity checked by our QA team and by me, but they do not go under a full
test pass like our Preview releases do. This is an area of
experimentation for the ASP.NET team and so far, is proving successful.

### Fully Supported Out-of-Band release

Internally, we usually call these OOB releases (pronounced “oob” like
it’s spelled).

A Fully Supported Out-of-Band release is a release that is not part of
the Framework (i.e. it's not included in an installation of the .NET
Framework), but is fully supported as if it were. For example, you can
call up PSS (Microsoft's Tech Support) for support on a fully supported
OOB release.

One example of this was “Atlas” which later became Microsoft Ajax and
was rolled into ASP.NET 3.5. ASP.NET MVC 1.0 will be an example of an
OOB release.

### RTM and RTW release

RTM stands for “Released to Manufacturing” and is a throwback to the
days when software was mostly released as CDs. When a project went
“Gold”, it was released to manufacturing who then burned a bunch of CDs
and packaged them up to be put on store shelves. True, this still goes
on today believe it or not, but this mode of delivery is on the decline
for certain types of software.

RTW is a related term that stands for “Released to Web” which is more
descriptive of how software is actually shipped these days. For example,
while we like to use the term RTM internally out of habit, ASP.NET MVC
will actually be RTW.

### Service Pack

A Service Pack (or SP) is simply an RTM (or RTW) release of fixes and/or
improvements to some software. It used to be that SPs rarely included
new features, but it seems to be the norm now that they do. Service
Packs tend to include all the hotfixes and patches released since the
product originally was released, which is convenient for the end user in
not having to install every fix individually.

Technorati Tags:
[beta](http://technorati.com/tags/beta),[ctp](http://technorati.com/tags/ctp),[alpha](http://technorati.com/tags/alpha),[rtw](http://technorati.com/tags/rtw),[rtm](http://technorati.com/tags/rtm)

