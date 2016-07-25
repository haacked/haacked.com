---
layout: post
title: Trouble With Your Firewall Software After Installing Windows XP SP2?
date: 2004-09-13 -0800
comments: true
disqus_identifier: 1200
categories: []
redirect_from: "/archive/2004/09/12/trouble-with-your-firewall-software-after-installing-windows-xp-sp2.aspx/"
---

If you're having problems with your computer after upgrading to Windows
XP SP2 and you are using a software firewall such as ZoneAlarm or
BlackIce, try uninstalling your firewall software and re-installing it.
That solved the problem for me. BlackIce recommends uninstalling their
firewall before upgrading to SP2.

On two different machines (one with ZoneAlarm and the other with
BlackIce), I had simply upgraded to SP2 and turned off the Windows
Firewall. Even so, my machines would freeze up, especially when
performing network operations. It seems that even with the Windows
Firewall off, there's some sort of contention for the network devices
that is resolved by reinstalling.

