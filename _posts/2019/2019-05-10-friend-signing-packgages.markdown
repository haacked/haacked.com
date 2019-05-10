---
title: "Package Author Identity through Social Proofs"
description: "Rather than use certificates, social proofs may be a better approach to establishing the identity of a package author"
tags: [nuget,security,oss]
excerpt_image: https://user-images.githubusercontent.com/19977/57546529-c51a8a80-7311-11e9-9047-117c25f47649.png
---

In my post on [Why NuGet Package Signing Is Not Yet For Me](https://haacked.com/archive/2019/04/03/nuget-package-signing/) I noted...

> as a NuGet consumer, there’s no way, within reason, that I can take advantage of package signing to make my environment more secure. At least not yet.

For the most part, Microsoft implemented package signing in NuGet to comply with its own internal security policies. But for the rest of us, it has little benefit today.

So what would I propose? Perhaps we can get a little help from our friends!

![Cast of Friends](https://user-images.githubusercontent.com/19977/57546529-c51a8a80-7311-11e9-9047-117c25f47649.png)

No, not those friends. Your friends!

## Establishing Identity

Let's back up a step. The purpose of a certificate is to establish identity. Suppose you see a package by some user [`troyhunt` on NuGet](https://www.nuget.org/profiles/troyhunt) (I'm just using Troy as an example here, but it could be anybody).

![Troy Hunt on NuGet could be anybody!](https://user-images.githubusercontent.com/19977/57544685-f80e4f80-730c-11e9-8108-1208e613ada0.png)

Is that the _real_ Troy Hunt? How would you know? Anyone can scrape his photo off the web and create a `troyhunt` account. And if that person uploads a malicious package and gets caught, we don't know anything about them. They can come back as `tr0yhunt` and start all over again.

## Certificates

Certificate signing packages is one way of resolving this issue. If the `troyhunt` on NuGet signs his packages with a certificate, and we verify the certificate, we can establish that it is indeed a real person named Troy Hunt.

1. If you know Troy, you know this is not an imposter.
2. If Troy uploads something bad, NuGet could go after him.

But what if Troy doesn't certificate sign his packages like the vast majority of people on NuGet? Or even if he does, that only validates his packages, not his account. Someone could create a spoof `tr0yhunt` NuGet account and upload a few signed packages from the real `troyhunt` if Troy forgets to reserve the name and publishes in-progress signed packages on MyGet.

That's perhaps an unlikely scenario for Troy, but it could happen to someone else who doesn't get the sequence right.

## Social Proofs

Is there another way we could establish his identity? After all, any random hacker could find his photo on the internet and create an account on NuGet using his name. How do we know he's the REAL Troy Hunt?

Perhaps if there were a way he could verify that the `troyhunt` on NuGet is also [`@troyhunt` on Twitter](https://twitter.com/troyhunt), that would give us more confidence.

![troyhunt on Twitter follows you](https://user-images.githubusercontent.com/19977/57544859-779c1e80-730d-11e9-86c3-9b79bcc5ea91.png)

And perhaps to provide even more assurance, what if he could prove that he's also the [`@troyhunt` on GitHub is the real Troy Hunt](https://github.com/troyhunt)?

![@troyhunt on GitHub](https://user-images.githubusercontent.com/19977/57550806-3d864900-731c-11e9-9e1d-c8a72e4d924f.png)

Then, if you knew and trusted the @troyhunt on GitHub, you could trust that the same person is in control of the `troyhunt` account on NuGet.

This concept is called a social proof. At a simple level, if Troy were to tweet on Twitter that he's `troyhunt` on NuGet, and then he updated his profile on NuGet to note he's `@troyhunt` on Twitter, that would provide a proof that the same person is in charge of both accounts.

That alone doesn't prove he's the real Troy Hunt. To prove that, we rely on other signals or the fact that he's a verified user on Twitter. If we know the `@troyhunt` on Twitter is the real Troy Hunt, through the transitive property (remember math?) of identity, we know that the `troyhunt` on NuGet is also the real Troy Hunt.

## Leveraging Social Proofs

So this seems like a lot of work for the NuGet team to build up all these social proofs. Fortunately, they don't have to. There's already an open source project called [Keybase.io](https://keybase.io/) that does it. Here's a screenshot of the Keybase desktop app that shows many of my social proofs. [I'm haacked on Keybase](https://keybase.io/haacked)

![Haacked on Keybase](https://user-images.githubusercontent.com/19977/57545727-1d508d00-7310-11e9-856c-ac45354a0885.png)

Through Keybase, I've proven I'm `haacked` on just about every site you can think of. You can also see my GPG public key on Keybase. This is the key I use to sign my git commits. For example, [the commit](https://github.com/haacked/haacked.com/commit/30175fa1bba09a4c13dbb3e7b378dfa76f42d1a8) where I added a security.txt file to my blog is signed with this key.

And if we check out [Troy's keybase profile](https://keybase.io/troyhunt), we can see he is indeed all the `troyhunt`s I mentioned earlier.

I can imagine a world where package managers leverage Keybase to provide more secure options for establishing identity. Today, when you publish a package to NuGet.org, NuGet.org signs the package with its own certificate. The public key of that certificate ought to be published on Keybase.io. NPM does this with its own registry key.

With that, I could imagine a policy where I tell the NuGet client, I trust all packages signed by nuget.org from people I follow on GitHub and Twitter. Or, if I want to be extra secure, I may tell NuGet, require that they sign the package too, but allow GPG signing as long as the key is published on keybase.io and associated with their NuGet account.

## Conclusion

People spend a lot of time grooming their online identities. Most people _don't_ spend time looking at each others certificate public keys. If we're concerned about establishing identity on a package manager, we should leverage social proofs.

To be clear, this doesn't solve everything. For example, it's unclear to me how you implement a policy of key rotation with Keybase if people do use GPG to sign keys. And, there's ways for malicious actors to capitalize on these established identities to harm users. I'll talk about that in a future post maybe.

But for now, I'd love to see every package manager leverage social proofs to better establish identity. And let's all hope Troy never turns his talents to evil.
