---
title: More on Terminating Threads and the depravity of Thread.Abort
date: 2004-11-13 -0800 9:00 AM
tags: [concurrency,dotnet]
redirect_from: "/archive/2004/11/12/the-depravity-of-thread-abort.aspx/"
---

In response to [Ian’s
post](http://www.interact-sw.co.uk/iangblog/2004/11/12/cancellation "Ian Griffiths")
on thread.abort, [Richard
Blewett](http://www.dotnetconsult.co.uk/weblog/ "Richard Blewett's blog")
[points out a
situation](http://www.dotnetconsult.co.uk/weblog/permalink.aspx/4f52c396-1b0d-4419-8871-6ca6992460ca "Ian on Thread.Abort and a Comment")
when the thread you are attempting to cancel can’t check the volatile
book flag to determine whether it should cancel itself or not.

An example he presents is when the thread is waiting on a
synchronization primitive. The solution given is to call
Thread.Interrupt.

This is a handy technique when you have a reference to the thread you
wish to cancel, but this is not often the case when dealing with
asynchronous method calls such as spawned by calling `BeginInvoke`. You
won’t have a reference to the thread that an asynchronous method call is
operating on.

So what is the would be thread terminator to do? Rather than go back in
time and stop the thread from being spawned in the first place (my
apologies for the poor cinema reference), avoid having indefinite waits
on synchronization primitives in the first place. With a
`ManualResetEvent` for example, you can specify a timeout for the
WaitOne method. I recommend that you do so.

