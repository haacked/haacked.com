---
layout: post
title: "GitHub Inside Your Visual Studio"
date: 2015-04-30 -0800
comments: true
categories: [github visualstudio]
---

I heard you liked GitHub, so today [my team put GitHub inside of your Visual Studio](https://github.com/blog/1989-improving-the-github-workflow-for-the-microsoft-community). This has been a unique collaboration with the Visual Studio team. In this post, I'll walk you through installation and the features. I'll then talk a bit about the background for how this came to be.

__If you are attending Build 2015, I'll be giving a demo of this as part of [the talk Martin Woodward and I are giving](http://channel9.msdn.com/Events/Build/2015/3-746) in room 2009__

If you're a fan of video, here's a [video I recorded for Microsoft's Channel 9 site](https://channel9.msdn.com/Series/ConnectOn-Demand/217) that walks through the features. I also recorded an interview with the [.NET Rocks folks](http://www.dotnetrocks.com/default.aspx?showNum=1133) where we have a rollicking good time talking about it.

## Installation

If you have Visual Studio 2015 installed, visit the Visual Studio Extension gallery to download and install the extension. You can use the following convenient URL to grab it: [https://aka.ms/ghfvs](https://aka.ms/ghfvs).

If you haven't installed Visual Studio 2015, you can obtain the installation as part of the installation process. Just make sure to customize the installation.

<img src="https://cloud.githubusercontent.com/assets/19977/6360312/fabb6a96-bc2d-11e4-9998-a39cf86c072e.png" width="365" alt="Customize install" />

This brings up a list of optional components. Choose wisely. Choose GitHub!

<img src="https://cloud.githubusercontent.com/assets/19977/6360313/fabc8a66-bc2d-11e4-9a86-b714b2b3d570.png" width="365" alt="GitHub option" />

This'll install the GitHub Extension for Visual Studio (heretofore shortened to GHfVS to save my fingers) as part of the Visual Studio installation process.

## Login

One of the previous pain points with working with GitHub using Git inside of Visual Studio was dealing with Two-Factor authentication. If you have 2fa set up (and you should!), then you probably ran across this [great post by Kris van der Mast](http://blog.krisvandermast.com/GithubAndVisualStudioAndTwoFactorAuthentication.aspx).

I hope you don't mind Kris, but we've just made your post obsolete.

If you go to the Team Explorer section, you'll see an invitation to connect to GitHub.

<img src="https://cloud.githubusercontent.com/assets/19977/7338185/3e8995d6-ebf9-11e4-9a28-87939c5eb7d1.png" width="320" alt="GitHub Invitation Section" />

Click the "Connect..." button to launch the login dialog. If you've used GitHub for Windows, this'll look a bit familiar.

<img src="https://cloud.githubusercontent.com/assets/19977/7338192/6aae0caa-ebf9-11e4-8c05-03f6ed91e2af.png" width="365" alt="Login Dialog" />

After you log in, you'll see the Two-Factor authentication dialog if you have 2fa enabled.

<img src="https://cloud.githubusercontent.com/assets/19977/7338197/844c91d6-ebf9-11e4-8a67-465a9d65cd3f.png" width="365" alt="2fa dialog" />

Once you log-in, you'll see a new GitHub section in Team Explorer with a button to clone and a button to create.

<img src="https://cloud.githubusercontent.com/assets/19977/7338216/38b84f0c-ebfa-11e4-8bec-7501acf725b1.png" width="320" alt="GitHub Section" />

## Clone

Click the clone button to launch the Repository Clone Dialog. This is a quick way to get one of your repositories (or any repository shared with you), into Visual Studio.

<img src="https://cloud.githubusercontent.com/assets/19977/7338249/fbc308fc-ebfa-11e4-960b-d57af620b4f0.png" width="365" alt="Clone dialog" />

Double click a repository (or select one and click Clone) to clone it to your machine.

## Create

Click the "create" button to launch the Repository Creation Dialog. This lets you create a repository both on your machine and on GitHub all at once.

<img src="https://cloud.githubusercontent.com/assets/19977/7338270/ced65334-ebfb-11e4-8be1-157969909498.png" width="365" alt="Create dialog" />

## Repository Home Page

When you open a repository in Visual Studio that's connected to GitHub (its remote "origin" is a github.com URL), the Team Explorer homepage provides GitHub specific navigation items.

<img src="https://cloud.githubusercontent.com/assets/19977/7338291/404ce28a-ebfc-11e4-9143-3a338381895c.png" width="320" alt="GitHub Repository Home Page" />

Many of these, such as Pull Requests, Issues, and Graphs, simply navigate you to GitHub.com. But over time, who knows what could happen?

## Publish

If you have a repository open that does not have a remote (it's local only), click on the Sync navigation item for the repository and you'll see a new option to publish to Github.

<img src="https://cloud.githubusercontent.com/assets/19977/7338299/868f2c58-ebfc-11e4-9b9e-236d3a2a550a.png" width="320" alt="Publish control" />

## Open in Visual Studio

The last feature is actually a change to GitHub.com. When you log in to the extension for the first time, GitHub.com learns that you have the extension installed. So if you're also logged into GitHub.com, you'll notice a new button under the _Clone in Desktop_ button.

<img src="https://cloud.githubusercontent.com/assets/19977/7338309/ec1f7b40-ebfc-11e4-8cca-2a341b8de0e7.png" width="320" alt="Open in Visual Studio" />

The _Open in Visual Studio_ button launches Visual Studio 2015 and clones the repository to your machine.

## Epilogue

This has been an exciting and fun project to work on with the Visual Studio and TFS team. It required that Microsoft create some new extensibility points for us and helped walk us through getting included in the new optional installation process.

On the GitHub side, Andreia Gaita ([shana on GitHub](https://github.com/shana) and [@sh4na on Twitter](https://twitter.com/sh4na)) and I wrote most of the code, borrowing heavily from GitHub for Windows (GHfW). Andreia provided the expertise, especially with Visual Studio extensibility. I provided moral support, cheerleading, and helped port code over from GHfW.

This collaboration with Microsoft really highlights the New Microsoft to me. When I pitched this project, our CEO asked me why don't we ask Microsoft to include it. Based on my history and battle scars, I gave him several rock solid reasons why that would never ever ever happen. But later, I had an unrelated conversation with my former Microsoft manager (Scott Hunter) who was regaling me with how much commitment the new CEO of Microsoft, Satya Nadella, has with changing the company. Even drastic changes.

So that got me thinking, it doesn't hurt to ask. So I went to a meeting with Somasegar (aka Soma), the Corporate VP of Developer Division and asked him. I'm pretty sure it went something like, "Hey, I don't know if you'd be interested in this crazy idea. I mean, just maybe, only if you're interested, it's no big deal if you don't want to. But, what do you think of including GitHub functionality inside of Visual Studio?" Ok, maybe I didn't downplay it that much, but I wasn't expecting what happened next.

Without hesitation, he said yes! Let's do it! And so here we are, working hard to make using GitHub an amazing and integrated part of working with your code from Visual Studio. Stay tuned as we have big plans for the future.  
