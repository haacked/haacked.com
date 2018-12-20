---
title: Abstract Base Classes Have Versioning Problems Too
date: 2008-02-21 -0800
disqus_identifier: 18458
categories: [code,dotnet,csharp,versioning]
redirect_from: "/archive/2008/02/20/abstract-base-classes-have-versioning-problems-too.aspx/"
---

This is part 2 in an ongoing series in which I talk about various design
and versioning issues as they relate to Abstract Base Classes (ABC),
Interfaces, and Framework design. In [part
1](https://haacked.com/archive/2008/02/21/versioning-issues-with-abstract-base-classes-and-interfaces.aspx#66512 "Versioning Issues With Abstract Base Classes and Interfaces")
I discussed some ways in which ABCs are more resilient to versioning
than interfaces. I haven’t covered the full story yet and will address
some great points raised in the comments.

In this part, I want to point out some cases in which Abstract Base
Classes fail in versioning. In my last post, I mentioned you could
simply add new methods to an Abstract Base Class and not break clients.
Well that’s true, it’s possible, but I didn’t emphasize that this is not
true for all cases and can be risky. I was saving that for another post
(aka this one).

I had been thinking about this particular scenario a while ago, but it
was recently solidified in talking to a coworker today (thanks Mike!).
Let’s look at the scenario. Suppose there is an abstract base class in a
framework named `FrameworkContextBase`. The framework also provides a
concrete implementation.

```csharp
public abstract class FrameworkContextBase
{
  public abstract void MethodOne();
}
```

Somewhere else in another class in the framework, there is a method that
takes in an instance of the base class and calls the method on it for
whatever reason.

```csharp
public void Accept(FrameworkContextBase arg)
{
  arg.MethodOne();
}
```

With me so far? Good. Now imagine that you, as a consumer of the
Framework write a concrete implementation of `FrameworkContextBase`. In
the next release of the framework, the framework includes a method to
`FrameworkContextBase` like so...

```csharp
public abstract class FrameworkContextBase
{
  public abstract void MethodOne();
  public virtual void MethodTwo()
  {
    throw new NotImplementedException();
  }
}
```

And the `Accept` method is updated like so...

```csharp
public void Accept(FrameworkContextBase arg)
{
  arg.MethodOne();
  arg.MethodTwo();
}
```

Seems innocuous enough. You might even be lulled into the false sense
that all is well in the world and decide to go ahead and upgrade the
version of the Framework hosting your application without recompiling.
Unfortunately, somewhere in your application, you pass your old
implementation of the ABC to the new `Accept` method. Uh oh! Runtime
exception!

The fix sounds easy in theory, when adding a new method to the ABC, the
framework developer need to make sure it has a *reasonable default
implementation*. In my contrived example, the default implementation
throws an exception. This seems easy enough to fix. But how can you be
sure the implementation is *reasonable* for all possible implementations
of your ABC? You can’t.

This is often why you see guidelines for .NET which suggest making all
methods non-virtual unless you absolutely need to. The idea is that the
Framework should provide checks before and after to make sure certain
invariants are not broken when calling a virtual method since we have no
idea what that method will do.

As you might guess, I tend to take the approach of *buyer beware*.
Rather than putting the weight on the Framework to make sure that
virtual methods don’t do anything weird, I’d rather put the weight on
the developer overriding the virtual method. At least that’s [the
approach](http://weblogs.asp.net/leftslipper/archive/2007/12/10/asp-net-mvc-design-philosophy.aspx "ASP.NET MVC Design Philosophy")
we’re taking with ASP.NET MVC.

Another possible fix is to also add an associated `Supports{Method}`
property when you add a method to an ABC. All code that calls that new
method would have to check the property. For example...

```csharp
public abstract class FrameworkContextBase
{
  public abstract void MethodOne();
  public virtual void MethodTwo()
  {
    throw new NotImplementedException();
  }
  public virtual bool SupportsMethodTwo {get{return false;}}
}

//Some other class in the same framework
public void Accept(FrameworkContextBase arg)
{
  arg.MethodOne();
  if(arg.SupportsMethodTwo)
  {
    arg.MethodTwo();
  }
}
```

But it may not be clear to you, the framework developer, what you should
do when the instance doesn’t support `MethodTwo`. This might not be
clear nor straightforward.

This post seems to contradict my last post a bit, but I don’t see it
that way. As I stated all along, there is no perfect design, we are
simply trying to optimize for constraints. Not only that, I should add
that *versioning is a hard problem*. I am not fully convinced we have
made all the right optimizations (so to speak) hence I am writing this
series.

Coming up: More on versioning interfaces with real code examples and
tradeoffs. More on why breaking changes suck. ;)

