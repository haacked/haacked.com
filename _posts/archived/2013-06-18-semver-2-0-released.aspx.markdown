---
layout: post
title: SemVer 2.0 Released
date: 2013-06-18 -0800
comments: true
disqus_identifier: 18892
categories:
- code
redirect_from: "/archive/2013/06/17/semver-2-0-released.aspx/"
---

One of the side projects I’ve been working on lately is helping to
shepherd the [Semantic Versioning
specification](http://semver.org/ "SemVer") (SemVer) along to its 2.0.0
release. I want to thank everyone who sent pull requests and engaged in
thoughtful, critical, spirited feedback about the spec. Your involvement
has made it better!

I also want to thank
[Tom](http://tom.preston-werner.com/ "Top Preston-Werner") for creating
SemVer in the first place and trusting me to help move it along.

I’ve mentioned [SemVer in the past as it relates to
NuGet](https://haacked.com/archive/2011/10/24/semver-nuget-nightly-builds.aspx "SemVer, NuGet, and Nightly builds").
The 2.0.0 release of SemVer addresses some of the issues I raised.

What’s Changed?
---------------

Not too much has changed. Most of the changes focus around
clarifications.

### Build metadata

Perhaps the biggest change is the addition of optional *build metadata*
(what we used to call a build number). This simply allows you to add a
bit of metadata to a version in a manner that’s compliant with SemVer.

The metadata does not affect version precedence. It’s analogous to a
code comment.

It’s useful for internal package feeds and for being able to tie a
specific version to some mechanism that generated it.

For existing package managers that choose to be SemVer 2.0 compliant,
the logic change needed is minimal. Instead of reporting an error when
encountering a version with build metadata, all they need to do is
ignore or strip the build metadata. That’s pretty much it.

Some package managers may choose to do more with it (for internal feeds
for example) but that’s up to them.

### Pre-release identifiers

Pre-release labels have a little more structure to them now. For
example, they can be separated into identifiers using the “.” delimiter
and identifiers that only contain digits are compared numerically
instead of lexically. That way, `1.0.0-rc.1` \< `1.0.0-rc.11` as you
might expect. See the specification for full details.

### Clarifications

The rest of the changes to the specification are concerned with
clarifications and resolving ambiguities. For example, we clarified that
leading zeroes are not allowed in the Major, Minor, or Patch version nor
in pre-release identifiers that only contain digits. This makes a
canonical form for a version possible.

If you find an ambiguity, feel free [to report
it](https://github.com/mojombo/semver/issues?state=open "mojombo/semver issues on GitHub").

What’s Next?
------------

As SemVer matures, we expect the specification to become a little more
formal in nature as a means of removing ambiguities. One such effort
underway is to include a [BNF
grammar](https://github.com/mojombo/semver/pull/116 "BNF Grammar Pull Request")
for the structure of a version number in the spec. This should hopefully
be part of SemVer 2.1.

