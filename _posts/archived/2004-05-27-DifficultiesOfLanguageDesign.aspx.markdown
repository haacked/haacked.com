---
layout: post
title: 'Tech-Ed 2004: The Difficulties of Language Design'
date: 2004-05-27 -0800
comments: true
disqus_identifier: 492
categories: []
redirect_from: "/archive/2004/05/26/DifficultiesOfLanguageDesign.aspx/"
---

The best part of Tech-Ed 2004 is how Microsoft puts us developers in
touch with the people who are creating the languages and tools we use.
They've accomplished this in two ways.

First, with the Rio system (nothing to do with Duran Duran). This system
allows you to search for attendees (who've registered in the system) by
name or interests and request a meeting with them. This is the debut of
the system and I don't believe it's being well utilized by attendees.
This is great for me as most everyone I've wanted to talk to has been
available and I've had a chance to meet members of the C\# team such as
[Eric Gunnerson](http://blogs.msdn.com/ericgu "Eric G") (PM for the C\#
compiler) and Anders Hejlsberg (a Distinguished Engineer at Microsoft
and chief designer of Delphi and the C\# language). Unfortunately Steve
Ballmer was not in the system.

Second, by having Microsoft employees hang around the cabana areas
(replete with comfortable couches), it's easy to walk up to the devs who
are building the next generation of tools I use (such as [Anson
Horton](http://blogs.msdn.com/ansonh "Anson") and [Cyrus
Najmabadi](http://blogs.msdn.com/cyrusn "Cyrus")) and ask, "So whatcha
got?"

If there's one thing I've taken away from my various discussions about
language design is that language design is hard. This may be obvious to
you, but it's not obvious to everyone. Look in any unmoderated newsgroup
about programming language and you'll hear plenty of "Java sucks!" or
"C\# stinks" (perhaps even more colorful than that). A lot of people
carry a one language fits all mentality when in reality, each language
has a purpose and target in mind.

There are three reasons that come to mind to explain the difficulty of
language design:

-   Language changes shouldn't break existing code...too much.
-   Total language purity is unattainable, but we try anyway.
-   Language design must take into consideration human behavior.

**Language changes shouldn't break existing code...too much**\
This is one of the more difficult issues when designing a language. How
do you update the language without breaking thousands if not millions of
lines of code out in the wild. Even small changes that seemingly should
cause no problems can break code. Well hopefully you have legions and
legions of regression tests, but they can only go so far. This remains a
difficult challenge.

**Total language purity is unattainable, but we try anyway** \
Let's face it, if we're not seeking the ideal pure perfect design, why
are we in the business. It's a natural tendency. However, a good
designer realizes that total purity is unattainable. It's a simple fact:
real world pressures are factors in language design. These guys have to
ship and deadlines will affect which features they keep or don't keep.
More subtly, sometimes the order in which a feature is designed affects
the language design.

I asked Eric Gunnerson whether they've considered adding a timeout
syntax to the lock statement ala Ian Griffiths' TimedLock structure. He
in turn asked me, would creating this new syntax have any more clarity
than using the TimedLock structure? Ummm... I guess not since we can
already do this in a clean and concise manner. Right. So why add syntax.
Not only that, the TimedLock demonstrates what the C\# team had in mind
with the **`using`** statement. It wasn't intended just for cleanup, but
for situations just like this.

Naturally, if this were the case, why even have the **`lock`**
statement, as the using statement makes it unnecessary. It turns out,
the lock statement was introduced long before they introduced the using
statement when creating the language. At that point it wouldn't make
sense to refactor the lock statement out of the language as it was
likely used all over the place and would introduce a major breaking
change (see the first reason why language design is hard). Wow, you mean
real world issues such as timing will affect the purity of language
design? Indeed, total purity is an illusion.

**Language design must take into consideration human behavior**\
Another reason language design is hard is that it must take into account
human behavior. Just as we have usability testing for GUI applications,
usability testing for APIs and languages are also important.

Take the "throws" statement in Java when declaring a method. This
statement is followed by the type of exception (or exceptions) that the
method may throw. When calling this method, the developer must catch and
handle every exception declared by the throws clause. From a purity
standpoint, this is beautiful. If a method could throw this exception,
certainly the developer should be forced to do something about it. But
now let's examine the behavior of real developers in the field. They
just want to call this method to get the work done and handle the
exceptional cases later (or in a method up the call stack). However, the
code won't compile until they catch each exception. So what do they
typically do? They catch(Exception e) and forget about it. No more
compiler errors, but if they never return, they've lost a lot of
valuable information about the exception.

Some will argue that we shouldn't pander to developers with bad habits
like this and teach them to do the right thing. But this isn't
necessarily a case of developer ignorance. The physics of software
development states that developers naturally take the path of least
resistance i.e. we're lazy. We have to ship software. We don't have time
to do everything in the most pure fashion. The language has to work for
us, not against us. We absolutely have to pander (to a degree) to lazy
programmers because they're creating the software that's running our
cars, flight control, etc... Figure out why developers don't perform a
best practice and learn how to make the best practice the path of least
resistance. That should be a focus of language design. It's not always
possible. Sometimes we just have to admit that software development is
hard. I certainly don't want developers to be lazy about security. But
whenever possible, I want to make writing secure code, the path of least
resistance.

