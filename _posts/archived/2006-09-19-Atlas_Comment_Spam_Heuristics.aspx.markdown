---
layout: post
title: Atlas Comment Spam Heuristics
date: 2006-09-19 -0800
comments: true
disqus_identifier: 16843
categories: []
redirect_from: "/archive/2006/09/18/Atlas_Comment_Spam_Heuristics.aspx/"
---

Remember my [recent
post](https://haacked.com/archive/2006/08/29/Comment_Spam_Heuristics.aspx)
in which I suggested that we need more heuristic approaches to the
comment spam problem?

Check out this new **[NoBot
control](http://atlas.asp.net/atlastoolkit/NoBot/NoBot.aspx) in the
Atlas Control Toolkit.  I wonder if this came out before or after I
wrote my piece, because I don’t want y’all to think I cribbed my ideas
from this control.  It has a couple features that I mentioned.

> -   Forcing the client’s browser to perform a configurable JavaScript
>     calculation and verifying the result as part of the postback. (Ex:
>     the calculation may be a simple numeric one, or may also involve
>     the DOM for added assurance that a browser is involved)
> -   Enforcing a configurable delay between when a form is requested
>     and when it can be posted back. (Ex: a human is unlikely to
>     complete a form in less than two seconds)
> -   Enforcing a configurable limit to the number of acceptable
>     requests per IP address per unit of time. (Ex: a human is unlikely
>     to submit the same form more than five times in one minute)

I think that will be a nice minor addition to a comment spam fighter’s
toolkit. It’s **Invisible CAPTCHA**.  Very cool!

tags: [ASP.NET](http://technorati.com/tag/ASP.NET),
[Atlas](http://technorati.com/tag/Atlas), [Comment
Spam](http://technorati.com/tag/Comment+Spam)

