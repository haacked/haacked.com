---
title: Atwood Is Preaching And I'm In The Choir
tags: [source-control]
redirect_from: "/archive/2006/08/14/AtwoodIsPreachingAndImInTheChoir.aspx/"
---

![Choir](https://haacked.com/assets/images/AtwoodIsPreachingAndImInTheChoir_CE5B/choir_thumb6.jpg)In
[Jeff Atwood’s](http://www.codinghorror.com/blog/ "Jeff Atwood's Blog")
latest post entitled [Source Control: Anything But
SourceSafe](http://www.codinghorror.com/blog/archives/000660.html "Atwood on Source Control")
he is preaching the gospel message to choose something other than Visual
Source Safe and I am screaming *amen* in the choir section.

There are three common reasons I hear for sticking with Visual Source
Crap (I sometimes swap that last word with one that doesn’t break the
acronym).

*1. It is free!*

*UPDATE: As a lot of people pointed out, VSS isn't free. What I meant
was that it comes with the MSDN Universal Subscription, so many
companies already have a copy of VSS.*

So is
[Subversion](http://subversion.tigris.org/ "Subversion on Tigris").  I
was on a project recently in which VSS corrupted the code twice!  The
time spent administering to it and time lost was a lot more costly than
a license to [SourceGear
Vault](http://www.sourcegear.com/vault/ "Vault").

*2. We know how to use it and don’t want to learn a new system.*

When I hear this, what I am really hearing is *we like our bad habits
and don't want to spend the time to learn good habits*.  Besides, Eric
Sink already wrote a [wonderful
tutorial](http://www.ericsink.com/scm/source_control.html "Source Control Howto").

*3. We have so much invested in VSS.*

Well you had a lot invested in classic ASP (or other such technology)
and that didn't stop you from switching over to ASP.NET (Ruby on Rails,
Java, etc...), did it?

The reason I spend time and energy trying to convince clients to switch
is that it saves them money and saves me headaches.  It really is worth
the effort.

For Open Source projects (or any project that receives user code
contributions), Subversion and CVS have the nice benefit of a [patching
feature](http://www.hanselman.com/blog/ExampleHowToContributeAPatchToAnOpenSourceProjectLikeDasBlog.aspx "Contribute a patch")
making it easy to contribute without having write access.

