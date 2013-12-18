---
layout: post
title: "Remote Desktop On A Non-Standard Port"
date: 2006-10-16 -0800
comments: true
disqus_identifier: 18090
categories: [tech]
---
For a project I’m working on, we have an automated build server running
[CruiseControl.NET](http://confluence.public.thoughtworks.org/display/CCNET/Welcome+to+CruiseControl.NET "CruiseControl.NET Continuous Integration Server") hosted
in a virtual machine.  We do the same thing for
[Subtext](http://subtextproject.com/ "Subtext Project"). 

Some of you may have multiple virtual servers running on the same
machine.  Typically in such a setup (at least typically for me), each
virtual server won’t have its own public IP Address, instead sharing the
public IP of the host computer.

This makes it a tad bit difficult to manage the virtual servers since
using Remote Desktop to connect to the public IP will connect to the
host computer and not the virtual machine.  The same thing applies to
multiple *real* computers behind a firewall.

One solution (and the one I use) is set up each virtual server to run
Terminal Services, but each one listens on a different port.  Then set
up port-forwarding on your firewall to forward requests for the
respective ports to the correct virtual machine.

### Configuring the Server

The setting for the Terminal Services port lives in the following
registry key:

`HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\TerminalServer\WinStations\RDP-Tcp`{.registry
.smallnote}

Open up Regedit, find this key, and look for the the **PortNumber**
value.

![PortNumber
Setting](http://haacked.com/images/haacked_com/WindowsLiveWriter/RemoteDesktopOnANonStandardPort_1438D/TerminalServicesPortRegistrySetting8.png)

Double click on the PortNumber setting and enter in the port number you
wish to use. Unless you think in hex (pat yourself on the back if you
do), you might want to click on *decimal* before entering your new port
number.

![Port Number Value
Dialog](http://haacked.com/images/haacked_com/WindowsLiveWriter/RemoteDesktopOnANonStandardPort_1438D/TerminalServicesPortNumberValue4.png)

Or, you can use my creatively named *[VelocIT Terminal Services Port
Changer](http://tools.veloc-it.com/tabid/58/grm2id/18/Default.aspx "Application with Source")*
application, which is available with source.  This is a simple five
minute application that does one thing and one thing only. It allows you
to change the port number that Terminal Services listens on.

![VelocIT Terminal Services Port
Changer](http://haacked.com/images/haacked_com/WindowsLiveWriter/RemoteDesktopOnANonStandardPort_1438D/VelocitTSPortChanger4.png)

Remember, all the usual caveats apply about tinkering with the registry.
You do so at your own risk.

### Connecting via Remote Desktop to the non-standard Port

Now that you have the server all set up, you need to connect to it. 
This is pretty easy.  Suppose you change the port for the virtual
machine to listen in on port 3900.  You simply append 3900 to the server
name (or IP) when connecting via Remote Desktop.

![Remote
Desktop](http://haacked.com/images/haacked_com/WindowsLiveWriter/RemoteDesktopOnANonStandardPort_1438D/RemoteDesktopNonStandard4.png)

### Keep In Mind

Keep in mind that the user you attempt to connect with must have the
*logon interactively* right as well as permissions to logon to the
terminal services session.  For more on that, check out this [extremely
helpful
article](http://www.windowsnetworking.com/articles_tutorials/Windows_2003_Terminal_Services_Part2.html "Windows 2003 terminal services tutorial")
with its trouble shooting section.

That’s pretty easy, no?  Now you should have no problem managing your
legions of virtual servers.

**Related Posts:**

-   [Connecting to Terminal Services When All Active Sessions are
    Used](http://haacked.com/archive/2005/10/13/Remote_Desktop_To_Console_Session.aspx "How to connect to the console session.")
-   [Mapping Drives via Remote
    Desktop](http://stevenharman.net/blog/archive/2006/10/22/Mapping_Drives_via_Remote_Desktop.aspx "Mapping Drives")


