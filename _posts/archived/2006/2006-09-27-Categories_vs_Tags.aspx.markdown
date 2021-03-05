---
title: Categories vs Tags
tags: [blogging]
redirect_from: "/archive/2006/09/26/Categories_vs_Tags.aspx/"
---

[![Tag](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CategoriesvsTags_833F/561962_price_tag_thumb%5B1%5D.jpg)
Duncan
Mackenzie](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CategoriesvsTags_833F/561962_price_tag%5B3%5D.jpg)
writes about the issue of [Categories vs
Tags](http://www.duncanmackenzie.net/blog/categories-vs-tags-in-blogs-and-blog-editors/)
in blogs and blog editors.  I tried to comment there with my thoughts,
but received some weird javascript errors.

I’ve thought alot about the same issues with
[Subtext](http://subtextproject.com/). Orginally my plan was to simply
repurpose the existing category functionality by slapping a big **tag**
sticker on its forehead and from henceforth, a category was really a
tag.  One big rename and bam!, I’m done.

But the API issue Duncan describes is a problem.  After more thinking
about it, I now plan to make tags a first class citizen alongside
categories.  In my mind, they serve different purposes.

**I see categories as a structural element and navigational aid.**  It
is a way to group posts into large high-level groupings.  Use sparingly.

**By contrast, I see tags as meta-data**, use liberally.

One thought around the API issue is that there is a [microformat for
specifying tags](http://microformats.org/wiki/rel-tag) (rel="tag") and
Windows Live Writer has plugins for inserting tags into the body of a
post. 

My current thinking is to pursue parsing tags from posted content and
using that to *tag* content.

