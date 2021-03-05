---
title: Cruise Control .NET Resources
tags: [ci]
redirect_from: "/archive/2004/08/16/ccnet-resources.aspx/"
---

Since I’m just getting started with Cruise Control, I thought I’d look
around the web and blogosphere and put together some resources on
configuring CruiseControl.NET.

-   [Cruise Control
    Hierarchy](http://pluralsight.com/wiki/default.aspx/Craig.CruiseControlDirectoryHierarchy)
    - Craig Andera describes how he likes to set up directories in
    CruiseControl.NET
-   [Setting Up Continuous
    Integration](http://blogs.geekdojo.net/joel/archive/2004/08/15/2827.aspx)
    - Joel outlines the steps he takes to set up CruiseControl.NET.
-   [Lessons Learned From Setting Up
    CruiseControl.NET](http://weblogs.ilg.com/ksyverstad/articles/155.aspx)
    - Kris Syverstad provides some lessons learned while trying to
    configure CruiseControl .NET and offers some tips.
-   [CruiseControl.NET from
    Scratch](http://joefield.mysite4now.com/blog/articles/146.aspx) -
    Joe Field writes a guide to getting started with CruiseControl.NET.

And there’s also the CruiseControl.NET [site
itself](http://ccnet.thoughtworks.com/) and the [community
site](http://confluence.public.thoughtworks.org/display/CCNETCOMM/Home).

After reading through many of these resources, I have a question about
directory structures. You see, I try to be an obedient Microsoft
developer (will it pay off?) and set up my directories as outlined in
the Microsoft Patterns & Practices article [Team Development with Visual
Studio .NET and Visual
SourceSafe](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/tdlg_rm.asp).

The article proposes that you group code into "Systems" which may
contain one or more VS.NET Solutions. A Solution of course may contain
one or more Projects. Below is figure 3.5 from the article illustrating
the directory structure.

![Directory Structure](/assets/images/FolderStructure.gif) \
 Figure 3.5. Visual Studio .NET and VSS Folder Structure

In general, projects and solutions won’t be shared across systems, i.e.
a solution in one system won’t reference a project in another. However I
do have one exception in that I have a code library system with projects
that other solutions may reference. For example:

    Projects
        CodeLibrarySystem
            CodeLibrarySolution
                Project1
                Project2

        SomeOtherSystem
            SomeSolution
                Project1
                Project3

So, in my case, should I map a CruiseControl.NET project to a System or
a Solution? Any recommendations?

