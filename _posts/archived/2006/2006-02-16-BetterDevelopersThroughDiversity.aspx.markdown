---
title: Better Developers Through Diversity
tags: [diversity,developers]
redirect_from: "/archive/2006/02/15/BetterDevelopersThroughDiversity.aspx/"
---

The local newspaper sports a comic strip that enjoys every opportunity
to mock the modern university’s emphasis on diversity. The strip holds
diversity accountable for lowered standards and educational quality.
However it ignores the danger of a lack of diversity, namely that a
student might not be exposed to new ideas that challenge pre-existing
assumptions that would improve the student’s overall education.

![Tunnel Vision](https://haacked.com/assets/images/TunnelVision.jpg) Likewise,
any institution that does not cultivate diverse viewpoints risks being
caught in institutional tunnel vision. At times, spending too much time
and energy duplicating the efforts of others due to simple ignorance. I
sometimes see hints of this within the .NET developer community.

## Copping a Borgitude

Over the past several years, there have been a proliferation of
developers who have what I call the "Microsoft" mindset, also known as
"Microsoftitis" or "Borgitude". This conditioned is characterized by a
general tendency to hold up the Microsoft way of doing things as the
holy bible of software development.

Please note that this is not a blanket criticism of the Microsoft way of
doing things. For the most part, Microsoft does a bang up job of
developing software and their development platform is fantastic.
However, not everything they preach is gospel. What works for Microsoft
isn’t always going to work as a one-size fits all solution for everyone
else. And at times, Microsoft has shown itself capable of completely
ignoring prior art.

## Test Driven Development

What really got me thinking about this is a while ago was Microsoft’s
blunder with their approach to TDDin Team System, [here summarized by
Sam Gentile](http://samgentile.com/blog/archive/2005/11/18/32103.aspx).

This is not to say that TDD is gospel either. But in adopting TDD as
part of Team System, Microsoft sets the stage to shape how many
developers with Microsoftitis will first encounter and view TDD. Rather
than abiding by their traditional "Embrace and Extend" philosophy, they
decided to "Ignore and Do It Our Way" which will leave many with a
skewed view of TDD and its benefits.

This is unfortunate because TDD has a rich history and a lot of prior
art spearheaded by the Java and Smalltalk communities. The fact that the
source of these practices are not Microsoft should not dissuade a
developer with Borgitude from giving them a fair shake. There is a lot
to learn from these other communities, and a lot to lose by not
expanding one’s experience.

## Source Control

Another classic example is source control. To many developers with
Microsoftitis (and I was this way for a long time) source control *is*
Visual Source Safe (derisively called Visual Source Crap by its
detractors). Other means of version control are unknown to such
developers.

Visual Source Safe has its place. For fairly small teams within the same
network, it does a decent enough job. But for developers who have a very
low tolerance of repository corruption, larger teams, or distributed
teams, VSS is not an ideal solution.

The most common reason given for sticking with VSS is that it is free.
This is ironic because it isn’t technically free. It merely comes
bundled with whichever MSDN subscription your company happened to buy.
However the real cost isn’t that subscription fee, it is the lost
productivity by being forced to adhere to the pessimistic locking
checkout/checkin model of VSS. For large teams this is extremely
restrictive. And when VSS corrupts your data (if it hasn’t yet, just
wait. You’re sitting on a ticking time bomb) add the cost of downtime
and developer time to bring it back. That is the real cost of VSS.

For distributed teams, VSS performs terribly and is basically unusable
unless you purchase
[SourceOffsite](http://www.sourcegear.com/sos/index.html "SourceOffSite Product")
made by a [VSS competitor](http://www.sourcegear.com/ "SourceGear").

What really burns my hide is that one of the reasons given for changing
the web project model in ASP.NET 2.0 is that large teams complained of
lock contention on the project file. This isn’t a problem with the
existing web project model, this is a problem with pessimistic locking
on a large project! This has led to many complaining about the [new
project
model](http://geekswithblogs.net/sbellware/archive/2005/08/07/49518.aspx "ASP.NET 2.0 Web Projects Complaint").

If the real argument for sticking with VSS is that it is free, one
should consider [Subversion](http://subversion.tigris.org/ "subversion")
which is orders of magnitude better.

I suspect that the real reason many stick with it is because they are
used to it. Many have never actually taken a class or [read a tutorial
on version
control](http://software.ericsink.com/scm/source_control.html "Source Control HowTo"),
so the ramp up time for a new system seems daunting.

On one hand, there is nothing wrong with that. If it is really working
for you, why change? On othe other hand, VB6 worked for a lot of
developers, but there is only so long you can ignore the advantages to
moving to VB.NET or C#.

## Development Idioms

If Microsoft provides a component, many developers will never evaluate
alternatives. This makes sense for many reasons such as [dependency
avoidance](http://www.codinghorror.com/blog/archives/000497.html "Dependency Avoidance").
Who has the time to evaluate other options when one is already built in?
Even so, one should have at least some small exposure to other
solutions. Especially when they too are free.

For example, in ASP.NET 1.1 it makes good sense to use the built in
Tracing mechanism for logging. It is very configurable and extensible
and generally meets the needs of most developers. However many
developers find it to be extremely slooow and discovered that
[Log4Net](http://logging.apache.org/log4net/) is an even better solution
for their needs. By breaking out of *Microsoftitis* they discover a
mature logging framework that meets their functional needs and
performance needs.

The fact that it is in essence a port of the java community’s Log4j does
not detract from its usefulness. Log4j has been around a long time and
that maturity is apparent as they have solved a lot of problems that
other roll-your-own logging frameworks are just now discovering.

## The Remedy

If you are a .NET developer I am by no means advocating that you switch
to Java (though you may want to spend some time programming in Java,
Ruby, Python, etc...). I firmly believe that in a language to language
comparison, C# is a much better language to write software in than
Java.

However, the language syntax itself is not the whole of the development
experience. Many of the features we are just oohing and ahhing about in
VS.NET 2005 have been in Java IDEs such as IDEA and Eclipse for a long
time now. It seems that we are rediscovering the many things that Java
and SmallTalk developers take for granted.

So what I am advocating is to diversify your development reading,
toolbox and experience. For example, instead of complaining about how
expensive Team System is, consider that by using
[Trac](http://www.edgewall.com/trac/ "Trac Project Management System"),
[MbUnit](http://mbunit.tigris.org/ "MBUnit Unit Test Framework") (or
[NUnit](http://nunit.org/ "NUnit Unit Test Framework")) and
[Subversion](http://subversion.tigris.org/ "Subversion Source Control System"),
you have many of the tools that TS provides, but for free. You don’t
have to be beholden to the Microsoft way.

Instead of only reading Microsoft Press, read a book on Java or Ruby.
Read up on MVC and other architectural styles and decide for yourself
whether or not it makes sense to use it on your next ASP.NET project.
Read up on REST as an alternate to SOAP and understand the differences
and the advantages and disadvantages of each.

Most of all, consider working on an open source project. In the comments
to [my post on Open
Source](https://haacked.com/archive/2006/01/16/MisperceptionsofOpenSource.aspx#comments),
Joe Brinkman [says it
well](https://haacked.com/archive/2006/01/16/MisperceptionsofOpenSource.aspx#comments)
when he describes the benefits of being involved in an open source
project.

> In my office, the closest I ever came to some of the best and
> brightest in the development community was seeing someone speaking
> onstage or reading their book/blog. Since working on an OS project, I
> have had the opportunity to trade ideas with some of the best .Net
> developers in the world.

