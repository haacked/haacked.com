---
layout: post
title: ASP.NET MVC 1.0 Release Candidate 2
date: 2009-03-02 -0800
comments: true
disqus_identifier: 18593
categories:
- asp.net mvc
- asp.net
redirect_from: "/archive/2009/03/01/aspnetmvc-changes-for-rc2.aspx/"
---

UPDATE: This post is outdated. ASP.NET MVC 1.0 [has been released
already](http://haacked.com/archive/2009/03/18/aspnet-mvc-rtw.aspx "ASP.NET MVC 1.0 Release")!

Today we’ve made the [**Release Candidate 2 for ASP.NET
MVC**](http://go.microsoft.com/fwlink/?LinkId=144443 "ASP.NET MVC RC 2 Download Page")** **available
for download.

This post will cover some of the changes with [ASP.NET
MVC](http://asp.net/mvc "ASP.NET MVC Website") we made in response to
internal and external feedback since our last [Release
Candidate](http://haacked.com/archive/2009/01/27/aspnetmvc-release-candidate.aspx "ASP.NET MVC Release Candidate").

Let me provide the quick and dirty summary, and then fill in the
details.

-   **Setup will now require .NET 3.5 SP1**
-   **Bin deployment to 3.5 host without SP1**[still
    possible](http://haacked.com/archive/2008/11/03/bin-deploy-aspnetmvc.aspx "Bin Deploying ASP.NET MVC")
-   **New server-only install mode**

Now onto the details

Setup Requires .NET Framework 3.5 SP1
-------------------------------------

The new installer will require that .NET Framework 3.5 SP1 be installed
on your machine. For your development environment, we recommend that you
also install Visual Studio 2008 SP1, but this is not required.

The reason we made this change is that we were including the
`System.Web.Routing.dll` and `System.Web.Abstractions.dll` assemblies
with the MVC installer. However, it does not make sense for us to
co-ship assemblies which are part of the Framework as this would
negatively affect our ability to service these two assemblies.

Bin deployment to 3.5 host without SP1 Still Possible
-----------------------------------------------------

We are not taking a runtime dependency on SP1 other than our existing
dependency on `System.Web.Routing.dll` and
`System.Web.Abstractions.dll`. Thus you can still bin deploy your
application to a hosting provider who has .NET 3.5 installed without SP1
by following [these
instructions](http://haacked.com/archive/2008/11/03/bin-deploy-aspnetmvc.aspx "Bin Deploy ASP.NET MVC").

Note that in such a configuration, you take on the risk of servicing
those assemblies. Should we release any important updates to any of
these assemblies, you’ll have to manually patch your application.
However, you will still enjoy full CSS (formerly PSS) support for this
configuration.

New Server-Only Install Mode
----------------------------

We’re adding an option to the installer that enables installing on a
server that does not have Visual Studio at all on the machine, which is
useful for production servers and hosting providers.

The installer will no longer block on a machine that does not have
Visual Studio installed. Instead, it will continue the standard MVC
installation without installing the Visual Studio templates. The
assemblies will still be installed into the GAC and native images will
also be generated.

Certain other requirements have also been relaxed. For example, if the
machine on which the installation is performed contains Visual Web
Developer Express Edition 2008 without SP1, the installation will still
proceed, but with a warning prompt. You can also automate this
installation by invoking the installer using the command line (all on
one line):

> `msiexec /i AspNetMvc-setup.msi /q /l*v .\mvc.log MVC_SERVER_INSTALL="YES"`

Summary
-------

As a result of these changes, we realized it would be prudent to have
one more public release candidate. As I mentioned, there are *very few
runtime and tooling* changes. Most of the changes are in the installer
and we want to make sure that the installer is rock solid before we call
it an RTM.

Based on all *your* feedback from the first Release Candidate, as well
as our own investigations and testing, we are confident that the Release
Candidate 2 will be solid and lead to a strong RTM.

In case you missed it above, here’s the **[link to the download
page](http://go.microsoft.com/fwlink/?LinkId=144443 "Download Link")**.
You can find out what else has changed [in the RC 2 release
notes](http://go.microsoft.com/fwlink/?LinkId=137662 "ASP.NET MVC RC 2 Release Notes").

And before I forget, as usual, we published the **[source code and our
MvcFutures assembly on
CodePlex](http://aspnet.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=24142#ReleaseFiles "ASP.NET MVC CodePlex")**.

