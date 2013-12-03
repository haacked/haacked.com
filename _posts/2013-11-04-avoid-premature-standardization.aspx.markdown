---
layout: post
title: "Avoid Premature Standardization"
date: 2013-11-04 -0800
comments: true
disqus_identifier: 18904
categories: [open source,code,github]
---
Most developers are aware of the potential pitfalls of premature
optimization and [premature
generalization](http://haacked.com/archive/2005/09/19/avoid_premature_generalization.aspx "Avoid Premature Generalization").
At least I hope they are. But what about premature standardization, a
close cousin to premature generalization?

It’s human nature. When patterns emerge, they tempt people to drop
everything and put together a standard to codify the pattern. After all,
everyone benefits from a standard, right? Isn’t it a great way to ensure
interoperability?

Yes, standards can be helpful. But to shoehorn a pattern into a standard
prematurely can stifle innovation. New advances are often evolutionary.
Multiple ideas compete and the best ones (hopefully) gain acceptance
over time while the other ideas die out from lack of interest.

Once standardization is in place, people spend so much energy on abiding
by the standard rather than experiment with alternative ideas. Those who
come up with alternative ideas become mocked for not following “the
standard.” This is detrimental.

In his [Rules of
Standardization](http://www.goland.org/innovationandstandards/ "Rules of Standardization"),
Yaron Goland suggests that before we adopt a standard,

> The technology must be very old and very well understood

He proposes twenty years as a good rule of thumb. He also suggests that,

> Standardizing the technology must provide greater advantage to the
> software writing community then keeping the technology incompatible

This is a good rule of thumb to contemplate before one proposes a
standard.

Social Standards
----------------

So far, I’ve focused on software interoperability standards. Software
has a tendency to be a real stickler when it comes to data exchange. If
even one bit is out of place, software loses its shit.

For example, if my code sends your code a date formatted as [ISO
8601](http://en.wikipedia.org/wiki/ISO_8601 "ISO 8601 Date Format"), but
your code expects a date in [Unix
Time](http://en.wikipedia.org/wiki/Unix_time "Unix Time"), stuffs gonna
be broke™.

But social standards are different. By a “social standard” I mean a
convention of behavior among people. And the thing about people is we’re
pretty damn flexible, Hacker News crowd notwithstanding.

Rather than being enforced by software or specifications, social
standards tend to be enforced through the use of encouragement,
coercion, and shaming.

Good social standards are not declared so much as they emerge based on
what people do already. If people converge on a standard, then it
becomes the standard. And it’s only the standard so long as people adopt
it.

This reminds me of a quote by W.L. Gore & Associates’ CEO, [Terri Kelly
on
leadership](http://blogs.wsj.com/management/2010/03/18/wl-gore-lessons-from-a-management-revolutionary/ "On Leadership")
at a non-hierarchical company,

> If you call a meeting, and no one shows up, you’re probably not a
> leader, because no one is willing to follow you.

Standard GitHub issue labels?
-----------------------------

I wrote a [recent
tweet](https://twitter.com/haacked/status/395693870098292737 "Tweet") to
announce a label that the Octokit team uses to denote low hanging fruit
for new contributors,

> For those looking to get started with .NET OSS and
> [http://Octokit.net](http://t.co/S6iDnGGLoO), we tag low hanging fruit
> as "easy-fix".
> [https://github.com/octokit/octokit.net/issues?labels=easy-fix](https://github.com/octokit/octokit.net/issues?labels=easy-fix)

It was not my intention to create a new social standard.

Someone questioned me why we didn’t use the “jump in” label [proposed by
Nik Molnar](http://nikcodes.com/2013/05/10/new-contributor-jump-in/),

> The idea for a standardized issue label for open source projects came
> from the two pieces of feedback I consistently hear from would-be
> contributors:
>
> 1.  “I’m not sure where to start with contributing to project X.”
> 2.  “I’ll try to pick off a bug on the backlog as soon as I’ve
>     acquainted myself enough with the codebase to provide value.” “”

In the comments to that blog post, Glenn Block notes that the ScriptCS
project is using the “YOU TAKE IT” label [to accomplish the same
thing](https://github.com/scriptcs/scriptcs/issues/79 "YOU TAKE IT").

[About two and a half years
earlier](http://haacked.com/archive/2010/10/14/nupack-up-for-grabs-items.aspx "Up for grabs"),
I blogged about the “UpForGrabs” label the NuGet team was using for the
same reason.

As you can see, multiple people over time have had the same idea. So the
question was raised to me, **would I agree that “standardizing” a label
to invite contributors might be a good thing?**

To rephrase one of Goland’s rule of standardization,

> A social standard must provide greater advantage to the software
> community than just doing your own thing.

This is a prime example of a social standard and in this case, I don’t
think it provides a greater advantage than each project doing it’s own
thing. At least not yet. If one arises naturally because everyone thinks
it’s a great idea, then I’m sold! But I don’t think this is something
that can just be declared to be a standard. It requires more
experimentation.

I think the real problem is that these labels are just not descriptive
enough. One issue I have with *Up For Grabs*, *You Take It,*and *Jump
In*is they seem too focused on giving commands to the potential
contributor, “HEY! YOU TAKE IT! WE DON’T WANT IT!”. They’re focused on
the relationship of the core team to the issue. I think the labels
should describe the issue and not how the core team wants new
contributors to interact with the issue.

What makes an issue appeal to a new contributor is different from
contributor to contributor. So rather than a generic “UpForGrabs” label,
I think a set of labels that are descriptive of the issue make sense.
People can then self-select the issues that appeal to them.

For many new contributors, an issue labeled as “easy-fix” is going to
appeal to their need to dip their toe into OSS. For others, issues
labeled as
“[docs-and-samples](https://github.com/octokit/octokit.net/issues?labels=docs-and-samples "Docs and Samples for Octokit")”
will fit their abilities better.

So far, I’ve been delighted that several brand new OSS contributors sent
us pull requests. It far surpassed my expectations. Of course, I don’t
have a control Octokit.net project with the different labels, so I can’t
rightly attribute it to the labels. Science doesn’t work that way. Even
if we did, I doubt it’s the labels that made much of any difference
here.

Again, this is not an attempt to propose a new standard. This is just an
approach we’re experimenting with in Octokit.net. If you like this idea,
please steal it. If you have a better idea, I’d love to hear it!

