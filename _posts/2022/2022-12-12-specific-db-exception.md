---
title: "Why Did That Database Throw That Exception?"
description: "Entity Framework Core will throw a DbUpdateException when something goes wrong trying to save your changes. But why it did can be important when trying to recover from such an exception."
tags: [aspnetcore efcore]
excerpt_image: https://user-images.githubusercontent.com/19977/206928216-e465caff-5f86-4449-bf91-0f1fbe4a2da6.png
---

In the [previous installment](https://haacked.com/archive/2022/12/05/recover-from-dbupdate-exception/) of the adventures of the hapless developer, Bill Maack, Bill faced some code that tries to recover from a race condition when creating a `User` if the `User` entity doesn't already exist.

As a reminder, these events are based on real events with real production code, but with names, locations, and code changed to protect the guilty. All code samples have been simplified for brevity.

At the end of the last post, Bill pondered the following question:

> There's a problem with the exception handling. A unique constraint violation is not the only reason EF might throw a `DbUpdateException`. And what if it's a violation for another table?

![Robot at a computer that's on fire](https://user-images.githubusercontent.com/19977/206928216-e465caff-5f86-4449-bf91-0f1fbe4a2da6.png "I should have caught that exception.")

"What if" indeed! Bill decided to dig into that. Here's the section of the relevant code:

```csharp
catch (DbUpdateException) {
    // Maybe the user already exists? If so, return that user.
    user = await _db.Users.SingleOrDefaultAsync(u => u.SlackId == slackId);
    if (user is null) {
        throw;
    }
    _db.Entry(user).State = EntityState.Detached;
}
```

How can Bill be certain that this `DbUpdateException` really corresponds to a unique constraint violation and not some other random database exception. He could try and parse the exception message, but that's fragile and error prone. A total Bill Maack thing to do, but Bill is trying to be better. Instead, let's look at the underlying database provider error.

Abbot, the application Bill works on, uses PostgreSQL as the database. The code accesses the database via the [Entity Framework Core provider for Npgsql](https://www.npgsql.org/efcore/). Npgsql is an open source ADO.NET Data Provider for PostgreSQL. That's a mouthful, isn't it?

When running into a database error, `DbUpdateException` exposes the underlying provider specific exception via the `InnerException` property. In the case of Npgsql, this is a `PostgresException` which exposes the `TableName` and `ConstraintName` along with the underlying PostgreSQL error code. The [error codes are documented here](https://www.postgresql.org/docs/current/static/errcodes-appendix.html).

Bill could define a custom exception type for unique constraint violations, and there's nothing wrong with that if you're into that sort of thing. Bill decided to go another way. He also didn't want the calling code to have to know what the underlying database provider in cases he uses this code on other projects.

First, he defined a base `DatabaseError` record.

```csharp
/// <summary>
/// Provides additional Database specific information about 
/// a <see cref="DbUpdateException"/> thrown by EF Core.
/// </summary>
/// <param name="TableName">The table involved, if any.</param>
/// <param name="ConstraintName">The constraint involved, if any.</param>
/// <param name="Exception">The unwrapped database provider specific exception.</param>
public record DatabaseError(string? TableName, string? ConstraintName, Exception Exception);
```

And then defined a specific one for unique constraints.

```csharp
/// <summary>
/// Provides additional Postgres specific information about a 
/// <see cref="DbUpdateException"/> thrown by EF Core.This describes 
/// the case where the exception is a unique constraint violation.
/// </summary>
/// <param name="ColumnNames">The column names parsed from the constraint 
/// name assuming the constraint follows the "IX_{Table}_{Column1}_..._{ColumnN}" naming convention.</param>
/// <param name="TableName">The table involved, if any.</param>
/// <param name="ConstraintName">The constraint involved, if any.</param>
/// <param name="Exception">The unwrapped database provider specific exception.</param>
public record UniqueConstraintError(
    IReadOnlyList<string> ColumnNames,
    string? TableName,
    string? ConstraintName,
    Exception Exception) : DatabaseError(TableName, ConstraintName, Exception) {
    
    /// <summary>
    /// Creates a <see cref="UniqueConstraintError"/> from a <see cref="PostgresException"/>.
    /// </summary>
    /// <param name="postgresException">The <see cref="PostgresException"/>.</param>
    /// <returns>A <see cref="UniqueConstraintError"/> with extra information about the unique constraint violation.</returns>
    public static UniqueConstraintError FromPostgresException(PostgresException postgresException)
    {
        var constraintName = postgresException.ConstraintName;
        var tableName = postgresException.TableName;
        var constrainPrefix = tableName is not null
            ? $"IX_{tableName}_"
            : null;

        var columnNames = constrainPrefix is not null
                  && constraintName is not null
                  && constraintName.StartsWith(constrainPrefix, StringComparison.Ordinal)
            ? constraintName[constrainPrefix.Length..].Split('_')
            : Array.Empty<string>();

        return new UniqueConstraintError(columnNames, tableName, constraintName, postgresException);
    }
}
```

And finally, to connect it all together, add an extension method to map PostgreSQL error codes to these new error record types.

```csharp
/// <summary>
/// Extensions to <see cref="DbUpdateException"/> used to retrieve more 
/// database specific information about the thrown exception.
/// </summary>
public static class DbUpdateExceptionExtensions
{
    /// <summary>
    /// Retrieves a <see cref="DatabaseError"/> with database specific error 
    /// information from the <see cref="DbUpdateException"/> thrown by EF Core. 
    /// </summary>
    /// <param name="exception">The <see cref="DbUpdateException"/> thrown.</param>
    /// <returns>A <see cref="DatabaseError"/> or derived class if the inner 
    /// exception matches one of the supported types. Otherwise returns null.</returns>
    public static DatabaseError? GetDatabaseError(this DbUpdateException exception)
    {
        if (exception.InnerException is PostgresException postgresException)
        {
            return postgresException.SqlState switch
            {
                PostgresErrorCodes.UniqueViolation => UniqueConstraintError
                    .FromPostgresException(postgresException),
                //... Other error codes mapped to other error types.
                _ => new DatabaseError(
                    postgresException.TableName,
                    postgresException.ConstraintName,
                    postgresException)
            };
        }

        return null;
    }
}
```

Putting it all together, Bill made the following changes to the original code. Hold your britches here, because he leans heavily on recent C# pattern matching features!

```csharp
catch (DbUpdateException e) when (e.GetDatabaseError()
    is UniqueConstraintError { TableName: "Users", ColumnNames: [nameof(SlackId)] } constraintError)
{
    var existing = await _db.Users.SingleOrDefaultAsync(u => u.SlackId == slackId);
    if (existing is null) {
        throw;
    }
    _db.Entry(user).State = EntityState.Detached;
    user = existing;
}
```

Let me break down that `catch` expression as a refresher for those unfamiliar with some of the new pattern matching expressions.

When catching an exception, we can use a `when` expression to apply a filter to which exceptions we catch. In our case, we only want to catch exceptions where `e.GetDatabaseError() is UniqueConstraintError`. However, that's not enough, we only want `UniqueConstraintError` where the `TableName` is `Users` and the `ColumnNames` is a list with a single element, "SlackId". The `ColumnNames: [nameof(SlackId) ]` is an example of a list pattern. This is useful in cases where the unique constraint encompasses multiple columns. We could easily match on the set of columns like so:

```csharp
is UniqueConstraintError { TableName: "TableName", ColumnNames: ["Column1", "Column2", ..., "ColumnN"]}
```

Here's what this code would be expanded out in old school C#.

```csharp
catch (DbUpdateException e) {
  var uniqueConstraintError = e.GetDatabaseError() as UniqueConstraintError;
  if (uniqueConstraintError != null
        && uniqueConstraintError.TableName == "Users"
        && uniqueConstraintError.ColumnNames.Length == 1
        && uniqueConstraintError.ColumnNames[0] == "SlackId") {
    
    user = await _db.Users.SingleOrDefaultAsync(u => u.SlackId == slackId);
    if (user != null) {
      _db.Entry(user).State = EntityState.Detached;
      return user;
    }
  }
  throw;
}
```

The point being, Bill now has full confidence that the code is not trying to recover from an error it shouldn't be. The only thing he doesn't like about this code is how the column names are parsed from the unique constraint name. We haven't yet found a better approach there.

To mitigate the risk, we review all our migrations so we know that this pattern is always in effect. This gives us reasonable confidence in this code.

The code is now way more robust than it was before, but our hapless protagonist is not done yet. At the end of the first post, we asked another question.

> Also, isn't it a bit fragile that our top-level processing code could throw because the `DbContext` is in a weird state? Yes. Yes it is fragile.

In the next installment of the adventures of Bill Maack the hapless Developer, we'll cover how to make this code more robust.
