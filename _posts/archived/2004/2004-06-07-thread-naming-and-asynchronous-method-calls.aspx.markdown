---
title: Thread Naming and Asynchronous Method Calls
tags: [dotnet,code,concurrency]
redirect_from: "/archive/2004/06/06/thread-naming-and-asynchronous-method-calls.aspx/"
---

![Thread](/assets/images/Thread.jpg)Typically when you spawn a new thread, you
want to give it a name to facilitate debugging. For example:

```csharp
using System.Threading;
//.. other stuff....
var thread = new Thread(new ThreadStart(DoSomething); thread.Name = "DoingSomething";
threat.Start();
```

The code in the method `DoSomething` (not shown) will run on a thread
named "DoingSomething."

Now suppose you're writing a socket server using the asynchronous programming model. You might write something that looks like the following:

```csharp
using System.Net.Sockets;
using System.Threading;
var allDone = new ManualResetEvent(false);

public static void Main() {
    Socket socket = new Socket(...);
    //you get the idea
    while(true) { 
        allDone.Reset();
        socket.BeginAccept(new AsyncCallback(OnSocketAccept), socket);
        allDone.WaitOne();
    }
}
    
public void OnSocketAccept() {
    Thread.CurrentThread.Name = "SocketAccepted";
    allDone.Set();
    // Some socket operation.
}
```

In the example above, we're setting up a socket to call the method
**OnSocketAccept** asynchronously when a new connection occurs.

When you run this, it may work just fine for a while. It might even pass
all your unit tests. Don't you just feel all warm and fuzzy when the
green bar appears? Put this in production, however, and that warm and
fuzziness may turn into cold dread as you're guaranteed to run into
an**InvalidOperationException**.

Why is that? Underneath the hood, when the **OnSocketAccept** method is
called, the CLR rips a thread from from the CLR's thread pool. When the
method completes, the thread happily returns to the pool to finish its
Pina Colada. Eventually, that thread will resurface, and that's where
the problem arises.

You can change the name of a thread, but you can only change it once.If
you try to change it again, you're greeted with an
InvalidOperationException. When a thread is returned to the thread pool,
it holds onto its name. Its happy to have a sense of identity and will
hold onto it even when it resurfaces to execute another method. To
protect from this, always check the name of a thread before setting it
like so:

```csharp
if(Thread.CurrentThread.Name == null)
  Thread.CurrentThread.Name = "MyNameIsBob";
```

Your threads will thank you for it.
