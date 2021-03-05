---
title: Querying the Future With Reactive Extensions
tags: [code,rx]
redirect_from: "/archive/2010/03/25/enumerating-future.aspx/"
---

UPDATE: After an email exchange with Eric Meijer, I learned that I was a
bit *imprecise* in this treatment. Or, as the colloquial term goes,
“wrong”. :) I’ve changed the title to reflect more accurately what
Reactive extensions provide.

Iterating over a collection of items seems like a pretty straightforward
mundane concept. I don’t know about you, but I don’t spend the typical
day thinking about the mechanics of iteration, much like I don’t spend a
lot of time thinking about how a roll of toilet paper is made. At least
I didn’t until watching [Elmo Potty
Time](http://www.amazon.com/gp/product/B000G0O5F0?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=390957&creativeASIN=B000G0O5F0 "Elmo Potty Time on Amazon.com")
with my son. Now I think about it all the time, but I digress.

[![the-future](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/EnumeratingtheFutureWithTheReactiveFrame_1263C/the-future_3.jpg "the-future")](http://www.sxc.hu/photo/1194467 "Shimmering lights 1 - by e-Eva-a")Historically,
I’ve always thought of iteration as an action over a static set of
items. You have this collection of elements, perhaps a snapshot of data,
and you then proceed to grab a reference to each one in order and do
something with that reference. What you do with it is your business. I’m
not going to pry.

It wasn’t till the yield operator was introduced into C# that I
realized this was a very limited view of iteration. For example, using
the yield operator makes it easy to enumerate over computed sets, as
demonstrated by [iterating over the Fibonacci
sequence](http://chrisfulstow.com/fibonacci-numbers-iterator-with-csharp-yield-statements/ "Fibonacci numbers iterator with C# yield statements").
In this case, the set of elements being iterated is not a static set.

### Reactive Extensions

Recently, [Matt “is his middle name really not F\#”
Podwysocki](http://codebetter.com/blogs/matthew.podwysocki/ "Matt Podysocki blog")
swung by my office to show me yet another way of thinking about
iterations via the [Reactive Extensions to
JavaScript](http://codebetter.com/blogs/matthew.podwysocki/archive/2010/02/16/introduction-to-the-reactive-extensions-to-javascript.aspx "Introduction to Reactive Extensions to JavaScript").
These extensions are based on the same concept applied in the [Reactive
Extensions for
.NET](http://msdn.microsoft.com/en-us/devlabs/ee794896.aspx "Reactive Extensions for .NET")
which I’ve sadly ignored until now.

There’s a channel9 video where [Eric Meijer describes these
extensions](http://channel9.msdn.com/posts/Charles/Erik-Meijer-Rx-in-15-Minutes/ "Video: Reactive Extensions in 14 minutes")
as push collections, as contrasted with normal collections where you
pull each item from the collection.

Unfortunately, when I first heard this analogy, it didn’t click in my
head. That’s not terribly unusual as it often takes a few bat swings at
my head for something to stick. It wasn’t till I understood the pattern
of code that reactive extensions are a replacement for, did it click. By
inverting the analogy that Eric used, these extensions made a lot more
sense to me.

Typically, when you write code to handle user interactions, you write
events and methods (event handlers) which handle the events. In my mind,
this is a very “push” way to handle it. For example, as soon as a user
moves the mouse over an element you’re interested in, a *mouseover*
event gets pushed to your *mouseover* event handler method.

Reactive extensions inverts this model by taking what I would call a
“pull” model of events. Using these extensions, you can treat the
sequence of user events (such as the sequence of mouse over events) as
if it were a normal collection (well actually, as an enumeration). Thus
you can write LINQ queries over the collection which do things like
filtering, grouping, composing, etc.

Your code really looks like it’s dealing with a fully “populated”
collection, even though elements of that collection may not have
occurred yet.

Effectively, you’re ~~enumerating~~ querying over the future.

The mental shift for me is to realize that we’re actually working with
sequences being “pushed” into our query in this case and not queries
running over already populated collections.

Speaking of keyboard presses, Matt Podwysocki took my [Live Preview
jQuery
Plugin](https://haacked.com/archive/2009/12/15/live-preview-jquery-plugin.aspx "Live Preview jQuery Plugin")
and ported it to use the Reactive Extensions for JavaScript. You can see
[a demo of it in action
here](http://demo.haacked.com/livepreview-rx/ "Live Preview with Rx")
(view source for the code).

The snippet that’s pretty cool to me is the following:

```csharp
textarea
  .ToObservable("keyup")
  .Take(1)
  .SelectMany(function() {
  return Rx.Observable.Start(function() {
    return textarea.reloadPreview(); });
  }).Repeat()
.Subscribe(function() {});
```

As Matt told me, if you squint hard enough, it looks like you’re writing
a LINQ query in JavaScript. :)

