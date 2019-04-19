---
title: When to Build a Smart Client Over a Web App
date: 2005-11-02 -0800 9:00 AM
tags: [web]
redirect_from: "/archive/2005/11/01/when-to-build-a-smart-client-over-a-web-app.aspx/"
---

So when should you choose to build a smart client rather than a web
application (or in addition to). The typical answer I’ve seen is when
extreme usability is required. As AJAX techniques get more mature, I
think this will become less of a consideration.

As I thought of it more, it hit me. The same thing Jeff Atwood [said
about strored
procedures](http://www.codinghorror.com/blog/archives/000117.html),
â€œStored Procedures should be considered database assembly language:
for use in only the most performance critical situationsâ€ applies to
applications.

**Smart Clients should be considered Application assembly language: for
use in only the most performance critical situations.**

This is why you won’t see the next version Halo running in a browser
(though you might see the first version someday). This is also why you
won’t run Photoshop in a browser. Performance is critical in such
applications.

There are other important considerations as well, such as **security**.
I wouldn’t run an RSA key ring in a browser. Also apps that constantly
run and perform a service on your machine. For example, system tray
icons, though even that concept [seems to be
changing](http://microsoftgadgets.com/default.aspx).

Anyways, I need to chew on this some more. At this point in time,
usability is still a concern. That is why I run a client [RSS
Aggregator](http://www.rssbandit.org/) and use
[w.Bloggar](http://www.wbloggar.com/) to post to my blog.

Although the deployment issues for web-based applications are great, the
development environments for AJAX applications pale in comparison to
writing a rich UI. There’s just something about writing object oriented
compiled code that makes me cringe at writing everything as javascript.

