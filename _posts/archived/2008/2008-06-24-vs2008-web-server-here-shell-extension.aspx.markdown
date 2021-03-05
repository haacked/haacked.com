---
title: VS2008 Web Server Here Shell Extension
tags: [code,tools]
redirect_from: "/archive/2008/06/23/vs2008-web-server-here-shell-extension.aspx/"
---

UPDATE: Updated the registry settings per James Curran’s comment. Thanks
James!

One of the most useful registry hacks I use on a regular basis is one
[Robert McLaws](http://weblogs.asp.net/rmclaws/ "Robert McLaws") wrote,
the [“ASP.NET 2.0 Web Server Here” Shell
Extension](http://weblogs.asp.net/rmclaws/archive/2005/10/25/428422.aspx "Web Server Here Extension").
This shell extension adds a right click menu on any folder that will
start `WebDev.WebServer.exe` (aka Cassini) pointing to that directory.

![Webserver-Here](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/VS2008WebServerHereShellExtension_87F4/Webserver-Here_3.png "Webserver-Here")

I recently had to repave my work machine and I couldn’t find the .reg
file I created that would recreate this shell extension. When I brought
up Robert’s page, I noticed that the settings he has are out of date for
Visual Studio 2008.

Here is the updated registry settings for VS2008 (note, edit the
registry at your own risk and this only has the [**works on my machine**
seal of
approval](http://www.codinghorror.com/blog/archives/000818.html "Works on My Machine Certification")).

### 32 bit (x86)

    Windows Registry Editor Version 5.00

    [HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Directory\shell\VS2008 WebServer]
    @="ASP.NET Web Server Here"

    [HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Directory\shell\VS2008 WebServer\command]
    @="C:\\Program Files\\Common Files\\microsoft shared\\DevServer
    \\9.0\\Webdev.WebServer.exe /port:8080 /path:\"%1\""

### 64 bit (x64)

    Windows Registry Editor Version 5.00

    [HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Directory\shell\VS2008 WebServer]
    @="ASP.NET Web Server Here"

    [HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Directory\shell\VS2008 WebServer\command]
    @="C:\\Program Files (x86)\\Common Files\\microsoft shared\\DevServer
    \\9.0\\Webdev.WebServer.exe /port:8080 /path:\"%1\""

For convenience, here is [a zip file containing the reg
files](https://haacked.com/code/Webserver-Here.zip "WebServer Here Reg Files").
The x64 one I tested on my machine. The x86 one I did not. If you
installed Visual Studio into a non-standard directory, you might have to
change the path within the registry file.

