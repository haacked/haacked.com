---
layout: post
title: Hotfix for Installing ASP.NET MVC With Azure, Power Commands, or Resharper
date: 2009-03-06 -0800
comments: true
disqus_identifier: 18596
categories:
- asp.net mvc
redirect_from: "/archive/2009/03/05/hotfix-for-installing-aspnetmvc.aspx/"
---

Yesterday, I wrote about troubleshooting [Windows MSI
Installers](https://haacked.com/archive/2009/03/05/troubleshooting-installers.aspx "Installer Troubleshooting")
and talked about the pain we here feel when an installation fails. Turns
out, it’s not *always* our fault. ;) It appears there’s a hotfix
released for Visual Studio which addresses a problem with installing
ASP.NET MVC when you have a third party add-in installed. I mentioned
the three above because they are among the most commonly used add-ins
which run into problems.

You can [read about the
Hotfix](http://blogs.msdn.com/webdevtools/archive/2009/03/03/hotfix-available-for-asp-net-mvc-crashes-with-azure-power-commands-resharper.aspx "Hotfix")
on the Visual Web Developer blog or simply go here to [**download
it**](https://connect.microsoft.com/VisualStudio/Downloads/DownloadDetails.aspx?DownloadID=16827&wa=wsignin1.0 "Hotfix Download Page").

Note, that this doesn’t solve the problem for all add-ins. For example,
someone reported an installation failure when they had Clone Detective
installed. Uninstalling Clone Detective, installing ASP.NET MVC, and
then reinstalling Clone Detective solved the problem. We’re still
looking into why our installers are having problems with these add-ins.

