---
layout: post
title: "Do Not Adjust Your Browser"
date: 2010-01-12 -0800
comments: true
disqus_identifier: 18676
categories: [subtext,code]
---
This blog is experiencing technical difficulties. Do not adjust your
browser.

Hi there. If you’ve tried to visit this blog recently you might have
noticed it’s been down a lot in the last two days. My apologies for
that, but hopefully you found what you needed via various online web
caches.

I’ve been dogfooding the latest version of Subtext and as CodingHorror
points out, [dogfood tastes
bad](http://www.codinghorror.com/blog/archives/000287.html "The Difficulty of Dogfooding").

I’ve done a lot of testing on my local box, but there are a class of
bugs that I’m only going to find on a high traffic real site, and boy
have I found them!

Some of them might be peculiar to my specific data or environment, but
others were due to assumptions I made that were wrong. For example, if
you use `ThreadPool.QueueUserWorkItem` to launch a task, and that task
throws an unhandled exception, that can bring your entire App Domain
down. Keep that in mind if you think to use that method for a
fire-and-forget style task.

In any case, the point of this post is to say that we’re not going to
release the next version of Subtext until it’s rock solid. My blog going
down occasionally is the cost I’m incurring in order to make sure the
next version of Subtext is a beast that won’t quit.

