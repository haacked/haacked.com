---
layout: post
title: "[Mix06] Microformats Autodiscovery"
date: 2006-03-21 -0800
comments: true
disqus_identifier: 12131
categories: []
redirect_from: "/archive/2006/03/20/Mix06MicroformatsAutodiscovery.aspx/"
---

It is unfortunate that I haven’t been able to blog much about the Mix06
conference so far, as there is so much worth writing about.
Unfortunately I have acquired a bad cold on top of a splitting headache
that will not go away. Such is the price one pays when partying with the
hard charging Microsofties (particularly with such Indigo team members
such as [Steve Maine](http://hyperthink.net/blog/ "Brain.Save()") and
[Clemens
Vasters](http://staff.newtelligence.net/clemensv/ "Clemen's Blog")).

When I get a chance, I will write more, but I wanted to drop a note in
here about
[Microformats](http://microformats.org/ "Microformats Website"). With
Bill Gates on stage saying that “We Need Microformats”, there is a
pretty good chance that Microformats are here to stay.

I attended a Birds of a Feather (BOF) session on Structured Blogging and
Microformats with [Tantek Çelik](http://tantek.com/ "Tantek's Blog") and
[Marc Canter](http://blog.broadbandmechanics.com/ "Marc Canter's Blog")

I asked a question on whether or not there is an autodiscovery story for
Microformats. Consider how an aggregator finds an RSS feed for a site.
In general, the aggregator scrapes the HTML looking for a several common
indicators of an RSS feed. Ideally the web page adds a \<link /\> tag
using the RSS autodiscovery format.

But I am not aware of any such mechanism for discovering my Microformat
contacts. Let’s say that I do not want to have all my contacts on my
home page. How would an aggregator find my contacts short of spidering
my whole site. Tantek’s answer was that they are essentially working on
it and there is nothing set yet.

One problem he pointed out is that sites may end up hosting a large
number of microformats. Adding an autodiscovery link in the HEAD section
of a page for each format supported could get unwieldy. One idea I had
would be to work on a “Table of Contents” microformat (I am sure someone
else will have a better name) that would serve as an index for where my
site hosts certain microformats of interest.

This would be an optional format. For example, for a site that uses very
few microformats. The site can make do with autodiscovery links within
the HEAD section of the home page. But if the site uses an exceedingly
large number of formats, it could have a single autodiscovery link to
the page that contains the Table of Contents Microformat. Aggregators
would then know where to find specific microformatted information.

Thoughts?

