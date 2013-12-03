---
layout: post
title: "The Feedburner to Google Domain Transition Foul Up"
date: 2008-12-10 -0800
comments: true
disqus_identifier: 18564
categories: [personal]
---
UPDATE: There’s a [workaround mentioned in the Google
Groups](http://groups.google.com/group/feedburner/web/known-issues-workarounds "Workaround for MyBrand issue").
It’s finally resolved.

Ever since I first [started using
FeedBurner](http://haacked.com/archive/2007/03/08/Burning_My_Feeds.aspx "Burning My Feeds"),
I was very happy with the service. It was exactly the type of service I
like, fire and forget and it just worked. My bandwidth usage went down
and I gained access to a lot of interesting stats about my feed.

When I was first considering it, others warned me about losing control
over my RSS feed. That led me to pay for the [MyBrand PRO
feature](http://www.feedburner.com/fb/a/publishers/mybrand;jsessionid=90DFA112C8BFE3762618061519CF877F.fb1 "MyBrand overview and FAQ")
which enabled me to burn my feed using my own domain name at
[http://feeds.haacked.com/haacked](http://feeds.haacked.com/haacked) by
simply creating a CNAME from *feeds.haacked.com* to
*feeds.feedburner.com*.

The idea was that if I ever wanted to reclaim my feed because something
happened to FeedBurner or because I simply wanted to change, I could
simply remove the CNAME and serve up my feed itself.

That was the idea at least.

This week, I learned the fatal flaw in that plan. When Google bought
FeedBurner, nothing really changed immediately, so I was completely fine
with it. But recently, they decided to change the domain over to a
google domain. I had assumed they would do a CNAME to their own domain
for MyBrand users. Instead, I saw that they were redirecting users to
their own domain, thus taking control away from me over my own feed and
completely nullifying the whole point of the MyBrand service.

That’s right. People who were subscribed via my feeds.haacked.com domain
were being redirected to a google domain.

Ok, perhaps I’m somewhat at fault for ignoring the email on November 25
announcing the transition, but I’m a busy guy and that didn’t leave me
much time to plan the transition.

**And even if I had, I would have still been screwed**. I followed the
instructions provided and set up a CNAME from *feeds.haacked.com* to
*haackedgmail.feedproxy.ghs.google.com* and watched the next day as my
subscriber count dropped from 11,000 to 827. I’ve since changed the
CNAME back to the feedburner.com domain (I tried several variations of
the CNAME beforehand) and am resigned to let the redirect happen for
now.

In the meanwhile, I’ve set up a CNAME from *feeds3.haacked.com* to the
one they gave me and it still doesn’t work. At the time I write this,
[http://feeds3.haacked.com/haacked](http://feeds3.haacked.com/haacked)
gives a 404 error.

I’m very disappointed in the haphazard manner they’ve made this
transition, but I’m willing to give a second chance if they’d only fix
the damn MyBrand service.

