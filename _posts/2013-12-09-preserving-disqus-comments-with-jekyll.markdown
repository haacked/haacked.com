---
layout: post
title: "Preserve Disqus Comments with Jekyll"
date: 2013-12-09 -0800
comments: true
categories: [jekyll]
---

In my last post, I wrote about [preserving URLs when migrating to Jekyll](http://haacked.com/archive/2013/12/03/jekyll-url-extensions/). In this post, show how to preserve your Disqus comments.

This ended up being a little bit tricker. By default, disqus stores comments keyed by a URL. So if you people create Disqus comments at `http://example.com/foo.aspx`, you need to preserve that _exact_ URL in order for those comments to keep showing up.

In my last post, I showed how to preserve such a URL, but it's not quite exact. With Jekyll, I can get a request to `http://example.com/foo.aspx` to redirect to `http://example.com/foo.aspx/`. Note that trailing slash. To Disqus, these are two different URLs and thus my comments for that page would not load anymore.

Fortunately, Disqus allows you to set a [Disqus Identifier](http://help.disqus.com/customer/portal/articles/472099-what-is-a-disqus-identifier-) that it uses to look up a page's comment thread. For example, if you view source [on a migrated post of mine](http://haacked.com/archive/2013/10/28/code-review-like-you-mean-it.aspx/), you'll see something like this:

```html
<script type="text/javascript">
  var disqus_shortname = 'haacked';
      
  var disqus_identifier = '18902';
  var disqus_url = 'http://haacked.com/archive/2013/10/28/code-review-like-you-mean-it.aspx/';
  
  // ...omitted
</script>
```

The `disqus_identifier` can pretty much be any string. Subtext, my old blog engine, set this to the database generated ID of the blog post. So to keep my post comments, I just needed to preserve that as I migrated over to Jekyll.

So what I did was add my own field to my migrated Jekyll posts. You can see an example by [clicking edit on one of the older posts](https://github.com/Haacked/haacked.com/edit/gh-pages/_posts/2013-10-28-code-review-like-you-mean-it.aspx.markdown). Here's the Yaml frontmatter for that post.

```yaml
---
layout: post
title: "Code Review Like You Mean It"
date: 2013-10-28 -0800
comments: true
disqus_identifier: 18902
categories: [open source,github,code]
---
```

This adds a new `disqus_identifier` field that can be accessed in the Jekyll templates. Unfortunately, the default templates you'll find in the wild (such as the Octopress ones) won't know what to do with this. So I updated the `disqus.html` Jekyll template include that comes with most templates. You can see the [full source in this gist](https://gist.github.com/Haacked/7885542).

But here's the gist of that gist:

{% raw  %}
```js
var disqus_identifier = '{% if page.disqus_identifier %}{{ page.disqus_identifier}}{% else %}{{ site.url }}{{ page.url }}{% endif %}';
var disqus_url = '{{ site.url }}{{ page.url }}';
```
{% endraw  %}

If your current blog engine doesn't explicitly set a `disqus_identifier`, the identifier is the exact URL where the comments are hosted. So you could set the `disqus_identifier` to that for your old posts and leave it empty for your new ones. 