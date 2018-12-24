---
title: How To Get The Calling Method And Type
date: 2006-08-11 -0800
tags: [reflection,csharp,dotnet]
redirect_from: "/archive/2006/08/10/howtogetthecallingmethodandtype.aspx/"
---

Here are a couple of useful methods for getting information about the
caller of a method. The first returns the calling method of the *current
method*. The second returns the type of the caller. Both of these
methods require declaring the `System.Diagnostics` namespace.

```csharp
private static MethodBase GetCallingMethod()
{
  return new StackFrame(2, false).GetMethod();
}

private static Type GetCallingType()
{
  return new StackFrame(2, false).GetMethod().DeclaringType;
}
```

**Pop Quiz!** Why didn’t I apply the principle of code re-use and
implement the second method like so?

```csharp
public static Type GetCallingType()
{
    return GetCallingMethod().DeclaringType;
}
```

A virtual cigar and the admiration of your peers to the first person to
answer correctly.

