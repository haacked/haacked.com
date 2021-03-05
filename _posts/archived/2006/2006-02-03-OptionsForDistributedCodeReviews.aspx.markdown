---
title: Options For Distributed Code Reviews
tags: [code-review,methodologies]
redirect_from: "/archive/2006/02/02/OptionsForDistributedCodeReviews.aspx/"
---

![Screen Showing Code](https://haacked.com/assets/images/CodeReview.jpg) The
great thing about being involved in a couple of open source projects is
that they provide great opportunities to learn and to teach, especially
through the vehicle of code reviews.

To enable this within the [Subtext
Project](http://subtextproject.com/ "Subtext Project Website"), I
recently [set up commit
emails](/archive/2006/01/17/SettingUpCVSCommitEmailsInSourceForge.aspx "setting up commit emails in CVS")
within the CVS repository. This makes it possible for more people to
review changes to the code. However, this brings up a bit of a quandary.
How should we go about conducting the reviews? We don’t have any policy
in place.

Ideally I would love to have regularly scheduled code reviews in which a
small group of developers gather around the screen and review the code
at the same time. But as several of our team are in Europe, that is just
not feasible. At least not so soon after the code is checked in. I think
it would be helpful to have more immediate feedback.

There are two ways to handle this that I will put forth in the hopes of
soliciting feedback.

Make Suggestions In Comments
----------------------------

One approach proposed by [Steve
Harman](http://stevenharman.net/blog/ "Steve Harman's Blog") is an
approach he uses at work. When reviewing code and finding something that
might need to be changed, the reviewer puts a comment like so:

` //REVIEW: haacked - Ummm... Hey, this violates an FxCop rule.  Perhaps you should do FOO instead.`{.example}

What I like about this approach is that it is deferential. You can shoot
off an email to the person who wrote the code and say something along
the lines of ...

> Hey, the code you checked in looks good. However, I noticed a few
> things you might want to look at. I put the appropriate comments in
> the code.

This approach allows the original author the chance to learn from the
reviewer comments as well as to defend or elaborate upon the code if he
or she feels that the code is indeed correct. Should the original author
not have time to change it, the reviewer can return to change it.

Change the Code and Notify
--------------------------

One drawback to the previous approach is that it takes two people to fix
code. One thing you’ll find when participating in an open source project
is that people are REALLY BUSY. Sometimes a contributor will contribute
a piece of code, and then get slammed at work and home, only to return
in three months time to contribute again. That’s a long time to wait for
code to get corrected.

An alternate approach is one borrowed from certain agile methodologies.
Practices such as Extreme Programming promote the concept of “Collective
Ownership”. This is an important principle to apply to open source
projects, as the code belongs to everyone. Likewise, everyone should
feel free to contribute to any piece of code in a project, assuming they
use good judgement and understand the code.

In this approach, one would just fix the code and in the source control
checkin comments, explain why the change was made. Additionally the
reviewer could send the author a quick email mentioning that he made
some changes and to keep an eye out on the commit emails.

The benefit of this approach is that the code gets improved immediately,
while still preserving the learning opportunity for the original author.
This approach also addresses the fact that any code you change in
codebase most like at one point or another was written by someone else.
Sometimes a reviewer might be looking at code long after it was
committed. The reasonable expectation here is that the reviewer should
feel free to modify the code, rather than spending time figuring out who
wrote it and making the review comments.

### Something In Between?

Perhaps there is an even better option that sits somewhere between these
two options. What do you think? I would love to hear from you in my
comments.

