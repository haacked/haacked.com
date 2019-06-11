---
title: "Package Manager Security"
description: "A summary of a new attack and a list of my posts on package manager security"
tags: [nuget,security,oss]
excerpt_image: https://user-images.githubusercontent.com/19977/59305523-420d8c80-8c4f-11e9-86de-5c48576298e9.png
---

It happened [again](https://twitter.com/bcrypt/status/1136714575770816512). A group of hackers targeted another cryptocurrency wallet via a malicious NPM package. The good news is that [this attempt was foiled](http://blog.npmjs.org/post/185397814280/plot-to-steal-cryptocurrency-foiled-by-the-npm).

> Yesterday, the npm, Inc. security team, in collaboration with Komodo, helped protect over $13 million USD in cryptocurrency assets as we found and responded to a malware threat targeting the users of a cryptocurrency wallet called Agama.

![Guards](https://user-images.githubusercontent.com/19977/59305523-420d8c80-8c4f-11e9-86de-5c48576298e9.png)

The bad news is this is just the attempt we know about.

This attack is a variant of the `event-stream` attack I wrote about.

> The attack was carried out by using a pattern that is becoming more and more popular; publishing a “useful” package (`electron-native-notify`) to npm, waiting until it was in use by the target, and then updating it to include a malicious payload.

With `event-stream`, the attackers took over an existing popular package. In this situation, they created a package that then became popular. In effect, it's a honeypot malicious package.

This seems like a difficult attack to pull off. Not only does the attacker have to write a package that is useful. The attacker has to make the package grow in popularity. It's competing against hundreds of thousands of packages on NPM. On top of that, the attackers have to hope the target open source application makes use of the package.

[Stronger identities](https://haacked.com/archive/2019/05/10/friend-signing-packages/) with reputation systems might have helped in this situation. I don't know enough details to be sure. I haven't heard whether the attackers impersonated well known authors or simply spent the effort to become well-known. Or maybe package consumers install software from unknown authors at a clip way more than I expected.

Package Manager security is a topic that's been on my mind lately. I wrote this post to consolidate my writings on this topic.

* __[Trust and Nuget](https://haacked.com/archive/2013/02/19/trust-and-nuget.aspx/)__ _Feb 2013_ - Discusses some ideas specific to determining trust on NuGet (a package manager for the .NET ecosystem). The ideas are not specific to NuGet.
* __[The Problem of Package Manager Trust](https://haacked.com/archive/2018/11/28/package-manager-trust/)__ _Nov 2018_ - Some thoughts about the `event-stream` incident and some high level ideas on what package manager authors and the overall software community should do about it.
* __[Why NuGet Package Signing Is Not (Yet) for Me](https://haacked.com/archive/2019/04/03/nuget-package-signing/)__ _Apr 2019_ - A critique of the NuGet package signing feature. Certificate signing seems promising, but it's useless if nobody does it. Spoiler alert, nobody does it.
* __[Package Author Identity through Social Proofs](https://haacked.com/archive/2019/05/10/friend-signing-packages/)__ _May 2019_ - An alternative approach to certificate signing for establishing identity through social proofs.
* __[Better Security Through Package Fingerprints](https://haacked.com/archive/2019/05/13/package-fingerprint/)__ _May 2019_ - An exploration of package fingerprinting as one method of reducing risk with packages. This would provide a way to ensure that a package in a package repository matches the source code on GitHub.
* __[Maintainer burnout and package security](https://haacked.com/archive/2019/05/28/maintainer-burnout/)__ _May 2019_ - This post draws the conclusion that no matter what we do, malicious packages will find their way in repositories and possibly on our machines. The answer to that is security-in-depth and being quick to respond and repair.

I'm pretty done with this topic for the moment, but if I ever write about it again, I'll add the post to this list.
