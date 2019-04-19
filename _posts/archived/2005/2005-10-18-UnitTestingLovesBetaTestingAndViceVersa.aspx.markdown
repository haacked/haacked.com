---
title: Unit Testing Loves Beta Testing And Vice Versa
date: 2005-10-18 -0800 9:00 AM
tags: [testing,methodologies]
redirect_from: "/archive/2005/10/17/UnitTestingLovesBetaTestingAndViceVersa.aspx/"
---

[Jeff links](http://www.codinghorror.com/blog/archives/000420.html) to a
post by Wil Shipley [criticizing unit
testing](http://wilshipley.com/blog/2005/09/unit-testing-is-teh-suck-urr.html).
You knew I had to chime in on this... ;)

I won’t rehash what [I’ve already written on the
subject](https://haacked.com/archive/2004/12/06/1704.aspx), but will
merely try to add a couple key points.

There are a couple of misconceptions I want to clear up.

First, the proponents of unit testing are not promoting it as the end
all and be all of software development. However, I would say that unit
tests are very important when done right, much in the same way version
control is important when done right. Unit tests should be applied using
a cost benefit analysis just as you’d do anything else. For example,
problematic or tricky code should have more unit tests. Important code
(such as the code in a banking system that performs a calculation)
should have more unit tests. Simple getters and setters on the other
hand can do without.

> But I've NEVER, EVER seen a structured test program that (a) didn't
> take like 100 man-hours of setup time, (b) didn't suck down a ton of
> engineering resources, and (c) actually found any particularly
> relevant bugs.

Then perhaps you haven’t seen unit testing done right. My setup time for
unit testing is about as long as it takes to set up a class library and
run NUnit or MBUnit. Marginal.

Most unit tests are written as code is developed, not tacked on after
the fact. The design aspect of writing unit tests cannot be overstated.
Especially in teams where one person writes a piece of code that another
person is going to call. It's very easy to create a really awful API to
a class library that then costs other developers who have to use the API
extra time to fiddle around with it and understand it. With unit tests,
at least the author has had to "dogfood" his own medicine and the API is
more likely to be usable. If it’s still confusing, well the unit test
can serve as a code sample. I tend to learn better from code samples
than Intellisense.

> ​1) When you modify your program, test it yourself. Your goal should
> be to break it, NOT to verify your code.

Yes, unit testing does not take away the need to test your own code.
However, pages and pages of studies show how easy it is for even very
talented developers to develop blind spots when testing their own code.
That’s why you still have a QA team dedicated to the task without the
baggage that the developer carries.

However, testing the feature you changed isn’t necessarily good enough.
For example, suppose you make a slight schema change. Are you sure you
haven’t broken a feature developed by another developer? Are you
prepared to test the entire system for every change you make? With unit
tests you have some degree of regression testing at your fingertips.

While it is true that unit testing can some additional upfront time, in
my experience, especially if you work in a team, it always produces a
cost savings overall. The time and cost savings of unit tests cannot be
overstated. Yes. Savings!

One TDD practice I am a firm proponent of is to make sure that when a
bug is discovered in the code, before you fix the bug, you write a unit
test that exposes the bug (if possible and cost effective). By
“exposing” the bug I mean you write a test that would pass if the code
was working properly, but fails because of the bug. Afterwards you fix
the bug, make sure the test passes and then check in the code and the
unit test. Now you have a fair degree of certainty that particular bug
won’t crop up again. By the very existence of the bug in the first
place, you know that area of code is troublesome and deserves to have
unit tests testing it.

These sort of unit tests address issues that unit tests are too soft on
the code as they are effectively generated as a result of human
interaction with the system.

On a recent project, a developer checked in a schema change and tested
the system and it seemed to work just fine. Meanwhile, I had gotten
latest and noticed several of my unit tests were suddenly failing. After
a few minutes of digging, I called the other developer and confirmed the
schema change. It required a small change in my code and everything was
running smoothly. Without my suite of unit tests, I would have no easy
way to judge the true impact of that schema change. In fact, it may have
taken hours for me to even notice the problem, as things “seemed” to be
working fine from a UI perspective. It was the underlying calculations
that were broken.

> Real testers hate your code. A unit test simply verifies that
> something works. This makes it far, far too easy on the code. Real
> testers hate your code and will do whatever it takes to break it--
> feed it garbage, send absurdly large inputs, enter unicode values,
> double-click every button in your app, etcetera.

Yes they do! And when they throw in garbage that breaks the code, I make
sure to codify that as a unit test so it doesn’t break again. In fact,
over time as I gain experience with unit testing, I realize I can just
as easily throw garbage at my code as a human tester can. So I write my
unit tests to be harsh. To be mean angry bad mofos. I make sure the
tests probe the limits of my code. It takes me a bit of upfront time,
but you know what? My automated unit test can throw garbage at my code
faster than a human can. Plus, don’ forget, it is a human who is writing
the test.

> Testing is hugely important. Much too important to trust to machines.
> Test your program with actual users who have actual data, and you'll
> get actual results.

Certainly true, but regression testing is much too boring to be left to
humans. Humans make mistakes, especially when performing boring
repetitive tasks. You would never tell a human manually sum up a long
row of numbers, that’s what computers are for. You let the machine do
the tasks its well suited for, and the humans can do what they are well
suited for. It all works hand in hand. Unit testing is no substitute for
Beta testing. But Beta testing is certainly no substitute for unit
testing.

