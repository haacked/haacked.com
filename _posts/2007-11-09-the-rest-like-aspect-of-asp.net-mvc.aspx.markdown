---
layout: post
title: "The REST-Like Aspect Of ASP.NET MVC"
date: 2007-11-09 -0800
comments: true
disqus_identifier: 18420
categories: [asp.net,asp.net mvc]
---
While at DevConnections/OpenForce, I had some great conversations with
various people on the topic of ASP.NET MVC. While many expressed their
excitement about the framework and asked when they could see the bits
(soon, I promise), there were several who had mixed feelings about it. I
relish these conversations because it helps highlight the areas in which
we need to put more work in and helps me become a better communicator
about it.

One thing I’ve noticed is that most of my conversations focused too much
on the MVC part of the equation. Dino Esposito (who I met very briefly),
wrote an [insightful
post](http://weblogs.asp.net/despos/archive/2007/11/07/devconn07-fall-mvc-fx-is-it-car-or-motorcycle.aspx "Dino Esposito")
pointing out that it isn’t the MVC part of the framework that is most
compelling:

> So what's IMHO the main aspect of the MVC framework? It uses a
> REST-like approach to ASP.NET Web development. It implements each
> request to the Web server as an HTTP call to something that can be
> logically described as a "remote service endpoint". The target URL
> contains all that is needed to identify the controller that will
> process the request up to generating the response--whatever response
> format you need. I see more REST than MVC in this model. And, more
> importantly, REST is a more appropriate pattern to describe what pages
> created with the MVC framework actually do.

In describing the framework, I’ve tended to focus on the MVC part of it
and the benefits in separation of concerns and testability. However,
others have pointed out that by keeping the UI thin, a good developer
could do all these things without MVC. So what's the benefit of the MVC
framework?

I agree, yet I still think that MVC provides even greater support for
Test Driven Development than before both in substance and in style, so
even in that regard, there’s a benefit. I need to elaborate on this
point, but I’ll save that for another time.

But MVC is not the only benefit of the MVC framework. I think the
REST-like nature is a big selling point. Naturally, the next question
is, *well why should I care about that*?

Fair question. Many developers won’t care and perhaps shouldn’t. In
those cases, this framework might not be a good fit. Some developers do
care and desire a more REST-like approach. In this case, I think the MVC
framework will be a good fit.

This is not a satisfying answer, I know. In a future post, I hope to
answer that question better. In what situations should developers care
about REST and in which situations, should they not? For now, I really
should get some sleep. Over and out.

Technorati Tags: [ASP.NET
MVC](http://technorati.com/tags/aspnetmvc),[REST](http://technorati.com/tags/REST)

