---
layout: post
title: "Why Code Coverage is not Enough"
date: 2004-11-03 -0800
comments: true
disqus_identifier: 1559
categories: [code,tdd]
---
One of the holy grails for unit testing is to get 100% code coverage
from your tests. However, you can’t sit back and smoke a cigar when you
reach that point and assume your code is invulnerable. Code coverage
just is not enough.

One obvious reason is that Code Coverage cannot help you find errors of
omission. That is, even if you had 100% code coverage from your tests,
if you forget to implement a feature (and a test for that feature), then
you’re shit out of luck.

However, apart from errors of omission, there’s the case presented here.
Imagine you have the following simple class (I’m sure your real world
class is much more complicated and interesting, but bear with me).

```csharp
using System;
using System.Collections;

public class MyClass
{
    Dictionary&lt;string, int&gt; _values = new Dictionary&lt;string, int&gt;();

    public MyClass()
    {
        _values.Add("keyOne", "1");
        _values.Add("keyTwo", "7");
        _values.Add("keyThree", "10");

        // ...
    }

    public int SumIt(string[] keys)
    {
        int total = 0;
        
		foreach(string key in keys)
        {
            total += _values[key];
            _values[key] = total;

            //Maybe we do some other
            //interesting things here.
        }

        return total;
    }
}
```

Now imagine you test this class with the following NUnit fixture.

```csharp
using System;
using XUnit;

public class MyClassTest
{
    [Fact]
    public void TestSumIt()
    {
        var mine = new MyClass();
        string[] keys = {"keyOne", "keyTwo"};
        Assert.Equal(8, mine.SumIt(keys));
    }
}
```

Voila! 100% code coverage. But does this satisfy the little QA tester
inside? I would hope not and suggest that it shouldn’t. Code coverage is
worthy goal, but often unnattainable in large systems (hence the need
for prioritization) and doesn’t provide all the benefits it would seem.

To handle situations like this, unit tests need to go beyond
concentrating on code coverage and also consider data coverage. Of
course, that’s not always practical. In the above example, if I only
have 10 keys, testing the possible permutations of SumIt becomes a huge
burden. Often the best you can do is to test a small sample and the
boundary cases.
