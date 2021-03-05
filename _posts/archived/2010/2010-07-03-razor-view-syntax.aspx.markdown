---
title: Razor View Syntax
tags: [aspnet,aspnetmvc,razor]
redirect_from: "/archive/2010/07/02/razor-view-syntax.aspx/"
---

UPDATE: Check out my [Razor View Syntax Quick
Reference](https://haacked.com/archive/2011/01/06/razor-syntax-quick-reference.aspx "Razor Quick Reference")
for a nice quick reference to Razor.

There’s an old saying, “Good things come to those who wait.” I remember
when I first joined the ASP.NET MVC project, I (and many customers)
wanted to include a new streamlined custom view engine. Unfortunately at
the time, it wasn’t in the card since we had higher priority features to
implement.

Well the time for a new view engine has finally come as announced by
[Scott Guthrie](http://weblogs.asp.net/scottgu/ "Scott Guthrie's Blog")
in [this very detailed blog
post](http://weblogs.asp.net/scottgu/archive/2010/07/02/introducing-razor.aspx "Introducing Razor").

[![Photo by "clix"
http://www.sxc.hu/photo/955098](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/RazorViewEngineSyntax_7125/razor_thumb.jpg "Photo by "clix" http://www.sxc.hu/photo/955098")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/RazorViewEngineSyntax_7125/razor_2.jpg)

While I’m very excited about the new streamlined syntax, there’s a lot
under the hood I’m also excited about.

[Andrew Nurse](http://blog.andrewnurse.net/ "co-worker met friend"), who
writes the parser for the Razor syntax, [provides more under-the-hood
details in this blog
post](http://blog.andrewnurse.net/2010/07/03/IntroducingRazorNdashANewViewEngineForASPNet.aspx "Introducing Razor").
Our plan for the next version of ASP.NET MVC is to make this the new
default view engine, but for backwards compatibility we’ll keep the
existing WebForm based view engine.

As part of that work, we’re also focusing on making sure ASP.NET MVC
tooling supports any view engine. In ScottGu’s blog post, if you look
carefully, you’ll see
[Spark](http://sparkviewengine.com/ "Spark View Engine") listed in the
view engines drop down in the Add View dialog. We’ll make sure it’s
trivially easy to add Spark, Haml, whatever, to an ASP.NET MVC project.
:)

Going back to Razor, one benefit that I look forward to is that unlike
an ASPX page, it’s possible to fully compile a CSHTML page without
requiring the ASP.NET pipeline. So while you can allow views to be
compiled via the ASP.NET runtime, it may be possible to fully compile a
site using T4 for example. A lot of cool options are opened up by a
cleanly implemented parser.

In the past several months, our team has been working with other teams
around the company to take a more holistic view of the challenges
developing web applications. ScottGu recently blogged about the results
of some of this work:

-   [SQLCE
    4](http://weblogs.asp.net/scottgu/archive/2010/06/30/new-embedded-database-support-with-asp-net.aspx "SQLCE 4 for ASP.NET")
    – Medium trust x-copy deployable database for ASP.NET.
-   [IIS
    Express](http://weblogs.asp.net/scottgu/archive/2010/06/28/introducing-iis-express.aspx "IIS Express")
    – A replacement for Cassini that does the right thing.

The good news is there’s a lot more coming! In some cases, we had to
knock some heads together (our heads and the heads of other teams) to
drive focus on what developers really want and need rather than too much
pie in the sky architectural astronomy.

I look forward to talking more about what I’ve been working on when the
time is right. :)

