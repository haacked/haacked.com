---
title: Subtext Cruising In CruiseControl.NET
tags: [ci,subtext]
redirect_from: "/archive/2006/05/02/subtextcruisingincruisecontrol.net.aspx/"
---

![Cruise Control Logo](https://haacked.com/assets/images/ccnet_logo.gif) With
many thanks to [Simone
Chiaretta](http://blogs.ugidotnet.org/piyo/ "FoxyBlog") (blog in
Italian) for his effort, we now have a working
[CruiseControl.NET](http://confluence.public.thoughtworks.org/display/CCNET/Welcome+to+CruiseControl.NET "Cruise Control Homepage")
setup for [Subtext](http://subtextproject.com/ "Subtext Project Site").
Check out the chrome (or lack thereof) on our CCNET
[dashboard](http://build.subtextproject.com/ccnet/ "Subtext Cruise Control Panel").

Though we have some kinks to work out (the build is apparently broken
according to CCNET), I am particularly happy about getting this up and
running. As a distributed open source project, it is part of our master
plan to follow agile development practices that are well suited to
building Subtext. Continuous integration is particularly important for
us since we are in different time zones and locations.

The CCNet server is running on Windows 2003 within a VMWare Virtual
Server on my old development workstation. That makes our build server
very portable should we decide to host it elsewhere someday.

Once we get the kinks worked out, you can download the CCTray system
tray applet and keep tabs on the development of Subtext. You’ll know
exactly who and when someone breaks the build. How is that for **open**
source?

To get CCTray to work, make sure your firewall allows **TCP traffic over
port 21234**. Then add the server **build.subtextproject.com:21234**.

Though for now, let’s be adults and keep the teasing to a minimum. I
apparently broke the build, but I am betting it is a configuration issue
with moving the virtual server from Italy to Los Angeles. Ciao!

