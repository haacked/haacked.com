---
layout: post
title: "Examine an Exception in a Catch() Block"
date: 2006-06-06 -0800
comments: true
disqus_identifier: 13179
categories: []
---
Found a useful nugget in Richter’s recent [*CLR via
C\#*](http://www.microsoft.com/MSPress/books/6522.asp "CLR via C# Second Edition")
book I want to share with you. But first some background.

Sometimes when I write a catch block, I don’t really have any plans for
the caught exception. The following is a contrived example that is
somewhat realistic.

```csharp
try
{
    DoSomething();
}
catch(System.SomeException)
{
    DoSomethingElse();
    throw;
}
```

In the above code, I only want `DoSomethingElse()` to execute if
`DoSomething()` throws an exception of type `SomeException`. I can’t put
`DoSomethingElse()` in a finally block because then it would always get
called and not just when the exception is thrown. I don’t need to do
anything with SomeException because I am propagating it up the callstack
via the `throw` keyword to let some other method handle it.

But now, as I am stepping through the code in the debugger, I may
actually want to examine `SomeException` when the debugger reaches the
line `DoSomethingElse()`. Typically I would have to rewrite the code
like so:

```csharp
try
{
    DoSomething();
}
catch(System.FormatException e)
{
    DoSomethingElse();
    throw;
}
```

Just so I can examine the exception now stored in the variable `e`. This
is plain dumb and Richter points out why in a little tip in his book.
You can use the debugger variable **\$exception** provided by the Visual
Studio.NET Debugger to examine the exception in a catch block. I wish I
had known about this a while ago.

