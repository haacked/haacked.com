---
title: Patterns in Number Sequences
date: 2005-10-20 -0800
disqus_identifier: 10899
categories: []
redirect_from: "/archive/2005/10/19/patterns-in-number-sequences.aspx/"
---

You know you’re a big geek when a sequence of numbers with an
interesting property just pops in your head. No, I’m not talking about
myself (this time). [Jayson Knight](http://jaysonknight.com/blog/) is
the big geek as he [noticed a
pattern](http://jaysonknight.com/blog/archive/2005/10/20/2248.aspx) in a
sequence of numbers that popped in his head...

> This just popped into my head the other day for no other reason than
> to bug me: Square all odd numbers starting with 1...subtract 1 from
> the result...then divide by 8. Now look for the pattern in the
> results.

He even provides a code sample to do the math for you, but you can
easily do it by hand on paper. The pattern he noticed can be phrased
another way, the square of any odd number when divided by eight leaves a
remainder of 1.

This is actually a pattern noticed by John Horton Conway and Richard Guy
in 1996. They stated that in general, the odd squares are congruent to 1
(mod 8).

I couldn’t find their proof, but it is easily [proved by
induction](http://www.cc.gatech.edu/people/home/idris/AlgorithmsProject/ProofMethods/Induction/ProofByInduction.html).
I’ll walk you through the steps.

**The Proposition**\
 We want to prove that

 

> `(x2 - 1) mod 8 = 0 for all x >= 1 where x is an odd integer.`

Note that this is the same as proving that `x2 mod 8 = 1`. In other
words, if we prove this, we prove the interesting property Jayson’s
noticed.

**Verify the Base Case**\
 Here’s where our heavy duty third grade math skills come into play. We
try out the case where `x = 1`.

> `(12 - 1) mod 8 = 0 mod 8`

So yes, 0 mod 8 is zero, so we’re good with the base case.

**Formulate the Inductive Hypothesis**\
 Ok, having demonstrated the fact for x = 1, let’s hypothesise that it
is indeed true that

> `(x2 - 1) mod 8 = 0 for all odd x > 1 where x is an odd integer`

**Now prove it**\
 Here we prove the *next* case. So assuming our above hypothesis is
true, we want to show that the it must be true for the next odd number.
We want to show that

> `((x+2)2 - 1) mod 8 = 0`

Well that can be multiplied out to...

> `((x2 + 4x + 4) - 1) mod 8 = 0` *Note I don't subtract the one from
> the four.*

So just re-arranging the numbers a bit we get...

> `((x2 - 1) + 4x + 4) mod 8 = 0`

Now I factor the right side and get (You do remember factoring right?)

> `((x2 - 1) + 4(x + 1)) mod 8 = 0`

Ok, you should notice here that we know what's on the left side is
certainly divisible by 8 due to our hypothesis. So we just need to prove
that 4(x+1) is also divisible by 8. If two numbers are divisible by
another number, we know the sum of the two numbers are also divisble by
that number.

Well it should be pretty clear that 4(x+1) is divisible by eight. How?
Well since x is an odd number, x + 1 must be an EVEN number. We can
rewrite (x + 1) as 2n where n is an integer (the very definition of an
even number). So our equation becomes...

> `(x2 - 1) + 4(2n) mod 8 = 0`

Which is naturally...

> `((x2 - 1) + 8n) mod 8 = 0`

And we’re pretty much done. We know that (x^2^ - 1) is divisible by
eight due to our inductive hypothesis. We also know 8n is divisible by
eight. Therefore the sum of the two numbers must be divisble by 8. And
the proof is in the pudding.

Ok, some of you are probably thinking I am hand waving that last
conclusion. So I will quickly prove the last step. Since we know that
the left hand side is divisible by eight, we can substitute 8m where m
is an integer (the very definition of a number divisible by eight).

That leaves us with...

> `(8m + 8n) mod 8 = 0`

which factors to...

> `(8(m + n)) mod 8 = 0`

**Conclude the proof for formality sake**\
 And thus, proposition is true for all odd integers.

~~Sorry for such a long boring~~No need to thank me for a long and
scintillating math post, but it’s been a loooong time since I’ve
stretched my math muscles. This was a fun excercise in inductive proofs.

**So how does an inductive proof prove anything?** At first glance, for
those unfamiliar with inductive proofs, it hardly seems like we proved
anything. Our proof rests on an assumption. We stated that *if* our
assumption is true for one odd number, then next odd number must
exihibit the same behavior. We went ahead and proved that to be true,
but it still leaves the possibility that this isn’t true for any odd
number.

That’s where our base case comes in. We showed that for x = 1, it is
indeed true. So since it is true for x = 1, we’ve proved it is true for
x = 3. Since it is true for x = 3, we know it is true for x = 5. Ad
infinitum.

And that concludes today’s math lesson.

UPDATE: Fixed a couple typos. Thanks Jeremy! Also, optionsScalper in my
comments list a lot of great links about number theory and congruences.
I applied his correct suggestion to clarify the mod operations by
putting a parenthesis around the left hand side.

