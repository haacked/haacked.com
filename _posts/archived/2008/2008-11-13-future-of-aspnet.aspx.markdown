---
title: The Future of WebForms And ASP.NET MVC
date: 2008-11-13 -0800
tags: [aspnet]
- aspnetmvc
redirect_from: "/archive/2008/11/12/future-of-aspnet.aspx/"
---

I’ve heard a lot of concerns from people worried that the ASP.NET team
will stop sparing resources and support for Web Forms in favor of
ASP.NET MVC in the future. I thought I would try to address that concern
in this post based on my own observations.

At the PDC, a few people explicitly told me, not without a slight tinge
of anger, that they don’t get ASP.NET MVC and they like Web Forms just
fine thank you very much. Hey man, that’s totally cool with me! Please
don’t make me the poster boy for killing Web Forms. If you like Web
Forms, it’s here to stay. ;)

I can keep telling you that we’re [continuing to invest in Web
Forms](http://www.misfitgeek.com/Will+ASPNET+MVC+Be+The+Main+Web+UI+Platform+For+ASPNET.aspx "Will ASP.NET MVC be the main UI platform for ASP.NET")
until my face is blue, but that probably won’t be convincing. So I will
instead present the facts and let you draw your own conclusions.

ASP.NET Themes
--------------

If you watch the [ASP.NET 4.0 Roadmap talk at
PDC](http://channel9.msdn.com/pdc2008/PC20/ "ASP.NET 4.0 Roadmap at PDC"),
you’ll see that there are five main areas of investment that the ASP.NET
team is working on. I’ll provide a non-comprehensive brief summary of
the five here.

### Core Infrastructure

With our core infrastructure, we’re looking to address key customer pain
points and improve scale and performance.

One feature towards this goal is cache extensibility which will allow
plugging in other cache products such as Velocity as a cache provider.
We’ll also enhance ASP.NET Session State APIs. There are other
scalability investments I don’t even personally understand all too
deeply. ;)

To learn more about our cache extensibility plans, check out this [PDC
talk by Stefan
Schackow](http://channel9.msdn.com/pdc2008/PC41/ "Stefan Schackow's Cache Extensibility Talk at PDC").

### Web Forms

In WebForms, we’re looking to address Client IDs which allow developers
to control the id attribute value rendered by server controls. We’re
adding support for URL routing with Web Forms. We’re planning to improve
ViewState management by providing fine grain control over it. And we’re
making investments in making our controls more CSS friendly. There are
many other miscellaneous improvements to various control we’re making
that would require me to query and filter the bug database to list, and
I’m too lazy to do that right now.

### AJAX

With Ajax, we’re implementing client side templates and data binding.
Our team now owns the Ajax Control Toolkit so we’re looking at
opportunities to possibly roll some of those server controls into the
core framework. And of course, we’ve added jQuery to our offerings along
with jQuery Intellisense.

To see more about our investments here, check out [Bertrand Le
Roy’s](http://blogs.msdn.com/scothu/ "Tales from the Evil Empire") Ajax
[talk at
PDC](http://channel9.msdn.com/pdc2008/PC32/ "ASP.NET Ajax Futures").

### Data and Dynamic Data

In Dynamic Data (which technically could fall in the Web Forms bucket)
we’re looking to add support for an abstract data layer which would
allow for POCO scaffolding. We’re implementing many-to-many
relationships, enhanced filtering, enhanced meta-data, and adding new
field templates.

There’s a lot of cool stuff happening here. To get more details on this,
check out [Scott
Hunter’s](http://blogs.msdn.com/scothu/ "Scott Hunter's Blog") Dynamic
Data [talk at
PDC](http://channel9.msdn.com/pdc2008/PC30/ "ASP.NET Dynamic Data at PDC").

### ASP.NET MVC

We’re still working on releasing 1.0. In the future, we hope to leverage
some of the Dynamic Data work into ASP.NET MVC.

Notice here that [ASP.NET MVC](http://asp.net/mvc "ASP.NET MVC Website")
is just one of these five areas we’re investing in moving forward. It’s
not somehow starving our efforts in other areas.

Feature Sharing
---------------

One other theme I’d like to highlight is that when we evaluate new
features, we try and take a hard look at how it fits into the entire
ASP.NET architecture as a whole, looking both at ASP.NET MVC and ASP.NET
Web Forms. In many cases, we can share the bulk of the feature with both
platforms with a tiny bit of extra work.

For example, ASP.NET Routing was initially an ASP.NET MVC feature only,
but we saw it could be more broadly useful and it was shared with
Dynamic Data. It will eventually make its way into Web Forms as well.
Likewise, Dynamic Data started off as a Web Forms specific feature, but
much of it will make its way into ASP.NET MVC in the future.

Conclusions
-----------

It’s clear that ASP.NET MVC is getting a lot of attention in part
because it is shiny and new, and you know how us geeks loves us some
shiny new toys. Obviously, I don’t believe this is the only reason it’s
getting attention, as there is a lot of goodness in the framework, but I
can see how all this attention tends to skew perceptions slightly. To
put it in perspective, let’s look at the reality.

Currently, ASP.NET MVC hasn’t even been released yet. While the number
of users interested in and building on ASP.NET MVC is growing, it is
clearly a small number compared to the number of Web Form developers we
have.

Meanwhile, we have millions of ASP.NET developers productively using Web
Forms, many of whom are just fine with the Web Form model as it meets
their needs. As much as I love the ASP.NET MVC way of doing things, I
understand it doesn’t suit everyone, nor every scenario.

So with all this investment going on, I hope it’s clear that we are
continuing to invest in Web Forms along with the entire ASP.NET
framework.

