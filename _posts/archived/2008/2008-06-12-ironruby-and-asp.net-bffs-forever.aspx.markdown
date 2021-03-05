---
title: IronRuby and ASP.NET BFFs Forever
tags: [aspnetmvc,languages,ruby]
redirect_from: "/archive/2008/06/11/ironruby-and-asp.net-bffs-forever.aspx/"
---

UPDATE: I just posted the [working demo
here](https://haacked.com/archive/2008/07/20/ironruby-aspnetmvc-prototype.aspx "IronRuby ASP.NET MVC Prototype").

I wish I could have been there, but I was celebrating my son’s first
birthday (which gives me an opening to gratuitously post a picture of
the kid here).

[![cody-birthday](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IronRubyandASP.NETBFFsForever_836B/cody-birthday_thumb.jpg "cody-birthday")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IronRubyandASP.NETBFFsForever_836B/cody-birthday_2.jpg "Cody's birthday")

I’m talking about Tech-Ed 2008 Orlando of course where [John
Lam](http://www.iunknown.com/ "John Lam's IUnknown blog") presented a
demo of [IronRuby running on top of ASP.NET
MVC](http://www.iunknown.com/2008/06/ironruby-and-aspnet-mvc.html "IronRuby and ASP.NET MVC").

This demo builds on prototype work I’ve done with [defining ASP.NET MVC
routes and views using
IronRuby](https://haacked.com/archive/2008/04/22/defining-asp.net-mvc-routes-and-views-in-ironruby.aspx "Defining MVC Routes").

The final missing piece was defining controllers using IronRuby. Working
with members of John’s team, Levi (a dev on the ASP.NET MVC team) made
the necessary adjustments to get a prototype IronRuby controller working
with ASP.NET MVC.

**Disclaimer:** This is all a very rough prototype that we’ve been doing
in our spare time for fun. We just wanted to prove this could work at
all.

Unfortunately, we can’t release the demo yet because it relies on
unreleased ASP.NET MVC code. When we deploy our next CodePlex interim
release of ASP.NET MVC, the demo that John provided should actually
work. :)

And for those that don’t know, BFF is an acronym for Best Friends
Forever. Yes, the “Forever” is redundant in the blog title, but it’s
just how people use it. As an aside, I found out recently that a good
buddy of mine in L.A. is actually working on the set of a new show
tentatively titled “Paris Hilton’s My New BFF”, where contestants
compete to become [Paris Hilton’s new Best
Friend](http://www.reuters.com/article/televisionNews/idUSN1335541620080315 "Who wants to be Paris Hilton's Best Friend?").
