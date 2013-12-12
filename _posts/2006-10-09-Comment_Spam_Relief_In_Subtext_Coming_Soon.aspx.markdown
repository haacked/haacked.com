---
layout: post
title: "Comment Spam Relief In Subtext Coming Soon"
date: 2006-10-08 -0800
comments: true
disqus_identifier: 17913
categories: []
---
Personal matters (good stuff) and work has been keeping me really busy
lately, but every free moment I get I plod along, coding a bit here and
there, getting Subtext 1.9.1 “Shields Up” ready for action.

There were a couple of innovations I wanted to include in this version
as well as a TimeZone handling fix, but recent comment spam shit storms
have created a sense of urgency to get what I have done out the door
ASAP.

In retrospect, as soon as I finished the
[Akismet](http://akismet.com/ "Akismet") support, I should have
released.

I have a working build that I am going to test on my own site tonight. 
If it works out fine, I will deploy a beta to SourceForge.  This will be
the first Subtext release that we label *Beta*.  I think it will be just
as stable as any other release, but there's a significant schema change
involved and I want to test it more before I announce a full release.

Please note, **there is a significant schema change** in which data gets
moved around, so backup your database and all applicable warnings
apply.  Upgrade at your own risk.  I am going to copy my database over
and upgrade offline to test it out before deploying.

Shields up edition will contain Akismet support and CAPTCHA.  The
Akismet support required adding comment “folders” to allow the user to
report false positives and false negatives.



