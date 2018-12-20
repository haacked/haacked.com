---
title: Null Or Empty Coalescing
date: 2010-06-16 -0800
tags:
- code
redirect_from: "/archive/2010/06/15/null-or-empty-coalescing.aspx/"
---

In my last blog post, I wrote about the proper way to [check for empty
enumerations](https://haacked.com/archive/2010/06/10/checking-for-empty-enumerations.aspx "Checking For Empty Enumerations")
and proposed an `IsNullOrEmpty` method for collections which sparked a
lot of discussion.

This post covers a similar issue, but from a different angle. A very
long time ago, I wrote about my [love for the null coalescing
operator](https://haacked.com/archive/2006/08/07/tinytrickforviewstatebackedproperties.aspx/ "Null Coalescing Operator").
However, over time, I’ve found it to be not quite as useful as it could
be when dealing with strings. For example, here’s the code I might want
to write:

```csharp
public static void DoSomething(string argument) {
  var theArgument = argument ?? "defaultValue";
  Console.WriteLine(theArgument);
}
```

But here’s the code I actually end up writing:

```csharp
public static void DoSomething(string argument) {
  var theArgument = argument;
  if(String.IsNullOrWhiteSpace(theArgument)) {
    theArgument = "defaultValue";
  }
  Console.WriteLine(theArgument);
}
```

The issue here is that I want to treat an argument that consists only of
whitespace as if the argument is null and replace the value with my
default value. This is something the null coalescing operator won’t help
me with.

This lead me to [jokingly propose a null or empty coalescing
operator](http://twitter.com/haacked/status/15836957374 "@haacked on twitter")
on Twitter with the syntax `???`. This would allow me to write something
like:

`var s = argument ??? "default";`

Of course, that doesn’t go far enough because wouldn’t I also need a
null or whitespace coalescing operator???? ;)

Perhaps a better approach than the PERLification of C\# is to write an
extension method that normalizes string in such a way you can use the
tried and true (and existing!) null coalescing operator.

Thus I present to you the `AsNullIfEmpty` and `AsNullIfWhiteSpace`
methods!

Here’s my previous example refactored to use these methods.

```csharp
public static void DoSomething(string argument) {
  var theArgument = argument.AsNullIfWhiteSpace() ?? "defaultValue";

  Console.WriteLine(theArgument);
}
```

You can also take the same approach with collections.

```csharp
public static void DoSomething(IEnumerable<string> argument) {
  var theArgument = argument.AsNullIfEmpty() ?? new string[]{"default"};

  Console.WriteLine(theArgument.Count());
}
```

The following is the code for these simple methods.

```csharp
public static class EnumerationExtensions {
  public static string AsNullIfEmpty(this string items) {
    if (String.IsNullOrEmpty(items)) {
      return null;
    }
    return items;
  }

  public static string AsNullIfWhiteSpace(this string items) {
    if (String.IsNullOrWhiteSpace(items)) {
      return null;
    }
    return items;
  }
        
  public static IEnumerable<T> AsNullIfEmpty<T>(this IEnumerable<T> items) {
    if (items == null || !items.Any()) {
      return null;
    }
    return items;
  }
}
```

Another approach that some commenters to my last post recommended is to
write a `Coalesce` method. That’s also a pretty straightforward approach
which I leave as an exercise to the reader. :)

