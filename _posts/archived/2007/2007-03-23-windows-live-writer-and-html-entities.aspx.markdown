---
title: Windows Live Writer and Html Entities
date: 2007-03-23 -0800
disqus_identifier: 18260
tags: []
redirect_from: "/archive/2007/03/22/windows-live-writer-and-html-entities.aspx/"
---

I’ve been banging my head against a couple of problems with the
interaction between
[Subtext](http://subtextproject.com/ "Subtext Project Website") and
[Windows Live
Writer](http://windowslivewriter.spaces.live.com/ "Windows Live Writer")
that I thought I’d post on this here blog in the hopes that someone can
help.

I expect that [Mr.
Hanselman](http://hanselman.com/blog/ "Scott Hanselman") might know the
answer, but will only tell me after properly extolling
[DasBlog’s](http://www.dasblog.net/ "DasBlog") superiority over Subtext
first. Very well.

Here’s the first issue. I’m kind of a fan of typography and go through
the extra effort to use proper apostrophes and quotes.

For example. Instead of using ' for a quote, I will use ’. Instead of
"quotes", I will use “real quotes”. It’s just how I roll.

For the apostrophe, I use the HTML entity code &\#8217;. For quotes I
use the opening quotes &\#8220; followed by the closing quotes &\#8221;.

However, when you enter these things in WLW and post them to your blog,
it converts them to the actual characters. Thus when I query my
database, I see `“quotes”` instead of `&#8220;quotes&#8221;` as I would
expect.

I wish WLW would not screw around with these conversionsn, but until
then, I was thinking about doing a simple conversion on the server back
to the original entity encodings.

However, I can’t just call the HttpUtility.HtmlEncode method as that
would encode the angle brackets et all. I still want the HTML as HTML, I
just want the special characters to remain entity encoded.

Anyone have a clever method for doing this, or will I need to brute
force this sucker?

