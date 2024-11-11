---
title: "Supercharge your debugging with git bisect"
description: "Git bisect is an underrated but very powerful tool to include in your debugging toolbox. In short, it helps you find the commit that introduced a bug. Here's an example of how to use it."
tags: [git]
excerpt_image: https://github.com/user-attachments/assets/0b9e8bd0-e26d-490a-9642-fcc9cc49e2bc
---

Ever look for a recipe online only to scroll through a self-important rambling 10-page essay about a trip to Tuscany that inspired the author to create the recipe? Finally, after wearing out your mouse, trackpad, or Page Down key to scroll to the end, you get to the actual recipe. I hate those.

So I'll spare you the long scroll and start this post with a `git bisect` cheat sheet and then I'll tell you about my trip to hell that lead me to write this post.

```bash
$ git bisect start
$ git bisect bad                 # Current version is bad
$ git bisect good v2.6.13-rc2    # v2.6.13-rc2 is known to be good
. # Repeat git bisect [good|bad] until you find the commit that introduced the bug
$ git bisect reset
```

![bisect](https://github.com/user-attachments/assets/0b9e8bd0-e26d-490a-9642-fcc9cc49e2bc)

## The One Where Poor Phil is Stumped

Like Groot, I was stumped.

I'm learning Blazor by building a simple app. After a while of working in a feature branch, I decided to test a logged out scenario.

When I tried to log back in, the login page was stuck in an infinite redirect loop.

I tried everything I could think of. I found every line of code that did a redirect and put a breakpoint in it, none of them were hit. I allowed anonymous on the login page. I tried playing with authorization policies. No dice. I asked Copilot for help. It offered some support and good advice, but it led nowhere. I even sacrificed two chickens and a goat, but not even the denizens of the seven hells could help me.

I switched back to my `main` branch to see if the bug was there, and lo and behold it was! That meant this bug had been in the code for a while and I hadn't noticed because I was always logged in.

Often, when faced with such a bug, you might go on a divide and conquer mission. Start removing code you think might be related and see if the bug goes away. But in my case, that would encompass a large search area because I had no idea what the cause was or where to start cutting.

It was clear to me that some cross-cutting concern was causing this bug. I needed to find the commit that introduced it to reduce the scope of my search. Enter `git bisect`.

## Git Bisect to the Rescue

From [the docs](https://git-scm.com/docs/git-bisect):

> This command uses a binary search algorithm to find which commit in your project’s history introduced a bug. You use it by first telling it a "bad" commit that is known to contain the bug, and a "good" commit that is known to be before the bug was introduced. Then git bisect picks a commit between those two endpoints and asks you whether the selected commit is "good" or "bad". It continues narrowing down the range until it finds the exact commit that introduced the change.

The key thing to note here is that it's a binary search. So even if the span of commits you're searching is 128 commits, it'll take at most 7 steps to find the commit that introduced the bug (<code>2<sup>7</sup> = 128</code>).

Here's how I used it:

```bash
$ git bisect start # Get the ball rolling.
$ git bisect bad   # The current commit is bad.
```

Now I need to supply the last known good commit. That could be a search of its own, but usually you have a good idea. For example, you might know the last release was good so you use the tag for that release.

In my case, I found the commit, `543ada5`, where I first implemented the login page because I know it worked then. Yes, I do [test my own code](https://haacked.com/archive/2013/03/04/test-better.aspx/).

```bash
$ git bisect good 543ada5
Bisecting: 7 revisions left to test after this (roughly 3 steps)
[9736e3f90b571bebf512c2acb1f7ef14f3a77df4] Update all the NPMs
```

After calling `git bisect good` with the known good commit, git bisect picked a commit between the bad and good commit, `9736e3f`.

I tested that commit and it turns out the bug wasn't there! So I told git bisect that commit was good.

```bash
$ git bisect good
Bisecting: 3 revisions left to test after this (roughly 2 steps)
[b9db65316a7f569c3ef9ed1eb4caa2072a6ba5d8] Show guests on Details page
```

After a few more iterations of this, git bisect found the commit that introduced the bug.

```bash
4e08eb48956b80a7a33987df272d30acb5bd6ee2 is the first bad commit
commit 4e08eb48956b80a7a33987df272d30acb5bd6ee2
Author: Phil Haack <haacked@gmail.com>
Date:   Thu Oct 10 15:38:21 2024 -0700
```

That commit seemed pretty innocuous but I did notice something odd.

I made this change in the `App.razor` file because I was tired of adding render mode `InteractiveServer` to nearly every page.

```diff
- <Routes />
+ <Routes @rendermode="InteractiveServer" />
```

It turns out, this change wasn't exactly wrong, just incomplete. I can save the proper fix as a follow-up post. I'm annoyed that Copilot wasn't able to offer up the eventual solution because I found it by googling around.

Now that I found the culprit, I can get back to my original state before running `git bisect` by calling `git bisect reset`.

## Challenges with Git Bisect

I encourage you to read the docs on `git bisect` as there are other sub-commands that are important.

For example, sometimes a commit cannot be tested, such as a broken build. In that case, you can call `git bisect skip` to skip that commit.

In practice, I found cases where you have to do a bit of tweaking to get the commit to run. For example, one commit had the following build error:

> error NU1903: Warning As Error: Package 'System.Text.Json' 8.0.4 has a known high severity vulnerability

At the time that I wrote that commit, everything built fine. Since I only want to build and test locally, I ignored that warning in order to test the commit.

## Automating Git Bisect

The reason I bring up these challenges is `git bisect` has the potential to be automated. You could write a script that builds and tests each commit. If a commit fails to build or test, the script could call `git bisect skip` for you.

For example, it'd be nice to do something like this:

```bash
git bisect run dotnet test
```

This would run `dotnet test` on each commit and automatically call `git bisect skip` if the test fails.

However, in practice, it doesn't work as well as you'd like. Commits that were fine when you wrote them might not build any longer. Also, what you really probably want to do is inject a new test to be run during the git bisect process.

Not to mention if you have integration tests that hit the database. You'd have to have migrations run up and down during the process of `git bisect`.

I've considered building tooling that would solve these problems, but in my experience, so few .NET developers I know make regular use of `git bisect` that it's hard to justify the effort. Maybe this post will convince you to add this tool to your repoirtoire.
