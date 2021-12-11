---
title: "Async Disposables The Easy Way"
description: "Sometimes you want to ensure that an async method at the end of a block of code no matter what. We can use IAsyncDisposable for that. This post covers a nice helper class for creating ad-hoc IAsyncDisposables"
tags: [abbot,csharp]
---

In the `System.Reactive.Disposables` namespace (part of Reactive Extensions), there's a small and useful `Disposable` class. It has a [`Create` method](https://docs.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh229378(v=vs.103)) that takes in an `Action` and returns an `IDisposable` instance. When that instance is disposed, the action is called. It's a nice way of creating an ad-hoc `IDisposable`. I use them often for creating a scope in code where something should happen at the end of the scope. Here's an exceedingly trivial example:

```csharp
Console.WriteLine("Working on it...");
using var scope = Disposable.Create(() => Console.WriteLine("Done!"))
{
    // Do stuff.
} // scope is disposed and Working on it... is printed to console 
```

As trivial as it is, it's still a little cleaner than the longer form:

```csharp
Console.WriteLine("Working on it...");
try {
    // Do stuff.
}
finally {
    Console.WriteLine("Done!")
}
```

It is real handy when implementing a method that returns an `IDisposable`. Rather than creating a custom class that inherits `IDisposable`, you can return `Disposable.Create`.

Here's the code for `Disposable` we use in [Abbot](https://ab.bot/).

```csharp
using System;

namespace Serious;

/// <summary>
/// Provides a set of static methods for creating Disposables. This is based off of
/// https://docs.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh229792(v=vs.103)
/// </summary>
public static class Disposable
{
    /// <summary>
    /// Creates the disposable that invokes the specified action when disposed.
    /// </summary>
    /// <param name="onDispose">The action to run during IDisposable.Dispose.</param>
    /// <returns>The disposable object that runs the given action upon disposal.</returns>
    public static IDisposable Create(Action onDispose) => new ActionDisposable(onDispose);

    class ActionDisposable : IDisposable
    {
        readonly Action _onDispose;

        public ActionDisposable(Action onDispose)
        {
            _onDispose = onDispose;
        }

        public void Dispose()
        {
            _onDispose();
        }
    }
}
```

## What about Async methods?

A limitation of `IDisposable` is that the `Dispose` method is not async. Suppose you need to make a web request at the end of the scope. You don't want to use `Dispose.Create` for that. Fortunately, C# 8.0 introduced the [`IAsyncDisposable` interface](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncdisposable?view=net-6.0) for these kind of scenarios. 

So naturally, I wrote an async analog to `Disposable` called `AsyncDisposable`. Yes, I'm good at naming things.

Here's an example of where we use it. When someone issues a command to Abbot in chat, we want Abbot to add a reaction to the user's message to let the user know Abbot is working on it. When Abbot responds, we remove the reaction. So the code looks something like this (simplified).

```csharp
await AddReactionAsync(message, "robot_face");
await using var scope = AsyncDisposable.Create(async () => RemoveReactionAsync(message, "robot_face"))
{
    // Handle the incoming message
} // The reaction is removed here.
```

It makes the code so much cleaner this way.

If you try Abbot today, you won't see this functionality yet because Slack takes 4-6 weeks (or longer) to approve new permissions. We're waiting with fingers crossed and bated breaths.

Here's the code for `AsyncDisposable`.

```csharp
using System;
using System.Threading.Tasks;

namespace Serious.Tasks;

/// <summary>
/// Helper class for creating an asynchronous scope. A scope is simply a using block that calls an async method
/// at the end of the block by returning an <see cref="IAsyncDisposable"/>. This is the same concept as
/// the <see cref="Disposable.Create"/> method.
/// </summary>
public static class AsyncDisposable
{
    /// <summary>
    /// Creates an <see cref="IAsyncDisposable"/> that calls the specified method asynchronously at the end
    /// of the scope upon disposal.
    /// </summary>
    /// <param name="onDispose">The method to call at the end of the scope.</param>
    /// <returns>An <see cref="IAsyncDisposable"/> that represents the scope.</returns>
    public static IAsyncDisposable Create(Func<ValueTask> onDispose)
    {
        return new AsyncScope(onDispose);
    }

    class AsyncScope : IAsyncDisposable
    {
        readonly Func<ValueTask> _onDispose;

        public AsyncScope(Func<ValueTask> onDispose)
        {
            _onDispose = onDispose;
        }
        
        public ValueTask DisposeAsync()
        {
            return _onDispose();
        }
    }
}
```

I hope you find this useful in your own projects.
