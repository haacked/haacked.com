---
layout: post
title: "Enabling IPv6 For Your .NET Application"
date: 2004-09-23 -0800
comments: true
disqus_identifier: 1265
categories: []
---
RSS Bandit Developer [Torsten](http://www.rendelmann.info/blog/) points
out a very long and arduous process for enabling IPv6 in your .NET
application. Here is the extremely tedious set of instructions.

> A while ago one user complains about RSS Bandit was not running with
> installed/configured IPv6 on a windows machine. Today I got the answer
> (by mail from Frank Fischer, Microsoft Germany - thanks again!):
> open **machine.config** located at
> C:\\Windows\\Microsoft.NET\\Framework\\v1.1.xxxx\\CONFIG and change
> the XML tag:
>
> `<!-- <ipv6 enabled="false" /> -->`
>
> to:
>
> `<ipv6 enabled="true" />`
>
> This change allows the framework to parse and resolve IPv6 addresses.
> So any .NET application will be IPv6 enabled, not only RSS Bandit (but
> also ;-)
>
> ![](http://www.rendelmann.info/blog/aggbug.ashx?id=d4155df5-ab83-42b6-8abd-321e4ccf3324)

*[Via [torsten's .NET
blog](http://www.rendelmann.info/blog/PermaLink.aspx?guid=d4155df5-ab83-42b6-8abd-321e4ccf3324)]*

Gee, that seems like a lot of work to me. I'm going to reward myself
with a beer. Thanks Torsten!

