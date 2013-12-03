---
layout: post
title: "Viewing the Raw Xml for RSS Bandit Newspaper Views Using an Identity Transformation"
date: 2004-12-28 -0800
comments: true
disqus_identifier: 1777
categories: []
---
Ok, the title is a mouthful, but it addresses a concern with the new
[alpha version of RSS
Bandit](http://www.25hoursaday.com/weblog/PermaLink.aspx?guid=a0007553-f42b-430c-beb5-39a82ebc7560).
As of the current build, the feature to view the raw XML before it is
rendered by the stylesheet is no longer there.

However, here is an [identity
transformation](http://haacked.com/xslt/identity.zip) you may use. Just
copy this to the **templates** sub folder of your RSS Bandit
installation. You can then go to the **Display** menu (via Tools |
Options) and select the Identity.fdxsl for your stylesheet. Afterwards,
view any feed and then view source to see the raw XML.

