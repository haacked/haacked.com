---
title: Hosting Your Own Local and Remote NuGet Feeds
tags: [nuget,code,oss]
redirect_from: "/archive/2010/10/20/hosting-your-own-local-and-remote-nupack-feeds.aspx/"
---

*Note: Everything I write here is based on a very early pre-release
version of NuGet ([formerly known as
NuPack](https://haacked.com/archive/2010/10/29/nupack-is-now-nuget.aspx "NuPack is now NuGet"))
and is subject to change.*

A few weeks ago I wrote a [blog post introducing the first preview, CTP
1, of NuGet Package
Manager](https://haacked.com/archive/2010/10/06/introducing-nupack-package-manager.aspx "Introducing NuPack Package manager").
It’s an open source ([we welcome
contributions!](https://haacked.com/archive/2010/10/14/nupack-up-for-grabs-items.aspx "NuPack UpForGrabs Items"))
developer focused package manager meant to make it easy to discover and
make use of third party dependencies as well as keep them up to date.

As of CTP 2 NuGet by default points to an ODATA service temporarily
located at
[http://go.microsoft.com/fwlink/?LinkID=204820](http://go.microsoft.com/fwlink/?LinkID=204820)
(in CTP 1 this was an ATOM feed located at
[http://go.microsoft.com/fwlink/?LinkID=199193](http://go.microsoft.com/fwlink/?LinkID=199193 "http://go.microsoft.com/fwlink/?LinkID=199193")).

This feed was set up so that people could try out NuGet, but it’s only
**temporary**. We’ll have a more permanent gallery set up as we get
closer to RTM.

If you want to get your package in the temporary feed, follow the
instructions at a companion project,
[NuPackPackages](http://nupackpackages.codeplex.com/ "NuPackPackages on CodePlex.com")
on CodePlex.com.

Local Feeds
-----------

Some companies keep very tight control over which third party libraries
their developers may use. They may not want their developers to point
NuGet to arbitrary code over the internet. Or, they may have a set of
proprietary libraries they want to make available for developers via
NuGet.

NuGet supports these scenarios with a combination of two features:

1.  NuGet can point to any number of different feeds. You don’t have to
    point it to just our feed.
2.  NuGet can point to a local folder (or network share) that contains a
    set of packages.

For example, suppose I have a folder on my desktop named *packages* and
drop in a couple of packages that I created like so:

![packages-folder](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Local-Package-Sources-for-NuPack_8B1A/packages-folder_91c16aae-1e0a-4997-8b50-992692dfbe74.png "packages-folder")

I can add that director to the NuGet settings. To get to the settings,
go to the Visual Studio *Tools*| *Options* dialog and scroll down to
*Package Manager*.

A shortcut to get there is to go to the Add Package Dialog and click on
the *Settings* button or click the button in the Package Manager Console
next to the list of package sources. This brings up the Options dialog
(*click to see larger image*).

[![Package Manager
Options](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Local-Package-Sources-for-NuPack_8B1A/Options_thumb.png "Package Manager Options")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Local-Package-Sources-for-NuPack_8B1A/Options_2.png)

Type in the path to your packages folder and then click the *Add*button.
Your local directory is now added as another package feed source.

[![Options-with-local-source-added](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Local-Package-Sources-for-NuPack_8B1A/Options-with-local-source-added_thumb.png "Options-with-local-source-added")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Local-Package-Sources-for-NuPack_8B1A/Options-with-local-source-added_2.png)

When you go back to the *Package Manager Console*, you can choose this
new local package source and list the packages in that source.

[![MvcApplication7 - Microsoft Visual Studio (Administrator)
(2)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Local-Package-Sources-for-NuPack_8B1A/MvcApplication7%20-%20Microsoft%20Visual%20Studio%20(Administrator)%20(2)_thumb.png "MvcApplication7 - Microsoft Visual Studio (Administrator) (2)")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Local-Package-Sources-for-NuPack_8B1A/MvcApplication7%20-%20Microsoft%20Visual%20Studio%20(Administrator)%20(2)_2.png)

You can also install packages from your local directory. If you’re
creating packages, this is a great way to test them out without having
to publish them online anywhere.[![MvcApplication7 - Microsoft Visual
Studio (Administrator)
(4)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Local-Package-Sources-for-NuPack_8B1A/MvcApplication7%20-%20Microsoft%20Visual%20Studio%20(Administrator)%20(4)_thumb.png "MvcApplication7 - Microsoft Visual Studio (Administrator) (4)")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Local-Package-Sources-for-NuPack_8B1A/MvcApplication7%20-%20Microsoft%20Visual%20Studio%20(Administrator)%20(4)_2.png)

Note that if you launch the *Add Package Reference Dialog*, you won’t
see the local package feed unless you’ve made it the default package
source. This limitation is only temporary as we’re changing the dialog
to allow you to select the package source.

[![Options-setting-default](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Local-Package-Sources-for-NuPack_8B1A/Options-setting-default_thumb.png "Options-setting-default")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Local-Package-Sources-for-NuPack_8B1A/Options-setting-default_2.png)

Now when you launch the *Add Package Reference Dialog*, you’ll see your
local packages.

[![add-package-reference-local-packages](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Local-Package-Sources-for-NuPack_8B1A/add-package-reference-local-packages_thumb.png "add-package-reference-local-packages")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Local-Package-Sources-for-NuPack_8B1A/add-package-reference-local-packages_2.png)

Please note, as of CTP 1, if one of these local packages has a
dependency on a package in another registered feed, it won’t work.
However, we are [tracking this
issue](http://nupack.codeplex.com/workitem/204 "Package Source Fallback Behavior")
and plan to implement this feature in the next release.

Custom Read Only Feeds
----------------------

Let’s suppose that what you really want to do is host a feed at an URL
rather than a package folder. Perhaps you are known for your great taste
in music and package selection and you want to host your own curated
NuGet feed of the packages you think are great.

Well you can do that with NuGet. For step by step instructions, check
out this follow-up blog post, [**Hosting a Simple “Read Only” NuGet
Package
Feed**](https://haacked.com/archive/2011/03/31/hosting-simple-nuget-package-feed.aspx "Hosting your own NuGet Feed").

We imagine that the primary usage of NuGet will be to point it to our
main online feed. But the flexibility of NuGet to allow for private
local feeds as well as curated feeds should appeal to many.

