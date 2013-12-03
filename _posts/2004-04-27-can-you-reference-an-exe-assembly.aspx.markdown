---
layout: post
title: "Pop Quiz: Can you reference an exe assembly"
date: 2004-04-27 -0800
comments: true
disqus_identifier: 374
categories: []
---
With Visual Studio.NET 2005 and on, you can now reference an exe
assembly just fine.

Can you reference an exe assembly? If you answer yes, you are correct.
If you answer no, you may also be correct. It depends on which tool you
are using. It turns out that VS.NET will not let you reference an exe
assembly. However, you can reference an exe via the C\# compiler using
the /r switch.

This is quite problematic for me as I᾿m a firm believer in the benefits
of test-driven development with unit tests (my tool of choice is
[NUnit](http://www.nunit.org/)). I like to have my unit tests in a
separate class library from the code I᾿m testing, and have my UnitTest
assembly reference the code I᾿m testing.

However, if I᾿m working on an exe, VS.NET won᾿t let me reference the
exe. Thus, I either have to add my unit test fixtures to the exe and
have the exe project reference the NUnit class libraries (which I am
loathe to do), or move as much of the logic of the exe into an
extraneous class library just so I can unit test it. Of course there is
a third option which is to use Visual Notepad and the csc command line,
but I᾿d lose a lot of productivity that way. Hopefully this is fixed in
Whidbey.

For more info on test-driven development in C\#, check out this [MSDN
article](http://msdn.microsoft.com/msdnmag/issues/04/04/ExtremeProgramming/ "MSDN Article").

