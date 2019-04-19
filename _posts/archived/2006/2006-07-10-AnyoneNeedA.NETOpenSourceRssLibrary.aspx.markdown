---
title: Anyone Need A .NET Open Source Rss Library
date: 2006-07-10 -0800 9:00 AM
tags: [oss,rss,dotnet]
redirect_from: "/archive/2006/07/09/AnyoneNeedA.NETOpenSourceRssLibrary.aspx/"
---

I have been considering using a separate library for generating the RSS
and Atom feeds in Subtext. My first thought was to use
[RSS.NET](http://sourceforge.net/projects/rss-net "RSS.NET on SourceForge")
but I noticed that there seemed to be no recent activity.

I contacted the admin and found out that RSS.NET has been bought by
[ToolButton Inc](http://www.web2.0tools.net/ "ToolButton, Inc.") and
will be released as a product. Very cool!

In the meanwhile, I still need an open source RSS library to package up
with Subtext. Fortunately, RSS.NET was developed under the MIT license
which, as I mentioned before, is [very compatible
with](https://haacked.com/archive/2006/01/24/DevelopersGuideToOpenSourceSoftwareLicensing.aspx "Guide to open source licenses")
our BSD license.

So one option is to simply copy the code into our Subtext code base. My
only qualm about this approach is that I would like to keep stand-alone
libraries that are not central to the Subtext Domain out of the Subtext
codebase as much as possible, preferring to reference them as an
external library.

Ideally, I would like to start a new project that is essentially a fork
in RSS.NET, perhaps called *FeedGenerator.NET* (call me the forkmaster).
I could probably host it on CodePlex in order to give me an opportunity
to try it out and provide feedback. Would anyone find such a library
useful other than us blog engine developers? Anyone have a better name?

I probably wouldn’t spend much time on this project except to provide
changes and bug fixes as needed by Subtext. It would by no means be
*intended* to compete with Web 2.0 Tools products, since they are
probably going to be much more full featured than our humble needs.
Besides, under the MIT license, any improvements we make would be
available for them to roll into their product (following the terms of
the license of course). It is the beauty of the MIT and BSD licenses.

Any thoughts? Suggestion? Etc...?

