---
title: ASP.NET MVC Installer For Visual Studio 2010 Beta 1 And Roadmap
tags: [aspnetmvc]
redirect_from: "/archive/2009/06/07/aspnetmvc-vs10beta1-roadmap.aspx/"
---

A little while ago I announced our plans for [ASP.NET
MVC](http://asp.net/mvc "ASP.NET MVC Website") as it relates to Visual
Studio 2010. ASP.NET MVC wasn’t included as part of Beta 1, which raised
a few concerns by some (if not conspiracy theories!) ;). The reason for
this was simple as I pointed out:

> One thing you’ll notice is that ASP.NET MVC is not included in Beta 1.
> The reason for this is that Beta 1 started locking down before MVC 1.0
> shipped. ASP.NET MVC will be included as part of the package in VS10
> Beta 2.
>
> …
>
> We’re working hard to have an out-of-band installer which will install
> the project templates and tooling for ASP.NET MVC which works with
> VS2010 Beta 1 sometime in June on CodePlex. Sorry for the
> inconvenience. I’ll blog about it once it is ready.

Today I’m happy to announce that we’re done with the work I described
and the installer is [**now available on
CodePlex**](http://aspnet.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=28527 "ASP.NET MVC For Visual Studio 2010 Beta 1").
Be sure to give it a try as many of the new VS10 features intended to
support the TDD workflow fit very nicely with ASP.NET MVC, which
[ScottGu](http://weblogs.asp.net/scottgu "Scott Guthrie's Blog") will
describe in an upcoming blog post.

If you run into problems with the intaller, try out this
[troubleshooting guide by
Jacques](http://weblogs.asp.net/jacqueseloff/archive/2009/06/09/troubleshooting-the-mvc-installer-for-visual-studio-2010-beta-1.aspx "Troubleshooting the MVC for VS10 Beta 1 installer"),
the developer who did the installer work and do provide feedback.

You’ll notice that the installer says this is ASP.NET MVC *1.1*, but as
the readme notes point out, this is really ASP.NET MVC *1.0* retargeted
for Visual Studio 2010. The 1.1 is just a placeholder version number. We
bumped up the version number to avoid runtime conflicts with ASP.NET MVC
1.0. All of this and more is described in the [**Release
Notes**](http://aspnet.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=28527#DownloadId=71127 "Release Notes").

When VS10 Beta 2 comes out, you won’t need to download a separate
standalone installer to get ASP.NET MVC (though a standalone installer
will be made available for VS2008 users that will run on ASP.NET 3.5
SP1). A pre-release version of ASP.NET MVC 2 will be included as part of
the Beta 2 installer as described in the …

Roadmap
-------

[![Road Blur: Photo credit: arinas74 on
stock.xchng](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ASP.NETMVCInstallerForVisualStudio2010Be_7EAC/fast-road_3.jpg "Road Blur. Photo credit: arinas74 on stock.xchng")](http://www.sxc.hu/photo/1158482 "Photo credit: arinas74 on stock.xchng")

I recently published the [**Roadmap for ASP.NET MVC
2**](http://aspnet.codeplex.com/Wiki/View.aspx?title=Road%20Map&referringTitle=Home "ASP.NET MVC Roadmap")
which gives a high level look at what features we plan to do for ASP.NET
MVC 2. The features are noticeably lacking in details as we’re deep in
the planning phase trying to gather pain points.

Right now, we’re avoiding focusing the implementation details as much as
possible. When designing software, it’s very easy to have preconceived
notions about what the solution should be, even when we really don’t
have a full grasp of the problem that needs to be solved.

Rather than guiding people towards what we think the solution is, I hope
to focus on making sure we understand the problem domain and what people
want to accomplish with the framework. That leaves us free to try out
alternative approaches that we might not have considered before such as
[alternatives to expression based URL
helpers](https://haacked.com/archive/2009/06/02/alternative-to-expressions.aspx "Alternative Approach to strongly typed helpers").
Maybe the alternative will work out, maybe not. Ideally, I’d like to
have several design alternatives to choose from for each feature.

As we get further along the process, I’ll be sure to flesh out more and
more details in the Roadmap and share them with you.

Snippets
--------

One cool new feature of VS10 is that snippets now work in the HTML
editor. Jeff King from the [Visual Web Developer
team](http://blogs.msdn.com/webdevtools/ "Visual Web Developer Team Blog")
sent me the snippets we plan to include in the next version. They are
also downloadable from the [CodePlex release
page](http://aspnet.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=28527 "Release page").
Installation is very simple:

> Installation Steps:
>
> ​1) Unzip "ASP.NET MVC Snippets.zip" into
> "C:\\Users\\\<username\>\\Documents\\Visual Studio 10\\Code
> Snippets\\Visual Web Developer\\My HTML Snippets", where "C:\\" is
> your OS drive. \
> 2) Visual Studio will automatically detect these new files.

Try them out and let us know if you have ideas for snippets that will
help you be more productive.

Important Links:

-   [CodePlex Download
    Page](http://aspnet.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=28527 "ASP.NET MVC For Visual Studio 2010 Beta 1")
-   [Release
    Notes](http://aspnet.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=28527#DownloadId=71127 "ASP.NET MVC For Visual Studio 2010 Beta 1 Release Notes")
    (also linked to from above page)
-   [ASP.NET MVC 2
    Roadmap](http://aspnet.codeplex.com/Wiki/View.aspx?title=Road%20Map&referringTitle=Home "ASP.NET MVC 2 Roadmap")
-   [Troubleshooting Info for this
    installer](http://weblogs.asp.net/jacqueseloff/archive/2009/06/09/troubleshooting-the-mvc-installer-for-visual-studio-2010-beta-1.aspx "Troubleshooting the MVC for VS10 Beta 1 installer")


