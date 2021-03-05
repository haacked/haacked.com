---
title: Subtext Awakens From Its Slumber
tags: [code]
redirect_from: "/archive/2008/04/07/subtext-awakens-from-its-slumber.aspx/"
---

![Subtext Submarine
Logo](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Subtext1.9.5Release_EEA4/subtextsubmarinelogo6.png)It’s
been all quiet on the Subtext front for a while. While I think many open
source projects face the occasional lull, Subtext was hit by a Perfect
Storm of inactivity.

This was mostly because several of the key developers all ended up
having job changes (and moves) around the same time. For me, the move to
Microsoft and up to the Seattle area took up a lot of my time and
energy.

I finally feel settled in so I fired up the old TortoiseSVN client and
got latest from the tree excited to see what new goodness people checked
in during my absence.

[![Dsvnsubtexttrunk - TortoiseSVN Update...
Finished!](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/SubtextAwakensFromItsSlumber_12CC2/Dsvnsubtexttrunk%20-%20TortoiseSVN%20Update...%20Finished!_thumb_1.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/SubtextAwakensFromItsSlumber_12CC2/Dsvnsubtexttrunk%20-%20TortoiseSVN%20Update...%20Finished!_4.png "original")

Ok, that’s not exactly true, but captures the spirit of the truth.

In any case, now that I’m mostly settled into lovely Bellevue, WA, I
decided to spend a bit of time last week working on Subtext. I’m now
swamped again, but I got a few key fixes in already which I’m happy
about.

We decided to scale back the 2.0 release a bit. We were a bit too
ambitious with the feature set and supporting two branches became way to
time consuming. So we replaced the trunk with the 1.9 branch and all new
development is in the trunk where it should be.

The next version will still target ASP.NET 2.0, but after that, we want
to have fun with this so we’ll start to target ASP.NET 3.5. I won’t go
into the feature list for 2.0 right now. The bulk of the work is on bug
fixes, small tweaks and improvements, and infrastructure improvements. I
feel like one of the best parts of Subtext is our [build process and
continuous integration
server](http://build.subtextproject.com/ccnet/ViewFarmReport.aspx "Subtext Build Server").

Since we decided to label the next version 2.0, we will be adding a few
new hotly requested features. It should be a nice release. Sorry it’s
been so quiet for so long, but the engine is back up and running full
speed ahead.

