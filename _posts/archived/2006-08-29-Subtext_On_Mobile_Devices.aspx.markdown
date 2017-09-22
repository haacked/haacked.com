---
layout: post
title: Subtext On Mobile Devices
date: 2006-08-29 -0800
comments: true
disqus_identifier: 16169
categories: []
redirect_from: "/archive/2006/08/28/Subtext_On_Mobile_Devices.aspx/"
---

Scott writes about [making DasBlog work on Mobile
Devices](http://www.hanselman.com/blog/MakingDasBlogWorkOnMobileDevices.aspx "DasBlog"). 
The approach he takes is to programmatically detect that the device is a
mobile device and then present an optimized TinyHTML (*his term*) theme.

Ideally though, wouldn’t it be nice to have mobile versions of every
theme?  In fact, I thought this could be handled without any code at all
via CSS media types.

Unfortunately (or is that *fortunately*) I don’t own a BlackBerry or any
such mobile device with a web browser, so I can’t test this, but in
theory, another approach would be to declare a CSS file specifically for
mobile devices like so:

```csharp
<link rel="stylesheet" href="mobile.css" type="text/css" 
    media="handheld" />
```

The mobile browser *should* use this CSS to render its screen while a
regular browser would ignore this.  *Should* being the operative word
here.  Unfortunately, at least for Scott’s Blackberry, it doesn’t.  He
told me he *does* include a mobile stylesheet declaration and the
BlackBerry doesn’t pick it up.  Does anyone know which devices, if any,
do support this attribute?

For those devices that do, a skin in subtext can be made mobile ready by
specifing the *media* attribute in the `Style` element of `Skin.config`
like so (note this feature is available in Subtext 1.5).

```csharp
<Style href="mobile.css" media="handheld" />
```

Refer to my recent [overview of Subtext
skinning](https://haacked.com/archive/2006/08/26/Mile_High_Overview_Of_Subtext_Skinning.aspx) to
see the media attribute in play for printable views, which *does* seem
to work for IE and Firefox.

