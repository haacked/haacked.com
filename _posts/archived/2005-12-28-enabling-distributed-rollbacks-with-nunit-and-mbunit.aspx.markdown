---
layout: post
title: "Enabling Distributed Rollbacks With NUnit and MbUnit"
date: 2005-12-28 -0800
comments: true
disqus_identifier: 11377
categories: []
---
In some of my projects, I take a less purist approach to unit testing in
that I allow [unit tests to touch the
database](http://haacked.com/archive/2005/10/21/10941.aspx). In order to
“reset” the database to the state it was in prior to the test, the code
enlists COM+ 1.5 transactions via the [RollBack] attribute in MbUnit
([there’s also one for
NUnit](http://haacked.com/archive/2005/06/10/4580.aspx)).

My preference is to have a local copy of the database when developing,
but there have been times when this was not possible and I needed to run
my unit tests against a remote database I did not have full control
over. The problem is that the RollBack enlists the local Distributed
Transaction Coordinator to acquire a transaction. If the database server
is on a remote server, it attempts to use Transaction Internet Protocol
(TIP) instead which is disabled by default.

At the time, I was being lazy and left it alone, moving on to something
else. Fortunately, my laziness has paid off as another [smart person dug
into it and figured out the
details](http://stevenharman.net/blog/archive/2005/12/28/MbUnitCOMTransactions.aspx)
to getting this to work properly. See, sometimes laziness does pay off.
;) [Thanks Steve](http://stevenharman.net/blog/)!

