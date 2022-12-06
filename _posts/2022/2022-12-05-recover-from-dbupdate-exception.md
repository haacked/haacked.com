---
title: "How to Recover from a DbUpdateException With EF Core"
description: "There are cases where you can recover from a DbUpdateException if you play your cards right. This post highlights one such scenario, a pitfall that's easy to run into, and how to recover."
tags: [aspnetcore efcore]
excerpt_image: https://user-images.githubusercontent.com/19977/205463714-68148077-0539-45c9-955d-5c687058cfa8.png
---

There are cases where recovery from an Entity Framework Core (EF Core) `DbUpdateException` is possible if you play your cards right. Play them wrong and the result is heartbreak and tears as every call to `SaveChangesAsync` rethrows the same exception.

The following story examines one example of heartache and tears. This story is based on actual events. Only the names, locations, and code have been changed to protect the guilty. All code samples have been simplified for brevity.

![Robot recovering in an ice bath](https://user-images.githubusercontent.com/19977/205463714-68148077-0539-45c9-955d-5c687058cfa8.png "Even robots need to recover")

My company, [A Serious Business, Inc.](https://www.aseriousbusiness.com/) (the real name), has a product called [Abbot](https://ab.bot/) (name is also real), a Slack app that helps customer success/support teams keep track of conversations within Slack and support more customers with less effort.

As a Slack App, it needs to ingest a lot of Slack events. At a high level, we have a `Controller` that receives Slack events over HTTP, saves a `SlackEvent` entity to the database, and then sends a message to a background service (Hangfire) to process the event.

A while ago, one of our developers, we'll call him Bill Maack (any similarity to persons real or imagined is entirely "coincidental"), wrote code to run our bot pipeline on incoming Slack events. The code looks something like this (`_db` is an instance of `AbbotContext`, our `DbContext` derived class).

```csharp
// This code runs in a background Hangfire job.
public async Task ProcessEventAsync(int id) {
    var nextEvent = (await _db.SlackEvents.FindAsync(id))
        ?? throw new InvalidOperationException($"Event not found: {id}");
    
    try {
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
```

This code worked well enough for a long time, but we noticed that our logs would have the occasional uncaught `DbUpdateException`. More odd was it was thrown in the `finally` block at the line where `_db.SaveChangesAsync()` is called. The exception message indicated a unique constraint violation for a user record. This was confusing because we're trying to save a `SlackEvent` which is totally unrelated to saving a user. So Phil...I mean Bill, ~~ignored it for a while in a confused stupor~~immediately jumped into action. Our Application Insights logs indicated the exception was a result of the following SQL query:

```sql
UPDATE "SlackEvents" SET "Completed" = @p0
WHERE "Id" = @p1;
INSERT INTO "Users" ("SlackId")
VALUES (@p2)
RETURNING "Id";
```

The plot thickens! Why are there two queries? Well we need a little more background about our app to understand that.

Here's our `User` entity, stripped down to its skivvies for brevity.

```csharp
public class User {
    public int Id { get; set; }

    // Slack User Id looks something like U012BILJMAK
    public string SlackId { get; set; } = null!;
    //...
}
```

The `OnModelCreating` method of `AbbotContext` configures a unique constraint on the `SlackId` column of `User`:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder) {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<User>()
        .HasIndex(i => i.SlackId)
        .IsUnique();
    // ...
}
```

At some point in our pipeline, we try to retrieve a user by their slack id, and if we don't find one, we create a new one. The code looks something like this:

```csharp
public async Task<User> GetUserBySlackIdAsync(string slackId) {
    var user = await _db.Users.SingleOrDefaultAsync(u => u.SlackId == slackId);
    if (user is null) {
        user = new User {
            SlackId = slackId,
        };
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    return user;
}
```

If you squint, you can see a race condition lying in wait to prey on the hapless developer in this code. After the code checks if the `User` exists, another thread could have created the `User`. So when we try to save the `User`, we get a `DbUpdateException` because the unique constraint is violated.

Here's Bill's first naive attempt to fix this:

```csharp
public async Task<User> GetUserBySlackIdAsync(string slackId) {
    var user = await _db.Users.SingleOrDefaultAsync(u => u.SlackId == slackId);
    if (user is null) {
        user = new User {
            SlackId = slackId,
        };
        await _db.Users.AddAsync(user);
        try {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException) {
            // Maybe the user already exists? If so, return that user.
            user = await _db.Users.SingleOrDefaultAsync(u => u.SlackId == slackId);
            if (user is null) {
                throw;
            }
        }
    }

    return user;
}
```

After testing it a bit, Bill called it a day. A job well done. How little did Silly Billy know. As it turns out, this was the source of the weird `DbUpdateException`s at the top of our callstack. Do you see the issue?

The problem is that when the `DbUpdateException` is thrown, the attempt to insert the new `User` record is still in the EF change tracker. So any subsequent calls to `SaveChangesAsync` will continue to try and save the `User` instance and throw the same exception.

The solution is to detach the `User` instance from the change tracker after the exception is thrown. We can do this in a generic way by detaching all the relevant entries reported by the exception like so:

```csharp
    catch (DbUpdateException e) {
        // ...
        foreach (var entry in e.Entries) {
            entry.State = EntityState.Detached;
        }
    }
```

However, in a rare flash of wisdom, Bill opted to be very specific in this case. It's unlikely but possible that some other entity caused this exception. That would violate our expectations so we'd want this to throw in that case. Since we have a specific entity we're trying to create, we should only detach that element.

```csharp
_db.Entry(user).State = EntityState.Detached;
```

Here's that code in context.

```csharp
public async Task<User> GetUserBySlackIdAsync(string slackId) {
    var user = await _db.Users.SingleOrDefaultAsync(u => u.SlackId == slackId);
    if (user is null) {
        user = new User {
            SlackId = slackId,
        };
        await _db.Users.AddAsync(user);

        try {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException) {
            _db.Entry(user).State = EntityState.Detached;
            // Maybe the user already exists? If so, return that user.
            user = await _db.Users.SingleOrDefaultAsync(u => u.SlackId == slackId);
            if (user is null) {
                throw;
            }
        }
    }

    return user;
}
```

And that solved the immediate problem. But we're not done yet. There's a problem with the exception handling. A unique constraint violation is not the only reason EF might throw a `DbUpdateException`. And what if it's a violation for another table? Also, isn't it a bit fragile that our top-level processing code could throw because the `DbContext` is in a weird state? Yes. Yes it is fragile.

Stay tuned for the further adventures of our intrepid developer, Bill Maack, as he continues to iterate on this code to make it more robust and tries to be less of a pain in the ass to his team.
