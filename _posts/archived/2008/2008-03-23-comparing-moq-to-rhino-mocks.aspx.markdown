---
title: Comparing Moq to Rhino Mocks
tags: [code,tdd]
redirect_from: "/archive/2008/03/22/comparing-moq-to-rhino-mocks.aspx/"
---

UPDATE: I should have entitled this “Comparing Rhino Mocks and MoQ for
State Based Testing”. I tend to prefer [state-based testing over
interaction based
testing](http://martinfowler.com/articles/mocksArentStubs.html "Mocks Aren't Stubs")
except in the few cases where it is absolutely necessary or makes the
test much cleaner. When it is necessary, it is nice to have interaction
based testing available. So my comparison is incomplete as I didn’t
compare interaction based testing between the two frameworks.

For the longest time I’ve been a big fan of [Rhino
Mocks](http://www.ayende.com/projects/rhino-mocks/downloads.aspx "Download Page")
and have often written about it
glowingly.
When [Moq](http://code.google.com/p/moq/ "Moq") came on the scene, I
remained blissfully ignorant of it because I thought the lambda syntax
to be a bit gimmicky. I figured if using lambdas was all it had to
offer, I wasn’t interested.

Fortunately for me, several people in my twitter-circle recently heaped
praise on Moq. Always willing to be proven wrong, I decided to check out
what all the fuss was about. It turns out, the use of lambdas is not the
best part of Moq. **No, it’s the clean discoverable API design and lack
of the record/playback model that really sets it apart.**

To show you what I mean, here are two unit tests for a really simple
example, one using Rhino Mocks and one using Moq. The tests use the mock
frameworks to fake out an interface with a single method.

```csharp
[Test]
public void RhinoMocksDemoTest()
{
  MockRepository mocks = new MockRepository();
  var mock = mocks.DynamicMock<ISomethingUseful>();
  SetupResult.For(mock.CalculateSomething(0)).IgnoreArguments().Return(1);
  mocks.ReplayAll();

  var myClass = new MyClass(mock);
  Assert.AreEqual(2, myClass.MethodUnderTest(123));
}

[Test]
public void MoqDemoTest()
{
  var mock = new Mock<ISomethingUseful>();
  mock.Expect(u => u.CalculateSomething(123)).Returns(1);

  var myClass = new MyClass(mock.Object);
  Assert.AreEqual(2, myClass.MethodUnderTest(123));
}
```

Notice that the test using Moq only requires four lines of code whereas
the test using Rhino Mocks requires six. Lines of code is not the
measure of an API of course, but it is telling in this case. The extra
code in Rhino Mocks is due to creating a `MockRepository` class and for
calling `ReplayAll`.

The other aspect of Moq I like is that the expectations are set on the
mock itself. Even after all this time, I still get confused when setting
up results/expecations using Rhino Mocks. First of all, you have to
remember to use the correct static method, either `SetupResult` or
`Expect`. Secondly, I always get confused between `SetupResult.On` and
`SetupResult.For`. I feel like the MoQ approach is a bit more intuitive
and discoverable.

The one minor thing I needed to get used to with Moq is that I kept
trying to pass the mock itself rather than mock.Object to the
method/ctor that needed it. With Rhino Mocks, when you create the mock,
you get a class of the actual type back, not a wrapper. However, I see
the benefits with having the wrapper in Moq’s approach and now like it
very much.

My only other complaint with Moq is the name. It's hard to talk about
Moq without always saying, “Moq with a Q”. I’d prefer *MonQ* to *MoQ*.
Anyways, if that’s my only complaint, then I'm a happy camper! You can
learn more about MoQ and download it from [its Google Code
Page](http://code.google.com/p/moq/ "MoQ on Google Code").

Nice work
[Kzu](http://www.clariusconsulting.net/blogs/kzu/archive/2007/12/18/46465.aspx "Linq to Mock")!

**Addendum**

The source code for `MyClass` and the interface for `ISomethingUseful`
are below in case you want to recreate my tests.

```csharp
public interface ISomethingUseful 
{
  int CalculateSomething(int x);
}

public class MyClass
{
  public MyClass(ISomethingUseful useful)
  {
    this.useful = useful;
  }

  ISomethingUseful useful;
    
  public int MethodUnderTest(int x)
  {
    //Yah, it's dumb.
    return 1 + useful.CalculateSomething(x);
  }
}
```

Give it a whirl.

