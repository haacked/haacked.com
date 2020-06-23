---
title: Make Async Your Buddy With Reactive Extensions
tags: [code,rx]
redirect_from: "/archive/2012/04/08/reactive-extensions-sample.aspx/"
---

For a long time, good folks like [Matt
Podwysocki](http://weblogs.asp.net/podwysocki/ "Matt Podwysocki") have
extolled the virtues of Reactive Extensions (aka Rx) to me. It piqued my
interest enough for me to write a [post about
it](https://haacked.com/archive/2010/03/26/enumerating-future.aspx "Querying the future"),
but that was the extent of it. It sounded interesting, but it didn’t
have any relevance to any projects I had at the time.

Fortunately, now that I work at GitHub I have the pleasure to work with
an Rx Guru, [Anaïs Betts](https://blog.anaisbetts.org, "Anaïs Betts"), on a
project that actively uses Rx. And man, is my mind blown by Rx.

Hits Me Like A Hurricane
------------------------

What really blew me away about Rx is how it allows you to handle complex
async interactions declaratively. No need to chain callbacks together or
worry about race conditions. With Rx, you can easily compose multiple
async operations together. It’s powerful.

The way I describe it to folks is to think of how the `IEnumerable` and
`IEnumerator` are involved when iterating over an enumerable. Now take
those and reverse the polarity. That’s Rx. But with Rx, the
`IObservable` and `IObserver` interfaces are involved and rather than
enumerate over existing sequences, you write queries against sequences
of future events.

Hear that? That’s the sound of my head asploding again.

[![the-future](https://haacked.com/images/haacked_com/WindowsLiveWriter/EnumeratingtheFutureWithTheReactiveFrame_1263C/the-future_3.jpg "the-future")](http://www.sxc.hu/photo/1194467 "Shimmering lights 1 - by e-Eva-a")

Rx has a tendency to twist and contort the mind in strange ways. But
it’s really not all that complicated. It only hurts the head at first
because it’s a new way to think about async, sequences, and queryies for
many folks.

Here’s a simple example that helps demonstrate the power of Rx. Say
you’re writing a client app (such as a WPF application) and want to save
the application to persist its window’s position and size. That way, the
next time the app starts, the position is restored.

How you save the position isn’t so important, but if you’re curious, I
found this post, [Saving window size and location in WPF and
WinForms](http://blogs.msdn.com/b/davidrickard/archive/2010/03/09/saving-window-size-and-location-in-wpf-and-winforms.aspx "Window Placement"),
helpful.

I modified it in two ways for my needs. First, I replaced the Settings
object with an asynchronous cache as the storage for the placement info.

I then changed it to save the placement info when the window is resized,
rather than when the application exits. That way, if the app crashes, it
won’t forget its last position.

Handling Resize Events
----------------------

So let’s think about this a bit. When you resize a window, the resize
event might be fired a large number of times. We probably don’t want to
save the position on every one of those calls. It’s not just a
performance problem, but it could be a data corruption problem if I’m
using an async method to save the placement. It might be possible for a
later call to occur before an earlier call when so many happen so close
together.

What we really want to do is save the setting when there’s a pause
during a resize operation. For example, a user starts to resize the
window, then stops. Five seconds later, if there’s been no other resize
operation, only then do we save the setting.

How would you do this with traditional code? You could probably figure
it out, ut it’d be ugly. Perhaps have the resize event start a timer for
five seconds, if it isn’t started already. Each subsequent event would
reset the timer. When the timer finishes, it saves the setting and turns
itself off. The code is going to be a bit gnarly and all over the place.

Here’s what it looks like with Rx.

```csharp
Observable.FromEventPattern<SizeChangedEventHandler, SizeChangedEventArgs>
    (h => SizeChanged += h, h => SizeChanged -= h)
    .Throttle(TimeSpan.FromSeconds(5), RxApp.DeferredScheduler)
    .Subscribe(_ => this.SavePlacement());
```

That’s it! Nice and self contained in a single expression.

Let’s break it down a bit.

```csharp
Observable.FromEventPattern<SizeChangedEventHandler, SizeChangedEventArgs>
    (h => SizeChanged += h, h => SizeChanged -= h)
```

This first part of the expression converts the `SizeChangedEvent` into
an observable. The specific type of this observable is
`IObservable<EventPattern<SizeChangedEventArgs>>`. This is analogous to
an `IEnumerable<EventPattern<SizeChangedEventArgs>>`, but with its
polarity reversed. Having an observable will allow us to subscribe to a
stream of size changed events. But first:

```csharp
.Throttle(TimeSpan.FromSeconds(5), RxApp.DeferredScheduler)
```

This next part of the expression uses the [Throttle
method](http://msdn.microsoft.com/en-us/library/hh229298(v=vs.103).aspx "Throttle Method on MSDN")
to throttle the sequence of events coming from the observable. It will
ignore events in the sequence if a newer one arrives within the
specified time span. In other words, this observable won’t return any
item until there’s a five second lull in events.

The `RxApp.DeferredScheduler` comes from the ReactiveUI framework and is
equivalent to new `DispatcherScheduler(Application.Current.Dispatcher)`.
It indicates which scheduler to run the throttle timers on. In this
case, we indicate the dispatcher scheduler which runs the throttle timer
on the UI thread.

```csharp
.Subscribe(_ => this.SavePlacement());
```

And we end with the `Subscribe` call. This method takes in an `Action`
to run for each item in the observable sequence when it arrives. This is
where we do the work to actually save the window placement.

Putting it all together, every time a resize event is succeeded by a
five second lull, we save the placement of the window.

But wait, compose more
----------------------

Ok, that’s pretty cool. But to write imperative code to do this would be
slightly ugly and not all that hard. Ok, let’s up the stakes a bit,
shall we?

We forgot something. You don’t just want to save the placement of the
window when it’s resized. You also want to save it when it’s moved.

So we really need to observe **two sequences** of events, but still
throttle both of them as if they were one sequence. In other words, when
either a resize or move event occurs, the timer is restarted. And only
when five seconds have passed since either event has occurred, do we
save the window placement.

The traditional way to code this is going to be very ugly.

**This is where Rx shines.** Rx provides ways to compose observables in
very interesting ways. In this case we’ll deal with two observables, the
one we already created that handles `SizeChanged` events, and a new one
that handles `LocationChanged` events.

Here’s the code for the `LocationChanged` observable. I’ll save the
observable into an intermediate variable for clarity. It’s exactly what
you’d expect.

```csharp
var locationChanges = Observable.FromEventPattern<EventHandler, EventArgs>
  (h => LocationChanged += h, h => LocationChanged -= h);
```

I’ll do the same for the `SizeChanged` event.

```csharp
var sizeChanges = Observable.FromEventPattern
    <SizeChangedEventHandler, SizeChangedEventArgs>
    (h => SizeChanged += h, h => SizeChanged -= h);
```

We can use the `Observable.Merge` method to merge these sequences into a
single sequence. But going back to the `IEnumerable` analogy, these are
both sequences of different types. If you had two enumerables of
different types and wanted to combine them into a single enumerable,
what would you do? You’d apply a transformation with the `Select`
method! And that’s what we do here too.

Since I don’t care what the event arguments are, just **when** they
arrive, I’ll transform each sequence into an `IObservable<Unit.Default>`
by calling `Select(_ => Unit.Default)` on each observable. `Unit` is an
Rx type that indicates there’s no information. It’s like returning
`void`.

```csharp
var merged = Observable.Merge(
    sizeChanges.Select(_ => Unit.Default), 
    locationChanges.Select(_ => Unit.Default)
);
```

I’ll then call `Observable.Merge` to merge the two sequences together
into a single sequence of event args.

Now, with this combined sequence, I can simply apply the same throttle
and subscription I did before.

```csharp
merged
    .Throttle(TimeSpan.FromSeconds(5), RxApp.DeferredScheduler)
    .Subscribe(_ => this.SavePlacement());
```

Think about that for a second. I was able to compose various sequences
of events and into a single observable and I didn’t have to change the
code to throttle the events or to subscribe to them.

As you get more familiar with Rx, it starts to get easier to read the
code and you tend to use less intermediate variables. Here’s the full
more idiomatic expression.

```csharp
Observable.Merge(
    Observable.FromEventPattern<SizeChangedEventHandler, SizeChangedEventArgs>
        (h => SizeChanged += h, h => SizeChanged -= h)
        .Select(e => Unit.Default),
    Observable.FromEventPattern<EventHandler, EventArgs>
        (h => LocationChanged += h, h => LocationChanged -= h)
        .Select(e => Unit.Default)
).Throttle(TimeSpan.FromSeconds(5), RxApp.DeferredScheduler)
.Subscribe(_ => this.SavePlacement());
```

That single declarative expression handles so much crazy logic. Very
powerful stuff.

Even if you don’t write WPF apps, there’s still probably something
useful here for you. This same powerful approach is [also available for
JavaScript](http://codebetter.com/matthewpodwysocki/2010/02/16/introduction-to-the-reactive-extensions-to-javascript/ "Reactive Extensions for JavaScript").

See it in action
----------------

I put together a really rough sample app that demonstrates this concept.
It’s not using the async cache, but it is using Rx to throttle resize
and move events and then save the placement of the window after five
seconds.

Just grab the *WindowPlacementRxDemo* project from my [CodeHaacks GitHub
repository](https://github.com/Haacked/CodeHaacks "CodeHaacks on GitHub.").

More Info
---------

For more info on Reactive Extensions, I recommend the following:

-   A recent [.NET Rocks episode with Bart de Smet on
    Rx](http://www.dotnetrocks.com/default.aspx?showNum=756 ".NET Rocks Episode 756")
-   A very [detailed blog post by Bart on Rx 2.0
    Beta](http://blogs.msdn.com/b/rxteam/archive/2012/03/12/reactive-extensions-v2-0-beta-available-now.aspx "Rx 2.0 Beta")
    with details on how this works with the await keyword.

