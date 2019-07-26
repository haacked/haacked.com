---
title: "Git Alias to browse "
tags: [git,aliases]
description: "A git alias to launch your browser to the current repository"
---

Happy New Year! I hope you make the most of this year. To help you out, I have a tiny little Git alias that might save you a few seconds here and there.

When I'm working with Git on the command line, I often want to navigate to the repository on GitHub. So I open my browser and type in the URL like a Neanderthal. Yes, a little known fact about Neanderthals is that they were such hipsters they were using browsers before computers were even invented. Look it up.

But I digress. Typing in all those characters is a lot of work and I'm lazy and I like to automate all the things. So I wrote the following Git alias.

```
[alias]
  open = "!f() { REPO_URL=$(git config remote.origin.url); explorer ${REPO_URL%%.git}; }; f"
  browse = !git open
```

So when I'm in a repository directory on the command line, I can just type `git open` and it'll launch my default browser to the URL specified by the remote `origin`. In my case, this is typically a GitHub repository, but this'll work for other hosts.

The second line in that snippet is an alias for the alias. I wrote that because I just know I'm going to forget one day and type `git browse` instead of `git open`. So future me, you're welcome.

This alias makes a couple of assumptions.

1. You're running Windows
2. You use `https` for your remote origin.

In the first case, if you're running a Mac, you probably want to use `open` instead of `explorer`. For Linux, I have no idea, but I assume the same will work.

In the second case, if you're not using https, I can't help you. You might [try this approach](https://gist.github.com/igrigorik/6666860) instead.

_Update 2017-05-09_ I updated the alias to truncate `.git` at the end.
