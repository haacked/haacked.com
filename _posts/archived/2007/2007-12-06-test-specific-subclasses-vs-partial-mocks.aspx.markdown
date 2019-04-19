---
title: Test Specific Subclasses vs Partial Mocks
date: 2007-12-06 -0800
tags: [code]
- tdd
redirect_from: "/archive/2007/12/05/test-specific-subclasses-vs-partial-mocks.aspx/"
---

Sometimes when writing unit tests, you run into the case where you want
to override the behavior of a specific method.

Here’s a totally contrived example I just pulled from my head to
demonstrate this idea. Any similarity to specific real world scenarios
is coincidental ;). Suppose we have this class we want to test.

```csharp
public class MyController
{
  public void MyAction()
  {
      RenderView("it matches?");
  }

  public virtual void RenderView(string s)
  {
      throw new NotImplementedException("To ensure this method is overridden.");
  }
}
```

What we have here is a class with a public method `MyAction` that calls
another virtual method, `RenderView`. We want to test the `MyAction`
method and make sure it calls `RenderView` properly. But we don’t want
the implementation of `RenderView` to execute because it will throw an
exception. Perhaps we plan to implement that later.

### Using A Partial Mock

There are two easy ways to test it. One is to create a partial mock
using a Mocking framework such as [Rhino
Mocks](http://www.ayende.com/projects/rhino-mocks.aspx "Rhino Mocks homepage").

```csharp
[TestMethod]
public void DoMethodCallsRenderViewProperly()
{
  MockRepository mocks = new MockRepository();
  MyController fooMock = mocks.PartialMock<MyController>();
  Expect.Call(delegate { fooMock.RenderView("it matches?"); });
  mocks.ReplayAll();

  fooMock.MyAction();
  mocks.VerifyAll();
}
```

If you’re not familiar with mock framework, what we’ve done here is
dynamically create a proxy for our `MyController` class and we’ve
overridden the behavior of the `RenderView` method by setting an
*expectation*. Basically, the expectation is that the method is called
with the string “it matches?”. At the end, we verify that all of our
expectations were met.

This is a pretty neat way of testing abstract and non-sealed classes. If
a method does something that would break the test, and you don’t want to
deal with that, or you don’t care if that method even runs, you can use
this technique.

However, there are two problems you might run into this approach. First,
`VerifyAll` doesn’t allow you to specify a message. That is a minor
concern, but it’d be nice to supply an assert message there.

Secondly, and more importantly, what if `RenderView` is protected and
not public? You won’t be able to use a partial mock (at least not using
Rhino Mocks).

### Using a Test-Specific Subclass

One approach is to use a test-specific subclass. I’ve used this test
pattern many times before, but didn’t know there was a name for it till
my colleague, Chris Tavares of the P&P group (no blog as far as I can
tell), told me about the book [xUnit Test Patterns: Refactoring Test
Code](http://www.amazon.com/gp/product/0131495054?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=9325&creativeASIN=0131495054 "xUnit Test Patterns on Amazon").

In the book, the author categorizes various useful test techniques into
groups of test patterns. The Test-Specific Subclass pattern addresses
the situation described in this post. So looking at the above code, but
assuming that `RenderView` is protected, we can still test it by doing
the following.

```csharp
[TestMethod]
public void DoMethodCallsRenderViewProperly()
{
  FooTestDouble fooDouble = new FooTestDouble();
  fooDouble.MyAction();
  Assert.AreEqual("it matches?", fooDouble.ReceivedArgument, "Did render the right view.");
}

private class FooTestDouble : MyController
{
  public string ReceivedArgument { get; private set; }

  protected override void RenderView(string s)
  {
      this.ReceivedArgument = s;
  }
}
```

All we did was write a class specific to this test called
`FooTestDouble`. In that class we override the protected `RenderView`
method and set a property with the passed in argument. Then in our test,
we can simply check that the argument matches our expectation (and we
get a human friendly assert message to boot).

### Is this a valid test pattern?

Interestingly enough, I have shown this technique to some developers who
told me it made them feel dirty (I’m not naming names). They didn’t feel
this was a valid way to write a unit test. One complaint is that one
shouldn’t have to inherit from a class in order to test that class.

So far, none of these complaints have provided empirical reasons behind
this *feeling* that it is wrong. One complaint I’ve heard is that we are
not testing the class, we are testing a derived class.

Sure, we’re technically testing a subclass and not the class itself, but
we are in control of the subclass. We know that the behavior of the
subclass is *exactly* the same except for what we chose to override.

Not only that, the same argument could be applied to using a partial
mock. After all, what is the mocking example (which many feel is more
appropriate) doing but *implicitly* generating a class that inherits
from the class being tested whereas this pattern *explicitly* inherits
from the class.

My own feeling on this is - I want to choose the technique that involves
less code and is more understandable for any given situation. In some
cases, using a mock framework does this. For example, in the first case
when `RenderView` is public, I like having my test fully self-contained.
But in the second case, `RenderView` is protected, I think the
test-specific subclass is perfectly valid. The test-specific subclass is
also great for those who are not familiar with a mock framework.

While some *guidelines* around TDD and unit testing are designed to
produce better tests (for example, the Red Green approach and trying not
to touch external resources such as the database) and better design, I
don’t like to subscribe to *arbitrary* rules that only make writing
tests harder and don’t seem to provide any measurable benefit based on
some vague *feeling*of dirtiness.

