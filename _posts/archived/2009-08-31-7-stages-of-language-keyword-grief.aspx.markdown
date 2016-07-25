---
layout: post
title: 7 Stages of new language keyword grief
date: 2009-08-31 -0800
comments: true
disqus_identifier: 18641
categories: []
redirect_from: "/archive/2009/08/30/7-stages-of-language-keyword-grief.aspx/"
---

My [last
post](http://haacked.com/archive/2009/08/26/method-missing-csharp-4.aspx "Fun with method missing in C# 4")
on the new `dynamic` keyword sparked a range of reactions which are not
uncommon when discussing a new language keyword or feature. Many are
excited by it, but there are those who feel a sense of…well…grief when
their language is “marred” by a new keyword.

C\#, for example, has seen it with the `var` keyword and now with the
`dynamic` keyword. I don’t know, maybe there’s something to this idea
that developers go through the seven stages of grief when their favorite
programming language adds new stuff (*Disclaimer: Actually, I’m totally
making this crap up*)

### 1. Shock and denial.

With the introduction of a new keyword, initial reactions include shock
and denial.

> *No way are they adding lambdas to the language! I had a hard enough
> time with the `delegate` keyword!*

> *What is this crazy ‘expression of funky tea’ syntax? I’ll just ignore
> it and hope it goes away.*

> *Generics will never catch on! Mark my words.*

### 2. Longing for the past

Immediately, even before the new feature is even released, developers
start to wax nostalgic remembering a past that never was.

> *I loved language X 10 years ago when it wasn’t so bloated, man.*

They forget that the past also meant managing your own memory
allocations, punch cards, and dying of the black plague, which totally
sucks.

### 3. Anger and FUD

Soon this nostalgia turns to anger and FUD.

Check out [this
reaction](http://bugs.php.net/bug.php?id=48669 "PHP now includes GOTO")
to adding the goto keyword to PHP, emphasis mine.

> This is a problem. Seriously, PHP has made it \
> this far without `goto`, **why turn the language into a public
> menace?**

“*Yes Robin, PHP is a menace terrorizing Gotham City. To the
Batmobile!*”

The `dynamic` keyword elicited similar anger with comments like:

> *C\# was fine as a static language. If I wanted a dynamic language,
> I’d use something else!*

Or

> *I’ll never use that feature!*

It’s never long before anger turns to spreading FUD (Fear Uncertainty
Doubt). The `var` keyword in C\# is a prime example of this. Many
developers wrote erroneously that using it would mean that your code was
no longer strongly typed and would lead to all hell breaking use.

“*My friend used the `var` keyword in his program and it formatted his
hard drive, irradiate his crotch, and caused the recent economic crash.
True story.*”

Little did they know that the `dynamic` keyword was on its way which
really would fulfill all those promises. ;)

Pretty much the new feature will destroy life on the planet as we know
it and make for some crappy code.

### 4. Depression, reflection, and wondering about its performance

> *Sigh. I now have to actually learn this new feature, I wonder how
> well it performs.*

This one always gets me. It’s almost always the first question
developers ask about a new language feature? “*Does it perform?*”.

I think wondering about its performance is a waste of time. For your
website which gets 100 visitors a day, yeah, it probably performs just
fine.

The better question to ask is “*Does my application perform well enough
for my requirements?*” And if it doesn’t then you start measuring, find
the bottlenecks, and then optimize. Odds are your performance problems
are not due to language features but to common higher level mistakes
such as the [Select N+1
problem](http://ayende.com/Blog/archive/2006/05/02/CombatingTheSelectN1ProblemInNHibernate.aspx "Select N+1 Problem").

### 5. The upward turn

> *Ok, my hard drive wasn’t formatted by this keyword. Maybe it’s not so
> bad.*

At this point, developers start to realize that the new feature doesn’t
eat kittens for breakfast and just might not be evil incarnate after
all. Hey! It might even have some legitimate uses.

This is the stage where I think you see a lot of experimentation with
the feature as developers give it a try and try to figure out where it
does and doesn’t work well.

### 6. Code gone wild! Everything is a nail

I think we all go through this phase from time to time. At some point,
you realize that this new feature is really very cool so you start to go
hog wild with it. In your hands the feature is the Hammer of Thor and
every line of code looks like a nail ready to be smitten.

Things can get ugly at this stage in a fit of excitement. Suddenly every
object is anonymous, every callback is a lambda, and every method is
generic, whether it should be or not.

It’s probably a good idea to resist this, but once in a while, you have
to let yourself give in and have a bit of fun with the feature. Just
remember the following command.

`svn revert -R`

Or whatever the alternative is with your favorite source control system.

### 7. Acceptance and obliviousness

At this point, the developer has finally accepted the language feature
as simply another part of the language like the `class` or `public`
keyword. There is no longer a need to gratuitously use or over-use the
keyword. Instead the developer only uses the keyword occasionally in
cases where it’s really needed and serves a useful purpose.

It’s become a hammer in a world where not everything is a nail. Or maybe
it’s an awl. I’m not sure what an awl is used for, but I’m sure some of
you out there do and you probably don’t use it all the time, but you use
it properly when the need arises. Me, I never use one, but that’s
perfectly normal, perfectly fine.

For the most part, the developer becomes oblivious to the feature much
as developers are oblivious to the `using` keyword. You only think about
the keyword when it’s the right time to use it.

### Conclusion

Thanks to everyone on Twitter who provided examples of language keywords
that provoked pushback. It was helpful.

