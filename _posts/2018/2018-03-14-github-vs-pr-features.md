---
layout: post
title: "PR information at your fingertips"
date: 2018-03-15 -0800
comments: true
categories: [github VisualStudio pull-request]
---

The Information Industry Association adopted the motto "Putting Information at Your Fingertips" way back in the hazy days of the 1970s. However it was during a 1990 Comdex keynote ([you can watch a scratchy VHS recording of it on YouTube](https://www.youtube.com/watch?v=uGA1Chm_8RE)), when a relatively young Bill Gates articulated a vision to bring that idea to reality.

[![Look at him, so young and hopeful.](https://user-images.githubusercontent.com/19977/37441105-f9b9141a-27bc-11e8-931a-e9520eb717ed.png)](https://www.youtube.com/watch?v=uGA1Chm_8RE)

In the intervening time, that vision has mostly come to fruition...for VIM users. For the rest of us, it's more like information at the end of your mouse clicks. But close enough.

Visual Studio is a salient embodiment of this vision. It contains a rich set of features along with a third-party extension ecosystem such that nearly every task a developer needs to accomplish can be done in the IDE. Tools like IntelliSense provide helpful context while coding.

This creates an environment where developers who use Visual Studio __love__ to stay in Visual Studio. So much so that their friends start to worry about them, like that friend who just went through a rough break up and hasn't left their apartment in weeks. I say that out of love as one who has spent many fond hours churning out code assisted by the helpful embrace of IntelliSense.

### GitHub For Visual Studio

This week, the Editor Tools team at GitHub (the team who brought you the [GitHub Extension for Visual Studio](https://visualstudio.github.com/)) released a [new version](https://github.com/github/VisualStudio/releases/tag/v2.4.3.1737) that contributes to this vision by bringing Pull Requests closer to your fingertips!

The three main features included in this release are:

### Reviewing a PR with submodule changes

Besides rebasing and merge conflicts, submodules may be one of those features that cause the most angst in Git. This feature seeks to reduce that angst. When switching to a PR branch, it brings any submodule changes to the developer's attention. There's even a handy little button to update them for you!

![Submodule status and update  button](https://github-team.s3.amazonaws.com/uploads/general/2169716d-b6ee-4602-95bc-5f65ef193db4.png)

Previously, there was no indication that submodules had changed apart from random build failures and checking Git status on the command line. From there, you would have to sync and update submodules from the command line. This unnecessarily forced developers outside of Visual Studio when doing a GitHub related action (opening a PR).

#### Show current PR on status bar

Prior to this release, you would have to go to the PR list and take an educated guess based on the current branch name to find the active PR:

![Which PR is active?](https://github-team.s3.amazonaws.com/uploads/general/23093919-b465-4fa8-9871-7f55d61dcf4f.png)

![Oh it's this one!](https://github-team.s3.amazonaws.com/uploads/general/b3cc9ca1-24c5-4fc4-863d-a85fd38cc2ff.png)

With this release, you can see which PR you are on directly from the status bar, and navigate to its details by clicking the PR number!

![PR at your fingertips](https://github-team.s3.amazonaws.com/uploads/general/5d5bd62e-68f4-46d0-aaa9-e1950b1b1bff.png)

### Enable navigation from diff view to editor

Finally, this update enables developers to quickly switch from a diff view back into the editor.

When viewing a diff view in Visual Studio, it is a jarring experience for a developer to be _within_ their IDE and not be able to edit (since you can’t edit a diff view). This releaseadds a simple way for developers reviewing a diff to jump right back into coding. All you have to do is press Enter in the file! ([Video here](https://drive.google.com/open?id=1ePiF4FM3hwKejSur1cU7OdjkozLFq83N) for a better view of what this animated gif shows):

![Switching from diff to code](https://github-team.s3.amazonaws.com/uploads/general/09853368-78a1-4385-a133-e5177af74391.gif)

### Shout outs

A special thanks to [Jamie Cansdale](https://twitter.com/jcansdale) (you may know him from such hits as [TestDriven.net](https://www.testdriven.net/)) for working on these three main features and getting them out to you!

Thanks to [Sarah Guthals](https://twitter.com/sarahguthals), the relatively new manager of the Editor Tools team for doing the real work in writing this post. And to

And to the rest of the Editor Tools team for making this release possible. It takes a village! Be sure to follow them on Twitter!

* [sh4na](https://twitter.com/sh4na)
* [iammeaghanlewis](https://twitter.com/iammeaghanlewis)
* [stan_programmer](https://twitter.com/stan_programmer)
* [grokys](https://twitter.com/grokys)

### Busy in 2017

And in case you haven't paid close attention last year, here are some cool features we shipped in 2017.

* Viewing PRs with Diff View ([20 second video](https://drive.google.com/open?id=1OgRg9fyIGGGkPpuY_55XnQIHTh6EX5eF))
* Leaving Inline Comments in Diff View in a PR ([20 second video](https://drive.google.com/open?id=1epqDTACMRT0h5EnmxbD7WKzStsPoydaU))

### Related Posts

* [GitHub Inside Your Visual Studio](https://haacked.com/archive/2015/04/30/github-in-your-visual-studio/)
* [The Open Sourcing of the GitHub Extension for Visual Studio](https://haacked.com/archive/2015/07/20/ghfvs-oss/)
* [Information at your fingertips](https://spectrum.ieee.org/computing/software/information-at-your-fingertips)
