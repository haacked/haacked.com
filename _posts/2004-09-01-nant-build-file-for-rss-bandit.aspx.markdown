---
layout: post
title: "NAnt Build File For Rss Bandit"
date: 2004-09-01 -0800
comments: true
disqus_identifier: 1109
categories: []
---
Recently I blabbed on and on about how to [create a sane build
process](http://haacked.com/archive/2004/08/26/978.aspx). One question
I've heard in the past is what's the point of a setting up a big formal
build process when you have a very small project, perhaps with a team of
one or two?

Well, I'd have to say there is no point to a BIG FORMAL build process
for a small project. Rather, the build process should match the size and
needs of your project and team. However, I will say this. Start early,
because before you know it, your project and team will get big and
you'll be glad you have a build process in place. In the early stages, a
simple [NAnt](http://nant.sourceforge.net/) (or MSBuild) script will
suffice. Over time, that script will grow and grow. That's exactly what
I'm starting off with for Rss Bandit.

At this point, the script simply gets the latest version of the source
code from CVS into a clean directory, compiles the code, and generates a
compiled help file (.chm) using [NDoc](http://ndoc.sourceforge.net/).

I plan to add a task to run unit tests, perform an FxCop analysis, and
increment version numbers. However, I need to discuss version numbering
with Torsten and Dare first. Eventually, I hope to add CruiseControl.NET
integration. The purpose of this is to gain some experience with CCNET
since I [can't yet use it at
work](http://haacked.com/archive/2004/08/27/984.aspx).

**Please Help!**\
So this is all great and dandy, but the build file doesn't work. I'm not
terribly familiar with CVS, so if anybody can help me get this working,
I'll check it in to the CVS repository for RSS Bandit.

Get the [BUILD FILE HERE](http://haacked.com/code/default.build.zip).

