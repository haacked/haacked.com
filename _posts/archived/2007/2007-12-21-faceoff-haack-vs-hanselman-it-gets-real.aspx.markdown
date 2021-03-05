---
title: Faceoff! Haack vs Hanselman - It Gets Real
tags: [code]
redirect_from: "/archive/2007/12/20/faceoff-haack-vs-hanselman-it-gets-real.aspx/"
---

![Faceoff](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/FaceoffHaackvsHanselmanItGetsReal_8279/a1aadf66-4786-4f6a-9a99-1acbd7d07220_ms%5B1%5D_3.jpg)
Recently, Maxfield Pool from
[CodeSqueeze](http://www.codesqueeze.com/ "Code Squeeze") sent me an
email about a new monthly feature he calls [Developer
Faceoff](http://www.codesqueeze.com/developer-faceoff-scott-hanselman-vs-phil-haack/ "Developer Faceoff")
in which he pits two developers side-by-side for a showdown.

It’s an obvious attempt to gain readers via an appeal to vanity of the
featured bloggers who have no choice to link to it. Brilliant!  :)

Seriously, I think it’s a fun and creative idea, but I have no
credibility in saying so because I’m obviously biased being featured
alongside my longtime nemesis and friend, [Scott
Hanselman](http://www.hanselman.com/blog/ "Computerzen"). So [check it
out for
yourself](http://www.codesqueeze.com/developer-faceoff-scott-hanselman-vs-phil-haack/ "Developer Faceoff: Scott Hanselman vs Phil Haack").

Some of my answers were truncated due to the format so I thought it’d be
fun to elaborate on a couple of questions.

**5. Are software developers - engineers or artists?**

Don’t take this as a copout, but a little of both. I see it more as
craftsmanship. Engineering relies on a lot of science. Much of it is
demonstrably empirical and constrained by the laws of physics. Software
is less constrained by physics as it is by the limits of the mind.

Software in many respects is as much a social activity as it is an
engineering discipline. Working well as a team is essential, as is
understanding your users and how they get their work done so your
software helps, rather than hinders.

Much of software is based on faith and anecdotal evidence. For example,
do we have scientific evidence that TDD improves the design of code? Do
we have empirical evidence that it doesn’t? Research is scant, in part
because it’s extremely challenging to set up valid experiments. Much of
software research focuses on retrospective research, the sort that
sociologists do.

So again, back to the question. Crafting a sorting algorithm is
engineering. Building a line of business app that delights all the users
is an art.

**8. What is the biggest mistake you made along the way?**

My first year as software developer, I deployed a reset password feature
to a large music community site. A week or so after we deployed, a child
of a VP of the client (or maybe it was an investor, I can’t remember)
complained she couldn’t get into her account and hadn’t received her new
password.

I was “sure” I had tested it, but it turned out I hadn’t done a very
good job of it. There was a bug in my code and a bunch of users were
waiting around for a regenerated password that would never arrive.

Needless to say, my boss wasn’t very happy it and for a good while I
tread lightly worried I’d lose my job if I made another mistake. In
retrospect, it was a good thing to get such a big production mistake out
of the way early because I was extremely careful afterwards, always
triple checking my work.

