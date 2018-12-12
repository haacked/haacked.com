---
title: Refreshing ASP.NET Dynamic Language Support
date: 2008-09-23 -0800
disqus_identifier: 18532
categories:
- asp.net
- dlr
redirect_from: "/archive/2008/09/22/refreshing-asp.net-dynamic-language-support.aspx/"
---

This afternoon we released a refresh of our DLR/IronPython support for
ASP.NET, now called “[ASP.NET Dynamic Language
Support](http://www.codeplex.com/aspnet/Wiki/View.aspx?title=Dynamic%20Language%20Support "ASP.NET MVC Dynamic Language Support")”,
on our [CodePlex site](http://codeplex.com/aspnet).

This was originally part of our July 2007 ASP.NET Futures package, along
with several other features. As updates to these features were made
available, we would have liked to remove them from the package, but we
wanted to wait till everything within the package was updated.

Well that time has come. This CodePlex release contains two exceedingly
simple sample applications, one for WebForms and one for [ASP.NET
MVC](http://asp.net/mvc "ASP.NET MVC Website"). It’s compiled against
the latest DLR assemblies, and our goal is to continue to push it
forward fixing bugs here and there. Keep in mind that this initial
refresh is pretty barebones and doesn’t contain everything that the
original package contained because certain features (such as the project
system) are still being updated.

I won’t go too deeply into the specifics of how to use it. Instead, be
sure to check out [David Ebbo’s whitepaper on IronPython and
ASP.NET](http://www.asp.net/IronPython/whitepaper/ "IronPython Whitepaper")
which was written a while ago, but still mostly relevant. Also, [Jimmy
Schementi](http://blog.jimmy.schementi.com/ "Jimmy's Blog") from the DLR
team has written a nice [brief write-up on this
release](http://blog.jimmy.schementi.com/2008/09/aspnet-dynamic-language-support.html "ASP.NET DLR Support").

I have the pleasure of taking over as the PM for this feature (in MS
parlance we’d say I “own” this feature now) which nicely complements my
duties as the PM for ASP.NET MVC. If you’ve followed my blog, you know I
have an [interest in dynamic
languages](https://haacked.com/archive/2008/07/20/ironruby-aspnetmvc-prototype.aspx "IronRuby ASP.NET MVC Prototype")
and now I can channel that interest into work time, rather than on my
own time. :)

This initial release only has
[IronPython](http://www.codeplex.com/IronPython "IronPython on CodePlex")
support, but [IronRuby](http://www.ironruby.net/ "IronRuby") support
will be coming soon. This gives me an opportunity to learn a bit about
Python, and let me tell you, the fact that whitespace matters in this
language can be nice within a normal code file, but a real pain within a
view.

One nice thing about this implementation above and beyond my old
IronRuby prototype is that it has true support for a `Global.py file`,
the IronPython equivalent for `Global.asax.cs`. This allowed me to
define my routes in IronPython directly in that file rather than reading
in a separate file. I did implement some helper methods in C\# that make
it easy to define routes using a Python dictionary.

