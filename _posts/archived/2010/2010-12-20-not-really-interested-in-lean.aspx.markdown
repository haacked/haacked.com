---
title: Not Really Interested In Lean
tags: [code,nuget]
redirect_from: "/archive/2010/12/19/not-really-interested-in-lean.aspx/"
---

*We could have done better.* That’s the thought that goes through my
mind when looking back on this past year and reflecting on NuGet.

Overall, I think we did pretty well with the product. Nobody died from
using it, we received a lot of positive feedback, and users seem
genuinely happy to use the product. So why start off with a negative
review?

It’s just my way. If you can’t look back on every project you do and say
to yourself “I could have done better”, then you weren’t paying
attention and you weren’t learning. For example, why stop at [double
rainbows](http://www.youtube.com/watch?v=OQSNhk5ICTI "Double Rainbow (All the Way)")
when we could have gone for triple?

[![leaning](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Not-Really-Interested-In-Lean_10556/leaning_f2e193c8-335d-4239-8725-a03596790c3c.jpg "leaning")](http://www.flickr.com/photos/hern42/4606409286/ "Leaning Tower of Pisa by hem42 (http://creativecommons.org/licenses/by-sa/2.0/deed.en)")

When starting out on NuGet, we hoped to accomplish even more in our
first full release. Like many projects, we have iteration milestones
which each culminate in a public release. Ours tended to be around two
months in duration, though our last one was one month.

Because we were a bit short staffed in the QA department, at the end of
each milestone our one lone QA person, [Drew
Miller](http://half-ogre.com/ "Drew Miller's Blog"), would work like,
well, a mad Ninja on fire trying to verify all the fixed bugs and test
implemented features. Keep in mind that the developers do test out their
own code and write unit tests before checking the code in, but it’s
still important to manually test code with an eye towards thinking like
a user of the software.

This my friends, does not scale.

When we look back on this past year, we came to the conclusion that our
current model was not working out as well as it could. We weren’t
achieving the level of quality that we would have liked and continuing
in this fashion would burn out Drew.

I came to the realization that we need to assume we’ll never be fully
staffed on the QA side. Given this, it became obvious that we need a new
approach.

This was apparent to the developers too. [David
Fowler](http://weblogs.asp.net/davidfowler/ "David Fowler's blog") noted
to me that we needed to have features tested closer to when they were
implemented. As we discussed this, I remember a radical notion that Drew
told me about when he first joined our QA team. He told me that he wants
to eliminate dedicated testers. Not actually kill them mind you, just
get rid of the position.

An odd stance for someone who is joining the QA profession. But as he
explained it to me in more detail over time, it started to make more
sense. In the most effective place he worked, every developer was
responsible for testing. After implementing a feature and unit testing
it (both manually and via automated tests), the developer would call
over another developer and fully test the feature end-to-end as a pair.
So it wasn’t that there was no QA there, it was that QA was merely a
role that every developer would pitch in to help out with. In other
words, everyone on the team is responsible for QA.

So as we were discussing these concepts recently, something clicked in
my thick skull. They echoed some of the concepts I learned [attending a
fantastic
presentation](https://haacked.com/archive/2009/06/28/ndc2009-trip-report.aspx "NDC 2009 Trip report")
back in 2009 at the Norwegian Developer’s Conference by [Mary
Poppendieck](http://www.poppendieck.com/ "Mary Poppendieck's Website").
Her set of talks focused on the concept of a problem solving
organization and the principles of Lean. She gave a fascinating account
of how the Empire State Building finished in around a year and under
budget by employing principles that became known as Lean. I remember
thinking to myself that I would love to learn more about this and how to
apply it at work.

Well fast forward a year and I think the time is right. Earlier in the
year, I had discussed much more conservative changes we could make. But
in many ways, by being an external open source project with a team open
to trying new ideas out, the NuGet team is well positioned to try out
something different than we’ve done before as an experiment. We gave
ourselves around two months starting in January with this new approach
and we’ll evaluate it at the end of those two months to see how it’s
working for us.

We decided to go with an approach where each feature was itself a
micro-iteration. In other words, a feature was not considered “done”
until it was fully done and able to be shipped.

So if I am a developer working on a feature, I don’t get to write a few
tests, implement the feature, try it out a bit, check it in, and move on
to the next feature. Instead, developers need to call over Drew or
another available developer and pair test the feature end-to-end. Once
the feature is fully tested, only then does it get checked into the main
branch of our main fork.

Note that under this model, every developer also wears the QA hat
throughout the development cycle. This allows us to scale out testing
whether we have two dedicated QA, one dedicated QA, or even zero. You’ll
notice we’re still planning to keep Drew as a dedicated QA person while
we experiment with this new approach so that he can help guide the
overall QA process and look at system level testing that might slip by
the pair testing. Over time, we really want to get to a point where most
of our QA effort is spent in preventing defects in the first place, not
just finding them.

Once a feature has been pair tested, that feature should be in a state
that it can be shipped publicly, if we so choose.

We’re also planning to have a team iteration meeting every two weeks
where we demonstrate the features that we implemented in the past two
weeks. This serves both to review the overall user experience of the
features as well as to ensure that everyone on the team is aware of what
we implemented.

You’ll note that I’m careful not to call what we’re doing “Lean” with a
capital “L”. Drew cautioned me to user lower-case “lean” as opposed to
capital “Lean” because he wasn’t familiar with Lean when he worked this
model at his previous company. We wouldn’t want to tarnish the good name
of Lean with our own misconceptions about what it is.

This is where I have to confess something to you. Honestly, I’m not
really that interested in Lean. What I’m really interested in is getting
better results. It just seems to me that the principles of Lean are a
very good approach to achieving those results in our situation.

I’m not one to believe in one true method of software development that
works for all situations. What works for the start-up doesn’t work for
the Space Shuttle and vice versa. But from what I understand, NuGet
seems to be a great candidate for gaining benefits from applying lean
principles.

So when I said I’m not interested in Lean, yeah, that was a bit of a
fib. I definitely am interested in learning more about Lean (and I
imagine I’ll learn a lot from many of you). But I am so much more
interested on the better results we hope to achieve by applying lean
principles.

