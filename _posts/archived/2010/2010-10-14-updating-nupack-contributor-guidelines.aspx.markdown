---
title: Updating NuGet Contributor Guidelines
tags: [nuget,code,oss]
redirect_from: "/archive/2010/10/13/updating-nupack-contributor-guidelines.aspx/"
---

A couple days ago I wrote a blog post entitled, [Running Open Source In
A Distributed
World](https://haacked.com/archive/2010/10/12/running-open-source-in-a-distributed-world.aspx "Running OSS in a Distributed World")
which outlined some thoughts I had about how managing core contributors
to an open source project changes when you move from a centralized
version control repository to distributed version control.

The post was really a way for me to probe for ideas on how best to
handle feature contributions. In the post, I asked this question,

> Many projects make a distinction between who may contribute a bug fix
> as opposed to who may contribute a feature. Such projects may require
> anyone contributing a feature or a non-trivial bug fix to sign a
> Contributor License Agreement. This agreement becomes the gate to
> being a contributor, which leaves me with the question, do we go
> through the process of getting this paperwork done for anyone who
> asks? Or do we have a bar to meet before we even consider this?

None other than [Karl
Fogel](http://www.red-bean.com/kfogel "Karl Fogel"), whose book has
served me well to this point, and whose book I was critiquing provided a
great answer,

> One simple way is, just get the agreement from each contributor the
> first time any change of theirs is approved for incorporation into the
> code. No matter whether it's a large feature or a small bugfix -- the
> contributor form is a small, one-time effort, so even for a tiny
> bugfix it's still worth it (on the theory that the person is likely to
> contribute again, and the cost of collecting the form is amortized
> over all the contributions that person ever makes anyway).

So simple I’ll smack myself every hour for a week for not thinking of
it.
![Smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/NuPack-Issues-Up-For-Grabs_7BB8/wlEmoticon-smile_2.png)

Unfortunately, the process for accepting a contributor agreement is not
yet fully automated (the [Outercurve
Foundation](http://outercurve.org "Outercurve Foundation") is working on
it), so we won’t be doing this for small bug fixes. But we will do it
for any feature contributions.

I’ve updated our guide to [Contributing to
NuGet](http://nuget.codeplex.com/wikipage?title=Contributing%20to%20NuPack "Contributing to NuGet")
based on this feedback. I welcome feedback on how we can improve the
guide. I really want to make sure we can make it easy to contribute
while still ensuring the integrity of the intellectual property. Thanks!

