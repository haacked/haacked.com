---
title: "When Your DbContext Has The Wrong Scope"
description: "In this post, we look at a scenario when creating a new DbContext in its own scope is the right call."
tags: [aspnetcore,efcore]
excerpt_image: https://user-images.githubusercontent.com/19977/211218857-0acd9f7d-e9a2-474a-9789-79785f9ca7f3.png
---

This is the final installment of the adventures of Bill Maack the Hapless Developer (any similarity to me is purely coincidental and a result of pure random chance in an infinite universe). Follow along as Bill continues to improve the reliability of his ASP.NET Core and Entity Framework Core code. If you haven't read the previous installments, you can find them here:

1. [How to Recover from a DbUpdateException With EF Core](https://haacked.com/archive/2022/12/05/recover-from-dbupdate-exception/)
2. [Why Did That Database Throw That Exception?](https://haacked.com/archive/2022/12/12/specific-db-exception/)

In the first post, we looked at a background [Hangfire](https://www.hangfire.io/) job that processed incoming Slack event and it raised some questions such as:

> DbContext is not supposed to be thread safe. Why are allowing your repository method to be executed concurrently from multiple threads?

This post addresses that question and more!

![Looking through a scope at an island in the middle of the ocean](https://user-images.githubusercontent.com/19977/211218857-0acd9f7d-e9a2-474a-9789-79785f9ca7f3.png "Is that DbContext scoped properly?")

Part of the confusion lies in the fact that the original example didn't provide enough context. Let's take a deeper look at the scenario.

Bill works on the team that builds [Abbot](https://abbot.app/), a Slack app that helps customer success/support teams keep track of conversations within Slack and support more customers with less effort. The app is built on ASP.NET Core and Entity Framework Core.

As a Slack App, it receives events from Slack in the form of HTTP POST requests. A simple ASP.NET MVC controller can handle that. Note that the following code is a paraphrase of the actual code as it leaves out some details such as verifying the Slack request signature. Bill would never skimp on security and definitely validates those Slack signatures.

```csharp
public class SlackController : Controller {
    readonly AbbotDbContext _db;
    readonly ISlackEventParser _slackEventParser;
    readonly IBackgroundJobClient _backgroundJobClient; // Hangfire

    public SlackController(AbbotContext db, ISlackEventParser slackEventParser, IBackgroundJobClient backgroundJobClient) {
        _db = db;
        _slackEventParser = slackEventParser;
        _backgroundJobClient = backgroundJobClient;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync() {
        var slackEvent = await _slackEventParser.ParseAsync(Request);
        _db.SlackEvents.Add(slackEvent);
        await _db.SaveChangesAsync();

        _backgroundJobClient.Enqueue<SlackEventProcessor>(x => x.ProcessEventAsync(id));
    }
}
```

This code is pretty straightforward. Bill parses the incoming Slack event, saves it to the database, and then enqueues it for background processing using Hangfire.

When Hangfire is ready to process that event, it uses the ASP.NET Core dependency injection container to create an instance of `SlackEventProcessor` and calls the `ProcessEventAsync` method. What's nice about this generic method approach is that `SlackEventProcessor` itself doesn't even need to be registered in the container, only all of its dependencies need to be registered.

Here's the `SlackEventProcessor` class that handles the background processing.

```csharp
public class SlackEventProcessor {
    readonly AbbotContext _db;

    public SlackEventProcessor(AbbotContext db) {
        _db = db; // AbbotContext derives from DbContext
    }

    // This code runs in a background Hangfire job.
    public async Task ProcessEventAsync(int id) {
        var nextEvent = (await _db.SlackEvents.FindAsync(id))
            ?? throw new InvalidOperationException($"Event not found: {id}");
        
        try {
            // This does the actual processing of the Slack event.
            await RunPipelineAsync(nextEvent);
        }
        catch (Exception e) {
            nextEvent.Error = e.ToString();
        }
        finally {
            nextEvent.Completed = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
    }
}
```

The key thing to note here is that in the case of Hangfire, every time Hangfire processes a job, it creates a unit of work (aka a scope) for that job. The end result is that as long as your `DbContext` derived instance (in this case `AbbotContext`) is registered with a lifetime of `ServiceLifetime.Scoped`, Hangfire will inject a new instance of your `DbContext` when invoking a job. So the code here doesn't call any `DbContext` methods on multiple threads concurrently. We're Ok here in that regard.

However, there *is* an issue with Bill's code here. I glossed over it before, but the `RunPipelineAsync` method internally uses dependency injection to resolve a service to handle the Slack event processing. That service depends on `AbbotContext`. Since this is all running as part of a Hangfire job, it's all in the same Lifetime scope. What that means is that the `AbbotContext` instance that is used to retrieve the `SlackEvent` instance is the same instance that is used to process the event. That's not good.

The `AbbotContext` instance in `SlackEventProcessor` should only be responsible for retrieving and updating the `SlackEvent` instance that it needs to process. It should not be the same instance that is used when running the Slack event processing pipeline.

The solution is to create a separate `AbbotContext` instance for the outer scope. To do that, Bill needs to inject an `IDbContextFactory` into `SlackEventProcessor` and use that to create a new `AbbotContext` instance for the outer scope, resulting in:

```csharp
public class SlackEventProcessor {
    readonly IDbContextFactory<AbbotContext> _dbContextFactory;

    public SlackEventProcessor(IDbContextFactory<AbbotContext> dbContextFactory) {
        _dbContextFactory = dbContextFactory;
    }

    // This code runs in a background Hangfire job.
    public async Task ProcessEventAsync(int id) {
        await using var db = await _dbContextFactory.CreateDbContextAsync();
        var nextEvent = (await db.SlackEvents.FindAsync(id))
            ?? throw new InvalidOperationException($"Event not found: {id}");
        
        try {
            // This does the actual processing of the Slack event.
            // The AbbotContext is injected into the pipeline and is not shared with `SlackEventProcessor`.
            await RunPipelineAsync(nextEvent);
        }
        catch (Exception e) {
            nextEvent.Error = e.ToString();
        }
        finally {
            nextEvent.Completed = DateTime.UtcNow;
            await db.SaveChangesAsync();
        }
    }
}
```

The instance of `AbbotContext` created by the factory will always be a new instance. It won't be the same instance injected into any dependencies that are resolved by the DI container.

This is a pretty straightforward fix, except the first time Bill tried it, it didn't work.

## Registering the DbContextFactory Correctly

Let's take a step back and look at how Bill registered the `DbContext` instance with the DI container. Since Bill is working on an ASP.NET Core application, the recommended way to register the `DbContext` is to use the `AddDbContext` extension method on `IServiceCollection`.

```csharp
services.AddDbContext<AbbotContext>(options => {...});
```

This sets the `ServiceLifetime` for the `DbContext` to `ServiceLifetime.Scoped`. This means that the `DbContext` instance is scoped to the current HTTP request. This is the default and recommended behavior for ASP.NET Core applications.

We wouldn't want this to be a `ServiceLifetime.Singleton` as that would cause issues with concurrent calls to the `DbContext` which is a big no no.

You'll never guess the name of the method to register a `DbContextFactory` with the DI container. Yep, it's `AddDbContextFactory`.

```csharp
services.AddDbContextFactory<AbbotContext>(options => {...});
```

Now here's where it gets tricky. When Bill ran this code, he ran into an exception that looked something like:

```
Cannot consume scoped service 'Microsoft.EntityFrameworkCore.DbContextOptions1[AbbotContext]' from singleton 'Microsoft.EntityFrameworkCore.IDbContextFactory1[AbbotContext]'.
```

What's happening here is that `AddDbContext` is not just registering our `DbContext` instance, it's also registering the `DbContextOptions` instance used to create the `DbContext` instance. The lifetime of `DbContextOptions` is the same as `DbContext`, aka `ServiceLifetime.Scoped`.

However, `DbContextFactory` *also* needs to consume the `DbContextOptions` instance, but `DbContextFactory` has a lifetime of `ServiceLifetime.Singleton`. As a Singleton, it can't consume a `Scoped` service because the `Scoped` service has a shorter lifetime than the `Singleton` service.

To summarize, `DbContext` is `Scoped` while `DbContextFactory` is `Singleton` and they both need a `DbContextOptions` which is `Scoped` by default.

Fortunately, there's a simple solution. Well, it's simple when you know it, otherwise it's the kind of thing that makes a Bill want to pull his hair out. The solution is to make `DbContextOptions` a `Singleton` as well. Then both `DbContext` and `DbContextFactory` could both use it.

There's an overload to `AddDbContext` that accepts a `ServiceLifetime` specifically for the `DbContextOptions` and you can set *that* to `Singleton`. So Bill's final registration code looks like:

```csharp
services.AddDbContextFactory<AbbotContext>(options => {...});
services.AddDbContext<AbbotContext>(options => {...}, optionsLifetime: ServiceLifetime.Singleton);
```

Bill used a named parameter to make it clear what the lifetime is for. So to summarize, `DbContext` still has a lifetime of `Scoped` while `DbContextFactory` and `DbContextOptions` have a `Singleton` lifetime. And EF Core is happy and Bill's code works and is more robust. The End!
