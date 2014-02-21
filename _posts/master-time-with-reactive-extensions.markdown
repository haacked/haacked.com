---
layout: post
title: "Master time with Reactive Extensions"
date: 2014-02-14 21:09 -0800
comments: true
categories: [rx, software]
---

When I was a kid, there was this TV movie called The Girl, The Gold Watch, and Everything where the main character, Kirby, inherits a very special gold watch from his Uncle that can stop time. With time frozen, the bearer of the watch was free to move around and affect his or her surrounds. Here's a clip from the movie where Kirby and his friend have a bit of fun with it.

[![The Girl, The Gold Watch, and Everything](https://f.cloud.github.com/assets/19977/2189430/ca7e9b00-9816-11e3-9bc3-062fbb4940b7.jpg)](http://www.youtube.com/watch?v=tY9sBATdA0Q)

This motif has been repeated in more recent movies as well. I remember being fascinated by the idea. I'd daydream about the shenanigans I could get involved in with such a device. I'm sure you're thinking the same thing I am. I'd use the device to write deterministic tests of asynchronous code of course!

Well fortunately, when you use Reactive Extensions (Rx), you have such a device!

In the past, I've written how Rx can [reduce the cognitive load of asynchronous code through a declarative model](http://haacked.com/archive/2013/11/20/declare-dont-tell.aspx/). Rather than attempt to orchestrate all the interactions that must happen asynchronously at the right time, you simply describe the operations that need to happen and Reactive Extensions orchestrates everything for you.

This nearly eliminates race conditions and deadlocks while also reducing the cognitive load and potential for mistakes when writing asynchronous code.

Those are all amazing benefits of this approach, yet those aren’t even my favorite thing about Reactive Extensions. My favorite thing is how the abstraction allows me to bend time itself to my will when writing unit tests. FEEL THE POWER!

Everything in Rx is scheduled using schedulers. Schedulers are classes that implement the [`IScheduler` interface](http://msdn.microsoft.com/en-us/library/system.reactive.concurrency.ischeduler(v=vs.103).aspx). Rx provides the [`TestScheduler` class](http://msdn.microsoft.com/en-us/library/microsoft.reactive.testing.testscheduler(v=vs.103).aspx) (available in the [`Rx-Testing` NuGet package](http://www.nuget.org/packages/Rx-Testing/)) to give you absolute control over timing making it possible to write deterministic repeatable unit tests.

In the following simple example, I’m going to write a unit test of the existing `Throttle` method. Here’s the description of what it does from MSDN:

Ignores the values from an observable sequence which are followed by another value before due time with the specified `source`, `dueTime` and `scheduler`.

`Throttle` is the type of method you might use with a text field that does incremental search while you’re typing. If you type a set of characters quickly one after the other, you don’t want a separate HTTP request for each character to be made. You’d rather wait till there’s a slight pause before searching because the old results are going to be discarded anyways. Here's a super simple `Throttle` example that throttles values coming from some subject. No matter how quickly the subject produces values, the `Subscribe` callback will only see values every 10 milliseconds. 

```csharp
  subject.Throttle(TimeSpan.FromMilliseconds(10))
      .Subscribe(value => seenValue = value);
```

As I mentioned before, you can use the `TestScheduler` from the `Rx-Testing` package to control the timing of observables for testing purposes. However, it's a bit of a pain to use as-is which is why Paul Betts took it upon himself to write some useful `TestScheduler` extension methods available in the [`reactiveui-testing` NuGet package](http://www.nuget.org/packages/reactiveui-testing/). This library provides the `OnNextAt` method. We'll use this to create an observable that provides values at specified times.

```csharp
[Fact]
public void Test()
{
  var sched = new TestScheduler();
  var subject = sched.CreateColdObservable(
      sched.OnNextAt(500, "m"),  // Provides "m" at 500 ms
      sched.OnNextAt(1000, "o"), // Provides "o" at 1000 ms
      sched.OnNextAt(2000, "r"), // Provides "r" at 2000 ms
      sched.OnNextAt(3000, "k")  // Provides "k" at 3000 ms
  );

  string seenValue = null;
  subject.Throttle(TimeSpan.FromSeconds(4), sched)
      .Subscribe(value => seenValue = value);

  sched.AdvanceByMs(3000);
  Assert.Null(seenValue); // Only 3 seconds has passed. Throttle hasn't kicked.
  sched.AdvanceByMs(1000);
  Assert.Equal("e", seenValue); // 4 seconds has passed, so we should see the last value
}
```

We start off by creating an instance of a `TestScheduler`. We then create a subject (observable) that provides four values. Using the `OnNextAt` method we can control when the values are provided by the scheduler. Not that these are timings on a virtual clock. The code in this test runs pretty much instantaneously.