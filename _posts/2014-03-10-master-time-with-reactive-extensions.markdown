---
layout: post
title: "Master time with Reactive Extensions"
date: 2014-03-10 08:58 -0700
comments: true
categories: [rx, software]
---

What would you do if you could stop time for everyone but yourself?

When I was a kid, I watched a TV movie called _The Girl, The Gold Watch, and Everything_ that explored this question. The main character, Kirby, inherits a very special gold watch from his Uncle that can stop time, but not for the bearer of the watch who is free to move around and troll people. Here's a clip from the movie where Kirby and his friend have a bit of fun with it.

[![The Girl, The Gold Watch, and Everything](https://f.cloud.github.com/assets/19977/2189430/ca7e9b00-9816-11e3-9bc3-062fbb4940b7.jpg)](http://www.youtube.com/watch?v=tY9sBATdA0Q)

This motif has been repeated in more recent movies as well. I often daydream about the shenanigans I could get into with such a device. If you had such a device, I'm sure you would do what I would do: use the device to write deterministic tests of asynchronous code of course!

Writing tests of asynchronous code can be very tricky. You often have to resort to calling `Thread.Sleep` or `Task.Delay` within an asynchronous callback so you can control the timing and assert what you need to assert.

For the most part, these are ugly hacks. What you really want is a way to control execution timing with fine grained control. You need a device like Kirby's golden watch.

Here's the good news. When you use Reactive Extensions (Rx), you have such a device at your disposal! Try not to get into too much trouble with it.

In the past, I've written how Rx can [reduce the cognitive load of asynchronous code through a declarative model](http://haacked.com/archive/2013/11/20/declare-dont-tell.aspx/). Rather than attempt to orchestrate all the interactions that must happen asynchronously at the right time, you simply describe the operations that need to happen and Reactive Extensions orchestrates everything for you.

This nearly eliminates race conditions and deadlocks while also reducing the cognitive load and potential for mistakes when writing asynchronous code.

Those are all amazing benefits of this approach, yet those aren’t even my favorite thing about Reactive Extensions. My favorite thing is how the abstraction allows me to bend time itself to my will when writing unit tests. FEEL THE POWER!

Everything in Rx is scheduled using schedulers. Schedulers are classes that implement the <a href="http://msdn.microsoft.com/en-us/library/system.reactive.concurrency.ischeduler(v=vs.103).aspx"><code>IScheduler</code> interface</a>. This simple, but powerful, interface contains a `Now` property as well as three `Schedule` methods for scheduling actions to be run.

## Control Time with the The `TestScheduler`

Rx provides the <a href="http://msdn.microsoft.com/en-us/library/microsoft.reactive.testing.testscheduler(v=vs.103).aspx"><code>TestScheduler class</code></a> (available in the [`Rx-Testing` NuGet package](http://www.nuget.org/packages/Rx-Testing/)) to give you absolute control over scheduling. This makes it possible to write deterministic repeatable unit tests.

Unfortunately, it's a bit of a pain to use as-is which is why [Paul Betts](http://paulbetts.org/) took it upon himself to write some useful `TestScheduler` extension methods available in the [`reactiveui-testing` NuGet package](http://www.nuget.org/packages/reactiveui-testing/). This library provides the `OnNextAt` method. We'll use this to create an observable that provides values at specified times.

The following test demonstrates how we can use the `TestScheduler`.

```csharp
[Fact]
public void SchedulerDemo()
{
    var sched = new TestScheduler();
    var subject = sched.CreateColdObservable(
        sched.OnNextAt(100, "m"), // Provides "m" at 100 ms
        sched.OnNextAt(200, "o"), // Provides "o" at 200 ms
        sched.OnNextAt(300, "r"), // Provides "r" at 300 ms
        sched.OnNextAt(400, "k")  // Provides "k" at 400 ms
    );

    string seenValue = null;
    subject.Subscribe(value => seenValue = value);

    sched.AdvanceByMs(100);
    Assert.Equal("m", seenValue);

    sched.AdvanceByMs(100);
    Assert.Equal("o", seenValue);

    sched.AdvanceByMs(100);
    Assert.Equal("r", seenValue);

    sched.AdvanceByMs(100);
    Assert.Equal("k", seenValue);
}

```

We start off by creating an instance of a `TestScheduler`. We then create an observable (`subject`) that provides four values at specific times. We subscribe to the observable and set the `seenValue` variable to whatever values the observable supplies us.

After we subscribe to the observable, we start to advance the scheduler's clock using the `OnNextAt` method. At this point, we are in control of time as far as the scheduler is concerned. Feel the power! The test scheduler is your gold watch.

Note that these are timings on a virtual clock. When you run this test, the code executes pretty much instantaneously. When you see `AdvanceByMs(100)`, the scheduler's clock advances by that amount, but your computer's real clock does not have to wait 100 ms. You could call `AdvanceByMs(99999999)` and that statement would execute instantaneously.

## Real World Example

Ok, that's neat. But let's see something that's a bit more real world. Suppose you want to kick off a search (as in an autocomplete scenario) when someone types in values into a text box. You probably don't want to kick off a search for every typed in value. Instead, you want to throttle it a bit. We'll write a method to do that that takes advantage of the <a href="http://msdn.microsoft.com/en-us/library/hh229400(v=vs.103).aspx"><code>Throttle</code> method</a>. From the MSDN documentation, the `Throttle` method:

> Ignores the values from an observable sequence which are followed by another value before due time with the specified `source`, `dueTime` and `scheduler`.

`Throttle` is the type of method you might use with a text field that does incremental search while you’re typing. If you type a set of characters quickly one after the other, you don’t want a separate HTTP request for each character to be made. You’d rather wait till there’s a slight pause before searching because the old results are going to be discarded anyways. Here's a super simple `Throttle` example that throttles values coming from some subject. No matter how quickly the subject produces values, the `Subscribe` callback will only see values every 10 milliseconds. 

```csharp
  subject.Throttle(TimeSpan.FromMilliseconds(10))
      .Subscribe(value => seenValue = value);
```



```csharp
public static IObservable<string> ThrottleTextBox(TextBox textBox, IScheduler scheduler)
{
    return Observable.FromEventPattern<TextChangedEventHandler, TextChangedEventArgs>(
        h => textBox.TextChanged += h,
        h => textBox.TextChanged -= h)
        .Throttle(TimeSpan.FromMilliseconds(400), scheduler)
        .Select(e => ((TextBox)e.Source).Text);
}
```

What we do here is use the <a href="http://msdn.microsoft.com/en-us/library/system.reactive.linq.observable.fromeventpattern(v=vs.103).aspx"><code>Observable.FromEventPattern</code> method</a> create an observable from the `TextChanged` event. If you're not used to it, the `FromEventPattern` method is kind of gnarly.

Once again, Paul Betts has your back with the very useful [`ReactiveUI-Events` package on NuGet](https://www.nuget.org/packages/reactiveui-events/). This package adds an `Events` extension method to most Windows controls that provides observable event properties. Here's the code rewritten using that. It's much easier to understand.

```csharp
public static IObservable<string> ThrottleTextBox(TextBox textBox, IScheduler scheduler)
{
    return textBox
        .Events()
        .TextChanged // IObservable<TextChangedEventArgs>
        .Throttle(TimeSpan.FromMilliseconds(400), scheduler)
        .Select(e => ((TextBox)e.Source).Text);
}
```

What we're doing here is creating a method that signals us when the text of the `TextBox` changes, but only after there's been no change for 400 milliseconds. It will then give us the full text of the text box.

Here's a unit test to make sure we wrote this correctly.

```csharp
[Fact]
public void TextBoxThrottlesCorrectly()
{
    var textBox = new TextBox();

    new TestScheduler().With(sched =>
    {
        string observed = null;
        ThrottleTextBox(textBox, sched)
            .Subscribe(value => observed = value);

        textBox.Text = "m";
        Assert.Null(observed);
        
        sched.AdvanceByMs(100);
        textBox.Text = "mo";
        Assert.Null(observed);
        
        textBox.Text = "mor";
        sched.AdvanceBy(399);  // Just about to kick off the throttle
        Assert.Null(observed);
        
        textBox.Text = "mork"; // But we changed it just in time.
        Assert.Null(observed);
        
        sched.AdvanceByMs(400); // Wait the throttle amount
        Assert.Equal("mork", observed);
    });
}
```

In this test, we're using the `With` extension method provided by `reactiveui-testing` package. This method takes in a lambda expression that provides us with a scheduler to pass into our Throttle method.

Within that lambda, I am once again in complete control of time. As you can see, I start advancing the clock here and there and changing the `TextBox`'s `Text` values. As you'd expect, as long as I don't advance the clock more than 400 ms in between text changes, the `ThrottleTextBox` observable won't give us any values.

But at the end, I go ahead and advance the clock by 400 ms after a text change and we finally get a value from the observable.

### Conclusion

The throttling of a `TextBox` (for autocomplete and search scenarios) is probably an overused and abused example for Rx, but there's a good reason for that. It's easy to grok and explain. But don't let that stop you from seeing the full power and potential of this technique.

It should be clear how this ability to control time makes it possible to write tests that can verify even the most complex asynchronous interactions in a _deterministic_ manner (cue ["mind blown"](https://github.com/Haacked/gifs/blob/master/mind-blown/Mind-Blown-Russell-Brand.gif)).

Unfortunately, and this next point is important, the `TestScheduler` doesn't extend into real life, so your shenanigans are limited to your asynchronous Reactive code. Thus, if you call `Thread.Sleep(1000)` in your test, that thread will really be blocked for a second. But as far as the test scheduler is concerned, no time has passed.

The good news is, with the `TestScheduler`, you generally don't need to call `Thread.Sleep` in your tests. There are many methods in Reactive Extensions for converting asynchronous calling patterns into Observables.

So the `TestScheduler` might not be as much fun as Kirby's golden watch, it should make writing and testing asynchronous code a whole lot more fun than it was in the past.
