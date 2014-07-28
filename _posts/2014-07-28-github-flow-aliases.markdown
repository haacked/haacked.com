---
layout: post
title: "GitHub Flow Like a Pro with these 13 Git Aliases"
date: 2014-07-28 -0800
comments: true
categories: [git github]
---

[GitHub Flow](http://scottchacon.com/2011/08/31/github-flow.html) is a Git work flow with a simple branching model. The following diagram of this flow is from Zach Holman's talk on [How GitHub uses GitHub to build GitHub](http://zachholman.com/talk/how-github-uses-github-to-build-github/).

![github-branching](https://cloud.githubusercontent.com/assets/19977/3638839/8343b14c-1063-11e4-8369-537f7d62be06.png)

You are now a master of GitHub flow. Drop the mic and go release some software!

Ok, there's probably a few more details than that diagram to understand. The basic idea is that new work (such as a bug fix or new feature) is done in a "topic" branch off of the `master` branch. At any time, you should feel free to push the topic branch and create a pull request (PR). A Pull Request is a discussion around some code and not [necessarily the completed work](https://github.com/blog/1124-how-we-use-pull-requests-to-build-github).

At some point, the PR is complete and ready for review. After a few rounds of review (as needed), either the PR gets closed or someone merges the branch into master and the cycle continues. If the reviews have been respectful, you may even still continue to like your colleagues.

It's simple, but powerful.

Over time, my laziness spurred me to write a set of Git aliases that streamline this flow for me. In this post, I share these aliases and some tips on writing your own. These aliases start off simple, but they get more advanced near the end. The advanced ones demonstrate some techniques for building your own very useful aliases.

## Intro to Git Aliases

An alias is simply a way to add a shorthand for a common Git command or set of Git commands. Some are quite simple. For example, here's a common one:

```bash
git config --global alias.co checkout
```

This sets `co` as an alias for `checkout`. If you open up your `.gitconfig` file, you can see this in a section named `alias`.

```ini
[alias]
    co = checkout
```

With this alias, you can checkout a branch by using `git co some-branch` instead of `git checkout some-branch`. Since I often edit aliases by hand, I have one that opens the `gitconfig` file with my default editor.

```ini
    ec = config --global -e
```

These sort of simple aliases only begin to scratch the surface.

## GitHub Flow Aliases

### Get my working directory up to date.

When I'm ready to start some work, I always do the work in a new branch. But first, I make sure that my working directory is up to date with the origin before I create that branch. Typically, I'll want to run the following commands:

```bash
git pull --rebase --prune
git submodule update --init --recursive
```

The first command pulls changes from the remote. If I have any local commits, it'll rebase them to come after the commits I pulled down. The `--prune` option removes remote-tracking branches that no longer exist on the remote.

This combination is so common, I've created an alias `up` for this.

```ini
    up = !git pull --rebase --prune $@ && git submodule update --init --recursive
```

Note that I'm combining two git commands together. I can use the `!` prefix to execute everything after it in the shell. This is why I needed to use the full git commands. Using the `!` prefix allows me to use _any_ command and not just git commands in the alias.

### Starting new work

At this point, I can start some new work. All new work starts in a branch so I would typically use `git checkout -b new-branch`. However I alias this to `cob` to build upon `co`.

```ini
    cob = checkout -b
```

Note that this simple alias is expanded in place. So to create a branch named "emoji-completion" I simply type `git cob emoji-completion` which expands to `git checkout -b emoji-completion`.

With this new branch, I can start writing the crazy codes. As I go along, I try and commit regularly with my `cm` alias.

```ini
    cm = !git add -A && git commit -m
```

For example, `git cm "Making stuff work"`. This adds all changes including untracked files to the index and then creates a commit with the message "Making Stuff Work".

Sometimes, I just want to save my work in a commit without having to think of a commit message. I could stash it, but I prefer to write a proper commit which I will change later.

`git save` or `git wip`.  The first one adds all changes including untracked files and creates a commit. The second one only commits tracked changes. I generally use the first one.

```ini
    save = !git add -A && git commit -m 'SAVEPOINT'
    wip = !git add -u && git commit -m "WIP" 
```

When I return to work, I'll just use `git undo` which resets the previous commit, but keeps all the changes from that commit in the working directory.

```ini
    undo = reset HEAD~1 --mixed
```

Or, if I merely need to modify the previous commit, I'll use `git amend`

```ini
    amend = commit -a --amend
```

The `-a` adds any modifications and deletions of existing files to the commit but ignores brand new files. The `--amend` launches your default commit editor (Notepad in my case) and lets you change the commit message of the most recent commit.

### A proper reset

There will be times when you explore a promising idea in code and it turns out to be crap. You just want to throw your hands up in disgust and burn all the work in your working directory to the ground and start over.

In an attempt to be helpful, people might recommend: `git reset HEAD --hard`.

Slap those people in the face. It's a bad idea. Don't do it!

That's basically a delete of your current changes without any undo. As soon as you run that command, Murphy's law dictates you'll suddenly remember there was that one gem among the refuse you don't want to rewrite.

Too bad. If you reset work that you _never committed_ it is gone for good. Hence, the `wipe` alias.

```ini
    wipe = !git add -A && git commit -qm 'WIPE SAVEPOINT' && git reset HEAD~1 --hard
```

This commits everything in my working directory and then does a hard reset to remove that commit. The nice thing is, the commit is still there, but it's just unreachable. Unreachable commits are a bit inconvenient to restore, but at least they are still there. You can run the `git reflog` command and find the SHA of the commit if you realize later that you made a mistake with the reset. The commit message will be "WIPE SAVEPOINT" in this case.

### Completing the pull request

While working on a branch, I regularly push my changes to GitHub. At some point, I'll go to github.com and create a pull request, people will review it, and then it'll get merged. Once it's merged, I like to [tidy up and delete the branch via the Web UI](https://github.com/blog/1335-tidying-up-after-pull-requests). At this point, I'm done with this topic branch and I want to clean everything up on my local machine. Here's where I use one of my more powerful aliases, `git bdone`.

This alias does the following.

1. Switches to master (though you can specify a different default branch)
2. Runs `git up` to bring master up to speed with the origin
3. Deletes all branches already merged into master using another alias, `git bclean`

It's quite powerful and useful and demonstrates some advanced concepts of git aliases. But first, let me show `git bclean`. This alias is meant to be run from your master (or default) branch and does the cleanup of merged branches.

```ini
bclean = "!f() { git branch --merged ${1-master} | grep -v " ${1-master}$" | xargs -r git branch -d; }; f"
```

If you're not used to shell scripts, this looks a bit odd. What it's doing is defining a function and then calling that function. The general format is `!f() { /* git operations */; }; f` We define a function named `f` that encapsulates some git operations, and then we invoke the function at the very end.

What's cool about this is we can take advantage of arguments to this alias. In fact, we can have optional parameters. For example, the first argument to this alias can be accessed via `$1`. But suppose you want a default value for this argument if none is provided. That's where the curly braces come in. Inside the braces you specify the argument index (`$0` returns the whole script) followed by a dash and then the default value.

Thus when you type `git bclean` the expression `${1-master}` evaluates to `master` because no argument was provided. But if you're working on a GitHub pages repository, you'll probably want to call `git bclean gh-pages` in which case the expression `${1-master}` evaluates to `gh-pages` as that's the first argument to the the alias.

Let's break down this alias into pieces to understand it.

`git branch --merged ${1-master}` lists all the branches that have been merged into the specify branch (or master if none is specified). This list is then piped into the `grep -v "${1-master}"` command. [Grep](http://www.gnu.org/software/grep/manual/grep.html) prints out lines matching the pattern. The `-v` flag inverts the match. So this will list all merged branches that are not `master` itself. Finally this gets piped into `xargs` which takes the standard input and executes the `git branch -d` line for each line in the standard input which is piped in from the previous command.

In other words, it deletes every branch that's been merged into `master` except `master`. I love how we can compose these commands together.

With `bclean` in place, I can compose my git aliases together and write `git bdone`.

```ini
    bdone = "!f() { git checkout ${1-master} && git up && git bclean ${1-master}; }; f"
```

I use this one all the time when I'm deep in the GitHub flow. And now, you too can be a GitHub flow master.

## The List

Here's a list of all the aliases together for your convenience.

```ini
[alias]
    co = checkout
    ec = config --global -e
    up = !git pull --rebase --prune $@ && git submodule update --init --recursive
    cob = checkout -b
    cm = !git add -A && git commit -m
    save = !git add -A && git commit -m 'SAVEPOINT'
    wip = !git add -u && git commit -m "WIP" 
	undo = reset HEAD~1 --mixed
    amend = commit -a --amend
    wipe = !git add -A && git commit -qm 'WIPE SAVEPOINT' && git reset HEAD~1 --hard
    bclean = "!f() { git branch --merged ${1-master} | grep -v " ${1-master}$" | xargs -r git branch -d; }; f"
    bdone = "!f() { git checkout ${1-master} && git up && git bclean ${1-master}; }; f"
```

## Credits and more reading

It would be impossible to source every git alias I use as many of these are pretty common and I've adapted them for my own needs. However, here are a few blog posts that provided helpful information about git aliases that served as my inspiration. I also added a couple posts about how GitHub uses pull requests.

* [Git Aliases Parameters](http://jondavidjohn.com/git-aliases-parameters/) taught me how to specify a default value for positional parameters.
* [Delete already merged branches](http://stevenharman.net/git-clean-delete-already-merged-branches) was the inspiration for `git bclean`.
* [Aliases Git Wiki Page](https://git.wiki.kernel.org/index.php/Aliases) has a bunch of useful aliases.
* [How GitHub uses Pull Requests](https://github.com/blog/1124-how-we-use-pull-requests-to-build-github)
* [How GitHub uses GitHub to build GitHub](http://zachholman.com/talk/how-github-uses-github-to-build-github/)

_PS: If you liked this post [follow me on Twitter](https://twitter.com/haacked) for interesting links and my wild observations about pointless drivel_

_PPS_: For Windows users, these aliases don't require using Git Bash. They work in PowerShell and CMD when msysgit is in your path. For example, if you install [GitHub for Windows](https://windows.github.com/) and use the GitHub Shell, these all work fine.
