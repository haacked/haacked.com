---
title: Projects Winding Down. Off to Alaska Soon.
date: 2004-12-22 -0800
disqus_identifier: 1755
tags: []
redirect_from: "/archive/2004/12/21/projects-winding-down-off-to-alaska-soon.aspx/"
---

As with many blogs right now, my blog has been graced by the quiet
sounds of tumbleweeds rolling by due to a long period of lack of use.
The primary reason for my absence is an end of year push to get several
projects completed before I head off to vacation.

For my day job, I've been working on exposing our platform to cell
phones. I've built a series of ASP.NET controls that render a
proprietary markup for a browser like app that will run on the phones.

On the side, I've been writing a Windows service (not as hyped as Web
Services these days) to obtain market data via a socket server API. What
I like about this project is that the API provided an XSD so I was able
to generate objects to represent all the messages (Requests and
Responses) and used XML Serialization to send the messages over the
socket.

Also on the side, I've worked on an app to post data from a SQL database
over to a perl script via XML over HTTP.

Finally, I updated the unit tests for [RSS
Bandit](http://www.rssbandit.org/) not to require Cassini.dll to be
registered in the GAC. They are now truly self contained. At the same
time I also checked in my changes to the Shortcut management. Torsten
discovered some improvements I should make which I hope to get to in the
new year.

In any case, Akumi and I are flying to Alaska tonight to stay with my
family. It'll be a balmy -2 degrees when we arrive, so wish us well.

