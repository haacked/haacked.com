---
title: 'Problem: Can''t Access Anything on LocalHost?'
tags: [tech]
redirect_from: "/archive/2005/07/10/trouble-accessing-localhost.aspx/"
---

Just the other day, I tried viewing a web application I’m developing on
my local machine. After navigating to http://localhost/MyWebApp/ I got a
blank browser screen. Nada. Zippo. Nothing. Not even the benefit of an
error message.

Fortunately, the nice people at
[SysInternals](http://www.sysinternals.com/) have graced the development
world with their suite of [fantastic
utilities](http://www.sysinternals.com/Utilities.html) including
[TCPView](http://www.sysinternals.com/Utilities/TcpView.html).

I ran TCPView and noticed that Skype.exe was listening on port 80. I
shut that down, restarted IIS and sure enough, my local sites were back
to their springy selves.

Turns out that the latest version of Skype attempts to listen in on port
80 and 443 by default, in case your firewall blocks all other ports.
That’s an interesting feature, and one I’ll probably thank them for some
day, but I wish they would have indicated that they were going to
attempt this.

To fix this issue, I went to the *Tools* | *Options* menu in Skype and
selected the *Connection* and unchecked the box next to “Use port 80 and
443 as alternatives for incoming connections.”

![](https://haacked.com/assets/images/SkypeOptions.jpg)

