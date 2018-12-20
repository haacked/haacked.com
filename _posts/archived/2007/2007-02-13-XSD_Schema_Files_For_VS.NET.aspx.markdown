---
title: XSD Schema Files For VS.NET
date: 2007-02-13 -0800
tags: []
redirect_from: "/archive/2007/02/12/XSD_Schema_Files_For_VS.NET.aspx/"
---

I’m posting this more as a reminder for myself. Where exactly are you
supposed to place XSD files to give you intellisense for XML files in
Visual Studio.NET?

**Visual Studio.NET 2003**

*C:\\Program Files\\Microsoft Visual Studio .NET
2003\\Common7\\Packages\\schemas\\xml*

**Visual Studio.NET 2005**

*C:\\Program Files\\Microsoft Visual Studio
8\\Common7\\Packages\\schemas\\xml*

This is useful for such things as
[Nant.xsd](http://nant.sourceforge.net/ "Nant") and
[UrlRewriting.xsd](http://www.urlrewriting.net/ "UrlRewriting Library").

To get intellisense for NAnt, you have a couple of [other steps to
do](http://nant.sourceforge.net/faq.html#enable-intellisense "Enable Intellisense in VS.NET").

