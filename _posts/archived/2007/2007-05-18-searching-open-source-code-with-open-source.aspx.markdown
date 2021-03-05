---
title: Searching Open Source Code With Open Source
tags: [oss]
redirect_from: "/archive/2007/05/17/searching-open-source-code-with-open-source.aspx/"
---

First week on the job and I’ve already got the keys to the [company
blog](http://www.koders.com/blog/ "Koders Blog"). I just posted my first
post at [koders.com](http://koders.com/ "Open Source Search Engine")
announcing the latest set of [site
updates](http://www.koders.com/blog/?p=71 "Koders gets a tune-up").

One thing that I was surprised to learn this week, though it really
shouldn’t surprise me, is that Koders uses an open source search engine
to create the full-text index. More specifically, it uses
[Lucene.NET](http://incubator.apache.org/lucene.net/ "Lucene.NET"), a
port of the Java Lucene project.

I’m familiar with Lucene.NET because the
Subtext and [RSS
Bandit](http://www.rssbandit.org/ "RSS Bandit") projects both use it for
searching (though I was not the one to implement it in either case). As
far as I know, it pretty much is the de-facto standard for open source
search software on the .NET platform.

Of course Lucene.NET is only part of the Koders code search picture. It
provides the full-text indexing, but if you use the Koders search
engine, you’ll notice that there is some level of semantic analysis on
top of the text index. Otherwise, you wouldn’t be able to search for
method names and class names and such, not to mention the syntax
highlighting when viewing code.

I’m still learning about all the layers and extensions Koders built on
top of Lucene.NET. As I said in the post, those are probably topics I
can’t get write about in too much detail.

[![Screenshot of project
browser](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/SearchingOpenSourceWithOpenSource_C02C/image%7B0%7D_thumb.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/SearchingOpenSourceWithOpenSource_C02C/image%7B0%7D%5B2%5D.png "Subtext Code Browser on Koders")One
thing I should point out is that code search is only part of the
picture. Koders also has a pretty sweet code repository browser (click
on thumbnail for larger view).

My favorite open source project is now in the index and can be [viewed
here](http://www.koders.com/info.aspx?c=ProjectInfo&pid=DFV7667WQ72FL9EV6BL8TGSE3G "Subtext Project on Koders").

On a side note, I recently talked about [Search Driven
Development](https://haacked.com/archive/2007/03/16/increase-productivity-with-search-driven-development.aspx "Increase Productivity with Search Driven Development")
in the theoretical sense, but have been able to put it to good use
already at Koders.com. In the great developer tradition of
[dogfooding](http://en.wikipedia.org/wiki/Eat_one's_own_dog_food "Dogfooding on Wikipedia"),
I needed to look at some code from home before I had my VPN setup. It
was nice to be able to login to Koders internal [Enterprise
Edition](http://www.koders.com/corp/products/enterprise-code-search/ "Koders Enterprise Edition")
and find the snippet of code I needed.

