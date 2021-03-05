---
title: ASP.NET 4 Web Server Here Shell Extension
tags: [aspnet]
redirect_from: "/archive/2009/10/26/aspnet4-webserver-here-shell-extension.aspx/"
---

Have you ever needed to quickly spawn a web server against a local
folder to preview a web application? If not, [what would you say you do
here](http://www.youtube.com/watch?v=iKa68kWkP48&feature=related "What would you say, you do here?")?

This is actually quite common for me since I receive a lot of zip files
containing web applications which reproduce a bug. After I unzip the
repro, I need a way to quickly point a web server at the folder and run
the web site.

A while back I wrote about [a useful registry
hack](https://haacked.com/archive/2008/06/24/vs2008-web-server-here-shell-extension.aspx "VS2008 Web Server Here Shell Extension")
to do just this. It adds a right click menu to start a web server
(Cassini) pointing to any folder. This was based on a [shell extension
by Robert
McLaws](http://weblogs.asp.net/rmclaws/archive/2005/10/25/428422.aspx "ASP.NET 2.0 Web Server Here").

Well that was soooo 2008. It’s almost 2010 and Visual Studio 2010 Beta 2
is out which means it’s time to update this shell extension to run an
ASP.NET 4 web server.

![add-web-server-here](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ASP.NET4WebServerHereShellExtension_91C4/add-web-server-here_3.png "add-web-server-here")

Obviously this is not rocket science as I merely copied my old settings
and updated the paths. But if you’re too lazy to look up the new file
paths, you can just copy these settings (changes are in bold).

### 32 bit (x86)

    Windows Registry Editor Version 5.00
     
    [HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Directory\shell\VS2010 WebServer]
    @="ASP.NET 4 Web Server Here"
     
    [HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Directory\shell\VS2010 WebServer\command]
    @="C:\\Program Files\\Common Files\\microsoft shared\\DevServer
    \\10.0\\Webdev.WebServer40.exe /port:8081 /path:\"%1\""

### 64 bit (x64)

    Windows Registry Editor Version 5.00
     
    [HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Directory\shell\VS2010 WebServer]
    @="ASP.NET 4 Web Server Here"
     
    [HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Directory\shell\VS2010 WebServer\command]
    @="C:\\Program Files (x86)\\Common Files\\microsoft shared\\DevServer
    \\10.0\\Webdev.WebServer40.exe /port:8081 /path:\"%1\""

I chose a different port and name for this shell extension so that it
lives side-by-side with my other one.

Of course, I wouldn’t even bother trying to copy these settings from
this blog post since I conveniently **[zipped up .reg files you can
run](http://code.haacked.com/util/AspNet4WebServerHereRegistry.zip "ASP.NET 4 Webserver Here")**.

