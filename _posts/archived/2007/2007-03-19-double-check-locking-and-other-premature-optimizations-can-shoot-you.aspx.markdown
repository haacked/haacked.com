---
title: Double Check Locking and Other Premature Optimizations Can Shoot You In The
  Foot
tags: [code]
redirect_from: "/archive/2007/03/18/double-check-locking-and-other-premature-optimizations-can-shoot-you.aspx/"
---

![Lock](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/AvoidDoubleCheckLockingForSingletons_EBC6/736837_combination_lock7.jpg)
After reading Scott Hanselman’s post on [Managed
Snobism](http://www.hanselman.com/blog/ManagedSnobism.aspx "Managed Snobism")
which covers the snobbery some have against managed languages because
they don’t “perform” well, I had to post the [following
rant](http://www.hanselman.com/blog/ManagedSnobism.aspx#bd322d6f-210d-4d1f-9ef7-3b929aa66714 "My Rant")
in his comments:

> What is it that makes huge populations of developers think they’re
> working on a Ferrari when their app is really just a Pinto? \
> \
> “*I’m writing a web app that pulls data from a database and puts it on
> a web page. I never use 'foreach' because I heard it’s slower than
> explicitly iterating a for loop.*”

In my time as a developer I’ve experienced too many instances of this
[Micro
Optimization](http://www.codinghorror.com/blog/archives/000185.html "Micro Optimization and Meatballs"),
also known as [Premature
Optimization](http://blogs.msdn.com/ericgu/archive/2006/06/26/647877.aspx "Premature Optimization").

Premature optimization tends to lead “clever” developers to shoot
themselves in the foot (metaphorically speaking, of course). Let’s look
at one common example I’ve run into from time to time—double check
locking for
[singletons](http://en.wikipedia.org/wiki/Singleton_pattern "Singletons").

## Double Check Locking Refresher

As a refresher, here is an example of the double check pattern.

```csharp
public sealed class MyClass
{
  private static object _synchBlock = new object();
  private static volatile MyClass _singletonInstance;

  //Makes sure only this class can create an instance.
  private MyClass() {}
  
  //Singleton property.
  public static MyClass Singleton
  {
    get
    {
      if(_singletonInstance == null)
      {
        lock(_synchBlock)
        {
          // Need to check again, in case another cheeky thread 
          // slipped in there while we were acquiring the lock.
          if(_singletonInstance == null)
          {
            _singletonInstance = new MyClass();
          }
        }
      }
    }
  }
}
```

The premise behind this approach is that all this extra ugly code will
wring out better performance by lazy loading the singleton. If it is
never accessed, it never needs to be instantiated. **Of course this
raises the question, *Why define a Singleton if it’s quite likely it’ll
never get used?***

The `Singleton` property checks the static singleton member for null. If
it is null, it attempts to acquire a lock before checking if its null
again. Why the second null check? Well in the time our current thread
took to acquire the lock, another thread could have snuck in and
initialized the singleton.

Note that we use the `volatile` keyword for the `_singletonInstance`
static member. Why? Long story made short, this has to do with how
[different memory models can reorder reads and
writes](http://msdn.microsoft.com/msdnmag/issues/05/10/MemoryModels/ "Memory models").
For the current CLR you can ignore the volatile keyword in this case.
But if you run your code on Mono or some other future platform, you may
need it, so no point in not leaving it there.

## Criticisms or If this is fast, how much faster is triple check locking?

Jeffrey Richter in his book [CLR via
C#](http://www.amazon.com/gp/product/0735621632?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=9325&creativeASIN=0735621632 "CLR via C# on Amazon.com")
criticizes this approach (starting on page 639) as “not that
interesting” (*Yes, he can be scathing!*)

> The double-check locking technique is less efficient than the class
> constructor technique because you need to construct your own lock
> object (in the class constructor) and write all of the additional
> locking code yourself.

The cost of initializing the singleton instance would have to be
significantly more than the cost of instantiating the object used to
synchronize access to it (not to mention all the conditional checks when
accessing the singleton) to be worth it.

## A Better Approach? The No Look Pass of Singletons

So what’s the better approach? Use a static initializer in what I call
the *No Check No Locking Technique*.

```csharp
public sealed class MyClass
{
  private static MyClass _singletonInstance = new MyClass();

  //Makes sure only this class can create an instance.
  private MyClass() {}
  
  //Singleton property.
  public static MyClass Singleton
  {
    get
    {
      return _singletonInstance;
    }
  }
}
```

**The CLR guarantees that the code in a static constructor (implicit or
explicit) is only called once. You get all that thread safety for
free!** No need to write your own error prone locking code in this case
and no need to dig through Memory Model implications. It just works,
unlike your Pinto, sorry, “Ferrari”.

See, sometimes you can have your cake and eat it too. This code, which
is simpler and easier to understand, happens to perform better and
requires one less object instantiaton. How do you like them apples?

It turns out that this approach is also recommended for Java, as it was
discovered that the [double check locking approach wasn’t guaranteed to
work](http://www.cs.umd.edu/~pugh/java/memoryModel/DoubleCheckedLocking.html "Double Check Locking in Java").

## What!? You’re Still Using Singletons?!

Now that I’ve gone through all this trouble to show you the proper way
to create a Singleton, I leave you with this thought. Should a well
designed system use Singletons in the first place, or [is it just
a stupid
idea](http://steve.yegge.googlepages.com/singleton-considered-stupid "Singleton Considered Stupid")?
That’s a topic for another time.

Please note that double check locking doesn’t only apply to Singletons.
It just happens to be the place where it is most often seen in the wild.

