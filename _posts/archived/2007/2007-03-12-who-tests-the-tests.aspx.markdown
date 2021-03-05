---
title: Who Tests The Tests?
tags: [tdd,testing]
redirect_from: "/archive/2007/03/11/who-tests-the-tests.aspx/"
---

[Leon Bambrick](http://secretgeek.net/ "Secret Geek") (aka SecretGeek)
has started a series on [Agile methodologies and Test Driven Development
(TDD)](http://secretgeek.net/agile_v_tdd.asp "Agile and Test Driven: A Marriage Made In Hell?")
in which he brings up his own various hidden objections to TDD in order
to see if his prejudices can be overcome.

One of the questions he asks is an age old argument against TDD. *[Who
Tests the Tests?](http://secretgeek.net/tdd_wttt.asp "Who Tests the Tests?")*Leon
sees potential for a stack overflow since, given that the tests are
code, and that according to TDD, code should be tested, shouldn’t there
be tests for the tests?

**The short answer is that the code tests the tests, and the tests test
the code.**

Huh?

## Testing Atomic Clocks

Let me start with an analogy. Suppose you are travelling with an atomic
clock. How would you know that the clock is calibrated correctly?

One way is to ask your neighbor with an atomic clock (because everyone
carries one around) and compare the two. If they both report the same
time, then you have a high degree of confidence they are both correct.

[![](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WhoTestsTheTests_1055D/734117_watch_thumb1.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WhoTestsTheTests_1055D/734117_watch4.jpg)

If they are different, then you know one or the other is wrong.

So in this situation, if the only question you are asking is, "Is my
clock giving the correct time?", then do you really need a third clock
to test the second clock and a fourth clock to test the third? Not if
all. Stack Overflow avoided!

## Principle of Triangulation

This really follows from the principle of triangulation. Why do sailors
without electronic navigation systems bring three sextants with them on
board a ship?

With one sextant, you could rely on the manafacture testing to assume
its measurements are correct, but wear and tear over time(not much
unlike the wear and tear a codebase suffers over time) might make the
measurements slightly off.

If you take measuremnts with two sextants, then you have enough
information to decide if both are measuring accurately or if one is not.
However in this situation, we need to know **exactly** which measurement
is correct.

So we take a third sextant out. The two sextants that take measurements
most closely together are most likely correct. Accurate enough to [cross
the
Atlantic](http://www.sailingbreezes.com/Sailing_Breezes_Current/Articles/JULY99/HINZITE.HTM "Crossing the Atlantic").

