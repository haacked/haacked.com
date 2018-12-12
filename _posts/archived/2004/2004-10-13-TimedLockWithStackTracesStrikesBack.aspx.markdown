---
title: TimedLock with Stack Traces Strikes Back
date: 2004-10-13 -0800
disqus_identifier: 1341
categories:
- code
redirect_from: "/archive/2004/10/12/TimedLockWithStackTracesStrikesBack.aspx/"
---

By now you probably think I have an unhealthy obsession over the
`TimedLock` struct. Well, you're right. I think it's emblematic of the
right way to do things and shows that the right way isn't always the
easiest way.

In Ian's [last
post](http://www.interact-sw.co.uk/iangblog/2004/05/12/timedlockstacktrace)
on the TimedLock, he talked a bit about the performance considerations
with my [solution](https://haacked.com/archive/2004/05/12/timedlock_yet_again_revisited.aspx/) to
keeping track of stack traces in a multi-threaded situation. To sum, my
solution is pokey in certain situations. As always, measure measure
measure.

However, Ian mentioned a solution outlined by Marek Malowidzki that
avoids creating a stack trace on every lock acquisition. Instead, he
only stores the stack trace when a lock timeout occurs, thus avoiding
the performance penalty of my implementation. Unfortunately, there's no
source code posted for examination.

So I decided to implement Marek's solution based on Ian's write up. As
Ian mentioned, it would probably be best if the blocking thread didn't
throw an exception, but logged diagnostic information if it detects that
some other thread timed out while trying to acquire a lock on the
target. I put a very helpful `//TODO:` right where that should happen
since everyone has their own preferred logging framework.

As stated in the write-up, when thread fails to acquire a lock, it adds
the object it was trying to lock to a Hashtable as a key with a NULL
value. When the blocking thread is about to exit the synchronization
block, it checks this hash table and if it finds the object it is
locking in there, it will set the value for the Hashtable item as its
own stack trace.

Thus if you catch the LockTimeoutException, you can have it try to
obtain the stack trace from the Hashtable (supplying a wait since it
might not be there immediately). However, there's one potential problem
here. If you don't remove that object from the Hashtable after you've
looked at it, the next time you lock on that object and then release it,
your blocking thread will think an error occured and log some diagnostic
information. This isn't too bad since it doesn't cause a
`LockTimeoutException` to be thrown.

One thought I had was to check the Hashtable when I first successfully
acquire the lock on a target and remove the target if its already there.
However, that's not safe as it's quite possible that another thread
failed after the blocking thread acquired the lock but before it
examines the Hashtable.

Instead, when you call `GetBlockingStackTrace`, it retrieves the stack
trace from the Hashtable, stores a reference locally, and then removes
it from the Hashtable.

In any case, I've posted the source code
in my [TimedLock repository on GitHub](https://github.com/Haacked/TimedLock/).

DISCLAIMER: The code to keep track of stack traces is designed for
debugging only and is turned on by setting the conditional compilation
variable \#DEBUG = true. I make no guarantees and pity the fool who
deploys this version of TimedLock.cs in a production system with \#DEBUG
= true. Please let me know if you see any problems with this
implementation. So far it passes my tiny suite of unit tests.

UPDATE: I accidentally linked to the old `TimedLock`. The link above has
been updated.

UPDATE \#2: [Eric](http://www.randomtree.org/eric/techblog/) discovered
a subtle bug in my older implementation of `TimedLock` which he [describes
here](http://www.randomtree.org/eric/techblog/archives/2004/10/multithreading_is_hard.html).
Nice work in tracking that down!

