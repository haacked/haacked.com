---
title: 'Threading Tips: Never Lock a Value Type. Never Lock "This"'
tags: [code,dotnet,dispose,concurrency]
redirect_from:
  - "/archive/2005/04/11/neverlockthis.aspx/"
  - "/archive/2005/04/12/NeverLockThis.aspx/"
---

UPDATE: As a commenter pointed out, the original code example did not properly demonstrate the problem with locking on the `this` keyword within a normal method. I have corrected this example and [wrote a better example](https://haacked.com/archive/2006/08/08/threadingneverlockthisredux.aspx/ "Never Lock This") that demonstrates that this problem still exists even in a “normal”
method.

Take a look at this code:

```csharp
private bool isDisposed = false;
 
//... code...
~MyClass()
{
    lock(isDisposed)
    {
        if(!isDisposed)
        {
            //Do Stuff...
        }
    }
}
```

Hopefully you can see the problem here right away. The lock statement takes an `object` instance as a parameter. So what happens to the boolean `isDisposed` within the `lock` statement? That’s right! It gets boxed, meaning a new object instance is allocated and passed to the `lock` statement. Thus every time you lock on a value type, you’re locking on a new object.

Ok, so let’s try to fix this up a bit.

```csharp
private bool isDisposed = false;
 
//... code...
~MyClass()
{
    lock(this)
    {
        if(!isDisposed)
        {
            //Do Stuff...
        }
    }
}
```

So is there anything wrong with this? You’ve probably seen the Microsoft examples locking on `this`. Well never give full trust to example code (especially as it’s unlikely you’ll add the code to the GAC) ;). Suppose this snippet is from a class `MyClass`. What do you think will happen with the following code:

```csharp
MyClass instance = new MyClass();

Monitor.Enter(instance);
instance = null;

GC.Collect();
GC.WaitForPendingFinalizers();
```

You guessed it! Deadlock.

Every time you lock a .NET object, the runtime associates a `SyncBlock` structure to that object. Locking works by checking who owns an object’s `SyncBlock` when attempting to acquire a lock. Thus in the code sample above, the client code and the `Dispose()` Method are attempting to lock on the same object.

For a more in-depth discussion, I highly recommend Jeffrey Richter’s article [Safe Thread Synchronization](http://msdn.microsoft.com/msdnmag/issues/03/01/NET/ "Safe Threading") which is where I first learned about this subtle threading issue.

Likewise you might also take a look at Dr Gui’s [Don’t Lock Type Objects](http://msdn.microsoft.com/library/default.asp?url=/archive/en-us/dnaraskdr/htmlaskgui06032003.asp "Do Not Lock The Type") post.
