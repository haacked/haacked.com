---
title: Drinking From an RSS Fire Hose
tags: [rss]
redirect_from:
  - "/archive/2005/03/22/Drinking_From_An_Rss_Firehose.aspx/"
  - "/archive/2005/03/24/2450.aspx/"
---

![Firehose](/assets/images/firehose.jpg) So now that you’ve subscribed to your
4000 feeds, how do you keep on top of the flood of incoming items? Dare
[talks about
this](http://www.25hoursaday.com/weblog/PermaLink.aspx?guid=14d0413e-d0dc-4382-9ee9-57e95d7b3544 "Nightcrawler Thoughts"),
“the attention problem”, that faces power users of RSS (and ATOM)
aggregators such as [RSS Bandit](http://www.rssbandit.org/).

> Ideally a user should be able to tell a client, “Here are the sites
> I’m interested in, here are the topics I’m interested in, and now only
> show me stuff I’d find interesting or important”. This is the next
> frontier of features for RSS/ATOM aggregators and an area I plan to
> invest a significant amount of time in for the next version of RSS
> Bandit.

One way to think of it is that there’s a cacophony of content out there.
You want an automated system to filter out the noise and allow through
the music.

There are several difficulties inherent in any automated system designed
to filter content based on your tastes and preferences. **Often times,
you don’t really know what you like till you see it.** So how does the
automated filter know if you’re going to like something or not? Well,
you can train it by rating items. You can perhaps incorporate ratings of
others. You can build rules about which content you like and dislike.

All of these methods run into the problem that your likes and dislikes
tend to evolve and change over time as a product of your life’s
experience and automated filters tend to narrow the items they allow
through. If you set up hard rules for filtering data, you need to make
sure to change them over time. A constant task of tweaking. If you’re
using a system that requires you to train it based on an initial set of
sample data, you have to make sure the training set is not to small or
narrow. Otherwise the filter will only bring in items that meet some
narrow facet of your personality. Collaborative filters are particularly
prone to this problem. Think of how drab a lot of music on your
mega-radio stations are today, a result of a gigantic collaborative
filter. **I doubt I could train a human to filter items for myself, much
less an automated system.**

This is not to say that filters can’t do a half-way decent job of being
a personal editor. They can. The point here is that a really good filter
has to allow a bit of noise through. My favorite radio station right now
is KCRW, especially the show Metropolis. I don’t necessarily like
everything Jason Bently spins, but I’m constantly being introduced to
music that I’ve never heard of that I end up really enjoying. I believe
that’s a result of a filtering system (human DJ) that allows some bit of
noise through in order to expose listeners to new items.

**Some noise is essential to a good content filtration system.**

In any case, the effort to add filtration to RSS Bandit is of particular
interest to me. Having studied Bayes theorem and read up a small amount
on autonomous agent systems, I’m really excited about the potential for
intelligent filtering in RSS Bandit. In particular, one method of
filtering I like is creating de-facto editors via assignment. For
example, like Dare, I read everything by [Don
Box](http://pluralsight.com/blogs/dbox/). So I might assign a rule that
always includes items by Don. But I might also go a step further and
think that anything Don links to will probably be of interest to me, so
I might state that everything he links to should be subscribed to
automatically (perhaps based on my filtering rules based on how much I
trust Don). Effectively, Don becomes an editor for my aggregator
(without even knowing so). He becomes a content DJ.

