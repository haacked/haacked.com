---
title: Visualize Git with SeeGit
tags: [git,github,code]
redirect_from: "/archive/2012/03/14/visualize-git-with-seegit.aspx/"
---

I recently gave my first talk on Git and GitHub to [the Dot Net Startup
Group about Git and
GitHub](http://www.dotnetstartup.com/events/51574692/?eventId=51574692&action=detail ".NET Startup Group").
I was a little nervous about how I would present Git. At its core, Git
is based on a simple structure, but that simplicity is easily lost when
you start digging into the myriad of confusing command switches.

I wanted a visual aid that showed off the structure of a git repository
in real time while I issued commands against the repository. So I hacked
one together in a couple afternoons.
[SeeGit](https://github.com/Haacked/SeeGit "SeeGit") is an open source
instructive visual aid for teaching people about git. Point it to a
directory and start issuing git commands, and it automatically updates
itself with a nice graph of the git repository.

[![seegit](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Visualizing-Git-with-SeeGit_11C41/seegit_thumb.png "seegit")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Visualizing-Git-with-SeeGit_11C41/seegit_2.png)

During my talk, I docked SeeGit to the right and my Console2 prompt to
the left so they were side by side. As I issued git commands, the graph
came alive and illustrated changes to my repository. It updates itself
when new commits occur, when you switch branches, and when you merge
commits.

It doesn’t handle rebases well yet due to a bug, but I’m hoping to add
that as well as a lot of other useful features that make it clear what’s
going on.

Part of the reason I was able to write a useful, albeit buggy, tool so
quickly was due to the fantastic packages available on NuGet such as
[LibGit2Sharp](https://github.com/libgit2/libgit2sharp "LibGit2Sharp on GitHub"),
[GraphSharp](http://graphsharp.codeplex.com/ "GraphSharp on CodePlex"),
and
[QuickGraph](http://quickgraph.codeplex.com/ "QuickGraph on CodePlex")
among others. Installing those got me up and running in no time.

I hope to add a nice visual illustration of a rebase soon as well as the
ability to toggle the display of unreachable commits. I hope to use this
in many future talks as a nice way of teaching git. Who knows, it might
become useful in its own right as a tool for developers using Git on
real repositories.

But it’s not quite there yet. If you would like to contribute, I would
love to have some help. And let me know if you make use of this!

If you want to try it out and don’t want to deal with downloading the
source and compiling it, I put together [a zip package with the
application](https://github.com/downloads/Haacked/SeeGit/SeeGitApp.zip "SeeGitApp.zip").
I’ve only tested it on Windows 7 so it might break if you run on XP.

