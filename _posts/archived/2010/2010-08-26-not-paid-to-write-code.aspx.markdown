---
title: We're Not Paid To Write Code
tags: [code,software,developers,methodologies]
redirect_from: "/archive/2010/08/25/not-paid-to-write-code.aspx/"
---

On Twitter yesterday I made the [following
comment](http://twitter.com/haacked/status/22118616918 "Comment on Twitter"):

> We're not here to write software, we're here to ship products and
> deliver value. Writing code is just a fulfilling  means to that end :)

[![binary-code](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WereNotHereToWriteSoftware_134DA/binary-code_thumb.jpg "binary-code")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WereNotHereToWriteSoftware_134DA/binary-code_2.jpg)
*All I see now is blonde, brunette, redhead.*

For the most part, I received a lot of tweets in agreement, but there
were a few who disagreed with me:

> While I agree in principle, the stated sentiment "justifies" the
> pervasive lack of quality in development

> Doctors with this mentality don't investigate root causes, because
> patients don't define that as valuable

> That's BS. If you live only, or even primarily, for end results you're
> probably zombie. We're here to write code AND deliver value.

I have no problem with people disagreeing with me. Eventually they’ll
learn I’m *always* right. ;) In this particular case, I think an
important piece of context was missing.

What’s that you say? Context missing in a 140 character limited tweet?
That could never happen, right? Sure, you keep telling yourself that
while I pop a beer over here with Santa Claus.

The tweet was a rephrasing of something I told a Program Manager
candidate during a phone interview. It just so happens that the role of
a program manager at Microsoft is not focused on writing code like
developers. But that wasn’t the point I was making. I’ve been a
developer in the past (and I still play at being a developer in my own
time) and I still think this applies.

What I really meant to say was that we’re not ***paid*** to write code.
I absolutely love writing code, but in general, it’s not what I’m paid
to do and I don’t believe it ever was what I was paid to do even when I
was a consultant.

For example, suppose a customer calls me up and says,

“Hey man, I need software that allows me to write my next book. I want
to be able to print the book and save the book to disk. Can you do that
for me?”

I’m not going to be half way through writing my first unit test in
Visual Studio by the end of that phone call. Hell no! I’ll step away
from the IDE and hop over to Best Buy to purchase a copy of Microsoft
Word. I’ll then promptly sell it to the customer with a nice markup for
my troubles and go and sip Pina Coladas on the beach the rest of the
day. Because that’s what I do. I sip on Pina Coladas.

At the end of the day, I get paid to provide products to my customers
that meet their needs and provides them real value, whether by writing
code from scratch or finding something else that already does what they
need.

Yeah, that’s a bit of cheeky example so let’s look at another one.
Suppose a customer really needs a custom software product. I could write
the cleanest most well crafted code the world has ever seen (what a guy
like me might produce during a prototype session on an off night), **but
if it doesn’t ship, I don’t get paid**. Customer doesn’t care how much
time I spent writing that code. They’re not going to pay me, until I
deliver.

### Justifying lack of quality

Now, I don’t think, as one Twitterer suggested, that this “justifies a
pervasive lack of quality in development” by any means.

Quality in development is important, but it has to be scaled
appropriately. Hear that? That’s the sound of a bunch of eggs lofted at
my house in angry disagreement. But hear me out before chucking.

A lot of people will suggest that all software should be written with
the utmost of quality. But the reality is that we all scale the quality
of our code to the needs of the product. If that weren’t true, we’d
*all* use [Cleanroom Software
Engineering](http://en.wikipedia.org/wiki/Cleanroom_Software_Engineering "Cleanroom Software Engineering")
processes like those employed by the [Space Shuttle
developers](http://www.fastcompany.com/node/28121/print "They Write the Righ Stuff").

So why don’t we use these same processes? Because there are factors more
important than quality in building a product. While even the Space
Shuttle coders have to deal with changing requirements from time to
time, in general, the laws of physics don’t change much over time last I
checked. And certainly, their requirements don’t undergo the level of
churn that developers trying to satisfy business needs under a rapidly
changing business climate would face. Hence the rise of agile
methodologies which recognize the need to embrace change.

Writing software that meets changing business needs and provides value
is more important than writing zero defect code. While this might seem
I’m giving quality a short shrift, another way to look at it is that I’m
taking a higher view of what defines *quality* in the first place.
Quality isn’t just the defect count of the code. It’s also how well the
code meets the business needs that defines the “quality” of an overall
product.

[The debunking of the Betamax is better than
VHS](http://www.guardian.co.uk/technology/2003/jan/25/comment.comment "debunking betamax better than vhs")
myth is a great example of this idea. While Betamax might have been
technically superior to VHS in some ways, when you looked at the “whole
product”, it didn’t satisfy customer needs as well as VHS did.

[Nate Kohari](http://kohari.org/ "Nate Kohari's Blog") had an
interesting insight on how important delivering value is when he writes
about the [lessons learned building Agile
Zen](http://kohari.org/2010/08/24/looking-back/ "Looking Back"), a
product I think is of wonderful quality.

> It also completely changed the way that I look at software. I’ve tried
> to express this to others since, but I think you just have to
> experience it firsthand in order to really understand. It’s a unique
> experience to build a product of your own, from scratch, with no
> paycheck or deferred responsibility or venture capital to save you —
> you either create real value for your customers, or you fail. And I
> don’t like to fail.

**Update:** Dare Obasanjo wrote a timely blog that dovetails nicely with
the point I’m making. He writes that [Google Wave and REST vs SOAP
provide a cautionary
tale](http://www.25hoursaday.com/weblog/2010/08/27/LessonsFromGoogleWaveAndRESTVsSOAPFightingComplexityOfOurOwnChoosing.aspx "Complexity of our own choosing")
for those who focus too much on solving hard technical problems and miss
solving their customers actual problems. Sometimes, when we think we’re
paid to code, we write way too much code. Sometimes, less code solves
the actual problems we’re concerned with just fine.

### Code is a part of the whole

The Betamax vs VHS point leads into another point I had in mind when I
made the original statement. As narcissistic developers (c’mon admit it.
You are all narcissists!), we tend to see the code as being the only
thing that matters. But the truth is, it’s one part of the whole that
makes a product.

There’s many other components that go into a product. A lot of time is
spent identifying future business needs to look for areas where software
can provide value. After all, no point in writing the code if nobody
wants to use it or it doesn’t provide any value.

Not to mention, at Microsoft, we put a lot of effort into localization
and globalization ensuring that the software is translated into multiple
languages. On top of this, we have writers who produce documentation,
legal teams who work on licenses, marketing teams who market the
product, and the list goes on. A lot goes into a product beyond just the
code. There are also a lot of factors outside the product that
determines its success such as community ecosystem, availability of
add-ons, etc.

### I love to code

Now don’t go running to tell on me to my momma.

“Your son is talking trash about writing code!”

It’d break her heart and it’d be completely untrue. I love to code!
There, I said it. In fact, I love it so much, I tried to marry it, but
then got a much better offer from a very lovely woman. But I digress.

Yes, I love coding so much I often do it for free in my spare time.

I wasn’t trying to make a point that writing code isn’t important and
doesn’t provide value. It absolutely does. In fact, I firmly believe
that writing code is a huge part of providing that value or we wouldn’t
be doing it in the first place. This importance is why we spend so much
time and effort trying to elevate the craft and debating the finer
points of how to write good software. It’s an essential ingredient to
building great software products.

The mere point I was making is simply that while writing code is a huge
factor in providing value, it’s not the part we get paid for. Customers
pay to receive value. And they only get that value when the code is in
their hands.

