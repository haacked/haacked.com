---
title: 'TIP: Url Encoding (or Hex encoding)'
date: 2004-02-05 -0800
disqus_identifier: 161
tags: []
redirect_from: "/archive/2004/02/04/tip-url-encoding-or-hex-encoding.aspx/"
---

Quick, what's the hex code for question mark? How about the ampersand?
Since, like me, you probably don't waste valuable space in your brain
with that type of information, I have a little trick for you to quickly
look up the hex encoding (or URLEncoding) for a character that doesn't
require you building an ASP or ASP.NET page and calling
Server.UrlEncode().

Go to [Google](http://www.google.com "Google") and type the character in
the search box and then click "Google Search". Now look in your address
bar at the very end. Everything after the "q=" is the encoding of your
character. For example, if I search on "?" I get:

http://www.google.com/search?hl=en&ie=UTF-8&oe=UTF-8&**q=%3F**

Thus the hex encoding for ? is %3F.

