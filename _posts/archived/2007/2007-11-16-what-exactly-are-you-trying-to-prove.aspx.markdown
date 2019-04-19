---
title: What Exactly Are You Trying To Prove?
date: 2007-11-16 -0800
tags: [tdd,methodologies]
redirect_from: "/archive/2007/11/15/what-exactly-are-you-trying-to-prove.aspx/"
---

Frans Bouma wrote an *interesting* response to my last post, [Writing
Testable Code Is About Managing
Complexity](https://haacked.com/archive/2007/11/14/writing-testable-code-is-about-managing-complexity.aspx "My Last Post")
entitled [Correctness Provability should be the goal, not
Testability](http://weblogs.asp.net/fbouma/archive/2007/11/14/correctness-provability-should-be-the-goal-not-testability.aspx "Frans talks about code correctness provability").

He states in his post:

> When focusing on testability, one can fall into the trap of believing
> that the tests prove that your code is correct.

God I hope not. Perhaps someone in theory *could* fall into that trap,
but a person could also fall into the trap and buy a modestly priced
bridge I have to sell to them in the bay area? This seems like a [straw
man
fallacy](http://en.wikipedia.org/wiki/Straw_man "Straw Man on Wikipedia")
to me.

Certainly no major TDD proponent has ever stated that testing provides
*proof* that your code is correct. That would be outlandish.

Instead, what you often hear testing proponents talk about is
*confidence*. For example, in my post [Unit Tests cost More To
Write](https://haacked.com/archive/2005/12/06/unit-tests-cost-more-to-write.aspx "Unit tests do cost more to write")
I make the following point (emphasis added):

> They reduce the true cost of software development by [promoting
> cleaner code with less
> bugs](https://haacked.com/archive/2004/12/06/unit-testing-benefits.aspx "Benefits of TDD").
> They reduce the TCO by documenting how code works and by serving as
> regression tests, giving maintainers **more confidence** to make
> changes to the system.

Frans goes on to say (emphasis mine)...

> Proving code to be correct isn’t easy, **but it should be your main
> focus** when writing solid software. Your first step should be to
> prove that your algorithms are correct. If an algorithm fails to be
> correct, you can save yourself the trouble typing the executable form
> of it (the code representing the algorithm) as it will never result in
> solid correct software.

Before I address this, let me tell you a short story from my past. I
promise it’ll be brief.

When I was a young bright eyed bushy tailed math major in college, I
took a fantastic class called [Differential
Equations](http://en.wikipedia.org/wiki/Differential_equation "Differential Equations on Wikipedia")
that covered equations which describe continuous phenomena in one or
more dimension.

During the section on partial differential equations, we wracked our
brains going through crazy mental gymnastics in order to find an
explicit formula that solved a set of equations with multiple
independent variables. With these techniques, it seemed like we could
solve anything. Until of course, near the end of the semester when the
cruel joke was finally revealed.

The sets of equations we solved were heavily contrived examples. As
difficult as they were to solve, it turns out that only the most trivial
sets of differential equations can be solved by an explicit formula. All
that mental gymnastics we were doing up until that point was essentially
just mental masturbation. Real world phenomena is hardly ever described
by sets of equations that lined up so nicely.

Instead, mathematicians use techniques like [Numerical
Analysis](http://en.wikipedia.org/wiki/Numerical_methods "Numerical Analysis")
([the Monte Carlo
Method](http://en.wikipedia.org/wiki/Monte_Carlo_method "Monte Carlo Method on Wikipedia")
is one classic example) to attempt to find approximate solutions with
reasonable error bounds.

Disillusioned, I never ended up taking Numerical Analysis (the next
class in the series), choosing to try my hand at studying stochastic
processes as well as number theory at that point.

The point of this story is that trying to prove the correctness of
computer programs is a lot like trying to solve a set of partial
differential equations. It works great on small trivial programs, but is
incredibly hard and costly on anything resembling a real world software
system.

**Not only that, what exactly are you trying to prove?**

In mathematics, a mathematician will take a set of axioms, a postulate,
and then spend years converting caffeine into a long beard (whether you
are male or female) and little scribbles on paper (which mathematicians
call *equations*) that hopefully result in a proof that the postulate is
true. At that point, the postulate becomes a theorem.

The key here is that the postulate is an unambiguous specification of a
truth you wish to prove. To prove the correctness of code, you need to
know exactly what correct behavior is for the code, i.e. a complete and
unambiguous specification for what the code should do. So tell me dear
reader, when was the last time *you* received an unambiguous fully
detailed specification of an application?

If I ever received such a thing, I would simply execute that sucker,
because the only unambiguous complete spec for what an application does
is code. Even then, you have to ask, how do you prove that the
specification does what the customers want?

**This is why proving code should *not* be your *main* focus, unless,
maybe, you write code for the Space Shuttle.**

Like differential equations, it’s too costly to explicitly prove code in
all but the most trivial cases. If you are an algorithms developer
writing the next sort algorithm, perhaps it is worth your time to prove
your code because that cost is amortized over the life of such a small
reusable unit of code. You have to look at your situation and see if the
cost is worth it.

For large real world data driven applications, proving code correctness
is just not reasonable because it calls for an extremely costly
specification process, whereas tests are very easy to specify and cheap
to write and maintain.

This is somewhat more obvious with an example. Suppose I asked you to
write a program that could break a CAPTCHA. Writing the program is very
time consuming and difficult. But first, before you write the program,
what if I asked you to write some tests for the program you will write.
That's trivially easy, isn't it? You just feed in some CAPTCHA images
and then check that the program spits out the correct value. How do you
know your tests are correct? You apply the
[red-green-refactor](http://codebetter.com/blogs/scott.bellware/archive/2005/11/22/134954.aspx "Red-Green-Refactor")
cycle along with the [principle of
triangulation](https://haacked.com/archive/2007/03/12/who-tests-the-tests.aspx "Who Tests the tests").
;)

As we see, testing is easy. So how do you *prove* its correctness? Is it
as easy as testing it?

As I said before, testing doesn’t give you a proof of correctness, but
like the approaches of numerical analysis, it can give you an
*approximate* proof with reasonable error bounds, aka, a confidence
factor. The more tests, the smaller the error bounds and the better your
confidence. This is a way better use of your time than trying to prove
everything you write.

