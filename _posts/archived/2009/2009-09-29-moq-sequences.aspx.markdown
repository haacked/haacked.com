---
title: Successive Method Calls With MoQ
date: 2009-09-29 -0800
disqus_identifier: 18645
categories:
- code
- tdd
redirect_from: "/archive/2009/09/28/moq-sequences.aspx/"
---

*UPDATE: For a better approach, check out [MoQ Sequences
Revisited](https://haacked.com/archive/2010/11/24/moq-sequences-revisited.aspx "A better MoQ sequences post").*

One area where using MoQ is confusing is when mocking successive calls
to the same method of an object.

For example, I was writing some tests for legacy code where I needed to
fake out multiple calls to a data reader. You remember data readers,
don’t you?

Here’s a snippet of the code I was testing. Ignore the map method and
focus on the call to `reader.Read`.

```csharp
while(reader.Read()) {
  yield return map(reader);
}
```

Notice that there are multiple calls to `reader.Read`. The first couple
times, I wanted `Read` to return `true`. The last time, it should return
`false`. And here’s the code I hoped to write to fake this using MoQ:

```csharp
reader.Setup(r => r.Read()).Returns(true);
reader.Setup(r => r.Read()).Returns(true);
reader.Setup(r => r.Read()).Returns(false);
```

Unfortunately, MoQ doesn’t work that way. The last call wins and
nullifies the previous two calls. Fortunately, there are many overloads
of the `Returns` method, some of which accept functions used to return
the value when the method is called.

That’s the approach I found on [Matt Hamilton’s blog
post](http://www.madprops.org/blog/moq-triqs-successive-expectations/ "Moq Triqs - Successive Expectations")
(Mad Props indeed!) where he describes his clever solution to this issue
involving a `Queue`:

```csharp
var pq = new Queue<IDbDataParameter>(new[]
    { 
        mockParam1.Object, 
        mockParam2.Object 
    });
mockCommand.Expect(c => c.CreateParameter()).Returns(() => pq.Dequeue());
```

Each time the method is called, it will return the next value in the
queue.

One cool thing I stumbled on is that the syntax can be made even cleaner
and more succinct by passing in a method group. Here’s my MoQ code for
the original `IDataReader` issue I mentioned above.

```csharp
var reader = new Mock<IDataReader>();
reader.Setup(r => r.Read())
  .Returns(new Queue<bool>(new[] { true, true, false }).Dequeue);
```

I’m defining a Queue inline and then passing what is effectively a
pointer to its `Dequeue` method. Notice the lack of parentheses at the
end of `Dequeue `which is how you can tell that I’m passing the method
itself and not the result of the method.

Using this apporach, MoQ will call `Dequeue` each time it calls
`r.Read() `grabbing the next value from the queue. Thanks to Matt for
posting his solution! This is a great technique for dealing with
sequences using MoQ.

UPDATE: There’s a great discussion in the comments to this post.
Fredrik Kalseth proposed an
extension method to make this pattern even simpler to apply and much
more understandable. Why didn’t I think of this?! Here’s the extension
method he proposed (but renamed to the name that Matt proposed because I
like it better).

```csharp
public static class MoqExtensions
{
  public static void ReturnsInOrder<T, TResult>(this ISetup<T, TResult> setup, 
    params TResult[] results) where T : class  {
    setup.Returns(new Queue<TResult>(results).Dequeue);
  }
}
```

Now with this extension method, I can rewrite my above test to be even
more readable.

```csharp
var reader = new Mock<IDataReader>();
reader.Setup(r => r.Read()).ReturnsInOrder(true, true, false);
```

In the words of Borat, Very Nice!

