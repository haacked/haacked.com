---
layout: post
title: Log4Net Patch For ConnectionStrings
date: 2006-08-07 -0800
comments: true
disqus_identifier: 14756
categories: []
redirect_from: "/archive/2006/08/06/Log4NetPatchForConnectionStrings.aspx/"
---

I submitted [my first
patch](https://issues.apache.org/jira/browse/LOG4NET-88 "Patch 88") to
the [Log4Net](http://logging.apache.org/log4net/ "Log4Net logging")
project today. The patch enables the AdoNetAppender to get its
connection string from the `ConnectionStrings` section of a web.config
file. Hopefully it gets accepted and applied.

