---
title: Service Unavailable Errors in IIS 7 Are Killing Me
date: 2007-05-18 -0800 9:00 AM
tags: [tech]
redirect_from: "/archive/2007/05/17/service-unavailable-errors-in-iis-7-are-killing-me.aspx/"
---

UPDATE: [Problem
solved](https://haacked.com/archive/2007/05/21/the-iis-7-team-rocks.aspx "The IIS 7 Team Rocks")
thanks to some members of the IIS 7 team!

I am at my wits end trying to get IIS 7 to work on my Vista Ultimate box
and I have tried everything I can think of.

I’ve tried every step in the following tutorial, [Where did my IIS7
Server go? Troubleshooting “server not found”
errors](http://mvolo.com/blogs/serverside/archive/2006/10/16/Where-did-my-IIS7-server-go_3F00_-Troubleshooting-guide-for-_2200_server-not-found_2200_-errors.aspx "Troubleshooting IIS 7").
I also tried every step in [this post on troubleshooting “service
unavailable”
errors](http://mvolo.com/blogs/serverside/archive/2006/10/19/Where-did-my-IIS7-server-go_3F00_-Troubleshooting-_2200_service-unavailable_2200_-errors.aspx "Troubleshooting IIS 7").
Trust me when I say I went through every one of these steps twice. [Rob
Conery](http://blog.wekeroad.com/ "Rob Conery’s Blog") can back me up on
this because he watched me do so in a when I shared my desktop with him
via GoToMeeting.

As far as I can tell, IIS 7 or http.sys must be corrupted somehow and
the only thing left for me to try is to repave my machine and reinstall
Vista. Unless of course one of my dear readers has an insight that will
help me solve this, or knows someone who does.

### The Problem

I’m running Vista Ultimate which has IIS 7 installed. When I navigate to
<http://localhost/> or
[http://localhost/iisstart.htm](http://localhost/iisstart.htm), I get an
HTTP Error 503 Service Unavailable message.

### What I’ve Tried

Confirmed that [Skype is not listening to port
80](https://haacked.com/archive/2005/07/11/trouble-accessing-localhost.aspx "Can’t Access Anything on Localhost?")
(Skype tries to listen on port 80 by default).

​1. Confirmed that App Pools were configured correctly and started.

​2. Ran the following command: *appcmd list apppools* which produced the
output:

> `APPPOOL "DefaultAppPool" (MgdVersion:v2.0,MgdMode:Integrated,state:Started) APPPOOL "Classic .NET AppPool" (MgdVersion:v2.0,MgdMode:Classic,state:Started)`

​3. Confirmed that Website was started.

​4. Ran the following command: *netstat -a -o -b* which produced the
output:

> `TCP [::]:80 METAVERSE:0 LISTENING 4Can not obtain ownership information`

The 4 there is the PID which I confirmed to be *System* as in the *NT
Kernal & System*.

​5. Confirmed bindings were configured in *IIS Manager* and set for
*localhost* port 80.

​6. No error messages in the event log under *System* nor *Application*.

​7. Made sure that my user account and the *Network Service* account
both have access to the *c:\\inetpub\\wwwroot* directory.

​8. Tried browsing to <http://MYCOMPUTERNAME/>, <http://localhost:80/>,
[http://127.0.0.1/](http://127.0.0.1/), etc... (I was getting
desparate).

​9. Tried changing the default AppPool’s *managed pipeline mode* to
*Classic*.

​10. Tried changing the default AppPool’s .NET framework version to *No
Managed Code* (recall that I am trying to request a static HTML page).

​11. Able to ping localhost.

​12. Tried to *telnet localhost 80* and then issue the command *GET /*
and received the same message.

​13. Double checked that all the *Handler Mappings* were enabled.

​14. Made sure *Anonymous Authentication* was enabled. Heck, I tried it
with them all enabled and tried it with only *Windows Authentication*
enabled.

​15. Authorization Rules has one rule: *Mode=allow*, *Users=All Users*.

​16. Enabled *Failed Request Tracking*. Nothing showed up in the logs.

​17. Uninstalled and [reinstalled IIS
7](http://blogs.msdn.com/davbosch/archive/2006/04/30/587096.aspx "Installing IIS 7").

​18. Tried pulling my hair out and rending my garments and sacrificing
chickens^1^.

### Any ideas?

So there you go. I’ve tried everything I can think of and now I appeal
to you for help.

The funny thing is that this works on my Vista box at work and I
compared every setting in IIS. This confirms in my mind that something
got fubar’d. But I hesitate to repave my machine just yet in the hope
that someone out there has some definitive answers for me.

^1^ *No actual chickens were harmed in troubleshooting this problem.*

