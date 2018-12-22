---
title: C# .NET Quizzes
date: 2005-01-26 -0800
tags: [csharp,dotnet]
redirect_from: "/archive/2005/01/25/c-net-quizzes.aspx/"
---

[Colin](http://www.jtleigh.com/people/colin/blog/) has a nice [little
quiz](http://www.jtleigh.com/people/colin/blog/archives/2005/01/c_and_net_quizz.html)
about enumeration on his blog. Basically he asks, how would you
implement a class to enumerate through all the letters of the alphabet.
Below is my "cute" response.

```csharp
using System;
using System.Collections;
 
public class Alphabet : IEnumerable
{
  public IEnumerator GetEnumerator()
  {
    return "abcdefghijklmnopqrstuvwxyz".GetEnumerator();
  }
}
```

Now if you compile my answer and run it, it seems to answer the question
correctly (for an academic quiz), but it's completely wrong for a real
world developer. The right answer is "Well, which alphabet or alphabets
must I support? Does it need to be localizable based on the current
locale?".

Yes my friends, the answer is to gather more requirements. Make sure you
really understand the problem domain. This is why software isn't as easy
as "well I want it to do this so just do it." This quiz asks what seems
to be a very straightforward question. If you as a developer gave me the
solution I wrote above, I'd be pretty pissed as a client if I was ready
to deploy this to Korea.

