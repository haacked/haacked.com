---
layout: post
title: "Bandwidth-gobbling RSS aggregators: foiled!"
date: 2004-12-08 -0800
comments: true
disqus_identifier: 1717
categories: []
---
This is great! Rather than wait for all the RSS Aggregators to properly
use the If-Modified-Since header, implement it on the server instead via
an IP address and User Agent combination. Now your first thought is
probably "Wait, that's not perfect. What about users of internet
providers such as AOL which uses a shared pool of IP Addresses?"

True, theoretically there could be an instance where you don't receive a
blog entry because your IP and User Agent string just happened to match
someone else. But really, how many AOLers are subscribing to RSS feeds
in the first place? RSS is still mostly in the domain of the more
technically sophisticated. Secondly (unfair cracks on AOL aside), the
chances that two users with the same IP and User Agent requesting your
pathetic little blog close enough together in time is probably very
slight.

UPDATE: A commenter lamented that users behind a corporate firewall will
lose out. This is a more likely scenario as your coworker is likely to
subscribe to the same blogs that you do. My solution is to only throttle
aggregators that misbehave (you know who you are). Or conversely, don't
throttle well behaved aggregators. This provides incentives for the
misbehaving aggregator developers to fix their aggregators. [RSS
Bandit](http://www.rssbandit.org/) is well behaved in this regard.

> **Xeni Jardin**: Last month, Cory posted an item about [Glenn
> Fleishman](http://blog.glennf.com)'s analysis of the impact of RSS
> aggregators on his blogs' bandwidth use.
> ([Link](http://www.boingboing.net/2004/11/14/badly_behaved_rss_re.html)
> to previous BoingBoing post). Now, Glenn updates us with this news:
>
> > I've run the latest statistics on RSS usage after adding a simple
> > throttling program that uses a database to track the last access by
> > an RSS aggregator (or anyone trying to retrieve a syndication file).
> > One retrieval per file update is now the limit. I've seen my
> > bandwidth use on RSS drop almost in half with no commensurate drop
> > in actual users, and only a single note describing a problem in
> > retrieving my feed (from a very old aggregator).
>
> [Link](http://blog.glennf.com/mtarchives/004540.html)

*[Via [Boing
Boing](http://www.boingboing.net/2004/12/07/bandwidthgobbling_rs.html)]*

