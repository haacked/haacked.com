---
title: More On Medium Trust and Trackbacks
date: 2006-07-09 -0800 9:00 AM
tags: [aspnet,security]
redirect_from: "/archive/2006/07/08/MoreOnMediumTrustAndTrackbacks.aspx/"
---

In [my last
post](https://haacked.com/archive/2006/07/09/ConfiguringLog4NetWithASP.NET2.0InMediumTrust.aspx "Medium Trust and Log4Net"),
one of the restrictions listed when running in medium trust is that HTTP
access is only allowed to the same domain. It is possible in web.config
to add a single domain via the `originUrl` attribute of the `<trust>`
element as [described by
Cathal](http://developers.ie/blogs/cconnolly/archive/2005/07/01/1498.aspx "Supporting Web Services").

To add more than one domain requires editing machine.config or creating
a [custom trust
policy](http://west-wind.com/weblog/posts/6344.aspx "ASP.NET in Medium Trust")
which will not be accessible to many users in a hosted environment. This
may pose a big problem for those who care about trackbacks since even if
you could modify machine.config, there is no way to predetermine every
domain you will trackback.

One solution is to beg your hosting environment to relax the
`WebPermission` in medium trust. If trackbacks and pingbacks are
important to you, you shouldn’t be above begging. ;)

Another is for someone to create a passthrough trackback system in a
fully trusted environment. Essentially this would act on behalf of the
medium trusted trackback creator and forward a trackback to the final
destination. It would require blogging engines affected by medium trust
to trust this single domain. Of course the potential for abuse is high
and the rewards are low (unless people out there absolutely love
trackbacks).

