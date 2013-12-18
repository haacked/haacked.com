---
layout: post
title: "Ideas To Reduce Comment Spam"
date: 2004-06-04 -0800
comments: true
disqus_identifier: 529
categories: []
---
In an email to [Ian Griffiths](http://www.interact-sw.co.uk/iangblog/) I
mentioned that I wished he had a comments section because some of his
posts are so intriguing I have to reply. ;) His reply relayed a common
angst regarding enabling comments on a blog, comment spam

Looking around, I see this is a common problem as evidenced by the
following posts by [Roy
Osherove](http://weblogs.asp.net/rosherove/archive/2004/05/22/138856.aspx)
who wants to turn comments off, [Chris
Anderson](http://www.simplegeek.com/permalink.aspx/a12905a5-a839-44ec-8275-8ec605fd4405)
who threatens to turn them off, and [John
Lam](http://www.iunknown.com/000441.html) who did turn them off.

This is disheartening because comments can be a vital part of a blog
encouraging lively and insightful conversation. But then again, not if
your constantly getting these type of comments

> Noticed on a dirty white van, letters made by hand: \
>  "I Wish My Wife Was As Dirty As This." \
>  Underneath, different style: \
>  "She Is!"

Funny? Maybe. But off-topic. Unlike the garden variety email spam, the
bulk of comment spam tends not to be automated. If it were, it'd be
plenty easy to stop by requiring users to type in some text they see in
an image.

Rather, much of the smelly meat is due to the fact Google is bringing
droves and droves of visitors to blogs as bloggers all link to each
other. Some of these unwashed masses decide to leave their mark on your
site.

As [John Lam](http://www.iunknown.com/000438.html) pointed out, simple
IP filtering isn't enough. I've been thinking alot about how to leverage
network effects to reduce comment spam. For example, in general I'll
trust people who have subscribed to my blog to make comments, and if
they've been subscribed a while, I'll trust those that subscribe to
theirs.

I can imagine adding features to blogging back-ends such as .TEXT or
DasBlog whereby trust relationships can be built by using something
similar to the TrackBack API. Suppose I subscribe to your blog and you
try to make a comment on my site. Since my blog knows that I am
subscribed to yours (this will require aggregator integration), it
automatically lets you comment. It then can go one step further. Perhaps
it will ask you, "Any changes to your whitelist since we last exchanged
data?". We can then exchange whitelist info. Certain spam engines work
in this manner.

The big problem with this approach is that identity is a tough nut to
crack without requiring that commenters create a login and password and
building in some sort of verification system.

