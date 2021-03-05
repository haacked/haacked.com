---
title: ASP.NET MVC 3 Release Candidate 2
tags: [aspnet,aspnetmvc,nuget,code]
redirect_from: "/archive/2010/12/09/asp-net-mvc-3-release-candidate-2.aspx/"
---

Almost [exactly one month
ago](https://haacked.com/archive/2010/11/09/asp-net-mvc-3-release-candidate.aspx "ASP.NET MVC 3 RC"),
we released the Release Candidate for ASP.NET MVC 3. And today we learn
why we use the term “Candidate”.

As Scott writes, [Visual Studio 2010 SP1 **Beta** was
released](http://www.hanselman.com/blog/VisualStudioExplosionVS2010SP1BETAReleasedAndContext.aspx "Visual Studio Explosion")
just this week and as we were testing it we found a few
incompatibilities with it and the ASP.NET MVC 3 RC that we had just
released.

![newdotnetlogo\_2](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/e8a61f0bb792_82AE/newdotnetlogo_2_c9dd42e8-7ef8-4ec3-ba52-ec5615b32fe6.png "newdotnetlogo_2")That’s
when we, in the parlance of the military, scrambled the jets to get
another release candidate prepared.

You can [**install it
directly**](http://www.microsoft.com/web/gallery/install.aspx?appid=MVC3 "Install ASP.NET MVC 3 RC 2 via Web PI")
using the Web Platform Installer (Web PI) download the installer
yourself from [from
here](http://go.microsoft.com/fwlink/?LinkID=191799 "Download Page for ASP.NET MVC 3 RC 2").

Be sure to **[read the release
notes](http://www.asp.net/learn/whitepapers/mvc3-release-notes "ASP.NET MVC 3 RC 2 Release Notes")**
for known issues and breaking changes in this release. I’m not saying I
put an Easter egg in there or not, but you’ll have to read all the notes
to find out.

In particular, there are two issues I want to call out.

Breaking Change Alert!
----------------------

The first is a breaking change. Remember way back when I wrote about
[Dynamic Methods in
ViewData](https://haacked.com/archive/2010/08/02/dynamic-methods-in-view-data.aspx "Dynamic Methods in ViewData")?
Near the end of that post I wrote an Addendum about the property name
mismatch between `ViewModel` and `View`.

Well we finally resolved that mismatch. The new property name, both in
the controller and in the view is `ViewBag`. This may break many of your
existing ASP.NET MVC 3 pre-release applications.

NuGet Upgrade Alert
-------------------

The other issue I want to call out is that if you already have NuGet
installed, running the ASP.NET MVC 3 RC 2 installer will not upgrade it.
Instead, you need to go to the Visual Studio Extension Manager dialog
(via the *Tools* | *Extensions* menu option) and click on the Updates
tab. You should see NuGet listed there (click on image for larger
image):

[![extension-manager](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/e8a61f0bb792_82AE/extension-manager_thumb.png "extension-manager")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/e8a61f0bb792_82AE/extension-manager_2.png)

The NuGet.exe command line tool for creating packages is [available on
CodePlex](http://nuget.codeplex.com/releases/view/52018 "NuGet 1.0 Release Candidate Download").

Overall, this release consists mostly of bug fixes along with some fit
and finish work for ASP.NET MVC 3. We’ve updated the version of jQuery
and jQuery Validation that we include in the project templates and now
also include [jQuery UI](http://jqueryui.com/ "jQuery UI homepage"), a
library that builds on top of jQuery to provide animation, advanced
effects, as well as themeable widgets.

In terms of NuGet, this release contains a significant amount of work.
I’ll try and follow up soon with more details on the NuGet release along
with release notes.

