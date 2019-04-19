---
title: Code Based Repeater for ASP.NET MVC
tags: [aspnet,aspnetmvc]
redirect_from: "/archive/2008/05/02/code-based-repeater-for-asp.net-mvc.aspx/"
---

Not long ago, my compadre [Scott
Hanselman](http://www.hanselman.com/blog/ "Scott Hanselman's Blog")
related the [following
story](http://www.hanselman.com/blog/ASPNETMVCWebFormsUnplugged.aspx "ASP.NET MVC Webforms unplugged")...

> In a recent MVC design meeting someone said something like "we’ll need
> a Repeater control" and a powerful and very technical boss-type said:
>
> "We’ve got a repeater control, it’s called a foreach loop."

I beg to differ. I think we can do better than a `foreach` loop. A
`foreach `loop doesn’t help you handle alternating items, for example.
My response to this story is, “The `foreach` loop is not our repeater
control. Our repeater control is an iterating extension method with
lambdas!”. Because who doesn’t [love
lambdas](http://blog.wekeroad.com/2008/03/17/my-personal-lambda-crusade/ "Rob Conery's Personal Lambda crusade")?

Not many people realize that within an ASPX template, it’s possible to
pass sections of the template into a lambda. Here, let me show you the
end result of using my `Repeater<T>` helper method. It’s an extension
method of the `HtmlHelper` class in ASP.NET MVC.

```aspx-cs
<table>
  <% Html.Repeater<Hobby>("Hobbies", hobby => { %>
  <tr class="row">
    <td><%= hobby.Title %></td>
  </tr>
  <% }, hobbyAlt => { %>
  <tr class="alt-row">
    <td><%= hobbyAlt.Title %></td>
    </tr>
  <% }); %>
</table>
```

This renders a table with alternating rows. The Repeater method takes in
two lambdas, one which represents the item template, and another that
represents the alternating item template.

This particular overload of the Repeater method takes in a key to the
`ViewData` dictionary and casts that to an `IEnumerable<T>`. In this
case, it tries to cast `ViewData["Hobbies"]` to `IEnumerable<Hobby>`.
I’ve included overloads that allow you to explicitly specify the items
to repeat over.

This isn't very remarkable when you think a bout it. What the above
template code translates to is the following (roughly speaking)...

```csharp
Response.Write("<table>");

Html.Repeater<Hobby>("Hobbies", hobby => {
    Response.Write("  <tr class=\"row\">");
    Response.Write("    <td>");
    Response.Write(hobby.Title);
    Response.Write("    </td>");
    Response.Write("  </tr>");
  }, hobbyAlt => { 
    Response.Write("  <tr class=\"alt-row\">");
    Response.Write("    <td>");
    Response.Write(hobbyAlt.Title);
    Response.Write("    </td>");
    Response.Write("  </tr>");
  });

Response.Write("</table>");
```

The code for the `Repeater` method is simple, short, and sweet.

```csharp
public static void Repeater<T>(this HtmlHelper html
  , IEnumerable<T> items
  , Action<T> render
  , Action<T> renderAlt)
{
  if (items == null)
    return;

  int i = 0;
  items.ForEach(item => {
    if(i++ % 2 == 0 ) 
      render(item);
    else
      renderAlt(item); 
  });
}

public static void Repeater<T>(this HtmlHelper html
  , Action<T> render
  , Action<T> renderAlt)
{
  var items = html.ViewContext.ViewData as IEnumerable<T>;
  html.Repeater(items, render, renderAlt);
}

public static void Repeater<T>(this HtmlHelper html
  , string viewDataKey
  , Action<T> render
  , Action<T> renderAlt)
{
  var items = html.ViewContext.ViewData as IEnumerable<T>;
  var viewData = html.ViewContext.ViewData as IDictionary<string,object>;
  if (viewData != null)
  {
    items = viewData[viewDataKey] as IEnumerable<T>;
  }
  else
  {
    items = new ViewData(viewData)[viewDataKey] as IEnumerable<T>;
  }
  html.Repeater(items, render, renderAlt);
}
```

Some of the `ViewData` machinations you see here is due to the fact that
`ViewData` might be a dictionary, or it might be an unknown type, in
which case we perform the equivalent of a `DataBinder.Eval` call on it
using the supplied view data key.

It turns out that the regular `<asp:Repeater />` control works just fine
with ASP.NET MVC, so there’s no need for such an ugly method call. I
just thought it was fun to try out and provides an alternative approach
that doesn’t require databinding.

UPDATE: I wanted to end this post here, but my compadre and others took
exception to my implementation. Read on to see my improvement...

As astute readers of my blog noted, the example I used forces me to
repeat a lot of template code in the alternative item case. The point of
this post was on how to mimic the repeater, not in building a better
one. Maybe you *want* to have a completely different layout in the
alternate item case. I was going to build a another one that relied only
on one template, but I figured I would leave that to the reader. But
noooo, you had to complain. ;)

So the following is an example of a repeater method that follows the
most common pattern in an alternating repeater. In this common case, you
generally want to simply change the CSS class and nothing else. So with
these overloads, you specify two CSS classes - one for items and one for
alternating items. Here’s an example of usage.

```aspx-cs
<table>
  <% Html.Repeater<Hobby>("Hobbies", "row", "row-alt", (hobby, css) => { %>
  <tr class="<%= css %>">
    <td><%= hobby.Title%></td>
  </tr>
  <% }); %>
</table>
```

And here’s the source for the extra overloads. Note that I refactored
the code for getting the enumerable from the `ViewData` into its own
method.

```csharp
public static void Repeater<T>(this HtmlHelper html
  , IEnumerable<T> items
  , string className
  , string classNameAlt
  , Action<T, string> render)
{
  if (items == null)
    return;

  int i = 0;
  items.ForEach(item =>
  {
    render(item, (i++ % 2 == 0) ? className: classNameAlt
  });
}

public static void Repeater<T>(this HtmlHelper html
  , string viewDataKey
  , string cssClass
  , string altCssClass
  , Action<T, string> render)
{
  var items = GetViewDataAsEnumerable<T>(html, viewDataKey);

  int i = 0;
  items.ForEach(item =>
  {
    render(item, (i++ % 2 == 0) ? cssClass : altCssClass);
  });
}

static IEnumerable<T> GetViewDataAsEnumerable<T>(HtmlHelper html, string viewDataKey)
{
  var items = html.ViewContext.ViewData as IEnumerable<T>;
  var viewData = html.ViewContext.ViewData as IDictionary<string, object>;
  if (viewData != null)
  {
    items = viewData[viewDataKey] as IEnumerable<T>;
  }
  else
  {
    items = new ViewData(viewData)[viewDataKey] 
      as IEnumerable<T>;
  }
  return items;
}
```

Hopefully that gets some people off my back now. ;)
