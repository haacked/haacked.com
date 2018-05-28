---
layout: post
title: 'Question: When Is A Good Time To Call GC.Collect()?'
date: 2004-08-13 -0800
comments: true
disqus_identifier: 918
categories: []
redirect_from: "/archive/2004/08/12/question-when-is-a-good-time-to-call-gccollect.aspx/"
---

**Answer:** When you don't have enough change for the phone booth.

I'll be here all week, thank you very much. Bad pun notwithstanding, the
answer to this question is pretty much never (see
[Rico's](http://blogs.gotdotnet.com/ricom/) [almost rule
\#1](http://blogs.gotdotnet.com/ricom/PermaLink.aspx/5ce9cc25-7698-418e-b266-24397e5376c7)).
The Garbage Collector in .NET is like a highly motivated and skilled
employee. If you quit being a micro-manager (["You forgot to put the
cover sheet on the TPS report"](http://www.imdb.com/title/tt0151804/))
and stop looking over its shoulder, it's able to just do its job and
perform quite well.

However, note that Rico says "*Almost* Rule \#1". That must mean there
are appropriate exceptions to the rule, no matter how few they may be.
What are those situations? The reason I ask is I ran into the following
code on the net (dramatization):

```csharp
/// 
/// Stops the socket server and closes 
/// every client connection.
/// 
public void Stop()
{
    if(_isDisposed)
        throw new ObjectDisposedException(
            "SocketServer", 
            "Object is already disposed.");

    CloseConnectedClients();
    CloseListener();

    GC.Collect();
    GC.WaitForPendingFinalizers();
}
```

This is the Stop() method of your typical Socket Server. It closes any
connected socket clients and then closes the listening thread. After
that, it calls GC.Collect() and GC.WaitForPendingFinalizers(), violating
Rico's almost rule. Is this perhaps one of those appropriate times to
call GC.Collect()?

Typically, your socket server will have been running for a long time, so
it is very likely it will have been promoted to Generation 2 and contain
references to a several other Generation 2 objects. Rico points out that

> If your algorithm is regularly producing objects that live to gen2 and
> then die shortly thereafter, you're going to find that the percent
> time spent in GC goes way up. Forcing more of these collects is really
> the last thing you wanted to do (assuming you could, note again
> GC.Collect() doesn't promise to do a gen2 collect).

However, this is situation is different in that the server has been
around a while and calling Stop() on a server typically means you're not
planning to use the Server any time soon afterwards. In fact, you're
most likely about to dispose of it.

Given that, It seems to me that this **might** be one of those cases
where calling GC.Collect() is appropriate. The goal here is a one time
Generation 2 collection. Of course, there's no guarantee that a
Generation 2 collect will occur. Maybe this is a situation where it
makes no difference either way. Any thoughts?

For more reading:\
[Garbage Collector Basics and Performance
Hints](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dndotnet/html/dotnetGCbasics.asp)\
[Programming For Garbage
Collection](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpguide/html/cpconprogrammingessentialsforgarbagecollection.asp)

