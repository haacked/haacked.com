---
title: rel=&quot;nofollow&quot; Fix Applied
date: 2005-01-19 -0800
tags: [blogging,web]
redirect_from: "/archive/2005/01/18/relnofollow-fix-applied.aspx/"
---

I applied a patch to my .Text installation as recommended by [Scott
Watermasysk](http://scottwater.com/blog/) in [this
entry](http://scottwater.com/blog/archive/2005/01/19/rel_nofollow_quickchange)
of his blog.

New comments will now have the *rel="nofollow"* attribute applied, thus
[preventing Google (and others) from indexing the
link](http://www.google.com/googleblog/2005/01/preventing-comment-spam.html)
and giving the comment spammers a higher page rank.

I found a slight problem with the patch. It works for URLs within the
body of the comment but if the user specifies a URL in the URL field, it
doesn't modify that URL. Thus you can still comment spam me, but only
one URL at a time. I posted a comment in Scott's blog about this.

In any case, I doubt this will stop the comment spam anyways. It may
well be good enough for the spammers to continue. Despite the fact that
their Google page rankings won't increase as a result, by spamming
enough sites, they'll get enough exposure on enough blogs (et all) that
enough users will click through. It's the same way with email spam. All
it takes is a very small percentage of suckers to bite.

This does take away one of the key motivators to comment spam. I will
probably add a CAPTHA tool later after some investigation.

