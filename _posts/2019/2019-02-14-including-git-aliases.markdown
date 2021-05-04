---
title: "Include my Git Aliases"
description: "An easy way to include all of my Git aliases in your git config"
tags: [git,aliases]
excerpt_image: https://user-images.githubusercontent.com/19977/52824672-5ad5af00-306e-11e9-8cc3-db186bf00f13.png
---

I'm a big fan of Git aliases as a means of improving your developer workflow when using Git. They are great for automating common tasks. They also can help make sense of the byzantine set of options Git has.

![Alias TV show cast](https://user-images.githubusercontent.com/19977/52824672-5ad5af00-306e-11e9-8cc3-db186bf00f13.png)

So far, I've written a few blog posts with helpful aliases.

* [GitHub Flow Like a Pro with these 13 Git Aliases](https://haacked.com/archive/2014/07/28/github-flow-aliases/)
* [Git Alias To Migrate Commits To A Branch](https://haacked.com/archive/2015/06/29/git-migrate/)
* [Git Alias to browse](https://haacked.com/archive/2017/01/04/git-alias-open-url/)
* [Git alias to create a pull request in the browser](https://twitter.com/haacked/status/1077790446372368384)

It's wonderful to see all these aliases out there, but it's a bit tedious to copy and paste all these aliases from all these sources into your `.gitconfig` file. It's like going on a wild goose chase for a Rambaldi device. I'm here to reduce the tedium in your life.

## Git Config Includes

In Git 1.7.10 and later, you can add an include section to your Git config file with a path to another file that includes config settings. For example, if someone were to...say...publish a file with just their Git aliases named `.gitconfig.aliases`. You could copy that file to your home directory and reference it in your `.gitconfig` like so:

```ini
[include]
    path = ~/.gitconfig.aliases
```

Now who in the world is so kind to include all their Git aliases in a single file you ask? Me. I'm that person.

A common pattern on GitHub is for people to publish their configuration settings in a repository on GitHub named `dotfiles`. For example, here's my [haacked/dotfiles](https://github.com/haacked/dotfiles) repository. I wasn't the one to come up with this idea. I copied [holman/dotfiles](https://github.com/holman/dotfiles) who wrote a whole post suggesting that [Dotfiles are Meant to be Forked](https://zachholman.com/2010/08/dotfiles-are-meant-to-be-forked/).

You can find all my aliases (and just my aliases) in a file named `.gitconfig.aliases`. This way, you can add my aliases without having to adopt any of my other peculiar Git settings.

## Installation

But before you start cloning repositories and editing config files, I made an even simpler way to obtain my aliases - just run the following commands in your shell.

```bash
curl -o ~/.gitconfig.aliases https://raw.githubusercontent.com/haacked/dotfiles/main/git/gitconfig.aliases.symlink
git config --global include.path "~/.gitconfig.aliases"
```

The first command downloads my `.gitconfig.aliases` file into your home directory (aka `~/`). The second command adds an include pointing to this file.

__WARNING:__ If you already have an `include` section in your `.gitconfig` file, the previous command will overwrite it. In that case, just edit your `include` section and add another path property with the path to the `gitconfig.aliases` file.

From now on, when I blog a new Git alias, I'll be sure to add it to that file and remind you how to install it.
