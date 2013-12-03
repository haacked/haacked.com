---
layout: post
title: "An Even Better TimedLock"
date: 2004-05-12 -0800
comments: true
disqus_identifier: 436
categories: [code]
---
Wow! After posting my update to the TimedLock class entitled [TimedLock
Yet Again
Revisited...](/archive/2004/05/12/timedlock_yet_again_revisited.aspx),
[Ian Griffiths](http://www.interact-sw.co.uk/iangblog/) posts [this
gem](http://www.interact-sw.co.uk/iangblog/2004/05/12/timedlockstacktrace)
which outlines a solution to one of the problem's with my approach to
keeping a stack trace.

The problem is that my code acquires a stack trace every time it
acquires a lock just in case another thread fails to acquire a lock. The
purpose of this action is so that we can examine the stack trace of the
blocking thread to find out why we couldn't acquire a lock. This can be
a big performance cost in some situations.

Ian received the solution via an email from Marek Malowidzki. Marek, if
you're out there. I'd love to see the proof of concept code you wrote. I
won't rehash the explanation of the solution, but will mention that it
avoids creating and storing a StackTrace every time a lock is acquired,
and rather, finds a way to obtain the blocking thread's stack trace if
and only if another thread fails to acquire a lock. How? You have to
read the Ian's post to find out.

