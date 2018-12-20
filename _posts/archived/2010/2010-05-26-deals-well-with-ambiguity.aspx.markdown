---
title: Deals Well With Ambiguity
date: 2010-05-26 -0800
tags:
- code
redirect_from: "/archive/2010/05/25/deals-well-with-ambiguity.aspx/"
---

A while ago I was talking with my manager at the time about traits that
we value in a Program Manager. He related an anecdote about an interview
he gave where it became clear that the candidate did not deal well with
ambiguity.

This is an important trait for nearly every job, but especially for PMs
as projects can often change on a dime and it’s important understand how
to make progress amidst ambiguity and eventually drive towards resolving
ambiguity.![](http://farm2.static.flickr.com/1343/1398087375_d985da0da0_o.jpg)

Lately, I’ve been asking myself the question, doesn’t this apply just as
much to software?

**One of the most frustrating aspects of software today is that it
doesn’t deal well with ambiguity.** You could take the most well crafted
robust pieces of software, and a cosmic ray could flip one bit in memory
and potentially take the whole thing down.

The most common case of this fragility that we experience is in the form
of breaking changes. Pretty much all applications have dependencies on
other libraries or frameworks. One little breaking change in such a
library or framework and upgrading that dependency will quickly take
down your application.

Someday, I’d love to see software that really did deal well with
ambiguity.

For example, lets take imagine a situation where a call to a method
which has changed its signature wouldn’t result in a failure but would
be resolved automatically.

In the .NET world, we have something close with the concept of [assembly
binding
redirection](http://msdn.microsoft.com/en-us/library/2fc472t2(VS.80).aspx "Assembly Binding Redirection documentation on MSDN"),
which allow you to redirect calls compiled against one version of an
assembly to another. This is great if none of the signatures of existing
methods have changed. I can imagine taking this further and allowing
application developers to apply redirection to method calls account for
such changes. In many cases, the method itself that changed could
indicate how to perform this redirection. In the simplest case, you
simply keep the old method and have it call the new method.

More challenging is the case where the semantics of the call itself have
changed. Perhaps the signature hasn’t changed, but the behavior has
changed in subtle ways that could break existing applications.

In the near future, I think it would be interesting to look at ways that
software that introduce such breaks could also provide hints at how to
resolve the breaks. Perhaps code contracts or other pre conditions could
look at how the method is called and in cases where it would be broken,
attempt to resolve it.

Perhaps in the further future, a promising approach would move away from
programming with objects and functions and look at building software
using [autonomous software
agents](http://en.wikipedia.org/wiki/Software_agent "Software Agents in Wikipedia")
that communicate with each other via messages as the primary building
block of programs.

In theory, autonomous agents are aware of their environment and flexible
enough to deal with fuzzy situations and make decisions without human
interaction. In other words, they know how to deal with some level of
ambiguity.

I imagine that even in those cases, situations would arise that the
software couldn’t handle without human involvement, but hey, that
happens today even with humans. I occasionally run into situations I’m
not sure how to resolve and I enlist the help of my manager and
co-workers to get to a resolution. Over time, agents should be able to
employ similar techniques of enlisting other agents in making such
decisions.

Thus when an agent is upgraded, ideally the entire system continues to
function without coming to a screeching halt. Perhaps there’s a brief
period where the system’s performance is slightly degraded as all the
agents learn about the newly upgraded agent and verify their
assumptions, etc. But overall, the system deals with the changes and
moves on.

A boy can dream, eh? In the meanwhile, if reducing the tax of backwards
compatibility is the goal, there are other avenues to look at. For
example, by you could apply isolation using virtualization so that an
application always runs in the environment it was designed for, thus
removing any need for dealing with ambiguity (apart from killer cosmic
rays).

In any case, I’m excited to see what new approaches will appear over the
next few decades in this area that I can’t even begin to anticipate.

