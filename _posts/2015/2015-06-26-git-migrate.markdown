---
layout: post
title: "Git Alias To Migrate Commits To A Branch"
date: 2015-06-26 -0800
comments: true
categories: [github git]
---

Show of hands if this happens to you. You get excited about an idea and just start writing code and creating commits only to realize you're in the wrong branch. Well, you'll have to tell me about it in the comments because I can't see your hands. This happens to me all the time because I lack impulse control.

As you may know, a key component of the GitHub Flow workflow is to do all new feature work in a branch. Fixing a bug? Create a branch! Adding a new feature? Create a branch! Need to climbe a tree? Well, you get the picture.

So what happens when you run into this situation? Are you stuck? Heavens no! [The thing about Git](http://2ndscale.com/rtomayko/2008/the-thing-about-git) is that its very design supports fixing up mistakes after the fact. It's very forgiving in this regard. For example, a recent blog post on the GitHub blog highlights all the different ways [you can undo mistakes in Git](https://github.com/blog/2019-how-to-undo-almost-anything-with-git).

### The Easy Case - Fixing `master`

This is the simple case. I made commits on master that were intended for a branch off of master.

I can run the following set of Git commands to migrate my changes to a new branch and restore master to reflect its state on the remote (typically GitHub.com in my case).

```bash
git branch new-branch
git reset origin/master --hard
git checkout new-branch
```

Let me walk through this with some pictures for the visual among you.

So here's the state of my repository before I got all itchy trigger finger on it.

![Original state](https://cloud.githubusercontent.com/assets/19977/8369477/2d2b8812-1b6f-11e5-9e68-190f988172af.png)

As you can see, I have two commits to the `master` branch. HEAD points to the tip of my current branch. You can also see a remote tracking branch named `origin/master`. So at this point, my local `master` matches the `master` on the server as far as I can tell.

This is when I am struck by inspiration and I start some work.

![First Commit](https://cloud.githubusercontent.com/assets/19977/8369476/2d2ab536-1b6f-11e5-8971-079be907489a.png)

I make one commit. Then two.

![Second Commit - It's fixing time!](https://cloud.githubusercontent.com/assets/19977/8369478/2d2bd268-1b6f-11e5-893b-abeceefb4650.png)

Each time I make a commit, the local `master` branch is updated to the new commit. Uh oh! I meant to create these two commits on a new branch creatively named `new-branch`. I better fix this up.

So the first thing I do is create this branch without switching to it.

`git branch new-branch`

![new branch](https://cloud.githubusercontent.com/assets/19977/8369479/2d2f10d6-1b6f-11e5-8fe9-0a30c4b4ad27.png)

Notice that `HEAD` still points to `master`. Also note that `new-branch` simply points to the same location. This is why branching is so cheap. A branch is just a pointer to a commit.

Creating a branch pointing to the latest commit makes it safe for me to reset the master branch back to the state it is on the server.

`git reset origin/master --hard`

![master restored](https://cloud.githubusercontent.com/assets/19977/8369481/2d3feabe-1b6f-11e5-91ff-22fc27d0d13b.png)

Notice that `HEAD` and `master` are back to their original position, but `new-branch` continues to point to the latest commit.

Now I can simply check out the `new-branch` and continue my work.

![checkout the new branch](https://cloud.githubusercontent.com/assets/19977/8369480/2d3d416a-1b6f-11e5-94c8-301aa8fbd4d1.png)

This moves `HEAD` to the new branch and now I can continue to work on the branch as if I had never made this mistake. Yay!

### Fixing up a non-master branch

![The wrong branch](https://cloud.githubusercontent.com/assets/19977/8369613/48019364-1b71-11e5-9a28-b748a2802ed7.png)

This case is a bit more complicated. Here I have a branch named `wrong-branch` that is my current branch. But I thought I was working in the master branch. I inadverdently make two commits in this branch, leaving us in this fine mess.

![A fine mess](https://cloud.githubusercontent.com/assets/19977/8369614/4801b83a-1b71-11e5-898a-4c116e83b749.png)

What I want here is to migrate commits `E` and `F` to a new branch off of `master`. Here's the set of commands.

```bash
git branch new-branch
git reset origin/wrong-branch --hard
git rebase --onto master origin/wrong-branch new-branch
```

Let's walk through these steps one by one. Not to worry, as before, I create a new branch.

`git branch new-branch`

![A new branch](https://cloud.githubusercontent.com/assets/19977/8369616/48025ac4-1b71-11e5-9e0f-114e9bbb0001.png)

Again, just like before, I reset the current branch to the state of the current branch as it exists on the server.

`git reset origin/wrong-branch --hard`

![Reset the current branch](https://cloud.githubusercontent.com/assets/19977/8369612/47fe6374-1b71-11e5-92dd-26ddf71d5a7a.png)

But now, I need to move the `new-branch` onto `master`.  

`git rebase --onto master origin/wrong-branch new-branch`

![Final result](https://cloud.githubusercontent.com/assets/19977/8382092/06f71640-1be5-11e5-9f90-2b433bd6ffd8.png)

As you know, the `git rebase` command is the way you move (well, actually you replay) commits onto other branches. The handy `--onto` flag makes it possible to specify a range of commits to move elsewhere. [Pivotal Labs has a helpful post](http://pivotallabs.com/git-rebase-onto/) that describes this option in more detail. So in this case, I moved commits `E` and `F` because they are the ones between `origin/wrong-branch` exclusive and `new-branch` inclusive.

### Fix up local only branches

The assumption I made in the past two examples is that I'm working with branches that I've pushed to a remote. That's a convenient scenario because the version of the branch on the server (the remote) is a handy "Save Point" that I can reset to in the form of the remote tracking branch (the ones named `origin/*`).

This makes it easy to find the range of commits that are only on my machine and move those.

But I could be in the situation where I don't have a remote branch. Or maybe the branch I started muddying up already had a local commit.

That's fine, I can just specify a commit range. For example, if I only wanted to move the last commit on `wrong-branch` into a new branch, I might do this.

```bash
git branch new-branch
git reset HEAD~1 --hard
git rebase --onto master HEAD~1 new-branch
```

### Alias was a fine TV show, but a better Git technique

What I've shown are rote series of steps. Sounds like a job for a Git Alias! Aliases are a powerful way of automating or extending Git with your own Git commands.

In a blog post I wrote last year, [GitHub Flow Like a Pro with these 13 Git aliases](http://haacked.com/archive/2014/07/28/github-flow-aliases/), I wrote about some aliases I use to support my workflow.

Well now I have one more to add to this list. I decided to call this alias, `migrate`.

```
    migrate = "!f(){ git branch $1 && git reset @{u} --hard && git rebase --onto ${2-master} @{u} $1; }; f"
```

So if I'm on `master` with a repository that's tracking a remote and I want move my local commits to a new branch, I'd just run `git migrate new-branch`.

If I'm not on master, but on a branch named `wrong-branch` I'd just do this:

`git migrate new-branch origin/wrong-branch`

Here's the output from that command using the second example where I migrate commits `E` and `F` to a new branch.

![git migrate command output](https://cloud.githubusercontent.com/assets/19977/8381990/681cd2c6-1be4-11e5-96db-56e4cee7306c.png)

And there you go. A nice alias that automates a set of steps to fix a common mistake. Let me know if you find it useful!
