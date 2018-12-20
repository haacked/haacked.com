---
title: My Mental Laziness Mistakes
date: 2005-09-18 -0800
disqus_identifier: 10226
tags: []
redirect_from: "/archive/2005/09/17/my-mental-laziness-mistakes.aspx/"
---

It wouldn’t be fair to point out the [mistakes of other developers being
lazy](https://haacked.com/archive/2005/09/18/10204.aspx) without pointing
out that I have been very guilty of this myself. The point of the post
is not to trash another person’s coding habits, but to present an ideal
to work towards. Sometimes, intellectual “laziness” is absolutely
necessary as in the example presented in the [comments of that
post](https://haacked.com/archive/2005/09/18/10204.aspx#10223).

When I started off as an ASP developer (remember VBScript?) I needed to
store name value pairs within a cookie. So I started off storing a
string like so in the cookie.

> ` Response.Cookies("ChocolateChip") = "name1=value1,name2=value2,..."`

But I ran into an issue that some of the values contained commas, so I
chose a delimiter I was sure would never be in the content...

> ` Response.Cookies("ChocolateChip") = "name1=value1*&*name2=value2*&*..."`

And proceeded to write a butt load of string parsing code to insert and
extract values from the string, making sure not to insert duplicate
names, etc...

Of course later, I got around to reading more about Cookies in ASP.NET
and I discovered that you can create cookies with keys. So the ugly code
above became...

> ` Response.Cookies("ChocolateChip")("name1") = "value1" Response.Cookies("ChocolateChip")("name2") = "value2" '...`

Had I spent a few extra minutes up front reading about cookies rather
than **programming by intellisense**, I would have saved myself a lot of
time. In the end I ripped out my code and used the built in mechanism.

\<blatantLie\>To my defense, I was only five at the time and I had been
hit by a bat earlier that day so I was seeing double.\</blatantLie\>

