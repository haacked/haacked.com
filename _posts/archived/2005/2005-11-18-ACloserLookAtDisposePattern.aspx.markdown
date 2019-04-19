---
title: A Closer Look At The Dispose Pattern
date: 2005-11-18 -0800 9:00 AM
tags: [patterns,csharp,dotnet,dispose]
redirect_from: "/archive/2005/11/17/ACloserLookAtDisposePattern.aspx/"
---

The [Framework Design Guidelines](http://www.amazon.com/gp/product/0321246756/103-9411210-6787060?v=glance&n=283155&v=glance)
has an illuminating discussion on the Dispose pattern for implementing `IDisposable` in chapter 9 section 3. However, there was one place where I found a potential problem.

But first, without rehashing the whole pattern, let me give a brief
description. The basic Dispose Pattern makes use of a template method
approach. I really like how they take this approach. Let me
demonstrate...

```csharp
public class DisposableObject : IDisposable
{
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    //This is the template method...
    protected virtual void Dispose(bool disposing)
    {
        if(disposing)
        {
            Console.WriteLine("Releasing Managed Resources in base class!");
        }
    }
}
```

This disposable class implements a non-virtual `Dispose` method that calls a protected virtual method. According to the guidelines, classes that inherit from this class should simply override the `Dispose(bool);`
method like so.

```csharp
public class SubDisposable : DisposableObject
{
    protected override void Dispose(bool disposing)
    {
        Console.WriteLine("Releasing Managed Resources in Sub Class.");
    }
}
```

Notice anything odd about this? Shouldn’t this inheriting class call `base.Dispose(disposing)`? The guidelines make no mention of calling the base dispose method. However, just to make sure I wasn’t missing something, I ran the following code.

```csharp
using(SubDisposable obj = new SubDisposable())
{
    Console.WriteLine(obj.ToString());
}
```

This produces the following output:

>     UnitTests.Velocit.Threading.SubDisposableReleasing Managed Resources in Sub Class.

Notice that resources in the base class are never released.

Also, while I applaud the use of a protected template method to implement the dispose pattern, I think it is possible to take the pattern one step further. The purpose of using template methods is bake in an algorithm consisting of a series of steps. You provide abstract or virtual methods to allow implementations to change the behavior of those distinct steps.

When I think of the steps it takes to dispose an object using the simple pattern, it consists of the following discrete step:

-   Calling `Dispose` indicates object is being disposed and not being
    finalized
-   Call into protected `Dispose(bool);`
-   Protected method releases unmanaged resources
-   if disposing
    -   release unmanaged resources
    -   Suppress finalization

So why not codify these series of steps into the pattern. The Simple Dispose pattern might look like...

```csharp
public class DisposableObject : IDisposable
{
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    void Dispose(bool disposing)
    {
        ReleaseUnmanagedResources();
        if(disposing)
        {
            ReleaseManagedResources();
        }
    }

    //Template method
    protected virtual void ReleaseUnmanagedResources()
    {}

    //Template method
    protected virtual void ReleaseManagedResources()
    {}
}
```

Notice that the `Dispose(bool);` method is now private. There are two new virtual template methods. Also note these are virtual. I did not make these abstract, since this gives the inheritor a choice on whether
to implement them or not. This might seem like overkill, but it removes one more decision to be made when overriding this class. That is the goal of these patterns, to make doing the right thing automatic to the
implementer.

In the previous pattern, an implementer has to remember what to do when `disposing` is true as opposed to it being false. Do I release unmanaged when its true? Or when its false. Certainly if you’re implementing the
pattern, you should really know this down pat. But still it doesn’ hurt to make the algorithm more readable. Looking at this modified pattern, it is quite obvious what I need to do in the method `ReleaseManagedResources`. I probably need to add documentation to tell the overriding implementer to make sure to call `base.ReleaseManagedResources()`.

The only open question I have with this pattern is whether or not it is safe to call `base.ReleaseUnmanagedResources()` from an implementing class. I need to dig into the C# specs to understand that issue fully. The issue is that from within ReleaseUnmanagedResources, you really shouldn’t touch any managed resources, because this method could be called from a finalizer thread. Is calling a method on your base class
in violation of this rule?
