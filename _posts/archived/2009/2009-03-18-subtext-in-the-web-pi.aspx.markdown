---
title: Subtext 2.1.1 Available Via the Web Platform Installer
tags: [subtext]
redirect_from: "/archive/2009/03/17/subtext-in-the-web-pi.aspx/"
---

![subtext200x200](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Sub.1AvailableViatheWebPlatformInstaller_DD87/subtext200x200_3.png "subtext200x200")One
of the cool products that I’m personally excited about announced at Mix
is the updated Web Platform Installer.

I’m not going to lie. Part of the reason I’m excited about it is that it
includes the latest version
[Subtext](http://subtextproject.com/ "Subtext")! The Web PI tool is a
really nice way of installing and trying out various free and open
source applications out there. It installs everything you need to get
Subtext up and running on your local machine.

All you have to do is go to the [Web App
Gallery](http://www.microsoft.com/web/gallery/ "Web App Gallery"), find
an application and click the Install button and it will install the
application (if you have Web PI already installed). If you don’t have
Web PI installed, it will prompt you to install Web PI and then install
the app.

In this screenshot, you can see the dependencies needed to run Subtext
are already listed.

![Install
(2)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Sub.1AvailableViatheWebPlatformInstaller_DD87/Install%20(2)_3.png "Install (2)")

Subtext 2.1.1 is a very minor update to Subtext with a few bug fixes.
The major fix is that the “forgot password” feature now works properly.

This was a little last minute surprise for the Subtext team as I
literally put the required install package together in the last minutes
leading up to Mix. In the meanwhile, major refactoring work is ongoing
in our subversion repository. For example, the trunk now uses a custom
Routing implementation (not yet built on ASP.NET MVC, but moving that
way) Feel free to join in and help fix bugs and test. :)

