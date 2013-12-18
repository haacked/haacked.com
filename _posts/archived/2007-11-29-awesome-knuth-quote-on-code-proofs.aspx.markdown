---
layout: post
title: "Awesome Knuth Quote On Code Proofs"
date: 2007-11-29 -0800
comments: true
disqus_identifier: 18426
categories: [code,tdd]
---
My friend (and former boss and
[business](http://veloc-it.com/ "VelocIT - Where I used to work")
partner) [Micah](http://micahdylan.com/ "Micah Dylan") found this gem of
a quote from Donald Knuth addressing code proofs.

> Beware of bugs in the above code; I have only proved it correct, not
> tried it.

Micah writes [more on the
topic](http://micahdylan.com/archive/2007/11/29/nothing-to-prove-here-move-along.aspx "Nothing to prove here, move along")
and reminds me of why I enjoyed working with him so much. He’s always
been quite thoughtful in his approach to problems. And I’m not just
saying that because he agrees with me. ;)

On another note, several commenters pointed out that one thing I didn’t
mention before, but should have, is that verifying the quality of code
is only one small aspect of unit testing and Test Driven Development.

The more important factor is that TDD is a *design process*. Employing
TDD is one (not the only one, but I think it is a good one) approach for
improving the design of code and especially the usability of your code.
By usability, I mean from another developer’s perspective.

If I have to create twenty different objects in order to call a method
on your class, your class is probably not very usable to other
developers. TDD is one approach that forces you to find that out sooner,
rather than later.

A code proof won’t necessarily find that “flaw” because it is not a flaw
in logic.

Tags: [TDD](http://technorati.com/tags/TDD/ "TDD tag") , [Code
Provability](http://technorati.com/tags/Code%20Provability/ "Code Provability tag")

