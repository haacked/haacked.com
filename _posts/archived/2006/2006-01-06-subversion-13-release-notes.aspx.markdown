---
title: Subversion 1.3 Release Notes
date: 2006-01-06 -0800
disqus_identifier: 11421
tags: []
redirect_from: "/archive/2006/01/05/subversion-13-release-notes.aspx/"
---

Via [Larkware News](http://www.larkware.com/dg4/TheDailyGrind792.html) I
noticed that Subversion 1.3 has been released. Looking at the release
notes I noticed one thing in particular that caught my attention.

> **Official support for Windows '\_svn' directories (client and
> language bindings)**
>
> The "\_svn" hack is now officially supported: since some versions of
> ASP.NET don't allow directories beginning with dot (e.g., ".svn", the
> standard Subversion working copy administrative directory), the svn
> command line client and svnversion now treat the environment variable
> SVN\_ASP\_DOT\_NET\_HACK specially on Windows. If this variable is set
> (to any value), they will use "\_svn" instead of ".svn". We recommend
> that all Subversion clients running on Windows take advantage of this
> behaviour. Note that once the environment variable is set, working
> copies with standard ".svn" directories will stop working, and will
> need to be re-checked-out to get "\_svn" instead.

What this means for VS.NET developers using Subversion is that using
[Ankh](http://ankhsvn.tigris.org/) to provide Source Code Control
Integration (SCCI) becomes a more attractive option. One reason I held
off on using Ankh is that it required using a separate build of
Subversion. But now, I’m so comfortable using
[TortoiseSVN](http://tortoisesvn.tigris.org/ "Tortoise SVN") that I
prefer it to using source control bindings, so I probably won’t switch
just yet. The SCCI interface just doesn’t seem rich enough compared to
the turtle and its shell extensions.

