---
title: P2P RSS and the Return of Push!
tags: [rss,blogging]
redirect_from:
  - "/archive/2004/04/28/p2p-rss-and-the-return-of-push.aspx/"
  - "/archive/2004/04/30/387.aspx/"
---

![Wired Magazine May 2004](/assets/images/cover_wired_190.jpg) I just finished
reading an essay in the latest edition of Wired by Gary Wolf entitled
"The Return Of Push!" discussing whether or not RSS will realize his
prediction made seven years ago. His prediction in Wired 5.03 was that
web browsers were about to become obsolete due to push technologies such
as PointCast.

Instead, browsers flourished while PointCast whithered. In the essay, he
mentions a point that Meg Hourihan, cofounder of Pyra Labs (think
Blogger), makes that RSS depends on a "polling" system which she feels
is inadequate.

> Can you imagine 1 million news readers all checking 300-plus sites
> every 15 minutes? Or even every hour? It's no horribly inneficient.

She hopes to see some sort of peer-to-peer solution.

At the moment (and perhaps due to ignorance), I'm skeptical that this is
a serious problem for several reasons: 1. It is unlikely that these
millions of aggregators will all hit the same site at the same time. The
traffic patterns are more predictable and steady than what one sees when
a news story breaks on a news site. 2. Many RSS aggregators such as RSS
Bandit and web sites implement conditional GET requests so that new
content is only downloaded when there's something new to get. 3. The
actual RSS feed is something that is very static in the sense that it is
updated rarely and there's no need to dynamically personalize the
content (in general). Thus, it is possible for blogging tools to
generate a static RSS file after updates. Static files are served quite
efficiently by a web server.

Having said that, the idea of merging P2P with something like RSS Bandit
is intriguing. I'm about to throw out some crazy ideas here. The good
ones are the result of my evil genius, and the bad ones are due to the
muscle relaxants I'm on and the late hour.

If the polling system truly becomes inefficient, perhaps a technology
like BitTorrent can be incorporated. In such a situation, I wouldn't
necessarily get feed updates from the source, but rather get it from a
P2P network.

In terms of improving the social networking aspects, how about tighter
integration with blogging tools. For example, in a very narcissistic
fashion, I subscribe to my own feed. Suppose I could configure RSS
Bandit with my user name and password to my .TEXT blog so that when
someone posts a comment on my blog via RSS Bandit, our aggregators would
make a P2P connection and the comment would appear as an IM within RSS
Bandit (or MSN Messenger... who knows?). Then I could reply to the
comment immediately and the ensuing conversation might appear in the
comments section of the blog.

Perhaps when I first get online, RSS Bandit would give my own blog
special treatment so that I can automatically see all new comments on my
own blog.

