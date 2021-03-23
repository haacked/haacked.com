---
title: "Global Query Filters for Interfaces"
description: "This post describes how to apply an Entity Framework Core Global Query filter on all entity types that implement an interface using a strongly typed expression."
tags: [data, ef]
excerpt_image: https://user-images.githubusercontent.com/19977/61970776-55bc4a80-af92-11e9-9894-a772c6162adf.jpg
---

__UPDATE: At the bottom is an update that works for EF Core 5.0.2 and above that doesn't rely on internal interfaces.__

_This post describes how to apply an Entity Framework Core Global Query filter on all entity types that implement an interface using a strongly typed expression. And why you might want to do that in the first place._

One way to implement a multi-tenant application is to use a discriminator column (aka a `tenant_id` column on every table). This is a risky proposition. Every query must remember to filter by the `tenant_id`. One missed query and you expose data from one tenant to another. That'll get you featured in the next [Troy Hunt](https://www.troyhunt.com/) security fail keynote. You don't want that.

There are other features that can impact every query. For example, to implement soft deletes, you might have a `deleted` column on every table. Every query needs to filter on that column.

This is where the [Global Query Filter feature of EF Core 2.0](https://docs.microsoft.com/en-us/ef/core/querying/filters) and above comes in handy. If you use NHibernate, you've had this feature for a long time.

![Color Filters - by Carlos Ebert - CC BY 2.0](https://user-images.githubusercontent.com/19977/61970776-55bc4a80-af92-11e9-9894-a772c6162adf.jpg)

Here's a quick example of a query filter in action. First, we start with the class that's used in every example ever, the `Post` class. Someday we'll be more creative and create an example other than creating a blog engine. Blog engines is so passé.

First, let's assume we have a `Post` entity.

```csharp
public class Post
{
  public int Id { get; set; }
  public string Content { get; set; }
  public bool IsDeleted { get; set; }
}
```

We add a query filter to the `OnModelCreating` method of a `DbContext` derived class.

```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
  modelBuilder.Entity<Post>()
      .HasQueryFilter(p => !p.IsDeleted);
  //... Probably more code
}
```

Now every time you query for blog posts, the query only includes posts with `IsDeleted` set to `false`.

But your blog engine is the talk of the town. It needs more than just posts, it needs comments and tags. Now your set of query filters look like this.

```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
  modelBuilder.Entity<Post>()
    .HasQueryFilter(p => !p.IsDeleted);
  modelBuilder.Entity<Comment>()
    .HasQueryFilter(p => !p.IsDeleted);
  modelBuilder.Entity<Tag>()
    .HasQueryFilter(p => !p.IsDeleted);
  //... Probably more code
}
```

Yuck! That's starting to get repetitive. And if you add a new entity, you have to remember to add a query filter for that entity.

## Will Interfaces Save Us?

But you, you are a smart developer. You see this problem and you think, "I know, I'll solve it with an interface!"

```csharp
public interface ISoftDeletable
{
  bool IsDeleted { get; set; }
}
```

Then you'll make each of your entities implement this interface. And you'll rewrite your query filter like so.

```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
  modelBuilder.Entity<ISoftDeletable>()
    .HasQueryFilter(p => !p.IsDeleted);
    //... Probably more code
}
```

And you'll be wrong! This won't work because EF infers the table to filter based on the type passed in. EF Core requires a query filter for each entity type. Well, it does for the average developer. But you read this blog, so you are above average and you won't be bound by the limits of mere mortals.

## Filtering by Interface

When you set a filter, EF looks at the expression provided and applies it to the entity. For example, in the above example, the expression is `p => !p.IsDeleted` where `p` has the type `ISoftDeletable`. All we have to do is find every type that implements `ISoftDeletable` and rewrite this expression for each type. Specifically, we need to change the parameter type of this expression for each entity type. Sounds easy right? So how do you rewrite an expression?

Fortunately I found a pretty [good answer on StackOverflow on how to replace the parameter type in a lambda expression](https://stackoverflow.com/questions/38316519/replace-parameter-type-in-lambda-expression). I had to make some tweaks to use it for my needs, but here's the code.

```csharp
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

public static class ExpressionExtensions
{
  // Given an expression for a method that takes in a single parameter (and
  // returns a bool), this method converts the parameter type of the parameter
  // from TSource to TTarget.
  public static Expression<Func<TTarget, bool>> Convert<TSource, TTarget>(
    this Expression<Func<TSource, bool>> root)
  {
    var visitor = new ParameterTypeVisitor<TSource, TTarget>();
    return (Expression<Func<TTarget, bool>>)visitor.Visit(root);
  }
  
  class ParameterTypeVisitor<TSource, TTarget> : ExpressionVisitor
  {
    private ReadOnlyCollection<ParameterExpression> _parameters;

    protected override Expression VisitParameter(ParameterExpression node)
    {
      return _parameters?.FirstOrDefault(p => p.Name == node.Name)
        ?? (node.Type == typeof(TSource) ? Expression.Parameter(typeof(TTarget),  node.Name): node);
    }

    protected override Expression VisitLambda<T>(Expression<T> node)
    {
      _parameters = VisitAndConvert<ParameterExpression>(node.Parameters, "VisitLambda";
      return Expression.Lambda(Visit(node.Body), _parameters);
    }
  }
}
```

The `Convert` method accepts an `Expression<Func<TSource, bool>>`. The `Func<TSource, bool>` describes a method that receives an argument of type `TSource` and returns a `bool`. The same signature as a query filter. It then returns an `Expression<Func<TTarget, bool>>`. 

This is useful to take our `Expression<Func<ISoftDeletable, bool>>` and convert it to `Expression<Func<Post, bool>>` and `Expression<Func<Comment, bool>>` and so on. Let's write some code to do that.

```csharp
public static class ModelBuilderExtensions
{
  static void SetQueryFilter<TEntity, TEntityInterface>(
    this ModelBuilder builder,
    Expression<Func<TEntityInterface, bool>> filterExpression)
      where TEntityInterface : class
      where TEntity : class, TEntityInterface
  {
    var concreteExpression = filterExpression
      .Convert<TEntityInterface, TEntity>();
    builder.Entity<TEntity>()
      .HasQueryFilter(concreteExpression);
  }

  // More code to follow...
}
```

So what this method does is take in our interface based expression and convert it to an expression for the entity type. We could in theory call it like this.

```csharp
builder.SetQueryFilter<Post, ISoftDeletable>(p => p.IsDeleted);
```

But we have one problem, here's the code to retrieve every entity type.

```csharp
foreach (var type in builder.Model.GetEntityTypes()
  .Select(t => t.ClrType)
  .Where(t => typeof(TEntityInterface).IsAssignableFrom(t)))
{
    // What do we do? This method requires a type known at compile time.
    builder.SetQueryFilter<type, ISoftDeletable>(p => p.IsDeleted)
}
```

Do you see the problem?

Yes, this won't compile because `SetQueryFilter` is a generic method. It expects a known type at compile time. We can't pass in `type` as part of the generic signature.

Reflection to the rescue!

```csharp
static readonly MethodInfo SetQueryFilterMethod = typeof(ModelBuilderExtensions)
  .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
  .Single(t => t.IsGenericMethod && t.Name == nameof(SetQueryFilter));
```

We set up a `MethodInfo` instance so we can invoke `SetQueryFilter` dynamically. Here's a helper method to do that.

```csharp
static void SetEntityQueryFilter<TEntityInterface>(
  this ModelBuilder builder,
  Type entityType,
  Expression<Func<TEntityInterface, bool>> filterExpression)
  {
    SetQueryFilterMethod
      .MakeGenericMethod(entityType, typeof(TEntityInterface))
      .Invoke(null, new object[] { builder, filterExpression });
  }
```

This method now lets us pass in an entity type at runtime. So we can do this:


```csharp
var type = typeof(Post);
builder.SetEntityQueryFilter<ISoftDeletable>(type, p => p.IsDeleted);
```

Now we just need one more method to apply this filter on every entity type that implements our interface.

```csharp
public static void SetQueryFilterOnAllEntities<TEntityInterface>(
  this ModelBuilder builder,
  Expression<Func<TEntityInterface, bool>> filterExpression)
{
  foreach (var type in builder.Model.GetEntityTypes()
    .Where(t => t.BaseType == null)
    .Select(t => t.ClrType)
    .Where(t => typeof(TEntityInterface).IsAssignableFrom(t)))
  {
    builder.SetEntityQueryFilter<TEntityInterface>(
      type,
      filterExpression);
  }
}
```

Note the `.Where(t => t.BaseType == null)` clause here. Query filters may only be applied to the root entity type of an inheritance hierarchy. This clause ensures we don't try to apply a filter on a non-root type.

And going back to our `DbContext` derived class, we can invoke this method like so:

```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
  modelBuilder.SetQueryFilterOnAllEntities<ISoftDeletable>(p => !p.IsDeleted);
  //... Probably more code
}
```

If you're interested in seeing the full source code all together, [check out this gist](https://gist.github.com/haacked/febe9e88354fb2f4a4eb11ba88d64c24).

__UPDATE: Aug 19, 2019__ There was a subtle bug in the original implementation of this method. If you ran the method twice for different interfaces, and an entity implemented more than one interface, only the last query filter would be applied. For details on why that's the case, read [this EF Core issue](https://github.com/aspnet/EntityFrameworkCore/issues/10275). Here's [my comment on the issue](https://github.com/aspnet/EntityFrameworkCore/issues/10275#issuecomment-522686950) noting a scenario where the current behavior is surprising.

Fortunately, [@YZahringer](https://github.com/YZahringer) posted [a workaround](https://github.com/aspnet/EntityFrameworkCore/issues/10275#issuecomment-457504348) that I incorporated into my implementation.

__UPDATE: Mar 23, 2021__ Thanks to [this comment on GitHub](https://github.com/dotnet/efcore/issues/10275#issuecomment-785916356) by magiak, I now have an implementation that works for EF Core 5.0.2 and above. Just replace `AddQueryFilter` with `AppendQueryFilter` below. I updated [the gist with the full implementation](https://gist.github.com/haacked/febe9e88354fb2f4a4eb11ba88d64c24).

```csharp
public static void AppendQueryFilter<T>(
    this EntityTypeBuilder<T> entityTypeBuilder, Expression<Func<T, bool>> expression)
    where T : class
{
    var parameterType = Expression.Parameter(entityTypeBuilder.Metadata.ClrType);

    var expressionFilter = ReplacingExpressionVisitor.Replace(
        expression.Parameters.Single(), parameterType, expression.Body);

    if (entityTypeBuilder.Metadata.GetQueryFilter() != null)
    {
        var currentQueryFilter = entityTypeBuilder.Metadata.GetQueryFilter();
        var currentExpressionFilter = ReplacingExpressionVisitor.Replace(
            currentQueryFilter.Parameters.Single(), parameterType, currentQueryFilter.Body);
        expressionFilter = Expression.AndAlso(currentExpressionFilter, expressionFilter);
    }

    var lambdaExpression = Expression.Lambda(expressionFilter, parameterType);
    entityTypeBuilder.HasQueryFilter(lambdaExpression);
}
```
