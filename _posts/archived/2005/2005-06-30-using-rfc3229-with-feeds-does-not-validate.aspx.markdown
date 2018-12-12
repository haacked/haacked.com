---
title: Using RFC3229 with Feeds Does Not Validate
date: 2005-06-30 -0800
disqus_identifier: 7415
categories: []
redirect_from: "/archive/2005/06/29/using-rfc3229-with-feeds-does-not-validate.aspx/"
---

In an effort to cut down on the bandwidth used by RSS feeds,
[Subtext](http://subtextproject.com/) implements the [RFC3229 for
feeds](http://bobwyman.pubsub.com/main/2004/09/using_rfc3229_w.html) as
proposed by [Bob Wyman](http://bobwyman.pubsub.com/).

Unfortunately, [FeedValidator.org](http://feedvalidator.org/) now says
[my feed](https://haacked.com/Rss.aspx) is not valid. The reason is that
Feed Validator assumes that the HTTP Status code of `226 IM Used` is an
error. Perhaps it does not yet recognize RFC3229.

