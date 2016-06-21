---
layout: post
title: "Templated Razor Delegates"
date: 2011-02-27 -0800
comments: true
disqus_identifier: 18766
categories: [asp.net mvc,code,razor]
---
[David Fowler](http://weblogs.asp.net/davidfowler/ "Fowler's Blog") turned me on to a really cool feature of Razor I hadn’t realized made it into 1.0, Templated Razor Delegates. What’s that? I’ll let the code do the speaking.

<pre class="csharpcode"><code>
<span class="asp">@</span>{
  Func&lt;dynamic, <span class="kwrd">object</span>&gt; b = @&lt;strong&gt;@item&lt;/strong&gt;;
}
<span class="kwrd">&lt;</span><span class="html">span</span><span class="kwrd">&gt;</span>This sentence is <span class="asp">@</span>b("In Bold").<span class="kwrd">&lt;/</span><span class="html">span</span><span class="kwrd">&gt;</span>
</code></pre>

That could come in handy if you have friends who’ll jump on your case for using the bold tag instead of the strong tag because it’s “not semantic”. Yeah, I’m looking at you [Damian](http://damianedwards.wordpress.com/ "Damian") ![Smile with tongue
out](http://haacked.com/images/haacked_com/WindowsLiveWriter/Templated-Razor-Delegates_C83C/wlEmoticon-smilewithtongueout_2.png).
I mean, don’t both words signify being forceful? I digress.

Note that the delegate that’s generated is a `Func<T, HelperResult>`. Also, the `@item` parameter is a special magic parameter. These
delegates are only allowed one such parameter, but the template can call into that parameter as many times as it needs.

The example I showed is pretty trivial. I know what you’re thinking. Why not use a helper? Show me an example where this is really useful. Ok, you got it!

Suppose I wrote this really cool HTML helper method for generating any kind of list.

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

This List method accepts a templated Razor delegate, so we can call it like so.

<pre class="csharpcode"><code>
<span class="asp">@</span>{
  var items = new[] { "one", "two", "three" };
}

&lt;ul>
<span class="asp">@</span>items.List(<span class="asp">@</span>&lt;li>@item</li>)
&lt;/ul>
</code></pre>

As I mentioned earlier, notice that the argument to this method, `<span class="asp">@</span>&lt;li><span class="asp">@</span>item&lt;/li>` is automatically converted into a `Func&lt;dynamic, HelperResult>` which is what our method requires.

Now this `List` method is very reusable. Let’s use it to generate a table of comic books.

<pre><code>
<span class="asp">@</span>{
    <span class="kwrd">var</span> comics = new[] { 
        <span class="kwrd">new</span> ComicBook {Title = "Groo", Publisher = "Dark Horse Comics"},
        <span class="kwrd">new</span> ComicBook {Title = "Spiderman", Publisher = "Marvel"}
    };
}

&lt;table>
@comics.List(
  @&lt;tr>
    &lt;td><span class="asp">@</span>item.Title</td>
    &lt;td><span class="asp">@</span>item.Publisher</td>
  &lt;/tr>)
&lt;/table>
</code></pre>

This feature was originally implemented to support the WebGrid helper method, but I’m sure you’ll think of other creative ways to take
advantage of it.

If you’re interested in how this feature works under the covers, check out [this blog post](http://vibrantcode.com/blog/2010/8/2/inside-razor-part-3-templates.html "Insider Razor Templates Part 3") by [Andrew Nurse](http://vibrantcode.com/blog/ "met friend co-worker").

