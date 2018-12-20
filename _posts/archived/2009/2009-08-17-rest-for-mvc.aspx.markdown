---
title: Rest For ASP.NET MVC SDK and Sample
date: 2009-08-17 -0800
tags: [aspnetmvc]
redirect_from: "/archive/2009/08/16/rest-for-mvc.aspx/"
---

When building a web application, it’s a common desire to want to expose
a simple Web API along with the HTML user interface to enable various
mash-up scenarios or to simply make accessing structured data easy from
the same application.

A common question that comes up is when to use ASP.NET MVC to build out
REST-ful services and when to use WCF? I’ve answered the question
before, but not as well as [Ayende
does](http://ayende.com/Blog/archive/2009/08/17/taking-advantage-on-the-data-transfer-tier.aspx "Ayende")
(when discussing a different topic). This is what I tried to express.

> In many cases, the application itself is the only reason for
> development *[of the service]*

“*[of the service]*” added by me. In other words, when the only reason
for the service’s existence is to service the one application you’re
currently building, it may make more sense  would stick with the simple
case of using ASP.NET MVC. This is commonly the case when the only
client to your JSON service is your web application’s Ajax code.

When your service is intended to serve multiple clients (not just your
one application) or hit large scale usage, then moving to a real
services layer such as WCF may be more appropriate.

However, there is now a third hybrid choice that blends ASP.NET and WCF.
The WCF team saw that many developers building ASP.NET MVC apps are more
comfortable with the ASP.NET MVC programming model, but still want to
supply more rich RESTful services from their web applications. So the
WCF team put together an SDK and samples for building REST services
using ASP.NET MVC.

You can download the samples and SDK from **[ASP.NET MVC 1.0 page on
CodePlex](http://aspnet.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=24471#DownloadId=79561 "Download Rest for MVC")**.

Do read through the overview document as it describes the changes you’ll
need to make to an application to make use of this framework. Also, the
zip file includes several sample movie applications which demonstrate
various scenarios and compares them to the baseline of not using the
REST approach.

At this point in time, this is a sample and SDK hosted on our CodePlex
site, but many of the features are in consideration for a future release
of ASP.NET MVC (no specifics yet).

This is where you come in. We are keenly interested in hearing feedback
on this SDK. Is it important to you, is it not? Does it do what you
need? Does it need improvement. Let us know what you think. Thanks!

