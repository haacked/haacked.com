---
title: CAPTCHA For Trackbacks
date: 2006-10-31 -0800
disqus_identifier: 18124
categories: []
redirect_from: "/archive/2006/10/30/CAPTCHA_For_Trackbacks.aspx/"
---

[Jeff Atwood](http://codinghorror.com/ "CodingHorror") points [out
several
problems](http://www.codinghorror.com/blog/archives/000715.html#comments "Whitelist, blacklist, greylist")
with using blacklists (specifically
[Akismet](http://akismet.com/ "Akismet")) to prevent comment spam.  He
makes the following point:

> **The core problem is relying on a single method of defense against
> spam.**

Absolutely. 
[Subtext](http://subtextproject.com/ "Subtext Project Website") employs
several measures against comment spam, mostly of a
[heuristic](https://haacked.com/archive/2006/08/29/Comment_Spam_Heuristics.aspx "Comment Spam Heuristics")
nature.  The [latest
release](https://haacked.com/archive/2006/10/25/Subtext_1.9.2_quotShields_Upquot_Edition_Released.aspx "Subtext Shields Up Release")
adds Akismet support as well as Visible and [Invisible
CAPTCHA](https://haacked.com/archive/2006/09/26/Lightweight_Invisible_CAPTCHA_Validator_Control.aspx "Invisible CAPTCHA").

The funny thing about CAPTCHA and especially Invisible CAPTCHA is the
number of people who claim it won’t work and is broken. As [Jeff points
out](http://www.codinghorror.com/blog/archives/000712.html "CAPTCHA Effectiveness"),
this may be true among researchers, but it is not the case in the wild. 
However let me back up his post with some numbers.

For the past four days, I have not emptied my blog spam folder, just to
see what gets put in there.  So far, in that time, my blog has received
1441 comments, trackbacks, etc...  Of those, 1407 have been flagged as
spam by Akismet or marked as spam by me.  Of those, only one was a
comment.  The rest were trackbacks/pingbacks.

So as far as I am concerned, Invisible CAPTCHA is working well so far. 
And it has the benefit of being usable, assuming you can do simple math.

So assuming that CAPTCHA, for now, is the best approach to fighting
comment spam, we need to deal with its critical weakness.

**The real problem is how do we enable CAPTCHA for trackbacks?**

I wrote about [this problem
before](https://haacked.com/archive/2006/08/31/What_About_CAPTCHA.aspx "What about CAPTCHA") when
I discussed my qualms about CAPTCHA.

> The reason I didn’t mention CAPTCHA is that it would be ineffective
> for me.  Much of my spam comes in via automated means such as a
> [trackback](http://en.wikipedia.org/wiki/Trackback)/[pingback](http://en.wikipedia.org/wiki/Pingback)
> .  The whole point of a trackback is to allow another **computer** to
> post a comment on my site.  So telling a computer apart from a human
> in that situation is pointless.

I mentioned this to Atwood who pointed out that trackbacks and pingbacks
are indeed automated, but they are left on behalf of a user.  This is
true.  When I write a blog post, Subtext will look at all the links in
my post and attempt to trackback each one for me.

**Unfortunately, the trackback and pingback APIs have no facility
for dealing with CAPTCHA.** Unless there were a community effort to
revise these specs (I would be happy to join in), CAPTCHA for trackbacks
and pingbacks are not gonna happen.

Even with such a community effort, implementing CAPTCHA for trackbacks
is going to be a lot of work for blog implementers.  In part, this is
indicative of a usability issue with CAPTCHA based approaches. 
**CAPTCHA requires human intervention**.  This makes integrating CAPTCHA
with something like trackbacks hard work, whereas if someone comes up
with a better automated filter, integrating that is easy.

So for the time being, we have two choices.

1.  Abandon Trackbacks/Pingbacks
2.  Find better ways to filter trackbacks and pingbacks.

I know many have decided to simply abandon trackbacks.  I understand
this choice, but I personally am not ready to throw in the towel just
yet.  Trackbacks can and do add a lot of value to discussions that occur
via blogs.  So far, Akismet has allowed me to reclaim trackbacks.

**What is the next step?** Well I agree with Jeff:

> Akismet is a fine addition to our anti-spamming toolkit. But that
> doesn't mean it's a good idea to outsource your entire anti-spam
> effort to a single website, either. Anti-spam security starts at home.
> For best results, use [defense in
> depth](http://en.wikipedia.org/wiki/Defence_in_depth) and **combine
> local anti-spam measures, such as CAPTCHA, with Akismet as a backup.**

Though I think we need to start working on some better non-CAPTCHA
filters to combine with Akismet.

