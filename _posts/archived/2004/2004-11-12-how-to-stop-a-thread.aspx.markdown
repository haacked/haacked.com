---
title: How To Stop a Thread in .NET (and Why Thread.Abort is Evil)
date: 2004-11-12 -0800
tags: []
redirect_from: "/archive/2004/11/11/how-to-stop-a-thread.aspx/"
---

[Ian
Griffiths](http://www.interact-sw.co.uk/iangblog/ "Ian Griffiths’ Blog")
(one of my favorite tech bloggers) wrote this [fine
piece](http://www.interact-sw.co.uk/iangblog/2004/11/12/cancellation "How To Stop a Thread in .NET (and Why Thread.Abort is Evil)")
on why `Thread.Abort` is a representation of all that is evil and
threatens the American (and British) way of life.

> The problem with `Thread.Abort` is that it can interrupt the progress
> of the target thread at any point. It does so by raising an
> ’asynchronous’ exception, an exception that could emerge at more or
> less any point in your program. (This has nothing to do with the .NET
> async pattern by the way - that’s about doing work without hogging the
> thread that started the work.)

If you’re interested in how `Thread.Abort` raises an exception in
another thread, read [Chris
Sells’](http://www.sellsbrothers.com/ "Chris Sells' Blog") (another
favorite blogger) [investigative report
here](http://www.ondotnet.com/pub/a/dotnet/2003/02/18/threadabort.html "Plumbing the Depths of the ThreadAbortException using Rotor").

I’ve taken this to heart in the design of my Socket server class (which
I will release to the public some day) and in any situation where I have
a service running that spawns asynchronous operations. Ian’s appoach to
cancelling an asynchronous operation is the similar to mine:

> The approach I always recommend is dead simple. Have a volatile bool
> field that is visible both to your worker thread and your UI thread.
> If the user clicks cancel, set this flag. Meanwhile, on your worker
> thread, test the flag from time to time. If you see it get set, stop
> what you’re doing.

One difference is that I chose not to use a
[volatile](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/csspec/html/vclrfcsharpspec_10_4_3.asp "Volatile on MSDN")
bool field. My reasoning was that if my asynchronous operation only
reads the value (and never writes it) and just happened to be reading it
while my main thread was changing it to false (in response to a user
cancellation effort), I’m not so concerned that asynchronous operation
might read true even though it’s being set to false. Why not? Well it’ll
stay false by the time I check it again and the chance of that small
synchronization flaw is very minute and has a low cost even if it does
occur.

The question is, am I missing something more important by not using a
volatile field in this instance?

