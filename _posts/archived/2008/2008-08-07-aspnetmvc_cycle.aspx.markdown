---
title: An Arbitrary Cycle Method For ASP.NET MVC
date: 2008-08-07 -0800
disqus_identifier: 18517
categories: [aspnetmvc]
redirect_from: "/archive/2008/08/06/aspnetmvc_cycle.aspx/"
---

In his [Practical Review of ASP.NET
MVC](http://www.joshuamcharles.com/blog/2008/08/a-practical-review-aspnet-mvc/ "Practical Review"),
Josh Charles provides a helpful review of ASP.NET MVC from a Rails
developer’s perspective. It seemed fair and balanced, and the end result
is that there’s room for improvement, which we’re taking to heart.

However, that’s not the part that caught my attention. He mentioned that
he wrote a `cycle` method but couldn’t write it as an extension method
to `HtmlHelper`.

> this was an instance method that would take two strings and return the
> one that it didn’t return the last time it was called. In my
> templates, I used this to change the classes for each row of data, to
> give them different background colors. I considered writing an
> extension method to the Html object used for other Html operations in
> the view page, but this method specifically required the use of an
> additional private variable, so that would not work.

If you don’t mind cheating a bit, there is a way to write this as an
extension method. And while we’re doing that, why stop at only two
strings? Why not take an indefinite number? :)

```csharp
public static string Cycle(this HtmlHelper html, params string[] strings) {
    var context = html.ViewContext.HttpContext;
    int index = Convert.ToInt32(context.Items["cycle_index"]);

    string returnValue = strings[index % strings.Length];

    html.ViewContext.HttpContext.Items["cycle_index"] = ++index;
    return returnValue;
}
```

Perhaps allowing an indefinite number of strings is overkill (who ever
heard of a table with tri-color highlighting?) but I thought it was fun
to do regardless. Here’s an example of usage with three different CSS
styles:

```aspx-cs
<style>
    .first {background-color: #ddd;}
    .second {background-color: khaki;}
    .third {background-color: #fdd;}
</style>

<table>
<% for (int i = 0; i < 5; i++) { %>
    <tr class="<%= Html.Cycle("first", "second", "third") %>">
        <td>Stuff</td>
    </tr>
<% } %>
</table>
```

And the output...

```html
<table><tbody>
    <tr class="first">
      <td>Stuff</td>
    </tr>

    <tr class="second">
      <td>Stuff</td>
    </tr>

    <tr class="third">
      <td>Stuff</td>
    </tr>

    <tr class="first">
      <td>Stuff</td>
    </tr>

    <tr class="second">
      <td>Stuff</td>
    </tr>
  </tbody></table>
```

With this, go forth and spread tri-color highlighted tables all over the
web. Or if you’re really crazy player, go with four color highlighting!

