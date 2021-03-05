---
title: Putting a Crimp in the RSS Fire Hose
tags: [rss]
redirect_from: "/archive/2005/03/29/putting-a-crimp-in-the-rss-fire-hose.aspx/"
---

![fire hose](https://haacked.com/assets/images/firehose.jpg) In my post entitled
[Drinking From an RSS Fire Hose](https://haacked.com/archive/2005/03/23 /Drinking_From_An_Rss_Firehose.aspx/) I dealt with some
of the issues surrounding the flood of incoming RSS entries within an
RSS aggregator raised by Dare's post ["Nightcrawler Thoughts: Thums Up,
Thumbs Down and
Attention.xml"](http://www.25hoursaday.com/weblog/PermaLink.aspx?guid=14d0413e-d0dc-4382-9ee9-57e95d7b3544).

**The Keep It Simple Stupid Solution**
 Reading through some of the comments on both posts, I realize that for
a great majority of users, a very simple system will satisfy their
needs. One user mentioned that it'd be nice to be able to have items
with specific keywords automatically marked as read. This is great if
you're tired of hearing about, say, Paris Hilton. Add the keyword "Paris
Hilton" and no longer will you have to endure her name in your
aggregator.

**A Short Story**
 I started to get a buttload of comment spam on this blog recently. I
thought about using CAPTCHA, Bayesian Spam Filtering, etc... But in the
end, I simply added a trigger modified from [this
one](http://netnerds.net/articles/285.aspx) that simply blocks posts
with a certain number of link. This resulted in a dramatic decrease in
the number of posts about online poker and has been working quite well
for me. At some point, I'll probably need to employ more sophisticated
tactics, but for the time being, this simple rule works.

**Extensibily Model**
 Personally, I think the initial solution isn't a filter at all, but the
[extensibility model](https://haacked.com/archive/2005/03/03/building-a-better-extensibility-model-for-rss-bandit.aspx/)
prototyped by [Torsten](http://www.rendelmann.info/blog/PermaLink.aspx?guid=d3c8dfd5-c3f7-4e74-bdb0-0168eb4e2d82).

**Rules Engine**
 On top of this, I'd probably build a simple rules engine plug-in
similar to Outlook's. For example, you might create a keyword rule
associated with one of the following actions: Mark as Read, Flag For
Review, Give Priority, etc... As my short story above illustrates (see,
there was a point to it), a simple rules engine approach will often give
you the 80% of the 80/20 rule.

**The Goal**
 The goal with this approach is to get something to the users quickly
that will elicit feedback on what the pimped out
"baysesian/collaborative/neural networked/throw dart at dartboard"
filter should do. A collateral benefit is that users will inevitably
create their own plug-ins (we hope) and we have the option to take the
best ideas and integrate them as a first class feature.

[Listening to: So What'cha Want - Beastie Boys - Check Your Head (3:37)]

