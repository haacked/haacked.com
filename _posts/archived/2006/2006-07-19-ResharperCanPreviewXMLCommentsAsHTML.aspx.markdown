---
title: Resharper Can Preview XML Comments as HTML
tags: [tools]
redirect_from: "/archive/2006/07/18/ResharperCanPreviewXMLCommentsAsHTML.aspx/"
---

Golly gee whiz but you learn something new every day. I was a big fan of
[Quickdoc
viewer](http://www.kyrsoft.com/opentools/qdocviewer.html "QuickDoc Viewer site")
as a means to quickly view an HTML rendering of your XML comments
directly from within VS.NET.

The reason I say *was* is that I cannot for the life of me get that
sucker to work in VS.NET 2005 on my machine. I am also having problems
with [GhostDoc](http://www.roland-weigelt.de/ghostdoc/ "Ghost Doc"). It
seems any plugin that uses the new XML file based registration (instead
of the registry) doesn’t work for me. Perhaps because I am running as a
[LUA](http://en.wikipedia.org/wiki/Least_user_access "Least User Account")?

In any case, I just noticed that
[Resharper](http://www.jetbrains.com/resharper/ "Resharper") has a
similar feature, though not nearly as nice as Quickdoc. If you select a
method or class and hit CTRL+Q, it will show a window with a more
readable version of your XML comments. The following image demonstrates
the feature.

![](https://haacked.com/assets/images/resharper_docExample.png)

I am very happy to find this, but only because I can’t get QuickDoc to
work. Quickdoc is **far better** in how it renders the documentation in
pretty much the same way that
[NDoc](http://ndoc.sourceforge.net/ "NDoc") does. Maybe I should the
Jetbrains folk with a feature request to improve this a tad bit.

