---
layout: post
title: "Preserve URL Extensions with Jekyll"
date: 2013-12-03 -0800
comments: true
categories: [jekyll]
---

In [my last post](http://haacked.com/archive/2013/12/02/dr-jekyll-and-mr-haack/) I wrote about migrating my blog to Jekyll and GitHub Pages. Travis Illig, a long time Subtext user asked me the following question:

> The only thing I haven't really figured out is how to nicely handle the redirect from old URLs (/archive/blah/something.aspx) to the new ones without extensions (/archive/blah/something/). I've seen some meta redirect stuff combined with JavaScript but... UGH.

UGH Indeed! I decided not to bother with changing my existing URLs to be extensionless. Instead, I focused on preserving my existing permalinks by structuring my posts such that they preserved their existing URLs.

How did I do this? My old URLs have an ASP.NET `.aspx` extension. Surely, GitHub Pages won't serve up `ASPX` files. This is true. But what it will serve up is a folder that just happens to have a name that ends with ".aspx".

The trick is in how I named the markdown files for my old posts. For example, check out a recent post: [2013-11-20-declare-dont-tell.aspx.markdown](https://github.com/Haacked/haacked.com/blob/gh-pages/_posts/2013-11-20-declare-dont-tell.aspx.markdown)

Jekyll takes the part after the date and before the `.markdown` extension and uses that as the post's URL slug. In this case, the "slug" is `declare-dont-tell.aspx`.

The way it handles extensionless URLs is to create a folder with the slug name (in this case a folder named `declare-dont-tell.aspx`) and creates the blog post as a file named `index.html` in that folder. Simple.

Thus the URL for that blog post is [http://haacked.com/archive/2013/11/20/declare-dont-tell.aspx/](http://haacked.com/archive/2013/11/20/declare-dont-tell.aspx/). But here's the beautiful part. GitHub Pages doesn't require that trailing slash. So if you make a request for [http://haacked.com/archive/2013/11/20/declare-dont-tell.aspx](http://haacked.com/archive/2013/11/20/declare-dont-tell.aspx), everything still works! GitHub simply redirects you to the version with the trailing slash.

Meanwhile, all my new posts from this point on will have a nice clean extensionless slug without breaking any permalinks for my old posts.
