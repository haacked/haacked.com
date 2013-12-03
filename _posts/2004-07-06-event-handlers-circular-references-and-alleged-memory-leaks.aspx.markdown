---
layout: post
title: "Event Handlers, Circular References, and Alleged Memory Leaks"
date: 2004-07-06 -0800
comments: true
disqus_identifier: 781
categories: []
---
[Ian Griffiths](http://www.interact-sw.co.uk/) wrote this informative
[follow-up](http://www.interact-sw.co.uk/iangblog/2004/07/07/circulareventrefs)
to my question on delegate references. The .NET framework definitely
does not use Weak References to implement delegates.

