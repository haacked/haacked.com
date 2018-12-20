---
title: Design Patterns Isn&#8217;t a Golden Hammer
date: 2005-05-30 -0800
tags: []
redirect_from: "/archive/2005/05/29/design-patterns-isn-8217t-a-golden-hammer.aspx/"
---

One trap that developers need to be wary of is the mentality of the
[Hammer Truism](http://c2.com/cgi/wiki?HammerTruism). This states that

> When the only tool you have is a hammer, everything looks like a nail.

This is especially true of Design Patterns. I particularly liked what
Erich Gamma said in [this
interview](http://www.artima.com/lejava/articles/gammadp.html)

> Do not start immediately throwing patterns into a design, but use them
> as you go and understand more of the problem. Because of this I really
> like to use patterns after the fact, refactoring to patterns.

All too often, I’ve encountered code that uses a pattern because the
developer felt he **should** use a pattern there, not because he
**needed** the pattern.

Not every developer understands that patterns add complexity to a
solution. Certainly abstraction and redirection are important benefits
of many design patterns, but they come at a cost. To use design patterns
effectively is to know when the benefits will payback that cost with
interest.

The important concept to understand is that Design Patterns are
**descriptive** not **prescriptive**. They aren’t intended to instruct
how one **should** design a system, but merely **describe** successful
designs that have worked in the past for common problems. Should the
problem you’re tackling fit a particular recurring pattern, then
applying a design pattern is certainly a good choice. But when the
problem doesn’t quite fit one of the patterns, trying to cram the round
pattern into the square design just doesn’t fit.

I’ve recently seen an example of this in regards to using an interface.
I generally follow the rule of threes regarding polymorphism. For
example, if I have a class with an enum indicating its "type" (for
example, a User class with an enum property indicating whether the User
is an employee or a manager), when that enum contains three values, I’ll
consider refactoring the class to have a base class and inherited
classes (for example, the User class might have an Employee subclass and
Manager Subclass). Maybe I'll use an IUser interface instead.

However, I caution against using an interface (or inheritance) just
because it’s the "right" thing to do. There’s no point to implementing
polymorphism if it is never used.

For example, I recently saw several classes in some code I was reading
that implemented an interface we’ll call ISomeInterface. But nowhere did
I find any code that referenced ISomeInterface. Instead, there were only
references to concrete classes. I expected to see something like this
somewhere in the code.

foreach(ISomeInterface something in SomeInterfaceCollection)

{

    something.DoSomething();

}

But no such code could be found. This was a prime example of a
gratuitous use of an interface. This interface served no purpose and
needed to be removed.

The important lesson here is to always start off by writing the simplest
code possible and only add interfaces and design patterns when they are
absolutely needed.

[Listening to: Voices (DJ Remy Remix) - Bedrock - Gatecrasher Global
Sound System: Latitude (Disc 2) (5:13)]

