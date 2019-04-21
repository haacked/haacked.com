---
title: Microsoft Listens and Don Box Responds
tags: [microsoft]
redirect_from: "/archive/2004/07/23/microsoft-listens-and-don-box-responds.aspx/"
---

I have to hand it to Microsoft, they really do listen to their
customers. And I don't mean in that head nodding "I hear you but I don't
know what you're saying" kind of way so common with people who really
want you to think they're listening, but have no time for you. I posted
[my question about device support](https://haacked.com/archive/2004/07/23/why-should-i-care-about-the-wire-format-in-soap.aspx/) on [Don Box's Wiki](http://pluralsight.com/wiki/default.aspx/Don.HomePage) and here's
his response:

> There are several small XML parsers (expat being the most well-known)
> that should make it exceedingly straightforward to implement basic
> SOAP functionality on your device (you will have to write some code,
> as the tool support is zip when using this approach). The challenge
> going forward is getting XML DSIG and Encryption, both of which
> require a real investment if your platform doesn't include support for
> them. How long it takes Sun to bring these technologies to J2ME is out
> of our control. If it's any consolation, we don't have them on .NET
> Compact Framework yet either. For the near-to-mid-term future, if you
> want reach, transport-level security is your best option for getting
> messages out of the device.

Now, I know this may be out of Don's control, but I think its pertinent
and I might as well ask. The way I see it, a huge source of consumers
for SOA will be mobile devices running on other platforms connecting to
these services. Although J2ME (and BREW etc...) is out of Microsoft's
control, now that Sun and Microsoft are good buddies, can we expect to
see some more collaboration and perhaps even a bit of friendly pressure
on Sun to provide toolkit support for WSE 2.0 now and Indigo near the
time Indigo is released? At the very least Microsoft should be (and I
imagine are) concurrently building Indigo support into the .NET Compact
Framework. Out of curiosity, is that the case? Microsoft says they've
run out of things to buy, but perhaps they should buy or start some
spinoff companies to build toolkits for platforms that do not have
decent support for Web Services. What better way to complete the
chicken-egg problem than to make both?

