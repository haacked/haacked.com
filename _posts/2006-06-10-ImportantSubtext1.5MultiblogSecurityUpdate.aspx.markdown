---
layout: post
title: "Important Subtext 1.5 Multiblog Security Update"
date: 2006-06-10 -0800
comments: true
disqus_identifier: 13214
categories: [subtext]
---
If you are hosting multiple blogs on a single installation of Subtext,
the recent Subtext 1.5 release unfortunately introduces a security bug
that will allow an admin of one blog to login to another blog. The fix
has already been [posted to
Sourceforge](http://sourceforge.net/project/showfiles.php?group_id=137896 "Subtext 1.5.1")
as part of the Subtext 1.5.1 release.

If you already upgraded to Subtext 1.5, you only need to update the
`Subtext.Framework.dll` file in the `bin` directory. The fix was a one
line code change. I apologize for the inconvenience and for the mistake.
Please spread the word.

Tags: [Subtext](http://haacked.com/tags/subtext/default.aspx)

