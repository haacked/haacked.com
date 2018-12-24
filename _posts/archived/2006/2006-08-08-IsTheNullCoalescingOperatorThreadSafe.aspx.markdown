---
title: Is The Null Coalescing Operator Thread Safe?
date: 2006-08-08 -0800
tags: [concurrency,dotnet,csharp]
redirect_from: "/archive/2006/08/07/IsTheNullCoalescingOperatorThreadSafe.aspx/"
---

In response to [my blog
post](https://haacked.com/archive/2006/08/07/TinyTrickForViewStateBackedProperties.aspx "Tiny Trick")
on ViewState backed properties and the Null Coalescing operator, [Scott
Watermasysk](http://scottwater.com/blog/ "Scott Watermasysk") expresses
a worry that the null coalescing operator opens one up to a race
condition in the comments of [his blog
post](http://scottwater.com/blog/archive/2006/08/07/The-null-Coalescing-Operator.aspx "The Null Coalescing Operator").

He provides a code example of a thread safe means of reading the
`ViewState` that copies the value from the `ViewState` into a local
variable before performing the null check.

That got me worried as well. Not so much about the `ViewState` but about
applying the null coalescing operator against the `Cache` or `Session`.
These are classes where you are more likely to run into thread
contention. Take a look at this method.

```csharp
public void Demo(ref object obj){    Console.WriteLine((int)(obj ?? "null"));}
```

The worry is that it might be possible for another thread to set value
of the reference to `obj` to null in between the null coalescing
operator’s check for null and returning it to the cast operation,
potentially causing a `NullReferenceException`.

However, looking at the generated IL (with my comments interspersed), it
seems to me (and I am no IL expert so correct me if I am wrong) that
everything is just fine. It seems to copy the value before it performs
the null check. So it looks like the null coalescing operator is roughly
equivalent to the code Scott uses.

```csharp
.method public hidebysig instance void Demo(object& obj)     cil managed{  .maxstack 8  L_0000: nop   L_0001: ldarg.1   L_0002: ldind.ref   L_0003: dup //copy value to stack     L_0004: brtrue.s L_000c //jump to L_000c if value isn’t null     L_0006: pop      L_0007: ldstr "null"     L_000c: unbox.any int32     L_0011: call void [mscorlib]System.Console::WriteLine(int32)  L_0016: nop   L_0017: ret }
```

So it looks like this is a thread safe operation to me. I look forward
to any IL experts informing me if I happen to be missing something.

