---
title: 'Subtext Issue: Missing Emoticons File'
tags: [subtext]
redirect_from: "/archive/2006/03/03/SubtextIssueMissingEmoticonsFile.aspx/"
---

UPDATE: Ok, this is totally my fault. I took a perfectly good NAnt
script another developer wrote and tried to add a few things in there
and made a dumb error. I should have a unit test for our NAnt script. ;)
I’ll write up a post later describing the issue.

So I guess [my fears of the
release](https://haacked.com/archive/2006/03/03/ReflectionsOnTheRelease.aspx "Reflections on the Release")
weren’t totally out of order. The first major bug report has come in.
Fortunately it is an extremely easy fix.

The emoticons.txt file appears to be missing from the webroot in the
distribution package. I looked at our codebase, and it is there. I run
an NAnt script which automates creating a distribution package. I see
the line where it is supposed to add the emoticons.txt file into the
package, but it has decided that it would rather not. I need to dig into
this.

In the meanwhile, I just updated the distribution in SourceForge. For
those of you who already downloaded Subtext, please download and [unzip
this file](https://haacked.com/assets/images/emoticons.zip "emoticons file")
into the root directory of your Subtext site.

For those of you about to download Subtext, [SourceForge has the
corrected
version](http://prdownloads.sourceforge.net/subtext/Subtext_1.0.0.2_INSTALL.zip?download "Download Subtext").

