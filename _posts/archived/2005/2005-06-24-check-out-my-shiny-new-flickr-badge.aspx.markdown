---
title: Check Out My Shiny New Flickr Badge
tags: [meta]
redirect_from: "/archive/2005/06/23/check-out-my-shiny-new-flickr-badge.aspx/"
---

> You know what, I do want to express myself, okay. And I don't need 37
> pieces of flair to do it.

![Sand Sharks](https://haacked.com/assets/images/SandSharkThum.jpg)No. I need a
[Flickr Badge](http://www.flickr.com/badge_new.gne)! For those of you
reading this from an aggregator, you’ll need to take a step out of the
comfort of the aggregator into the scary world of “web browsing” and
view my [page from a browser](https://haacked.com/) to see the
[flickr](http://flickr.com/) badge on the right.

I know, I’ve [gushed about
Flickr](https://haacked.com/archive/2005/06/01/3962.aspx) in the past,
but I like how easy they make it to integrate into your own website.
They also allow you to put up a a nifty Flash version of a badge, but I
settled for the simple HTML badge.

Now if you’re concerned about such things as XHTML validating markup,
you really need to quit being so anal and get a life. Having said that,
let me show you some small tweaks you’ll need to make so that your
flickr badge validates as XHTML 1.0 Transitional (I haven’t tried strict
yet). Us anal types can’ go around with invalid markup, now can we?

When you create your badge, Flickr will give you some code to inject
into your web page. The first step is to take the `style` element and
either move it inside the `head` element of your page or scrape its guts
and slap them inside a css file.

After that, you’ll notice that there’s a `script` tag just before a `tr`
tag. That tag essentially is a javascript repeater that repeats the
following tr block. If you move it, you break the badge. Unfortunately,
it doesn’t validate as is so I haacked it (get it? “haacked it” As in
hack? Oh Never mind) by placing the script within a `caption` element.

Finally, I went through and replaced all the ampersands within the URLs
with `&amp;`. After that, voila! My homepage is back to being valid
XHTML 1.0. Do I get a trophy or win prizes for this work? No. But I do
get to sport this nifty piece of geek flair. [![Valid XHTML
1.0!](http://www.w3.org/Icons/valid-xhtml10)](http://validator.w3.org/check?uri=referer)

