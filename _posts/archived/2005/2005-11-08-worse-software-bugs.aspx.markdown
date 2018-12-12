---
title: Worst Software Bugs in History
date: 2005-11-08 -0800
disqus_identifier: 11152
categories:
- personal
- code
redirect_from: "/archive/2005/11/07/worse-software-bugs.aspx/"
---

Wired News has a very interesting article on [History’s worst software
flaws](http://wired.com/news/technology/bugs/0,2924,69355-2,00.html?tw=wn_story_page_next1).

It makes me think of my worst software bug when I first started off as
an ASP developer right out of college. I was working on a large music
community website and was told to implement a “Forgot Password” feature.
Sounds easy enough. I coded it, ran a quick test, and then deployed it
(that alone should rankle your feathers).

We didn’t quite have a formal deployment process at the time. A few days
later, we find out that the code never sent out any emails, and never
logged who made the requests, leaving us no way to really know how many
users were affected.

I believe we found out (and my memory is hazy here), through a relative
of our client’s president. After reviewing the code, there was no way it
could have sent out emails. There was a glaring bug in there, which
makes me wonder how it passed my test.

In any case, I coded on egg shells for a while after that, fearing I
might lose my first coding job.

