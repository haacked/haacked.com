---
title: Dynamic Methods in View Data
tags: [aspnet,aspnetmvc,code]
redirect_from: "/archive/2010/08/01/dynamic-methods-in-view-data.aspx/"
---

In [ASP.NET MVC 3 Preview
1](https://haacked.com/archive/2010/07/27/aspnetmvc3-preview1-released.aspx "ASP.NET MVC 3 Preview 1 Released"),
we introduced some syntactic sugar for creating and accessing view data
using new dynamic properties.

[![sugar](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DynamicViewDataIsNotJustForProperties_9E33/sugar_thumb.jpg "sugar")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DynamicViewDataIsNotJustForProperties_9E33/sugar_2.jpg)*Sugar,
it’s not just for breakfast.*

Within a controller action, the `ViewModel` property of `Controller`
allows setting and accessing view data via property accessors that are
resolved dynamically at runtime. From within a view, the `View` property
provides the same thing (*see the addendum at the bottom of this post
for why these property names do not match*).

### Disclaimer

*This blog post talks about ASP.NET MVC 3 Preview 1, which is a
pre-release version. Specific technical details may change before the
final release of MVC 3. This release is designed to elicit feedback on
features with enough time to make meaningful changes before MVC 3 ships,
so please comment on this blog post if you have comments.*

Let’s take a look at the old way and the new way of doing this:

### The old way

The following is some controller code that adds a string to the view
data.

```csharp
public ActionResult Index() {
  ViewData["Message"] = "Some Message";
  return View();
}
```

The following is code within a view that accesses the view data we
supplied in the controller action.

```aspx-cs
<h1><%: ViewData["Message"] %></h1>
```

### The new way

This time around, we use the `ViewModel` property which is typed as
`dynamic`. We use it like we would any property.

```csharp
public ActionResult Index() {
  ViewModel.Message = "Some Message";
  return View();
}
```

And we reference it in a Razor view. Note that this also works in a
WebForms View too.

```csharp
<h1>@View.Message</h1>
```

Note that `View.Message` is equivalent to `View["Message"]`.

### Going beyond properties

However, what might not be clear to everyone is that you can also store
and call *methods* using the same approach. Just for fun, I wrote an
example of doing this.

In the controller, I defined a lambda expression that takes in an index
and two strings. It returns the first string if the index is even, and
the second string if the index is odd. It’s very simple.

The next thing I do is assign that lambda to the `Cycle` property of
`ViewModel`, which is created on the spot since `ViewModel` is
`dynamic`.

```csharp
public ActionResult Index() {
  ViewModel.Message = "Welcome to ASP.NET MVC!";

  Func<int, string, string, string> cycleMethod = 
    (index, even, odd) => index % 2 == 0 ? even : odd;
  ViewModel.Cycle = cycleMethod;

  return View();
}
```

Now, I can dynamically call that method from my view.

```html
<table>
@for (var i = 0; i < 10; i++) {
    <tr class="@View.Cycle(i, "even-css", "odd-css")">
        <td>@i</td>
    </tr>
}
</table>
```

As a fan of dynamic languages, I find this technique to be pretty slick.
:)

The point of this blog post was to show that this is possible, but it
raises the question, “why would anyone want to do this over writing a
custom helper method?”

Very good question! Right now, it’s mostly a curiosity to me, but I can
imagine cases where this might come in handy. However, if you re-use
such view functionality or really need Intellisense, I’d highly
recommend making it a helper method. I think this approach works well
for rapid prototyping and maybe for one time use helper functions.

Perhaps you’ll find even better uses I didn’t think of at all.

### Addendum: The Property name mismatch

Earlier in this post I mentioned the mismatch between property names,
*ViewModel* vs *View*. I also talked about this in a [video I recorded
for
MvcConf](http://www.viddler.com/explore/mvcconf/videos/4/ "MvcConf talk: ASP.NET MVC 3 Preview 1")
on MVC 3 Preview 1. Originally, we wanted to pick a nice terse name for
this property so when referencing it in the view, there is minimal
noise. We liked the property `View` for this purpose and implemented it
for our view page first.

But when we went to port this property over to the `Controller`, we
realized it wouldn’t work. Anyone care to guess why? Yep, that’s right.
`Controller` already has a method named `View` so it can’t also have a
property named the same thing. So we called it `ViewModel` for the time
being and figured we’d change it once we came up with a better name.

So far, we haven’t come up with a better name that’s both short and
descriptive. And before you suggest it, the acronym of “View Data” is
**not an option**.

If you have a better name, do suggest it. :)

### Addendum 2: Unit Testing

Someone on Twitter asked me how you would unit test this action method.
Here’s an example of a unit tests that shows you can simply call this
dynamic method directly from within a unit test (see the *act* section
below).

```csharp
[TestMethod]
public void CanCallCycle() {
  // arrange
  var controller = new HomeController();
  controller.Index();

  // act
  string even = controller.ViewModel.Cycle(0, "even", "odd");

  // assert
  Assert.AreEqual("even", even);
}
```

