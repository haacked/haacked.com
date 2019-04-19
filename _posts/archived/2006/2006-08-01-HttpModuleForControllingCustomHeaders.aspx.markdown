---
title: HttpModule For Controlling Custom Headers
tags: [aspnet]
redirect_from: "/archive/2006/07/31/HttpModuleForControllingCustomHeaders.aspx/"
---

In a triumphant return after about three months of not blogging, [Barry
“idunno.org” Dorrans](http://idunno.org/ "Barry") has published an
[HttpModule for modifying custom HTTP
headers](http://idunno.org/displayBlog.aspx/2006080101 "HttpModule") in
response to a throw away comment by [Scott
Hanselman](http://www.hanselman.com/blog/ "Scott The Hanselnator") in
[his post on P3P
requests](http://www.hanselman.com/blog/TheImportanceOfP3PAndACompactPrivacyPolicy.aspx "P3P Headers").

The nice thing about Barry’s module is that it is much widely general
and applicable than just for P3P headers.

> This is a configurable HttpModule, allowing you to use web.config to
> specify what headers and values you wish added to requests.

So if you need tight control over your HTTP headers, this is the module
for you.

