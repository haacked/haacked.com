---
title: Demeter Transmogrifiers To The Rescue
tags: [code,patterns]
redirect_from: "/archive/2009/08/15/demeter-transmogrifiers.aspx/"
---

In a recent post, [The Law of Demeter Is Not A Dot Counting
Exercise](https://haacked.com/archive/2009/07/14/law-of-demeter-dot-counting.aspx "Law of Demeter Dot Counting"),
I wanted to peer into the dark depths of the Law of Demeter to
understand it’s real purpose. In the end I concluded that the real goal
of the guideline is to reduce coupling, not dots, which was a relief
because I’m a big fan of dots (and stripes too judging by my shirt
collection).

However, one thing that puzzled me was that there are in essence two
distinct formulations of the law, the object form and the class form.
Why are there two forms and how do they differ in a practical sense?

Let’s find an example of where the law seems to break down and perhaps
apply these forms to solve the conundrum as a means of gaining better
understanding of the law.

[Rémon Sinnema has a great
example](http://sinnema313.wordpress.com/2008/06/16/the-law-of-demeter/ "The Law of Demeter")
of where the law seems to break down that can serve as a starting point
for this discussion.

> Code that violates the Law of Demeter is a candidate for *Hide
> Delegate*, e.g. `manager = john.getDepartment().getManager()` can be
> refactored to `manager = john.getManager()`, where the `Employee`
> class gets a new `getManager()` method.
>
> However, not all such refactorings make as much sense. Consider, for
> example, someone who’s trying to kiss up to his boss:
> `sendFlowers(john.getManager().getSpouse())`. Applying *Hide Delegate*
> here would yield a `getManagersSpouse()` method in `Employee`. Yuck.

This is an example of one common drawback of following LoD to the
letter. You can end up up with a lot of one-off wrapper methods to
propagate a property or method to the caller. In fact, this is so common
there’s a term for such a wrapper. It’s called a [Demeter
Transmogrifier](http://en.wikipedia.org/wiki/Transmogrifier_(computer_science) "Transmogrifier on Wikipedia")!

![transmogrifier\_small](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DemeterTransmogrifiersToTheRescue_9721/transmogrifier_small_3.jpg "transmogrifier_small")

Who knew that Calvin was such a rock star software developer?

Too many of these one-off “transmogrifier” methods can clutter your API
like a tornado in a paper factory, but like most things in software,
it’s a trade-off that has to be weighed against the benefits of applying
LoD in any given situation. These sort of judgment calls are part of the
craft of software development and there’s just no “one size fits all
follow the checklist” solution.

While this criticism of LoD may be valid at times, it may not be so *in
this particular case.* Is this another case of dot counting?

For example, suppose the `getManager` method returns an instance of
`Manager` and `Manager` implements the `IEmployee `interface. Also
suppose that the `IEmployee` interface includes the `getSpouse()`
method. Since John is also an `IEmployee`, shouldn’t he be free to call
the `getSpouse()` method of his manager without violating LoD? After
all, they are both instances of `IEmployee`.

Let’s take another look at the the [general formulation of the
law](http://www.ccs.neu.edu/research/demeter/demeter-method/LawOfDemeter/general-formulation.html "General formulation of the law"):

> Each unit should have only limited knowledge about other units: only
> units "closely" related to the current unit. Or: Each unit should only
> talk to its friends; Don’t talk to strangers.

Notice that the word *closely* is in quotes. What exactly does it mean
that one unit is *closely* related to another? In the short form of the
law, *Don’t talk to strangers,*we learn we shouldn’t talk to strangers.
But who exactly is a stranger? Great questions, if I do say so myself!

The formal version of the law focuses on sending messages to *objects*.
For example, a method of an object can always call methods of itself,
methods of an object it created, or methods of passed in arguments. But
what about types? Can an object always call methods of an object that is
the same *type* as the calling object? In other words, if I am a
`Person` object, is another `Person` object a stranger to me?

According to the general formulation, there is a *class form* of LoD
which applies to statically typed languages and seems to indicate that
yes, this is the case. It seems it’s fair to say that for a statically
typed language, an object has knowledge of the inner workings of another
object of the same type.

Please note that I am qualifying that statement with “seems” and “fair
to say” because I’m not an expert here. This is what I’ve pieced
together in my own reading and am open to someone with more expertise
here clearing up my understanding or lack thereof.

