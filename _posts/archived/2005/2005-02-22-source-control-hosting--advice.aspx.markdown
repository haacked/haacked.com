---
title: Source Control Hosting - Advice?
date: 2005-02-22 -0800
tags: [source-control]
redirect_from: "/archive/2005/02/21/source-control-hosting--advice.aspx/"
---

For my personal coding projects at home, I've been using the free one-user version of [SourceGear vault](http://www.sourcegear.com/vault/) for my personal source control needs. As you know, having proper source
control is #1 on [The Joel Test](http://www.joelonsoftware.com/articles/fog0000000043.html).

But since I'm embarking on a project with Micah who lives in San Francisco, we need something better than to host on my little Shuttle system at home. We need hosting!

After some preliminary research, I'm leaning towards a [semi-dedicated server from WebHost4Life](http://www.webhost4life.com/manageddedi.asp). The Basic plan includes 150 GB of bandwidth, 2000 MB of Space, 2GB of SQL Server 2000, ASP.NET 1.1, Sharepoint Team/Windows Services, and on and on. Plus, no setup fees, and it's $79.35 a month. This is a LOT cheaper than an equivalent option at DataPipe. DataPipe offers 5 nine uptime, but I think the 3 nines will be adequate.

This server will host our source control (which requires SQL Server and .NET) as well as a staging site for our client. If you know of a better option, please leave a comment. I'll let you know what we end up deciding.
