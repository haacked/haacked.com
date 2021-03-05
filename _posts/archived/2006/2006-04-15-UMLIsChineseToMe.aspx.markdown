---
title: UML Is Chinese To Me
tags: [software,patterns]
redirect_from: "/archive/2006/04/14/UMLIsChineseToMe.aspx/"
---

![Chinese Character for
Money](https://haacked.com/assets/images/MoneyInChinese.jpg)
[Jeff](http://www.codinghorror.com/blog/ "CodingHorror blog") has a
great post in which he compares UML to circuit diagrams and then asks,
**why doesn’t UML enjoy the same currency for software development?**

In the comments [Scott
Hanselman](http://www.hanselman.com/blog/ "ComputerZen blog") makes a
great point...

> It’s because, IMHO, UML isn’t freaking obvious. It’s obtuse. What’s
> the open arrow, open circle mean again?

I think he is spot on. But you could also say that about any programming
language, right? What is that colon between the two words mean?

```csharp
public class Something : IObscurity<-- What the heck is that?
{
}
```

If you are a VB programmer, it might be unfamiliar. But if you are a C#
programmer my question is like asking what is that funny curly line and
dot at the end of this sentence? Oh that’s an interface implementation
silly. Of course!

Don’t get me started on C++ with its double colon craziness and its
`@variable` and `variable*` which leave the befuddled developer asking
*what exactly do they mean*?

### Isn’t UML a Decent Abstraction Layer?

The evolution of software has been a steady stream towards higher level
abstractions. We no longer punch holes in cards to represent computer
calculations in binary (at least I hope not). As a managed code
developer, I don’t even have to worry about allocating memory (malloc
anybody?) before I use code...Glory be! So doesn’t it seem natural that
UML would be the next evolutionary step in that chain?

Umm...Well no.

**The most successful widespread abstractions are those that abstract
the underlying computing architecture**, which itself is abstract.
Memory, for example, is pretty the same thing to everybody, no matter
what kind of software you are working on. If the machine can handle
allocating and deallocating it for you so you don’t have to think about
it all the time, then all the better for everybody.

But that same principle doesn’t work as well when we start raising the
abstraction level to cover our real world concepts. The next obvious
level of abstraction are domain classes. How many times have you written
an `Order` class? I’ve written one. Great! Since I did the work, I can
simply post that baby on
[SourceForge](http://sourceforge.net/ "Open Source Project Site") and
save the rest of you suckers a bunch of time. Now anybody can simply
just drag the UML representation into their UML diagrams and **bam!**,
their Web 2.0 revolutionary microformatted shopping cart application is
complete. Sit back and watch the flood of money flow in.

If only it were that easy.

### Make a Wish

![Evil Genie](https://haacked.com/assets/images/genie.jpg) It would be nice to
be able to work with such high level abstractions and wire them up. *Oh,
here, I’ll just draw a line from this order to the shopping cart and
**boom!** when the user clicks this button, the item goes into the
cart.* But what about the various business rules triggered around adding
this order to the cart? What about the fact that the cart lives in
another process on a separate server and the order needs to serialized?
What about the persistence mechanism? How do you express that in UML?

You can’t. **Writing code is like asking an evil genie for a wish. No
matter how carefully you craft the wish, there is always some pernicious
detail left out just waiting to jab you in the eye**. *I wish I were
rich* and now I am some poor slob named *Rich* living in abject poverty.
There are just too many moving parts and pitfalls in a piece of software
to deal with and worry about.

UML has a bit of trouble capturing the semantics of code. Like
snowflakes, no two `Order` classes are alike. Every client has their
peculiar and idiosyncratic ideas on what an order is and how it should
work in their environment. So what do we do? We start encumbering UML
with all sorts of new symbols and glyphs so that we can work toward a
semantically expressive UML ([executable
UML](http://www.awprofessional.com/articles/article.asp?p=28274&rl=1 "Executable UML")
anyone?)

But this just turns UML into another programming language. The fact that
it is in a diagram form doesn’t make it any more expressive than code.
**In a way, adopting UML is like changing from English to Chinese. Sure
a single Chinese character can represent a whole word or even multiple
words, but that doesn’t make it any easier to grasp.** Now, you have to
learn thousands of characters.

Not to mention the fact that you are writing the same code twice. Once
by dragging a bunch of diagrams around with a mouse (how slow is that?)
and again by writing out the actual compilable code. Granted, that
particular issue my be solved by executable UML in which the model is
the code. But that suffers from its own range of problems, not the least
of which is the huge number of symbols required to make it work.

### What is UML Good For?

Now to be fair, my criticism is about formal UML and UML modelling tools
such as Rational Rose. If you are prepared to run wild and loose with
your UML, it can be useful at a very high level as a planning tool. I
sometimes sketch out interaction diagrams to help me think through the
interactions of my class objects. That is useful. But I rarely keep
these diagrams around because hell will freeze over before I waste a
bunch of my time trying to keep all of them up to date with the actual
code. **[The code really is the
design](http://www.developerdotstar.com/mag/articles/reeves_design_main.html "Code as design: Three Essays by Jack W. Reeves")**.
The only diagram potentially worth keeping around is the very high level
system architecture diagram outlining the various subsystems.

