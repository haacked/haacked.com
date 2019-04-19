---
title: SOAP vs. REST in the Real World
date: 2004-10-26 -0800
tags: [code]
redirect_from: "/archive/2004/10/25/soap-vs-rest.aspx/"
---

There’s a lot of focus these days on SOAP vs REST and the proliferation
of WS-\* specifications. Sometimes you wonder if WS-\* solves problems
that aren't all that common or have already been solved.

For example, some in the REST camp will say, HTTP has security built in.
It\#8217;s called SSL. Why not use it instead of building WS-Security.

Another example is WS-Addressing. This places addressing information
within the SOAP envelope so that the message can be delivered via
transports other than HTTP. At first glance, I wonder how often this
will be useful for web services when HTTP is the predominant mode of
transport.

However, Pat Caldwell illustrates a [real world
scenario](http://www.cauldwell.net/patrick/blog/PermaLink,guid,9ba58b84-f978-4b49-8aa5-5c2c0e0c9458.aspx "WS-Addressing")
in which WS-Addressing solved a real need that REST couldn\#8217;t and
doesn\#8217;t address.

REST has its place, but for some of those nitty gritty situations, SOAP
keeps everything clean.

