---
title: Why Oh Why Couldn't WebPermission Be Part Of Medium Trust?
tags: [aspnet]
redirect_from: "/archive/2006/10/16/Why_Oh_Why_Couldnt_WebPermission_Be_Part_Of_Medium_Trust.aspx/"
---

![Source:
http://macibolt.hu/pag/goldilock.html](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WhyOhWhyCouldntWebPermissionBePartOfMedi_11C05/threebears%5B3%5D.gif)This
is a bit of rant born out of some frustrations I have with ASP.NET. When
setting the trust level of an ASP.NET site, you have the following
options:

`Full, High, Medium, Low, Minimal`

It turns out that many web hosting companies have chosen to congregate
around Medium trust as a sweet spot in terms of tightened security while
still allowing decent functionality. Only natural as it *is* the one in
the middle.

For the most part, I am sure there are very good reasons for which
permissions make it into Medium trust and which ones are not allowed.
But some decisions seem rather arbitrary. For example, `WebPermission`.
Why couldn’t that be a part of the default Medium trust configuration? I
mean really? Why not? (Ok, there *are* really good reasons, but
remember, this is a rant, not careful analysis. Bear with me. Let me get
it off my chest.)

Web applications have very good reason to make web requests (ever hear
of something called a *web service*. They may take off someday) and how
damaging is that going to be to a hosting environment. I mean, put a
throttle on it if you are that concerned, but don’t rule it out
entirely!

I really do want to be a good ASP.NET citizen and support Medium Trust
with applications such as Subtext, but what a huge pain it is when some
of the best features do not work under Medium Trust. For example,
[Akismet](http://akismet.com/ "Akismet").

Akismet makes a web request in order to check incoming comments for
spam. I tried messing around with wildcards for the `originUrl`
attribute of the `<trust />` element, but they don’t work. In fact, I
only found a [single blog
post](http://developers.ie/blogs/cconnolly/archive/2005/07/01/1498.aspx "Wildcards for originUrl")
that said it would work, but no documentation that backed that claim up.

Instead, you need access to the `machine.config` file (as the previously
linked blog post describes), which no self respecting host is going to
just give you willy nilly. Nope. In order to get Akismet to work under
medium trust, I have to tell Subtext users that they must beg, canoodle,
and plead with their host provider to update the `machine.config` file
to allow unrestricted access to the `WebPermission`. Good luck with
that.

If they don’t give unrestricted access, then they need to add an
`originURl` entry for each URL you wish to request. Hopefully
`machine.config` entries do allow wildcards because the URL for an
Akismet request includes the Akismet API code. Otherwise running an
Akismet enabled multiple user blog in a custom Medium Trust environment
would be a royal pain.

Hopefully you can see the reason behind all my bitching and moaning. A
major goal for Subtext is to provide a really simple and easy
installation experience. At least as easy as possible for installing a
web application.  Having an installation step that requires groveling
does not make for a good user experience.  But then again, security and
usability have always created tension between them.

[Scott Watermasysk](http://scottwater.com/blog/ "Ancora Imparo") points
out a [great
guide](http://weblogs.asp.net/hosterposter/archive/2006/03/22/440886.aspx "Enabling Web Permission In Medium Trust")
to enabling `WebPermission` in Medium Trust for hosters. So if you’re
going to be groveling, at least you have a helpful guide to give them.
The guide also points out the security risks in involved with Medium
Trust.

Related Posts:

-   [More on Medium Trust and
    Trackbacks](https://haacked.com/archive/2006/07/10/MoreOnMediumTrustAndTrackbacks.aspx "Medium Trust and Trackbacks")



