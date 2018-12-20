---
title: Styling Rss entries in an Aggregator
date: 2004-02-29 -0800
disqus_identifier: 216
tags: []
redirect_from: "/archive/2004/02/28/styling-rss-entries-in-an-aggregator.aspx/"
---

I'm working on a project to add beauty back to the blogging experience,
even when read by an Rss Aggregator.

For example, in RssBandit, you can select a style for displaying blog
entries (via XSLT formatters). Currently, this setting applies to all
feeds. In order for this to work well, some creative stripping of the
style within an item must occur, otherwise you can get some very ugly
(even unreadable) results.

I've been working (albeit slowly) on a spec allowing a blog to "suggest"
a style (or custom formatter) for a blog entry when read in an
aggregator. Based on your aggregator settings, the aggregator would
either accept the style from the blog, or it would use your predefined
style.

Some nice benefits include the fact that the style the RssFeed suggests
could be customized to the user's aggregator. For example, if the
aggregator supports Longhorn, the RssFeed might suggest a formatter that
converts the blog entry into XAML. This nicely separates the
presentation from the content, and yet gives both the content producer
and consumer some control over the presentation.

Some issues I need to think about is whether this should be an extension
to RSS or use a new API. Not only that, I need to gather some feedback
on whether or not this is even a good idea and worth my time.

comments?

