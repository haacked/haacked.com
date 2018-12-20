---
title: Async Lambdas
date: 2013-01-24 -0800
tags:
- code
redirect_from: "/archive/2013/01/23/async-lambdas.aspx/"
---

Today I learned something new and I love that!

I was looking at some code that looked like this:

```csharp
try
{
    await obj.GetSomeAsync();
    Assert.True(false, "SomeException was not thrown");
}
catch (SomeException)
{
}
```

That’s odd. We’re using xUnit. Why not use the `Assert.Throws` method?
So I tried with the following naïve code.

```csharp
Assert.Throws<SomeException>(() => await obj.GetSomeAsync());
```

Well that didn’t work. I got the following helpful compiler error:

> error CS4034: The 'await' operator can only be used within an async
> lambda expression. Consider marking this lambda expression with the
> 'async' modifier.

Oh, I never really thought about applying the async keyword to a lambda
expression, but it makes total sense. So I tried this:

```csharp
Assert.Throws<SomeException>(async () => await obj.GetSomeAsync());
```

Hey, that worked! I rushed off to tell the internets on Twitter.

But I made a big mistake. That only made the compiler happy. It doesn’t
actually work. It turns out that Assert.Throws takes in an Action and
thus that expression doesn’t return a Task to be awaited upon. Stephen
Toub explains the issue in this helpful blog post, [Potential pitfalls
to avoid when passing around async
lambdas](http://blogs.msdn.com/b/pfxteam/archive/2012/02/08/10265476.aspx "Potential pitfalls with async lambdas").

Ah, I’m gonna need to write my own method that takes in a `Func<Task>`.
Let’s do this!

I wrote [the following](https://gist.github.com/4616366 "ThrowsAsync"):

```csharp
public async static Task<T> ThrowsAsync<T>(Func<Task> testCode)
      where T : Exception
{
  try
  {
    await testCode();
    Assert.Throws<T>(() => { }); // Use xUnit's default behavior.
  }
  catch (T exception)
  {
    return exception;
  }
  // Never reached. Compiler doesn't know Assert.Throws above always throws.
  return null;
}
```

Here’s an example of a unit test (using xUnit) that makes use of this
method.

```csharp
[Fact]
public async Task RequiresBasicAuthentication()
{
  await ThrowsAsync<SomeException>(async () => await obj.GetSomeAsync());
}
```

And that works. I mean it actually works. Let me know if you see any
bugs with it.

Note that you have to change the return type of the test method (fact)
from void to return Task and mark it with the `async` keyword as well.

So as I was posting all this to Twitter, I learned that [Brendan
Forster](https://twitter.com/shiftkey "ShiftKey") (aka @ShiftKey)
already [built a
library](https://github.com/pprovost/AssertEx/ "https://github.com/pprovost/AssertEx/")
that has this type of assertion. But it wasn’t on NuGet so he’s dead to
me.

But he [remedied
that](https://nuget.org/packages/AssertEx/ "AssertEx on NuGet") five
minutes later.

`Install-Package AssertEx.`

So we’re all good again.

If I were you, I’d probably just go use that. I just thought this was an
enlightening look at how `await` works with lambdas.

