---
title: NuGet Needs Your Input
tags: [nuget,oss,code]
redirect_from: "/archive/2011/04/05/nuget-needs-your-input.aspx/"
---

Hi there, it’s time to shine the bat-signal, or better yet, the
NuGet-Signal!

![batman-sending-nuget-signal](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Need-Your-Help_9F1D/batman-sending-nuget-signal_df9c27c8-24bb-414d-88b4-4800ad33c5d0.png "batman-sending-nuget-signal")**The
NuGet community needs your help!** We’re wrestling with some interesting
wide ranging design decisions and we need data to test out our
assumptions and help us make the best possible choices. I won’t go into
too much detail about the specific issue as I don’t want to bias the
results of the following survey. I simply want to gather information
about common practices by answering a set of questions that mostly have
empirical answers.

I think it’s a given that most Visual Studio solutions consist of
multiple projects. What’s I’m not so sure about is how often those
solutions consist of multiple applications?

For example, is it more common for your solution to have a single core
app and all of the other projects support that app?

![multi-project-single-app](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Need-Your-Help_9F1D/multi-project-single-app_1211289e-7934-4f72-b8d3-678ac4bcf85c.png "multi-project-single-app")

Or is it more common to have a solution with two different apps such as
two WinForm apps or two web apps?

![multi-project-multi-app](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Need-Your-Help_9F1D/multi-project-multi-app_26cf64c4-afd6-4a4d-923d-275c3ef85584.png "multi-project-multi-app")

So please answer the following questions:

This survey requires using a browser that supports iframes.

As an example for that last question, here’s the packages folder for a
sample solution I created. It has four packages where there are multiple
versions.

![packages](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Need-Your-Help_9F1D/packages_9c75cf40-9c85-4208-943b-d37b272e125d.png "packages")

Thanks for taking the time to answer these questions. I’ll follow up
later with more details on what we’re working on.

And feel free to elaborate in the comments if you have more to say!

