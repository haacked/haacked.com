---
layout: post
title: "Git Alias To Migrate Commits To A Branch"
date: 2015-06-29 -0800
comments: true
categories: [github git]
---

Show of hands if this ever happens to you. After a long day of fighting fires at work, you settle into your favorite chair to unwind and write code. Your fingers fly over the keyboard punctuating your code with semi-colons or paretheses or whatever is appropriate.

But after a few commits, it dawns on you that you're in the wrong branch. Yeah? Me too. This happens to me all the time because I lack impulse control. You can put your hands down now.

### GitHub Flow

As you may know, a key component of the [GitHub Flow](https://guides.github.com/introduction/flow/) lightweight workflow is to do all new feature work in a branch. Fixing a bug? Create a branch! Adding a new feature? Create a branch! Need to climb a tree? Well, you get the picture.

So what happens when you run into the situation I just described? Are you stuck? Heavens no! [The thing about Git](http://2ndscale.com/rtomayko/2008/the-thing-about-git) is that its very design supports fixing up mistakes after the fact. It's very forgiving in this regard. For example, a recent blog post on the GitHub blog highlights all the different ways [you can undo mistakes in Git](https://github.com/blog/2019-how-to-undo-almost-anything-with-git).

### The Easy Case - Fixing `master`

This is the simple case. I made commits on master that were intended for a branch off of master. Lets walk through this scenario step by step with some visual aids.

The following diagram shows the state of my repository before I got all itchy trigger finger on it.

![Original state](https://cloud.githubusercontent.com/assets/19977/8369477/2d2b8812-1b6f-11e5-9e68-190f988172af.png)

As you can see, I have two commits to the `master` branch. HEAD points to the tip of my current branch. You can also see a remote tracking branch named `origin/master` (_this is a special branch that tracks the `master` branch on the remote server_). So at this point, my local `master` matches the `master` on the server.

This is the state of my repository when I am struck by inspiration and I start to code.

![First Commit](https://cloud.githubusercontent.com/assets/19977/8369476/2d2ab536-1b6f-11e5-8971-079be907489a.png)

I make one commit. Then two.

![Second Commit - It's fixing time!](https://cloud.githubusercontent.com/assets/19977/8369478/2d2bd268-1b6f-11e5-893b-abeceefb4650.png)

Each time I make a commit, the local `master` branch is updated to the new commit. Uh oh! As in the scenario in the opening paragraph, I meant to create these two commits on a new branch creatively named `new-branch`. I better fix this up.

So the first thing I do is create this branch without switching to it.

`git branch new-branch`

![new branch](https://cloud.githubusercontent.com/assets/19977/8369479/2d2f10d6-1b6f-11e5-8fe9-0a30c4b4ad27.png)

Notice that `HEAD` still points to `master`. Also note that `new-branch` points to the same location. This is why branching is so cheap. A branch is just a pointer to a commit.

I created a branch that points to the latest commit to make it safe for me to reset the master branch back to the state it is on the server. I want to make sure I have an easy way to still reference the commit that the branch points to before I reset it.

`git reset origin/master --hard`

![master restored](https://cloud.githubusercontent.com/assets/19977/8369481/2d3feabe-1b6f-11e5-91ff-22fc27d0d13b.png)

Notice that `HEAD` and `master` are back to their original position, but `new-branch` continues to point to the latest commit.

Now I can simply check out the `new-branch` and continue my work.

![checkout the new branch](https://cloud.githubusercontent.com/assets/19977/8369480/2d3d416a-1b6f-11e5-94c8-301aa8fbd4d1.png)

This moves `HEAD` to the new branch and now I can continue to work on the branch as if I had never made this mistake. Yay!

Here's the set of commands that I ran all together.

```bash
git branch new-branch
git reset origin/master --hard
git checkout new-branch
```

### Fixing up a non-master branch

![The wrong branch](https://cloud.githubusercontent.com/assets/19977/8369613/48019364-1b71-11e5-9a28-b748a2802ed7.png)

This case is a bit more complicated. Here I have a branch named `wrong-branch` that is my current branch. But I thought I was working in the `master` branch. I make two commits in this branch by `mistake` which causes this fine mess.

![A fine mess](https://cloud.githubusercontent.com/assets/19977/8369614/4801b83a-1b71-11e5-898a-4c116e83b749.png)

What I want here is to migrate commits `E` and `F` to a new branch off of `master`. Here's the set of commands.

Let's walk through these steps one by one. Not to worry, as before, I create a new branch.

`git branch new-branch`

![A new branch](https://cloud.githubusercontent.com/assets/19977/8369616/48025ac4-1b71-11e5-9e0f-114e9bbb0001.png)

Again, just like before, I reset the current branch to the state of the current branch as it exists on the server.

`git reset origin/wrong-branch --hard`

![Reset the current branch](https://cloud.githubusercontent.com/assets/19977/8369612/47fe6374-1b71-11e5-92dd-26ddf71d5a7a.png)

But now, I need to move the `new-branch` onto `master`.  

`git rebase --onto master origin/wrong-branch new-branch`

![Final result](https://cloud.githubusercontent.com/assets/19977/8382092/06f71640-1be5-11e5-9f90-2b433bd6ffd8.png)

The `git rebase` command is a great way to move (well, actually you replay commits, but that's a story for another day) commits onto other branches. The handy `--onto` flag makes it possible to specify a range of commits to move elsewhere. [Pivotal Labs has a helpful post](http://pivotallabs.com/git-rebase-onto/) that describes this option in more detail.

So in this case, I moved commits `E` and `F` because they are the ones between `origin/wrong-branch` exclusive and `new-branch` inclusive

Here's the set of command I ran all together.

```bash
git branch new-branch
git reset origin/wrong-branch --hard
git rebase --onto master origin/wrong-branch new-branch
```

### Migrate commit ranges - great for local only branches

The assumption I made in the past two examples is that I'm working with branches that I've pushed to a remote. When you push a branch to a remote, you can create a local "remote tracking branch" that tracks the state of the branch on the remote server using the `-u` option.

For example, when I pushed the `wrong-branch`, I ran the command `git push -u origin wrong-branch` which not only pushes the branch to the remote (named `origin`), but creates the branch named `origin/wrong-branch` which corresponds to the state of `wrong-branch` on the server.

I can use a remote tracking branch as a convenient "Save Point" that I can reset to if I accidentally make commits on the corresponding local branch. It makes it easy to find the range of commits that are only on my machine and move just those.

But I could be in the situation where I don't have a remote branch. Or maybe the branch I started muddying up already had a local commit that I _don't_ want to move.

That's fine, I can just specify a commit range. For example, if I only wanted to move the last commit on `wrong-branch` into a new branch, I might do this.

```bash
git branch new-branch
git reset HEAD~1 --hard
git rebase --onto master HEAD~1 new-branch
```

### Alias was a fine TV show, but a better Git technique

When you see the set of commands I ran, I hope you're thinking "Hey, that looks like a rote series of steps and you should automate that!" This is why I like you. You're very clever and very correct!

Automating a series of git commands sounds like a job for a Git Alias! Aliases are a powerful way of automating or extending Git with your own Git commands.

In a blog post I wrote last year, [GitHub Flow Like a Pro with these 13 Git aliases](http://haacked.com/archive/2014/07/28/github-flow-aliases/), I wrote about some aliases I use to support my workflow.

Well now I have one more to add to this list. I decided to call this alias, `migrate`.

```
    migrate = "!f(){ git branch $1 && git reset @{u} --hard && git rebase --onto ${2-master} @{u} $1; }; f"
```

So if I'm on `master` with a repository that's tracking a remote and I want move my local commits to a new branch, I'd just run `git migrate new-branch`.

If I'm not on `master`, but on a branch named `wrong-branch` I'd just do this:

`git migrate new-branch origin/wrong-branch`

Here's the output from that command using the second example where I migrate commits `E` and `F` to a new branch.

![git migrate command output](https://cloud.githubusercontent.com/assets/19977/8381990/681cd2c6-1be4-11e5-96db-56e4cee7306c.png)

And there you go. A nice alias that automates a set of steps to fix a common mistake. Let me know if you find it useful!
