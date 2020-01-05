---
title: "Better Security Through Package Fingerprints"
description: "How do we ensure that a package matches the code in a GitHub repository and does not have any extra surprises injected into it? This post proposes an idea worth more investigation to see if it's a viable option."
tags: [nuget,security,oss]
excerpt_image: https://user-images.githubusercontent.com/19977/71785414-0f90fb80-2fb4-11ea-946a-7ebe3a805e77.jpg
---

It seemed like an innocuous enough update. Someone yanked `bootstrap-sass` ruby gem version 3.2.0.2 and published 3.2.0.3. Ruby gems more or less follows the [SemVer versioning scheme](https://semver.org/) (albeit with an extra version number). An increment of the patch number communicates that this release should be a safe bug fix update. The command, `bundle update --patch`, should be safe as it updates to the next patch version which should be safe.

Only, in this case, it was not. Version 3.2.0.3 of `bootstrap-sass` contained a remote execution vulnerability. Fortunately, a sharp-eyed [Derek Barnes](https://github.com/dgb) noticed something was askance and [opened an issue](https://github.com/twbs/bootstrap-sass/issues/1195). He noticed there was some code in the gem that was not in the GitHub repository. This code created a remote execution exploit. For more detail about what happened, check out [this write-up on the Snyk blog](https://snyk.io/blog/malicious-remote-code-execution-backdoor-discovered-in-the-popular-bootstrap-sass-ruby-gem/).

## Code Provenance

[My last post](https://haacked.com/archive/2019/05/10/friend-signing-packgages/) discussed how verified identities could help avoid malicious packages. The `bootstrap-sass` vulnerability reminds us that bad actors can co-opt identities. In the case of `bootstrap-sass`, the Snyk post notes...

> We assume that the attacker has obtained the credentials to publish the malicious RubyGems package from one of the two maintainers, but this has not been officially confirmed.

I've written a lot about how [package signing is not the solution](https://haacked.com/archive/2019/04/03/nuget-package-signing/). In this case, it's ironic, because package signing could have helped. We assume the attackers only obtained the credentials to rubygems.org. In that case, they could not upload a package with a proper signature.

Of course, this only helps if everyone who installs the gem verifies its signature. It also assumes that the attackers didn't also steal the signing key.

In practice, almost nobody signs their gems as [vcsjones noted on Twitter](https://twitter.com/vcsjones/status/920042541587853312). If nobody signs packages, nobody bothers to verify them.

## Matching Package to Repository

There's another approach that could help mitigate this sort of attack. Imagine that we have a way to confirm that an uploaded package matches a commit SHA on [GitHub](https://github.com/). The registry could flag that the malicious package had extra code not on GitHub and reject it. How useful would that be!?

What about the [GitHub Package Registry](https://github.blog/2019-05-10-introducing-github-package-registry/) (GPR)? Does GPR address this scenario?

It's a step in the right direction, but it doesn't solve this problem yet. The best way to think about GPR is it's a _per-repository_ package registry. It's a place to put your nightly builds. It's less like NuGet and NPM, which provide a feed for all packages, and more like MyGet, with its per-package feed. 

GPR can associate a commit to the package in the feed, but at this point, it is a weak association. GPR does not confirm that a package matches the code in the repository. It's still possible for a build process to insert all sorts of code into a package before publishing it.

## Fingerprinting Package to Commit SHA

I've talked to [Phani Rajuyn](https://twitter.com/PhaniRajuyn/) about this problem in the past. Phani is the lead developer for GPR. He proposed an interesting idea to fingerprint packages against code. Imagine we could generate a fingerprint from a package. And then imagine that we could match that fingerprint up with a fingerprint from the source code. We'd have a deterministic way to know that the code for a commit SHA built a package.

This is easier said than done. To even begin to make this work, we'd need deterministic builds of packages. This means that given the same set of inputs, you always get the same set of outputs.

![Fingerprint scanner by Mike MacKenzie www.vpnsrus.com](https://user-images.githubusercontent.com/19977/71785414-0f90fb80-2fb4-11ea-946a-7ebe3a805e77.jpg)

In the .NET world, the [C# compiler supports deterministic builds](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/deterministic-compiler-option). It might not be obvious why a build isn't always determinstic. The documentation for the `-deterministic` flag explains...

> By default, compiler output from a given set of inputs is unique, since the compiler adds a timestamp and a GUID that is generated from random numbers. You use the `-deterministic` option to produce a deterministic assembly, one whose binary content is identical across compilations as long as the input remains the same.

Unfortunately, NuGet does not yet support deterministic packages. But [there's an open pull request to add it](https://github.com/NuGet/NuGet.Client/pull/2775).

Let's assume NuGet gets the ability to create deterministic builds. Is this enough to start fingerprinting packages against source? In theory, yes! If we had a simple fingerprint scheme that we could generate from code and match to a package, this could work.

In practice, it would only work against the most simple packages. It would be very fragile. Any custom build step could break the fingerprint. For example, some packages might have a post-build step such as IL rewriting. Others might inject content from another repository.

Another approach would be to run the full build process to generate the fingerprint. But this takes us back to square one. A malicious build could have custom steps that inject bad code.

I'm excited about the idea of fingerprinting, but more research is necessary to see if it's a viable option. It may turn out that the simple fingerprint approach works for 99% of packages. If that were the case, then it could be worth implementing it.

## Trusted Builds

Another approach would be some sort of trusted build process. In the case of NuGet, a typical package only needs `csc.exe` and `nuget pack` to build it. What if the way to publish a package was to publish a GitHub repository URL to NuGet. NuGet then runs a build in a restricted environment. If the build files have any custom scripts or requires non-approved tools, it fails.

Or, NuGet could accept a package if it's built by a trusted build provider. For example, I assume NuGet trusts GitHub. Imagine a trusted GitHub Action for building verified NuGet packages. When you use GPR to build such a package, GPR signs the package. NuGet could then receive these signed package without having to reverify them. The same process could apply to NPM and RubyGems.

This doesn't help in the case where there are custom build steps. In that case, we could consider a review process for any custom steps to maintain validation. The closer you stay to a vanilla build, the less custom review is necessary.

There would also need to be some sort of random audit process in place. A separate service would select packages at random from trusted build providers. It would then review the build and packages to ensure the provider is still trustworthy.

All this would be an immense service to the software world. It would help mitigate this entire class of malicious packages.

To be clear, I am not proposing a specific design. I'm not even sure if this idea is workable. It's an idea I've been bandying around that warrants more exploration. What do you think?

In a future post, I'll cover how even this wouldn't protect us from every malicious package. It would do a lot, but there's always trouble in the water.
