---
title: Ready To Fire My QA Department
tags: [testing]
redirect_from: "/archive/2006/03/13/ReadyToFireMyQADepartment.aspx/"
---

![Fire](https://haacked.com/assets/images/FireBurn.jpg) I have had it up to here
(imagine my hand on my head) and I am ready to fire the damn QA
department for my blog. Unfortunately I *am* the QA department, so
things will be a little awkward around here till I find a replacement
willing to take zero pay with no benefits.

I read [this recent
post](http://www.thejoyofcode.com/Is_a_script_tag_slowing_down_your_web_page.aspx "Deferring Script Tags")
on [The Joy of Code](http://www.thejoyofcode.com/ "The Joy Of Code")
that describes how to defer the execution of a javascript that is
slowing down the rendering of your site.

So I go ahead and try it out, test in Firefox, and deploy to my hosting
provider. The next day, I notice that hits to my site are *way* down.
Strange. Everybody must be taking a break from blogs.

It wasn’t till today that a kind soul sent me an email saying my blog
acts funny in IE.

Funny? I’ll say. Visitors using IE would get a scripting error and then
see my Flickr Badge. Only my Flickr badge.

I have gotten so comfortable with Firefox that I completely forgot to
test in IE, which the majority of web users are still using. It is fun
to be on the bleeding edge, but never forget your audience. In the
meanwhile, I am going to try and patch things up with my QA person
because there are no resumes coming in.

UPDATE: As far as I can tell, the reason IE broke and not FireFox is
that IE seems to actually honor the “defer” attribute while Firefox
ignored it. This was not a bug in IE, but a bug in my head in applying
the defer to scripts that should not have defer applied. And the Joy of
Code article warned me too.

