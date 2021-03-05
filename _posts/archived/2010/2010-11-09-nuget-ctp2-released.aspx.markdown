---
title: NuGet CTP 2 Released!
tags: [nuget,code]
redirect_from: "/archive/2010/11/08/nuget-ctp2-released.aspx/"
---

My team has been hard at work the past few weeks cranking out code and
today we are releasing the second preview of
[NuGet](http://nuget.codeplex.com/ "NuGet on CodePlex.com") (which you
may have heard referred to as NuPack in the past, but was [renamed for
CTP
2](https://haacked.com/archive/2010/10/29/nupack-is-now-nuget.aspx "NuPack is now NuGet")
by the community). If you’re not familiar with what NuGet is, please
read my [introductory blog post on the
topic](https://haacked.com/archive/2010/10/06/introducing-nupack-package-manager.aspx "Introduction to NuGet").

For a detailed list of what changed, check out the [**NuGet Release
Notes**](http://nuget.codeplex.com/wikipage?title=NuGet%201.0%20Release%20Notes "NuGet Release Notes").

To see NuGet in action, watch the talk that [Scott
Hanselman’s](http://hanselman.com/blog/ "Scott Hanselman's Blog") gave
at the Professional Developer’s Conference which was the highest rated
talk. You can [watch it
online](http://player.microsoftpdc.com/Session/e0c3ce51-9869-456c-a197-63dc0283f57e "Watch it online")
or [download it in
HD](http://videoaz.microsoftpdc.com/vod/downloads/FT01_High.mp4 "HD MP4").

How do I get it?
----------------

There are three ways to get NuGet CTP 2.

### Via MVC 3

NuGet CTP 2 is included as part of the ASP.NET MVC 3 Release Candidate
installation (**[install via Web
PI](http://www.microsoft.com/web/gallery/install.aspx?appid=MVC3 "Install ASP.NET MVC 3 via Web PI")**
or download [the standalone
installer](http://go.microsoft.com/fwlink/?LinkID=191797 "ASP.NET MVC 3 RC installer"))
. So when you install ASP.NET MVC 3 RC, you’ll have NuGet installed.

### Via the Visual Studio Extension Gallery

If you want to try out NuGet without installing ASP.NET MVC 3 RC, feel
free to install it via the Visual Studio Extension Gallery.

### Via CodePlex.com

As with all of our releases, we also make the download [available on our
CodePlex
website](http://nuget.codeplex.com/releases/view/52017 "NuGet v1 CTP 2").

What’s new?
-----------

As the release notes point out, we’ve made a lot of improvements. Some
of the big ones are changes to the [NuSpec package
format](http://nuget.codeplex.com/documentation?title=Nuspec%20Format "NuSpec Package Format"),
so if you have any old *.nupkg* files laying around, you’ll need to
build them with the new [CTP 2 NuGet.exe command line
tool](http://nuget.codeplex.com/releases/52017/download/165468 "NuGet command line tool").

But to be nice, we already updated all the packages in the temporary
feed which is at a new location now, so you won’t need to do that. But
if you’re building new packages, be sure to update your copy of
Nuget.exe.

The NuSpec format now includes two new fields you should take advantage
of if you are creating packages:

-   The `iconUrl` field specifies the URL for a 32x32 png icon that
    shows up next to your package entry within the Add Package Dialog.
    Be sure to set that to distinguish your package.
-   The `projectUrl` field points to a web page that provides more
    information about your package.

Another big change we made is that the package feed is now an [Open Data
Protocol (OData) Service Endpoint](http://www.odata.org/ "OData"). This
makes it easy for clients to write  arbitrary queries using LINQ against
an `IQueryable` interface which is automatically translated to the
proper query URL. For example, to see the first 10 packages that start
with “N”:

[http://feed.nuget.org/current/odata/v1/Packages?\$filter=startswith(Id,'N')
eq
true&\$top=10](http://feed.nuget.org/current/odata/v1/Packages?$filter=startswith(Id,'N')%20eq%20true&$top=10 "OData query")

Also, when using the Powershell based Package Manager Console, be sure
to note that we renamed the `Add-Package` command to `Install-Package`
and the `Remove-Package` command to `Uninstall-Package`. We felt the new
names conveyed the right semantics.

How’s things?
-------------

So far, the project has been a lot of fun to work on, in large part due
to the enthusiasm and excitement that we’ve seen from the community. As
I mentioned in the past, this is truly an Open Source project and we’ve
had quite a few community code contributions.

Of course, we still have plenty of items [up for
grabs](https://haacked.com/archive/2010/10/14/nupack-up-for-grabs-items.aspx "NuGet Up For Grabs")
if you’re looking for something to work on.

### ReviewBoard

One cool thing we’ve done is integrated the use of ReviewBoard for doing
code reviews into our process. For information on that, check out our
[code review
instructions](http://nuget.codeplex.com/wikipage?title=Code%20Reviews "NuGet Code Reviews").
Our review board is currently hosted at
[http://reviewboard.nupack.com/](http://reviewboard.nupack.com/) but
that domain name will change soon.

### Continuous Integration

For those of you who like life in the fast lane, we do have a Team City
based Continuous Integration (CI) server hosted at
[http://ci.nuget.org:8080/](http://ci.nuget.org:8080/). You can get
daily builds compiled directly from our source tree. So for those of you
who knew about the build server, you would have been playing with the
CTP 2 for a while now. ![Winking
smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/NuGet-CTP-2-Released_9810/wlEmoticon-winkingsmile_2.png)

What’s next?
------------

Well our next release is going to be NuGet version 1.0 RTM. A lot of our
focus for this iteration will be on applying some spit and polish as
well as integration work on our sister project, [Gallery
Server](http://galleryserver.codeplex.com "Gallery Server").

The Gallery Server project is building what will become the official
gallery for NuGet (as well as for Orchard modules and other types of
galleries). It’s being developed as an Open Source project as well so
that anyone can take the source and host their own galleries.

Once the gallery server is completed and hosted, we’ll start to
transition from our current temporary feed over to the gallery server.
We’ll leave the temporary feed up for a while to allow people time to
transition over to whatever the final official gallery location ends up
at.

At this point, if you haven’t tried NuGet, give it a try. If you have
tried it, let us know what you think. I hope you enjoy using it, I know
I do.
![Smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/NuGet-CTP-2-Released_9810/wlEmoticon-smile_2.png)

