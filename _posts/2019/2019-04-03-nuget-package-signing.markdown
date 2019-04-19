---
title: "Why NuGet Package Signing Is Not (Yet) for Me"
description: "An exploration of the NuGet package signing feature."
date: 2019-04-03 -0700 09:05 AM
tags: [nuget,security,oss]
excerpt_image: https://user-images.githubusercontent.com/19977/55196121-ad28e600-516b-11e9-905d-6e1298dd8216.PNG
---

Strap in for a rollicking exploration of the NuGet package signing feature. What is the feature and what is it good for? And does it live up to its purpose? Yes, my friends, I know how to party.

To get reacquainted with my old pal NuGet, I updated some old packages and tweeted about the fun I had doing it. That's when my buddy Oren rained on my parade.

<img width="615" title="Oren: Did you sign it? Phil: Nope" alt="Oren: Did you sign it? Phil: Nope" src="https://user-images.githubusercontent.com/19977/55128835-41466f00-50d2-11e9-999f-d2be1c84ffee.png">

Perhaps my answer was a bit glib. Just a bit.

But if you peer behind the glib "nope", there's an overflowing cauldron of thoughts. These thoughts aren't compressible into a series of tweets.

I captured some of these thoughts in [a blog post I wrote about Trust and NuGet](https://haacked.com/archive/2013/02/19/trust-and-nuget.aspx/) back in 2013 (six years ago!).

> Just to be clear. I’m actually in favor of supporting package signing eventually. But I do not support requiring package signing to make it into the NuGet gallery. And I think there are much better approaches we can take first to mitigate the risk of using NuGet before we get to that point.

Well guess what?! It's six years later and NuGet has package signing. Were my predictions correct?

## The Ubiquity Problem

> Very few people will sign their packages.

To test this hypothesis, I wrote a [.NET Core console app](https://github.com/Haacked/NugetSignChecker). The app scrapes the top community packages from the [nuget.org stats page](https://www.nuget.org/stats/packages). It then checks each package for an author signature and outputs the result.

To be fair, the code doesn't check _every_ package. There are groups of packages that have the same prefix and are closely related to each other. For example, there are many `xunit.*` packages. In this case, I grouped the packages by prefix and took the first one. Here's a screenshot of the end result of running this code against the top 100 packages. Because of the grouping, it just checks 52 unique package groups.

![NuGet Sign Checker Output](https://user-images.githubusercontent.com/19977/55196121-ad28e600-516b-11e9-905d-6e1298dd8216.PNG)

Out of the top 52 unique package groups, eight were signed, which is around 15%. That's better than I expected, but still pretty low. And if you take a close look, some of these "community" packages are Microsoft affiliated. I suspect if I had time to run this against every package in nuget.org, the final percentage would be much much less.

## The Cost Problem

> The problem is this, if you require certificate signing, you’ve just created too much friction to create a package and the package manager ecosystem will dry up and die. Requiring signing is just not an option.
> The reason is that obtaining and properly signing software with a certificate is a costly proposition by its very nature.

It turns out that Digicert offers free certificates to Microsoft MVPs. That's very generous! But that doesn't help the rest of the non-MVP open source developer community (you're all MVPs to me though).

To get a code signing certificate from Digicert costs $474 per year if you buy three years up front. Be sure not to confuse a code signing cert with an SSL cert as I did when I first looked into this. [Oren](https://twitter.com/onovotny) (the one who nerd sniped me down this signing path in the first place) informed me that there's a [special deal for sysdevs](https://www.digicert.com/friends/sysdev/) where you can get a cert for $71 per year.

That's not an exorbitant cost if you live in the US or most parts of Europe, but it's still a cost. And in some parts of the world, that is a lot of money.

Besides the monetary cost, there's the time cost to buy a certificate. In [his blog post about setting up a code signing chain for NuGet](https://natemcmaster.com/blog/2018/07/02/code-signing/), Nate McMaster notes...

> Every vendor is different, but in order to get a certificate from DigiCert, I submitted documents (Verizon phone bill, photocopy of drivers license) and video chatted on Skype with them to prove my identity. This took a few weeks to complete.

There's also the added friction to set up code signing as part of your package deployment process.

> It’s only 122 lines of code, but it took a full weekend and several weeks of waiting on DigiCert to get this all worked out. Hopefully this guide helps you figure out how to set up code signing for your projects.

If you use the same tools as Nate (Azure Key Vault and AppVeyor), then you're in luck. It will probably be quick to set up. But if you want to use other tools, good luck!

So this leaves the question, what benefits do you get for all this effort? What is the end-user experience like now that yu sign your packages?

## The user experience

The default mode for NuGet is to accept all packages. So for most users, package signing makes no difference. If you want to start validating package signatures, run the following command.

```cmd
nuget config -set signatureValidationMode=require
```

This tells NuGet to require package signatures when you install them. If a package is already in your package cache, NuGet will not validate it when you try to install it. I ran into this the first time I experimented with this. You can run the following command to clear your local NuGet cache:

```cmd
nuget locals all -clear
```

Just for fun, I tried to install a package at random from the command line.

```cmd
nuget install NewtonSoft.Json
```

It failed. But this package must be signed, right? Let's look at the relevant error message.

> nuget : NU3034: Package 'Newtonsoft.Json 12.0.1' from source 'https://api.nuget.org/v3/index.json': signatureValidationMode is set to require, so packages are allowed only if signed by trusted signers; however, no trusted signers were specified.

That last bit is the important part. With `signatureValidationMode` set to `require`, NuGet rejects package installation unless the package is signed by a __trusted signer__. Let's take a look at the set of trusted signers on my machine.

```cmd
nuget trusted-signers list
```

This outputs the message, "There are no trusted signers." Looks like my machine has trust issues. Let's remedy that. One approach is to trust the repository a package comes from.

```cmd
nuget trusted-signers Add -Name nuget.org
```

Note that nuget.org is not a URL but the name of a NuGet source. You can see your list of NuGet sources by running:

```cmd
nuget sources
```

Now when I install `NewtonSoft.Json`, it works! But what happens if I try to install [`MagicEightBall`](https://www.nuget.org/packages/MagicEightBall/)? It also succeeds! What gives? I know _that_ package isn't signed because it's mine and I didn't sign it.

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

The package has two signatures - one is an `Author` signature, and the other is a `Repository` signature. NuGet.org signs every package it receives with a `Repository` signature. That's why I was able to install `MagicEightBall`, even though I never signed it, nuget.org signed it with its `Repository` signature when I published it.

Adding nuget.org as a trusted signer means that I trust _every_ package on nuget.org. That seems to defeat the purpose of requiring package signatures. Nuget.org is a big place. Yes, most of the people and packages on nuget.org are fine upstanding citizens. But chances are there are a few bad actors - bad actors who would love for you install their cryptomining package.

And to be fair, this most likely not the intended use case for adding a `Repository` to the `trusted-signers` list. The intent of this feature is to be able to trust an internal feed. For example, your company might have a curated feed of trusted packages. That would be the repository you add to your trusted-signers, not nuget.org.

With that in mind, lets remove nuget.org from the trusted signers list.

```cmd
nuget trusted-signers Remove -Name nuget.org
```

This takes me back to square one with nobody to trust. Rather than trust a repository, I could try to trust a person. In NuGet, you can add a package author to the `trusted-signers` list.

Hmm, where will we find a trustworthy author? How about that author of `Newtonsoft.Json`, [James Newton-King](https://twitter.com/JamesNK)? Just look at his innocent face. That is the face of a trustworthy chap if I ever saw one.

![James Newton-King](https://user-images.githubusercontent.com/19977/55116846-f4e53a00-50a5-11e9-8710-bdaa78edfa43.png)

There are two ways to add an author to your `trusted-signers` list. If you have a package file that they signed on your machine, you can use that.

```cmd
nuget trusted-signers add -Author Newtonsoft.Json.12.0.1.nupkg -Name "James Newton-King"
```

__PRO TIP:__ Adding an author in this way requires that you have the package file (.nupkg) on your machine. The typical way to get this file is to install the package. But if you have package verification turned on, you can't install it and now you have a chicken and egg problem. The good news is you can download a package file using the API.

```
https://api.nuget.org/v3-flatcontainer/newtonsoft.json/12.0.1/newtonsoft.json.12.0.1.nupkg
```

Another way to add a person to the `trusted-signers` list is with their certificate information. I could not find a way to get that information from NuGet.org. I suppose I could ask James for his certificate fingerprint. If he gave it to me, I'd run the following command.

```cmd
nuget trusted-signers add -Name "James Newton-King"
  -CertificateFingerprint "A3AF7AF11EBB8EF729D2D91548509717E7E0FF55A129ABC3AEAA8A6940267641"
```

Now, when I list trusted signers, I see the following:

```cmd
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

Now, let's try and install `MagicEightBall` again to see if it fails. It indeed fails with a message "This package is signed but not by a trusted signer." Just as I expected. Everything is coming up Milhouse.

Hmmm, my spidey sense is tingling. Any time software seems to just work, I get suspicious. This is working too well. James writes so many great packages, let's install one more.

```cmd
nuget install Newtonsoft.Dson
```

Oh no! It failed! How about `Newtonsoft.Json.Schema`? Failed too. It turns out that as I write this, James only signed two of his packages. The others are not author signed. My trust was misplaced. Out with you James!

```cmd
nuget trusted-signers Remove -Name "James Newton-King"
```

Again, here I am with nobody to trust.

I didn't find an option to trust all packages from nuget.org that have a valid author signature. As far as I can tell, you either trust a full repository, or you have to add every author you plan to trust.

To go back to the default behavior of accepting all packages, replace `require` with `accept`.

```cmd
nuget config -set signatureValidationMode=accept
```

## Intended Audience

NuGet package signing is ideal for security hardened enterprise environments. The type of environment where developers are not allowed to install packages from nuget.org. Instead, they must install packages from a curated internal feed that contains a set of blessed packages.

It isn't well suited for the general developer population - developers like me who install packages from nuget.org. Do I really have to add every single person in the world that I trust to the `trusted-signers` list?

And even if I went to all that trouble, I have to hope each trusted author remembers to sign every package they publish. As we saw with James (who I respect and admire), not every author does this. This is too bad, because when I tell NuGet that I trust James, that means I trust any package he uploads to NuGet. I don't care if he remembered to sign it or not. I trust people, not packages. There's no way to express this right now.

There are benefits to package signing as it's implemented today. For example, if a bad actor compromises his nuget.org account, they won't be able to upload packages signed with his signature. So by trusting his signature, I protect myself from this scenario. But in my thinking, if someone compromises his nuget.org account, why wouldn't I assume they've also compromised his certificate?

More likely is maybe James forgets to reserve the Newtonsoft prefix in NuGet and someone spoofs a package from him, this feature might protect me in that case.

It's also possible that he might _let_ another person upload packages using his account. In that case, a package signature might protect me as these packgaes might be signed with another signature. But that assumes he also doesn't share access to his code signing chain. So there is some value in the feature for folks like me, but it's not much.

## Conclusion

All in all, the package signing feature is still young. I anticipate that the NuGet team will iterate on package signing and it will get more and more useful. But as it stands, there's not much reason for me to sign my packages as an independent package author. And as a NuGet consumer, there's no way, within reason, that I can take advantage of package signing to make my environment more secure. At least not yet.

Let me know in the comments or [on Twitter](https://twitter.com/haacked/status/1113477770284126209) if I missed something in my analysis. Maybe there's some feature I didn't notice that would make this way more usable.

So Oren, when you asked me if I signed my packages, and I replied "Nope." This is what I meant by that nope. I may sign some of them anyways, just to understand the experience, but I'm still waiting on that certificate.

## Solutions

In this post, I focused on problems. It's not in my nature to leave it at that. In a follow-up post, I'll propose some ideas to solve these issues.

## UPDATE - Two hours after I originally posted this

> Let me know in the comments or on Twitter if I missed something in my analysis. Maybe there's some feature I didn't notice that would make this way more usable.

Sure enough, I did miss one important feature. Anand Gaurav, a NuGet PM, [replied on Twitter](https://twitter.com/adgrv/status/1113495219939336192) and noted [there's a way to trust NuGet owners](https://blog.nuget.org/20181205/Lock-down-your-dependencies-using-configurable-trust-policies.html#configure-trusted-package-repositories). I missed it because at the time I wrote this post, the `-owners` option wasn't mentioned on the [documentation for `trusted-signers`](https://docs.microsoft.com/en-us/nuget/tools/cli-ref-trusted-signers).

So instead of trusting James's certificate, I could trust his user account on NuGet.

```cmd
nuget.exe trusted-signers add -name NuGet.org -serviceindex https://api.nuget.org/v3/index.json -owners jamesnk
```

Now I can install all of his packages on nuget.org! Fantastic.

This command lets me specify a list of owners. For example,

```cmd
nuget.exe trusted-signers add -name NuGet.org -serviceindex https://api.nuget.org/v3/index.json -owners microsoft;nuget;jamesnk,haacked
```

What happens if we want to add another owner later?

```cmd
nuget.exe trusted-signers add -name NuGet.org -serviceindex https://api.nuget.org/v3/index.json -owners xunit
```

We get an error message stating

> A trusted signer 'Nuget.org' already exists.

So it seems the only way to add more owners is to remove the Nuget.org trusted signer and re-add it with a complete list of everyone on nuget.org that you trust.

The `-owners` option makes this feature more useful, but there's the problem of managing the list of people you trust remains. I'll write a follow-up post soon with more feedback on how I think this feature could be improved.
