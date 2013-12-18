---
layout: post
title: "PageRank in Decline.  Is it Nofollow's Fault?"
date: 2005-08-24 -0800
comments: true
disqus_identifier: 9646
categories: []
---
My PageRank has been in decline lately. I was as high as a five, but
just checked and am now down to three. Was it something I said? Or is
this the result of `rel="nofollow"`?

My guess is that it’s a little bit of both. It seems that this has been
implemented far and wide, but in such a manner as taking a sledgehammer
to pound in a nail.

Hopefully I can correct this in Subtext. First, I need to make sure that
`rel="nofollow"` can be turned off and on easily in Subtext. I really
don’t need it since I delete comments left on my blog almost
immediately.

Better yet is to have Subtext render the `rel="nofollow"` in the
attributes of comments for a short period of time (configurable of
course). After that period is over, the `rel="nofollow"` is removed. By
then you should have surely deleted the comment. That way we can all
spread the Google Juice around.

If that doesn’t help, then I’ll start link whoring. Hello? Link to me!

UPDATE: My blog’s homepage is ranked three, but [my
archives](http://haacked.com/Archives.aspx) are ranked five. Odd.

