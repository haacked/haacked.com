---
title: And Get Rid Of Those Pesky Programmers
tags: [ai,commentary,tech,developers]
redirect_from: "/archive/2009/06/11/getting-rid-of-programmers.aspx/"
---

Every now and then some email or website comes along promising to prove
Fred Brooks wrong about this crazy idea he wrote in [The Mythical Man
Month](http://www.amazon.com/gp/product/0201835959?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=390957&creativeASIN=0201835959 "The Mythical Man Month")
(highly recommended reading!) that [there is no silver
bullet](http://www.virtualschool.edu/mon/SoftwareEngineering/BrooksNoSilverBullet.html "There is no silver bullet")
which by itself will provide a tenfold improvement in productivity,
reliability, and simplicity within a decade.

This time around, the promise was much like others, but they felt the
need to note that their revolutionary new
application/framework/doohickey will allow business analysts to directly
build applications 10 times as fast **without the need for
programmers![![revenge-nerds](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/AndGetRidOfThosePeskyProgrammers_A0BE/revenge-nerds_thumb.jpg "revenge-nerds")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/AndGetRidOfThosePeskyProgrammers_A0BE/revenge-nerds_2.jpg)**

> Ah yeah! Get rid of those foul smelling pesky programmers! We don’t
> need em!

Now wait one dag-burn minute! Seriously?!

I’m going to try real hard for a moment to forget they said that and not
indulge my natural knee jerk reaction which is to flip the bozo bit
immediately. If I were a more reflective person, this would raised a
disturbing question:

> Why are these business types so eager to get rid of us programmers?

It’s easy to blame the suits for not understanding software development
and forcing us into a [Tom Smykowski
moment](http://www.hulu.com/watch/12879/office-space-people-skills "People Skills on Hulu")
having to defend what it is we do around here.

> Well-well look. I already told you: I deal with the god damn customers
> so the engineers don't have to. I have people skills; I am good at
> dealing with people. Can't you understand that? What the hell is wrong
> with you people?

Maybe, as Steven “Doc” List quotes from Cool Hand Luke in his latest
[End Bracket
article](http://msdn.microsoft.com/en-us/magazine/dd882508.aspx "Think Before You Speak")
on effective communication for MSDN Magazine,

> What we've got here is a failure to communicate.

[Leon Bambrick](http://www.secretgeek.net/ "Leon Bambrick") (aka
SecretGeek) recently wrote about this phenomena in his post entitled,
[The Better You Program, The Worse You
Communicate](http://www.secretgeek.net/program_communicate_4reasons.asp "Programmer communication"),
in which he outlines how techniques that make us effective software
developers do not apply to communicating with other humans.

After all, we can sometimes be hard to work with. We’re often so focused
on the technical aspects and limitations of a solution that we
unknowingly confuse the stakeholders with jargon and annoy them by
calling their requirements “ludicrous”. Sometimes, we fail to deeply
understand their business and resort to [making fun of our
stakeholders](http://www.youtube.com/watch?v=R2a8TRSgzZY "Vendor Client Relationships")
rather than truly understanding their needs. No wonder they want to do
the programming themselves!

Ok, ok. It’s not always like this. Not every programmer is like this and
it isn’t fair to lay all the blame at our feet. I’m merely trying to
empathize and understand the viewpoint that would lead to this idea that
moving programmers out of the picture would be a *good thing*.

Some blame does deserve to lie squarely at the feet of these snake oil
salespeople, because at the moment, they’re selling a lie. What they’d
like customers to believe is your average business analyst simply
describes the business in their own words to the software, and it spits
out an application.

The other day, I started an internal email thread describing in
hand-wavy terms some [feature I thought might be
interesting](https://haacked.com/archive/2009/06/02/alternative-to-expressions.aspx "Alternative To Expressions").
A couple hours later, my co-worker had an implementation ready to show
off.

Now *that* my friends, is the best type of declarative programming. I
merely declared my intentions, waited a bit, and voila!  Code! Perhaps
that’s along the lines of what these types of applications hope to
accomplish, but there’s one problem. In the scenario I described, it
required feeding requirements to a human. If I had sent that email to
some software, it would have no idea what to do with it.

At some point, something close to this might be possible, but only when
software has reached the point where it can exhibit sophisticated
artificial intelligence and really deal with fuzziness. In other words,
when the software itself becomes the programmer, only then might you
really get rid of the human programmer. But I’m sorry to say, you’re
still working with a programmer, just one who doesn’t scoff at your
requirements arrogantly (at least not in your face while it plots to
take over the world, carrot-top).

Until that day, when a business analyst wires together an applications
with Lego-like precision using such frameworks, that analyst has in
essence become a programmer. That work requires many of the same skills
that developers require. At this point, you really haven’t gotten rid of
programmers, you’ve just converted a business type into a programmer,
but one who happens to know the business very well.

In the end, no matter how “declarative” a system you build and how
foolproof it is such that a non-programmer can build applications by
dragging some doohickeys around a screen, there’s very little room for
imprecision and fuzziness, something humans handle well, but computers
do not, as Spock demonstrated so well in an episode of Star Trek.

> “Computer, compute the last digit of PI” - Spock

Throw into the mix that the bulk of the real work of building an
application is not the coding, but all the work surrounding that, as Udi
Dahan points out in his post on [The Fallacy of
ReUse](http://www.udidahan.com/2009/06/07/the-fallacy-of-reuse/ "The Fallacy of ReUse").

This is not to say that I don’t think we should continue to invest in
building better and better tools. After all, the history of software
development is about building better and better higher level tools to
make developers more productive. I think the danger lies in trying to
remove the discipline and traits that will always be required when using
these tools to build applications.

Even when you can tell the computer what you want in human terms, and it
figures it out, it’s important to still follow good software development
principles, ensure quality checks, tests, etc…

The lesson for us programmers, I believe is two-fold. One, we have to
educate our stakeholders about how software production really works.
Even if they won’t listen, a little knowledge and understanding here
goes a long way. Be patient, don’t be condescending, and hope for the
best. Secondly, we have to educate ourselves about the business in a
deep manner so that we are seen as valuable business partners who happen
to write the code that matters.

