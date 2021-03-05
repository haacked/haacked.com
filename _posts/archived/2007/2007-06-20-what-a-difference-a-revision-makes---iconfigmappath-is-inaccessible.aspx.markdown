---
title: What A Difference A Revision Makes - IConfigMapPath Is Inaccessible Due To
  Its Protection Level
tags: [tdd,aspnet]
redirect_from: "/archive/2007/06/19/what-a-difference-a-revision-makes---iconfigmappath-is-inaccessible.aspx/"
---

A few people mentioned that they had the following compiler error when
trying to compile
[HttpSimulator](https://haacked.com/archive/2007/06/19/unit-tests-web-code-without-a-web-server-using-httpsimulator.aspx "Unit Test Web Code Without A Web Server Using HttpSimulator"):

> HttpSimulator.cs(722,38): error CS0122:
> ’System.Web.Configuration.IConfigMapPath’ is inaccessible due to its
> protection level

Well you’re not alone. Our [build
server](http://build.subtextproject.com/ccnet/server/local/project/SubText-2.0/build/log20070620184757.xml/ViewBuildReport.aspx "Subtext Build Server")
is also having this same problem. Now before you curse me for releasing
something that doesn’t even compile, I’d like to point out that it
[works on my
machine](http://www.codinghorror.com/blog/archives/000818.html "Works on my machine certification").

[![works-on-my-machine-starburst](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IConfigMapPathIsInaccessibleDueToItsProt_1446B/works-on-my-machine-starburst_thumb.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IConfigMapPathIsInaccessibleDueToItsProt_1446B/works-on-my-machine-starburst.png)Fortunately,
we have our expert build problem solver, [Simone
Chiaretta](http://codeclimber.net.nz/ "Simone Chiaretta"), to look into
it.

After a bit of snooping, he discovered that the reason that it builds on
my machine is that I’m running Windows Vista with IIS 7.

The System.Web assembly on Vista is slightly newer than the one on
Windows 2003/XP.

-   VISTA: v2.0.50727.**312**
-   Windows 2003/XP: v2.0.50727.**210**

So there you have it. Now you finally have a good reason to upgrade to
Vista. HttpSimulator to the rescue! (Sorry, I know that punchline is
getting old).

I’ll see if I can create a workaround for those of you (such as our
build server) not running on Vista.

