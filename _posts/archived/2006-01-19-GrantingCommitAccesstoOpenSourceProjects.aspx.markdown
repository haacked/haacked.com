---
layout: post
title: Granting Commit Access to Open Source Projects
date: 2006-01-19 -0800
comments: true
disqus_identifier: 11559
categories:
- open source
redirect_from: "/archive/2006/01/18/GrantingCommitAccesstoOpenSourceProjects.aspx/"
---

Every open source project has its own procedures for granting the
all-important commit access to developers. Some require a set number of
submitted patches (which Fogel, author of [Producing Open Source
Software](https://haacked.com/archive/2006/01/16/RunningAnOpenSourceProject.aspx "Previous Blog Post About This Book"),
warns against). Others do not have any clear process and rely on the
whims of those with the ability to add other committers. Whichever
procedure your project chooses, it is important make sure make sure it
is clearly published in a visible location such as in the developer
guidelines.

Fogel discusses the methods by which the Subversion project grants
commit access, emphasis mine.

> In the Subversion project, we choose committers primarily on the
> Hippocratic Principle: *first, do no harm*. **Our main criterion is
> not technical skill or even knowledge of the code, but merely that the
> committer show good judgement.** Judgment can mean simply knowing what
> not to take on. A person might post only small patches, fixing fairly
> simple problems in the code; but if the patches apply cleanly, do not
> contain bugs, and are mostly in accord with the project’s log message
> and coding conventions, and there are enough patches to show a clear
> pattern, then an existing committer will usually propose that person
> for commit access. If at least three people say yes, and no one
> objects, then the offer is made. True, we might have no evidence that
> the person is able to solve complex problems in all areas of thecode
> baes, but that does not matter: the person has made it clear that he
> is capable of at lest judging his own abilities. Technical skills can
> be learned (and taught), but judgement, for the most part, cannot.
> Therefore, it is the the one thing you want to amke sure a person has
> before you give him commit access.

Obviously, one problem with this approach for very small projects is you
might not even have three committers. This merely reinforces the fact
that any policy should fit the size of the project and should be amended
as the project grows. But its emphasis on judgement as opposed to
technical skill is, in my opinion, a good one.

On any project, there will always be plenty of small tasks that do not
require Linus Torvalds to get it done. Rather they can be handled by Joe
Hobbyist and Jill N00b, as long as they exercise good judgement. And
having someone focused on these smaller easy tasks helps the overall
polish of the application, as the "serious" developers often do not
spend their time on them.

Another benefit of this approach is that it does not rely on an
artificial quota, but on recognition by other committers that someone
exhibits good judgement. Ostensibly, if the other committers were chosen
because of their good judgement, they can most likely be trusted to use
good judgment when recommending and voting on other committers.

I recommend reading the book to delve into the actual procedures for
handling the voting and making the offer.

