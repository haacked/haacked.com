---
title: Going Into Design Debt
date: 2005-09-24 -0800 9:00 AM
tags: [design,software,patterns,refactoring]
redirect_from: "/archive/2005/09/23/GoingIntoDesignDebt.aspx/"
---

Found [this
post](http://epudd.blogspot.com/2005/09/mentally-sweating.html) in the
trackbacks section of my post on [Mental
Laziness](https://haacked.com/archive/2005/09/18/10204.aspx). It’s a
classic example of where management pressure often leads to mental
laziness.

In this scenario (go [read
it](http://epudd.blogspot.com/2005/09/mentally-sweating.html), it’s
short), the `ViewState` of a system they were working on was extremely
large and causing problems for the client. The quick solution was to
perhaps use some sort of [Http
Compression](http://www.15seconds.com/issue/020314.htm) or [`ViewState`
compression](http://www.mostlylucid.co.uk/archive/2004/01/03/694.aspx).

But the author of the post, David, suggests to his coworker that they
should dig into *why* the `ViewState` is so large as the ideal course of
action.

In this situation, with management breathing down your neck, it is
prudent to spend some time digging into the root cause of an issue.
Often, you’ll end up finding an obvious mistake and dramatically improve
the application. But at the same time, you want to be careful not to
spend too much time banging your head against a problem when you have a
workable (albeit band-aid) solution in your hand.

In their situation, it makes sense to set a time limit to investigate
the root cause. Perhaps the root cause is that the pages are just plain
big due to requirements, and there is no “mistake” to correct. In that
situation, the right solution IS ViewState compression. The extra
investigation time bought you that assurance.

Suppose they couldn’t find the solution in that time span. At this
point, there is nothing wrong with just putting the compression solution
in place without having determined the root cause. However, doing so
will incur a Design Debt, and that has to be considered when making the
decision.

In the book *[Refactoring to
Patterns](http://www.amazon.com/exec/obidos/tg/detail/-/0321213351/103-9411210-6787060?v=glance)*,
Joshua Kerievsky talks about the concept of **Design Debt**. Design Debt
is the state your code is put in when you write crank out code without
regard to its design just to meet a deadline. There are situations when
this is necessary. Sometimes you just have to bite the bullet and weigh
business needs against design purity and just spew code.

As an illustration of design debt, consider building a house of cards
three stories tall. You set up the foundation for three stories, you
start to build it, but halfway through, your boss comes in and tells you
to build it seven stories tall, and get it done ASAP.

Sure, you can do it without redoing the foundation, but to add even more
stories later, you’re going to have to revisit the foundation or the
whole thing will come tumbling down. Or at least, it’ll be very slow to
build that next floor.

Software is much like this. If you are rushing code to meet a deadline,
you will go into design debt and that debt has to be paid off, otherwise
future development will often slow dramatically and be much more error
prone. The obvious problem is how do you sell this argument to your
boss? From the business people’s perspective, you’ve delivered working
software with X number of features in a short period of time. Why can’t
you deliver the next X number of features in the same period of time?

Ah. Therein lies the rub! The business people don’t understand software,
and by meeting their insane deadline, you’ve created the impression
that...well...that is a normal project pace.

This is where Kerievsky believes the Design Debt metaphor may be of
assistance. By phrasing it into financial terms the business folk can
understand, the hope is they’ll trust you and provide time to pay off
the debt. It’s a simple formula, **If you go into debt. The debt must be
paid.**

> Sure, I can meet this insane deadline. But we will go into debt, and
> it will have to be paid before we add any more features.

Try to make that an expectation up front if you can. It won’t always
work, but if it never works, consider moving to a less dysfunctional
work environment.

[Listening to: Suite No. 6 In D Major, Gavotte I - II - Yo-Yo Ma - The
Cello Suites - Inspired By Bach CD2 (3:42)]

