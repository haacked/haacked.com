---
title: "SemVer's New Maintainers"
description: "I am stepping down as the maintainer of SemVer. It will be maintained by a consortium of representatives from the major package managers."
date: 2019-02-11 -0800 09:01 AM PDT
tags: [semver]
excerpt_image: https://user-images.githubusercontent.com/19977/52528219-b1697480-2c8d-11e9-848a-6c620eecb5fb.jpg
---

For several years now, I've been the maintainer of [the SemVer specification](https://semver.org). It's been an honor and privilege to be in this position. But I'll be honest, it's also an enormous responsibility and a big pain in the ass. This is why I'm happy to say that I am stepping down as the maintainer of SemVer and passing the torch to a team of maintainers better suited to direct its future. Now the pain (and honor, don't forget the honor) can be distributed among multiple people, and not focused on just one.

![Ride into the sunset by Jérôme Molnar - CC BY-ND 2.0](https://user-images.githubusercontent.com/19977/52528219-b1697480-2c8d-11e9-848a-6c620eecb5fb.jpg)

## Backstory

The last major release of SemVer was nearly six years ago with [the release of SemVer 2.0](https://haacked.com/archive/2013/06/18/semver-2-0-released.aspx/).

Since then, not a lot has changed in the spec. Not for a lack of discussion. The issues have been very active in the past few years. There's been a lot of request for changes. I've ignored most of those changes for three reasons:

1. I've been busy and these are very nuanced issues that take time and a lot of thought.
2. I was worried about how the changes would affect all the different package managers.
3. I didn't think the changes were important enough to warrant all the hassle in making the changes.

My personal view is that a spec like this should change very slowly. Every change has a cost that's larger than first appearances.

As I write this, SemVer has been translated into 26 languages over at [semver/semver.org](https://github.com/semver/semver.org). Merging translations has been my primary task with SemVer for the past few years. Every time we release a new version of SemVer, every translation needs to be updated. Every implementation of SemVer needs to change.

It made a lot of sense for me to maintain SemVer when I was heavily involved with NuGet, a .NET package manager. However, over the years, my involvement in that area has gone to zero. And it occured to me that I no longer have skin in the game. I was only maintaining SemVer as a service to the software community, but I am no longer the right person to do that. It's too much responsibility for one person to make these decisions unilaterally.

In my view, the ones who should make those decisions are the ones who are both most impacted by SemVer and who also have a big impact in their respective software communities - the package managers.

## Creating the SemVer Team

So I reached out to folks from the various package managers that adhere to SemVer to some degree or other.

I asked each package manager to provide a representative to start the initial group that would maintain SemVer from now on. Together, this group put together a document that describes the governance model and the RFC (request for comments) process that describes how changes to the SemVer spec will be made in the future. This governance document is now the `CONTRIBUTING.md` document for [semver/semver])(https://github.com/semver/semver) repository.

These are the folks who have skin in the game when it comes to versioning. Package managers have immense importance to their respective software communities. I am excited to step down as the maintainer of SemVer and hand it off to these folks.

With this in place, there is now a path for future changes to SemVer that don't rely on me unilaterally making a decision.

## Acknowledgments

I'd like to acknowledge several people who've been a big help along the way.

1. [@jwdonahue](https://github.com/jwdonahue) has been tireless in vetting issues and managing discussions in the issue tracker.
2. [@zeke](https://github.com/zeke) who donated the GitHub [semver org](https://github.com/semver) to me so I could house SemVer in an org.
3. [@mojombo](https://github.com/mojombo) who created SemVer and also transferred the SemVer repositories to the semver org.
4. And of course, [the new SemVer maintainers team](https://github.com/orgs/semver/teams/maintainers/members). They've provided valuable advice about how to go about this transition.

I trust SemVer is in good hands now. If I knew how to ride a motorcycle, I'd cue the movie trope of riding off into the sunset. I've always wondered how long they ride before they realize they'll never get there.
