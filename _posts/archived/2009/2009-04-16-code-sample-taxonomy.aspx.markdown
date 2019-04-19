---
title: Code Sample Taxonomy
tags: [code]
redirect_from: "/archive/2009/04/15/code-sample-taxonomy.aspx/"
---

What responsibility do we have as software professionals when we post
code out there for public consumption?

I don’t have a clear cut answer in my mind, but maybe you can help me
formulate one. :)

For example, I recently posted [a sample on my
blog](https://haacked.com/archive/2009/04/14/using-jquery-grid-with-asp.net-mvc.aspx "Using jQuery Grid with ASP.NET MVC")
intended to show how to use jQuery Grid with ASP.NET MVC.

The point of the sample was to demonstrate shaping a JSON result for the
jQuery grid’s consumption. For the sake of illustration, I wanted the
action method to be relatively self contained so that a reader would
quickly understand what’s going on in the code without having to jump
around a lot.

Thus the code takes some shortcuts with data access, lack of exception
handling, and lack of input validation. It’s pretty horrific!

Now before we grab the pitchforks (and I did say “*we”* intentionally as
I’ll join you) to skewer me, I did preface the code with a big “warning,
**DEMO CODE AHEAD**” disclaimer and so far, nobody’s beaten me up too
bad about it, though maybe by writing this I’m putting myself in the
crosshairs.

Even so, it did give me pause to post the code the way I did. Was I
making the right trade-off in sacrificing code quality for the sake of
blog post demo clarity and brevity?

In this particular case, I felt it was worth it as I tend to categorize
code into several categories. **I’m not saying these are absolutely
correct**, just opening up my cranium and giving you a peek in my head
about how I think about this:

-   **Prototype Code** – Code used to hash out an idea to see if it’s
    feasible or as a means of learning a new technology. Often very ugly
    throwaway code with little attention paid to good design.
-   **Demo Code** – Code used to illustrate a concept, especially in a
    public setting. Like prototype code, solid design is sometimes
    sacrificed for clarity, but these sacrifices are *deliberate*and
    *intentional*, which is very important. My jQuery Grid demo above is
    an example of what I mean.
-   **Sample Code** – Very similar to demo code, the difference being
    that good design principles should be demonstrated for the code
    relevant to the concept the sample is demonstrating. Code irrelevant
    to the core concept might be fine to leave out or have lower
    quality. For example, if the sample is showing a data access
    technique, you might still leave out exception handling, caching,
    etc… since it’s not the goal of the sample to demonstrate those
    concepts.
-   **Production Code** – Code you’re running your business on, or
    selling. Should be as high quality as possible given your
    constraints. Sometimes, shortcuts are taken in the short run
    (incurring [technical
    debt](http://www.codinghorror.com/blog/archives/001230.html "Technical Debt"))
    with the intention of paying down the debt ASAP.
-   **Reference Code** – This is code that is intended to demonstrate
    the correct way to build an application and should be almost
    idealized in its embracement of good design practices.

As you might expect, the quality the audience might expect from these
characterizations is not hard and fast, but dependent on context. For
example, for the Space Shuttle software, I expect the Production Code to
be much higher quality than production code for some intranet
application.

Likewise, I think where the code is posted and by whom is can affect
perception. We might expect much less from some blowhard posting code to
his personal blog, ummm, like this one.

Then again, if the person claims that his example is a best practice,
which is a [dubious claim in the first
place](http://www.satisfice.com/blog/archives/27 "No such thing as Best Practice"),
we may tend to hold it to much higher standards.

Now if instead of a person, the sample is posted on an official website
of a large company, say Microsoft, the audience may expect a lot more
than from a personal blog post. In fact, the audience may not make the
distinction between sample and reference application. This appears to be
the case recently with Kobe and in the past with Oxite.

Again, this is my perspective on these things. But my views have been
challenged recently via internal and external discussions with many
people. So I went to the font of all knowledge where all your wildest
questions are answered: Twitter. I posed the following two questions:

> **Do you have different quality expectations for a sample app vs a
> reference app?**
>
> **What if the app is released by MS? Does that change your
> expectations?**

The answers varied widely. Here’s a small sampling that represents the
general tone of the responses I received.

> Yes. A sample app should be quick and dirty. A reference app should
> exhibit best practices (error checking, logging, etc)
>
> No, same expectations... Even I ignore what is the difference between
> both.
>
> Regardless of who releases the app, my expectations don't change.
>
> Yes being from MS raises the bar of necessary quality, because it
> carries with it the weight of a software development authority.
>
> I don't think I have ever thought about what the difference in the two
> is, isn't a sample app basically a reference app?
>
> I don't think most people discriminate substantively betw the words
> "sample" and "reference."
>
> Everyone, Microsoft included, should expect to be judged by everything
> the produce, sample or otherwise.
>
> yes, samples do not showcase scalability or security, but ref apps
> do... i.e ref apps are more "enterprisey"
>
> IMHO, sample implies a quick attempt; mostly throw-away. Ref. implies
> a proposed best practice; inherently higher quality.
>
> No. **We as a community should understand the difference**.However MS
> needs to apply this notion consistently to it's examples.
>
> Whatever you release as sample code, **is \*guaranteed\* to be
> copy-pasted everywhere** - ask Windows AppCompat if you don't believe
> me

Note that this is a very unscientific sampling, but there is a lot of
diversity in the views being expressed here. Some people make no
distinction between *sample* and *reference* while others do. Some hold
Microsoft to higher standards while others hold everybody to the same
standard.

I found this feedback to be very helpful because I think we tend to
operate under one assumption about how our audience sees our samples,
but your audience might have a completely different view. This might
explain why there may be miscommunication and confusion about the
community reaction to a sample.

I highlighted the last two responses because they make up the core
dichotomy in my head regarding releasing samples.

On the one hand, I have tended to lean towards the first viewpoint. If
code has the proper disclaimer, shouldn’t we take personal
responsibility in understanding the difference?

Ever since starting work on ASP.NET MVC, we’ve been approached by more
and more teams at Microsoft who are interested in sharing yet more code
on CodePlex (or otherwise) and want to hear about our experiences and
challenges in doing so.

When you think about it, this is a great change in what has been an
otherwise closed culture. There are a lot of teams at Microsoft and the
quality of the code and intent of the code will vary from team to team
and project to project. I would hate to slow down that stream of sample
code flowing out because some people will misunderstand its purpose and
intend and cut and paste it. Yes, some of the code will be very bad, but
some of it will still be worth putting out there. After all, I tend to
think that if we stop giving the bad programmers bad code to cut and
paste, they’ll simply write the bad code themselves. Yes, posting good
code is even better, but I think that will be a byproduct of getting
more code out there.

On the other hand, there’s the macro view of things to consider. People
should also know not to use a hair dryer in the shower, yet they still
have these [funny warning
labels](http://www.rinkworks.com/said/warnings.shtml "Funny warning label")
for a reason. The fact that people *shouldn’t* do something sometimes
doesn’t change the fact that may still do it. We can’t simply ignore
that fact and the impact it may have. No matter how many disclaimers we
put on our code, people will cut and paste it. It’s not so bad that a
bad programmer uses bad code, but that as it propagates, the code gets
confused with the right way and spreads to many programmers.

Furthermore, the story is complicated even more by the inconsistent
labels applied to all this sample code, not to mention the inconsistent
quality.

So What’s the Solution?
-----------------------

Stop shipping samples.

Nah, I’m just kidding. ;)

Some responses were along the lines of Microsoft should just post good
code. I agree, I would really love it if every sample was of superb
quality. I’d also like to play in a World Cup and fly without wings, but
I don’t live in that world.

Obviously, this is what we should be striving for, but what do we do in
the meantime? Stop shipping samples? I hope not.

Again, I don’t claim to have the answers, but I think there are a few
things that could help. One [twitter
response](http://twitter.com/serialseb/status/1537092090 "SerialSeb")
made a great point:

> a reference app is going to be grilled. Even more if it comes from the
> mothership. **Get the community involved** \*before\* it gets pub

Getting the community involved is a great means of having your code
reviewed to make sure you’re not doing anything obviously stupid. Of
course, even in this, there’s a challenge. Jeremy Miller made [this
great point
recently](http://codebetter.com/blogs/jeremy.miller/archive/2009/04/07/talking-alt-net-with-james-avery.aspx "Talking ALT.NET"):

> We don't have our own story straight yet.  We're still advancing our
> craft.  By no means have we reached some sort of omega point in our
> own development efforts. 

In other words, even with community involvement, you’re probably going
to piss someone off. But avoiding piss is not really the point anyways
(though it’s much preferred to the alternative). The point is to be a
participant in advancing the craft alongside the larger community.
Others might disagree with some of your design decisions, but hopefully
they can see that your code is well considered via your involvement with
the community in the design process.

This also helps in avoiding the perception of arrogance, a fault that
some feel is the root cause of why some of our sample apps are of poor
quality. Any involvement with the community will help make it very clear
that there’s much to learn from the community just as there is much to
teach.

While I think getting community involved is important, I’m still on the
fence on whether it must happen *before* its published. After all, isn’t
publishing code a means of getting community involvement in the first
place? As Dare
[says](http://twitter.com/Carnage4Life/status/1534037187 "Dare on twitter"):

> getting real feedback from customers by shipping is more valuable than
> any amount of talking to or about them beforehand

Personally, I would love for there to be a way for teams to feel free to
post *samples* (using the definition I wrote), without fear of
misconstrued intent and bad usage. Ideally in a manner where it’s clear
that the code is not meant for cut and paste into real apps.

Can we figure out a way to post code samples that are not yet the
embodiment of good practices in a responsible manner with the intent to
improve the code quality based on community feedback? Is this even a
worthy goal or should Microsoft samples just get it right the first
time, as mentioned before, or don’t post at all?

Perhaps both of those are pipe dreams. I’m definitely interested in
hearing your thoughts. :)

Another question I struggle with is what causes people to not
distinguish between reference apps and sample apps? Is there no
distinction to make? Or is this a perception problem that can be
corrected with a concerted effort to make such labels consistently
applied, perhaps? Or via some other means.

As you can see, I have my own preconceived notions about those things,
but I’m putting them out there and challenging them based on what I’ve
read recently. Please do comment and let me know your thoughts.

