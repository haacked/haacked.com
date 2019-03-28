---
title: "NuGet Package Signing"
description: "..."
date: 2019-02-19 -0800 09:52 AM PDT
tags: [nuget,security]
excerpt_image: 
---

Some of my NuGet packages are woefully out of date. I had fun updating one recently when my friend Oren on Twitter asked if I signed it.

<img width="615" alt="Oren: Did you sign it? Phil: Nope" src="https://user-images.githubusercontent.com/19977/55128835-41466f00-50d2-11e9-999f-d2be1c84ffee.png">

Perhaps my answer was a bit glib. If you peer behind that "nope", there is a boiling cauldron of thoughts that just can't be compressed into a series of tweets. But they can be written down in this blog post.

I put a lot of these thoughts down in 2013 (six years ago!) when I wrote about [Trust and NuGet](https://haacked.com/archive/2013/02/19/trust-and-nuget.aspx/). The post [contains an addendum](https://haacked.com/archive/2013/02/19/trust-and-nuget.aspx/#addendum-package-signing-is-not-the-answer) where I laid out my case for why package signing is not the answer.

> Just to be clear. I’m actually in favor of supporting package signing eventually. But I do not support requiring package signing to make it into the NuGet gallery. And I think there are much better approaches we can take first to mitigate the risk of using NuGet before we get to that point.

Well guess what? It's six years later and NuGet has package signing. Were my predictions correct?

## The Ubiquity Problem

> Very few people will sign their packages.

So, did this pan out? I decided to investigate. I wrote a .NET Core console app that scrapes the top community packages from the [nuget.org stats page](https://www.nuget.org/stats/packages) checks each one for an author signature.

In the case where there were multiple packages with the same prefix (such as the bajillion `xunit.*` packages) I just grouped them and took the first one. Here's a screenshot of the end result of running this code against the top 100 packages. Because of the grouping, it's only checking 52 unique package groups.

![NuGet Sign Checker Output](https://user-images.githubusercontent.com/19977/55196121-ad28e600-516b-11e9-905d-6e1298dd8216.PNG)

Out of the top 52 unique package groups, only eight were signed, which is around 15%. That's actually better than I expected, but still pretty low. And if you look carefully, you'll see that some of these "community" packages are actually Microsoft affiliated. I suspect if I had time to run this against every package in nuget.org, the final percentage would be much much less.

## The Cost Problem

> The problem is this, if you require certificate signing, you’ve just created too much friction to create a package and the package manager ecosystem will dry up and die. Requiring signing is just not an option.
> The reason is that obtaining and properly signing software with a certificate is a costly proposition by its very nature.

It turns out that Digicert offers free certificates to Microsoft MVPs. That's very generous! But that doesn't help the rest of the non-MVP open source developer community (you're all MVPs to me though).

To get a code signing certificate (not to be confused with an SSL certificate) from Digicert costs $474 per year if you buy three years up front (it's $499 if you buy for one year). However, [Oren](https://twitter.com/onovotny) (the one who nerd sniped me down this signing path in the first place) noted that there's a [special deal for sysdevs](https://www.digicert.com/friends/sysdev/) where you can get a cert for only $71 per year (again, if you buy for three years, though it's only $74 for one year).

That's not an exorbitant cost if you live in the US or most parts of Europe, but it's still a cost. And in some parts of the world, that is a lot of money.

In addition to the monetary cost, there's the time cost to obtain a certificate. In [his blog post about setting up a code signing chain for NuGet](https://natemcmaster.com/blog/2018/07/02/code-signing/), Nate McMaster notes...

> Every vendor is different, but in order to get a certificate from DigiCert, I submitted documents (Verizon phone bill, photocopy of drivers license) and video chatted on Skype with them to prove my identity. This took a few weeks to complete.

And then there's the added friction of setting up code signing as part of your package deployment process.

> It’s only 122 lines of code, but it took a full weekend and several weeks of waiting on DigiCert to get this all worked out. Hopefully this guide helps you figure out how to set up code signing for your projects.

Lucky for you, if you plan to use the same tools as Nate (Azure Key Vault and AppVeyor), then it might not take you as long to get it set up. But if you want to use other tools, good luck!

So this leaves the question, what benefits do you get for all this effort? What is the end-user experience like now that yu sign your packages?

## User Experience

The default mode for NuGet is to accept all packages. So for most users, package signing makes zero difference. However, if security is priority number one, you can choose to require that package signatures are validated. If you want to start validating signatures, run the following command.

```cmd
nuget config -set signatureValidationMode=require
```

This tells NuGet to start validating package signatures when you install them. It's important to note that if a package is already in your package cache, NuGet will not validate it when you try and install it. I ran into this the first time I experimented with this. You can run the following command to clear your local NuGet cache:

```cmd
nuget locals all -clear
```

Just for fun, I tried to install a package at random from the command line.

```cmd
nuget install NewtonSoft.Json
```

It failed. But certainly this package must be signed, right? Let's look at the relevant error message.

> nuget : NU3034: Package 'Newtonsoft.Json 12.0.1' from source 'https://api.nuget.org/v3/index.json': signatureValidationMode is set to require, so packages are allowed only if signed by trusted signers; however, no trusted signers were specified.

That last bit is the important part. With `signatureValidationMode` set to `require`, I may only install packages by trusted signers. Let's take a look at the set of trusted signers on my machine.

```cmd
nuget trusted-signers list
```

This outputs the message, "There are no trusted signers." Right now, NuGet on my machine trusts nobody like it's [Cashmere Cat with Selena Gomez](https://www.youtube.com/watch?v=1Vn1BXfsd4Q).

Great, so how do I trust this package? Well one way is to specify that I trust the repository it comes from.

```cmd
nuget trusted-signers Add -Name nuget.org
```

Note that nuget.org is not a URL but the name of a NuGet source. You can see your list of NuGet sources by running:

```cmd
nuget sources
```

Now when I install `NewtonSoft.Json`, it works! But what happens if I try and install [`MagicEightBall`](https://www.nuget.org/packages/MagicEightBall/)? It also succeeds! What gives? I know that package isn't signed because it's one of mine and I certainly didn't sign it.

## NuGet Package Signatures

This is where we need to delve a little deeper into how NuGet package signatures work. If you have a package file (a file with the .nupkg extension) on your machine, you can take a look at its signatures. For example, here's how you might view the signatures on `Newtonsoft.Json.12.0.1.nupkg`.

```cmd
nuget verify -Signatures Newtonsoft.Json.12.0.1.nupkg
```

This will spit out a lot of information. You can see the full output in [this gist if you're curious](https://gist.github.com/Haacked/83759fd3b722066623ff4e39b0d87b89). Below, I'll just post the most relevant part for this exploration.

```
...
Signature type: Author
...
Signature type: Repository
...
```

The package is signed with two signatures, one is an `Author` signature, and the other is a `Repository` signature. Every package published to nuget.org is signed with a `Repository` signature by nuget.org. That's why I was able to install `MagicEightBall`.

If the goal of requiring package signatures is to increase security, trusting all of NuGet.org is probably not a great idea. Nuget.org is a big place. Most of the people and packages on nuget.org are fine upstanding citizens. But there are almost certainly a few bad actors just waiting for you to install their cryptomining package. Trusting all of nuget.org is probably not what you want, as it defeats the purpose of package signing.

And to be fair, I don't think this is the intent of adding a Repository to the `trusted-signers` list. The intent is to allow you to add a trusted repository source such as an internal company NuGet feed or your own private MyGet feed as a trusted signer.

With that in mind, the following command removes nuget.org from the trusted signers list.

```cmd
nuget trusted-signers Remove -Name nuget.org
```

This takes us back to square one with nobody to trust. Rather than trust a repository, maybe I could trust a person. In NuGet, you can add a package author to the `trusted-signers` list.

Hmm, where will we find a trustworthy author? How about that author of `Newtonsoft.Json`, [James Newton-King](https://twitter.com/JamesNK)? Just look at his innocent face. That is most definitely the face of a trustworthy chap.

![James Newton-King](https://user-images.githubusercontent.com/19977/55116846-f4e53a00-50a5-11e9-8710-bdaa78edfa43.png)

There are two ways to add an author to your `trusted-signers` list. If you have a package file that they signed on your machine, you can use that with the following command.

```cmd
nuget trusted-signers add -Author Newtonsoft.Json.12.0.1.nupkg -Name "James Newton-King"
```

__PRO TIP:__ Suppose you don't have the file and you can't install it because you have package verification turned on, you can download a package file using the API.

```
https://api.nuget.org/v3-flatcontainer/newtonsoft.json/12.0.1/newtonsoft.json.12.0.1.nupkg
```

The other way to add a person to your `trusted-signers` list is to use their certificate information. I am not aware of a means of getting that information from NuGet.org. You'd have to find that information from the individual themself or you can obtain it from a signed package that they signed. But here's the syntax just in case.

```cmd
nuget trusted-signers add -Name "James Newton-King"
  -CertificateFingerprint "A3AF7AF11EBB8EF729D2D91548509717E7E0FF55A129ABC3AEAA8A6940267641"
```

Now, when I list trusted signers, I see the following:

```
Registered trusted signers:

 1.   James Newton-King [author]
      Certificate fingerprint(s):
        SHA256 - A3AF7AF11EBB8EF729D2D91548509717E7E0FF55A129ABC3AEAA8A6940267641
```

It feels good to have someone to trust. Let's install another package that James created (if you're following along at home, don't forget to clear the NuGet cache).

```cmd
nuget install Newtonsoft.Json.Bson
```

Success! Now we're cooking!

Now, let's try and install `MagicEightBall` again. It fails with a message "This package is signed but not by a trusted signer." Exactly as I expected. Everything is coming up Milhouse.

Hmmm, my spidey sense is tingling. Any time software seems to just work, I get suspicious. This is working too well. James writes so many great packages, let's install one more.

```cmd
nuget install Newtonsoft.Dson
```

Oh no! It failed! How about `Newtonsoft.Json.Schema`? Failed too. It turns out that as I write this, James only signed two of his packages. The others are not author signed. My trust was misplaced. Out with you James!

```cmd
nuget trusted-signers Remove -Name "James Newton-King"
```

Again, here I am with nobody to trust.

To go back to the default behavior of accepting all packages, replace `require` with `accept`.

```cmd
nuget config -set signatureValidationMode=accept
```

## Conclusions

I wasn't able to find a way to trust all packages from nuget.org that have a valid author signature. As far as I can tell, you either trust a full repository, or you have to add every author you plan to trust.

This might be great if you're in a hardened Enterprise environment with a private NuGet feed that only contains the packages you're allowed to use. But for the rest of us, this doesn't seem very usable or scalable.

How can I, as an independent developer, gain the benefit of the extra security of package signing? Do I really have to add every single person in the world that I trust to the `trusted-signers` list?

And even if I do, I have to hope that they remembered to sign every package they publish. As we saw with James (who I respect and admire), not every author does this. This is too bad, because when I state that I trust James, that means I trust any package he uploads to NuGet. There's no way to express this right now that I know of. I don't care if he remembered to sign it or not. I trust people, not packages.

Some might point out that if a bad actor compromises his nuget.org account, they won't be able to upload signed packages. So by trusting his signature, I protect myself from this scenario. But in my thinking, if someone compromises his nuget.org account, why wouldn't I assume they've also compromised his certificate?

All in all, the package signing feature is still young. I anticipate that the NuGet team will iterate on package signing and it will get more and more useful. But as it stands, there's really not much reason for me to sign my packages as an independent package author. And as a NuGet consumer, there's really no way I can realistically take advantage of package signing to make my environment more secure. At least not yet.

Let me know in the comments if I missed something in my analysis. Maybe there's some feature I didn't notice that would make this way more usable.

So Oren, when you asked me if I signed my packages, and I replied "Nope." This is what I meant by that nope.