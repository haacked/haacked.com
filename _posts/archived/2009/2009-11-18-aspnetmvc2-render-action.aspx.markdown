---
title: Html.RenderAction and Html.Action
date: 2009-11-18 -0800 9:00 AM
tags: [aspnetmvc]
redirect_from: "/archive/2009/11/17/aspnetmvc2-render-action.aspx/"
---

One of the upcoming new features being added to ASP.NET MVC 2 Beta is a
little helper method called `Html.RenderAction` and its counterpart,
`Html.Action`. This has been a part of our ASP.NET MVC Futures library
for a while, but is now being added to the core product.

Both of these methods allow you to call into an action method from a
view and output the results of the action in place within the view. The
difference between the two is that `Html.RenderAction` will render the
result directly to the Response (which is more efficient if the action
returns a large amount of HTML) whereas `Html.Action` returns a string
with the result.

For the sake of brevity, I’ll use the term `RenderAction` to refer to
both of these methods. Here’s a quick look at how you might use this
method. Suppose you have the following controller.

```csharp
public class MyController {
  public ActionResult Index() {
    return View();
  }
  
  [ChildActionOnly]
  public ActionResult Menu() {
    var menu = GetMenuFromSomewhere();
      return PartialView(menu);
  }
}
```

The Menu action grabs the Menu model and returns a partial view with
just the menu.

```aspx-cs
<%@ Control Inherits="System.Web.Mvc.ViewUserControl<Menu>" %>
<ul>
<% foreach(var item in Model.MenuItem) { %>
  <li><%= item %></li>
<% } %>
</ul>
```

In your Index.aspx view, you can now call into the `Menu` action to
display the menu:

```aspx-cs
<%@ Page %>
<html>
<head><title></title></head>
<body>
  <%= Html.Action("Menu") %>
  <h1>Welcome to the Index View</h1>
</body>
</html>
```

Notice that the Menu action is marked with a `ChildActionOnlyAttribute`.
This attribute indicates that this action should not be callable
directly via the URL. It’s not required for an action to be callable via
`RenderAction`.

We also added a new property to `ControllerContext` named
`IsChildAction`. This lets you know whether the action method is being
called via a `RenderAction` call or via the URL.

This is used by some of our action filters which should do not get
called when applied to an action being called via `RenderAction` such as
`AuthorizeAttribute` and `OutputCacheAttribute`.

### Passing Values With RenderAction

Because these methods are being used to call action methods much like an
ASP.NET Request does, it’s possible to specify route values when calling
`RenderAction`. What’s really cool about this is you can pass in complex
objects.

For example, suppose we want to supply the menu with some options. We
can define a new class, `MenuOptions` like so.

```csharp
public class MenuOptions {
    public int Width { get; set; }
    public int Height { get; set; }
}
```

Next, we’ll change the `Menu` action method to accept this as a
parameter.

```csharp
[ChildActionOnly]
public ActionResult Menu(MenuOptions options) {
    return PartialView(options);
}
```

And now we can pass in menu options from our action call in the view

```aspx-cs
<%= Html.Action("Menu", 
  new { options = new MenuOptions { Width=400, Height=500} })%>
```

### Cooperating with the ActionName attribute {.clear}

Another thing to note is that `RenderAction` honors the `ActionName`
attribute when calling an action name. Thus if you annotate the action
like so.

```csharp
[ChildActionOnly]
[ActionName("CoolMenu")]
public ActionResult Menu(MenuOptions options) {
    return PartialView(options);
}
```

You’ll need to make sure to use “CoolMenu” as the action name and not
“Menu” when calling `RenderAction`.

### Cooperating With Output Caching

Note that in previous previews of the `RenderAction` method, there was
an issue where calling `RenderAction` to render an action method that
had the `OutputCache` attribute would cause the whole view to be cached.
We fixed that issue by by changing the `OutputCache` attribute to not
cache if it’s part of a child request.

If you want to output cache the portion of the page rendered by the call
to `RenderAction`, you can use a technique [I mentioned
here](https://haacked.com/archive/2009/05/12/donut-hole-caching.aspx "Donut Hole Caching")
where you place the call to `RenderAction` in a `ViewUserControl` which
has its `OutputCache` directive set.

### Summary

Let us know how this feature works for you. I think it could really help
simplify some scenarios when composing a user interface from small
parts.

