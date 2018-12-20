---
title: ".NET 3.5 SP1 Beta and Its Effect on MVC"
date: 2008-05-12 -0800
tags: [aspntemvc,dotnet]
redirect_from: "/archive/2008/05/11/sp1-beta-and-its-effect-on-mvc.aspx/"
---

The [news is
out](http://weblogs.asp.net/scottgu/archive/2008/05/12/visual-studio-2008-and-net-framework-3-5-service-pack-1-beta.aspx ".NET 3.5 SP1"),
the beta for the Visual Studio 2008 and the .NET Framework 3.5 Service
Pack has been released. As it relates to ASP.NET MVC, there are two
important points to notice about the SP1 release:

-   ASP.NET MVC is *not* included
-   URL Routing *is* included

Now you can see why there’s been so much [focus on
Routing](https://haacked.com/archive/2008/04/10/upcoming-changes-in-routing.aspx "Upcoming changes in Routing")
from the MVC team, as Routing is now part of the Framework and is not
out-of-band. This meant that we had to put a lot more effort into
Routing to make sure it was production ready.

ASP.NET MVC continues to be an out-of-band release. With the Routing
code now effectively complete, you should hopefully start to see a lot
more progress in MVC as our development team can focus almost
exclusively on MVC.

### MVC Preview 2 Apps Are Affected By SP1

Another important thing to note is that installing SP1 Beta installs the
`System.Web.Routing` and `System.Web.Abstractions` assemblies into the
GAC (Global Assembly Cache). These assemblies have the same exact
assembly version as the ones we released as part of Preview 2.

What this means is that even though you might have a direct reference to
the Preview 2 assemblies, when your app runs, it will still load those
assemblies from the GAC.

We’ve posted the workaround for Preview 2 applications at [the bottom of
the page
here](http://www.asp.net/downloads/3.5-extensions/Readme/ "Readme") in
the section entitled *Changes for ASP.NET MVC Preview 2*.

### April CodePlex Apps

If you’re running the April CodePlex app, you should be okay using the
version from SP1 Beta. There might be minor behavioral differences in
edge cases.

### Preview 3

MVC Preview 3 will not be affected by having SP1 installed. Preview 3
will include newer privately versioned bin deployable builds of the
Routing and Abstractions DLLs. That way these assemblies will not be
affected by the versions in the GAC installed by SP1, because they will
have different version numbers.

They don’t call it “Bleeding Edge” for nothing when playing with these
previews, but we are sorry for the inconvenience and Preview 3 should
make it better.

