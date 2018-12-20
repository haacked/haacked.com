---
title: 'Urgent: Subtext Security Patch'
date: 2007-09-20 -0800
tags:
- personal
redirect_from: "/archive/2007/09/19/urgent-subtext-security-patch.aspx/"
---

UPDATE: We [released Subtext
2.0](https://haacked.com/archive/2008/08/10/subtext-2.0-released.aspx "Subtext 2.0")
which also includes the fix for this vulnerability among many other bug
fixes.

A Subtext user reported a security vulnerability due to a flaw in our
integration with the FCKEditor control which allows someone to upload
files into the images directory without being authenticated.

As far as we know, nobody has been seriously affected, but please update
your installation as soon as possible. Our apologies for the
inconvenience.

The fix should be relatively quick and painless to apply.

**The Fix**

If you’re running Subtext 1.9.\* we have a fix available consisting of a
single assembly, *Subtext.Providers.BlogEntryEditor.FCKeditor.dll*.
After you [download it (*Subtext1.9.5-PATCH.zip
7.72KB*)](http://downloads.sourceforge.net/subtext/Subtext1.9.5-PATCH.zip?use_mirror=easynews "Subtext 1.9.5 Patch")
, unzip the assembly (*I recommend backing up your old one just in
case*) and copy it into your bin directory.

**Alternative Workaround**

If you’re running a customized version and the above patch causes
problems, you can workaround this issue by backing up and then
temporarily removing the following directory in your installation.

`Providers\BlogEntryEditor\FCKeditor\editor\filemanager`

**Notes**

The Subtext team takes security very seriously and we regret that this
flaw made it into our system. We appreciate that a user discretely
brought it to our attention and worked quickly to create and test a
patch. I went ahead and updated the release on
[SourceForge](http://sourceforge.net/projects/subtext/ "Subtext SourceForge project site")
(if you’ve downloaded Subtext-1.9.5b then you’re safe) so that no new
downloads are affected.

The code also has been fixed in Subversion in case you’re running a
custom built version of Subtext.

I will follow up with a post later describing the issue in more detail
and what we plan to do to mitigate such risks in the future. I’ll also
write a post outlining general guidelines for reporting and handling
security issues in an open source project based on guidance provided by
the Karl Fogel book, *[Producing Open Source
Software](https://haacked.com/archive/2006/01/16/RunningAnOpenSourceProject.aspx "Running an open source project")*.

Again, I am sorry for any troubles and inconvenience this may have
caused. If you know any Subtext users, please let them know. I’ll be
updating the website momentarily.

**Download**

Again, [here is the patch
location](http://downloads.sourceforge.net/subtext/Subtext1.9.5-PATCH.zip?use_mirror=easynews "Download the patch").

