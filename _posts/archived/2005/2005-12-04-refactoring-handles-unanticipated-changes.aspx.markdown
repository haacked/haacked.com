---
title: Refactoring Handles Unanticipated Changes
date: 2005-12-04 -0800
tags: [patterns,refactoring]
redirect_from: "/archive/2005/12/03/refactoring-handles-unanticipated-changes.aspx/"
---

[Sam Gentile is
preaching](http://samgentile.com/blog/archive/2005/12/04/32144.aspx) and
I am in the choir. I’ve talked about the benefits of unit testing and
refactoring in the past, but Sam makes this great point.

> [BDUF](http://xp.c2.com/BigDesignUpFront.html) makes a huge gamble
> that all the business and development people can think of all the X's
> up front in the "requirements" stage. In my 22 years of software
> development, I have very rarely seen that this is the case, Why? Many
> the aspects of design of software and the problem are completely
> unknown until one begins to write the test or the code. Code speaks to
> you. It tells you which way to go. Often we need to learn what its
> saying, upgrade our original understandings. Thats why
> Red-Green-Refactor works so well. You think a bit, write a test, write
> some code, and then refactor it based on the new learnings that you
> have made while doing it.

This is in response to the critics of refactoring and TDD who suggest
that software developers simply spend more time thinking about the code
up front in order to produce better code. But as Sam points out, what
use is better code if it does the **wrong thing**?

And this point is crucial. When you wrote the code, it may well be doing
the **right thing**. But somewhere down the line, some biz dev guy is
going to realize what he sold the client is completely different than
what he told you. Now your correct code is incorrect and you don’t have
the safety net of unit tests to help you refactor the code to now
correct thing. This, Virginia, is how the real world works.

