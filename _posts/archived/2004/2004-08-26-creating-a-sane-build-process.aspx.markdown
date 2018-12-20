---
title: Creating A Sane Build Process
date: 2004-08-26 -0800
tags:
- code
redirect_from: "/archive/2004/08/25/creating-a-sane-build-process.aspx/"
---

I’m not proud of it (well maybe just a little), but I once created an
*insane* build process once. If
[Pat](http://www.patrickgannon.net/ "Patrick Gannon") (who maintained
the build after me) posts in my comments, he’ll tell you about it. Take
a stew of a proprietary microcomputer flavor of Fortran written in the
70s by a programmer most assuredly clad in polyester, churn it through a
Visual Basic 6.0 preprocessor that spits out Fortran 90 code, all the
while correcting memory bound issues, mix it together by compiling it
with a custom NAnt fortran compiler task, and voila!, 20 or so compiled
Win32 fortran dlls. At this point, the process compiled and sprinkled in
some C\# code.

I’m not sure that build process will ever run on another machine other
than the one it runs on.

To create a *sane* build process, you need a *sane* development
environment. I’m sure there are many important principles of a sound
build process, but I have just one big one to impart for now.

The build must be location independent!
---------------------------------------

I can’t stress enough how important this principle is. I should be able
to walk into your office (assuming you’d let me) and perform the
following steps to get a fully working build on my machine.

-   Set up my computer
-   Hook it up to your source control system
-   Set the working folder to any old directory, say
    *J:\\IBetThisFolderIsNotOnYourMachine\\NorThisOne*
-   Get latest on a solution and open it up in VS.NET (or whichever IDE)
-   Compile it

If that doesn’t work **because you have hard coded file paths**, then
FOO on you! But let’s not stop there, I should then be able to run your
unit tests (what? You don’t have unit tests? A hex and octal on your
code!) and **they should all pass** on my machine (assuming they pass on
any machine).

But wait, I’m not done mucking around your office. Next, I should be
able to head to your build server, copy the folder that serves as the
root of your build process (or better yet, your CruiseControl.NET root)
to any folder on my machine, and run your NAnt (or MSBuild) script, and
have the system compile correctly and pass all unit tests.

How do you do it?
-----------------

At first, it takes a bit of practice to get to this point. For example,
there should not be a single hard-coded path in your code, nor in your
build scripts. Find every way to get them out of there. Here’s a few
tips for tricky situations you may run into.

### NUnit/MbUnit configuration file

UPDATE: This section was rewritten due to changes in Visual Studio.NET
2005

In VisualStudio.NET 2005, you can include an `App.config` file in the
root of any class library project. Compiling the project will
automatically copy and rename the file appropriately
(*AssemblyName.dll.config*) into the output directory. NUnit and MbUnit
will use the settings in this file to run the tests. Make good use of
this.

### Testing With External Resource Files

Suppose your unit tests rely on some external files for testing (like an
xml or html file to parse). If you store these files with the code, you
can’t be guaranteed that your unit test will find them when running on a
build server (since the directory structure may be quite different). You
also don’t want to put these files within bin/debug for the reasons
mentioned above.

Instead, follow Patrick Cauldwell’s lead and [embed the files as
resources](http://www.cauldwell.net/patrick/blog/PermaLink,guid,e9a1451b-108c-4da7-8be9-2b6c2316f7b1.aspx).
Now, your unit test can just unpack the file it needs into a known
relative location when it runs, achieving location independence.

Limitations
-----------

Of course, there are limitations to location independence. You’re
allowed a few assumptions. For example, in the scenario above where I
stomp into your office and take your source code, you can assume that
I’m running on the same platform you are and have a source control
client and IDE installed. Try to reduce these assumptions as much as
possible, but we have to agree on some basic axioms.

I’m working on a new build process right now, and I hope to make this
one a sane one. Maybe I’ll post examples later when I get done. We’ll
see.

