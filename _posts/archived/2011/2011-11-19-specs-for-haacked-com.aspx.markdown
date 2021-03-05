---
title: Specs for Haacked.com
tags: [meta]
redirect_from: "/archive/2011/11/18/specs-for-haacked-com.aspx/"
---

Once in a while folks ask me for details about the hardware and software
that hosts my blog. Rather than write about it, a photo can provide all
the details that you need.

There you have it.

[![trs-80](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Hardware-Specs-for-Haacked.com_8473/trs-80_3.jpg "trs-80")](http://www.flickr.com/photos/37796451@N00/4820596557/ "By EasterBilby - Creative Commons Some Rights Reserved")

[Well
actually^TM^](http://tirania.org/blog/archive/2011/Feb-17.html "Well, Actually"),
my blog runs on a bit more hardware than that these days. Especially
after the Great Hard-Drive Failure of 2009. As longtime readers of my
blog might remember, nearly two years ago, this blog went down in flames
due [to a faulty
hard-drive](https://haacked.com/archive/2009/12/14/back-in-business-again.aspx "Back in Business")
on the hosting server.

My hosting provider, CrystalTech (now rebranded to be the Web Services
home of [The Small Business
Authority](http://webservices.thesba.com/crystaltech.aspx "CrystalTech new brand")),
took regular backups of the server, but I hosted my blog in a virtual
machine. As it turns out, the backups did not include the VM because it
was always “in use”. In order to backup a virtual machine, the backup
needs to take special action to ensure that works.

Today, I still host with CrystalTech in a large part due to their
response to the great hard-drive meltdown. First and foremost, they
didn’t jump to blame me. They focused on fixing the problem at hand. In
the past, I’ve hosted with other providers who excelled at making you
feel that anything wrong was your fault. Ever been in a relationships
like that?

Once things were settled, they worked with me to figure out what
systematic changes they should make to ensure this sort of thing doesn’t
happen again. Hard drives will fail. You can’t prevent that. But you can
ensure that the data customers care about are backed up and verified.

Not only that, they hooked me up with a pretty nice new dedicated
server.
![Smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Hardware-Specs-for-Haacked.com_8473/wlEmoticon-smile_2.png)

Even though they now are prepared to ensure VMs are backed up, I now
host on bare metal, in part because [my other
tenant](http://codinghorror.com/ "Jeff Atwood") moved off of the server
so I don’t really need to share it anymore. All miiiiiine!

Hardware
--------

-   **Case:**2U server dedicated server
-   **Processors:** 2x Intex Xeon CPU 3.20 GHZ (1 core, 2 logical
    processors) x64
-   **Memory:** 4.00 GB RAM
-   **OS Hard Drive:** C: 233 GB RAID 1 (2 physical drives)
-   **Data Hard Drive:** D: 467 GB RAID 5 (3 physical drives)

Software
--------

-   **OS:** Windows Server 2008 Datacenter SP2
-   **Database:** SQL Server 2008
-   **Web Server:** IIS 7 running ASP.NET 4
-   **Blog:**[Subtext](http://subtextproject.com)
-   **Backup:** In addition to the machine backus, I have a scheduled
    task that [7z](http://www.7-zip.org/ "7-zip") archives my web
    directories and also takes a SQL backup into a backups folder.
    Windows Live Mesh syncs those backup files to my home machine.

This server hosts the following sites:

-   [https://haacked.com/](https://haacked.com/) of course!
-   [http://code.haacked.com/](http://code.haacked.com/) Zip files
    containing code
-   [http://demo.haacked.com/](http://demo.haacked.com/) Working
    (mostly) demos associated with blog posts
-   [http://nuget.haacked.com/](http://nuget.haacked.com/) A NuGet
    package source for playing around with.
-   [http://subtextproject.com/](http://subtextproject.com/) The Subtext
    Project Homepage
-   And a few other minor sites.

For some of these sites, I plan to migrate them to other cloud based
solutions. For example, rather than have my own NuGet feed, I’ll just
use a [http://myget.org/](http://myget.org/) feed.

Even so, I plan to keep [https://haacked.com/](https://haacked.com/) on
this hardware for as long as The Small Business Authority lets me. It’s
a great way for me to keep my system administration skills from
completely atrophying and I like having a server at my disposal.

So thanks again to The Small Business Authority (though I admit, I liked
CrystalTech as a name better![Smile with tongue
out](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Hardware-Specs-for-Haacked.com_8473/wlEmoticon-smilewithtongueout_2.png))
for hosting this blog! And thank you for reading!

