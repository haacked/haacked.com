---
title: OriginUrl Supports Regular Expressions
date: 2006-10-19 -0800
tags: []
redirect_from: "/archive/2006/10/18/A_Hole_In_Medium_Trust_Regarding_Web_Permission.aspx/"
---

In a [recent post I
ranted](https://haacked.com/archive/2006/10/17/Why_Oh_Why_Couldnt_WebPermission_Be_Part_Of_Medium_Trust.aspx "Why Can’t WebPermission Be Part Of Medium Trust")
about how ASP.NET denies `WebPermission` in Medium Trust. I also
mentioned that there may be some legitimate reasons to deny this
permission based on [this hosting
guide](http://weblogs.asp.net/hosterposter/archive/2006/03/22/440886.aspx "Guide to Medium Trust for hosting").

Then [Cathal](http://developers.ie/blogs/cconnolly/ "Cathal's Blog")
(thanks!) emailed me and pointed out that the `originUrl` does not take
wildcards, **it takes a regular expression**.

So I updated the \<`trust />` element of `web.config` like so:

```csharp
<trust level="Medium" originUrl=".*" />
```

Lo and Behold, it works! Akismet works. Trackbacks work. All in Medium
Trust.

Of course, a hosting provider can easily override this as [Scott
Guthrie](http://weblogs.asp.net/scottgu "Scott Guthrie") points out in
my comments. I need to stop blogging while sleep deprived. I have a
tendency to say stupid things.

~~Now a smart hosting company can probably create a custom medium trust
policy in order to make sure this doesn’t work, but as far as I can
tell, this completely works around the whole idea of denying
`WebPermission` in Medium Trust.~~

~~If I can simply add a regular expression to allow all web requests,
what’s the point of denying WebPermission?~~

