---
title: TimedLock Success Story!
date: 2004-08-06 -0800
tags: []
redirect_from: "/archive/2004/08/05/timedlock-success-story.aspx/"
---

This seems to be my favorite geek subject, but I have to tell you a
success story using [Ian Griffith's Timed
Lock](http://www.interact-sw.co.uk/iangblog/2004/03/23/locking) struct
with my [enhancement](https://haacked.com/archive/2004/05/12/timedlock_yet_again_revisited.aspx/).

To recap, when you fail to acquire a lock on an object because another
thread already has one, my enhancement allows you to see the stack trace
of the blocking thread. Well the other day, I was running a suite of
unit tests against a socket server I was building when one of the tests
failed with a ThreadTimeoutException. Looking at the stack trace, I
found the line of code where another thread was unnecessarily holding a
lock on the object. I used to spend a lot of time poring through logs
trying to decipher threading issues such as this.

