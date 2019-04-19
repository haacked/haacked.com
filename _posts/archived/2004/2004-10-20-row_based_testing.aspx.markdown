---
title: Row based testing in MbUnit (i.e. RowTest)
date: 2004-10-20 -0800
tags: [code,tdd]
redirect_from: "/archive/2004/10/19/row_based_testing.aspx/"
---

Jonathan de Halleux, aka [Peli](http://blog.dotnetwiki.org/), never
ceases to impress me with his innovations within
[MbUnit](http://mbunit.tigris.org/). In case you're not familiar with
MbUnit, it's a unit testing framework similar to
[NUnit](http://nunit.sourceforge.net/).

The difference is that while NUnit seems to have stagnated, Jonathan is
constantly innovating new features, test fixtures, etc... for a complete
unit testing solution. In fact, he's even made it so that you can run
your NUnit tests within MbUnit without a recompile.

His [latest
feature](http://blog.dotnetwiki.org/archive/2004/10/20/1212.aspx) is not
necessarily a mind blower, but it's definitely will save me a lot of
time writing the same type of code over and over for testing a range of
values. I'll just show you a code snippet and you can figure out what
it's doing for you.

 

```csharp
[TestFixture]public class DivisionFixture{    [RowTest]    [Row(1000,10,100.0000)]    [Row(-1000,10,-100.0000)]    [Row(1000,7,142.85715)]    [Row(1000,0.00001,100000000)]    [Row(4195835,3145729,1.3338196)]    public void DivTest(double num, double den, double res)    {        Assert.AreEqual(res, num / den, 0.00001 );    }}
```

 

And if you're anal like me and wondering why I chose "num" instead of
"numerator" etc... Purely for blog formatting reasons. ;)

UPDATE: Jonathan points out that negative assertions are also supported.
Here's an illustrative code snippet. I can't wait to try this out.

 

```csharp
[RowTest] [Row(1000,10,100.0000)] ... [Row(1,0,0, ExpectedException =              typeof(ArithmeticException))] public void DivTest(double num, double den, double res) {...} 
```

