---
title: A Better Razor Foreach Loop
date: 2011-04-14 -0800
disqus_identifier: 18775
tags:
- razor
- code
- asp.net mvc
redirect_from: "/archive/2011/04/13/a-better-razor-foreach-loop.aspx/"
---

Yesterday, during my [ASP.NET MVC 3 talk at Mix
11](http://channel9.msdn.com/events/MIX/MIX11/FRM03 "ASP.NET MVC 3 The Time Is Now"),
I wrote a useful helper method demonstrating an advanced feature of
Razor, [Razor Templated
Delegates](https://haacked.com/archive/2011/02/27/templated-razor-delegates.aspx "Templated Razor Templates").

There are many situations where I want to quickly iterate through a
bunch of items in a view, and I prefer using the `foreach` statement.
But sometimes, I need to also know the current index. So I wrote an
extension method to `IEnumerable<T>` that accepts Razor syntax as an
argument and calls that template for each item in the enumeration.

```csharp
public static class HaackHelpers {
  public static HelperResult Each<TItem>(
      this IEnumerable<TItem> items, 
      Func<IndexedItem<TItem>, 
      HelperResult> template) {
    return new HelperResult(writer => {
      int index = 0;

      foreach (var item in items) {
        var result = template(new IndexedItem<TItem>(index++, item));
        result.WriteTo(writer);
      }
    });
  }
}
```

This method calls the template for each item in the enumeration, but
instead of passing in the item itself, we wrap it in a new class,
`IndexedItem<T>`.

```csharp
public class IndexedItem<TModel> {
  public IndexedItem(int index, TModel item) {
    Index = index;
    Item = item;
  }

  public int Index { get; private set; }
  public TModel Item { get; private set; }
}
```

And here’s an example of its usage within a view. Notice that we pass in
Razor markup as an argument to the method which gets called for each
item. We have access to the direct item and the current index.

<pre class="csharpcode"><code>
<span class="asp">@</span>model IEnumerable&lt;Question&gt;

&lt;ol&gt;
<span class="asp">@</span>Model.Each(<span class="asp">@</span>&lt;li&gt;Item <span class="asp">@</span>item.Index of <span class="asp">@</span>(Model.Count() - 1): <span class="asp">@</span>item.Item.Title&lt;/li&gt;)
&lt;/ol&gt;</code></pre>

If you want to try it out, I put the code in a package in my personal
NuGet feed for my code samples. Just connect NuGet to
[http://nuget.haacked.com/nuget/](http://nuget.haacked.com/nuget/) and
`Install-Package RazorForEach`. The package installs this code as source
files in *App\_Code*.

**UPDATE:** I updated the code and package to be more efficient
(4/16/2011).

