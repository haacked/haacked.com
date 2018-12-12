---
title: Community Server Fix for Posting Comments from Feed Readers and a Rant for
  Good Measure
date: 2005-04-18 -0800
disqus_identifier: 2730
categories: []
redirect_from: "/archive/2005/04/17/community-server-fix-for-posting-comments-from-feed-readers-and-a-rant-for-good-measure.aspx/"
---

[Jayson Knight](http://jaysonknight.com/blog/) has been cutting his
teeth on the latest version of .TEXT now called [Community
Server](http://communityserver.org/) trying to get it's RSS and
[CommentAPI](http://wellformedweb.org/story/9 "CommentAPI") support to
work.

Thankfully he posts [his
fix](http://jaysonknight.com/blog/archive/2005/04/19/1370.aspx) here for
others to use.

I'm pretty sure he'd agree with me that given the chance, he wouldn't
have upgraded to Community Server so soon after its release. To be fair,
it's an ambitious release intending to integrate Forums, Blogs, and
Photo Galleries all in one package. But for me personally, and I'd guess
for a lot of .TEXT users, this wasn't a necessary feature. Especially
not at the cost of having existing features break.

It seems to me that the [Telligent](http://www.telligentsystems.com/)
guys have released this puppy a bit too early. It's really bothersome
how much the RSS and commenting features regressed in this release. It's
not a case of simply introducing a bug that accidentally disables a
feature, but instead appears to be a case of dropping the code for
existing functionality. Who, besides the ever patient beta community,
tested this?

Installation is still a tricky beast with this software as I've had
difficulty getting it up on my development machine. Understand that I
was able to get .TEXT installed and CS is supposed to have simplified
the installation process. As many of you know, installing .TEXT is a
trip to the dentist without N2O.

The reason I had chosen .TEXT in the first place was for its SQL Server
support, and its the primary reason I'll stick with it as running ad-hoc
reports against my blog is quite convenient. I've considered
[DasBlog](http://www.dasblog.com/), but probably won't switch until
someone (or myself) writes a SQL provider.

One thing I like about DasBlog is that it's focused on being a good blog
engine. I think its that focus that will keep it very tight and without
the annoyances of CS. So far, CS is a big disappointment.

