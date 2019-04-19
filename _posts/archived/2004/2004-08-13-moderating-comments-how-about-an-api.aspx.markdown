---
title: Moderating Comments. How About An API?
date: 2004-08-13 -0800 9:00 AM
tags: [rss]
redirect_from: "/archive/2004/08/12/moderating-comments-how-about-an-api.aspx/"
---

I've heard a lot of complaints about what a b*tch moderating comments
turns out to be. So why not create an API for moderating comments?

Suppose your blog engine put all incoming comments in a private
authenticated RSS feed. You can then subscribe to this feed and for each
item, hit "YES" or "NO" via your RSS Aggregator. I'd be willing to write
an IBlogThis plug-in to support such an API if someone adds it to the
various blogging back-ends. If it caught on, I'd be happy to add it to
RSS Bandit if [Dare](http://www.25hoursaday.com/weblog/) and
[Torsten](http://www.rendelmann.info/blog/) like the idea and approved
it.

The API would probably be similar to the
[CommentAPI](http://wellformedweb.org/story/9) or my
[RatingAPI](https://haacked.com/archive/2004/04/24/359.aspx), but with a
few modifications specific to comments. I'll propose one later.

