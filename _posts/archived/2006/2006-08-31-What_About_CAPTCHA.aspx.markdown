---
title: What About CAPTCHA?
date: 2006-08-31 -0800
disqus_identifier: 16219
tags: []
redirect_from: "/archive/2006/08/30/What_About_CAPTCHA.aspx/"
---

I mentioned several heuristic approachs to blocking spam in my recent
post on [blocking comment
spam](https://haacked.com/archive/2006/08/29/Comment_Spam_Heuristics.aspx),
but commenters note that I failed to mention CAPTCHA (**C**ompletely
**A**utomated **P**ublic **T**uring test to tell **C**omputers and
**H**umans **A**part).  At the moment,
[CAPTCHA](http://en.wikipedia.org/wiki/Captcha "CAPTCHA on Wikipedia")
is quite effective, both at blocking spam and annoying users.

But I don’t have any real beef with CAPTCHA, apart from the
accessibility issues.  If I met CAPTCHA in a bar, I’d buy it a beer! *No
hard feelings despite the*[*trash
talking*](https://haacked.com/archive/2005/01/20/Image_Based_CAPTCHA_Losing_Appeal.aspx)*in
the past, right?*

There is successful code out there that can [break
CAPTCHA](https://haacked.com/archive/2005/01/31/Beating_CAPTCHA.aspx),
but that is pretty much true of every other method of blocking spam I’ve
mentioned.

The reason I didn’t mention CAPTCHA is that it would be ineffective for
me.  Much of my spam comes in via automated means such as a
[trackback](http://en.wikipedia.org/wiki/Trackback)/[pingback](http://en.wikipedia.org/wiki/Pingback)
.  The whole point of a trackback is to allow another **computer** to
post a comment on my site.  So telling a computer apart from a human in
that situation is pointless.

And at the moment, the [Comment API](http://wellformedweb.org/story/9)
has no support for CAPTCHA.  If comment spam isn’t coming in via the
comment api now, it is only a matter of time before it is the primary
source of comments.

So while I believe CAPTCHA is effective and may well be for a good while
until comment spammers catch up, I would like to look one step ahead and
focus on heuristics that can salvage the use of trackbacks and the
Comment API. 

