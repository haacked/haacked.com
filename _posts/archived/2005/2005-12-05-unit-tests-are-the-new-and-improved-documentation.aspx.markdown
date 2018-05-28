---
layout: post
title: Unit Tests Are The New And Improved Documentation
date: 2005-12-05 -0800
comments: true
disqus_identifier: 11301
categories: []
redirect_from: "/archive/2005/12/04/unit-tests-are-the-new-and-improved-documentation.aspx/"
---

In his post [Unit tests are the new
documentation](http://www.lazycoder.com/weblog/index.php/archives/2005/12/04/unit-tests-are-the-new-documentation/)
Scott sees unit tests as an undue burden much like documentation.
Documentation, typically written after the fact, inevitably falls sway
to the laws of entropy and becomes hopelessly outdated.

On one level, I do agree that unit tests are the new documentation, but
I would add new and **improved**! Unit tests are a great way to provide
code samples to document how an API is to be used. Though agile
proponents point out that the code (when well written) is the
documentation, well named methods and variables only tell you what it
does and what they are used for, but not how to use it. There are
several ways that I see unit tests as better than your traditional
documentation.

**Unit Tests Are Closer To The Code**\
 First of all, unlike traditional documentation sitting in a binder or
word doc on the corporate network, your unit tests are typically in a
project within the same solution the unit tests test. It is even
possible to place the unit tests within the code being tested, but I
personally avoid this approach. So it isn’t as close to the code as the
code itself, but it is far closer than a word doc sitting on the network
somewhere.

**Unit Tests Are Hard to Ignore**\
 Once you’ve adopted unit testing and made it a habit, having unit tests
get out of synch with your code is hard to ignore. For one thing, unlike
documentation, unit tests compile. If they don’t compile, well you know
they are out of date. Secondly, if the unit tests compile, but do not
pass, then you know they are out of date. Documentation doesn’t provide
any sort of notice.

**Unit Tests Are Written As You Go**\
 The holy grail of documentation is to write it as you go, but you
rarely see that in practice. However in places I have worked that have
adopted Test Driven Development, I have not seen this same problem with
unit tests. Unit tests are written as you develop. It becomes part of
the development process. In part, once a developer sees the value in
unit tests, it is not hard to integrate it into his/her programming
style.

**The Question of Discipline**\
 Many who question or are unfamiliar with unit testing assume that it
requires a lot of discipline and will be the first thing to fall out
during tight deadlines. But this isn’t necessarily true when TDD is
adopted as good software practice. It doesn’t require much more
discipline than using source control. These days, I hardly realize
source control requires any discipline at all because it is so well
integrated into my work style. But if you think about it, it really does
take discipline to do it properly. Especially if you employ branching.
Likewise TDD does take some discipline, but it becomes second nature.
And the benefits are worth it. Documentation, though providing some
benefits, requires much more discipline to keep in synch.

