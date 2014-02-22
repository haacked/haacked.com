---
layout: post
title: "How to review a merge commit"
date: 2014-02-21 -0800
comments: true
categories: [jekyll]
---

Git does a pretty amazing job when it merges one branch into another. Most of the time, it merges without conflict. In a fairy tale world with rainbow skittles and peanut butter butterflies, _every_ merge would be without conflict.
But we live in the real world where it rains a lot and where merge conflicts are an inevitable fact of life.

![Is this what an Octopus merge looks like? - Dhaka traffic by Ranveig Thatta license CC BY 2.0](https://f.cloud.github.com/assets/19977/2239004/c8908f52-9c0c-11e3-855e-366c67a0abc9.jpg)

Git can only do so much to resolve conflicts. If two developers change the same line of code in different ways, someone has to figure out what the end result should be. That can't be done automatically.

The impact of merge conflicts can be mitigated by doing work in small iterations and merging often. But even so, the occasional long running branch and gnarly merge conflict are unavoidable.

Often, we treat the work to resolve a merge conflict as trivial. But in reality, with a major merge conflict, it may take a significant amonut of work to resolve it. And any time there is significant work, others should probably review that code in a pull request.

When my team is in this situation, we actually push the merge commit into its own branch and send a pull request.

For example, suppose I want to merge the `master` branch into a branch named `long-running-branch`. I'll create a new branch named something like `merge-master-into-long-running-branch`. I'll then perform the merge in that branch (when I am resolving a gnarly merge my outbursts can be rightly described as a performance). When I'm done and everything is working, I'll push that new branch to GitHub and create a pull request from it for others to review.

In git that looks like:

```bash
git checkout long-running-branch
git checkout -b merge-master-into-long-running-branch
git merge master # Lots of work to resolve the conflicts
git push origin merge-master-into-long-running-branch
```

The first command just makes sure I'm in the `long-running-branch`. The second command uses the `-b` to create a new branch named `merge-master-into-long-running-branch` based off the current one. I then merge `master` into this branch. And finally I push it to GitHub.

That way, someone can do a quick review to make sure the merge doesn't break anything and merge it in.

However, this runs into some problems as articulated by my quotable co-worker [Paul Betts](http://paulbetts.org/). In a recent merge commit PR that I sent, he made the following comment shortly before merging my PR.

![I have no idea how to review a merge commit](https://f.cloud.github.com/assets/19977/2236359/c3c993ee-9b5b-11e3-8fc3-63c364ca3f08.png)

The problem he alludes to is that when you merge one branch into another, the diff of that merge commit will show every change since the last merge. For the most part, that's all code that's already been reviewed and doesn't need to be reviewed again.

What you really want to look at is whether there were conflicts and what shenanigans did the person have to do to resolve those conflicts.

Well my hero [Russell Belfer](https://github.com/arrbee) (no blog, but he's [@arrbee on Twitter](https://twitter.com/arrbee)) to the rescue! He works on [LibGit2](https://github.com/libgit2/libgit2) so as you'd expect, he knows a thing or two about how Git works.

For the following discussion, let's use [this merge commit](https://github.com/SignalR/SignalR/commit/cc5b002a5140e2d60184de42554a8737981c846c) from the SignalR project as an example. This merge commit merges their `release` branch into their `dev` branch. The SHA for this commit is cc5b002a5140e2d60184de42554a8737981c846c but I'll use cc5b002a for short.

Recall that any given merge commit will have two or more parents. Typically though, we're dealing with just two as in this case.

You can use git diff to look at either side of the merge. For example:

```bash
git diff cc5b002a^1 cc5b002a
git diff cc5b002a^2 cc5b002a
```

Recall that the `^` caret is used to denote which parent of a commit we want to look at. So `^1` is the first parent. `^2` is the second parent.

So how do we see only the lines that changed as part of the conflict resolution?

```bash
git diff-tree --cc cc5b002a
```

You can see the output [in this gist](https://gist.github.com/Haacked/9160205). Notice how much less there is there compared to the output of the merge commit.

If we look at the [`git diff-tree` documentation](http://git-scm.com/docs/git-diff-tree), we can see that the `--cc` flag is the one that's interesting to us.

> --cc
> This flag changes the way a merge commit patch is displayed, in a similar way to the -c option. It implies the -c and -p options and further compresses the patch output by omitting uninteresting hunks whose the contents in the parents have only two variants and the merge result picks one of them without modification. When all hunks are uninteresting, the commit itself and the commit log message is not shown, just like in any other "empty diff" case.

Since the `--cc` option describes itself in terms of the `-c` option, let's look at that too.

> -c
> This flag changes the way a merge commit is displayed (which means it is useful only when the command is given one <tree-ish>, or --stdin). It shows the differences from each of the parents to the merge result simultaneously instead of showing pairwise diff between a parent and the result one at a time (which is what the -m option does). Furthermore, it lists only files which were modified from all parents.

The `-p` option mentioned generates a patch output rather than a normal diff output.

In other words, this will show us ONLY what's different in this commit from all of the parents of this commit. If there were no conflicts, this would be empty.

This is a handy way to help focus your review on exactly what changed. There have been many times where a bad merge conflict resolution introduced bugs or regressions into a code base. So I hope you find this useful. Go review some code!

Note, if you're wondering how I found this example commit, I ran `git log --min-parents=2 -p --cc` and looked for a commit with a diff.

That filters the git log with commits that have at least two parents.
