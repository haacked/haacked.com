---
layout: post
title: Known Installation Issues With ASP.NET MVC
date: 2008-12-03 -0800
comments: true
disqus_identifier: 18562
categories:
- asp.net mvc
- asp.net
redirect_from: "/archive/2008/12/02/known-installation-issues-with-asp.net-mvc.aspx/"
---

I’m working to try and keep internal release notes up to date so that I
don’t have this huge amount of work when we’re finally ready to release.
Yeah, I’m always trying something new by giving procrastination a boot
today.

These notes were sent to me by Jacques, a developer on the [ASP.NET
MVC](http://asp.net/mvc "ASP.NET MVC Website") feature team. I only
cleaned them up slightly.

The sections below contain descriptions and possible solutions for known
issues that may cause the installer to fail. The solutions have proven
successful in most cases.

Visual Studio Add-ins
---------------------

The ASP.NET MVC installer may fail when certain Visual Studio add-ins
are already installed. The final steps of the installation installs and
configures the MVC templates in Visual Studio. When the installer
encounters a problem during these steps, the installation will be
aborted and rolled back. MVC can be installed from a command prompt
using `msiexec` to produce a log file as follows:

**`msiexec /i AspNetMVCBeta-setup.msi /q /l*v mvc.log`**

If an error occurred the log file will contain an error message similar
to the example below.

> MSI (s) (C4:40) [20:45:32:977]: Note: 1: 1722 2:
> VisualStudio\_VSSetup\_Command 3: C:\\Program Files\\Microsoft Visual
> Studio 9.0\\Common7\\IDE\\devenv.exe 4: /setup
>
> MSI (s) (C4:40) [20:45:32:979]: Product: Microsoft ASP.NET MVC Beta --
> Error 1722. There is a problem with this Windows Installer package. A
> program run as part of the setup did not finish as expected. Contact
> your support personnel or package vendor. Action
> VisualStudio\_VSSetup\_Command, location: C:\\Program Files\\Microsoft
> Visual Studio 9.0\\Common7\\IDE\\devenv.exe, command: /setup
>
> Error 1722. There is a problem with this Windows Installer package. A
> program run as part of the setup did not finish as expected. Contact
> your support personnel or package vendor. Action
> VisualStudio\_VSSetup\_Command, location: C:\\Program Files\\Microsoft
> Visual Studio 9.0\\Common7\\IDE\\devenv.exe, command: /setup

This error is usually accompanied by a corresponding event that will be
logged in the Windows Event Viewer:

> Faulting application **devenv.exe**, version 9.0.30729.1, time stamp
> 0x488f2b50, faulting module unknown, version 0.0.0.0, time stamp
> 0x00000000, exception code 0xc0000005, fault offset 0x006c0061,
> process id 0x10e0, application start time 0x01c9355ee383bf70

In most cases, removing the add-ins before installing MVC, and then
reinstalling the add-ins, will resolve the problem. ASP.NET MVC
installations have been known to run into problems when the following
add-ins were already installed.:

-   PowerCommands
-   Clone Detective

Cryptographic Services
----------------------

In a few isolated cases, the Windows Event Viewer may contain an Error
event with event source **CAPI2** and event ID **513**. The event
message will contain the following: **Cryptograhpic Services failed
while processing the OnIdentity() call in the System Writer Object.**

The article at
[http://technet.microsoft.com/en-us/library/cc734021.aspx](http://technet.microsoft.com/en-us/library/cc734021.aspx)
describes various steps a user can take to correct the problem, but in
some cases simply stopping and restarting the Cryptographic services
should allow the installation to proceed.

