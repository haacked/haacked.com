---
title: Blocking Outgoing Messages
tags: [tools]
redirect_from: "/archive/2004/08/05/blocking-outgoing-messages.aspx/"
---

[Dave Winer](http://archive.scripting.com/2004/08/06#When:6:31:31PM)
writes:

> There's something missing from Windows. An application that hooks into
> the outbound Internet message flow, and shows me where messages are
> going. This would allow me to figure out what spyware is running on my
> system even if the various utilities can't get rid of them. Then the
> next step would be to allow me to block traffic to certain servers.
> That would disable the spyware. It seems that I should have control of
> my machine at that level.

And Jeff Sandquist
[responds](http://www.jeffsandquist.com/PermaLink,guid,57cb97a1-f5ae-4cd6-8136-d7492fa2e79e.aspx)
that Windows XP SP2 can do this via the new firewall, but look again.
The firewall built into XP blocks **incoming** traffic, not outgoing. My
guess is that feature will come with SP3. For now, I recommend the free
version of
[ZoneAlarm](http://www.zonelabs.com/store/content/catalog/products/sku_list_za.jsp?lid=nav_za)
which is perennially considered by many to be the best desktop firewall
software out there. It will notify you when an application tries to send
an outgoing message to the internet and its quite easy to configure.

