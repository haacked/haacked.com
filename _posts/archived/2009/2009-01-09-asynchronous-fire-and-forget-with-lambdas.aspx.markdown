---
title: Asynchronous Fire and Forget With Lambdas
date: 2009-01-09 -0800
disqus_identifier: 18572
tags:
- code
redirect_from: "/archive/2009/01/08/asynchronous-fire-and-forget-with-lambdas.aspx/"
---

I’ve been having trouble getting to sleep lately, so I thought last
night that I would put that to use and hack on
[Subtext](http://subtextproject.com/ "Subtext Project Website") a bit.
While doing so, I ran into an old Asynchronous Fire and Forget helper
method written way back by [Mike
Woodring](http://www.bearcanyon.com/dotnet/#FireAndForget "Fire and Forget")
which allows you to easily call a delegate asynchronously.

On the face of it, it seems like you could simply call `BeginInvoke` on
the delegate and be done with it, Mike’s code addresses a concern with
that approach:

> Starting with the 1.1 release of the .NET Framework, the SDK docs now
> carry a caution that mandates calling `EndInvoke` on delegates you've
> called `BeginInvoke` on in order to avoid potential leaks. This means
> you cannot simply "fire-and-forget" a call to `BeginInvoke` without
> the risk of running the risk of causing problems.

The mandate he’s referring to, I believe, is this clause in the [MSDN
docs](http://msdn.microsoft.com/en-us/library/2e08f6yc.aspx "Calling Synchronous Methods Asynchronously"):

> No matter which technique you use, always call EndInvoke to complete
> your asynchronous call.

Note that it doesn’t explicitly say “or you will get a memory leak”. But
a little digging turns up the following [comment in the MSDN
forums](http://social.msdn.microsoft.com/Forums/en-US/clr/thread/b18b0a27-e2fd-445a-bcb3-22a315cd6f0d/ "Memory Leak").

> The reason that you should call EndInvoke is because the results of
> the invocation (even if there is no return value) must be cached by
> .NET until EndInvoke is called.  For example if the invoked code
> throws an exception then the exception is cached in the invocation
> data.  Until you call EndInvoke it remains in memory.  After you call
> EndInvoke the memory can be released.  For this particular case it is
> possible the memory will remain until the process shuts down because
> the data is maintained internally by the invocation code.  I guess the
> GC might eventually collect it but I don't know how the GC would know
> that you have abandoned the data vs. just taking a really long time to
> retrieve it.  I doubt it does.  Hence a memory leak can occur.

The thread continues to have some back and forth and doesn’t appear to
be conclusive either way, but this post by Don Box gives a very
pragmatic argument.

> …the reality is that some implementations rely on the EndXXX call to
> clean up resources.  Sometimes you can get away with it, but in the
> general case you can't.

In other words, why take the chance? In any case, much of this
discussion is made redundant with the C\# 3.0 `Action` class combined
with `ThreadPool.QueueUserWorkItem `aka (QUWI)

Here is the code in Subtext for sending email, more or less. I have to
define a delegate and then pass that to `FireAndForget`

```csharp
// declaration
delegate bool SendEmailDelegate(string to, 
      string from, 
      string subject, 
      string body);

//... in a method body
SendEmailDelegate sendEmail = im.Send;
AsyncHelper.FireAndForget(sendEmail, to, from, subject, body);
```

This code relies on the `FireAndForget` method which I show here. Note I
am not showing the full code. I just wanted to point out that the
arguments to the delegate are not strongly typed. They are simply an
array of objects which provide no guidance to how many arguments you
need to pass.

```csharp
public static void FireAndForget(Delegate d, params object[] args)
{
  ThreadPool.QueueUserWorkItem(dynamicInvokeShim, 
    new TargetInfo(d, args));
}
```

Also notice that this implementation uses QUWI under the hood.

With C\# 3.0, there is no need to abstract away the call to QUWI. Just
pass in a lambda, which provides the benefit that you’re calling the
actual method directly so you get Intellisense for the argumennts etc…
So all that code gets replaced with:

```csharp
ThreadPool.QueueUserWorkItem(callback => im.Send(to, from, subject, body));
```

Much cleaner and I get to get rid of more code! As I’ve said before, the
only thing better than writing code is getting *rid* of code!

