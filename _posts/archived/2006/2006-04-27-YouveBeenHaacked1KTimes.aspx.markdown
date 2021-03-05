---
title: You've Been Haacked 1K Times
tags: [meta]
redirect_from: "/archive/2006/04/26/YouveBeenHaacked1KTimes.aspx/"
---

![Axe](https://haacked.com/assets/images/bloodyAxe.jpg) Well this post marks my
1000th post on this blog. Since I am totally on board with the base 10
system, that makes this noteworthy to me. If we all used the hexadecimal
system (base 16), then this post would be my 3E8th post which really
wouldn’t warrant me even mentioning it in the first place. Be glad we
are on base 10.

So how shall I observe the 1000th time that you’ve been Haacked?
Obviously by writing about ways to avoid getting hacked.

I have a nice brand spanking new workstation so I figured now is as good
a time as any to make the jump to running as a non-admin. This is what
the security folks refer to as the principle of running with the least
privileges. This is also referred to as LUA which stands for
Least-Privileged User Account or Limited User Account depending on who
you ask.

Hopefully I am behind the times and most of you are already running as
LUA. But just in case, I will continue to plod on. This will be my third
attempt to run as a non-admin, but the tools have gotten better since I
last made the dive.

Temporarily Elevated Privileges
-------------------------------

One of my favorite approaches to dealing with privileges is the idea of
temporarily elevating privileges. This is in contrast to the approach in
which you use *RunAs* to run a program using another user’s credentials.
There are two ways to do this.

### MakeMeAdmin

First of of all, there is the excellent batch file **MakeMeAdmin**
written by Aaron Margosis and announced in [this blog
post](http://blogs.msdn.com/aaron_margosis/archive/2004/07/24/193721.aspx "MakeMeAdmin").

This batch file temporarily elevates your normal account to an admin.
This is useful in those scenarios when you need to install software and
you want the per-user settings to apply to your profile, not the
administrator’s profile.

### WinSUDO

[WinSUDO](http://home.toadlife.net/winsudo/ "WinSUDO") was inspired by
the MakeMeAdmin script, but consists of a client and server piece.
Instead of relying on a command window, WinSUDO installs as a shell
extension. Right click on a program in Explorer and select the *Sudo*
menu option. I haven’t tried it just yet as the author is in the middle
of a rewrite, but it’s worth keeping an eye on it.

Setting Shortcuts To Prompt For User
------------------------------------

If you right click on a shortcut and click the *Properties* menu item.
Then click the *Advanced* button. You can check an option to *Run with
different credentials*. When you double click on the shortcut, it
prompts you with an option to run as yourself, or run as a different
user.

Create Your Own Control Panel Shortcut
--------------------------------------

Control panel applets are a bit of a challenge since the *RunAs* option
is not there when you right click an applet or Control Panel itself. So
I went ahead and created my own control panel shortcut.

-   Right Click on the desktop and select *New* | *Shortcut* from the
    context menu.
-   For the location, just enter *control.exe*. For the name, I entered
    *Control Panel*.
-   Right click on the shortcut and click *Change Icon...* (looks
    matter!).
-   Select the icon that looks like the control panel (see the image
    below).
-   Now click on the *Advanced...* button and check the *Run with
    different credentials* option.

![Control Panel
Selection](https://haacked.com/assets/images/ControlPanelIconSelection.gif)

Visual Studio Development {.clear}
-------------------------

The article [Developing Software in Visual Studio .NET with
Non-Administrative
Privileges](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dv_vstechart/html/tchDevelopingSoftwareInVisualStudioNETWithNon-AdministrativePrivileges.asp "VS.NET as a non-admin")
is quite helpful in outlining the issues you may run into as a
developer.

One particularly challenging issue is debugging ASP.NET applications on
your local machine as a non-admin. Since a normal user doesn’t have the
rights to debug applications running in the context of other users’s
accounts. The article suggests editing machine.config and configuring
ASP.NET to run under your own account.

I really don’t like this solution. If you open up the *Group Policy
Editor* (*Start* | *Run* | *Type in "gpedit.msc" without the quotes*)
you can find a “Debug programs” policy option. I may try adding that to
my account instead, but I need to find out if it would open up a
security risk that totally invalidates the security benefits of running
as a LUA in the first place.

Community
---------

If you are interested in learning more, check out [this
site](http://nonadmin.editme.com/ "NonAdmin") devoted to a community of
PC users who want to run without admin privileges. They have some great
pointers to articles and tools to help mitigate the royal pain it is to
run as non-admin on Windows XP.

Conclusion
----------

Hopefully this time running as a non-admin will stick. I will keep you
posted during the next 1000 posts.

