---
title: Tip For Managing Remote VMWare Server
date: 2006-10-11 -0800 9:00 AM
tags: [tips]
redirect_from: "/archive/2006/10/10/tip_for_managing_remote_vmware_server.aspx/"
---

Quick tip for you if you need to remotely connect to a server with
VMWare Server installed in order to manage the virtual server. 

VMWare Server Console doesn’t work correctly if you Remote Desktop or
Terminal in. You have to physically be at the machine or [Remote Desktop
into the Console
session](https://haacked.com/archive/2005/10/13/Remote_Desktop_To_Console_Session.aspx "Connect To Console").

The symptoms I ran into was that I could not open a virtual machine, and
when I tried to create a new one, I got an “Invalid Handle” error.

