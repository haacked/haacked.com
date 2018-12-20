---
title: XHTML Conformance in ASP.NET 2.0
date: 2006-01-19 -0800
tags: []
redirect_from: "/archive/2006/01/18/XHTMLConformanceinASP.NET2.0.aspx/"
---

The key purpose of [my last
post](https://haacked.com/archive/2006/01/18/UsingaDecoratortoHookIntoAWebControlsRenderingforBetterXHTMLCompliance.aspx "Using a Decorator for Web Controls")
was to demonstrate how the ASP.NET web controls follow the Decorator
pattern when it comes to rendering and how developers can hook into that
to customize the rendered HTML.

The example I demonstrated made a `Button` control render XHTML
conformant markup. My article applies to ASP.NET 1.1. However, [one
commenter](http://klevo.aspweb.cz/) pointed out an even easier approach
if you are working with ASP.NET 2.0. You can simply set the
[`xhtmlConformance`
elment](http://msdn2.microsoft.com/en-us/library/ms228268.aspx) in
Web.config. For example:

> ` <xhtmlConformance mode="Transitional"/>`

Well, I am sure you’ll find other uses for the decorator technique I
wrote about.

