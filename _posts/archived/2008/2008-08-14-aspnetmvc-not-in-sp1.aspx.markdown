---
title: ASP.NET MVC Is Not Part of ASP.NET 3.5 SP1
date: 2008-08-14 -0800
disqus_identifier: 18523
categories:
- asp.net mvc
- asp.net
redirect_from: "/archive/2008/08/13/aspnetmvc-not-in-sp1.aspx/"
---

I wanted to clear up a bit of confusion I’ve seen around the web about
ASP.NET MVC and the .NET Framework 3.5 Service Pack 1. ASP.NET MVC was
*not* released as part of SP1. I repeat, ASP.NET 3.5 SP1 does not
include ASP.NET MVC.

What *was* released with SP1 was the ASP.NET Routing feature, which is
in use by both ASP.NET MVC and Dynamic Data. The Routing feature is my
first Framework RTM feature to ship at Microsoft! We also shipped a
bunch of other features such as [Dynamic
Data](http://blogs.msdn.com/scothu/archive/2008/08/11/dynamic-data-rtm-is-released.aspx "Dynamic Data"),
and this [short list of breaking
changes](http://www.mostlylucid.net/archive/2008/08/14/know-issues--breaking-changes-in-asp.net-3.5-sp1.aspx "Breaking Changes in ASP.NET 3.5 SP1").

I hope that clears things up and I apologize for the confusion.

And for my next feat, I’m going to try and *read your mind, oooooh!*
Right now, you’re thinking something along the lines of,

> Ok, so ASP.NET MVC didn’t ship as part of SP1. When *is* it going to
> ship?!

Good question! Scott Hanselman [once
quipped](http://www.hanselman.com/blog/ASPNETMVCPreview4UsingAjaxAndAjaxForm.aspx#c40bae1e-c243-49dc-a172-41bca9e3edd9 "Quip")
that it would ship in a month that ends in “-ber”. He also [recently
quipped](http://www.hanselman.com/blog/HiddenGemsNotTheSameOld35SP1Post.aspx "Not the same old 3.5 SP1 Post"),

> Anyway, Phil has always said that MVC is on its own schedule and will
> ship when its done. Possibly when [Duke Nukem
> Forever](http://en.wikipedia.org/wiki/Duke_Nukem_Forever "Duke Nukem Forever on Wikipedia")
> ships.

That Scott, he’s so full of quips. ;)

In any case, he’s right in that MVC is pretty much on its own schedule
since the first RTM version will be a fully supported out-of-band
release, much like Atlas was back in the day.

The MVC team really doesn’t want to rush the first release. We’re taking
the time to do the best we can in laying the groundwork for future
releases. My hope is that we’ll have very few, if any, moments where we
we want to make a breaking change because we didn’t provide the right
amount of extensibility.

At the same time, we also really want to get ASP.NET MVC in your hands
in an RTM form soon so you can start using it for your clients who are
uncomfortable working with a beta technology. Trust me, we are not in
the business of the “perpetual-beta” and are working towards an RTM. As
Scott pointed out, our hope is to get it out before the end of the year.
But as most of you know about how software scheduling works, anything
can happen between now and tomorrow.

### Metrics

As we move towards the tail end of the development cycle, we’ve been
pushing hard to get our bug/approved change request count down, which I
recently twittered about. I asked Carl, our tester, to print out an
Excel graph of our bug count over time. It feels really good to walk by
his office every day and see the line trending down towards zero (though
occasionally, it ticks up a bit). I think it’s a huge motivator to try
and fix and close out work items.

At the same time, this graph is for our benefit only and not something
we’re being evaluated on by any managers, which is extremely important.
One of the dangers of any metric is that developers are smart and
they’ll do what they can to optimize the metric. For example, the danger
with this metric is that we might be tempted to not log feature requests
and bugs. Joel Spolsky [wrote about this
phenomena](http://www.joelonsoftware.com/news/20020715.html "Metrics")
when measuring the performance of knowledge workers a while back,

> But in the absence of 100% supervision, workers have an incentive to
> “work to the measurement,” concerning themselves solely with the
> measurement and not with the actual value or quality of their work.

Since we’re the only ones who care about this graph (nobody is looking
over our shoulder) and QA is very motivated to find bugs, I think it’s a
safe to use as a fun source of motivation. For the most part, watching
the graph move towards zero feels good. Those are the metrics I like,
the ones that inspire positive feelings among the team and a sense of
forward motion and momentum. :)

