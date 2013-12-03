---
layout: post
title: "Templated Razor Delegates"
date: 2011-02-26 -0800
comments: true
disqus_identifier: 18766
categories: [asp.net mvc,code]
---
[David Fowler](http://weblogs.asp.net/davidfowler/ "Fowler's Blog")
turned me on to a really cool feature of Razor I hadn’t realized made it
into 1.0, Templated Razor Delegates. What’s that? I’ll let the code do
the speaking.

```csharp
@{
  Func<dynamic, object> b = @<strong>@item</strong>;
}
<span>This sentence is @b("In Bold").</span>
```

That could come in handy if you have friends who’ll jump on your case
for using the bold tag instead of the strong tag because it’s “not
semantic”. Yeah, I’m looking at you
[Damian](http://damianedwards.wordpress.com/ "Damian") ![Smile with
tongue
out](http://haacked.com/images/haacked_com/Windows-Live-Writer/Templated-Razor-Delegates_C83C/wlEmoticon-smilewithtongueout_2.png).
I mean, don’t both words signify being forceful? I digress.

Note that the delegate that’s generated is a `Func<T, HelperResult>`.
Also, the `@item` parameter is a special magic parameter. These
delegates are only allowed one such parameter, but the template can call
into that parameter as many times as it needs.

The example I showed is pretty trivial. I know what you’re thinking. Why
not use a helper? Show me an example where this is really useful. Ok,
you got it!

Suppose I wrote this really cool HTML helper method for generating any
kind of list.

```csharp
public static class RazorExtensions {
    public static HelperResult List<T>(this IEnumerable<T> items, 
      Func<T, HelperResult> template) {
        return new HelperResult(writer => {
            foreach (var item in items) {
                template(item).WriteTo(writer);
            }
        });
    }
}
```

This List method accepts a templated Razor delegate, so we can call it
like so.

```csharp
@{
  var items = new[] { "one", "two", "three" };
}

<ul>
@items.List(@<li>@item</li>)
</ul>
```

As I mentioned earlier, notice that the argument to this method,
`@<li>@item</li>` is automatically converted into a
`Func<dynamic, HelperResult>` which is what our method requires.

Now this `List` method is very reusable. Let’s use it to generate a
table of comic books.

```csharp
@{
    var comics = new[] { 
        new ComicBook {Title = "Groo", Publisher = "Dark Horse Comics"},
        new ComicBook {Title = "Spiderman", Publisher = "Marvel"}
    };
}

<table>
@comics.List(
  @<tr>
    <td>@item.Title</td>
    <td>@item.Publisher</td>
  </tr>)
</table>
```

This feature was originally implemented to support the WebGrid helper
method, but I’m sure you’ll think of other creative ways to take
advantage of it.

If you’re interested in how this feature works under the covers, check
out [this blog
post](http://vibrantcode.com/blog/2010/8/2/inside-razor-part-3-templates.html/ "Insider Razor Templates Part 3")
by [Andrew Nurse](http://vibrantcode.com/blog/ "met friend co-worker").

