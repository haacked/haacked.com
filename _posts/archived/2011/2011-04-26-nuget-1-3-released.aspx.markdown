---
title: NuGet 1.3 Released
tags: [nuget,code,oss]
redirect_from: "/archive/2011/04/25/nuget-1-3-released.aspx/"
---

In continuing our efforts to [release early, release
often](https://haacked.com/archive/2011/04/20/release-early-and-often.aspx "Release Early, Release Often"),
I’m happy to announce [the release of NuGet
1.3](http://nuget.org/announcements/nuget-1.3-released "NuGet 1.3 Released")!

**Upgrade!**If you go into Visual Studio and select *Tools* \>
*Extension Manager*, click on the Update tab and you’ll see that this
new version of NuGet is available as an update. Click the *Upgrade*
button and you’re all set. It only takes a minute and it really is that
easy to upgrade.

[![Extension
Manager](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/NuGet-1.3-Released_111EC/Extension%20Manager_thumb.png "Extension Manager")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/NuGet-1.3-Released_111EC/Extension%20Manager_2.png)

As always, there’s a new version of NuGet.exe corresponding with this
release as well as a new version of the Package Explorer. If you have a
fairly recent version of NuGet.exe, you can upgrade it by simply running
the following command:

`NuGet.exe u`

![076583
fg1019](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/NuGet-1.2-Released_142C7/076583%20fg1019_3.png "076583 fg1019")

Expect a new version of Package Explorer to be released soon as well. It
is a click once application so all you need to do is open it and it will
prompt you to upgrade it when an upgrade is available.

There’s a lot of cool improvements and bug fixes in this release as you
can see in the release announcement. One of my favorite features is the
ability to quicky create a package from a project file (csproj, vbproj)
and push the package with debugging symbols to the server. [David
Ebbo](http://blog.davidebbo.com/ "David Ebbo's Blog") wrote a great
[blog post about this
feature](http://blog.davidebbo.com/2011/04/easy-way-to-publish-nuget-packages-with.html "Easy way to publish NuGet Packages")
and [Scott Hanselman](http://hanselman.com/ "Scott Hanselman's Blog")
and I demonstrated this feature 20 minutes into our [NuGet in Depth talk
at Mix
11](http://channel9.msdn.com/events/MIX/MIX11/FRM09 "NuGet in Depth").

