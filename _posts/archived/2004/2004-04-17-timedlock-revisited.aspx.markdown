---
title: TimedLock revisited
tags: [dotnet,code,concurrency]
redirect_from: "/archive/2004/04/16/timedlock-revisited.aspx/"
---

In an earlier [blog entry](https://haacked.com/archive/2004/03/26/lock-statement-with-timeout.aspx/),
I asked the question if it made sense to add code in a debug version of
the TimedLock class (written by Ian Griffiths in [**this
post**](http://www.interact-sw.co.uk/iangblog/2004/03/23/locking) and
commented on by Eric Gunnerson in [**this
post**](http://blogs.msdn.com/ericgu/archive/2004/03/24/95743.aspx)) to
store the stack trace when acquiring a lock on an object so that if
another thread blocks an attempt to acquire a TimedLock, we can discover
the StackTrace of the blocking thread.

Well I stopped asking questions and started writing answers. I update
the TimedLock class with stack trace tracking and also wrote an NUnit
test that demonstrates the fact that we can identify the stack trace. 
Check out the source code [in the TimedLock repository](https://github.com/Haacked/TimedLock/).

Please keep in mind, this is meant to be a DEBUG version. In order to
store the stack trace, I place it and the object being locked into a
static hash table. In doing so, I acquire a lock on the hash table which
can hinder overall concurrency as it is a static member. Hopefully, this
will still be useful for tracking a pesky deadlock issue. I haven't done
any serious analysis or testing yet, so I welcome your comments if I'm
way off base.

