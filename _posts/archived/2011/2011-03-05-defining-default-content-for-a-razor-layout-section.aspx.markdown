---
title: Defining Default Content For A Razor Layout Section
date: 2011-03-05 -0800
disqus_identifier: 18767
tags:
- aspnet
- aspnetmvc
redirect_from: "/archive/2011/03/04/defining-default-content-for-a-razor-layout-section.aspx/"
---

Layouts in Razor serve the same purpose as Master Pages do in Web Forms.
They allow you to specify a layout for your site and carve out some
placeholder sections for your views to implement.

For example, here’s a simple layout with a main body section and a
footer section.

<pre class="csharpcode"><code>
<span class="kwrd">&lt;!</span><span class="html">DOCTYPE</span> <span class="attr">html</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;</span><span class="html">html</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;</span><span class="html">head</span><span class="kwrd">&gt;&lt;</span><span class="html">title</span><span class="kwrd">&gt;</span>Sample Layout<span class="kwrd">&lt;/</span><span class="html">head</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;</span><span class="html">body</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;</span><span class="html">div</span><span class="kwrd">&gt;</span><span class="asp">@</span>RenderBody()<span class="kwrd">&lt;/</span><span class="html">div</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;</span><span class="html">footer</span><span class="kwrd">&gt;</span><span class="asp">@</span>RenderSection("Footer")<span class="kwrd">&lt;/</span><span class="html">footer</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;/</span><span class="html">body</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;/</span><span class="html">html</span><span class="kwrd">&gt;</span></code></pre>

In order to use this layout, your view might look like.

<pre class="csharpcode"><code>
<span class="asp">@</span>{
    Layout = "MyLayout.cshtml";
}
<span class="kwrd">&lt;</span><span class="html">h1</span><span class="kwrd">&gt;</span>Main Content!<span class="kwrd">&lt;/</span><span class="html">h1</span><span class="kwrd">&gt;</span>
<span class="asp">@</span><span class="kwrd">section</span> Footer {
    This is the footer.
}</code></pre>

Notice we use the `@section` syntax to specify the contents for the
defined Footer section.

But what if we have other views that don’t specify content for the
Footer section? They’ll throw an exception stating that the “Footer”
section wasn’t defined.

To make a section optional, we need to call an overload of
`RenderSection` and specify `false` for the `required` parameter.

<pre class="csharpcode"><code>
<span class="kwrd">&lt;!</span><span class="html">DOCTYPE</span> <span class="attr">html</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;</span><span class="html">html</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;</span><span class="html">head</span><span class="kwrd">&gt;&lt;</span><span class="html">title</span><span class="kwrd">&gt;</span>Sample Layout<span class="kwrd">&lt;/</span><span class="html">head</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;</span><span class="html">body</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;</span><span class="html">div</span><span class="kwrd">&gt;</span><span class="asp">@</span>RenderBody()<span class="kwrd">&lt;/</span><span class="html">div</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;</span><span class="html">footer</span><span class="kwrd">&gt;</span><span class="asp">@</span>RenderSection("Footer", false)<span class="kwrd">&lt;/</span><span class="html">footer</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;/</span><span class="html">body</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;/</span><span class="html">html</span><span class="kwrd">&gt;</span></code></pre>

But wouldn’t it be nicer if we could define some default content in the
case that the section isn’t defined in the view?

Well here’s one way. It’s a bit ugly, but it works.

<pre class="csharpcode"><code>
<span class="kwrd">&lt;</span><span class="html">footer</span><span class="kwrd">&gt;</span>
  <span class="asp">@</span><span class="kwrd">if</span> (IsSectionDefined(<span class="str">"Footer"</span>)) {
      RenderSection(<span class="str">"Footer"</span>);
  }
  <span class="kwrd">else</span> { 
      <span class="kwrd">&lt;</span><span class="html">span</span><span class="kwrd">&gt;</span>This is the default yo!<span class="kwrd">&lt;/</span><span class="html">span</span><span class="kwrd">&gt;</span>   
  }
<span class="kwrd">&lt;/</span><span class="html">footer</span><span class="kwrd">&gt;</span>
</code></pre>

That’s some ugly code. If only there were a way to write a version of
`RenderSection` that could accept some Razor markup as a parameter to
the method.

[Templated Razor Delegates](https://haacked.com/archive/2011/02/27/templated-razor-delegates.aspx "Templated Razor Delegates") to the rescue! See, I told you these things would come in handy.

We can write an extension method on `WebPageBase` that encapsulates this
bit of ugly boilerplate code. Here’s the implementation.

<pre class="csharpcode"><code>
<span class="kwrd">public</span> <span class="kwrd">static</span> <span class="kwrd">class</span> Helpers {
  <span class="kwrd">public</span> <span class="kwrd">static</span> HelperResult RenderSection(<span class="kwrd">this</span> WebPageBase webPage, 
      <span class="kwrd">string</span> name, Func&lt;dynamic, HelperResult&gt; defaultContents) {
    <span class="kwrd">if</span> (webPage.IsSectionDefined(name)) {
      <span class="kwrd">return</span> webPage.RenderSection(name);
    }
    <span class="kwrd">return</span> defaultContents(<span class="kwrd">null</span>);
  }
}</code></pre>

What’s more interesting than this code is how we can use it now. My
Layout now can do the following to define the Footer section:

<pre class="csharpcode"><code>
<span class="kwrd">&lt;</span><span class="html">footer</span><span class="kwrd">&gt;</span>
  <span class="asp">@</span><span class="kwrd">this</span>.RenderSection("Footer", <span class="asp">@</span><span class="kwrd">&lt;</span><span class="html">span</span><span class="kwrd">&gt;</span>This is the default!<span class="kwrd">&lt;/</span><span class="html">span</span><span class="kwrd">&gt;</span>)
<span class="kwrd">&lt;/</span><span class="html">footer</span><span class="kwrd">&gt;</span></code></pre>

That’s much cleaner! But we can do even better. Notice how there’s that
ugly `this` keyword? That’s necessary because when you write an
extension method on the current class, you have to call it using the
`this` kewyord.

Remember when I wrote about [how to change the base type of a Razor
view](https://haacked.com/archive/2011/02/21/changing-base-type-of-a-razor-view.aspx "Changing the base type of a Razor view")?
Here’s a case where that really comes in handy.

What we can do is write our own custom base page type (such as the
`CustomWebViewPage` class I used in that blog post) and add the
`RenderSection` method above as an instance method on that class. I’ll
leave this as an exercise for the reader.

The end result will let you do the following:

<pre class="csharpcode"><code>
<span class="kwrd">&lt;</span><span class="html">footer</span><span class="kwrd">&gt;</span>
  <span class="asp">@</span>RenderSection("Footer", <span class="asp">@</span><span class="kwrd">&lt;</span><span class="html">span</span><span class="kwrd">&gt;</span>This is the default!<span class="kwrd">&lt;/</span><span class="html">span</span><span class="kwrd">&gt;</span>)
<span class="kwrd">&lt;/</span><span class="html">footer</span><span class="kwrd">&gt;</span></code></pre>

Pretty slick!

You might be wondering why we didn’t just include this feature in Razor.
My guess is that we wanted to but just ran out of time. Hopefully this
will make it in the next version of Razor.

