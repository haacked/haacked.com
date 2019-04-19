---
title: Why Block At All?  Thoughts on threading and sockets
date: 2004-08-06 -0800 9:00 AM
tags: [dotnet,concurrency]
redirect_from: "/archive/2004/08/05/why-block-at-all.aspx/"
---

The path of least resistance when writing threading code as well as
socket communications is to use techniques that cause indefinite
blocking of some sort. Personally, I prefer never to block indefinitely.
For example, it's quite common to see code such as:

```csharp
lock(someObject)
{
    //Do Something here...
}
```

Nothing wrong with this inherently, but this piece will try to acquire a
lock on someObject indefinitely. Imagine if you mistakenly had code like
(yes, it's a bit contrived)

```csharp
using System.Threading;

//... other stuff ...

object someObject = new Object();
object someOtherObject = new object();

public void LockItUp()
{
    lock(someObject)
    {
        Console.WriteLine("Lock 1 acquired.");

        ManualResetEvent mre = 
                new ManualResetEvent(false);
        WaitCallback callback = 
                new WaitCallback(ThrowAwayTheKey);        

        ThreadPool.QueueUserWorkItem(callback, mre);
        
        // wait till other thread has lock on 
        // someOtherObject
        mre.WaitOne(); 

        lock(someOtherObject)
        {
            Console.WriteLine("I never get called.");
        }
    }
}

void ThrowAwayTheKey(object resetEvent)
{
    lock(someOtherObject)
    {
        Console.WriteLine("Lock 2 acquired.");
        ManualResetEvent mre = 
                resetEvent as ManualResetEvent;
        if(mre != null)
            mre.Set(); //original thread can continue.

        lock(someObject)
        {
            Console.WriteLine("Neither do I");
        }
    }
}
```

Calling the method LockItUp will cause a deadlock and the application
will hang until you kill it. Although this example is a bit contrived,
you'd be surprised how easy it is in a sufficiently large and
complicated system with multiple developers for you to run into this
situation in a more roundabout manner. I see this often enough because
using the lock statement is the path of least resistance. Instead, try
using the [TimedLock
struct](http://www.interact-sw.co.uk/iangblog/2004/03/23/locking).

Another situation this type of thing comes up is with socket
programming. Often I see code like this:

```csharp
using System.Net.Sockets;

//... other stuff ...

byte[] _buffer = new byte[4096];
public void Listen(Socket socket)
{
    int bytesRead 
        = socket.Receive(_buffer, 0, 4096
                , SocketFlags.None);

    //You're sitting here all day.
}
```

If the remote socket isn't forthcoming with that data, you're going to
be sitting there all day holding that thread open. In order to stop that
socket, you'll need another thread to call Shutdown or close on the
socket. Contrast that with this approach:

```csharp
byte[] _buffer = new byte[4096];
public void BeginListen(Socket socket)
{
    socket.BeginReceive(_buffer, 0, 4096
            , SocketFlags.None
            , new AsyncCallback(OnDataReceived)
            , socket);
    //returns immediately.
}

void OnDataReceived(IAsyncResult ar)
{
    Socket socket = ar.AsyncState as Socket;
    int bytesRead = socket.EndReceive(ar);
    
    //go on with your bad self...
}
```

BeginListen returns immediately and OnDataReceived isn't called until
there's actual data to receive. An added benefit is that you're not
taking up a thread from the ThreadPool, but rather you're using IO
completion ports. IO Completion ports is a method Windows uses for
asynchronous IO operations. When an asynchronous IO is complete, Windows
will awaken and notify your thread. The IO operations run on a pool of
kernel threads whose only task in life is to process I/O requests.

Since BeginListen returns immediately, you're free to close the socket
if no data is received after a certain time or in response to some other
event. This may be a matter of preference, but this is a more elegant
and scalable approach to sockets.

For more on asynchronous sockets, take the time to read [Using an
Asynchronous Server
Socket](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpguide/html/cpconUsingNon-blockingClientSocket.asp)
and related articles.

