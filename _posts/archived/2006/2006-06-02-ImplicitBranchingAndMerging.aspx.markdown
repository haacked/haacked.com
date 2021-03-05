---
title: Implicit Branching and Merging
tags: [code]
redirect_from: "/archive/2006/06/01/ImplicitBranchingAndMerging.aspx/"
---

![branches](https://haacked.com/assets/images/branches.jpg) [Scott Allen writes
about](http://odetocode.com/Blogs/scott/archive/2006/06/01/3934.aspx "Branching and Merging Primer")
a [Branching and Merging primer
(doc)](http://blogs.msdn.com/chrisbirmele/attachment/611179.ashx "Word Doc")
written by [Chris
Birmele](http://blogs.msdn.com/chrisbirmele/ "Chris Birmele's Blog"). It
is a short but useful tool agnostic look at branching and merging in the
abstract. This is a nice complement to my favorite tutorial on source
control, Eric Sink’s [Source Control
HOWTO](http://www.ericsink.com/scm/source_control.html "Source Control Tutorial").

Another useful resource on branching strategies is Steve Harman’s guide
to [branching with
CVS](http://stevenharman.net/blog/archive/2006/05/26/989.aspx "Keeping your branch(es) in synch").

The primer takes a tool agnostic look at branching and points out
several branching models. One thing to keep in mind is that not every
model makes use of your source control tool’s *branching* feature. In
particular, let’s take a closer look at the *Branch-per-task* model.
This model is almost universally in use via what I call **implicit
branches**, which are private and not shared among other team members.

Using a pessimistic locking source control system like Visual Source
Safe (VSS), every time you check out a file (which grants you an
exclusive lock on that file), you are implicitly making a branch as soon
as you edit that file. However, this is not a *branch* that VSS
recognizes. It is merely a branch by fact that the code on your system
is not the same as the code in the repository. Also consider that other
team members may be making changes to other files in the same codebase.
Perhaps files that contain classes that the file you are working on are
dependent. So when you check that file back in, you are performing an
implicit merge.

This type of implicit branching pretty much maps to the primer’s
*Branch-per-Task* model of branching. Optimistic locking source control
systems such as CVS and Subversion make this implicit branching and
merging a bit more explicit. Rather than checking out a file, you
typically update your local desktop with the latest version from the
repository and just start working on files. There is no need to
exclusively lock files by checking them out which only gives you the
illusion of safety.

When you are ready to commit your changes back into the system, you
typically get latest again and merge in any changes that may have been
committed by other team members into your local workspace. Finally, you
commit your local changes (assuming everything builds) and resolve any
automatic merge conflicts (which is may not be very likely since you
just pulled all changes from the repository into your local workspace
unless there is a *lot* of repository activity going on).

The point here is to recognize that the implicit branching model
(*branch-per-task*) is almost certainly already in use in your day to
day work. It is not necessary to employ your source control’s branching
feature to employ this branching model, unless you need multiple
developers working on that single task. In that case, you would create
an explicit branch for that task so that it can be shared. However, keep
in mind that when multiple developers work on an explicit branch, the
branching and merging model for that individual branch will look like
the implicit *branch-per-task* model as I described.

