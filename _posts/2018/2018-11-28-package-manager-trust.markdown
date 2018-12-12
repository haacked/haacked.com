---
title: "The Problem of Package Manager Trust"
description: "Trust is a tricky issue when it comes to package managers, as evidence by recent events with the event-stream package."
date: 2018-11-28 -0800 09:30 AM PDT
categories: [npm nuget security trust safety package-managers]
---

Package managers are among the most valuable tools in a developer's toolkit. A package can inject hundreds to thousands of lines of useful code into a project that a developer would otherwise have to write by hand. Ain't nobody got time for that!

Of course, such tools do not come without risk as highlighted by the [`event-stream`](https://www.npmjs.com/package/event-stream) package incident.

> Streams are node's best and most misunderstood idea, and EventStream is a toolkit to make creating and working with streams easy.

This is a very popular NPM package with 1,592 downstream dependents and 1.9 Million weekly downloads (at the time I wrote this). Unfortunately, the most recent release of the package contained a malicious dependency which was not discovered for around two months. It was reported [in this GitHub issue](https://github.com/dominictarr/event-stream/issues/116).

How did this happen? Well the original maintainer was no longer interested in maintaining the package and handed it off to another contributor who had previously made real contributions to the package. It appears that these contributions were made to gain trust so as to gain access to publishing the package. Unfortunately, this new contributor was a bad actor, and not the Jean-Claude Van Damme variety of bad actors.

## Put down the pitch forks

As you can imagine, many took to their pitchforks and directed a lot of blame and vitriol towards the maintainer. Just stop.

Blaming the maintainer is mean and doesn't accomplish anything useful. It's also a sign of intense immaturity.

Other industries learned a long time ago that a culture of blame doesn't improve results. Mature teams think beyond the individual and apply the [approach of a blameless postmortem](https://codeascraft.com/2012/05/22/blameless-postmortems/).

> If we go with “blame” as the predominant approach, then we’re implicitly accepting that deterrence is how organizations become safer. This is founded in the belief that individuals, not situations, cause errors. It’s also aligned with the idea there has to be some fear that not doing one’s job correctly could lead to punishment. Because the fear of punishment will motivate people to act correctly in the future. Right?

The aviation industry realized that individual blame didn't improve safety, but a focus on human factor design applied to the problem would. We live in an increasingly complex world with increasingly complex systems. It's unreasonable to expect everyone will do the right thing every time. As [The Checklist Manifesto](https://www.samuelthomasdavies.com/book-summaries/health-fitness/the-checklist-manifesto/) notes,

> The volume and complexity of what we know has exceeded our individual ability to deliver its benefits correctly, safely, or reliably. Knowledge has both saved us and burdened us.

Even if you think that expectation is reasonable, it's foolish to trust in such a system that's entirely dependent on that expectation. This is why I'm very glad that pilots follow checklists rather than just rely on their memory when I'm a passenger.

## This is not an isolated problem

It's also important to understand, as [Dominic points out in his statement on the incident](https://gist.github.com/dominictarr/9fd9c1024c94592bc7268d36b8d83b3a), that this is not an isolated problem.

> Hey everyone - this is not just a one off thing, there are likely to be many other modules in your dependency trees that are now a burden to their authors.

He goes on to highlight the big challenge for maintainers.

>  So right now, we are in a weird valley where you have a bunch of dependencies that are "maintained" by someone who's lost interest, or is even starting to burnout, and that they no longer use themselves. You can easily share the code, but no one wants to share the responsibility for maintaining that code.

This could happen to a maintainer you know and love. This could happen to you.

## How bad was it?

Before we talk about solutions, it might also help to put this particular incident in perspective. The NPM team published [an incident report](https://blog.npmjs.org/post/180565383195/details-about-the-event-stream-incident) that describes how the vulnerability works. The malicious package only attacks users of the Copay Bitcoin wallet who have over 100 Bitcoin (BTC) or 1000 Bitcoin Cash (BCH). In USD that's around $600K...wait no...$500K...actually it's now $400K...still a lot of money. The afflicted are probably a pretty small subset of the population.

Also, it helps to understand that NPM has about [8 Billion weekly package downloads](https://slides.com/seldo/npm-future-of-javascript-qcon#/4). `event-stream` accounts for about 0.024% of those package downloads. So large in scale, but tiny in terms of overall package downloads. Of course this doesn't account for the number of potentially malicious packages we don't know about. The point here is it's still pretty difficult to get someone to download your malicious package.

## What's the solution?

Ok, now that you put down your pitchforks (which is curious to me because how many of you are actually farmers?), let's talk about solutions.

So what do we do about it? I don't believe there's a magic _solution_ out there, but I think there are some mitigations worth discussion.

In a previous life, I was part of a team responsible for the NuGet package manager. Thinking about these sort of attacks kept me up at night (but now I sleep like a baby). The approach I wanted to take at the time was to focus on [identity, reputation, and webs of trust](https://haacked.com/archive/2013/02/19/trust-and-nuget.aspx/). I believe many of those ideas still apply today, but they don't necessarily solve for this specific case. At least, not without a small adjustment.

NuGet recently added certificate signing of packages. This allows consumers to specify they only want to install packages of verified people. This also doesn't address this particular problem for two reasons. Not every package maintainer will bother with the certificates. And even if they do, NuGet supports packages signed by an org. So if a burnt-out maintainer adds a bad actor to their org, all bets are off.

What my previous threat models missed is that we treat every package the same whether it's depended on by a hundred people or a hundred million people. This ignores the fact that __the threat model for these repositories are not the same!__

For example, changing the owner (or giving someone else rights to publish) for my barely-used package does not have the same threat impact than changing the owner to my super-popular-left-pad package used by half of the internet.

There's a few things package managers might consider in this situation (in addition to the ideas I wrote about in [my Trust and NuGet post](https://haacked.com/archive/2013/02/19/trust-and-nuget.aspx/)).

1. Consider a change of owner as a [SemVer breaking change](https://semver.org). At least that would prevent the package from aggressively being updated in most package managers.
2. When changing owners (or giving publish access), provide easy to understand reputation information for very popular packages. Maybe even block ownership transfer on extremely popular packages to suspect individuals. The point here is to leverage reputation and trust information in some useful way.
3. Provide education, tools, and support to burnt-out maintainers. Work with open source foundations so that they can take over and perhaps vet and find maintainers for projects that maintainers want to dump. Don't leave the entire burden on maintainers of projects that became way more popular than they anticipated. We need to share that burden.

This is an area where GitHub (_full disclosure: I'm a former GitHub employee_) could really take a lead in concert with the various package managers. GitHub has a wealth of information about repositories, their dependencies, and the people who work on them. This information could help maintainers make better choices, if it was integrated with the information that package managers have on hand. This would require deep cooperation between package managers and GitHub. I think this would be a very good thing. 

## What about paying maintainers?

One solution a lot of folks bring up is paying maintainers to maintain these packages. The thinking goes that there are a lot of companies flush with money who get immense benefit from open source without giving back. Why shouldn't they contribute to the maintenance of these packages?

Of course I'm all for systems that help maintainers get paid for the work they do. At the same time, I'm skeptical that this will actually solve the trust problem.

Often, the resource that's really scarce for maintainers is time, not money. A lot of maintainers work on their open source projects on the side while holding down a full-time job. Part of the cause of burn out is the additional stress of working on a bunch of side project on the weekend. A few grand extra doesn't necessarily solve that problem. Their day job is unlikely to let them work less hours because they have a side project.

The only way this works is if maintainers are paid enough to quit their day jobs and maintain their open source projects full-time. This works great if [you're the maintainer of Linux](https://groups.google.com/forum/#!topic/sci.physics/MHnwywpxof4), but probably not sustainable for someone who maintains a handful of small packages.

This problem is a classic [tragedy of the commons](https://en.wikipedia.org/wiki/Tragedy_of_the_commons) example.

> The tragedy of the commons is a term used in social science to describe a situation in a shared-resource system where individual users acting independently according to their own self-interest behave contrary to the common good of all users by depleting or spoiling that resource through their collective action.

Some might object to this depiction because what resource is being spoiled by using a package? Every download of a package doesn't take anything away from the maintainer. In practice, it's the package author's attention that's depleted. The more people who use a package, the more issues the maintainer has to wade through. It becomes a big slog.

What I want to see is a large scale communal ownership attitude by companies towards open source. We need to take care of our commons, pitch in, find solutions. Don't just contribute a bit of cash to maintainers, but work together to provide maintainers work time to maintain their packages. Find creative solutions to _both_ the time and money scarcity problem.
