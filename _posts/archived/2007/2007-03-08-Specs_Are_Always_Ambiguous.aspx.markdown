---
title: Requirements and Specs Are Always Ambiguous
date: 2007-03-08 -0800
disqus_identifier: 18233
tags: []
redirect_from: "/archive/2007/03/07/Specs_Are_Always_Ambiguous.aspx/"
---

UPDATE: As an aside, it would probably be more accurate to say the
FizzBuzz question is a **Requirement**. So where you read the term
*Spec*, you can replace it with *Requirement*. Either way, the same
thing applies. The only thing not ambiguous is the code. As they say,
the code is the spec.

One last point, then I’m done with this topic of FizzBuzz and spec
writing. In a [recent
post](https://haacked.com/archive/2007/03/07/Why_Cant_Spec_Writers_Write.Specs.aspx "Why Can't Spec Writers Write Specs"),
I mentioned *tongue firmly in cheek*that the [FizzBuzz
“spec”](http://www.codinghorror.com/blog/archives/000781.html "Why Can't Programmers Program")
has certain flaws. Now I admit I’m taking this out of context a bit to
make a point. FizzBuzz is an *simple interview*question, not a spec,
possbily intended to elicit this type of analysis from the
candidate. Even so, I think there’s a good lesson to learn here.

My point was that **all** specs are merely rough approximations of the
actual requirement. **Specs are ambiguous, but software is not.**
Software doesn’t generally deal well with ambiguity. Change a random bit
in memory and all hell breaks loose.

However, some of that was lost due to the extremely nitpicky point I
made about the spec. So here’s another, still nitpicky, but a bit less
so.

Every so called “correct” program written in the comments of [Jeff’s
blog](http://codinghorror.com/blog/ "Jeff Atwood") had the following
output.

    1
    2
    Fizz
    4
    Buzz
    Fizz
    7
    Fizz
    Buzz
    11
    Fizz
    13
    14
    FizzBuzz

But, doesn’t the following output meet the letter of the spec
(*difference in bold*)?

    1
    2
    Fizz
    4
    5Buzz
    Fizz
    7
    8
    Fizz
    10Buzz
    11
    Fizz
    13
    14
    FizzBuzz

My point being, the spec is explicit about replacing numbers divisible
by three with “Fizz”, but it doesn’t say to replace numbers divisible by
five.

Yes, I agree. Developers should not act like total logicians and nitpick
every detail. **Human language is inexact, and we have to deal with that
fact of life.** Unfortunately, sofware doesn’t have the same resiliency
towards ambiguity. If this output was meant to be fed into another
software system, this ambiguity would cause bad data, software crashes,
who knows what calamity!

**You might say I’m *splitting hairs* here. Of course I am because the
compiler is going to split hairs.** The Web Service I’m trying to call
is going to split hairs. The HTML browser is going to try and not split
hairs, but is going to ultimately fail. **Software is all about
splitting hairs.**

Instead, we need to move beyond the spec and ask questions before
writing code, during writing code, and after writing code. Do not be
afraid to talk to the customer or customer representative. That’s all I
was trying to say.

Thanks to [Rob Conery](http://blog.wekeroad.com/ "Rob Conery") who was
trying to make this [point in my
comments](https://haacked.com/archive/2007/02/27/Why_Cant_Programmers._Read.aspx#35330 "Rob's Point"),
but it was lost on everybody. ;)

