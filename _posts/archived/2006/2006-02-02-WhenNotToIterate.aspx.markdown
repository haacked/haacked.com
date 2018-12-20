---
title: When Not To Iterate
date: 2006-02-02 -0800
disqus_identifier: 11645
tags: []
redirect_from: "/archive/2006/02/01/WhenNotToIterate.aspx/"
---

As a kid, one of my favorite tools I had was a shiny red cased
Victorinox swiss army knife. I’d carry that sucker around with me
everywhere, using the main blade to tackle nearly every problem like a
caveman with a stick. Until one day I sliced my finger trying to stick a
paper cup to a tree so we could throw ninja stars at it, but that’s a
story for another time.

As software developers, we have a swiss army knife of approaches to
tackling any given problem. And often, we like to pull out the same
blade over and over again. For example, one tool that we pull out nearly
all the time is the tool of “iterative” development.

As [Atwood](http://www.codinghorror.com/blog/ "Jeff Atwood Blog") said
in the comments to Micah’s post on [harmful
requirements](http://micahdylan.com/archive/2006/01/30/Requirementsmaybeharmful.aspx),
you gotta go “iterative”.

However, sometimes you really do need to pull out a different tool in
your swiss army knife. For large projects, breaking it up into phases
and iterations makes sense most of the time, but there are times where
an iteration itself may be longer than other iterations. Sometimes such
an iteration itself becomes a mini project that requires BDUF.

Here’s a scenario to chew on. Suppose an iteration of the project is to
produce a report based on data imported from a couple external sources.
The report only has to produce a single number based on a calculation
applied to the data. This is the type of problem that may not lend
itself well to iteration. The client is interested in the final number,
not any number in between.

Sure you can have an iteration in which you mock up the report to show
the client to get something in front of them, but even this can be
dangerous because you’ve set the expectation that you can deliver the
report. But do you know that until you’ve analyzed the data?

This is a situation we found ourselves in. The requirements appeared to
be describing a specific calculation we needed to produce. But after a
careful look at all the fields we were getting in, the data required to
produce that report just isn’t there. And we won’t be getting that data.
Yet we have to produce this report by Monday. It seems we’ve walked to
the blackboard to solve a series of two equations, but each equation has
three variables. “Does Not Compute”

It seems to me that this is a classic situation where some up front
analysis would have saved our butt and set expectations properly.
Unfortunately, we weren’t brought in to this project till later and had
to take it on faith that the analysis had occurred, but it was lacking.
Someone, anyone, should have looked at the columns of data we would be
receiving, and reconciled the data with the calculation we would be
producing. I’m not sure how we can iterate ourselves out of that
problem.

In our situation, it may well be that the real calculation we need to
produce is much simpler and less useful than the one we thought we were
going to produce. So we may be out of the fire on this one. But should
the stakeholder disagree, we’ve got issues.

