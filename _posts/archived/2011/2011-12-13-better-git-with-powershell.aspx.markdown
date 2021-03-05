---
title: Better Git with PowerShell
tags: [code,git]
redirect_from: "/archive/2011/12/12/better-git-with-powershell.aspx/"
---

*I’m usually not one to resort to puns in my blog titles, but I couldn’t
resist. Git it? Git it? Sorry.*

Ever since we introduced PowerShell into NuGet, I’ve become a big fan. I
think it’s great, yet I’ve heard from so many other developers that they
have no time to try it out. That it’s “on their list” and they really
want to learn it, but they just don’t have the time.

**But here’s the dirty little secret about PowerShell.** This might get
me banned from the PowerShell junkie secret meet-ups (complete with
secret handshake) for leaking it, but here it is anyways. **You don’t
have to learn PowerShell to get started with it and benefit from it!**

Seriously. If you use a command line today, and switch to PowerShell
instead, pretty much everything you do day to day still works without
changing much of your workflow. There might be the occasional hiccup
here and there, but not a whole lot. And over time, as you use it more,
you can slowly start accreting PowerShell knowledge and start to really
enjoy its power. But on your time schedule.

*UPDATE: Before you do any of this, make sure you have Git for Windows
(msysgit) installed. Read my post about [how to get this set up and
configured](https://haacked.com/archive/2011/12/19/get-git-for-windows.aspx "Get Git for Windows").*

There’s a tiny bit of [one time
setup](http://technet.microsoft.com/en-us/library/dd347628.aspx "Set-ExecutionPolicy on TechNet")
you do need to remember to do:

```csharp
Set-ExecutionPolicy RemoteSigned
```

*Note: Some folks simply use `Unrestricted` for that instead of
`RemoteSigned`. I tend to play it safe until shit breaks.*So with that
bit out of the way, let’s talk about the benefits.

Posh-Git
--------

If you do any work with Git on Windows, you owe it to yourself to check
out
[Posh-Git](https://github.com/dahlbyk/posh-git "Posh-Git on GitHub"). In
fact, there’s also Posh-HG for mercurial users and even Posh-Svn for
those so inclined.

Once you have Posh-Git loaded up, your PowerShell window lights up with
extra information and features when you are in a directory with a git
repository.

[![posh-git-info](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Git-Smooth-with-PowerShell_94DB/posh-git-info_thumb.png "posh-git-info")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Git-Smooth-with-PowerShell_94DB/posh-git-info_2.png)

Notice that my PowerShell prompt includes the current branch name as
well as information about the current status of my index. I have 2 files
added to my index ready to be committed.

More importantly though, Posh-Git adds tab expansions for Git commands
as well as your branches! The following animated GIF shows what happens
when I hit the tab key multiple times to cycle through my available
branches. That alone is just sublime.

[![ps-tab-expansion](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Git-Smooth-with-PowerShell_94DB/ps-tab-expansion_thumb.gif "ps-tab-expansion")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Git-Smooth-with-PowerShell_94DB/ps-tab-expansion_2.gif)

Install Posh-Git using PsGet
----------------------------

You’re ready to dive into Posh-Git now, right? So how do you get it?
Well, you could follow all those pesky *directions*on the [GitHub
site](https://github.com/dahlbyk/posh-git "Posh-Git on GitHub"). But
we’re software developers. We don’t follow no stinkin’ list of
instructions. It’s time to AWW TOE  MATE!

And this is where a cool utility named
[PsGet](http://psget.net/ "PSGet") comes along. There are several
implementations of “PsGet” around, but the one I cover here is so dirt
simple to use I cried the first time I used it.

To use posh-git, I only needed to run the following two commands:

```csharp
(new-object Net.WebClient).DownloadString("http://psget.net/GetPsGet.ps1") | iex
install-module posh-git
```

Here’s a screenshot of my PowerShell window running the command. Once
you run the commands, you’ll need to close and re-open the PowerShell
console for the changes to take
effect.[![ps-installing-posh-git](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Git-Smooth-with-PowerShell_94DB/ps-installing-posh-git_thumb.png "ps-installing-posh-git")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Git-Smooth-with-PowerShell_94DB/ps-installing-posh-git_2.png)That’s

Both of these commands are pulled right from the [PsGet
homepage](http://psget.net/ "PsGet Homepage"). That’s it! Took me no
effort to do this, but suddenly using Git just got that much smoother
for me.

Many thanks to [Keith Dahlby](http://solutionizing.net/ "Keith Dahlby")
for Posh-Git and [Mike
Chaliy](http://chaliy.name/ "Mike Chaliy's homepage") for PsGet. Now go
git it!

