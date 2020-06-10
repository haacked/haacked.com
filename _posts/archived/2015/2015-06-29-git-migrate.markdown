---
title: "Git Alias To Migrate Commits To A Branch"
description: "A git alias to migrate commits from the wrong branch to the right branch"
excerpt_image: https://cloud.githubusercontent.com/assets/19977/8382092/06f71640-1be5-11e5-9f90-2b433bd6ffd8.png
tags: [github,git,aliases]
---

Show of hands if this ever happens to you. After a long day of fighting fires at work, you settle into your favorite chair to unwind and write code. Your fingers fly over the keyboard punctuating your code with semi-colons or parentheses or whatever is appropriate.

But after a few commits, it dawns on you that you're in the wrong branch. Yeah? Me too. This happens to me all the time because I lack impulse control. You can put your hands down now.

## GitHub Flow

As you may know, a key component of the [GitHub Flow](https://guides.github.com/introduction/flow/) lightweight workflow is to do all new feature work in a branch. Fixing a bug? Create a branch! Adding a new feature? Create a branch! Need to climb a tree? Well, you get the picture.

So what happens when you run into the situation I just described? Are you stuck? Heavens no! [The thing about Git](http://2ndscale.com/rtomayko/2008/the-thing-about-git) is that its very design supports fixing up mistakes after the fact. It's very forgiving in this regard. For example, a recent blog post on the GitHub blog highlights all the different ways [you can undo mistakes in Git](https://github.com/blog/2019-how-to-undo-almost-anything-with-git).

## The Easy Case - Fixing the main branch

This is the simple case. I made commits on `master` that were intended for a branch off of master. Let's walk through this scenario step by step with some visual aids.

The following diagram shows the state of my repository before I got all itchy trigger finger on it.

![Initial state](https://cloud.githubusercontent.com/assets/19977/8390497/bbde5f8e-1c4c-11e5-9760-9e94236423d6.png)

As you can see, I have two commits to the `master` branch. HEAD points to the tip of my current branch. You can also see a remote tracking branch named `origin/master` (_this is a special branch that tracks the `master` branch on the remote server_). So at this point, my local `master` matches the `master` on the server.

This is the state of my repository when I am struck by inspiration and I start to code.

![First](https://cloud.githubusercontent.com/assets/19977/8390499/bbe3174a-1c4c-11e5-87ee-75aacf1a197a.png)

I make one commit. Then two.

![Second Commit - fixing time](https://cloud.githubusercontent.com/assets/19977/8390498/bbe2c524-1c4c-11e5-98c3-7526ae2277f9.png)

Each time I make a commit, the local `master` branch is updated to the new commit. Uh oh! As in the scenario in the opening paragraph, I meant to create these two commits on a new branch creatively named `new-branch`. I better fix this up.

The first step is to create the new branch. We can create it and check it out all in one step.

`git checkout -b new-branch`

![checkout a new branch](https://cloud.githubusercontent.com/assets/19977/8411869/0ba24220-1e3b-11e5-9895-42c486709937.png)

At this point, both the `new-branch` and `master` point to the same commit. Now I can force the master branch back to its original position.

`git branch --force master origin/master`

![force branch master](https://cloud.githubusercontent.com/assets/19977/8411975/b05c941e-1e3b-11e5-8e84-f36535fb7893.png)

Here's the set of commands that I ran all together.

```bash
git checkout -b new-branch
git branch --force master origin/master
```

## Fixing up a non-master branch

![The wrong branch](https://cloud.githubusercontent.com/assets/19977/8369613/48019364-1b71-11e5-9a28-b748a2802ed7.png)

This case is a bit more complicated. Here I have a branch named `wrong-branch` that is my current branch. But I thought I was working in the `master` branch. I make two commits in this branch by mistake which causes this fine mess.

![A fine mess](https://cloud.githubusercontent.com/assets/19977/8369614/4801b83a-1b71-11e5-898a-4c116e83b749.png)

What I want here is to migrate commits `E` and `F` to a new branch off of `master`. Here's the set of commands.

Let's walk through these steps one by one. Not to worry, as before, I create a new branch.

`git checkout -b new-branch`

![Always a new branch](https://cloud.githubusercontent.com/assets/19977/8412077/4d85a08c-1e3c-11e5-98eb-c421d2cf5159.png)

Again, just like before, I force `wrong-branch` to its state on the server.

`git branch --force wrong-branch origin/wrong-branch`

![force branch](https://cloud.githubusercontent.com/assets/19977/8412113/93984c46-1e3c-11e5-9329-f38adb158dcd.png)

But now, I need to move the commits from the branch `new-branch` onto `master`.  

`git rebase --onto master wrong-branch`

Note that `git rebase --onto` works on the _current branch (HEAD)_. So `git rebase --onto master wrong-branch` is saying migrate the commits between `wrong-branch` and `HEAD` onto master.

![Final result](https://cloud.githubusercontent.com/assets/19977/8382092/06f71640-1be5-11e5-9f90-2b433bd6ffd8.png)

The `git rebase` command is a great way to move (well, actually you replay commits, but that's a story for another day) commits onto other branches. The handy `--onto` flag makes it possible to specify a range of commits to move elsewhere. [Pivotal Labs has a helpful post](https://content.pivotal.io/blog/git-rebase-onto) that describes this option in more detail.

So in this case, I moved commits `E` and `F` because they are the ones since `wrong-branch` on the current branch, `new-branch`.

Here's the set of command I ran all together.

```bash
git checkout -b new-branch
git branch --force wrong-branch origin/wrong-branch
git rebase --onto master wrong-branch
```

## Migrate commit ranges - great for local only branches

The assumption I made in the past two examples is that I'm working with branches that I've pushed to a remote. When you push a branch to a remote, you can create a local "remote tracking branch" that tracks the state of the branch on the remote server using the `-u` option.

For example, when I pushed the `wrong-branch`, I ran the command `git push -u origin wrong-branch` which not only pushes the branch to the remote (named `origin`), but creates the branch named `origin/wrong-branch` which corresponds to the state of `wrong-branch` on the server.

I can use a remote tracking branch as a convenient "Save Point" that I can reset to if I accidentally make commits on the corresponding local branch. It makes it easy to find the range of commits that are only on my machine and move just those.

But I could be in the situation where I don't have a remote branch. Or maybe the branch I started muddying up already had a local commit that I _don't_ want to move.

That's fine, I can just specify a commit range. For example, if I only wanted to move the last commit on `wrong-branch` into a new branch, I might do this.

```bash
git checkout -b new-branch
git branch --force wrong-branch HEAD~1
git rebase --onto master wrong-branch
```

## Alias was a fine TV show, but a better Git technique

When you see the set of commands I ran, I hope you're thinking "Hey, that looks like a rote series of steps and you should automate that!" This is why I like you. You're very clever and very correct!

Automating a series of git commands sounds like a job for a Git Alias! Aliases are a powerful way of automating or extending Git with your own Git commands.

In a blog post I wrote last year, [GitHub Flow Like a Pro with these 13 Git aliases](https://haacked.com/archive/2014/07/28/github-flow-aliases/), I wrote about some aliases I use to support my workflow.

Well now I have one more to add to this list. I decided to call this alias, `migrate`. Here's the definition for the alias. Notice that it uses `git rebase --onto` which we used for the second scenario I described. It turns out that this happens to work for the first scenario too.

```ini
migrate = "!f(){ DEFAULT=$(git default); CURRENT=$(git symbolic-ref --short HEAD); git checkout -b $1 && git branch --force $CURRENT ${3-$CURRENT@{u}} && git rebase --onto ${2-$DEFAULT} $CURRENT; }; f"
```

Note this alias depends on an alias I wrote in my previous blog post.

```ini
default = !git symbolic-ref refs/remotes/origin/HEAD | sed 's@^refs/remotes/origin/@@'
```

There's a lot going on here and I could probably write a whole blog post unpacking it, but for now I'll try and focus on the usage pattern.

This alias has one required parameter, the new branch name, and two optional parameters.


parameter         | type     | Description
------------------|----------|------------------------------------------------------------------------
__branch-name__   | required | Name of the new branch. 
__target-branch__ | optional | Defaults to "master". The branch that the new branch is created off of. 
__commit-range__  | optional | The commits to migrate. Defaults to the current remote tracking branch.

This command always migrates the current branch.

If I'm on a branch and want to migrate the local only commits over to `master`, I can just run `git migrate new-branch-name`. This works whether I'm on `master` or some other wrong branch.

I can also migrate the commits to a branch created off of something other than `master` using this command: `git migrate new-branch other-branch`

And finally, if I want to just migrate the last commit to a new branch created off of master, I can do this.

`git migrate new-branch master HEAD~1`

And there you go. A nice alias that automates a set of steps to fix a common mistake. Let me know if you find it useful!

Also, I want to give a special thanks to [@mhagger](https://github.com/mhagger) for his help with this post. The [original draft pull request](https://github.com/Haacked/haacked.com/pull/205) had the grace of a two-year-old neurosurgeon with a mallet. The straightforward Git commands I proposed would rewrite the working tree twice. With his proposed changes, this alias never rewrites the working tree. Like math, there's often a more elegant solution with Git once you understand the available tools.
