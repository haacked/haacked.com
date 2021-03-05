---
title: Subtext 2.5 Released!
tags: [subtext]
redirect_from: "/archive/2010/06/05/subtext-2-5-released.aspx/"
---

Wow, has it already been over a year since the [last major version of
Subtext](https://haacked.com/archive/2008/11/27/subtext-2.1-security-update.aspx "Subtext 2.1.2 released")?
Apparently so.

Today I’m excited to announce the release of Subtext 2.5. Most of the
focus on this release has been under the hood, but there are some great
new features you’ll enjoy outside of the hood.

### Major new features

-   **New Admin Dashboard:** When you login to the admin section of your
    blog after upgrading, you’ll notice a fancy schmancy new dashboard
    that summarizes the information you care about in a single
    page.[![subtext-dashboard](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Subtext2.5Released_13148/subtext-dashboard_thumb.png "subtext-dashboard")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Subtext2.5Released_13148/subtext-dashboard_2.png)The
    other thing you’ll notice in the screenshot is the admin section
    received a face lift with a new more polished look and feel and
    ***many usability improvements***.
-   **Improved Search:**We’ve implemented a set of great search
    improvements. The biggest change is the work that** **[Simone
    Chiaretta](http://codeclimber.net.nz/ "Simone's Blog") did
    integrating Lucene.NET, a .NET search engine, as our built-in search
    engine. Be sure to check out his [tutorial on
    Lucene.NET](http://codeclimber.net.nz/archive/2009/08/31/lucene.net-the-main-concepts.aspx "Lucene.NET").
    Also, when clicking through to Subtext from a search engine result,
    we’ll show related blog posts. Subtext also implements the
    [OpenSearch
    API](http://www.opensearch.org/Home "OpenSearch provider").

### Core Changes

We’ve put in huge amounts of effort into code refactoring, bulking up
our unit test coverage, bug fixes, and performance improvements. Here’s
a sampling of some of the larger changes.

-   **Routing:** We’ve replaced the custom regex based URL handling with
    [ASP.NET
    Routing](http://msdn.microsoft.com/en-us/library/cc668201(v=VS.100).aspx "ASP.NET Routing")
    using custom routes based on the page routing work in ASP.NET 4.
    This took a lot of work, but will lead to better control over URLs
    in the long run.
-   **Dependency Injection:**Subtext now uses
    [Ninject](http://ninject.org/ "Ninject website"), an open source
    Dependency Injection container, for its Inversion of Control (IoC)
    needs. This improves the extensibility of Subtext.
-   **Code Reorganization and Reduced Assemblies:** A lot of work went
    into better organizing the code into a more sane and understandable
    structure. We also reduced the overall number of assemblies in an
    attempt to improve application startup times.
-   **Performance Optimizations:**We made a boat load of code focused
    performance improvements as well as caching improvements to reduce
    the number of SQL queries per request.
-   **Skinning Improvements:**This topic **[deserves its own blog
    post](https://haacked.com/archive/2010/06/06/subtext-skin-improvements.aspx "Subtext Skin Improvements")**,
    but to summarize, skins are now fully self contained within a
    folder. Prior to this version, adding a new skin required adding a
    skin folder to the */Skins* directory and then modifying a central
    configuration file. We’ve removed that second step by having each
    skin contain its own manifest, if needed. Most skins don’t need the
    manifest if they follow a set of skin conventions. For a list of
    Breaking changes, [check out our
    wiki](http://code.google.com/p/subtext/wiki/BreakingChangesSubtext25 "Breaking Changes").

### Upgrading

Because of all the changes and restructuring of files and directories,
upgrading is not as straightforward as it has been in the past.

To help with all the necessary changes, **[we’ve written a
tool](http://subtext.googlecode.com/files/SubtextUpgradeTool.exe "Subtext Upgrade Tool")**
that will attempt to upgrade your existing Subtext blog.

I’ve recorded a screencast that walks through [**how to upgrade a blog
to Subtext
2.5**](http://www.vimeo.com/12353661 "Screencast on How to upgrade Subtext")** **using
this new tool.

### Installation

Installation should be as easy and straightforward as always, especially
if you [install it using the Web Platform
Installer](http://www.microsoft.com/web/gallery/install.aspx?appsxml=http%3a%2f%2fwww.microsoft.com%2fweb%2fwebpi%2f2.0%2fWebApplicationList.xml&appid=Subtext "Install via Web PI")
(*Note, it may take up to a week for the new version to show up in Web
PI*). If you’re deploying to a host that supports SQLExpress, we’ve
included a freshly installed database in the App\_Data folder.

To install, [**download the zip file
here**](http://code.google.com/p/subtext/downloads/detail?name=SubText-2.5.zip&can=2&q= "Install Subtext 2.5")
and follow the [usual Subtext installation
instructions](http://subtextproject.com/Installing-and-Upgrading.ashx "Installing Subtext").

### More information

We’ll be updating our project website with more information about this
release in the next few weeks and I’ll probably post a blog post here
and there.

I’d like to thank the entire Subtext team for all their contributions.
This release probably contains the most diversity of patches and commits
of all our releases with lots of new people pitching in to help.

