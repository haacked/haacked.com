---
title: Easy To Deploy Identicon Handler
date: 2007-01-22 -0800
disqus_identifier: 18197
categories:
- code
redirect_from: "/archive/2007/01/21/Easy_To_Deploy_Identicon_Handler.aspx/"
---

Yesterday [I
mentioned](https://haacked.com/archive/2007/01/22/Identicons_as_Visual_Fingerprints.aspx "Identicons as Visual Fingerprints")
that [Jeff
Atwood](http://codinghorror.com/blog/ "CodingHorror, Jeff Atwood's Blog")
and [Jon
Galloway](http://weblogs.asp.net/jgalloway/ "Jon Galloway's Blog") wrote
an [Identicon implementation for
.NET](http://www.codinghorror.com/blog/archives/000774.html "Identicon for .NET").
It works beautifully, but they distributed the code as a Web Site
Project, which I cannot deploy to my blog as is.

For those of us who prefer Web Application projects, I repackaged their
code as compiled DLL and a handler file. This distribution will work for
both Web Application Projects as well as Website Projects.

Just download the binaries, copy the DLL to your bin directory, copy the
IdenticonHandler.ashx file to your website directory, and you are good
to go. You can simply add an image tag that references your identicon
handler.

`<img src="IdenticonHandler.ashx?hash=hash-of-ip-address" />`

[[Download
Binaries](https://haacked.com/code/IdenticonHandler.zip "Identicon Binaries")]

You can also grab the Solution I used to prepare the binaries.

[[Download
Source](https://haacked.com/code/IdenticonHandler_SOURCE.zip "Identicon Handler Source")]

**Gravatar Tip!**

If you use Gravatar, consider using the identicon handler as the default
image. That’s what I did for this website. That way, if the user does
not have a gravatar, the identicon will show up instead. Better than
nothing!

