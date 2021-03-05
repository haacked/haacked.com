---
title: What Feature Should Be Removed?
tags: [product-management]
redirect_from: "/archive/2006/10/26/What_Feature_Should_Be_Removed.aspx/"
---

In the essay entitled [Hold the
Mayo](http://gettingreal.37signals.com/ch05_Hold_the_Mayo.php#footer "Hold the Mayo"),
37signals points out the obvious fact that most surveys ask users what
features they want added to a product.  They rarely ask what features
they want removed.

I have in the past [asked users for permission to remove
features](https://haacked.com/archive/2006/09/16/A_Few_Questions_For_Subtext_Users.aspx "A Few Questions"),
but I've never taken the extra step of asking users, which features
would they like removed.  So here I go. 

**Which feature(s) would you like to see *removed* from Subtext?**

I think a natural response I will receive is the question, *Why would
you ever want to remove a feature?*

Features have many [hidden
costs](http://gettingreal.37signals.com/ch05_Hidden_Costs.php "Hidden Costs"). 
Even relatively simple features.  I am going to tell a short story about
one such occurrence that happened in Subtext.  I won't name names and
this is not meant to call anyone out for public embarrasment or
chastisement.  I have probably been more guilty of this than anyone.

Not too long ago, someone checked in a control into Subtext that
displays recent comments, just as we were close to preparing a release. 
I was a little miffed at the time because I was not expecting anyone to
add features at the time.

However this was a pet feature and some of the devs really wanted it in
the system.  Besides, it's such a small feature, what could go wrong? 
So I let it in not wanting to be a hard-ass about it.

The recent comments control is a pretty simple control in concept.  When
added to the main skin template, it simply displays recent comments left
on various blog posts.  Sounds simple, no?

However, as with any feature, the devil is in the details.  Several
problems immediately became apparent as we tested the release, and one
problem affected me in a later release.

1.  The control let you truncate comments after a certain number of
    characters.  However it didn't strip HTML out, so if it truncated a
    comment in the middle of a tag, it would completely mess up the
    whole page.
2.  The control bypassed our provider model and made stored proc calls
    directly.
3.  The control displayed all feedback for a blog, even ones that were
    left via the Contact Page, thus potentially displaying private
    messages.
4.  Recently, I tried to append a little HTML comment after messages
    that had been processed by Akismet to make it easy to see that
    Akismet was indeed working by viewing source.  Comments aren't being
    stripped by the Recent Comments control, so the site was broken in a
    way that I had not anticipated. It took a long time before I
    realized what was happening.

So for a relatively small feature, a lot of development time and effort
was used up in supporting it.  I am glad we have the feature now, I
really like it and plan to add it to my own blog at some point.

But the main point still stands, every feature is like an iceberg. When
scoping it out in your head, you typically only think of the top part
that sticks out above the water.  However, the real effort is in the
part under water that supports the whole thing.

[![Iceberg Photo from
http://shiftingbaselines.org/blog/archives/2005\_02.html](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WhatFeatureShouldBeRemoved_9E30/BigIceberg_thumb%5B1%5D.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WhatFeatureShouldBeRemoved_9E30/BigIceberg%5B5%5D.jpg)

So if there is a feature in your product that provides very little bang
for the support buck, consider getting rid of it.

So again, **Which feature(s) would you like to see *removed* from
Subtext?**

