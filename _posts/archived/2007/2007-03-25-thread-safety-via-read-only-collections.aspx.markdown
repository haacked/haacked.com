---
title: Thread Safety Via Read Only Collections
tags: [concurrency]
redirect_from: "/archive/2007/03/24/thread-safety-via-read-only-collections.aspx/"
---

UPDATE: Made some corrections to the discussion of ReadOnlyCollection’s
interface implementations near the bottom. Thanks to [Thomas
Freudenberg](http://thomasfreudenberg.com/ "Thomas Freudenberg") and
[Damien Guard](http://www.damieng.com/blog/ "Damien Guard") for pointing
out the discrepancy.

In a recent post I warned against needlessly using [double check
locking](https://haacked.com/archive/2007/03/19/double-check-locking-and-other-premature-optimizations-can-shoot-you.aspx "Double Check Locking")
for static members such as a Singleton. By using a static initializer,
the creation of your Singleton member is thread safe. However the story
does not end there.

One common scenario I often run into is having what is effectively a
Singleton collection. For example, suppose you want to expose a
collection of all fifty states. This should never change, so you might
do something like this.

```csharp
public static class StateHelper
{
  private static readonly IList<State> _states = GetAllStates();

  public static IList<State> States
  {
    get
    {
      return _states;
    }
  }

  private static IList<State> GetAllStates()
  {
    IList<State> states = new List<State>();
    states.Add(new State("Alabama"));
    states.Add(new State("Alaska"));
    //...
    states.Add(new State("Wyoming"));
    return states;
  }
}
```

While this code works just fine, there is potential for a subtle bug to
be introduced in using this class. Do you see it?

The problem with this code is that any thread could potentially alter
this collection like so:

```csharp
StateHelper.States.Add(new State("Confusion"));
```

This is bad for a couple of reasons. First, we intend that this
collection be read-only. Second, since multiple threads can access this
collection at the same time, we can run into thread contention issues.

**The design of this class does not express the intent that this
collection is meant to be read-only.** Sure, we used the `readonly`
keyword on the private static member, but that means the
`variable reference` is read only. The actual collection the reference
points to can still be modified.

The solution is to use the generic `ReadOnlyCollection<T>` class. Here
is an updated version of the above class.

```csharp
public static class StateHelper
{
  private static ReadOnlyCollection<State> _states = GetAllStates();

  public static IList<State> States
  {
    get
    {
      return _states;
    }
  }

  private static ReadOnlyCollection<State> GetAllStates()
  {
    IList<State> states = new List<State>();
    states.Add(new State("Alabama"));
    states.Add(new State("Alaska"));
    //...
    states.Add(new State("Wyoming"));
    return new ReadOnlyCollection<State>(states);
  }
}
```

Now, not only is our intention expressed, but it is enforced.

Notice that In the above example, the static `States` property still
returns a reference of type `IList<State>` instead of returning a
reference of type `ReadOnlyCollection<State>`.

This is a concrete example of the [Decorator
Pattern](http://en.wikipedia.org/wiki/Decorator_pattern "Decorator Pattern")
at work. The `ReadOnlyCollection<T>` is a *decorator* to the `IList<T>`
class. It implements the `IList<T>` interface and takes in an existing
collection as a parameter in its contstructor.

In this case, if I had any client code already making use of the
`States` property, I would not have to recompile that code.

One drawback to this approach is that interface `IList<T>` contains
an `Insert` method. Thus the developer using this code can attempt to
add a `State`, which will cause a runtime error.

If this was a brand new class, I would probably make the return type of
the `States` property `ReadOnlyCollection<State>` which explicitly
implements the `IList<T>` and `ICollection<T>` interfaces, thus hiding
the `Add` and `Insert` methods (unless of course you explicitly cast it
to one of those interfaces). That way the intent of being a read-only
collection is very clear, as there is no way (in general usage) to even
attempt to add another state to the collection.

