---
title: GetRecentPost Ignores Categories In Windows Live Writer
date: 2007-03-23 -0800
disqus_identifier: 18261
categories: []
redirect_from: "/archive/2007/03/22/getrecentpost-ignores-categories-in-windows-live-writer.aspx/"
---

It appears to me that [Windows Live
Writer](http://windowslivewriter.spaces.live.com/ "Windows Live Writer Space")
completely ignores categories returned by the `getRecentPosts`
[Metaweblog
API](http://www.xmlrpc.com/metaWeblogApi "Metaweblog API RFC") method.

It took me a long time to realize this because I write all my posts
using WLW and it stores the categories for a recent post on the local
machine. So as long as I do everything via WLW, I’d never notice.

But a recent bug report alerted me to the problem. I logged into my blog
via the web admin interface and changed the categories. I refreshed the
recent posts in WLW and opened up a post, and sure enough the categories
for the post were not updated.

I was experiencing the same thing in
[Blogjet](http://blogjet.com/ "Blogjet"), but after making a small tweak
in the code, everything works fine in BlogJet. Unfortunately WLW is
still broken in this respect.

I’ve carefully analyzed the HTTP traffic with Fiddler and cannot figure
out why this would happen. Everything looks absolutely correct on
Subtext’s end. I must conclude it’s a bug with WLW.

**Would someone be so kind to confirm this with a different blog engine
for me?** Just run through the repro steps I mentioned above and let me
know if it really works for you. I’d really be grateful.

**Just to be clear: Repro Steps**

1.  Create a post with **no categories**.
2.  Use another tool (such as your blog's web admin) to specify several
    categories.
3.  In Windows Live Writer, refresh the recent posts.
4.  Click on the post to edit it.
5.  Check whether or not the correct categories are selected in the
    category drop down.

Thanks Mucho!

