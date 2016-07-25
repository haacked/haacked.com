---
layout: post
title: RSS Bandit Code Documentation (CHM)
date: 2004-11-11 -0800
comments: true
disqus_identifier: 1601
categories: []
redirect_from: "/archive/2004/11/10/rss-bandit-code-documentation-chm.aspx/"
---

Using [NDoc](http://ndoc.sourceforge.net/) I've generated an update
version of the [CHM code documentation for RSS
Bandit](http://haacked.com/code/RSSBanditCodeDocumentation.chm). As
you'll see (if you take a look) this documentation is by no means
complete. Many of the public methods need better documentation. Also,
there are no Namespace summaries yet. I plan to spend some time adding
these summaries and some higher level API documentation.

This documentation is intended for interested developers and is meant to
supplement the existing documentation at the RSS Bandit [documentation
website](http://www.rssbandit.org/docs/).

Included in the documentation are three main components: RSSBandit.exe,
NewsComponents.dll, and RSSBandit.UnitTests.dll.

**RSSBandit.exe** is the main application code. The documentation here
covers all the Forms in use etc.

**NewsComponents.dll** contains all the classes used to fetch and parse
RSS feeds as well as NNTP. Much of core logic is contained in this
assembly.

**RssBandit.UnitTest.dll** I included the documentation of this assembly
so that you can read what unit tests we currently have (and thus infer
the many we are missing). The great thing about unit tests is that many
of them are demonstrations of how to use the API (when correctly written
which I can't yet vouch for my own) ;)

