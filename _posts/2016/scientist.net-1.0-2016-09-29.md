---
layout: post
title: "Scientist.NET 1.0 released!"
date: 2016-09-29 -0800
comments: true
categories: [github csharp dotnet scientist]
---

In the beginning of the year, I [announced a .NET Port](http://haacked.com/archive/2016/01/20/scientist/) of GitHub's [Scientist library](http://githubengineering.com/scientist/). Since then, I and several [contributors from the community](https://github.com/github/scientist.net/graphs/contributors) (kudos to them all!) have been hard at work getting this library to 1.0 status.

Today I released an official 1.0 version of Scientist.NET. I transferred the repository to the [github organization](https://github.com/github/) to make it all official and not just some side-project of mine. So if you want to get involved by logging issues, contributing code, whatever, it's now located at [https://github.com/github/scientist.net](https://github.com/github/scientist.net).

You can install it [via NuGet](https://www.nuget.org/packages/Scientist).

`Install-Package scientist`

You'll note that the actual package version is 1.0.1. Why the patch for the first release? Well I made a mistake and uploaded an early pre-release as 1.0.0 when I didn't mean to. And NuGet doesn't let you overwrite an existing version. Who's fault is that? Well, partly mine. When we first built NuGet, we didn't want people to be able to replace a known good package to help ensure repeatable builds. So while this decision bit me in the butt, I still stand by that decision.

Enjoy!
