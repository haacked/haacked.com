---
title: NuGet 1.1 Released!
tags: [nuget,oss,code]
redirect_from: "/archive/2011/02/11/nuget-1-1-released.aspx/"
---

Today I’m pleased to announce the release of NuGet 1.1 to the VS
Extension Gallery and CodePlex. If you have NuGet 1.0 installed, just
launch the VS Extension Manager (via *Tools* | *Extension Manager* menu)
and click on the Updates tab.

If you don’t see any updates, make sure to *enable automatic detection
of available updates*.

![Extension
Manager](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/e70eb912a429_D5FD/Extension%20Manager_3.png "Extension Manager")

If you are running VS 2010 SP1 Beta, you might run into the following
error message when attempting to *upgrade* to NuGet 1.1 if you have an
older version installed.

![Visual Studio Extension Installer
(3)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/e70eb912a429_D5FD/Visual%20Studio%20Extension%20Installer%20(3)_3.png "Visual Studio Extension Installer (3)")

The workaround is to simply uninstall NuGet and then install it from the
VS Extension Gallery.

It turns out that our previous VSIX was signed with an incorrect
certificate and our updated VSIX is signed with the correct certificate.
VS 2010 SP1 now compares and verifies that the certificates of the old
and new VSIX match during an upgrade.

If you don’t have NuGet installed, click the *Online* tab and type in
“NuGet” (sans quotes) to find it.

[![nuget-in-vs-gallery](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/e70eb912a429_D5FD/nuget-in-vs-gallery_thumb.png "nuget-in-vs-gallery")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/e70eb912a429_D5FD/nuget-in-vs-gallery_2.png)

The VSIX and updated command line tool (used to create and publish
packages) is also [available on
CodePlex.com](http://nuget.codeplex.com/releases/view/55760 "NuGet 1.1 Release").

What’s New in 1.1?
------------------

Much of the work in this release was focused on bug fixes. Now that
CodePlex.com supports directly linking to filtered views of the issue
tracker, I can provide you a [link to all the issues fixed in
1.1](http://nuget.codeplex.com/workitem/list/advanced?keyword=&status=All&type=All&priority=All&release=NuGet%201.1&assignedTo=All&component=All&sortField=LastUpdatedDate&sortDirection=Descending&page=0 "Issues fixed in 1.1").
![Smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/e70eb912a429_D5FD/wlEmoticon-smile_2.png)

In this post, I’ll highlight some of the new features.

### Recent Packages Tab

One of the first changes you might notice is that we have a new tab in
the dialog that shows packages that you’ve installed recently. Click the
screenshot below for a larger view.

[![NuGet-Recent-Packages](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/e70eb912a429_D5FD/NuGet-Recent-Packages_thumb.png "NuGet-Recent-Packages")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/e70eb912a429_D5FD/NuGet-Recent-Packages_2.png)

The recent packages shows the last 20 packages that you’ve *directly*
installed. This often comes in handy when you tend to use the same
packages over and over again in multiple projects. Right now, the list
simply shows the most recently used, but there has been discussion about
perhaps changing the behavior to sort by the packages used most often.
Feel free to [chime in if you want the behavior
changed](http://nuget.codeplex.com/Thread/View.aspx?ThreadId=242436 "What should the default order of packages be in the Recent Packages List?").

By the way, you can also use the Powershell within the Package Manager
Console to get this same information with the `–Recent` flag to
`Get-Package`.

![nuget-ps-recent](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/e70eb912a429_D5FD/nuget-ps-recent_17fc70ed-a9d4-4237-94f5-a40f794e6c12.png "nuget-ps-recent")

### Progress Bar During Installation

When you install a package, you’ll now notice a progress bar dialog that
shows up with output from installing the package.

![Installing](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/e70eb912a429_D5FD/Installing_b08e6cdd-7811-4c58-b90b-336402eb53f1.png "Installing")

The dialog is meant to give an indication of progress, but also gets out
of your way immediately when the installation is complete so you’re not
stuck clicking a bunch of *Close* buttons. But what happens if you
actually want to review that output?

### Package Manager Output Window

NuGet 1.1 also posts that output to the Output window now. When you go
to the Output window, you’ll need to select output from the *Package
Manager* to see that output as in the screenshot.

![nuget-output](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/e70eb912a429_D5FD/nuget-output_b80ddaad-bf65-4881-8d5e-349e746fa0aa.png "nuget-output")

This allows you to review what changes a package made at your leisure
after the fact.

### Dependency Resolution Algorithm

NuGet 1.1 includes an update to our dependency resolution algorithm
which is described in [David Ebbo’s blog post on this
topic](http://blog.davidebbo.com/2011/01/nuget-versioning-part-2-core-algorithm.html "NuGet Versioning Part 2")
in the section titled “NuGet 1.1 twist”.

### Support for F\# Project Types

If you are using F\#, this one’s for you.

### PowerShell Improvements

Thanks to our [newest core
contributor](http://nuget.codeplex.com/Thread/View.aspx?ThreadId=242878 "Welcome to our newest core contributor"),
[Oisin Grehan](http://nivot.org/ "Oising on Twitter") a PowerShell MVP
who really knows his stuff, NuGet 1.1 has a lot of improvements to the
PowerShell Console and scripts. I have to admit, a lot of it is over my
head as I’m no PowerShell guru, but we’re now much more compliant with
PowerShell conventions. Or so I’ve been told. Oisin has been driving a
lot of improvements with our PowerShell support.

We also now execute commands within the Powershell Console
asynchronously. This means that a long running command won’t freeze the
rest of Visual Studio while it runs.

### And many others!

There were a lot of other tweaks, bug fixes, and minor improvements that
were not worth mentioning here, but they are all listed in our release
notes.

Breaking Changes?
-----------------

There are some minor changes that hopefully won’t break 99.9% of you. If
you recall, we made our PowerShell scripts fit with PowerShell
conventions. If you have a package that calls one of these methods, your
package might need to be updated.Here’s the list of changes we made:

-   Removed `List-Package`. Use `Get-Package` instead.
-   `Get-ProjectNames` was removed. Use `Get-Project` instead and
    examine the `Name` property.
-   `Add-BindingRedirects` was renamed to `Add-BindingRedirect`.

What’s next?
------------

Our hope is to have a monthly point release, though we may adjust some
iterations to be longer as needed. To see what we’re planning for the
1.2 release, [check out this
link](http://nuget.codeplex.com/workitem/list/advanced?keyword=&status=Proposed&type=All&priority=All&release=NuGet%201.2&assignedTo=All&component=All&sortField=LastUpdatedDate&sortDirection=Descending&page=0&size=100 "NuGet 1.2 Planning")
of issues for 1.2 (note that by the time you read this, some of these
features might already be implemented). We’re constantly refining our
planning so nothing is set in stone.

For a small taste of what’s coming in 1.2, check out [this
video](http://www.youtube.com/user/davidebbo2#p/a/u/0/RxdUqw_PXII "A simple way to create NuGet packages")
by [David Ebbo](http://blog.davidebbo.com/ "David's Blog") showing a
streamlined workflow for creating packages.

Get Involved!
-------------

I bet many of you have some great ideas on what we should and shouldn’t
do for NuGet. We’d love to have you come over and share your great ideas
in [our discussion
list](http://nuget.codeplex.com/discussions "Discussion List"). Or if
you’re looking for other ways to contribute, check out our [guide to
contributing to
NuGet](http://nuget.codeplex.com/wikipage?title=Contributing%20to%20NuPack "Guid to contributing to NuGet").

