---
title: SemVer, NuGet, and Nightly Builds
redirect_from:
- "/archive/2011/10/26/semver-nuget-nightly-builds.aspx/"
- "/archive/2011/10/23/semver-nuget-nightly-builds.aspx/"
tags: [versioning,semver,nuget]
---

Recently, a group of covert ninjas within my organization started to
investigate what it would take to change our internal build and
continuous integration systems (CI) to take advantage of
[NuGet](http://nuget.org/ "NuGet Website") for many of our products,
**and I need your input!**

[![Hmm, off by one error slays me again. -Image from Ask A Ninja. Click on
the image to visit.](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/SemVer-and-Nightly-Builds_936F/AskANinja11_f5c89cab-0701-49d8-b269-8b22b93143ac.jpg "AskANinja11")](http://blip.tv/askaninja "Ask a Ninja")

Ok, they’re not really covert ninjas, that just sounds much cooler than
a team of slightly pudgy software developers. Ok, they’ve told me to
speak for myself, they’re in great shape!

In response to popular demand, we changed our minds and decided to
support [Semantic Versioning
(SemVer)](http://semver.org/ "Semantic Versioning") as the means to
specify pre-release packages for the next version of NuGet (1.6).

In part, this is the cause of the delay for this release as it required
extensive changes to NuGet and the
[NuGet.org](http://nuget.org/ "NuGet Gallery") gallery. I will write a
blog post later that covers this in more detail, but for now, you can
read [our spec on
it](http://nuget.codeplex.com/wikipage?title=Pre-Release%20Packages "Pre-release packages")
which is mostly up to date. I hope.

I’m really excited to change our own build to use NuGet because it will
force us to [eat our own
dogfood](http://en.wikipedia.org/wiki/Eat_one%27s_own_dog_food "Eating your own dog food")
and feel the pain that many of you feel with NuGet in such scenarios.
Until we feel that pain, we won’t have a great solution to the pain.

A really brief intro to SemVer
------------------------------

You can read the [SemVer spec here](http://semver.org/ "SemVer"), but in
case you’re lazy, I’ll provide a brief summary.

SemVer is a convention for versioning your public APIs that gives
meaning to the version number. Each version has three parts,
*Major.Minor.Patch*.

In brief, these correspond to:

-   **Major:** Breaking changes.
-   **Minor:** New features, but backwards compatible.
-   **Patch:** Backwards compatible bug fixes only.

Additionally, pre-release versions of your API can be denoted by
appending a dash and an arbitrary string after the *Patch number*. For
example:

-   1.0.1-**alpha**
-   1.0.1-**beta**
-   1.0.1-**Fizzlebutt**

When you’re ready to release, you just remove the pre-release part and
that version is considered “higher” than all the pre-release versions.
The pre-release versions are given precedence in alphabetical order
(well technically
[lexicographic](http://en.wikipedia.org/wiki/Lexicographical_order "Lexicographic")
ASCII sort order).

Therefore, the following is an example from lowest to highest versions
of a package.

-   1.0.1-alpha
-   1.0.1-alpha2
-   1.0.1-beta
-   1.0.1-rc
-   1.0.1-zeeistalmostdone
-   1.0.1

How NuGet uses SemVer
---------------------

As I mentioned before, I’ll write up a longer blog post about how SemVer
figures into your package. For now, I just want to make it clear that if
you’re using 4-part version numbers today, your packages will still work
and behave as before.

It’s only when you specify a 3-part version with a version string that
NuGet gets strict about SemVer. For example, NuGet allows **1.0.1-beta**
but does not allow **1.0.1.234-beta.234**.

How to deal with nightly builds?
--------------------------------

So the question I have is, how do we deal with nightly (or continous)
builds?

For example, suppose I start work on what will become **1.0.1-beta**.
Internally, I may post nightly builds of 1.0.1-beta for others in my
team to use. Then at some point, I’ll stamp a release as the official
1.0.1-beta for public consumption.

The problem is, each of those builds need to have the package version
incremented. This ensures that folks can revert back to a
last-known-good nightly build if a problem comes up. SemVer doesn’t seem
to address how to handle internal nightly (or continuous) builds. It’s
really focused on public releases.

Note, we’re thinking about this for our internal setup, not for the
public gallery. I’ll address that question later.

We had a few ideas in mind.

### Stick with the previous version number and change labels just before release

The idea here is that when we’re working on 1.0.1beta, we version the
packages using the alpha label and increment it with a build number.

-   1.0.1-alpha (*public release*)
-   1.0.1-alpha.1 (*internal build*)
-   1.0.1-alpha.2 (*internal build*)

A variant of this approach is to append the date (and counter) in number
format.

-   1.0.1-alpha (*public release*)**
-   1.0.1-alpha.20101025001 (*internal build*)
-   1.0.1-alpha.20101026001 (*internal build on the next day*)
-   1.0.1-alpha.20101026002 (*another internal build on the same day*)

With this approach, when we’re ready to cut the release, we simply
change the package to be 1.0.1-beta and release it.

The downside of this approach is that it’s not completely clear that
these are internal nightly builds of what will be 1.0.1-beta. They could
be builds of 1.0.1-alpha2.

Yet another variant of this approach is to name our public releases with
an even Minor or Patch number and our internal releases with an odd one.
So when we’re ready to work on 1.0.2-beta, we’d version the package as
1.0.1-beta. When we’re ready to release, we change it to 1.0.2-beta.

### Have a separate feed with its own versioning scheme.

Another thought was to simply have a completely separate feed with its
own versioning scheme. So you can choose to grab packages from the
stable feed, or the nightly feed.

In the nightly feed, the package version might just be the date.

-   2010.10.25001
-   2010.10.25002
-   2010.10.25003

The downside of this approach is that it’s not clear at all what release
version these will apply to. Also, when you’re ready to promote one to
the stable feed, you have to move it in there and completely change the
version number.

### Support an optional Build number for Semantic Versions

For NuGet 1.6, you can still use a four-part version number. But NuGet
is strict if the version is clearly a SemVer version. For example, if
you specify a pre-release string, such as 1.0.1-**alpha**, NuGet will
not allow a fourth version part.

But we had the idea that we could *extend* SemVer to support this
concept of a build number. This might be off by default in the public
NuGet.org gallery, but could be something you could turn on
individually. What it would allow you to do is continue to push new
builds of 1.0.1alpha with an incrementing build number. For example:

-   1.0.1-beta.0001 (nightly)
-   1.0.1-beta.0002 (nightly)
-   1.0.1-beta.0003 (nightly)
-   1.0.1-beta (public)

Note that unlike a standard 4-part version, 1.0.1-beta is a higher
version than 1.0.1-beta.0003.

While I’m hesitant to suggest a custom extension to SemVer, it makes a
lot of sense to me. After all, this would be a convention applied to
internal builds and not for public releases.

Question for you
----------------

So, do you like any of these approaches? Do you have a better approach?

While I love comments on my blog, I would like to direct discussion to
the [NuGet Discussions
page](http://nuget.codeplex.com/discussions/277189). I look forward to
hearing your advice on how to handle this situation. Whatever we decide,
we want to bake in first-class support into NuGet to make this sort of
thing easier.
