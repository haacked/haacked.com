---
title: Should Subtext Move To CodePlex
date: 2006-06-30 -0800 9:00 AM
tags: [subtext]
redirect_from: "/archive/2006/06/29/ShouldSubtextMoveToCodePlex.aspx/"
---

Seems like every day now someone asks me if I plan on moving
[Subtext](http://subtextproject.com/ "Subtext") over to
[CodePlex](http://www.codeplex.com/ "CodePlex - Microsoft's Community Development Website").
I figure it would save me a lot of trouble if I just answer this
question here once and for all.

Though of course I can’t answer *once and for all*. I can only answer
for the *here and now*. And right now, I have thought about it, but have
not strongly considered it for the following reasons.

Not Feeling the Pain
--------------------

First of all, I am not really feeling a lot of pain with our current
setup at SourceForge. We have CruiseControl.NET humming along nicely, a
great build process, and are very happy with Subversion. Life is good,
why should we change?

Also, we already made one switch from CVS to Subversion. To yet again
switch source control systems is a big hassle. There would have to be a
huge benefit to doing so to make it worthwhile. A minor benefit is not
enough.

Source Control
--------------

As you know, I am a big fan of Subversion and TortoiseSVN. Source
Control bindings in Visual Studio have been the biggest nightmare second
only to Front Page extensions that I have had the pleasure to deal with.
For example, I work with one client who uses Vault and another who uses
Visual Source Safe. Switching between the two is such a pain in the rear
as I have to remember to switch the SCC provider before I start working
on one or the other.

As far as I am concerned, there is a big hump to overcome to get me
comfortable with using SCC again. I understand that the Codeplex people
are working on
[Turtle](http://www.codeplex.com/Wiki/View.aspx?ProjectName=Turtle "Turtle")
which is a TortoiseSVN like interface to CodePlex. When this is as solid
as TortoiseSVN, perhaps we can talk.

Also, does Team System source control version renames and moves? That is
a big plus in Subversion. Does it work over HTTPS? Are checkins atomic?
Are branching and tagging fast? I haven’t looked into this and would
love to know.

Source Control History
----------------------

Can we import our Subversion history into Team System at CodePlex? Our
version history is very important to us. At least to me. I would hate to
lose that.

CruiseControl.NET
-----------------

It is probably only a matter of time before someone writes a plugin for
[CruiseControl.NET](http://confluence.public.thoughtworks.org/display/CCNET/Welcome+to+CruiseControl.NET "Cruise Control.NET")
that works with Team System. But this would be important to me. Now
[Simone](http://blogs.ugidotnet.org/piyo/ "Simone") tells me that Team
System has something equivalent that would replace CCNET as part of
CodePlex. If this is the case, then I would love to see details.

MbUnit
------

As you might also know, I love me some
[MbUnit](http://mbunit.com/ "MbUnit"). I made the switch [from
NUnit](https://haacked.com/archive/2005/10/18/SwitchingToMbUnit.aspx "Switching to MbUnit")
a while ago and have never looked back. If CodePlex has a CCNet
replacement, will it integrate with MbUnit. I know Team System has its
own unit test framework, but does it have the `Rollback` and `RowTest`
attributes and a `TypeFixture` or equivalents? And if you tell me about
its extensibility model and that I can write my own, I ask in response,
why should I? I already have those things.

Summary
-------

At this point, I would love to hear more details about CodePlex that
address my concerns. Perhaps a demo video that shows me what we’re
missing. But until these issues are addressed, or if all the other
Subtext developers are chomping at the bit for CodePlex and threaten a
mutiny if we do not switch over, I do not see any urgency or reason to
switch now. Sometimes being bleeding edge just leaves you with a bloody
mess.

