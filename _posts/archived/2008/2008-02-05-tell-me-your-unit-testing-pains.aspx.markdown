---
title: Tell Me Your Unit Testing Pains
tags: [tdd,aspnet,code]
redirect_from: "/archive/2008/02/04/tell-me-your-unit-testing-pains.aspx/"
---

If you know me, you know I go through
[great](https://haacked.com/archive/2007/06/19/unit-tests-web-code-without-a-web-server-using-httpsimulator.aspx "HttpSimulator") [pains](https://haacked.com/archive/2006/05/30/ATestingMailServerForUnitTestingEmailFunctionality.aspx "A Testing Mail Server")
to write automated unit tests for my code. Some might even call me anal
about it. Those people know me too well.

For example, in the active branch of Subtext, we have [882 unit
tests](http://build.subtextproject.com/ccnet/server/local/project/SubText-1.9/build/log20080120121001Lbuild.1.9.6.322.xml/MbUnitDetailsBuildReport.aspx "Subtext Unit Tests"),
of which I estimate I wrote around 800 of those. Yep, if you’re browsing
the Subtext unit test code and something smells bad, chances are I
probably dealt it.

![IMG\_1117](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TellMeYourUnitTestingPains_13FCD/IMG_1117_1.jpg)
Unfortunately, by most definitions of *Unit Test,* most of these tests
are really *integration tests*. Partly because I was testing legacy code
that and partly because I was blocked by the framework, not every method
under test could be easily tested in isolation.

Whether you’re a TDD fan or not, I think most of us can agree that unit
testing your own code is a necessary practice, whether it is manually or
automated.

I still think it’s worthwhile to take that one step further and
*automate* your unit tests whenever possible and where it makes most
sense (for large values of *make sense*).

When writing an automated unit test, the key is to try and isolate the
unit under test (typically a method) by both controlling the external
dependencies for the method and being able to capture any side-effects
of the method.

Sometimes though, external code that your code makes calls into can
sometimes be written in such a way that makes it challenging to test
*your* own code. Often, you have to resort to building all sorts of
Bridge or Adapter classes to abstract away the thing you’re calling.
Sometimes you are plain stuck.

What “external code” might exhibit this characteristic of making your
own code hard to test? I don’t have anything in particular in mind
but...oh...off the top of my head if you made me pick one totally
spontaneously, I might mention my way of example one little piece of
code called the .NET Framework.

For the most part, I’ve had very few problems with the Base Class
Libraries or other parts of the Framework. Most of my testing woes came
when writing code against ASP.NET. The ASP.NET MVC framework hopes to
help address some of that.

I’ve been in a lot of internal discussions recently talking with various
people and teams about testable code. In order to contribute more value
to these discussions, I am trying to gather specific cases and scenarios
where testing your code is really painful.

What I am *not* looking for is feedback such as

“*It’s hard to write unit tests when writing a Foo
application/control/part*”.

or

“*Class `Queezle` should really make method `Bloozle` public.*”

Perhaps `Bloozle` should be public, but I am not interested in
theoretical pains. **If the inaccessibility of `Bloozle` caused a real
problem in a real unit test, that's what I want to hear.**

What I *am* looking for is specifics! Concrete scenarios that are
blocked or extremely painful.Including the actual unit test is even
better! For example...

“*When writing ASP.NET code, I want to have helper methods that accept
an `HttpContext` instance and get some values from its various
properties and then perform a calculation. Because `HttpContext` is
sealed and is tightly coupled to the ASP.NET stack, I can't mock it out
or replace it with a test specific subclass. This means I always have to
write some sort of bridge or adapter that wraps the context which gets
tedious.*”

Only, you can’t use that one, because I already wrote it. Ideally, I’d
love to hear feedback from across the board, not just ASP.NET. Got
issues with WPF, WCF, Sharepoint Web Parts, etc... tell us about it.
Please post them in my comments or on your blog and link to this post.
Your input is very valuable and could help shape the future of the
Framework, or at least help me to sound like I am clued into customer
needs next time I talk to someone internal about this. ;)

