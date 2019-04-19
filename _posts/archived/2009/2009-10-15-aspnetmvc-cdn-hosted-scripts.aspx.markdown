---
title: ASP.NET MVC 1.0 Scripts Available on Microsoft CDN
date: 2009-10-15 -0800 9:00 AM
tags: [aspnetmvc]
redirect_from: "/archive/2009/10/14/aspnetmvc-cdn-hosted-scripts.aspx/"
---

A little while ago, Scott Guthrie announced [the launch of the Microsoft
Ajax
CDN](http://weblogs.asp.net/scottgu/archive/2009/09/15/announcing-the-microsoft-ajax-cdn.aspx "Microsoft Ajax CDN").
In his post he talked about how ASP.NET 4 will have support for the CDN
as well as the list of scripts that are included.

The good news today is due to the hard work of [Stephen Walther and the
ASP.NET Ajax
team](http://stephenwalther.com/blog/archive/2009/09/16/microsoft-ajax-cdn-and-the-jquery-validation-library.aspx "Stephen Walther"),
they’ve added a couple of new scripts to the CDN which are near and dear
to my heart, the ASP.NET MVC 1.0 scripts. The following code snippet
shows how you can start using them today.

```html
<script src="http://ajax.microsoft.com/ajax/3.5/MicrosoftAjax.js"
></script>
<script src="http://ajax.microsoft.com/ajax/mvc/MicrosoftMvcAjax.js"
></script>
```

Debug versions are also available on the CDN.

```html
<script src="http://ajax.microsoft.com/ajax/3.5/MicrosoftAjax.debug.js"
></script>
<script src="http://ajax.microsoft.com/ajax/mvc/MicrosoftMvcAjax.debug.js"
></script>
```

As ScottGu wrote,

> The Microsoft AJAX CDN makes it really easy to add the jQuery and
> ASP.NET AJAX script libraries to your web sites, and have them be
> automatically served from one of our thousands of geo-located
> edge-cache servers around the world.

We currently don’t have the ASP.NET MVC 2 scripts available on the CDN,
but that’s something we can consider as we get closer and closer to RTM.

