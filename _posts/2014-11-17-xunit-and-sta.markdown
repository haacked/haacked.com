---
layout: post
title: "Unit tests that require the STA Thread"
date: 2014-11-17 -0800
comments: true
categories: [csharp xunit tdd wpf]
---

If you've ever written a unit test that instantiates a WPF control, you might have run into one of the following errors:

> The calling thread cannot access this object because a different thread owns it.

or

> The calling thread must be STA, because many UI components require this.

Prior to xUnit 2.0, we used a little hack to force a test to run on the STA Thread. You simply set the Timeout to 0.


__XUnit 1.9__

```csharp
[Fact(Timeout=0 /* This runs on STA Thread */)]
public void SomeTest() {...}
```

But due to the asynchronous all the way down design of XUnit 2.0, the `Timeout` property was removed. So what's a WPF Testing person to do?

Well, I decided to fix that problem by writing two custom attributes for tests:

* `STAFactAttribute`
* `STATheoryAttribute`

`STAFactAttribute` is the same thing as `FactAttribute` but it makes sure the test runs on the STA thread. Same goes for `STATheoryAttribute`. It's the same thing as `TheoryAttribute`.

I contributed this code to the [xunit/samples](https://github.com/xunit/samples) repository on GitHub. There's a lot of great examples in this repository that demonstrate how easy it is to extend XUnit to provide a nice custom experience.

## STA Thread

So you might be curious, what is an STA Thread? Stop with the curiosity. Some doors you do not want to open.

But you keep reading because you can't help yourself. STA stands for Single Threaded Apartment. Apparently this is where threads go when their parents kick them out of the house and they haven't found a life partner yet. They mostly sit in this apartment, ordering takeout and playing X-Box all day long.

STA Threads come into play when you interop with COM. Most of the time, as a .NET developer, you can ignore this. Unless you write WPF code in which case many of the controls you use depend on COM under the hood.

What is COM? Didn't I tell you this rabbit hole goes deep? COM stands for Component Object Model. It's an insanely complicated thing created by Don Box to subjugate the masses. At least that's what my history book tells me.

Ok, I sort of glossed over the STA part, didn't I. If you want to know more, check out the [Process, Threads, and Apartments](http://msdn.microsoft.com/en-us/library/ms693344\(VS.85\).aspx) article on MSDN.

Apartments are a way of controlling communication between objects on multiple threads. A COM object lives in an apartment and can directly communicate (call methods on) their roommates. Calls to objects in other apartments require involving the nosy busybodies of the object world, proxies.

> Single-threaded apartments consist of exactly one thread, so all COM objects that live in a single-threaded apartment can receive method calls only from the one thread that belongs to that apartment. All method calls to a COM object in a single-threaded apartment are synchronized with the windows message queue for the single-threaded apartment's thread. A process with a single thread of execution is simply a special case of this model.

In WPF, the UI loop is an example of this. UI components must be created on the main application thread and only invoked on that thread. UI components may look pretty, but they're all single.

For completeness, the alternative to STA is MTA or Multithreaded Apartments. This is where things get really interesting.

> Multithreaded apartments consist of one or more threads, so all COM objects that live in an multithreaded apartment can receive method calls directly from any of the threads that belong to the multithreaded apartment. Threads in a multithreaded apartment use a model called free-threading. Calls to COM objects in a multithreaded apartment are synchronized by the objects themselves.

Yes, threads that live in a multithreaded apartment are into this whole "free-threading" lifestyle. Make of it what you will.
