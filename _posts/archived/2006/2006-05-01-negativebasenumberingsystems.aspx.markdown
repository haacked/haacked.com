---
title: Negative Base Numbering Systems
date: 2006-05-01 -0800
disqus_identifier: 12617
categories: [math]
redirect_from: "/archive/2006/04/30/negativebasenumberingsystems.aspx/"
---

UPDATE: I updated the article a bit to better explain decimal expansion to negabinary

Ok, here is where I go and really geek out a bit. Scott presents a [simple javascript](http://www.hanselman.com/blog/MakingNegativeNumbersTurnRedUsingCSSAndJavascript.aspx "Making Negative Numbers Turn Red") to display negative numbers as red. He takes a nice clean straightforward approach by using javascript to inject a CSS class on
specific elements that have a negative number.

As his script merrily iterates its way through the page’s elements, it checks the values of the element to see if the first character is a “-” (dash). And this works just fine for the majority of you people so thoroughly stuck on the “decimal” system.

But as I pointed out in his comments, this discriminates against negative base numbering systems such as ...drumroll... **Negabinary**!

*Doesn’t negabinary sound like one in a long string of major villains to attack Godzilla and end up destroying Tokyo yet again?*

Negabinary is a lot like binary’s evil twin. **Rather than a base 2 system, negabinary is base -2**. The beauty of negabinary is that there is no need for a negative sign (aka the sign bit). All integers, negative or positive, can be written as an unsigned stream of 1s and 0s.

To expand a decimal number into negabinary, you simply divide the number by -2 repeatedly. Each time you divide the number, you record the non-negative remainder of 0 or 1. Afterwards, you take those remainders in reverse order and there you have it, the negabinary expansion. Simple no?

Keep in mind that we are doing remainder division here. So -1/-2 is not one half, but 1 remainder 1. Likewise, 1/-2 is 0 remainder 1.

Huh?

**Keep in mind this simple algerbraic formula: if a / b = c remainder d, then bc + d = a.**

Thus, to expand decimal 2 in negabinary:

     2 / -2 = -1 remainder 0
    -1 / -2 =  1 remainder 1
     1 / -2 =  0 remainder 1

Taking those remainders in reverse order we get 110. So 110 is the negabinary representation of decimal 2.

I remember learning that there were computing systems built (perhaps experimental) that used negabinary instead of binary. Apparently there are benefits to representing a number without a signed bit. Unfortunately, like a good evil twin, negabinary makes arithmetic
operations quite complicated.

I was going to write up a whole exposé on negabinary, but the Wikipedia did a [much better
job](http://en.wikipedia.org/wiki/Negabinary "Negabinary") than I would have. My memory of my college math lectures on alternate numbering system is pretty hazy. Throughout history, humans have tried out various numbering systems other than base 10. The Mayans used some sort of hybrid of base 20 and base 360. I kid you not.

So with a small alteration, we can adjust Scott’s script to accomodate negabinary enthusiasts.
