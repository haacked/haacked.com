---
layout: post
title: "A better 404 page and redirects with GitHub Pages"
date: 2015-07-28 -0800
comments: true
categories: [github jekyll pages]
---

A while back I migrated my blog to [Jekyll and GitHub Pages](http://haacked.com/archive/2013/12/02/dr-jekyll-and-mr-haack/). I worked hard to [preserve my existing URLs](http://haacked.com/archive/2013/12/03/jekyll-url-extensions/).

But the process wasn't perfect. My old blog engine was a bit forgiving about URLs. As long as the URL "slug" was correct, the URL could have _any_ date in it. So there happened to be quite a few non-canonical URLs out in the wild.

So what I did was [create a 404 page](https://github.com/Haacked/haacked.com/blob/gh-pages/404.html) that had a link to log an issue against my blog. GitHub Pages will serve up this page for any file not found errors. Here's an example of [the rendered 404 page](http://haacked.com/file-not-found).

And the 404 issues started to roll in. Great! So what do I do with those issues now? How do I fix them?

GitHub Pages fortunately supports the [Jekyll Redirect From plugin](https://help.github.com/articles/redirects-on-github-pages/). For a guide on how to set it up on your GitHub Pages site, check out this [GitHub Pages help documentation](https://help.github.com/articles/redirects-on-github-pages/).

Here's an example of my first attempt at front-matter for a blog post on my blog that contains [a redirect](https://github.com/Haacked/haacked.com/pull/215/files#diff-9e168ebaefc83b0e55df0ee649a693edR7).

```
---
layout: post
title: "Localizing ASP.NET MVC Validation"
permalink: /404.html
date: 2009-12-07 -0800
comments: true
disqus_identifier: 18664
redirect_from: "/archive/2009/12/12/localizing-aspnetmvc-validation.aspx"
categories: [aspnetmvc localization validation]
---
```

As you can see, my old blog was an ASP.NET application so all the file extensions end with `.aspx`. Unfortunately, this caused a problem. GitHub currently serves unknown extensions like this using the `application/octet-stream` content type. So when someone visits the old URL using Google Chrome, instead of a redirect, they end up downloading the HTML for the redirect. It happens to work on Internet Explorer which I suspect does a bit of content sniffing.

It turns out, there's an easy solution as [suggested by @charliesome](https://github.com/Haacked/haacked.com/pull/215/files#r35655387). If you add the `.html` extension to a Jekyll URL, GitHub Pages will handle the omission of the extension just fine.

Thus, I fixed the redirect like so:

```
redirect_from: "/archive/2009/12/12/localizing-aspnetmvc-validation.aspx.html"
```

By doing so, a request for `http://haacked.com/archive/2009/12/12/localizing-aspnetmvc-validation.aspx` is properly redirected. This is especially useful to know for those of you migrating from old blog engines that appended a file extension other than ``.html` to all post URLs.

Also, if you need to redirect multiple URLs, you can use a Jekyll array like so:

```
redirect_from:
  - "/archive/2012/04/15/The-Real-Pain-Of-Software-Development-2.aspx.aspx.html/"
  - "/archive/2012/04/15/The-Real-Pain-Of-Software-Development-2.aspx.html/"
```

Note that this isn't just useful for blogs. If you have a documentation site and re-organize the content, use the `redirect_from` plug-in to preserve the old URLs. Hope to see your content on [GitHub Pages soon](https://pages.github.com/)!
