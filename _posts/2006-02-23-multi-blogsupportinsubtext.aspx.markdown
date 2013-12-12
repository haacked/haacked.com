---
layout: post
title: "Multi-Blog Support In Subtext"
date: 2006-02-22 -0800
comments: true
disqus_identifier: 11860
categories: []
---
Intro
-----

When I originally [announced the subtext
project](/archive/2005/05/04/2953.aspx "Announcement for Subtext"), I
had planned to not include multi-blog support even though it was already
included in the .TEXT codebase.

The primary reason I wanted to exclude this feature was to simplify the
code as well as the administration of a blog. If you ever had the
pleasure to install .TEXT, you would remember that there were four
web.config files from which you had to choose the appropriate one.

What changed my mind, besides the pleas from several multi-blog .TEXT
users, was the day I thought it would be nice to create a blog for my
homeowners association. I didn’t want to have to add another
installation of Subtext for each blog.

However, any multi-blog scenario would have to adhere to our goal in
making Subtext simple to set up and easy to use. So for the most part,
the Subtext user experience is optimized for users with a single blog.
For these users, you pretty much never need to know there is support for
multiple blogs.

It turns out there was a lot of duplication of effort among the four
config files, so by being careful, I distilled everything into a single
config file. This alone should make the process of installation much
simpler.

The Details
-----------

However, for those of you who wish to install multiple blogs, I’ve
written up [some
documentation](http://subtextproject.com/Developer/UrlToBlogMappings/tabid/119/Default.aspx "How an URL is Mapped To A Blog")
on how it works. Subtext supports multiple blogs from the same domain as
well as multiple blogs from different domains.

