---
layout: post
title: "Composition over Inheritance and other Pithy Catch Phrases"
date: 2007-12-11 -0800
comments: true
disqus_identifier: 18437
categories: []
---
Love them or hate them, the ALT.NET mailing list is a source of
interesting debate, commentary and insight. I can’t help myself but to
participate. Debate is good. Stifling debate is bad. Period. End of
debate. (*see!? That was bad!)*

The community itself is a young community, and as such, they are going
through a period of identity forming. What are their shared values? What
does it mean to be an ALT.NET-er? It's not exactly clear yet, but it is
starting to form.

One thing I would caution this community is to be careful in how they
define their shared principles. For example, in one thread one
individual mentioned debating me and then in the same message proposed
the idea of *Composition over Inheritance* as a shared principle.

In response, someone posted this:

> You can throw the book at those people--literally. Favoring
> composition over inheritance is straight out of the Gang of Four book.
> Don't like design patterns? Fine. No problem. I have a couple of \
>  Don Box COM+ books that say the exact same thing.

Here was my response, which I also wanted to put in a blog post since it
represents pretty well what I think.

> I think ALT.NET should focus more on the principles of *thinking for
> yourself*and a *desire to improve*.
>
> \> Favoring composition over inheritance is straight out of the Gang
> of Four book.
>
> So is the Singleton pattern. So is the Template Method pattern.
>
> Sorry, [Appeal to
> Authority](http://en.wikipedia.org/wiki/Appeal_to_authority "Logical Fallacy")
> doesn't work for me. Look, I’m not against composition over
> inheritance in many cases. Perhaps most cases. What I am against is
> saying that it applies in *all* cases and that if you don’t do it,
> you’re not ALT.NET.
>
> I’m against the blind application of these pithy catch phrases.
> **Blindly applying a “best practice” is just as irresponsible as never
> applying a “best practice”**. [There is no perfect
> design](http://haacked.com/archive/2005/05/31/ThereIsNoPerfectDesign.aspx "No Perfect Design").
> There is no one true way. There is no one size fits all.
>
> Why favor composition over inheritance? What trade-offs are you making
> when you do so? Developers should think through these things when they
> make these choices. And when a developer does think through the issue,
> but makes a choice that differs from what you think, you should
> applaud that. At least the developer thought through the decision.
>
> I don’t care that a developer doesn’t favor composition over
> inheritance in a specific case. I only care that the developer thought
> it through, had a reason for the decision, wants to improve.
>
> **The goal is not to bend developers to the will of some specific
> patterns, but to get them to *think about their work and what they are
> doing***. For example, one advantage with inheritance is that it is
> easier to use than composition. However, that ease of use comes at the
> cost that it is harder to *reuse*because the subclass is tied to the
> parent class.
>
> One advantage of composition is that it is more flexible because
> behavior can be swapped at runtime. One disadvantage of composition is
> that the behavior of the system may be harder to understand just by
> looking at the source. These are all factors one should think about
> when applying composition over inheritance.
>
> So while I agree that you should *favor* composition over inheritance,
> inheritance is still necessary. After all, “the set of components is
> never quite rich enough in practice.”
>
> That quote is from Erich Gamma, Richard Helm, Ralph Johnson, and John
> Vlissides in *Design Patterns*. But don’t believe it just because they
> said it. After all, I would hate to be guilty of an Appeal to
> Authority. ;)

Tags: [ALT.NET](http://technorati.com/tags/ALT.NET/ "ALT.NET tag") ,
[Design
Patterns](http://technorati.com/tags/Design%20Patterns/ "Design Patterns tag")

