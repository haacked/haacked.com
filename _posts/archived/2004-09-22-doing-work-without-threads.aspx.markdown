---
layout: post
title: Doing Work Without Threads
date: 2004-09-22 -0800
comments: true
disqus_identifier: 1255
categories: []
redirect_from: "/archive/2004/09/21/doing-work-without-threads.aspx/"
---

A while ago I wrote up a [post on Asynchronous
Sockets](http://haacked.com/archive/2004/08/06/882.aspx).
[Ian](http://www.interact-sw.co.uk/iangblog/) was kind enough to send me
an email correcting a few [niggles with
it](http://haacked.com/archive/0001/01/01/895.aspx) and in an email
exchange, cleared up a few other misconceptions about how sockets (and
other IO operations for that matter) really work.

Well now he posts a great article that points out that a program doesn't
always use a thread to perform some work.

> There seems to be a popular notion that in order for a program to
> perform an operation, it must have a thread with which to do it. This
> is not always the case. Often, the only points at which you need a
> thread are at the start and end of the operation....

This is recommended reading.

Read [the rest
here](http://www.interact-sw.co.uk/iangblog/2004/09/23/threadless)

