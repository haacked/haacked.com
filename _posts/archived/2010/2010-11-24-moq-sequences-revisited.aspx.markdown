---
title: Moq Sequences Revisited
date: 2010-11-24 -0800
tags:
- tdd
- code
redirect_from: "/archive/2010/11/23/moq-sequences-revisited.aspx/"
---

A while back I wrote about [mocking successive
calls](https://haacked.com/archive/2009/09/29/moq-sequences.aspx "Successive Method Calls With Moq")
to the same method which returns a sequence of objects. Read that post
for more context.

In that post, I had written up an implementation, but quickly was won
over by a better extension method implementation from [Fredrik
Kalseth](http://iridescence.no/ "Fredrik Kalseth").

```csharp
public static class MoqExtensions
{
  public static void ReturnsInOrder<T, TResult>(this ISetup<T, TResult> setup, 
    params TResult[] results) where T : class  {
    setup.Returns(new Queue<TResult>(results).Dequeue);
  }
}
```

As good as this extension method is, I was able to improve on it today
during a coding session. I was writing some code where I needed the
second call to the same method to throw an exception and realized this
extension wouldn’t allow for that.

However, it wasn’t hard to write an overload that allows for that.

```csharp
public static void ReturnsInOrder<T, TResult>(this ISetup<T, TResult> setup,
    params object[] results) where T : class {
  var queue = new Queue(results);
    setup.Returns(() => {
        var result = queue.Dequeue();
        if (result is Exception) {
            throw result as Exception;
        }
        return (TResult)result;
    });
}
```

So rather than taking a parameter array of `TResult`, this overload
accepts an array of `object` instances.

Within the method, we create a non generic `Queue` and then create a
lambda that captures that queue in a closure. The lambda is passed to
the `Returns` method so that it’s called every time the mocked method is
called, returning the next item in the queue.

Here’s an example of the method in action:

```csharp
var mock = new Mock<ISomeInterface>();
mock.Setup(r => r.GetNext())
    .ReturnsInOrder(1, 2, new InvalidOperationException());

Console.WriteLine(mock.Object.GetNext());
Console.WriteLine(mock.Object.GetNext());
Console.WriteLine(mock.Object.GetNext()); // Throws InvalidOperationException
```

In this sample code, I mock an interface so that when its `GetNext`
method is called a third time, it will throw an
`InvalidOperationException`.

I’ve found this to be a helpful and useful extension to Moq and hope you
find some use for it if you’re using Moq.

NOTE: As Richard Reeves pointed out to me in an email, do be careful if
you mock a property using this approach. If you evaluate the property
while within a debugger, you will dequeue the element potentially
causing maddening debugging difficulty.

