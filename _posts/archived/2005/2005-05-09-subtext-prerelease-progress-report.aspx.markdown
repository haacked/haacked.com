---
title: Subtext Progress Report
tags: [subtext]
redirect_from: "/archive/2005/05/08/subtext-prerelease-progress-report.aspx/"
---

![Subtext Logo](/assets/images/header_logo.gif) For the past several days, I’ve
been consumed with working on the [Subtext blogging
engine](https://haacked.com/archive/2005/05/04/2953.aspx) (not to be
confused with the [Subtext programming
language](http://c2.com/cgi/wiki?SubtextLanguage)). It's been the most
fun I've had writing software since, well, since working on [RSS
Bandit](http://www.rssbandit.org/) as a matter of fact. ;)

Speaking of RSS Bandit, [Dare offers some good
advice](http://www.25hoursaday.com/weblog/PermaLink.aspx?guid=766396c5-0c27-46a6-b029-5aa369605e32)
for those starting an Open Source project. I'm going to have to pick his
(and others) brain some more and maybe write a short article with advice
on starting and continuing an open source project. Especially since I've
already violated one piece of his advice, which is to save the
announcement till you have a release, in order to generate more
excitement.

Despite not having a release yet, I have seen some excitement in the
community over this project and I appreciate all the well wishers. If
you're interested in taking a look, you can get the latest source code
at any time, but you'll have to use CVS until we get a release prepared.
I was working furiously to get an installer package ready, but upon the
advice of the team, I put that aside so we could focus on having a more
compelling release first. That also bought me some time and breathing
room as I was completely stuck on a problem using WiX.

So far, some of the interesting features I've implemented are...

-   Friendly and informative error pages for missing blog\_config
    records and malformed (or just wrong) connection string.
-   Skins can now add an edit link visible only to the admin user to the
    ViewPost page. So when viewing a page as an admin, you can click the
    edit link to go directly to the post editor. Sometimes I like to
    edit an older post and hated having to page through so many records.
    Instead, I can use Google to find the post and then click the edit
    link.
-   Fixed the MetaBlogAPI. I hated the fact I couldn't edit old posts
    with [w.Bloggar](http://www.wbloggar.com/). Now I can.
-   Syndication compression (for aggregators that support it) using
    contributed code.
-   Applied a contributed patch to add "image", "license", and
    "copyright" elements to the RSS feed for those that want it.
-   Comments can be turned off after a configurable number of days.
-   ... and more!

Already it's at a point that I can't wait to deploy it to my own blog.
But I'm going to hold off till we can implement a few more features and
get the installation package together. That's the biggest technical
challenge right now and I welcome any offers of help on that.

My pace on this project will slow by necessity as I get my consulting
projects moving forward. But I have to admit, I'm having so much fun on
this I often catch myself daydreaming about finding a wealthy patron to
sponsor me to work on open source projects. But a foot set firmly in
reality snaps me out of that stupor and back to writing code.

[Listening to: Plantastic - Artifacts - Lee Burridge - Nubreed 005 CD2
(6:01)]

