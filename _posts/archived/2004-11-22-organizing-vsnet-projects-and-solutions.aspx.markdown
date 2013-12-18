---
layout: post
title: "Organizing VS.NET projects and Solutions"
date: 2004-11-22 -0800
comments: true
disqus_identifier: 1661
categories: []
---
Colin asks the question "[How do you organize your
code](http://www.jtleigh.com/people/colin/blog/archives/2004/11/how_do_you_orga_1.html)?"
and then goes on to describe the system in use at his shop.

Basically I adhere closely to the guidelines in the Patterns & Practices
guide: [Team Development with Visual Studio .NET and Visual
SourceSafe](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/tdlg_rm.asp).
[Chapter
3](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/tdlg_ch3.asp)
of this guide focuses on structuring projects and solutions.

The guide recommends a single solution model whenever possible which I
generally put in use with a slight modification. Chapter 3 talks about
composing your source control tree into Systems, Solutions, and
projects. So typically a large system might consist of only one
solution, but could consist of multiple solutions. Ideally each solution
is isolated from other solutions in that there are no project references
from one solution to another.

However I've added a separate system called CodeLibrarySystem which
contains a CodeLibrarySolution. When I create a new solution for a new
system, I'll add in the necessary projects from the CodeLibrarySolution
into the current system. So this breaks the "isolation" model of a
Single Solution Model, but provides the benefit of code sharing. Also,
by merely getting latest on my current solution, I can get all the
latest changes in the code library (which really shouldn't be changing
all that often).

At home and at work, I am constantly trying to refactor code so that it
can be dropped in the CodeLibrarySolution as opposed to having a bunch
of non-reusable code sitting in various solutions. This has worked out
pretty well for me as I'm starting to have a significant code library at
home. Any time I find an interesting example online, I add it to the
code library (with appropriate licensing information if any).

