---
title: Back in Business
tags: [blogging]
redirect_from: "/archive/2009/12/13/back-in-business-again.aspx/"
---

Yeah, the past few days have been a pretty low moment for me and this
blog. Long story short, on December 11, a hard-drive failure took down
the managed dedicated server which hosts my blog among other sites.

(*The following image is a dramatization of actual events and is not the
actual hard drive*)

[![Crufty
Hard-Drive](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/BackinBusiness_B3B/bad-harddrive_3.jpg "Crufty Hard-Drive")](http://www.sxc.hu/photo/291741 "Lost Data by pawel 231 from stock.xchng")This
is a server that Jeff Atwood and I share (we each host a Virtual Server
on the machine), thus all of the following sites were brought down by
the hardware malfunction:

-   [https://haacked.com/](https://haacked.com/ "My Blog")
-   [http://codinghorror.com/](http://codinghorror.com/ "Jeff's Blog")
-   [http://subtextproject.com/](http://subtextproject.com/ "Subtext Project Website")
-   [http://blog.stackoverflow.com/](http://blog.stackoverflow.com/ "Stack Overflow Blog")
-   [http://fakeplasticrock.com/](http://fakeplasticrock.com/ "Jeff's Guitar Hero/Rockband Blog")

That list doesn’t include my personal Subversion server (*yes, I’m
planning to switch to GitHub for that*).

The good news is that my hosting provider,
[CrystalTech](http://crystaltech.com/ "CrystalTech hosting"), was taking
regular backups of the machine. The bad news is that all of these sites
were hosted in virtual machines. The Virtual Hard Drive files (usually
referred to as VHD files) which contain the actual data for our virtual
machines were always in use and were not being backed up, silently
failing each time.

Properly backing up a live virtual server requires taking advantage of
[Volume Shadow Copy Service
(VSS)](http://msdn2.microsoft.com/en-us/library/aa384649.aspx "Volume Shadow Copy Service (VSS) on MSDN")
as described in this blog post to [backup live virtual server
VMs](http://virtualizationreview.com/articles/2007/10/31/backing-up-live-virtual-server-vms.aspx "how to backup live VMs"),
but this was not in place, probably due to a lack of coordination
between us and the hosting provider.

### Recovery

A data recovery company was brought in to try and recover the data. They
replaced the drive head assembly and took a forensic image from the
drive and started trying to recover our data. So far the actual VHD
files we need have not yet been recovered. However, they were able to
recover an older VHD I had backed up in 2007. That allowed me to grab
all my content files such as images and code samples from back in 2007.

Luckily, I had recently backed up my database locally a few months ago.
Not only that, thanks to the helpful [Rich
Skrenta](http://www.skrenta.com/ "Skrenta's Website"), I was able to
have a static web archive of my blog up and running quickly. He had a
cache of both my and Jeff’s
([http://codinghorror.com](http://codinghorror.com "Jeff's Blog")) blog
with the directory structure intact! That allowed me to retain my
permalinks and have my content in a readonly state. I can only assume
this cache is related to his search engine startup,
[http://blekko.com/](http://blekko.com/ "New Search Engine").

From there, I started using
[grepWin](http://tools.tortoisesvn.net/grepWin "grepWin") against a copy
of those static HTML files to strip out the relevant information and
convert the blog posts I didn’t have in my database into one big T-SQL
script which would insert all the blog posts and comments back into my
database.

I had to upgrade my blog to an unreleased version of Subtext because I
was in the process of testing the latest version against the copy of my
database. That’s why I copied it locally in the first place, so there
might be potential wonkiness if I made any mistakes in the upgrade.

At this point, most of the content for my blog is back up. I’m missing
some comments left on the most recent post and many of the images on
posts after 2007. Unfortunately getting cached images en masse is a
pretty big challenge.

I’m also missing some code samples etc, but I can start posting those
back up there when I have time.

### Lessons Learned

In general, I’m not a fan of the blame game as blame can’t change the
past. It sucks, but what’s done is done. I’ll certainly let my hosting
provider know what they can do better, but I also share in some of the
blame for letting this happen.

What’s more interesting to me is learning from the past to help realize
a better future, since that is something I can affect. What lessons did
I learn (and re-learned because the lesson didn’t make it through my
thick skull the first time) from this?

First and foremost as many mentioned to me on Twitter (*thanks!*):

> **An untested backup strategy is no backup strategy at all! Test your
> backups!**

I think a corollary to that is to try and have a backup strategy that’s
easy to setup. I actually had a process for backing up my database and
content regularly, but when I moved to the new hosting provider, I
forgot to set it up again.

I think the other lesson is that even if you have managed hosting, you
should have your own local backups of the important content in your
site.

### Backup Strategy

I’m setting up a much better back-up strategy which will include
automatic backup verifications by setting up my site on a local machine
so I can browse the backup locally. When I get it in place, I’ll write a
follow-up post and hope to get good suggestions on how to improve it.

UPDATE:Looks like I am having an issue with comments not showing up and
over-aggressive spam controls. This is the result of dogfooding the
latest trunk build of my software. ;) Glad to find these issues now
*before* releasing the latest version. :)

UPDATE: 12/14/2009Jeff Atwood [declares today to be International Backup
Awareness
Day](http://www.codinghorror.com/blog/archives/001315.html "International Backup Awareness Day")
and gives his perspective on the server failure that affected us both
and how he sucks. Yes, I must share in that suck too.

UPDATE 12/14/2009 10:19 PMI was able to recover most of my images
through a lucky break. I wrote about how the [IIS SEO Toolkit saves the
day](https://haacked.com/archive/2009/12/14/back-in-business-again.aspx "IIS SEO Toolkit Saves the day").
