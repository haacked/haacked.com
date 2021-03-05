---
title: ASP.NET MVC 3 and NuGet 1.0 Released (Including Source Code!)
tags: [aspnet,aspnetmvc,code,oss,nuget]
redirect_from: "/archive/2011/01/12/aspnetmvc3-released.aspx/"
---

The changing of the year is a time of celebration as people reflect
thoughtfully on the past year and grow excited with anticipation for
what’s to come in the year ahead.

Today, there’s one less thing to anticipate as we announce the final
release of ASP.NET MVC 3 and NuGet 1.0!

[![double-rainbow](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ASP.NET-MVC-3-RTM_11308/double-rainbow_3.jpg "double-rainbow")](http://www.flickr.com/photos/nicholas_t/281820290/ "Double Rainbow: CC by attribution")
\
*Oh yeah, this never gets old.*

**[Install it via Web Platform
Installer](http://www.microsoft.com/web/gallery/install.aspx?appid=MVC3 "Install ASP.NET MVC 3 via Web PI")**
or [download the installer
directly](http://go.microsoft.com/fwlink/?LinkID=208140 "Download Details Page")
to run it yourself.

Here are a few helpful resources for learning more about this release:

-   [What’s New in ASP.NET MVC
    3](http://asp.net/mvc/mvc3#overview "What's new in ASP.NET MVC 3")
-   [ASP.NET MVC 3 Release
    Notes](http://www.asp.net/learn/whitepapers/mvc3-release-notes "ASP.NET MVC 3 Release Notes")
-   [MSDN
    Documentation](http://go.microsoft.com/fwlink/?LinkId=205717 "ASP.NET MVC 3 MSDN Docs")
-   [NuGet Project
    Homepage](http://nuget.codeplex.com/ "NuGet Project Homepage")
-   The [MVC Music Store
    Tutorial](http://www.asp.net/mvc/tutorials/mvc-music-store-part-1 "MVC Music Store")
    and associated [CodePlex
    project](http://mvcmusicstore.codeplex.com "MVC Music Store CodePlex Project").

Those links will provide more details about what’s new in ASP.NET MVC 3,
but I’ll give a quick bullet list of some of the deliciousness you have
to look forward to. Again, visit the links above for full details.

-   [Razor view
    engine](http://weblogs.asp.net/scottgu/archive/2010/07/02/introducing-razor.aspx "Razor View Engine")
    which provides a very streamlined syntax for writing clean and
    concise views.
-   Improved support for [Dependency
    Injection](http://bradwilson.typepad.com/blog/2010/07/service-location-pt1-introduction.html "Service Location in ASP.NET MVC")
-   Global Action Filters
-   jQuery based [Unobtrusive
    Ajax](http://bradwilson.typepad.com/blog/2010/10/mvc3-unobtrusive-ajax.html "Unobtrusive Ajax")
    and [Client
    Validation](http://bradwilson.typepad.com/blog/2010/10/mvc3-unobtrusive-validation.html "Unobtrusive Validation").
-   `ViewBag` property for dynamic access to `ViewData`.
-   Support for view engine selection in the New Project and Add View
    dialog
-   And much more!

For those of you wishing to upgrade an ASP.NET MVC 2 application to
ASP.NET MVC 3, check out Marcin Dobosz’s post about our [ASP.NET MVC 3
Projct upgrader
tool](http://blogs.msdn.com/b/marcinon/archive/2011/01/13/mvc-3-project-upgrade-tool.aspx "MVC 3 Project Upgrader").
The tool itself can be found on [our CodePlex
website](http://aspnet.codeplex.com/releases/view/59008 "ASP.NET MVC 3 Project Upgrader").

NuGet 1.0 RTM
-------------

Also included in this release is the 1.0 release of NuGet. I’ll let you
in on a little secret though, if you upgraded NuGet via the Visual
Studio Extension Gallery, then you’ve been running the 1.0 release for a
little while now.

If you already have an older version of NuGet installed, the ASP.NET MVC
3 installer cannot upgrade it. Instead launch the VS Extension manager
(within Visual Studio go to the *Tools* menu and select *Extension
Manager*) and click on the *Updates* tab.

Just recently we announced the Beta release of our NuGet Gallery.
Opening the door to the gallery will make it very easy to publish
packages, so what are you waiting for!?

-   [Introducing the NuGet
    Gallery](http://blog.davidebbo.com/2011/01/introducing-nuget-gallery.html "Introducing the NuGet Gallery")
-   [Uploading Packages to the NuGet
    Gallery](https://haacked.com/archive/2011/01/12/uploading-packages-to-the-nuget-gallery.aspx "Uploading Packages")

At this point I’m obligated to point out that everything about NuGet is
open source and we’re [always looking for
contributors](https://haacked.com/archive/2010/10/14/nupack-up-for-grabs-items.aspx "NuGet UpForGrabs Items").
If you’re interested in contributing, but are finding impediments to it,
let us know what we can improve to make it easier to get involved.
Here’s the full list of OSS projects that make up the NuGet client and
the server piece:

-   [http://nuget.codeplex.com/](http://nuget.codeplex.com/) This the
    project I run.
-   [http://orchardgallery.codeplex.com/](http://orchardgallery.codeplex.com/)
    This project is an Orchard module that powers the front-end of
    Nuget.org (and the upcoming Orchard Gallery). Brad Millington of the
    Orchard Project heads that up.
-   [http://galleryserver.codeplex.com/](http://galleryserver.codeplex.com/)
    powers the back-end services such as our OData package feed and the
    WCF endpoints for publishing packages.

Show Me The Open Source Code!
-----------------------------

As we did with ASP.NET MVC 1.0 and ASP.NET MVC 2, the source for the
ASP.NET MVC 3 assembly is being released under the OSI certified Ms-PL
license. The [Ms-PL licensed source
code](http://download.microsoft.com/download/3/4/A/34A8A203-BD4B-44A2-AF8B-CA2CFCB311CC/mvc3-rtm-mspl.zip "ASP.NET MVC 3 Source Code under Ms-PL")
is available as a zip file at the download center.

If you’d like to see the source code for ASP.NET Web Pages and our MVC
Futures project, we posted [that on CodePlex.com
too](http://aspnet.codeplex.com/releases/view/58781 "Source Code").

What’s Next?
------------

So what’s next? Well you can probably count as well as I can, so it’s
time to start getting planning for ASP.NET MVC 4 and NuGet 2.0 in full
gear. Though this time around, with NuGet now available, we have the
means to easily distribute a lot of smaller releases throughout the year
as packages, with the idea that many of these may make their way back
into the core product. I’m sure you’ll see a lot of experimentation in
that regard.

