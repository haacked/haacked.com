---
title: Tagging In Subtext
tags: [subtext]
redirect_from: "/archive/2007/05/10/tagging-in-subtext.aspx/"
---

With the [announcement of the 1.9.5
release](https://haacked.com/archive/2007/05/11/subtext-1.9.5-release.aspx "Subtext 1.9.5 release announcement")
of Subtext, I thought I should talk about the new tagging and tag cloud
feature. You can see it in action in the sidebar of my site.

![A
Tag](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TaggingInSubtext_C85/atag4.jpg)
To implement tagging, we followed the model I [wrote about
before](https://haacked.com/archive/2006/09/27/Categories_vs_Tags.aspx "Categories vs Tags").
Tags do not replace categories in Subtext. Instead, we adopted an
approach using Microformats.

We see categories as a structural element and navigational aid, whereas
we see tags as meta-data. For example, in the future, we might consider
implementing sub-categories like WordPress does.

The other reason not to implement tags as categories is that most people
create way more tags than categories and blog clients are not well
suited to deal with a huge number of categories.

To create a tag, simply use the [rel-tag
microformat](http://microformats.org/wiki/rel-tag "rel-tag microformat").
For example, use the following markup...

```csharp
<a href="http://technorati.com/tag/ASP.NET" rel="tag">ASP.NET</a>
```

...to tag a post with *ASP.NET*.

Please note that according to the microformat, the last section of the
URL defines the tag, not the text within the anchor element. For
example, the following markup...

```csharp
<a href="http://technorati.com/tag/Subtext" rel="tag">Blog</a>
```

...tags the post with *Subtext* and not *Blog*.

Also note that the URL does not have to point to technorati.com. It can
point to anywhere. We just take the last portion of the URL according to
the microformat.

