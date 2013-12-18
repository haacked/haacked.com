---
layout: post
title: "Declare Don&rsquo;t Tell"
date: 2013-11-20 -0800
comments: true
disqus_identifier: 18908
categories: [code,rx]
---
Judging by the reaction to my [Death to the If
statement](http://haacked.com/archive/2013/11/08/death-to-the-if-statement.aspx "Death to the If statement")
where I talked about the benefits of declarative code and reducing
control statements, not everyone is on board with this concept. That’s
fine, I don’t lose sleep over people being wrong.

[![Photo by Grégoire Lannoy CC BY 2.0](http://haacked.com/images/haacked_com/WindowsLiveWriter/DeclareDontTell_8BB0/megaphone_thumb.jpg "megaphone")](http://haacked.com/images/haacked_com/WindowsLiveWriter/DeclareDontTell_8BB0/megaphone_2.jpg)

My suspicion is that the reason people don’t have the “aha! moment” is
because examples of “declarative” code are too simple. This is
understandable because we’re trying to get a concept across, not write
the War and Peace of code. A large example becomes unwieldy to describe.

A while back, I tried to tackle this with an example using Reactive
Extensions. Imagine the code you would write to handle both the resize
and relocation of a window, where you want to save the position to disk,
but only after a certain interval has passed since the last of *either*
event.

So you resize the window, then before the interval has passed you move
the window. And only have you stop moving it and resizing it for this
interval, does it save to disk.

Set aside your typical developer bravado and think about what that code
looks like in a procedural or object oriented language. You functional
reactive programmers can continue to smirk smugly.

The code is going to be a bit gnarly. You will have to write bookkeeping
code such as saving the time of the last event so you can check that the
duration has passed. This is because you’re telling the computer how to
throttle.

With declarative code, you more or less declare what you want. “Hey!
Give me a throttle please!” (*Just because you are declaring, it doesn’t
mean you can’t be polite. I like to add a Please suffix to all my
methods*). And declarations are much easier to compose together.

This example is one I wrote about in my post [Make Async Your Buddy with
Reactive
Extensions](http://haacked.com/archive/2012/04/08/reactive-extensions-sample.aspx "Make Async Your Buddy").
But I made a mistake in the post. Here’s the code I showed as the end
result:

```csharp
Observable.Merge(
    Observable.FromEventPattern
      <SizeChangedEventHandler, SizeChangedEventArgs>
        (h => SizeChanged += h, h => SizeChanged -= h)
        .Select(e => Unit.Default),
    Observable.FromEventPattern<EventHandler, EventArgs>
        (h => LocationChanged += h, h => LocationChanged -= h)
        .Select(e => Unit.Default)
).Throttle(TimeSpan.FromSeconds(5), RxApp.DeferredScheduler)
.Subscribe(_ => this.SavePlacement());
```

I’ll give you a second to recover from USBS (Ugly Syntax Blindness
Syndrome).

The code isn’t incorrect, but there’s a lot of noise in here due to the
boilerplate expressions used to convert an event into an observable
sequence of events. I think this detracted from my point.

So today, I realized I should add a couple of really simple extension
methods that describe what’s going on and hides the boilerplate.

```csharp
// Returns an observable sequence of a framework element's
// SizeChanged events.
public static IObservable<EventPattern<SizeChangedEventArgs>> 
    ObserveResize(this FrameworkElement frameworkElement)
{
  return Observable.FromEventPattern
    <SizeChangedEventHandler, SizeChangedEventArgs>(
        h => frameworkElement.SizeChanged += h,
        h => frameworkElement.SizeChanged -= h)
      .Select(ep => ep.EventArgs);
}

// Returns an observable sequence of a window's 
// LocationChanged events.
public static IObservable<EventPattern<EventArgs>> 
    ObserveLocationChanged(this Window window)
{
  return Observable.FromEventPattern<EventHandler, EventArgs>(
      h => window.LocationChanged += h,
      h => window.LocationChanged -= h)
    .Select(ep => ep.EventArgs);
}
```

This then allows me to rewrite the original code like so:

```csharp
this.ObserveResize()
  .Merge(this.ObserveLocationChanged())
  .Throttle(TimeSpan.FromSeconds(5), RxApp.MainThreadScheduler)
  .Subscribe(_ => SavePlacement());
```

That code is much easier to read and understand what’s going on and
avoids the plague of USBS (unless you’re a Ruby developer in which case
you have a high sensitivity to USBS).

The important part is we don’t have to maintain tricky bookkeeping code.
There’s no code here that keeps track of the last time we saw one or the
other event. Here, we just declare what we want and Reactive Extensions
handles the rest.

This is what I mean by declare, don’t tell. We don’t tell the code how
to do its job. We just declare what we need done.

*UPDATE:* ReactiveUI (RxUI) 5.0 has an assembly *Reactive.Events* that
maps every event to an observable for you! For example:

```csharp
control.Events()
  .Clicked
  .Subscribe(_ => Console.WriteLine("foo"));
```

That makes things much easier!

