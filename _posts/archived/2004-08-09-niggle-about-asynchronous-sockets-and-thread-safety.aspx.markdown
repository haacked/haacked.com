---
layout: post
title: A Niggle or Two About Asynchronous Sockets And Thread Safety
date: 2004-08-09 -0800
comments: true
disqus_identifier: 895
categories:
- code
redirect_from: "/archive/2004/08/08/niggle-about-asynchronous-sockets-and-thread-safety.aspx/"
---

[Ian Griffiths](http://www.interact-sw.co.uk/iangblog/) finds a
[niggle](http://dictionary.reference.com/search?q=niggle) about my
[post](http://haacked.com/archive/2004/08/06/882.aspx) on sockets.

This may surprise a few friends of mine who regard me as a "human
dictionary", but I had to look up the word "niggle". Apparently only the
"human" part of the appellation applies. I've apparently fooled them by
reading a lot of sci-fi fantasy and choosing to learn and use
"impressive" words such as Bacchanalian in everyday conversation ("I
wrote this code in a drunken stupor from a bacchanalian display of
excessive beer drinking."). It's really all smoke and mirrors. But I
digress...

His comment is quite insightful and well worth repeating here in full.

> One minor niggle with this code... \
>  \
>  Although the example is correct as it stands, it doesn't mention an
> important issue: the Socket class is not thread-safe. This means that
> if you do use the async operations (and by the way, I'm completely
> with you here - I'm a big fan of the async operations) you need to
> take steps to synchronize access to the socket. \
>  \
>  As it stands there's nothing wrong with this example as far as I can
> see. But what if you also have an asynchronous read operation
> outstanding? Can you guarantee that a read and a send won't complete
> simultaneously, and that you'll be trying to access the socket from
> both completion handlers simultaneously. \
>  \
>  So in practice, you tend to want to use some kind of locking to
> guarantee that your socket is only being used from one thread at a
> time, once you start using async socket IO. \
>  \
>  (Also, you left out one of the clever parts of IO completion ports -
> the scheduler tracks which threads are associated with work from an IO
> port, and tries to make sure that you have exactly as many running as
> you have CPUs. If one of the threads handling work from an IO
> completion port blocks, the OS will release another work item from the
> completion port. Conversely, if loads of IO operations complete
> simultaneously, it only lets them out of the completion port as fast
> as your system can handle them, and no faster - this avoids swamping
> the scheduler under high load.)

I have to say, Ian's depth of knowledge on such topics (or nearly any
geek topic) never ceases to impress me. Fortunately for my app, the
client socket only receives data every three seconds and never sends
data back to the remotely connected socket (how boring, I know). In any
case, I will double check that I am synchronizing access to the socket
just in case. Perhaps I'll use the TimedLock to do that. ;)

While we're in the business of finding niggles (Ian, you've hooked me on
this word. For some strange reason, I can't stop saying it) I should
also point out that IO Completion ports awaken threads from the
ThreadPool in order to perform an asynchronouse action. The entire
asynchronous invocation model of .NET is built on the ThreadPool.
Remember that the next time you call a method that starts with "Begin"
such as "BeginInvoke". Chances are, it's using a thread from the
ThreadPool (especially if its a framework method. I'll make no
guarantees for methods written by your coworkers.)"

By default, the max threadcount for the ThreadPool is 25 per processor.
In my application, the remote socket sends short packets of data on a
regular interval, so the threads that handle the received data are very
short lived. Sounds like an ideal use of the ThreadPool doesn't it?
However, if I were expecting a huge number of simultaneous connections,
I might look into changing the machine.config file to support more than
25 ThreadPool threads per processor. Before making any such change,
measure measure measure.

If you have a situation where the operations on the data are long lived,
you might consider spawning a full-fledged thread to handle the remote
client communications and operations. Long running operations aren't
necessarily the best place to use a thread from the .NET built in
ThreadPool.

