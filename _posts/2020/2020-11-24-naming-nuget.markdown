---
title: "Naming NuGet, A Lesson In Distributed Decision Making"
description: "A look back at the naming of NuGet and the lessons learned about distributed decision making."
tags: [remote,nuget]
excerpt_image: https://user-images.githubusercontent.com/19977/99890425-d4230e00-2c13-11eb-87b9-323745328bc7.png
---

It is notoriously difficult to make decisions in a distributed asynchronous manner. It's hard enough for me to make decisions by myself. Now introduce more people and timezones and you have yourself a hot mess. People tend to meet an online proposal with the silence of indifference. Or the silence that's a result of the bystander effect as everyone waits for someone else to chime in.

Or when people do chime in, the discussion goes off the rails and never leads to a concrete resolution.

One extreme approach to solicit input is to follow Cunningham's Law (named after [Ward Cunningham](https://twitter.com/WardCunningham), the creator of the Wiki) which states,

> the best way to get the right answer on the internet is not to ask a question; it's to post the wrong answer.

Well I'm good at that!

While funny and effective, I don't recommend taking this approach intentionally. Especially not with your team. Working with a team requires trust and collaboration, not techniques that may come across as a tad manipulative. Of course I can't help that I've inadverdently taken this approach many times.

Justin Spahr-Summers describes a more nuanced version of this approach in his post, [Working asynchronously](https://medium.com/@jspahrsummers/working-asynchronously-c4f4acd289ac) where he states,

> People are much more willing to chime in if they disagree with something concrete. So, __instead of asking an open-ended question, assert a direction.__

The point here is not to _dictate_ a direction, but to put forth a concrete proposal and _then_ ask for feedback. If you get expose a proposal, any proposal, early, it gives people an opportunity to provide feedback before it's too late.

In fact, this idea of getting input early is embodied [by how GitHub uses pull requests](https://github.blog/2012-05-02-how-we-use-pull-requests-to-build-github/),

> __Open a Pull Request as early as possible__
>
> Pull Requests are a great way to start a conversation of a feature, so start one as soon as possible- even before you are finished with the code. Your team can comment on the feature as it evolves, instead of providing all their feedback at the very end.

It's important to note that this is not an easy thing to do by any means. Nobody wants to look like a chump who doesn't know what they're doing. Also, it's harder for some people than others because of their circumstances. If you're new to a career or team, you may worry that exposing an idea early will erode trust in your abilities. If you're a member of an underrepresented group, you may receive harsher criticism due to bias. It's incumbent on leaders to create the type of environment where people feel safe exposing ideas early for feedback. Early feedback gets better results.

Why am I discussing this? Well, it started where most of my ideas for blog posts come from these days, with a Twitter discussion. In this case Immo Landwerth, a PM on the .NET team [tweeted](https://twitter.com/terrajobst/status/1329958007271088130),

> People still complain about the .NET Core naming. Just keep in mind that it's named by Microsoft so it's a miracle we didn't call it ".NET Framework without AppDomains, Remoting, and most of WCF but for multiple operating systems as long as you promise to run your cloud on Azure"

I [responded with](https://twitter.com/haacked/status/1330029441758662658),

> One of my crowning achievements is that NuGet wasn’t named Microsoft Visual Package Manager For The Delivery Of Assemblies 2008.

That name would be particularly weird because NuGet was released in 2010, but who's keeping track? Someone replied [with a great question](https://twitter.com/rjpajaron/status/1330091113974292480),

> It was genius move, who named that?

Hmmm, I don't know! So let's go back in time and find out.

__Renaming NuGet__

Picture a cool October in Redmond, WA ten years ago. [NuGet 1.0 was just released](https://haacked.com/archive/2010/10/06/introducing-nupack-package-manager.aspx/), but it was named NuPack at the time. NuPack was an Outercurve Foundation open source project. The OuterCurve Foundation was a precursor to the .NET Foundation.

However, Caltech informed the Outercurve Foundation they had a software package of the same name.

> NUPACK is a growing software suite for the analysis and design of nucleic acid structures, devices, and systems.

You know, the type of thing I do for fun when I'm bored. Who doesn't analyze nucleic acid structures in their free time?

To avoid confusion, the NuPack project team decided to rename NuPack. We put our heads together, brainstormed some names, and asked the public for help in [renaming NuGet](https://haacked.com/archive/2010/10/21/renaming-nupack.aspx/). Here are the proposed names we came up with.

* DotNetPack
* SnapFetch
* OuterPack
* NFetch

Remember Cunningham's Law? You might say we posted a "wrong answer" and boy did we get feedback. The first comment to my blog post was,

> This is a joke right? Those are our choices? I vote you don't change the name, all of those are terrible.

And that was one of the nicer comments.

A naming decision like this has [some unique constraints](https://haacked.com/archive/2010/10/22/naming-is-hard.aspx/) that lead us to propose a short list rather than an open ended request for names.

> In the original announcement, we listed three criteria:
>
> * Domain name available
> * No other project/product with a name similar to ours in the same field
> * No outstanding trademarks on the name that we could find

To be honest, I didn't like any of the names we came up with either. So how did we end up with NuGet, a name I love and prefer to the original name?

As far as I can tell it came from [a comment on my blog](https://haacked.com/archive/2010/10/21/renaming-nupack.aspx/#dsq-747533265) by someone who only identified themselves as GP.

> I like NuGet

Me too! I liked it a lot. We quickly secured the nuget.org domain name (nuget.com was not available and as I write this it's a site that presents the user with these three choices).

![Nugget or Nougat (or nuget.org)](https://user-images.githubusercontent.com/19977/99890425-d4230e00-2c13-11eb-87b9-323745328bc7.png)

I added NuGet to the survery and promoted the hell out of it because I intended to honor the results of the vote, but I disliked the other names.

And eight days later, what GP liked, GP got when [I declared that the winner is NuGet](https://haacked.com/archive/2010/10/29/nupack-is-now-nuget.aspx/).

Looking back, I'm pleased with the result. It was a messy affair and we made many mistakes on the way. But I'm glad that we put out our proposed names for feedback because it resulted in a better name.

I'm reminded of a comment by Paula Hunter (no relation to Scott Hunter), the Outercurve director at the time,

> Naming is tough, and you can’t please everyone, but a year from now, most won’t remember the old name. How many remember Mozilla “Firebird”?

She was right, but I'm still glad it's not called SnapFetch.
