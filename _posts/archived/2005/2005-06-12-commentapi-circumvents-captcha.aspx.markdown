---
title: CommentAPI Circumvents CAPTCHA
date: 2005-06-12 -0800
disqus_identifier: 4659
categories: []
redirect_from: "/archive/2005/06/11/commentapi-circumvents-captcha.aspx/"
---

Just so we’re all clear about this, the convenience of the
[CommentAPI](http://wellformedweb.org/story/9 "CommentAPI"), that nifty
little service that allows users to make comments to your blog from the
comfort of their favorite [RSS aggregator](http://rssbandit.org/), comes
at a cost. Enabling the CommentAPI supplies a back door for comment
spammers who want to bypass the CAPTCHA guard posted at the front door.

I was just chatting with [Andrew](http://andrewconnell.com/blog/) about
this and we realized it would be quite easy to add CAPTCHA support to
the CommentAPI if we could get both RSS Aggregator developers and blog
engine developers to agree on how to update to the CommentAPI to support
a CAPTCHA image url or a CAPTCHA text question. The RSS Aggregator would
then display this image or text, and provide the user a field in the
comment dialog to supply the answer to the CAPTCHA challenge, which the
CommentAPI would validate with the CAPTCHA control. Of course this
wouldn’t close the CAPTCHA backdoor for Trackbacks and Pingbacks.

In the meantime, I tend to favor non-CAPTCHA approaches to comment spam
filtering for this very reason. I want to fight comment spam tooth and
nail with every resource I have before I turn off the CommentAPI on my
blog. Likewise, I still support Trackbacks because I personally have
found them more beneficial than detrimental so far.

In any case, Subtext will provide configuration options to turn each of
these services on or off individually so that users have full control of
comment entry points.

