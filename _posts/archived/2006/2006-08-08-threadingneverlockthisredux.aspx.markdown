---
title: Threading - Never Lock This Redux
date: 2006-08-08 -0800
disqus_identifier: 14764
categories: [dotnet,csharp,threading,lock]
redirect_from:
  - "/archive/2006/08/07/threadingneverlockthisredux.aspx/"
  - "/archive/2006/08/08/ThreadingNeverLockThisRedux.aspx/"
---

A while ago I wrote that you [should never lock a value type and never lock `this`](https://haacked.com/archive/2005/04/12/NeverLockThis.aspx "Threading Tips"). I presented a code snippet to illustrate the point but I violated the cardinal rule for code examples: compile and test it in context. Mea Culpa! Today in my comments, someone named Jack rightly pointed out that my example doesn’t demonstrate a deadlock due to locking `this`. As he points out, if the code were in a Finalizer, then my example would be believable.

To my defense, I was just testing to see if you were paying attention. ;) Nice find Jack!

My example was loosely based on Richter’s example in his article on [Safe Thread Synchronization](http://msdn.microsoft.com/msdnmag/issues/03/01/NET/ "Safe Thread Synchronization"). Instead of rewriting his example, I will just [link to it](http://msdn.microsoft.com/msdnmag/issues/03/01/NET/default.aspx?fig=true#fig7 "Figure 7 - Threads Banging Heads").

His example properly demonstrates the problem with a Finalizer thread attempting to lock on the object. However Jack goes on to say that locking on `this` in an ordinary method is fine. I still beg to differ, and have a better code example to prove it.

Again, suppose you carefully craft a class to handle threading
internally. You have certain methods that carefully protect against
reentrancy by locking on the `this` keyword. Sounds great in theory, no?
However now you pass an instance of that class to some method of another
class. That class should not have a way to use the same `SyncBlock` for
thread synchronization that your methods do internally, right?

But it does!

In .NET, an object’s `SyncBlock` is not private. Because of the way
locking is implemented in the .NET framework, an object’s `SyncBlock` is
not private. Thus if you lock `this`, you are using to the current
object’s `SyncBlock` for thread synchronization, which is also available
to external classes.

Richter’s article [explains this
well](http://msdn.microsoft.com/msdnmag/issues/03/01/NET/ "Safe Thread Synchronization").
But enough theory you say, **show me the code!** I will demonstrate this
with a simple console app that has a somewhat realistic scenario. Here
is the application code. It simply creates a `WorkDispatcher` that
dispatches a `Worker` to do some work. Simple, eh?

```csharp
class Program
{
    static void Main()
    {
        WorkDispatcher dispatcher = new WorkDispatcher();
        dispatcher.Dispatch(new Worker());
    }
}
```

Next we have the carefully crafted `WorkDispatcher`. It has a single
method `Dispatch` that takes a lock on `this` (for some very good
reason, I am sure) and then dispatches an instance of `IWorker` to do
something by calling its `DoWork` method.

```csharp
public class WorkDispatcher
{
    int dispatchCount = 0;
    
    public void Dispatch(IWorker worker)
    {
        Console.WriteLine("Locking this");
        lock(this)
        {
            Thread thread = new Thread(worker.DoWork);
            Console.WriteLine("Starting a thread to do work.");
            dispatchCount++;
            Console.WriteLine("Dispatched " + dispatchCount);
            thread.Start(this);
            
            Console.WriteLine("Wait for the thread to join.");
            thread.Join();
        }
        Console.WriteLine("Never get here.");
    }
}
```

From the look of it, there should be no reason for this class to
deadlock in and of itself. But now let us suppose this is part of a
plugin architecture in which the user can plug in various
implementations of the `IWorker` interface. The user downloads a really
swell plugin from the internet and plugs it in there. Unfortunately,
this worker was written by a malicious *eeeeevil* developer.

```csharp
public class Worker : IWorker
{        
    public void DoWork(object dispatcher)
    {
        Console.WriteLine("Cause Deadlock.");
        lock (dispatcher)
        {
            Console.WriteLine("Simulating some work");
        }
    }
}
```

The evil worker disrupts the carefully constructed synchronization plans
of the `WorkDispatcher` class. This is a somewhat contrived example, but
in real world multi-threaded application, this type of scenario can
quite easly surface.

If the `WorkDispatcher` was really concerned about thread safety and
protecting its synchronization code, it would lock on something private
that no external class could lock on. Here is a corrected example of the
`WorkDispatcher`.

```csharp
public class WorkDispatcher
{
    object syncBlock = new object();
    int dispatchCount = 0;
    
    public void Dispatch(IWorker worker)
    {
        Console.WriteLine("Locking this");
        lock (this.syncBlock)
        {
            Thread thread = new Thread(worker.DoWork);
            Console.WriteLine("Starting a thread to do work.");
            dispatchCount++;
            Console.WriteLine("Dispatched " + dispatchCount);
            thread.Start(this);
            
            Console.WriteLine("Wait for the thread to join.");
            thread.Join();
        }
        Console.WriteLine("Now we DO get here.");
    }
}
```

So Jack, if you are reading this, I hope it convinces you (and everyone
else) that locking on `this`, even in a normal method, is a pretty bad
idea. It won’t always lead to problems, but why risk it?
