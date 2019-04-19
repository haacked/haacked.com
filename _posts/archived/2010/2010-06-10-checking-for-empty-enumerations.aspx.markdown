---
title: Checking For Empty Enumerations
date: 2010-06-10 -0800 9:00 AM
tags: [code]
redirect_from: "/archive/2010/06/09/checking-for-empty-enumerations.aspx/"
---

While spelunking in some code recently I saw a method that looked
something like this:

```csharp
public void Foo<T>(IEnumerable<T> items) {
  if(items == null || items.Count() == 0) {
    // Warn about emptiness
  }
}
```

This method accepts a generic enumeration and then proceeds to check if
the enumeration is null or empty. Do you see the potential problem with
this code? I’ll give you a hint, it’s this line:

`items.Count() == 0`

What’s the problem? Well that line right there has the potential to be
vastly inefficient.

If the caller of the `Foo` method passes in an enumeration that doesn’t
implement `ICollection<T>` (*for example, an `IQueryable` as a result
from an Entity Framework or Linq to SQL query*) then the `Count` method
has to iterate over the entire enumeration just to evaluate this
expression.

In cases where the enumeration that’s passed in to this method does
implement `ICollection<T>`, this code is fine. The `Count` method has an
optimization in this case where it will simply check the `Count`
property of the collection.

If we translated this code to English, it’s asking the question “*Is the
count of this enumeration equal to zero?*”. But that’s not really the
question we’re interested in. What we really want to know is the answer
to the question “*Are there any elements in this enumeration?*”

When you think of it that way, the solution here becomes obvious. Use
the `Any` extension method from the `System.Linq` namespace!

```csharp
public void Foo<T>(IEnumerable<T> items) {
  if(items == null || !items.Any()) {
    // Warn about emptiness
  }
}
```

The beauty of this method is that it only needs to call `MoveNext` on
the `IEnumerable` interface once! You could have an infinitely large
enumeration, but `Any` will return a result immediately.

Even better, since this pattern comes up all the time, consider writing
your own simple extension method.

```csharp
public static bool IsNullOrEmpty<T>(this IEnumerable<T> items) {
    return items == null || !items.Any();
}
```

Now, with this extension method, our original method becomes even
simpler.

```csharp
public void Foo<T>(IEnumerable<T> items) {
  if(items.IsNullOrEmpty()) {
    // Warn about emptiness
  }
}
```

With this extension method in your toolbelt, you’ll never inefficiently
check an enumeration for emptiness again.

