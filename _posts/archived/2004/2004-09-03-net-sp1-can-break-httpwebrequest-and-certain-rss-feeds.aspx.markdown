---
title: ".NET SP1 Can Break HttpWebRequest (and certain RSS Feeds)"
date: 2004-09-03 -0800 9:00 AM
tags: [dotnet]
redirect_from: "/archive/2004/09/02/net-sp1-can-break-httpwebrequest-and-certain-rss-feeds.aspx/"
---

If you installed SP1 for the .NET framework, you may notice that certain
feeds are broken and return an HTTP Protocol Error.
[Dare](http://www.25hoursaday.com/weblog/) looked into this and [posted
an explanation and workaround to the
problem](http://www.25hoursaday.com/weblog/PermaLink.aspx?guid=d98a420e-6679-474c-865a-30578338ceb8).

Apparently a lot of web servers out there are a bit loose with the HTTP
specification while SP1 tightens compliance. So c'mon people, stick the
chest out, shoulders back, stand up straight, and stick closely to the
spec.

