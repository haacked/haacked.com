---
layout: post
title: "[Tip Jar] Unit Test Events With Anonymous Delegates"
date: 2006-12-13 -0800
comments: true
disqus_identifier: 18166
categories: [tips tdd code csharp]
redirect_from: "/archive/2006/12/12/tip_jar_unit_test_events_with_anonymous_delegates.aspx/"
---

Here we are already looking ahead to learn about the language [features
of C\#
3.0](http://channel9.msdn.com/ShowPost.aspx?PostID=10276 "Channel 9 - Programming Data in C# 3.0")
and I am still finding new ways to make my code better with good “old
fashioned” C\# 2.0.

Like many people, I tend to fall into certain habits of writing code.
For example, today I was writing a unit test to test the source of a
particular event. I wanted to make sure that the event is raised and
that the event arguments were set properly. Here’s the test I started
off with (some details changed for brevity) which reflects how I would
do this in the old days.

```csharp
private bool eventRaised = false;

[Test]
public void SettingValueRaisesEvent()
{
    Parameter param = new Parameter("num", "int", "1");
    param.ValueChanged += OnValueChanged;
    parameter.Value = "42"; //should fire event.

    Assert.IsTrue(eventRaised, "Event was not raised");
}

void OnValueChanged(object sender, ValueChangedEventArgs e)
{
    Assert.AreEqual("42", e.NewValue);
    Assert.AreEqual("1", e.OldValue);
    Assert.AreEqual("num", e.ParameterName);
    eventRaised = true;
}
```

A couple of things rub me the wrong way with this code.

First, I do not like relying on the member variable `eventRaised`
because another test could inadverdently set that value, unless I make
sure to reset it in the `SetUp` method. So now I need a `SetUp` method.

Second, I don’t like the fact that this test requires this separate
event handler method, `OnValueChanged`. Ideally, I would prefer that the
unit test be self contained as much as possible.

Then it hits me. Of course! **I should use an anonymous delegate to
handle that event**. Here is the revised version.

```csharp
[Test]
public void SettingValueRaisesEvent()
{
    bool eventRaised = false;
    Parameter param = new Parameter("num", "int", "1");
    param.ValueChanged += 
        delegate(object sender, ValueChangedEventArgs e)
        {
            Assert.AreEqual("42", e.NewValue);
            Assert.AreEqual("1", e.OldValue);
            Assert.AreEqual("num", e.ParameterName);
            eventRaised = true;
        };
    param.Value = "42"; //should fire event.

    Assert.IsTrue(eventRaised, "Event was not raised");
}
```

**Now my unit test is completely self-contained in a single method.**
Lovely!

In general, I try not to use anonymous delegates all over the place,
especially delegates with a lot of code. I think they can become
confusing and hard to read. But this is a situation in which I think
using an anonymous delegate is particularly elegant.

Contrast this approach to the [approach using Rhino Mocks I wrote
about](https://haacked.com/archive/2006/06/23/UsingRhinoMocksToUnitTestEventsOnInterfaces.aspx "Using Rhino Mocks To Unit Test Events")
a while ago. In that scenario, I was testing that a subscriber to an
event handles it properly. In this case, I am testing the event source.

