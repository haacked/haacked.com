---
title: Why Should I Care About The Wire Format In SOAP?
tags: [code]
redirect_from:
  - "/archive/2004/07/22/why-should-i-care-about-the-wire-format-in-soap.aspx/"
  - "/archive/2004/07/23/843.aspx/"
---

![Soap](/assets/images/soap.jpg)In this
[post](http://pluralsight.com/blogs/tewald/archive/2004/06/29/460.aspx),
Tim Ewald talks about using Doc/Literal/Bare for your web service. There
are several benefits he ticks off, but one seems to be the aesthetic
effect of cleaning up the format of the XML within your SOAP message. In
SOAP, the XML sent back and forth is just the wire format. As a typical
developer, why should you care what the wire format is? In general, you
shouldn't. If you have the tools to generate WSDL and generate a proxy
off of a WSDL to call a web service, you're all set.

Unfortunately for me, it's not that easy. My job right now is to expose
my company's platform to clients running cell-phones, set-top boxes,
etc... These platforms are running J2ME, BREW, C, etc... Potential
future clients are interested in SOAP, but our first client is dead set
against it because they say it's too verbose for their tiny devices and
there is scant tool support for them.

So I went and took some sand-paper to our SOAP services and shaved off
every bit I could, smoothing out the edges, shortening the namespaces I
have control over, making everything so "Doc/Literal/Bare" you'd blush
just looking at it. Still, no go. They weren't having it. They have
their own proprietary XML format they want to send to us over HTTP with
a roll-our-own authentication scheme. I was hoping to take advantage of
all the plumbing VS.NET and the .NET Web Services provide.

I recently watched a video in which [Don Box](http://www.gotdotnet.com/team/dbox/) and [Doug Purdy](http://www.douglasp.com/default.aspx) discuss Indigo and SOA.
They hope that most developers will not have to become plumbers and
understand how it all works under the hood. You just use Indigo and it
automagically takes care of it for you. You just focus on your business
rules.

The problem I see arising is that as Microsoft takes web services and
SOA to the next level, not everybody is keeping up. How will I get these
people on mobile devices to interoperate with my service if they are
lacking the tools to even generate simple SOAP messages? These guys
didn't want to use XML until I showed them their format required very
little change to make it XML compliant. As much as I don't want to know
what's going on under the hood, I'm afraid I am forced to hike my pants
down a bit and expose some butt crack to become a plumber.

In my next post, I'll talk about my solution to this problem and a
problem I ran into.
