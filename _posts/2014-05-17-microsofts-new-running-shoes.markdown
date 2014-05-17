---
layout: post
title: "Microsoft's New Running Shoes"
date: 2014-05-17 -0800
comments: true
categories: [microsoft aspnet oss]
---

For a long time Microsoft, and in particular the ASP.NET/Azure Team, has been making improvements in its approach to open source and the developer community. It was never fast enough though. When I was still employed there, the prevailing metaphor that folks like Scott Hanselman and I would use was this idea of "baby steps". To change the momentum of such a large organization would take time and small steps in the right direction.

Here's a snippet from a post Scott wrote five years ago when we [first released ASP.NET MVC 1.0 under a permissive open source license](http://www.hanselman.com/blog/MicrosoftASPNETMVC10IsNowOpenSourceMSPL.aspx):

> These are all baby steps, but more and more folks at The Company are starting to "get it." We won't rest until we've changed the way we do business.

Here's my use of the phrase in my [notes about the release a year prior](http://haacked.com/archive/2008/03/21/a-few-notes-about-the-mvc-codeplex-source-code-release.aspx/).

> As I mentioned before, routing is not actually a feature of MVC which is why it is not included. It will be part of the .NET Framework and thus its source will eventually be available much like the rest of the .NET Framework source. It’d be nice to include it in CodePlex, but as I like to say, baby steps.
 
However, Microsoft's recent remarkable announcements around the next generation of ASP.NET this week have made it clear that they've dispensed with the baby steps and have put on their running shoes.

In summary,

* ASP.NET vNext builds on NuGet as unit of reference instead of assemblies.
* Roslyn-based runtime hackable compilation model.
* Dependency Injection from the ground up.
* No Strong-Naming! (See [this discussion](https://github.com/octokit/octokit.net/issues/405) for the headache strong-naming has been)

But most exciting to me is that all of this is open source, accepts contrtibutions, and [hosted on GitHub](https://github.com/aspnet).

In other areas of Microsoft they released Microsoft Office for the iPad and made Windows free for small devices. It's definitely a new Microsoft.

## How did this come about?

Well breathless headlines would have you believe that [Satya Nadella singehandedly built a new Microsoft in his first three months](http://www.businessinsider.com/nadella-builds-new-microsoft-in-3-months-2014-5). It makes for a good story, but it's clearly wrong. It's lazy thinking.

Look at this contribution graph for [Project K's Runtime](https://github.com/aspnet/KRuntime).

![Project KRuntime commit](https://cloud.githubusercontent.com/assets/19977/3005570/6e0ce8b4-dde8-11e3-8346-61b1437c190f.png)

The [initial commit was on November 7, 2013](https://github.com/aspnet/KRuntime/commit/69df15f0921792f01c80f0fad15d21cc7017b219). Satya become CEO on [February 4, 2014](http://www.microsoft.com/en-us/news/press/2014/feb14/02-04newspr.aspx). So clearly this had been underway for a long time before Satya became CEO. After all, the first commit doesn't mark the beginning of planning.

However, I don't want to take anything away from Satya. While this effort didn't start under him, he definitely is creating the right climate within Microsoft for this effort to thrive. His leadership is setting up these new efforts for success and that's a big deal. He definitely has the right vision for this new Microsoft.

Rather, the effort was more grass roots, albeit with the support of big hitters like Scott Guthrie. In a large part, the focus on the Azure business paved the way for this to happen.

Azure provides an environment where they are not limited to hosting .NET web applications. Azure makes money whether you host ASP.NET, NodeJs, or whatever. This is analogous to how releasing Office for iPad is a sign that Office will no longer help prop up Windows. Windows must live or die on its own merits.

In the same way, ASP.NET must compete on its own merits and to do so requires drastic changes.

In a follow-up post I'd love to delve a little more into the history of the ASP.NET changes and my thoughts on what this means for existing customers and backwards compatibility. If you find this sort of analysis interesting, let me know in the comments. Otherwise I'll go back to blogging about fart jokes and obscure code samples or something.