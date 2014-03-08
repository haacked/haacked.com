---
layout: post
title: "Master time with Reactive Extensions"
date: 2014-02-14 21:09 -0800
comments: true
categories: [rx, software]
---

What would you do if you could stop time for everyone but yourself?

When I was a kid, I watched a TV movie called The Girl, The Gold Watch, and Everything that explored this question. The main character, Kirby, inherits a very special gold watch from his Uncle that can stop time. With time frozen, the bearer of the watch could move around and affect his or her surrounds. Here's a clip from the movie where Kirby and his friend have a bit of fun with it.

[![The Girl, The Gold Watch, and Everything](https://f.cloud.github.com/assets/19977/2189430/ca7e9b00-9816-11e3-9bc3-062fbb4940b7.jpg)](http://www.youtube.com/watch?v=tY9sBATdA0Q)

This motif has been repeated in more recent movies as well. I often daydream about the shenanigans I could get into with such a device. If you had such a device, I'm sure you'd do what I would do—use the device to write deterministic tests of asynchronous code of course!

Here's the good news. When you use Reactive Extensions (Rx), you have such a device at your disposal!

In the past, I've written how Rx can [reduce the cognitive load of asynchronous code through a declarative model](http://haacked.com/archive/2013/11/20/declare-dont-tell.aspx/). Rather than attempt to orchestrate all the interactions that must happen asynchronously at the right time, you simply describe the operations that need to happen and Reactive Extensions orchestrates everything for you.

This nearly eliminates race conditions and deadlocks while also reducing the cognitive load and potential for mistakes when writing asynchronous code.

Those are all amazing benefits of this approach, yet those aren’t even my favorite thing about Reactive Extensions. My favorite thing is how the abstraction allows me to bend time itself to my will when writing unit tests. FEEL THE POWER!

Everything in Rx is scheduled using schedulers. Schedulers are classes that implement the [`IScheduler` interface](http://msdn.microsoft.com/en-us/library/system.reactive.concurrency.ischeduler(v=vs.103).aspx). This simple, but powerful, interface contains a `Now` property as well as three `Schedule` methods for scheduling actions to be run.

## Control Time with the The `TestScheduler`

Rx provides the [`TestScheduler` class](http://msdn.microsoft.com/en-us/library/microsoft.reactive.testing.testscheduler(v=vs.103).aspx) (available in the [`Rx-Testing` NuGet package](http://www.nuget.org/packages/Rx-Testing/)) to give you absolute control over timing making it possible to write deterministic repeatable unit tests.

In the following simple example, I’m going to write a unit test of the existing `Throttle` method. Here’s the description of what it does from MSDN:

Ignores the values from an observable sequence which are followed by another value before due time with the specified `source`, `dueTime` and `scheduler`.

`Throttle` is the type of method you might use with a text field that does incremental search while you’re typing. If you type a set of characters quickly one after the other, you don’t want a separate HTTP request for each character to be made. You’d rather wait till there’s a slight pause before searching because the old results are going to be discarded anyways. Here's a super simple `Throttle` example that throttles values coming from some subject. No matter how quickly the subject produces values, the `Subscribe` callback will only see values every 10 milliseconds. 

```csharp
  subject.Throttle(TimeSpan.FromMilliseconds(10))
      .Subscribe(value => seenValue = value);
```

As I mentioned before, you can use the `TestScheduler` from the `Rx-Testing` package to control the timing of observables for testing purposes. However, it's a bit of a pain to use as-is which is why [Paul Betts](http://paulbetts.org/) took it upon himself to write some useful `TestScheduler` extension methods available in the [`reactiveui-testing` NuGet package](http://www.nuget.org/packages/reactiveui-testing/). This library provides the `OnNextAt` method. We'll use this to create an observable that provides values at specified times.

The following test simply shows how we can create an observable that fires at specific times and how we can test that it does the right thing.

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

We start off by creating an instance of a `TestScheduler`. We then create a subject (observable) that provides four values. Using the `OnNextAt` method we can control when the values are provided by the scheduler. Not that these are timings on a virtual clock. The code in this test runs pretty much instantaneously.

After we create the observable, we subscribe to it and then start advancing the clock to ensure that the scheduler fires at the appropriate time.

## Real World Example

Ok, that's neat. But let's see something that's a bit more real world. Suppose you want to kick off a search when someone times in values into a text box. You probably don't want to kick off a search for every typed in value. Instead, you want to throttle it a bit. Here's a method that does that.

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

What we do here is use the [`Observable.FromEEventPattern`](http://msdn.microsoft.com/en-us/library/system.reactive.linq.observable.fromeventpattern(v=vs.103).aspx) method create an observable from the `TextChanged` event. If you're not used to it, the `FromEventPattern` method is kind of gnarly.

Once again, Paul Betts has your back with the very useful [`ReactiveUI-Events` package on NuGet](https://www.nuget.org/packages/reactiveui-events/). This package adds an `Events` extension method to most Windows controls that provides observable event properties. Here's the code rewritten using that. It's much easier to understand.

```csharp
public static IObservable<string> ThrottleTextBox(TextBox textBox, IScheduler scheduler)
{
    return textBox
        .Events()
        .TextChanged
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

Within that lambda, I am in complete control of time. As you can see, I start advancing the clock here and there and changing the `TextBox`'s `Text` values. As you'd expect, as long as I don't advance the clock more than 400 ms in between text changes, the `ThrottleTextBox` observable won't give us any values.

But at the end, I go ahead and advance the clock by 400 ms after a text change and we finally get a value from the observable.

### Conclusion

The throttling of a `TextBox` (for autocomplete and search scenarios) is probably an overused and abused example for Rx, but there's a good reason for that. It's easy to grok and explain. But don't let that stop you from seeing the full power and potential of this technique.

It's hopefully self evident how this ability to control time makes it possible to write tests that can verify even the most complex asynchronous interactions in a _deterministic_ manner (cue ["mind blown"](https://github.com/Haacked/gifs/blob/master/mind-blown/Mind-Blown-Russell-Brand.gif)).
