---
layout: post
title: "[Mix06] The Value Of A Partial Message With Indigo"
date: 2006-03-23 -0800
comments: true
disqus_identifier: 12144
categories: []
redirect_from: "/archive/2006/03/22/Mix06TheValueOfAPartialMessageWithIndigo.aspx/"
---

I was involved in a 5 AM breakfast conversation (a little late night
snack before we all turned in for the evening) with [Clemens
Vasters](http://staff.newtelligence.net/clemensv/ "Clemens Vasters Blog"),
[Steve Maine](http://hyperthink.net/blog/ "Steve Maine's Blog"), and
some others at Mix06 in which they explained how streaming content works
with WCF (code named Indigo).

They pointed out (as I mentioned in a tongue-in-cheek context in [my
last
post](http://haacked.com/archive/2006/03/23/AndTheAwardToTheFunniestCommenterOnThisBlogGoesTo.aspx "Funniest Commenter"))
that with streaming content such as streaming videos, the consumer of
the media is really concerned about headers and start tags, because they
plan on using the content as it flows over the wire.

In effect, streaming works because WCF promises to send the end tags
eventually. Might be hours from now. Might even be years from now. But
they will get sent and the message (as everything is a message in WCF)
will be well formed and complete.

Clemens then pointed out that even if they never did send the end tags,
what would it matter? The consumer of the feed, if he or she is
exceedingly anal, could decide to throw an exception then. But at that
point, the content has already served its purpose and has been consumed.
Remember, we are talking about the streaming content use case.

This struck me with two thoughts. **This is a software scenario where
the intent is more important than the execution**. The fact is that they
*intend* to send the end tags is very important, but whether they
actually do or not is of less importance.

Secondly, **with streaming content, the valuable deliverable is not a
whole message but a partial message.** By *message* I mean the entire
content and whatever SOAP envelope it may be wrapped in.

Of course, the key to streaming content producers is to make sure to
send any meta-data and commercials at the beginning of the message and
not after the streaming part. But you knew that already. ;)

