---
title: "Maintainer burnout and package security"
description: "At the end of the day, a determined attacker will get a malicious package in the package feed. Sometimes this is enabled by maintainer burnout. So what can we do? How do we mitigate this and provide security in depth?"
tags: [nuget,security,oss]
excerpt_image: https://user-images.githubusercontent.com/19977/58373908-23d32d00-7eea-11e9-8a36-b894d67bbb4a.jpg
---

I ended [my last post on package security through fingerprints](https://haacked.com/archive/2019/05/13/package-fingerprint/) with this ominous note...

> In a future post, I’ll cover how even this wouldn’t protect us from every malicious package. It would do a lot, but there’s always trouble in the water.

Welcome to the future my friends! This is that post. The stream of events I had in mind when I wrote that concerns the [`event-stream` NPM package](https://www.npmjs.com/package/event-stream).

> Normally, streams are only used for IO, but in event stream we send all kinds of objects down the pipe. If your application's input and output are streams, shouldn't the throughput be a stream too?

Based on this description, it sounds a bit like Reactive Extensions for node, but I haven't played around with it to be sure.

### The Event Stream Timeline

Chris Northwood wrote a [fantastic and detailed timeline](https://medium.com/@cnorthwood/todays-javascript-trash-fire-and-pile-on-f3efcf8ac8c7) of this vulnerability. The folllowing is a brief summary.

Sometime in 20189, the maintainer of `event-stream` handed off maintenance of the package to another maintainer. This was a very popular package with around 1.4 million weekly downloads. Not long after, the maintainer added the `flatmap-stream v0.1.0` dependency to `event-stream`. You can [see the commit on GitHub](https://github.com/dominictarr/event-stream/commit/e3163361fed01384c986b9b4c18feb1fc42b8285#diff-b9cfc7f2cdf78a7f4b91a753d10865a2). Seems pretty innocuous, right? At the time, it was.

Sometime later, an NPM user uploads `flatmap-stream v0.1.1` which contained a bit of extra obfuscated code at the end of the minimized code.

Notice that according to the rules of [Semantic Versioning (aka SemVer)](https://semver.org/), this is a patch update to the previous version of the package. This is supposed to indicate that this version only contains bug fixes and is backwards compatible.

If you can get a bit of obfuscated code to run on a million machines, what would you do? Something something Bitcoin of course! In this specific case, the malicious package targeted a specific application, Coinpay. And not just every Coinpay user, but those with 100 Bitcoin (or 1000 Bitcoin Cash) according to [the NPM blog post](https://blog.npmjs.org/post/180565383195/details-about-the-event-stream-incident). To put that in perspective, that's around $800,000 USD. Or if you wait a bit, now it's $500,000. Now it's $1,000,000. Nope, back down to $700,000. Regardless, it's a lot of money.

## The role of burnout

![Tire fire by Stephanus Riosetiawan - CC BY-SA 2.0](https://user-images.githubusercontent.com/19977/58373908-23d32d00-7eea-11e9-8a36-b894d67bbb4a.jpg)

From the details I've read, it's unclear if the extra obfuscated code was in the flatmap-stream repository or not. Would fingerprinting have caught this? Maybe. But for a moment, let's assume that flatmap-stream wasn't backed by a Git repo. Or perhaps it was, but nobody was taking a close look at it. This is a very difficult attack to counter act.

In my post about [establishing package author identities](https://haacked.com/archive/2019/05/10/friend-signing-packgages/), I talk about having well established identities can help guard against malicious packages.

But what I didn't discuss is what happens when a well established identity gets burnt out and hands off their package to someone else? In his own words, the original maintainer of `event-stream` [had this to say](https://gist.github.com/dominictarr/9fd9c1024c94592bc7268d36b8d83b3a)...

> If it's not fun anymore, you get literally nothing from maintaining a popular package.
> ...
> So right now, we are in a weird valley where you have a bunch of dependencies that are "maintained" by someone who's lost interest, or is even starting to burnout, and that they no longer use themselves. You can easily share the code, but no one wants to share the responsibility for maintaining that code. Like a module is like a piece of digital property, a right that can be transferred, but you don't get any benefit owning it, like being able to sell or rent it, however you still retain the responsibility.

For all the mitigations we put in place, nothing protects us from a maintainer either going rogue, or burning out and lending their trusted identity to another maintainer.

The maintainer goes on to suggest a couple of solutions

> I see two strong solutions to this problem...
>
> 1. Pay the maintainers!! Only depend on modules that you know are definitely maintained!
> 2. When you depend on something, you should take part in maintaining it.

The recent announcement of the [GitHub Sponsors program](https://github.com/sponsors) could be a huge step towards the first issue. Especially for critical open source software that so many depend on.

## Where foundations might fit in

Perhaps because I'm on the board of the .NET Foundation, I've been thinking a lot about the role of foundations in helping to mitigate this problem. In particular, there's two areas to consider.

### Maintainer burnout

This is probably a larger problem than we realize. How can foundations help provide support so that maintainers are not burning out. The GitHub Sponsors program could help if it provided the means for a maintainer to work on their project full-time. But for those who still have to maintain a full-time job, there's the potential for it to contribute to burnout as people who contribute feel more entitled to the maintainer's efforts.

### Maintainer succession plans

Perhaps foundations could also provide support to maintainers to provide guidance on succession and help when vetting new maintainers. At the very least, provide this for critical OSS projects. Identify the projects that would harm a huge population of developers if they were compromized. I'm not sure exactly what this looks like yet. This may be an area where GitHub could take a strong lead. GitHub understand which projects everyone depends on. It also has a sense of the reputation for contributors. Maybe they could combine this in some way.

I'll have to think about the implications of this more deeply. Any time you start to walk down this path, you start to face the law of unintended consequences, bad actors gaming the system, etc.

Or even better, maybe I can convince a deep thinker like [Nadia Eghbal](https://nadiaeghbal.com/) to weigh in. I'm ready to write about topics other than package management for a little while. :)

## Security in Depth

The final point to make is that when it comes to package security, all our usual approaches to security in depth are important. Developers are not going to stop using packages. The benefits far outweigh the risk.

At the same time, the risks are high and at the end of the day, a determined attacker is going to get people to download malicious packages.

As consumers, we have to apply good security practices throughout our stacks. That is a whole post (or even book) in its own right, but a few key principles come to mind.

1. __Observability:__ Ensure you have sufficient monitoring and logging so that you can quickly detect anomalous activity in your systems that might indicate a breach.
2. __Isolation:__ Isolate systems so that a breach in one does not spread to another.
3. __Resiliency:__ Build systems that are resilient to bugs and code. This goes hand in hand with isolation, but also includes ensuring that systems are able to run in a degraded state when a dependency is down.
4. __Recover:__ And finally, make sure that you are well practiced in recovery. How long does it take, once you discover a problem, to get your system back into a good recovered state? How often do you practice recovery?

So the answer to the package security threat is not to stop using packages. It's to continue to improve package manager security, while at the same time building up your system and organizational immune system so that a breach doesn't take down your business.