---
title: CoComment Support in Subtext
tags: [subtext]
redirect_from: "/archive/2006/04/03/CoCommentSupportInSubtext.aspx/"
---

![CoComment Logo](https://haacked.com/assets/images/cocommentlogo.gif) Since I
was [called
out](http://jaysonknight.com/blog/archive/2006/04/04/8001.aspx "CoComment Enable Your .TEXT Blog"),
I went ahead and quickly implemented
[CoComment](http://cocomment.com/ "Comment Tracking System") for
Subtext, but I
have yet to deploy it to my personal blog. It will be released as part
of our upcoming interim 1.0.5.0 release which is focused on bug fixes
and a few developer goodies thrown in.

I said before I wasn’t interested in supporting CoComment, hoping to see
a cleaner approach come along and surprise everyone. But it seems that
adoption of CoComment is going pretty well and I am not one to stand in
the way of progress. Besides, it really didn’t take long to implement at
all.

CoComment support in the latest build of Subtext is pretty automatic.
There is no need to update any skins. Simply go into the admin section
under the *Comment Settings* and click a checkbox to enable CoComments.
That’s it!

I wrote a base server control (in Subtext.Web.Controls if you are handy
with Subversion and want to get it from our source control repository)
for rendering out the CoComment script. This control lets you set the
various properties and renders out the appropriate CoComment script. I
then inherited from that class to implement a Subtext specific version.
That control gets rendered in the *head* section of the page to maintain
as much XHTML compliance as possible. I am seriously anal, aren’t I?

