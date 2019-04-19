---
title: Erich Gamma Talks About Flexibility and Reuse
date: 2005-06-02 -0800 9:00 AM
tags: [software,design,patterns]
redirect_from: "/archive/2005/06/01/erich-gamma-talks-about-flexibility-and-reuse.aspx/"
---

I just finished [reading part
2](http://www.artima.com/lejava/articles/reuse.html) of the Bill Venners
interview with Erich Gamma and Erich so eloquently distills some of what
I was trying to say [in a recent
post](https://haacked.com/archive/2005/05/31/3935.aspx).

It’s interesting to note how thinking about building systems has changed
in the ten years since Design Patterns was published. Bill Venners
quotes the GOF book as saying

> The key to maximizing reuse lies in anticipating new requirements and
> changes to existing requirements, and in designing your systems so
> they can evolve accordingly. To design a system so that it?s robust to
> such changes, you must consider how the system might need to change
> over its lifetime. A design that doesn?t take change into account
> risks major design in the future.

This is certainly something I was taught when I first started off as a
developer, but I think now, it?s becoming more and more clear that
speculation carries a lot of risk and can be more harmful than helpful.
I learned that the hard way, as clients are a fickle lot, and you can
guess what they?ll ask for next as easily as you can guess the next
super lotto numbers.

Erich?s approach to building an extensibility model with Eclipse
reflects how I try to approach projects I work on. In essence,
experience a little pain (be it duplication, etc...) before refactoring
with a pattern.

I eagerly anticipate part 3 of the interview. Be sure to also [read Part
1](http://www.artima.com/lejava/articles/gammadp.html) of the interview.

