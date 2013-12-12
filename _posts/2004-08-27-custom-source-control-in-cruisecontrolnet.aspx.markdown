---
layout: post
title: "Custom Source Control In CruiseControl.NET?"
date: 2004-08-27 -0800
comments: true
disqus_identifier: 984
categories: []
---
I'm in the unlucky position that CruiseControl.NET doesn't support the
source control provider (Seapine Surround SCM) we use here at work.
Briefly looking at the source code for CCNET, I noticed that I could
create support for Surround SCM by implementing the ISourceControl
interface via inheriting the ProcessSourceControl.cs class. However,
before I go down that road, does anyone know if I can add a custome
source control provider as a plug-in?

For example, if you want to use a build tool other than NAnt or
Devenv.exe, you can create a builder plug-in by following [these
instructions](http://confluence.public.thoughtworks.org/display/CCNET/Custom+Builder+Plug-in).
Will that work for creating a custom source control plugin? (Of course
I'd be replacing IBuilder with ISourceControl or
ProcessSourceControl.cs).

I'd prefer not to compile my update into the main code branch as I don't
want to maintain a variant of CruiseControl.NET. Likewise, I don't want
to write this plug-in if someone else already has one out there. It
might be a better use of my time to convince my dept to switch SCM
tools. ;)

