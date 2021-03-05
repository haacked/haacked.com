---
title: Remote Desktop On A Non-Standard Port
tags: [tech,tools,tips]
redirect_from: "/archive/2006/10/15/Remote_Desktop_On_A_NonStandard_Port.aspx/"
---

For a project I worked on, we had an automated build server running
[CruiseControl.NET](http://confluence.public.thoughtworks.org/display/CCNET/Welcome+to+CruiseControl.NET "CruiseControl.NET Continuous Integration Server") hosted in a virtual machine.  We did the same thing for Subtext (project [is dead](https://haacked.com/archive/2013/12/02/dr-jekyll-and-mr-haack/)). 

Some of you may have multiple virtual servers running on the same machine.  Typically in such a setup (at least typically for me), each
virtual server won’t have its own public IP Address, instead sharing the public IP of the host computer.

This makes it a tad bit difficult to manage the virtual servers since using Remote Desktop to connect to the public IP will connect to the host computer and not the virtual machine.  The same thing applies to multiple *real* computers behind a firewall.

One solution (and the one I use) is set up each virtual server to run Terminal Services, but each one listens on a different port.  Then set up port-forwarding on your firewall to forward requests for the respective ports to the correct virtual machine.

### Configuring the Server

The setting for the Terminal Services port lives in the following registry key:

`HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\TerminalServer\WinStations\RDP-Tcp`
Open up Regedit, find this key, and look for the the **PortNumber** value.

![PortNumber Setting](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/RemoteDesktopOnANonStandardPort_1438D/TerminalServicesPortRegistrySetting8.png)

Double click on the PortNumber setting and enter in the port number you wish to use. Unless you think in hex (pat yourself on the back if you do), you might want to click on *decimal* before entering your new port
number.

![Port Number Value Dialog](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/RemoteDesktopOnANonStandardPort_1438D/TerminalServicesPortNumberValue4.png)

Or, you can use my creatively named *[Terminal Services Port
Changer](https://github.com/Haacked/TerminalServicesPortChanger/releases/download/v1.0.0/TerminalServicesPortChangerExtractor.exe)*
application, which is available with [source on GitHub](https://github.com/Haacked/TerminalServicesPortChanger/).  This is a simple five minute application that does one thing and one thing only. It allows you to change the port number that Terminal Services listens on.

![Terminal Services Port Changer](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/RemoteDesktopOnANonStandardPort_1438D/VelocitTSPortChanger4.png)

Remember, all the usual caveats apply about tinkering with the registry. You do so at your own risk.

### Connecting via Remote Desktop to the non-standard Port

Now that you have the server all set up, you need to connect to it. This is pretty easy.  Suppose you change the port for the virtual
machine to listen in on port 3900.  You simply append 3900 to the server name (or IP) when connecting via Remote Desktop.

![Port Changer App Screenshot](https://user-images.githubusercontent.com/19977/29098708-a0923384-7c55-11e7-9714-dcfe8d2fc907.png)

### Keep In Mind

Keep in mind that the user you attempt to connect with must have the *logon interactively* right as well as permissions to logon to the
terminal services session.  For more on that, check out this [extremely helpful
article](http://www.windowsnetworking.com/articles_tutorials/Windows_2003_Terminal_Services_Part2.html "Windows 2003 terminal services tutorial") with its trouble shooting section.

That’s pretty easy, no?  Now you should have no problem managing your legions of virtual servers.

**Related Posts:**

-   [Connecting to Terminal Services When All Active Sessions are Used](https://haacked.com/archive/2005/10/13/remote_desktop_to_console_session.aspx/ "How to connect to the console session.")
-   [Mapping Drives via Remote Desktop](https://blogs.msdn.microsoft.com/brendangrant/2009/02/17/the-most-useful-feature-of-remote-desktop-i-never-knew-about/ "Useful features of remote deskotp")
