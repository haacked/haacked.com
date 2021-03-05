---
title: VS10 Beta 2 From an ASP.NET MVC Perspective
tags: [aspnet]
redirect_from: "/archive/2009/10/19/vs10beta2-and-aspnetmvc.aspx/"
---

You probably don’t need me to tell you that [Visual Studio 2010 Beta 2
has been
released](http://weblogs.asp.net/scottgu/archive/2009/10/19/vs-2010-and-net-4-0-beta-2.aspx "VS 2010 and .NET 4.0 Beta 2")
as it’s been blogged to death all over the place. Definitely check out
the many blog posts out there if you want more details on what’s
included.

This post will focus more on what Visual Studio 2010 means to ASP.NET
MVC and vice versa.

***Important: If you installed ASP.NET MVC for Visual Studio 2010 Beta
1, make sure to uninstall it (and VS10 Beta 1) before installing Beta
2.***

### In the box baby!

Well one of the first things you’ll notice is that [ASP.NET MVC 2
Preview
2](https://haacked.com/archive/2009/10/01/asp.net-mvc-preview-2-released.aspx "ASP.NET MVC 2 Preview 2")
is included in VS10 Beta 2. When you select the File | New menu option,
you’ll be greeted with an ASP.NET MVC 2 project template option under
the Web node.

[![New
Project](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WhatVisualStudio2010Beta2Meansfor.NETMVC_12658/New%20Project_thumb_1.png "New Project")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WhatVisualStudio2010Beta2Meansfor.NETMVC_12658/New%20Project_4.png)

Note that when you create your ASP.NET MVC 2 project with Visual Studio
2010, you can choose whether you wish to target ASP.NET 3.5 or ASP.NET
4.

![multi-target](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WhatVisualStudio2010Beta2Meansfor.NETMVC_12658/multi-target_3.png "multi-target")

If you choose to target ASP.NET 4, you’ll be able to take advantage of
the new [HTML encoding code
blocks](https://haacked.com/archive/2009/09/25/html-encoding-code-nuggets.aspx "HTML Encoding Code Blocks")
with ASP.NET MVC which I wrote about earlier.

*As an aside, you might find it interesting that the
`System.Web.Mvc.dll` assembly we shipped in VS10 is the exact same
binary we shipped out-of-band for VS2008 and .NET 3.5. How then does
that assembly implement an interface that is new in ASP.NET 4? That’s a
subject for another blog post.*

### What about ASP.NET MVC 1.0?

Unfortunately, we have no plans to support ASP.NET MVC 1.0 tooling in
Visual Studio 2010. When we were going through planning, we realized it
would’ve taken a lot of work to update our 1.0 project templates. We
felt that time would be better spent focused on ASP.NET MVC 2.

*However, that doesn’t mean you can’t develop an ASP.NET MVC 1.0
application with Visual Studio 2010!* All it means is you’ll have to do
so without the nice ASP.NET MVC specific tooling such as the *add
controller*and*add view*dialogs. After all, at it’s core, an ASP.NET MVC
project is a Web Application Project.

[Eilon
Lipton](http://weblogs.asp.net/leftslipper/ "Eilon Lipton's Blog"), the
lead dev for ASP.NET MVC, wrote a blog post a while back describing [how
to open an ASP.NET MVC project without having ASP.NET MVC
installed](http://weblogs.asp.net/leftslipper/archive/2009/01/20/opening-an-asp-net-mvc-project-without-having-asp-net-mvc-installed-the-project-type-is-not-supported-by-this-installation.aspx "Opening ASP.NET MVC project").
All it requires is for you to edit the .csproj file and remove the
following GUID from the `<ProjectTypeGuids>` element.

`{603c0e0b-db56-11dc-be95-000d561079b0};`

Once you do that, you’ll be able to open, code, and debug your project
from VS10.

### Upgrading ASP.NET MVC 1.0 to ASP.NET MVC 2

Another option is to upgrade your ASP.NET MVC 1.0 application to ASP.NET
MVC 2 and then open the upgraded project with Visual Studio 2010 Beta 2.

Eilon has your back again as he’s written a handy little [tool for
upgrading existing ASP.NET MVC 1.0 applications to version
2](http://weblogs.asp.net/leftslipper/archive/2009/10/19/migrating-asp-net-mvc-1-0-applications-to-asp-net-mvc-2.aspx "Migrating ASP.NET MVC 1.0 applications to ASP.NET MVC 2").

After using this tool, your project will still be a Visual Studio 2008
project. But you can then open it with VS10 and it knows how to open and
upgrade the project to be a VS10 project.

### What about automatic upgrades?

We are investigating implementing a more automatic process for upgrading
ASP.NET MVC 1.0 applications to ASP.NET MVC 2 when you try to open the
existing project in Visual Studio 2010. We plan to have something in
place by the RTM of VS10.

Ideally, when you try to open an ASP.NET MVC 1.0 project, instead of
showing an error dialog, VS10 will provide a wizard to upgrade the
project which will be somewhat based on the sample Eilon provided. So be
sure to supply feedback on his wizard soon!

