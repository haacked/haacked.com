---
title: Does Mort Know We're Talking Smack About Him Behind His Back?
date: 2005-08-02 -0800
tags: [culture,developers,commentary]
redirect_from: "/archive/2005/08/01/DoesMortKnowWeAreTalkingSmackAboutHimBehindHisBack.aspx/"
---

And if so, is he angry? I wondered this after reading *[Is Object Oriented Programming The Problem?](http://donxml.com/allthingstechie/archive/2005/08/01/2116.aspx)* in which Don invokes the programmer archetype, "Mort" to make a point.

### Who is Mort?

For the uninitiated, Mort is a creation of arrogant software developers
(I don’t exclude myself from this group) used to lump together and
define the quintessential "average" developer. Actually, "creation"
isn’t the correct word. In the same way that Design Patterns are
discovered, not created, Mort is a discovery. He’s a Developer Pattern
(If nobody has that term trademarked, I hereby claim it for myself).

Mort isn’t the only Developer Pattern I’ve seen in the wild, but he is
the only one famous enough to go by a single name (think Pele, Madonna,
etc..) that I know of. Other Developer Patterns that come to mind...

The Bleeding Edge Liberal
:   This one thinks Release Candidate software is so five minutes ago.
    Alpha and Beta software only, baby.
The Conservative Curmudgeon
:   This one still promotes punch cards as the solution to all software
    woes and believes things were better when a bug was best taken care
    of with a can of raid and a fly swatter.
The Trainer
:   The last time this one wrote a try/catch block was during the lesson
    on exception handling.
The True Believer
:   This one thinks that for any problem, there is one and only one
    correct solution. This person also believes there is only one true
    programming language and that language is...

I digress. As I stated before, Mort is the prototype of the average
developer. He works 9 to 5 in some cubicle farm of a large bureaucratic
corporation (perhaps he’s a government employee), and he rarely if ever
attends a developer conference or discusses software development outside
of the workplace. He almost certainly is not a technical blogger.

### We worry for Mort.

Yet, so many advanced developers, trainers, language designers, get
their panties in a tussle over how Mort is doing and what he is capable
of. Is Mort switching from VB to VB.NET? Why not? Is Object Oriented
Programming too difficult for Mort to grasp? How can we make it easier
on poor old Mort?

What I’d like to know is just how many Morts are there out there? For
example, if you ask the average developer if he or she is Mort, will you
get an honest answer? I may have been in the mold of Mort when I first
started writing software, but I most certainly am not Mort now, right?
Right?!

Does Mort even know he’s Mort? Or is the "other guy" always Mort.
There’s not much you can do for Mort unless Mort takes the first step
and admits that he is indeed Mort. If Mort is defined as the "average"
developer, we can’t all be "Not Mort"

Personally, I’ve never given Mort much thought. If Mort indeed does
exist, I personally don’t have much interesting working with Mort. I’d
much prefer working with a Jane or Juan. As a manager, my whole job was
to hire someone better than Mort, though I can’t claim I was always
successful.

From my experiences with Mort-like developers, Mort is capable of
writing crappy code in any language, OO or not. Alot of times, this was
due to what I call intellectual laziness. "Hmmm, this seems to solve the
problem, I’ll move on." Mort spends very little time reading up on the
tools and platforms he uses to get his job done. Call it, programming
via Intellisense. We all do it at times, and I wouldn’t want to program
without it, but when Intellisense is the only means of learning an API,
that’s a problem.

Mort cares little for learning about tools outside of the IDE that can
help him be more productive and less error prone. For example, Mort
won’t write automated unit tests, instead settling for a quick spot
test, leaving the broken code for someone else to discover and fix.

Sorry Mort, but you know it’s true.

### What is the Problem?

In any case, in answer to Don’s question, *[Is Object Oriented
Programming the
Problem?](http://donxml.com/allthingstechie/archive/2005/08/01/2116.aspx)*,
my response is no, it is not. The *problem* is that developing Software
is an extremely complex and difficult task. Engineering a sky-scraper
sounds like a complicated task with many moving parts, but consider that
the number of moving parts in a typical software project makes building
a skyscraper look like building a small tower using Duplo blocks.

The problem is complexity and how to manage it. I am afraid there are no
easy solutions. Consider that 80% of the time spent in all software
projects is during the maintenance phase. This phase is typically where
the use of procedural languages causes pain if not done well. If a
developer cannot understand OO principles, then that developer will also
very likely have trouble with writing modular code in a procedural
language. Using a procedural language requires even more diligence due
to its lack of language constructs to enforce encapsulation, etc...

I don’t know about you, but the thought of working on a legacy classic
ASP project makes me cringe (I usually turn down such projects). I’ve
seen reams and reams of spaghetti code using a procedural language (some
of it written by me as a brash youth) as to make me dry heave. This is
not to say a large system can’t be built well using procedural
languages, just that it takes much discipline.

Of course, OO is not a panacea. Equally bad code can be written using an
OO language, especially when OO principles are not well understood and
you end up with procedural code anyways, or really bad OO-like code. But
there’s hope! With well factored OO code, with a light sprinkle of
Design Patterns where appropriate, sometimes the internal workings of a
class or system are well enough encapsulated that you have a chance of
maintaining a large system. There aren’t as many variables to juggle in
your head at any one time.

### Are DSLs the Solution?

So where do Domain Specific Languages (DSL) fit in? The history of
programming languages is the story of using abstractions to hide
complexity. Assembly is a abstraction layer on top of the 0s and 1s that
make up binary. C is an abstraction on top of Assembly. SmallTalk, C#,
and Java uses objects to abstract details of lower level languages...
and so on.

Are DSLs the next level of abstraction? It would make sense, but they
may be hard to deliver upon. It was thought that Component Technology
would deliver the building blocks to make Mort’s life easier. Simply put
the blocks together like a big Lego structure and voila! You have a
working payroll system! In this scenario, your DSL is simply composed of
Domain Specific components (building blocks) and your general purpose
language is used to glue these blocks together.

But alas, it is never so simple as that. The problem is that although we
can divide the world into distinct "Domains", the variations within a
Domain might as much as between Domains. For example, the differences
between Chinese dialects are more than the differences between any two
Romance languages in Europe.

The result of such variation is that you will rarely find just the right
DSL for your situation or the DSL will itself be not much different than
a general purpose language. One solution to this problem is to build a
custom DSL for your business. But I wouldn’t task Mort with that
project. Make sure your Joel’s are working on that one. Perhaps with a
custom DSL, Mort has a chance to make a very meaningful contribution,
without causing too much damage. If he’d only show more of an interest
in building better software.

### So What is the Solution?

This may come as a surprise coming from a technophile like myself, but I
don’t think that we’ll find a technical solution for Mort. Even if DSLs
prove themselves to be the next great abstraction level for software
development, we’ll just take the extra productivity gains produced to do
even more with software. We’ll build larger and more complex systems,
and as we’ve learned, complexity is the problem in the first place.

Rather, I think the solution is to quit pandering to Mort with our
condescending paternalistic attitude, and instead demand better from
Mort. If the capabilities of the average developer truly is as bleak as
many make it out to be, we shouldn’t just accept it, but work to raise
the quality of the average developer. "Average developer" should
describe an acceptable level of competence.

We have to realize that Mort is responsible for a lot of important
systems. Systems that affect the general population. When I hear of
recent cases of identity thefts such as Choicepoint among others,
especially those caused by lax security such as using default passwords
for the database, I think of Mort. When I read that \$250 million worth
of taxpayer money has gone into an overhaul of the FBI Case File system,
and the system has to be scrapped. I think of Mort.

Given this much responsibility, we should expect more from Mort. So
Mort, I hate to say this but software development is not like working
the register at McDonalds where putting in your nine to five is enough.
I am all for work-life balance, but you have to understand that Software
development is an incredibly challenging field, requiring intense
concentration and strong mental faculty. It’s time for you to attend a
conference or two to improve your skills. It’s time for you to subscribe
to a few blogs and read a few more books. But read deeper books than
*How to program the VCR in 21 days*. For example, read a book on Design
Patterns or Refactoring. Mort, I am afraid it’s time for you to quit
coasting. It’s time for you to step it up a notch.

### Mort, Can We All Get Along?

So Mort, if you really do exist, and you recognize who you are, I
apologize if I came off as a bit harsh or critical in this post. I
wonder if we’re being a bit too arrogant for pigeonholing you. Is the
Mort archetype really useful for this discussion?

Perhaps my experiences have been with a sub-Mort, and not with you. I
really would like to think the average developer [deserves more
credit](https://haacked.com/archive/2005/07/26/9027.aspx). But from some
of my experiences, there are large numbers of just plain bad developers
out there. For you, I’d like you to know we’re thinking of you, and we
know best. If you’re not willing to step up, then the best thing you can
do now is go grab me a beer while we work to solve this problem.

