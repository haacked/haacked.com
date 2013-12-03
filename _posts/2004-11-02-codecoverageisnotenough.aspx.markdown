---
layout: post
title: "Why Code Coverage is not Enough"
date: 2004-11-02 -0800
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

~~~~ {style="MARGIN: 0px"}
using System;
~~~~

~~~~ {style="MARGIN: 0px"}
using System.Collections;
~~~~

~~~~ {style="MARGIN: 0px"}
 
~~~~

~~~~ {style="MARGIN: 0px"}
public class MyClass
~~~~

~~~~ {style="MARGIN: 0px"}
{
~~~~

~~~~ {style="MARGIN: 0px"}
    Hashtable _values = new Hashtable();
~~~~

~~~~ {style="MARGIN: 0px"}
    
~~~~

~~~~ {style="MARGIN: 0px"}
    public MyClass()
~~~~

~~~~ {style="MARGIN: 0px"}
    {
~~~~

~~~~ {style="MARGIN: 0px"}
        _values.Add("keyOne", "1");
~~~~

~~~~ {style="MARGIN: 0px"}
        _values.Add("keyTwo", "7");
~~~~

~~~~ {style="MARGIN: 0px"}
        _values.Add("keyThree", "10");
~~~~

~~~~ {style="MARGIN: 0px"}
        //...
~~~~

~~~~ {style="MARGIN: 0px"}
    }
~~~~

~~~~ {style="MARGIN: 0px"}
    
~~~~

~~~~ {style="MARGIN: 0px"}
    public int SumIt(string[] keys)
~~~~

~~~~ {style="MARGIN: 0px"}
    {
~~~~

~~~~ {style="MARGIN: 0px"}
        int total = 0;
~~~~

~~~~ {style="MARGIN: 0px"}
        foreach(string key in keys)
~~~~

~~~~ {style="MARGIN: 0px"}
        {
~~~~

~~~~ {style="MARGIN: 0px"}
            total += (int)_values[key];
~~~~

~~~~ {style="MARGIN: 0px"}
            _values[key] = total;
~~~~

~~~~ {style="MARGIN: 0px"}
            //Maybe we do some other
~~~~

~~~~ {style="MARGIN: 0px"}
            //interesting things here.
~~~~

~~~~ {style="MARGIN: 0px"}
        }
~~~~

~~~~ {style="MARGIN: 0px"}
        return total;
~~~~

~~~~ {style="MARGIN: 0px"}
    }
~~~~

~~~~ {style="MARGIN: 0px"}
}
~~~~

Now imagine you test this class with the following NUnit fixture.

~~~~ {style="MARGIN: 0px"}
using System;
~~~~

~~~~ {style="MARGIN: 0px"}
using NUnit.Framework;
~~~~

~~~~ {style="MARGIN: 0px"}
 
~~~~

~~~~ {style="MARGIN: 0px"}
[TestFixture]
~~~~

~~~~ {style="MARGIN: 0px"}
public class MyClassTest
~~~~

~~~~ {style="MARGIN: 0px"}
{
~~~~

~~~~ {style="MARGIN: 0px"}
    [Test]
~~~~

~~~~ {style="MARGIN: 0px"}
    public void TestSumIt()
~~~~

~~~~ {style="MARGIN: 0px"}
    {
~~~~

~~~~ {style="MARGIN: 0px"}
        MyClass mine = new MyClass();
~~~~

~~~~ {style="MARGIN: 0px"}
        string[] keys = {"keyOne", "keyTwo"};
~~~~

~~~~ {style="MARGIN: 0px"}
        Assert.AreEqual(8, mine.SumIt(keys));
~~~~

~~~~ {style="MARGIN: 0px"}
    }
~~~~

~~~~ {style="MARGIN: 0px"}
}
~~~~

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

