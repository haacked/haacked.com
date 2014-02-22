---
layout: post
title: "How to review a merge commit"
date: 2014-02-21 -0800
comments: true
categories: [jekyll]
---

If you lead a charmed life, most of your merges will be clean. Meaning that when you merge one branch into another, there are no conflicts.

But I don't live in that fairy tale land. From time to time, a merge will result in a conflict and I'll have to resolve those conflicts as part of the merge commit.

If you stick to small iterative branches, this usually won't be so bad. But from time to time, it's unavoidable that you will have a long running branch you need to merge into another that will require significant work in order to resolve the conflicts. 

Any time you have significant work, you should have others review that code in a pull request. When my team is in this situation, we actually push the merge commit into its own branch.

For example, suppose I want to merge the `master` branch into a branch named `long-running-branch`. I'll create a new branch named something like `merge-master-into-long-running-branch`. I'll then perform the merge in that branch (when I am resolving a gnarly merge my outbursts can be rightly described as a performance). When I'm done and everything is working, I'll push that new branch to GitHub and create a pull request from it for others to review.

In git that looks like:

```bash
git checkout long-running-branch
git checkout -b merge-master-into-long-running-branch
git merge master
git push origin merge-master-into-long-running-branch
```

The first command just makes sure I'm in the `long-running-branch`. The second command uses the `-b` to create a new branch named `merge-master-into-long-running-branch` based off the current one. I then merge `master` into this branch. And finally I push it to GitHub.

That way, someone can do a quick review to make sure the merge doesn't break anything and merge it in.

However, this runs into some problems as articulated by my co-worker Paul after merging my merge PR.

![how do you review a merge conflict](https://f.cloud.github.com/assets/19977/2236359/c3c993ee-9b5b-11e3-8fc3-63c364ca3f08.png)

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
git log -1 -p --cc cc5b002a
```

If we look at the [documentation `git log`](https://www.kernel.org/pub/software/scm/git/docs/git-log.html#_diff_formatting), we can see that the `--cc` flag is the one that's interesting to us.

> --cc
> This flag implies the -c option and further compresses the patch output by omitting uninteresting hunks whose contents in the parents have only two variants and the merge result picks one of them without modification.

Well, it probably helps to look at `-c` too then.

> -c
> With this option, diff output for a merge commit shows the differences from each of the parents to the merge result simultaneously instead of showing pairwise diff between a parent and the result one at a time. Furthermore, it lists only files which were modified from all parents.

In other words, this will show us ONLY what's different in this commit from all of the parents of this commit. If there were no conflicts, this would be empty.

This is a handy way to help focus your review on exactly what changed. There have been many times where a bad merge conflict resolution introduced bugs or regressions into a code base. So I hope you find this useful. Go review some code!

Note, if you're wondering how I found this example commit, I ran `git log --min-parents=2 -p --cc` and looked for a commit with a diff.

That filters the git log with commits that have at least two parents.