---
title: TDD Is Great...Except When It Isn't
date: 2004-10-17 -0800
disqus_identifier: 1382
categories: []
redirect_from: "/archive/2004/10/16/tdd-is-greatexcept-when-it-isnt.aspx/"
---

Saw this
[post](http://pluralsight.com/blogs/craig/archive/2004/10/17/2852.aspx)
by Craig Andera about Test Driven Development and I have to say I
completely agree with him.

I've been doing TDD for several years now and I tend to restrict it to
testing business and data access layers. Currently, it's not practical
to perform comprehensive TDD for UI layers (though tools like
[NUnitAsp](http://nunitasp.sourceforge.net/) and
[NUnitForms](http://nunitforms.sourceforge.net/) help).

Even these tools don't address one of the biggest challenges when it
comes to testing a UI layer. The UI layer is the layer most likely to
change and change often. After spending a few hours building your tests,
some guy in marketing will call you up and say, "can we replace that
button with a table of data with clickable rows?" What now?

Unit tests tend to be quite fragile when faced with a changing UI layer.
Human testers have no problem dealing with such change, but your unit
tests definitely will.

My recommendation for testing the UI layer are combining test scripts
(for human testers) along with writing unit tests when a bug is found in
the UI layer. At that point the UI should hopefully be frozen enough
that writing a unit test that exposes a bug and then fixing the bug will
be a worthwhile investment for regression purposes.

