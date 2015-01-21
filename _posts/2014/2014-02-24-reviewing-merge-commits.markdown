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

Often, we treat the work to resolve a merge conflict as trivial. Worse, merges often are not reviewed very carefully (I'll explain why later). A major merge conflict may contain a significant amount of work to resolve it. And any time there is significant work, others should probably review that code in a pull request (PR for short). After all, a bad merge conflict resolution could introduce or reintroduce subtle bugs that were presumed to be fixed already.

As a great example, take a look at the [recent Apple Goto Fail bug](http://www.wired.com/threatlevel/2014/02/gotofail/). I'm not suggesting this was the result of a bad merge conflict resolution, but you could easily see how a bad merge conflict might produce such a bug and bypass careful code review.

```
if ((err = SSLHashSHA1.update(&hashCtx, &serverRandom)) != 0)
    goto fail;
if ((err = SSLHashSHA1.update(&hashCtx, @signedParams)) != 0)
    goto fail;
    goto fail;
if ((err = SSLHashSHA1.final(&hashCtx, &hashOut)) != 0)
    goto fail;
```

When my team is in this situation, we actually push the merge commit into its own branch and send a pull request.

For example, suppose I want to merge the `master` branch into a branch named `long-running-branch`. I'll create a new branch named something like `merge-master-into-long-running-branch`. I'll then perform the merge in that branch (when I am resolving a gnarly merge my outbursts can be rightly described as a performance). When I'm done and everything is working, I'll push that new branch to GitHub and create a pull request from it for others to review.

In git that looks like:

```bash
git checkout long-running-branch
git checkout -b merge-master-into-long-running-branch
git merge master
# Manually do a lot of work to resolve the conflicts and commit those changes
git push origin merge-master-into-long-running-branch
```

The first command just makes sure I'm in the `long-running-branch`. The second command uses the `-b` to create a new branch named `merge-master-into-long-running-branch` based off the current one. I then merge `master` into this branch. And finally I push it to GitHub.

That way, someone can do a quick review to make sure the merge doesn't break anything and merge it in.

However, this runs into some problems as articulated by my quotable co-worker [Paul Betts](http://paulbetts.org/). In a recent merge commit PR that I sent, he made the following comment just before he merged my PR.

![I have no idea how to review a merge commit](https://f.cloud.github.com/assets/19977/2236359/c3c993ee-9b5b-11e3-8fc3-63c364ca3f08.png)

The problem he alludes to is that when you merge one branch into another, the diff of that merge commit will show every change since the last merge. For the most part, that's all code that's already been reviewed and doesn't need to be reviewed again.

What you really want to look at is whether there were conflicts and what shenanigans did the person have to do to resolve those conflicts.

Well my hero [Russell Belfer](https://github.com/arrbee) (no blog, but he's [@arrbee on Twitter](https://twitter.com/arrbee)) to the rescue! He works on [LibGit2](https://github.com/libgit2/libgit2) so as you'd expect, he knows a thing or two about how Git works.

Recall that when you merge one branch into another, a new merge commit is created that points to both branches. In fact, a merge commit may have two or more parents as it's possible to merge multiple branches into one at the same time. But in most cases a merge commit has exactly two parents.

Let's look at an example of [such a merge commit](https://github.com/SignalR/SignalR/commit/cc5b002a5140e2d60184de42554a8737981c846c) from the SignalR project. This commit merges their `release` branch into their `dev` branch. The SHA for this commit is `cc5b002a5140e2d60184de42554a8737981c846c` which is pretty easy to remember but to be fair to those with drug addled brains, I'll use `cc5b002a` as a shorthand to reference this commit.

You can use git diff to look at either side of the merge. For example:

```bash
git diff cc5b002a^1 cc5b002a
git diff cc5b002a^2 cc5b002a
```

Recall that the `^` caret is used to denote which parent of a commit we want to look at. So `^1` is the first parent, `^2` is the second parent, and so on.

So how do we see only the lines that changed as part of the conflict resolution?

```bash
git diff-tree --cc cc5b002a
```

UPDATE: I just now learned from [@jspahrsummers](https://twitter.com/jspahrsummers) that `git show cc5b002a` works just as well and in my shell gives you the color coded diff. The merge commit generally won't contain any content _except_ for the conflict resolution.

```bash
git show cc5b002a
```

As I'll show later, the `--cc` option is useful for finding interesting commits like this.

You can see the output of the `gist show` command [in this gist](https://gist.github.com/Haacked/9192002). Notice how much less there is there compared to the [full diff of the merge commit](https://github.com/SignalR/SignalR/commit/cc5b002a5140e2d60184de42554a8737981c846c).

The `git diff-tree` command is a lower level command and if I had to guess, `git show` builds on top of it.

If we look at the [`git diff-tree` documentation](http://git-scm.com/docs/git-diff-tree), we can see that the `--cc` flag is the one that's interesting to us.

> --cc
> This flag changes the way a merge commit patch is displayed, in a similar way to the -c option. It implies the -c and -p options and further compresses the patch output by omitting uninteresting hunks whose the contents in the parents have only two variants and the merge result picks one of them without modification. When all hunks are uninteresting, the commit itself and the commit log message is not shown, just like in any other "empty diff" case.

Since the `--cc` option describes itself in terms of the `-c` option, let's look at that too.

> -c
> This flag changes the way a merge commit is displayed (which means it is useful only when the command is given one <tree-ish>, or --stdin). It shows the differences from each of the parents to the merge result simultaneously instead of showing pairwise diff between a parent and the result one at a time (which is what the -m option does). Furthermore, it lists only files which were modified from all parents.

The `-p` option mentioned generates a patch output rather than a normal diff output.

If you're not well versed in Git (and perhaps even if you are) that's a mouthful to read and a bit hard to fully understand what it's saying. But the outcome of the flag is simple. This option displays ONLY what is different in this commit from all of the parents of this commit. If there were no conflicts, this would be empty. But if there were conflicts, it shows us what changed in order to resolve the conflicts.

As I mentioned earlier, the work to resolve a merge conflict could itself introduce bugs. This technique provides a handy tool to help focus a code review on those changes and reduce the risk of bugs. Now go review some code!

_If you're wondering how I found this example commit, I ran `git log --min-parents=2 -p --cc` and looked for a commit with a diff._

_That filters the git log with commits that have at least two parents._
