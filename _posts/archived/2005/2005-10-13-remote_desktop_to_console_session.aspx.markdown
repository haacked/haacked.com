---
layout: post
title: Connecting to Terminal Services When All Active Sessions are Used
date: 2005-10-13 -0800
comments: true
disqus_identifier: 10783
categories: []
redirect_from:
  /archive/2005/10/12/remote_desktop_to_console_session.aspx/
  /archive/2005/10/15/remote_desktop_to_console_session.aspx/
---

UPDATE: If you are using Windows Server 2008, the switch is **`/admin`** not `/console`. See this post [for details](http://blogs.msdn.com/ts/archive/2007/12/17/changes-to-remote-administration-in-windows-server-2008.aspx "details").

We use Remote Desktop (Terminal Services) to remotely manage a Windows 2003 server that is not part of our domain. Recently we ran into the two user limit for remote desktop connections, which barred anyone from connecting.

[Jon](http://weblogs.asp.net/jgalloway/) discovered a neat little trick that got us in. He ran the following command from the command line:

> `mstsc -console`

It turns out that mstsc.exe is the remote desktop connection application. The `-console` flag specifies that we want to connect to
the console session of a server. Since we generally launch Remote Desktop from the icon, we almost always leave this console session free.
Nice!

When I got back in the server, I used the *Terminal Services Manager*tool to reset the disconnected and idle sessions. I then used *Terminal Services Configuration* tool to set a timeout for disconnected sessions. Finally, I remembered to logout rather than simply close the remote desktop window. Simply closing remote desktop doesn’t reset the session.
