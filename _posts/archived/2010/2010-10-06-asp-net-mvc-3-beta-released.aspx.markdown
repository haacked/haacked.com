---
title: ASP.NET MVC 3 Beta Released
tags: [aspnetmvc,aspnet,code]
redirect_from: "/archive/2010/10/05/asp-net-mvc-3-beta-released.aspx/"
---

UPDATE: This post is a out of date. We recently released the [Release
Candidate for ASP.NET MVC
3](https://haacked.com/archive/2010/11/09/asp-net-mvc-3-release-candidate.aspx "ASP.NET MVC 3 RC Released").

Wow! It’s been a busy two months and change since we released [Preview
1](https://haacked.com/archive/2010/07/27/aspnetmvc3-preview1-released.aspx "ASP.NET MVC 3 Preview 1")
of ASP.NET MVC 3. Today I’m happy (and frankly, relieved) to announce
**the Beta release of ASP.NET MVC 3**. Be sure to read [Scott Guthrie’s
announcement as
well](http://weblogs.asp.net/scottgu/archive/2010/10/06/announcing-nupack-asp-net-mvc-3-beta-and-webmatrix-beta-2.aspx "Announcing MVC 3").

[![onward](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ASP.NETMVC3BetaReleased_12A2D/Onward_thumb_1.jpg "onward")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ASP.NETMVC3BetaReleased_12A2D/Onward_4.jpg)
*Credits: Image from ICanHazCheezburger
http://icanhascheezburger.com/tag/onward/*

Yes, you heard me right, we’re jumping straight to Beta with this
release! To try it out…

-   [Install it
    immediately](http://www.microsoft.com/web/gallery/install.aspx?appid=MVC3 "Install ASP.NET MVC 3 via Web PI")
    via the Web Platform Installer (Web PI).
-   OR, Download the [Installer
    files](http://go.microsoft.com/fwlink/?LinkID=191795 "Download Details for ASP.NET MVC 3 Beta")
    and install it manually.

As always, be sure to read the [release
notes](http://www.asp.net/learn/whitepapers/mvc3-release-notes "ASP.NET MVC 3 Release Notes")
(also available [as a Word
doc](http://download.microsoft.com/download/8/8/D/88D72201-4230-4E19-BFDA-5868B350AA09/ASP.NET-MVC-3-Beta-Release-Notes.doc "ASP.NET MVC 3 Release Notes Word Doc")
if you prefer that sort of thing) for all the juicy details about what’s
new in ASP.NET MVC 3.

A big part of this release focuses on polishing and improving features
started in Preview 1. We’ve made a lot of improvements (and changes) to
our support for Dependency Injection allowing you to control how ASP.NET
MVC creates your controllers and views as well as services that it
needs.

One big change in this release is that client validation now is built on
top of jQuery Validation in an unobtrusive manner. In ASP.NET MVC 3,
jQuery Validation is the default client validation script. It’s pretty
slick so give it a try and let us know what you think.

Likewise, our Ajax features such as the Ajax.ActionLink etc. are now
built on top of jQuery. There’s a way to switch back to the old behavior
if you need to, but moving forward, we’ll be leveraging jQuery for this
sort of thing.

### Where’s the Razor Syntax Highlighting and Intellisense?

This is probably a good point to stop and provide a little bit of bad
news. One of the most frequently asked questions I hear is when are we
going to get syntax highlighting? Unfortunately, it’s not yet ready for
this release, but the Razor editor team is hard at work on it and we
will see it in a future release.

I know it’s a bummer (believe me, I’m bummed about it) but I think it’ll
make it that much sweeter when the feature arrives and you get to try it
out the first time! See, I’m always looking for that silver lining. ;)

### What’s this NuPack Thing?

That’s been the other major project I’ve been working on which has been
keeping me very busy. I’ll be posting a follow-up blog post that talks
about that.

### What’s Next?

The plan is to have our next release be a Release Candidate. I’ve
updated [the
Roadmap](http://aspnet.codeplex.com/wikipage?title=Road%20Map&referringTitle=MVC "ASP.NET MVC Roadmap in CodePlex.com")
to provide an idea of some of the features that will be coming in the
RC. For the most part, we try not to add too many features between Beta
and RC preferring to focus on bug fixing and polish.

