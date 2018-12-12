---
title: Changing A Strong Name Is A Major Breaking Change
date: 2012-02-16 -0800
disqus_identifier: 18846
categories:
- open source
- nuget
- code
redirect_from: "/archive/2012/02/15/changing-a-strong-name-is-a-major-breaking-change.aspx/"
---

Recently, the Log4Net team released log4net 1.2.11 (*congrats by the
way!*). The previous version of log4Net was 1.2.10.

Despite which [version of
*version*](https://haacked.com/archive/2006/09/27/Which_Version_of_Version.aspx "Which version of version")
you subscribe to, we can all agree that only incrementing the third part
of a version indicates that the new release is a minor update and one
that hopefully has no breaking changes. Perhaps a bug fix release.

This is especially true if you subscribe to [Semantic Versioning
(SemVer)](http://semver.org/ "SemVer") as NuGet does. As [I wrote
previously](https://haacked.com/archive/2011/10/24/semver-nuget-nightly-builds.aspx "SemVer"),

> SemVer is a convention for versioning your public APIs that gives
> meaning to the version number. Each version has three parts,
> *Major.Minor.Patch*.
>
> In brief, these correspond to:
>
> -   **Major:** Breaking changes.
> -   **Minor:** New features, but backwards compatible.
> -   **Patch:** Backwards compatible bug fixes only.

Given that the Patch number is supposed to represent bug fixes only,
NuGet chooses the minimum Major and Minor version of a package to meet
the dependency contstraint, but the **maximum Patch version**. David
Ebbo describes the algorithm and rationale in [part 2 of his three part
series on NuGet
Versioning](http://blog.davidebbo.com/2011/01/nuget-versioning-part-2-core-algorithm.html "NuGet Versioning Part 2").

Strong Names and Versioning
---------------------------

The consequence of this is as follows. With the new log4Net release, if
you have a package that has log4net 1.2.10 or greater as a dependency:

```csharp
<dependency id="log4net" version="1.2.10" />
```

Installing that package would give you log4net 1.2.11. In most cases,
this is what you want because the newer release might have important bug
fixes such as security fixes.

However, in this case, Log4Net [changed the strong name for their
assembly](http://logging.apache.org/log4net/release/faq.html#two-snks "Two SNK Keys")
for 1.2.11. Whatever your feelings about using strong names or not
(that’s a [separate
discussion](http://nuget.codeplex.com/discussions/247827 "The Strong Name Conundrum")),
the fact is that if you choose to use them, changing the strong name is
changing the identity of your assembly. That’s a major breaking change.

And man, were a lot of people affected! We heard from tons of folks who
were broken by this and unsure how to fix it.

NuGet does support a workaround so that you can prevent inadvertent
upgrades. You can constrain the allowed versions of an installed package
by [manually modifying
packages.config](http://docs.nuget.org/docs/reference/Versioning#Constraining_Upgrades_To_Allowed_Versions "Constraining Upgrades").
Sadly, we don’t yet have a UI for this, so it’s a bit of a pain.

The Solution
------------

Apart from never changing your strong name, the solution in this case is
to treat this change as a major breaking change and increment the major
version number of the assembly.

I don’t anticipate the Log4Net team will change the version of their
assembly, but I reached out to the maintainer of the Log4Net package (no
connection to the Log4Net team so please don’t give him grief about
this) and [he graciously incremented the major
version](http://blog.cincura.net/232722-log4net-dependencies-problem-solved/ "Log4Net dependency problem solved")
of the [Log4Net
package](http://nuget.org/packages/log4net "Log4Net Package on NuGet")
to solve the problem.

Just to be clear, **the log4net 2.0 NuGet package contains the log4net
1.2.11 assembly.**

While it’s generally good form to have the package and assembly version
match to avoid confusion, it’s not necessary. This is a good example of
a case where they need to differ. I do suggest having the “Title” and
“Description” note this fact to help avoid further confusion.

I want to thank Jiri for maintaining the Log4Net package and being
responsive to the need out there! It’s much appreciated.

