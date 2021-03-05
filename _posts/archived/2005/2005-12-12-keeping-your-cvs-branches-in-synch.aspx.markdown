---
title: Keeping Your CVS Branches In Synch
tags: [source-control]
redirect_from: "/archive/2005/12/11/keeping-your-cvs-branches-in-synch.aspx/"
---

[![Branching
Diagram](https://haacked.com/assets/images/CVS_HowTo_SyncBranches.png)](http://stevenharman.net/blog/archive/2005/11/01/keepCVSBranchInSync.aspx)
I can’t believe I didn’t notice this when he first published it (I only
saw an internal email on it), but [Steve
Harman](http://stevenharman.net/blog/) wrote [this excellent guide to
branching](http://stevenharman.net/blog/archive/2005/11/01/keepCVSBranchInSync.aspx)
with CVS, complete with an easy to follow diagram.

He created these guidelines for the [Subtext
project](http://subtextproject.com/), but they can just as easily apply
to any project using CVS as an example of a sound branching policy. Keep
in mind that for many branches you may encounter in the wild, you are
typically done at Step D. However, in some cases you may want to
continue making experimental changes on the same branch (rather than
creating a new one), in which case (as the diagram points out), you
continue repeating steps B through E.

If anybody out there has some constructive feedback, I am sure Steve
would love to hear it. Afterwards, I will try and incorporate this into
my [Quickstart Guide to Open Source Development With CVS and
SourceForge](https://haacked.com/archive/2005/05/12/3178.aspx). Happy
branching!

