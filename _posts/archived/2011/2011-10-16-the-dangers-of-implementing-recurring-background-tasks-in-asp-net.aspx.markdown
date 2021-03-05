---
title: The Dangers of Implementing Recurring Background Tasks In ASP.NET
tags: [aspnet,dotnet]
redirect_from: "/archive/2011/10/15/the-dangers-of-implementing-recurring-background-tasks-in-asp-net.aspx/"
---

I like to live life on the wild side. No, I don’t base jump off of
buildings or invest in speculative tranches made up of junk stock
derivatives. What I do is attempt to run recurring background tasks
within an ASP.NET application.

[![110121-M-2339L-074](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Why-Backg.NET-and-How-To-Do-Them-Anyways_7C02/base-jumping_thumb.jpg "110121-M-2339L-074")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Why-Backg.NET-and-How-To-Do-Them-Anyways_7C02/base-jumping_2.jpg)
*Writing code is totally just like this - [Photo by
DVIDSHUB](http://www.flickr.com/photos/dvids/5387207067/) – [CC BY
2.0](http://creativecommons.org/licenses/by/2.0/deed.en) *

But before I do anything wild with ASP.NET, I always talk to my
colleague, Levi (sadly, no blog). As a developer on the internals of
ASP.NET, he knows a huge amount about it, especially the potential
pitfalls. He’s also quite the security guru. As you read this sentence,
he just guessed your passwords. All of them.

When he got wind of my plan, he let me know it was evil, unsupported by
ASP.NET and just might kill a cat. Good thing I’m a dog person. I
persisted in my foolhardiness and suggested maybe it’s not evil, just
risky. If so, how can I do it as safely as possible? What are the risks?

There are three main risks, one of which I’ll focus on in this blog
post.

1.  An unhandled exception in a thread not associated with a request
    will take down the process. This occurs even if you have a handler
    setup via the Application\_Error method. I’ll try and explain why in
    a follow-up blog post, but this is easy to deal with.
2.  If you run your site in a Web Farm, you could end up with multiple
    instances of your app that all attempt to run the same task at the
    same time. A little more challenging to deal with than the first
    item, but still not too hard. One typical approach is to use a
    resource common to all the servers, such as the database, as a
    synchronization mechanism to coordinate tasks.
3.  The AppDomain your site runs in can go down for a number of reasons
    and take down your background task with it. This could corrupt data
    if it happens in the middle of your code execution.

It’s this last risk that is the focus of this blog post.

Bye Bye App Domain
------------------

There are several things that can cause ASP.NET to tear down your
AppDomain.

-   When you modify web.config, ASP.NET will recycle the AppDomain,
    though the w3wp.exe process (the IIS web server process) stays
    alive.
-   IIS will itself recycle the entire w3wp.exe process every 29 hours.
    It’ll just outright put a cap in the w3wp.exe process and bring down
    all of the app domains with it.
-   In a shared hosting environment, many web servers are configured to
    tear down the application pools after some period of inactivity. For
    example, if there are no requests to the application within a 20
    minute period, it may take down the app domain.

If any of these happen in the middle of your code execution, your
application/data could be left in a pretty bad state as it’s shut down
without warning.

So why isn’t this a problem for your typical per request ASP.NET code?
When ASP.NET tears down the AppDomain, it will attempt to flush the
existing requests and give them time to complete before it takes down
the App Domain. ASP.NET and IIS are considerate to code *that they know
is running,*such as code that runs as part of a request.

Problem is, ASP.NET doesn’t know about work done on a background thread
spawned using a timer or similar mechanism. It only knows about work
associated with a request.

So tell ASP.NET, “Hey, I’m working here!”
-----------------------------------------

The good news is there’s an easy way to tell ASP.NET about the work
you’re doing! In the `System.Web.Hosting` namespace, there’s an
important class, `HostingEnvironment`. According to the MSDN docs, this
class…

> Provides application-management functions and application services to
> a managed application within its application domain

This class has an important static method,
[`RegisterObject`](http://msdn.microsoft.com/en-us/library/system.web.hosting.hostingenvironment.registerobject.aspx "RegisterObject on MSDN").
The MSDN description here isn’t super helpful.

> Places an object in the list of registered objects for the
> application.

For us, what this means is that the `RegisterObject` method tells
ASP.NET that, “Hey! Pay attention to this code here!” **Important! This
method requires full trust!**

This method takes in a single object that implements the
[`IRegisteredObject`
interface](http://msdn.microsoft.com/en-us/library/system.web.hosting.iregisteredobject.aspx "IRegisteredObject on MSDN").
That interface has a single method:

```csharp
public interface IRegisteredObject
{
    void Stop(bool immediate);
}
```

When ASP.NET tears down the AppDomain, it will first attempt to call
`Stop` method on all registered objects.

In most cases, it’ll call this method twice, once with `immediate` set
to `false.` This gives your code a bit of time to finish what it is
doing. ASP.NET gives all instances of `IRegisteredObject` a total of 30
seconds to complete their work, not 30 seconds each. After that time
span, if there are any registered objects left, it will call them again
with `immediate` set to `true. `This lets you know it means business and
you really need to finish up pronto! I modeled my parenting technique
after this method when trying to get my kids ready for school.

When ASP.NET calls into this method, your code needs to prevent this
method from returning until your work is done. Levi showed me one easy
way to do this by simply using a lock. Once the work is done, the code
needs to unregister the object.

For example, here’s a simple generic implementation of
`IRegisteredObject`. In this implementation, I simply ignored the
`immediate` flag and try to prevent the method from returning until the
work is done. The intent here is I won’t pass in any work that’ll take
too long. Hopefully.

```csharp
public class JobHost : IRegisteredObject
{
    private readonly object _lock = new object();
    private bool _shuttingDown;

    public JobHost()
    {
        HostingEnvironment.RegisterObject(this);
    }

    public void Stop(bool immediate)
    {
        lock (_lock)
        {
            _shuttingDown = true;
        }
        HostingEnvironment.UnregisterObject(this); 
    }

    public void DoWork(Action work)
    {
        lock (_lock)
        {
            if (_shuttingDown)
            {
                return;
            }
            work();
        }
    }
}
```

I wanted to get the simplest thing possible working. Note, that when
ASP.NET is about to shut down the AppDomain, it will attempt to call the
`Stop` method. That method will try to acquire a lock on the `_lock`
instance. The `DoWork` method also acquires that same lock. That way,
when the `DoWork` method is doing the work you give it (passed in as a
lambda) the `Stop` method has to wait until the work is done before it
can acquire the lock. Nifty.

Later on, I plan to make this more sophisticated by taking advantage of
using a `Task` to represent the work rather than an `Action`. This would
allow me to take advantage of task cancellation instead of the brute
force approach with locks.

With this class in place, you can create a timer on `Application_Start`
(I generally use
[WebActivator](https://bitbucket.org/davidebbo/webactivator) to register
code that runs on app start) and when it elapses, you call into the
`DoWork` method here. Remember, the timer must be referenced or it could
be garbage collected.

Here’s a small example of this:

```csharp
using System;
using System.Threading;
using WebBackgrounder;

[assembly: WebActivator.PreApplicationStartMethod(
  typeof(SampleAspNetTimer), "Start")]

public static class SampleAspNetTimer
{
    private static readonly Timer _timer = new Timer(OnTimerElapsed);
    private static readonly JobHost _jobHost = new JobHost();

    public static void Start()
    {
        _timer.Change(TimeSpan.Zero, TimeSpan.FromMilliseconds(1000));
    }

    private static void OnTimerElapsed(object sender)
    {
        _jobHost.DoWork(() => { /* What is it that you do around here */ });
    }
}
```

Recommendation
--------------

This technique can make your background tasks within ASP.NET much more
robust. There’s still a chance of problems occurring though. Sometimes,
the AppDomain goes down in a more abrupt manner. For example, you might
have a blue screen, someone might trip on the plug, or a [hard-drive
might
fail](https://haacked.com/archive/2009/12/14/back-in-business-again.aspx "Back in Business").
These catastrophic failures can take down your app in such a way that
leaves data in a bad state. But hopefully, these situations occur much
less frequently than an AppDomain shutdown.

Many of you might be scratching your head thinking it seems weird to use
a web server to perform recurring background tasks. That’s not really
what a web server is for. You’re absolutely right. My recommendation is
to do one of the following instead:

-   Write a simple console app and schedule it using Windows task
    schedule.
-   Write a Windows Service to manage your recurring tasks.
-   Use an Azure worker or something similar.

Given that those are my recommendations, why am I still working on a
system for scheduling recurring tasks within ASP.NET that handles web
farms and AppDomain shutdowns I call
[WebBackgrounder](https://github.com/NuGet/WebBackgrounder "WebBackgrounder on Github")
(NuGet package coming later)?

I mean, besides the fact that I’m thick-headed? Well, for two reasons.

The first is to make development easier. When you get latest from our
source code, I just want everything to work. I don’t want you to have to
set up a scheduled task, or an Azure worker, or a Windows server on your
development box. A development environment can tolerate the issues I
described.

The second reason is for simplicity. If you’re ok with the limitations I
mentioned, this approach has one less moving part to worry about when
setting up a website. There’s no need to configure an external recurring
task. It just works.

But mostly, it’s because I like to live life on the edge.

