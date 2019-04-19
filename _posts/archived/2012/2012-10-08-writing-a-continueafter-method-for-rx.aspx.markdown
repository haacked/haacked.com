---
title: Writing a ContinueAfter method for Rx
date: 2012-10-08 -0800
tags: [code]
- rx
redirect_from: "/archive/2012/10/07/writing-a-continueafter-method-for-rx.aspx/"
---

With Reactive Extensions you sometimes need one observable sequence to
run after another observable sequence completes. Perhaps the first one
has side effects the second one depends on. Egads! I know, side effects
are evil in this functional world, but it happens.

Let’s make this more concrete with some contrived sample code.

```csharp
public static class BlogDemo
{
  public static IObservable<int> Extract()
  {
    return new[] { 10, 20, 70, 100 }.ToObservable();
  }

  public static IObservable<string> GetStuffs()
  {
    return new[] { "Family Guy", "Cheetos", "Rainbows" }.ToObservable();
  }
}
```

Here we have a class with two methods. The first method extracts
something and returns a sequence of progress indicators. The second
method returns an observable sequence of strings (good stuffs I hope).
With me so far?

Now suppose I need to write a third method, let’s call it
`GetStuffsAfterExtract`. This method needs to run `Extract` , and only
when that is complete, return the sequence from `GetStuffs`. How would
we approach this?

Well, in the Task based world, `Extract` would probably return a
`Task<T>` instead of a observable. `Task<T>` represents a single future
value as opposed to a sequence of future values. If we did that, we
could use the `ContinueWith` method (or simply use the `await` keyword
it in .NET 4.5).

But I don’t live in that world. I live in shiny happy RxLand where we
always deal with sequences. Future sequences! It’s an awesome zany
world.

Note that this method I want to write doesn’t care about the actual
sequence of values from `Extract`. All I care to know is when it’s
complete and then it will return the sequence of values from `GetStuff`.

Here’s one way to do it with `Observable.Start`:

```csharp
public static IObservable<string> GetStuffsAfterExtract()
{
    return Observable.Start(() =>
    {
        Extract().Wait(); 
        return GetStuffs();
    }).Merge();
}
```

This works, but it’s not optimal. The use of `Observable.Start`
guarantees we’ll have a context switch to the TaskPool rather than doing
the operation on the current thread.

Also, it’s ugly.

Let’s try again:

```csharp
public static IObservable<string> GetStuffsAfterExtract()
{
  return Extract().TakeLast(1).SelectMany(_ => GetStuffs());
}
```

A little better. This works pretty well in most situations. If you’re
wondering what the underscore character is in the `SelectMany`
statement, that’s the name for lambda parameters I use to indicate that
the parameter is not needed. It’s a convention I learned from someone a
while back on the ASP.NET team. It makes my intention to not use it in
the expression clear..

But what happens if there’s a case where `Extract` legitimately returns
an empty observable, aka `Observable.Empty<int>()`. In this case, I
could just change it to not do that since I wrote it. But maybe I’m
calling a method written by someone else and we don’t trust that person
to do everything perfect like me.

Well `GetStuffs` will never get called because `SelectMany` projects
each element of the second sequence onto the first. If there are no
elements in the first, there’s nothing for it to do. Hey, maybe that’s
exactly what you want!

But that’s not what I want in this case. So with the help of my
co-worker [Paul “Rx Master of Disaster”
Betts](http://paulbetts.org/ "Paul Betts"), we went back and forth
through several iterations.

I figured the first step is to simply write a method that represents the
completion of an observable sequence whether it’s empty or not. I’ll
call this method `AsCompletion` and it’ll return a new sequence with a
single Unit.Default when the original sequence is complete.  It turns
out that [the Aggregate
method](http://msdn.microsoft.com/en-us/library/system.reactive.linq.observable.aggregate(v=vs.103).aspx "Aggregate")
is great for this (just like in standard Linq!):

```csharp
public static IObservable<Unit> AsCompletion<T>(this IObservable<T> observable)
{
  return observable.Aggregate(Unit.Default, (accumulation, _) => accumulation);
}
```

`Aggregate` is typically used to aggregate a sequence into a single
value. It’s also known as an accumulation function. But in this case, I
don’t care about any of the individual values. I simply keep returning
the accumulation unchanged.

The first parameter to `Aggregate` is a seed value. Since I seeded that
accumulation with a `Unit.Default`, it’ll keep returning that same
value.

In other words, this method will return a sequence of exactly one
`Unit.Default` when the sequence it’s called upon is complete whether
it’s empty or not. Cool.

Now I can use this to build my `ContinueAfter` method (I didn’t name it
`ContinueWith` because we don’t actually do anything with the previous
values and I want to make sure it’s clear we’re talking about doing work
after the sequence is complete and not as things come in).

```csharp
public static IObservable<TRet> ContinueAfter<T, TRet>(
  this IObservable<T> observable, Func<IObservable<TRet>> continuation)
{
  return observable.AsCompletion().SelectMany(_ => continuation());
}
```

You’ll notice that the body of this method looks pretty similar to my
first attempt, but instead of `TakeLast(1)` I’m just using the
`AsCompletion` method.

With this method in place, I can rewrite the code I set out to write as:

```csharp
public static IObservable<string> GetStuffsAfterExtract()
{
    return Extract().ContinueAfter(GetStuffs);
}
```

That is much more lickable code. One nice thing I like about this method
is it takes in a parameterless `Func`. That makes it very clear that it
won’t pass in a value to your expression that would then want to ignore
and in this case allows me to pass in a method group.

I write this full well knowing someone who’s a much better Rx master
than myself will point out an even better approach. I welcome it! For
now, this is working pretty well for me.

Oh, I almost forgot. I posted [the unit tests I have so far for this
method](https://gist.github.com/3855403 "ContinueAfter unit tests") as a
gist.

**UPDATE 10/9/2012** Just as I expected, folks chimed in with better
ideas. Some asked why didn’t I just use `Concat` since it’s perfect for
this. The funny thing is I did think about using it, but I dismissed it
because it requires both sequences to be of the same type, as someone
pointed out in my comments.

But then it occurred to me, I’m using Rx. I can transform sequences! So
here’s my new `ContinueAfter` implementation.

```csharp
public static IObservable<TRet> ContinueAfter<T, TRet>(
  this IObservable<T> observable
  , Func<IObservable<TRet>> continuation)
{
  return observable.Select(_ => default(TRet))
    .IgnoreElements()
    .Concat(continuation());
}
```

I also updated the `AsCompletion` method since I use that in other
places.

```csharp
public static IObservable<Unit> AsCompletion<T>(this IObservable<T> observable)
{
    return observable.Select(_ => Unit.Default)
        .IgnoreElements()
        .Concat(Observable.Return(Unit.Default));
}
```

Please note that I didn’t have to change `ContinueAfter`. I could have
just changed `AsCompletion` and I would have been ok. I just changed it
here to show I could have written this cleanly with existing Rx
operators. Also, and I should test this later, it’s probably more
efficient to have one `Concat` call than two.

I added another unit test to the gist I mentioned that makes sure that
the second observable doesn’t run if the first one has an exception. If
you still want it to run, you can catch the exception and do the right
thing.

**UPDATE 10/10/212** Ok, after some real world testing, I’m finding
issues with the `Concat` approach. Another commenter, Benjamin, came
forward with the most straightforward approach. It’s one I originally
had, to be honest, but wanted to try it in a more “functional” approach.
But what I’m doing is definitely not functional as I’m dealing with side
effects.

Here’s my final (hopefully) implementation.

```csharp
public static IObservable<Unit> AsCompletion<T>(this IObservable<T> observable)
{
  return Observable.Create<Unit>(observer =>
  {
    Action onCompleted = () =>
    {
      observer.OnNext(Unit.Default);
      observer.OnCompleted();
    };
    return observable.Subscribe(_ => {}, observer.OnError, onCompleted);
  });
}

public static IObservable<TRet> ContinueAfter<T, TRet>(
  this IObservable<T> observable, Func<IObservable<TRet>> selector)
{
  return observable.AsCompletion().SelectMany(_ => selector());
}
```

