---
title: TimedLock Yet Again Revisited...
tags: [dotnet,code,concurrency,dispose]
redirect_from: "/archive/2004/05/11/timedlock_yet_again_revisited.aspx/"
---

![lock](/assets/images/lock.jpg) In an [earlier
post](/archive/2004/04/17/timedlock-revisited.aspx), I updated the
TimedLock class (first introduced in [this
post](http://www.interact-sw.co.uk/iangblog/2004/03/23/locking)) to
allow the user to examine the stack trace of the thread that is holding
the lock to an object when the TimedLock fails to obtain a lock on that
object. This assumes that the blocking lock was obtained using the
TimedLock. Ian Griffiths pointed out a few flaws in my implementation
and I promised I would incorporate his feedback and revise the code.

Since that time, Ian [revisited the
TimedLock](http://www.interact-sw.co.uk/iangblog/2004/04/26/yetmoretimedlocking)
based on comments he received and changed it to be a struct in both
Debug and Release versions. He adds a new Sentinel class in the debug
version. The finalizer in the Sentinal is used to detect whether or not
the user of the TimedLock remembered to call Dispose. I've incorporated
his new changes as well as his comments and have released my newest
TimedLock struct.

I posted the code in my [TimedLock repository on GitHub](https://github.com/Haacked/TimedLock/).

As Ian points out, there are non-trivial costs involved in keeping track
of the stack trace of every lock just in case we wish to examine it
later. When I have some non-trivial free time, I'd like to examine other
possibilities.

