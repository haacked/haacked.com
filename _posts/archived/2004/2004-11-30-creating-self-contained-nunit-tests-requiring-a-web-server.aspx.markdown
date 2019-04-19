---
title: Creating Self Contained NUnit Tests Requiring A Web Server
date: 2004-11-30 -0800
tags: [code]
redirect_from: "/archive/2004/11/29/creating-self-contained-nunit-tests-requiring-a-web-server.aspx/"
---

This is a [fabulous
post](http://www.hanselman.com/blog/PermaLink.aspx?guid=944a5284-6b8d-4366-81e8-2e241401e1b3)
(did I just say "fabulous"?) on how to create self contained NUnit tests
when you need a web server.

As you may know (assuming you've read this blog for a while, which is a
BIG assumption), I'm a big fan of self contained Unit Tests. It's a key
component to having a [self contained location independent build
process](https://haacked.com/archive/2004/08/26/creating-a-sane-build-process.aspx).

I have an approach similar to Scott's in unit testing some of the
functionality of [RSS Bandit](http://www.rssbandit.org/). For example, I
have tests that will create a web directory, start a Cassini web server,
and then use the RssLocator class to search for RSS feeds. However, one
problem I had that I hadn't resolved was the issue that Cassini.dll
needed to be loaded in the GAC.

If you were to obtain a fresh build of the RSS Bandit unit tests and
didn't have Cassini.dll registered, many of the tests would fail. I was
planning to add code to register Cassini into the GAC, but Scott has
shown the path to a better way. He demonstrates a method such that
doesn't require Cassini to be placed in the GAC. Brilliant! Once I get
home, I shall make this change and truly rejoice at having self
contained tests.

