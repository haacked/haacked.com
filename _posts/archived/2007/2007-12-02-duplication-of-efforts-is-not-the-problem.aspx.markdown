---
title: Duplication of Efforts Is Not The Problem
date: 2007-12-02 -0800
disqus_identifier: 18429
categories: []
redirect_from: "/archive/2007/12/01/duplication-of-efforts-is-not-the-problem.aspx/"
---

[Oren Eini](http://www.ayende.com/Blog/ "Ayende"), aka Ayende, writes
about [his dissatisfaction with
Microsoft](http://www.ayende.com/Blog/archive/2007/12/03/Reasons-for-caring-Microsoft-amp-OSS.aspx "Reasons for caring: Microsoft and OSS")
reproducing the efforts of the OSS community. His post was sparked by
the following thread in the ALT.NET mailing list:

> **Brad:** If you're simply angry because we had the audacity to make
> our own object factory with DI, then I can't help you; the fact that
> P&P did ObjectBuilder does not invalidate any other object factory
> and/or DI container.
>
> **Ayende:** No, it doesn't. But it is a waste of time and effort.
>
> **Brad:** In all seriousness: why should you care if I waste my time?

Ayende’s response is:

-   I care because it means that people are going to get a product that
    is a duplication of work already done elsewhere, usually with less
    maturity and flexibility.
-   I care because people are choosing Microsoft blindly, and that puts
    MS in a position of considerable responsibility.
-   I care because I see this as continued rejection of the community
    efforts and hard work.
-   I care because it, frankly, shows contempt to anything except what
    is coming from Microsoft.
-   I care because it so often ends up causing me grief.
-   I care because it is doing disservice to the community.

As a newly minted employee of Microsoft, it may seem like I am incapable
of having a balanced opinion on this, but I am also an OSS developer and
was so before I joined, so hopefully I am not so totally unbalanced ;).

I think his sentiment comes from certain specific efforts by Microsoft
that, how can I put this delicately, *sucked* in comparison to the
existing open source alternatives.

Two specific implementations come to mind, MS Test and SandCastle.

However, as much I tend to enjoy and agree with much of what Ayende says
in his blog, I have to disagree with Ayende on this point that
**duplication of efforts** is the problem.

After all, open source projects are just as guilty of this duplication.
Why do we need
[BlogEngine.NET](http://www.codeplex.com/blogengine "BlogEngine.NET")
when there is already [Subtext](http://subtextproject.com/ "Subtext")?
And why do we need Subtext when there is already
[DasBlog](http://dalblog "http://www.dasblog.info/")? Why do we need
MbUnit when there is NUnit? For that matter, why do we need Monorail
when there is Ruby on Rails or RhinoMocks when there is NMock?

I think Ayende is well suited to answer that question. When he created
[RhinoMocks](http://www.ayende.com/projects/rhino-mocks.aspx "Rhino Mocks"),
there was already an open source mocking framework out there,
[NMock](http://nmock.org/ "NMock"). But NMock perhaps didn’t meet
Ayende’s need. Or perhaps he thought he could do better. In any case, he
went out and duplicated the efforts of NMock, but in many (but maybe not
all) ways, made it better. I personally love using RhinoMocks.

The thing is, there is no way for NMock nor RhinoMocks to meet all the
needs of all the possible constituencies needs for a mocking framework.
Technical superiority isn’t always the deciding factor. Sometimes
political realities come into play. For example, whether we like it or
not, some companies won’t use open source software. In an environment
like that, neither NMock nor RhinoMocks will make any headway, leaving
the door open for yet another mocking framework to make a dent.

**Projects that seem to duplicate efforts never make perfect copies**.
They each have a slightly different set of requirements they seek to
address. In an evolutionary sense, each *duplicate* contains mutations.
And like evolution, survival of the fittest ensues. Except this isn’t a
global winner takes all zero sum game.

What works in one area might not survive in another area. Like the real
world, niches form and that which can find a niche in which it is strong
will survive in that niche.

I’m reminded of this when I read that the [Opera Mini browser beats
Apple
Safari](http://operawatch.com/news/2007/04/update-opera-mini-beats-safari-netscape-and-mozilla-combined-in-ukraine.html "Opera Mini"),
Netscape, and Mozilla combined in the Ukraine. Another remindes is how
Google built [yet another social platform](http://orkut.com/ "Orkut")
that is really big Brazil.

So again, **Duplication Is Not The Problem**. Competition is healthy. If
anything, the problem is, to stick with the evolution analogy, is that
Microsoft because of its sheer might gives its creations quite the head
start, to survive when the same product would die had it been released
by a smaller company. We’ve seen this before when Microsoft let IE 6 rot
on the vine and risks doing [the same with IE
7](http://www.codinghorror.com/blog/archives/001006.html "What if they gave a browser war and Microsoft never came").
Fact of the matter is, Microsoft has a lot of influence.

So can we really fault Microsoft for duplicating efforts? Or only for
doing a half-assed job of it sometimes? As I wrote before when I asked
the question [Should Microsoft **Really** Bundle Open Source
Software](https://haacked.com/archive/2007/09/04/should-microsoft-really-bundle-open-source-software.aspx "Really?")?,
I’d like to see some balance that both recognizes business realities
that push Microsoft to duplicating community efforts, but at the same
time support the community.

After all, Microsoft can’t let what is out there completely dictate its
product strategy, but it also can’t ignore the open source ecosystem
which is a boon to the .NET Framework.

*Disclaimer:* *I shouldn’t need to say it, but to be clear, these are my
opinions and not necessarily those of my employer.*

