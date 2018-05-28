---
layout: post
title: Downloading the Latest Source for Subtext from CVS
date: 2005-06-17 -0800
comments: true
disqus_identifier: 5155
categories: []
redirect_from: "/archive/2005/06/16/downloading-the-latest-source-for-subtext-from-cvs.aspx/"
---

UPDATE: Sorry, but my previous instructions contained some errors. I’ll
make it up to you. In the meanwhile, here are the corrections.

If you don’t have developer access to the [Subtext project on
SourceForge](https://sourceforge.net/projects/subtext/) but want to take
a look at the latest version of source code and compile it yourself,
just follow the following steps. I simply borrowed this list from
[Scott’s
post](http://www.hanselman.com/blog/PermaLink,guid,b6603ac5-3464-490f-a557-62f56b7f5668.aspx)
and modified it for Subtext.

-   Download [TortoiseCVS](http://www.tortoisecvs.org/) and install it.
-   Right click on the folder where you generally create projects (for
    me it would be in the c:\\Projects folder) and select “CVS Checkout”
-   In the CVSROOT text box, enter in this:
    :pserver:anonymous@cvs.sourceforge.net:/cvsroot/subtext and in the
    Module text box enter “SubtextSystem”
-   Hit OK. You'll get all of Subtext. You should now have a
    SubtextSystem folder within your projects folder.
-   There is a file named “CreateSubtextVdir.vbs” that will setup
    Subtext in IIS as a Virtual Directory (thanks again Scott!)
-   Now you’re ready to compile and play around.

NOTE: When you do this you are implicitly getting a label in source
control called “HEAD.” That’s the latest stuff that the Subtext team has
checked into CVS.

Until Subtext has a release, that’s all there is to play with. Once we
do have releases, I’ll provide info on how to download a specific
release.

