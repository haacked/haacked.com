---
layout: post
title: "Preserve Disqus Comments with Jekyll"
date: 2013-12-09 -0800
comments: true
categories: [jekyll]
---

In my last post, I wrote about [preserving URLs when migrating to Jekyll](http://haacked.com/archive/2013/12/03/jekyll-url-extensions/). In this post, show how to preserve your Disqus comments.

This ended up being a little bit tricker. By default, disqus stores comments keyed by a URL. So if you people create Disqus comments at http://example.com/foo.aspx, you need to preserve that _exact_ URL in order for those comments to keep showing up.

In my last post, I showed how to preserve such a URL, but it's not quite exact. With Jekyll, I can get a request to http://example.com/foo.aspx to redirect to http://example.com/foo.aspx/. Note that trailing slash.

To Disqus, these are two different URLs.

Fortunately, Disqus allows you to set an identifier used to retrieve comments. For example, if you view source [on a migrated post of mine](http://haacked.com/archive/2013/10/28/code-review-like-you-mean-it.aspx/), you'll see something like this:

```xml
<script type="text/javascript">
  var disqus_shortname = 'haacked';
      
  var disqus_identifier = '18902';
  var disqus_url = 'http://haacked.com/archive/2013/10/28/code-review-like-you-mean-it.aspx/';
  
  // ...omitted
</script>
```

The `disqus_identifier` is the database ID that Subtext used as the disqus identifier. So that's the thing I need to preserve as I migrate over to Jekyll. If your current blog engine doesn't set a `disqus_identifier`, then the current exact URL must be set as the `disqus_url`.

So what I did was add my own field to my migrated Jekyll posts. You can see an example by [clicking edit on one of the older posts](https://github.com/Haacked/haacked.com/edit/gh-pages/_posts/2013-10-28-code-review-like-you-mean-it.aspx.markdown). Here's the Yaml frontmatter for that post.

```
---
layout: post
title: "Code Review Like You Mean It"
date: 2013-10-28 -0800
comments: true
disqus_identifier: 18902
categories: [open source,github,code]
---
```

This adds a new field I can now access in my template. Unfortunately, the default templates you'll find in the wild (such as the Octopress ones) won't know what to do with this. So I updated the `disqus.html` Jekyll template include that comes with most templates. You can see the [full source in this gist](https://gist.github.com/Haacked/7885542).

But here's the gist of that gist:

```xml
var disqus_identifier = '{% if page.disqus_identifier %}{{ page.disqus_identifier}}{% else %}{{ site.url }}{{ page.url }}{% endif %}';
var disqus_url = '{{ site.url }}{{ page.url }}';
```

If your current blog engine doesn't set a `disqus_identifier`, you might try using the OLD URL as the identifier. I'm not sure if that works or not. Or, you could simply use the same approach I did for the `disqus_url`.

```xml
var disqus_identifier = '{% if page.disqus_identifier %}{{ page.disqus_identifier}}{% else %}{{ site.url }}{{ page.url }}{% endif %}';
var disqus_url = '{% if page.disqus_identifier %}{{ page.disqus_url }}{% else %}{{ site.url }}{{ page.url }}{% endif %}';
```
