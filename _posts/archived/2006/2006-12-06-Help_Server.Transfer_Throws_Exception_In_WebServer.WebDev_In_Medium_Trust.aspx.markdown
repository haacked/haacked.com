---
title: Help! Server.Transfer Throws Exception In WebServer.WebDev In Medium Trust
date: 2006-12-06 -0800
tags: []
redirect_from: "/archive/2006/12/05/Help_Server.Transfer_Throws_Exception_In_WebServer.WebDev_In_Medium_Trust.aspx/"
---

Ok, I could use some really expert help here. I really like using the
built in WebServer.WebDev Web Server that is a part of Visual Studio
2005. For one thing, it makes getting a new developer working on Subtext
(or any project) that much faster. Just get the latest code, and hit
CTRL+F5 to see the site in your browser. No pesky IIS set up.

Today though, I ran into my first real problem with this approach. When
running the latest Subtext code from our trunk, I am getting a
`SecurityException` during a call to `Server.Transfer`.

Stepping through the code in the debugger, the page I transfer to
executes just fine without throwing an exception.

Based on the stack trace, the exception occurs when the content is being
flushed to the client. A security demand for Unmanaged Code is the cause
of this during a call to the IHttpResponseElement.Send method of the
HttpResponseUnmanagedBufferElement class.

What I don’t understand is why this particular class is handling my
request instead of the `HttpResponseBufferElement` class? This code
seems to work fine when I use IIS, so I think it’s a problem with
WebServer.WebDev. Anybody know anyone who understands these internals
well enough to enlighten me? I’d be eternally grateful.

I posted this question on the [MSDN
forums](http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=993855&SiteID=1&mode=1 "MSDN Forums")
as well.

