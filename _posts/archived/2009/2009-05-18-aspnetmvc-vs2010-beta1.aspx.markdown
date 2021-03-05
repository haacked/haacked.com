---
title: ASP.NET MVC For Visual Studio 2010 Beta 1
tags: [aspnet,code,aspnetmvc]
redirect_from: "/archive/2009/05/17/aspnetmvc-vs2010-beta1.aspx/"
---

This post is now outdated

I apologize for not blogging this over the weekend as I had planned, but
the weather this weekend was just fantastic so I spent a lot of time
outside with my son.

[![the-park](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ASP.NETMVCForVisualStudio2010Beta1_B2C1/the-park_thumb.jpg "the-park")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ASP.NETMVCForVisualStudio2010Beta1_B2C1/the-park_2.jpg)If
you haven’t heard yet, [Visual Studio 2010 Beta 1 is now
available](http://blogs.msdn.com/somasegar/archive/2009/05/18/visual-studio-2010-and-net-fx-4-beta-1-ships.aspx "Visual Studio 2010 and .NET 4 Beta 1 ships")
for MSDN subscribers to download. It will be more generally available on
Wednesday, according to Soma.

You can find a great whitepaper which describes [what is new for web
developers in ASP
4](http://www.asp.net/learn/whitepapers/aspnet40/ "ASP.NET 4 and VS 2010 Web Development Overview")
which is included.

One thing you’ll notice is that [ASP.NET
MVC](http://asp.net/mvc "ASP.NET MVC Website") is not included in Beta
1. The reason for this is that Beta 1 started locking down before MVC
1.0 shipped. ASP.NET MVC will be included as part of the package in VS10
Beta 2.

Right now, if you try and open an MVC project with VS 2010 Beta 1,
you’ll get some error message about the project type not being
supported. The easy fix for now is to remove the ASP.NET MVC
`ProjectTypeGuid` entry as [described by this
post](http://weblogs.asp.net/leftslipper/archive/2009/01/20/opening-an-asp-net-mvc-project-without-having-asp-net-mvc-installed-the-project-type-is-not-supported-by-this-installation.aspx "Opening MVC Project Without having ASP.NET MVC installed").

We’re working hard to have an out-of-band installer which will install
the project templates and tooling for ASP.NET MVC which works with
VS2010 Beta 1 sometime in June on CodePlex. Sorry for the inconvenience.
I’ll blog about it once it is ready.

