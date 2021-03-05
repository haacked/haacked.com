---
title: Introducing ASP.NET MVC 3 Tools Update
tags: [aspnetmvc]
redirect_from: "/archive/2011/04/11/introducing-asp-net-mvc-3-tools-update.aspx/"
---

Today at Mix, Scott Guthrie announced an update to the ASP.NET MVC 3
we’re calling the **ASP.NET MVC 3 Tools Update**. You can **[install it
via Web
PI](http://www.microsoft.com/web/gallery/install.aspx?appid=MVC3)** or
download the installer by going to the [download details
page](http://go.microsoft.com/fwlink/?LinkID=208140). Check out [the
release
notes](http://www.asp.net/learn/whitepapers/mvc3-release-notes "ASP.NET MVC 3 Release Notes")
as well for more details.

Notice the emphasis on calling it a **Tools Update**? The reason for
that is simple. This only updates the tooling for ASP.NET MVC 3 **and
not the runtime**. There are no changes to System.Web.Mvc.dll or any of
its other assemblies that ship as part of the ASP.NET MVC 3 Framework.
Instead, given that we just released ASP.NET MVC 3 this past January, we
focused on improvements to the tools and project templates that we
wanted to ship in time for Mix.

To drive this point home, here’s a screenshot of the Programs and
Features dialog with ASP.NET MVC 3 RTM installed.

**BEFORE**

[![mvc3-installed](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Introducing_763A/mvc3-installed_thumb_1.png "mvc3-installed")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Introducing_763A/mvc3-installed_4.png)

And here’s one with the Tools Update installed.

**AFTER**

[![mvc3-update-installed](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Introducing_763A/mvc3-update-installed_thumb.png "mvc3-update-installed")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Introducing_763A/mvc3-update-installed_2.png)

Did you see what changed? If not, I’ll help you.
![Smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Introducing_763A/wlEmoticon-smile_2.png)

[![mvc3-update-installed-highlighted](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Introducing_763A/mvc3-update-installed-highlighted_thumb_1.png "mvc3-update-installed-highlighted")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Introducing_763A/mvc3-update-installed-highlighted_4.png)

What’s new in this release?
---------------------------

We’ve added a lot of improvements to the tooling experience in this
release. For more details, check out the release notes.

-   New Intranet Project Template that enables Windows Authentication
    and does not include the `AccountController.`
-   HTML 5 checkbox to enable HTML 5 versions of project templates.
-   Add Controller Dialog now supports full automatic scaffolding of
    Create, Read, Update, and Delete controller actions and
    corresponding views. By default, this scaffolds data access code
    using EF Code First.
-   Add Controller Dialog supports *extensible scaffolds* via NuGet
    packages such as *MvcScaffolding*. This allows plugging in custom
    scaffolds into the dialog which would allow you to create scaffolds
    for other data access technologies such as NHibernate or even JET
    with ODBCDirect if you’re so inclined!
-   JavaScript libraries within project templates are updatable via
    NuGet! (We included them as pre-installed NuGet packages.)
-   Includes [Modernizr 1.7](http://modernizr.com/ "Modernizr"). This
    provides compatibility support for HTML 5 and CSS 3 in down-level
    browsers.
-   Includes EF Code First 4.1 as a pre-installed NuGet package.

We’ve also made several other small changes and fixed several bugs in
the MVC tooling for Visual Studio:

-   We did some major cleanup to the AccountController in the Internet
    project template
-   We now have more “sticky” options that remember their settings even
    when you restart Visual Studio
-   We have much smarter model type filtering logic in the Add View and
    Add Controller dialogs

NuGet 1.2 Included
------------------

Around 12 days ago, we [released NuGet
1.2](https://haacked.com/archive/2011/03/30/nuget-1-2-released.aspx "NuGet 1.2").
If you don’t already have NuGet 1.2 installed, ASP.NET MVC 3 Tools
Update will install it for you. In fact, it requires it because of the
pre-installed NuGet packages feature I mentioned earlier. When you
create a new ASP.NET MVC 3 Tools Update project, the script libraries
such as jQuery are installed as NuGet packages so that it’s easy to
update them after the fact.

Give it a spin and let us know what you think!

