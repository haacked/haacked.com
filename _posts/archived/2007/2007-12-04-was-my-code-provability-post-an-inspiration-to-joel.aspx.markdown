---
title: Was My Code Provability Post An Inspiration To Joel?
tags: [code,tdd,methodologies]
redirect_from: "/archive/2007/12/03/was-my-code-provability-post-an-inspiration-to-joel.aspx/"
---

*![Brazil
Jersey](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DidJoelReadMyPostOnCodeProvability_211/BrazilJersey_3.jpg)
Note that in the same vein as Pele, Ronaldinho and Ronaldo, Joel has
reach that Brazillian Soccer player level of stardom in the geek
community and can pretty much go by just his first name. Admit it, you
knew who I was referring to in the title. Admit it!*

Please indulge me in a brief moment of hubris.

I was reading part 1 of [Joel Spolsky’s talk at
Yale](http://www.joelonsoftware.com/items/2007/12/03.html "Joel's Talk at Yale Part 1")
that he gave on November 28 and came upon this quote on code
provability...

> The problem, here, is very fundamental. In order to mechanically prove
> that a program corresponds to some spec, the spec itself needs to be
> extremely detailed. In fact the spec has to define everything about
> the program, otherwise, nothing can be proven automatically and
> mechanically. Now, if the spec does define everything about how the
> program is going to behave, then, lo and behold, it contains all the
> information necessary to generate the program!

Hey, that sounds familiar! By coincidence, [I wrote the
following](https://haacked.com/archive/2007/11/16/what-exactly-are-you-trying-to-prove.aspx "What exactly are you trying to prove")
on November 16^th^,12 days prior to Joel’s talk...

> The key here is that the postulate is an unambiguous specification of
> a truth you wish to prove. To prove the correctness of code, you need
> to know exactly what correct behavior is for the code, i.e. a complete
> and unambiguous specification for what the code should do. So tell me
> dear reader, when was the last time you received an unambiguous fully
> detailed specification of an application?
>
> If I ever received such a thing, I would simply execute that sucker...

Sure, it isn’t exactly an original thought, but the timing seemed too
coincidental. Could it really be? Could my post inspired one small point
in a talk given by someone of his stature?

For a moment, I allowed myself to dream like a true fanboy that Mr. Joel
really did read my blog. Until I read this (cue sound of pots and pans
crashing down)...

> The geeks want to solve the problem automatically, using software.
> They propose things like unit tests, test driven development,
> automated testing, dynamic logic and other ways to “prove” that a
> program is bug-free.

Crestfallen, I realized Mr. Joel indeed did not read my post, in which I
claimed...

> Certainly no major TDD proponent has ever stated that testing provides
> proof that your code is correct. That would be outlandish.

To be fair, I wouldn’t call Joel a major TDD proponent, but I hoped he
would understand that TDD is about improving the design of code and also
providing *confidence* to make changes to the system. It’s no proof of
correctness, but can be a proof of incorrectness when a test fails.

Oh well, one can dream.

