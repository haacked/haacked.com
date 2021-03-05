---
title: Building a Self Updating Site Using NuGet
tags: [aspnetmvc,oss,nuget]
redirect_from: "/archive/2011/01/14/building-a-self-updating-site-using-nuget.aspx/"
---

For those of you who enjoy learning about a technology via screencast,
I’ve recorded a video to accompany and complement this blog post. The
screencast shows you what this package does, and the blog post covers
more of the implementation details.

A key feature of any package manager is the ability to let you know when
there’s an update available for a package and let you easily install
that update.

For example, when we deployed [the release candidate for
NuGet](https://haacked.com/archive/2010/12/10/asp-net-mvc-3-release-candidate-2.aspx "ASP.NET MVC 3 RC2 and NuGet RC released"),
the Visual Studio Extension Manager displayed the release in the
**Updates** section.

[![Extension Manager Displaying NuGet as an Available
Updates](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Building-a-Self-Updating-Site-Using-NuGe_88E2/extension-manager-update-available_thumb.png "Extension Manager Displaying NuGet as an Available Updates")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Building-a-Self-Updating-Site-Using-NuGe_88E2/extension-manager-update-available_2.png)

Likewise, [NuGet](http://nuget.codeplex.com/) lets you easily see
updates for installed packages. You can either run the
`List-Package –Updates` command:

[![list-package-updates](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Building-a-Self-Updating-Site-Using-NuGe_88E2/list-package-updates_thumb.png "list-package-updates")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Building-a-Self-Updating-Site-Using-NuGe_88E2/list-package-updates_2.png)

Or you can click on the **Updates** node of the Add Package dialog:

[![updates-tab](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Building-a-Self-Updating-Site-Using-NuGe_88E2/updates-tab_thumb.png "updates-tab")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Building-a-Self-Updating-Site-Using-NuGe_88E2/updates-tab_2.png)

This feature is very handy when using Visual Studio to develop software
such as
[Subtext](http://subtextproject.com/ "Subtext's Project Homepage"), an
open source blog engine I run in my spare time. But I started thinking
about the users of Subtext and the hoops they jump through to upgrade
Subtext itself.

Wouldn’t it be nice if Subtext could notify users when a new version is
available and let them install it directly from the admin section of the
running website completely outside of Visual Studio? Why yes, that would
be nice.

NuGet to the Rescue!
--------------------

Well my friends, that’s where
[NuGet](http://nuget.codeplex.com/ "NuGet CodePlex project") comes into
play. While most people know NuGet as a Visual Studio extension for
pulling in and referencing libraries in your project, there’s a core API
that’s completely agnostic of the hosting environment whether it be
Visual Studio, PowerShell, or other. That core API is implemented in the
assemly, ***NuGet.Core.dll***.

This assembly allows us to take advantage of many of the features of
NuGet outside of Visual Studio such as within a running web site!

The basic concept is this:

1.  Package up the first version of a website as a NuGet package.
2.  Install this package in the website itself. I know, crazy talk,
    right?
3.  Add a custom NuGet client that runs inside the website and checks
    for updates to the one package that’s installed.
4.  When the next version of the website is ready, package it up and
    deploy it to the package feed for the website. Now, the users of the
    website can be notified that an update is available.s

I should point out a brief note about step \#2, because this is going to
be confusing. When I say install the package in the website, I mean to
contrast that with installing a package into your Web Application
Project for the website.

When you install a package into your Web Application Project, you use
the standard NuGet client within Visual Studio. But when you deploy your
website, the custom NuGet client within the live website will install
the website package into a different location. In the example I’ll show
you, that location is within the `App_Data\packages` folder.

The AutoUpdate Package
----------------------

Earlier this week, I gave an online presentation to the [Community For
MVC (C4MVC)](http://www.c4mvc.net/ "C4MVC Website") user’s group on
NuGet. During that talk I demonstrated a prototype package I wrote
called *AutoUpdate*. This package adds a new area to the target website
named “Installation”. It also adds a *[nuspec
file](http://nuget.codeplex.com/documentation?title=Nuspec%20Format "NuSpec Format Docs")*
to the root of the application to make it easy to package up the website
as a NuGet Package.

The steps to use the package are very easy.

1.  `Install-Package AutoUpdate`.
2.  In Web.config, modify the appSetting `PackageSource` to point to
    your package source. In my demo, I just pointed it to a folder on my
    machine for demonstration purposes. But this source is where you
    would publish updates for your package.
3.  In the Package Console, run the `New-Package` script (This creates
    packages up the website in a NuPkg file).
4.  Copy the package into the *App\_Data\\Packages* folder of the site.
5.  When you are ready to publish the next version as an update,
    increment the version number in the *nuspec* file and run the
    `New-Package` script again.
6.  Deploy the updated package to the package source.
7.  Now, when your users visit */installation/updates/check* within the
    web site, they’ll be notified that an update is available and will
    be able to install the update.

The Results
-----------

Lets see the results of installing the *AutoUpdate* package and I’ll
highlight some of the code that makes the package work. The following
screenshot shows a very basic sample application I wrote.

![home-page](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Building-a-Self-Updating-Site-Using-NuGe_88E2/home-page_3.png "home-page")

The homepage here has a link to check for updates which links to an
action within the area installed by the *AutoUpdate* package. That
action contains the logic to check for updates for this application’s
package.

Clicking on that link requires me to login first and then I get to this
page:

[![update-available](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Building-a-Self-Updating-Site-Using-NuGe_88E2/update-available_thumb.png "update-available")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Building-a-Self-Updating-Site-Using-NuGe_88E2/update-available_2.png)

As I mentioned in the steps before, I packaged up the first version of
the application as a package and “installed” it into the *App\_Data*
folder.

That yellow bar above is the result of an asynchronous JSON request to
see if an update is available. It’s a little redundant on this page, but
I could have it show up on every page within the admin as a
notification.

Under the Hood
--------------

Let’s take a look at the controller that responds to that asynchronous
request.

```csharp
public ActionResult Check(string packageId) {
  var projectManager = GetProjectManager();
  var installed = GetInstalledPackage(projectManager, packageId);
  var update = projectManager.GetUpdate(installed);

  var installationState = new InstallationState {
    Installed = installed,
    Update = update
  };

  if (Request.IsAjaxRequest()) {
    var result = new { 
      Version = (update != null ? update.Version.ToString() : null), 
      UpdateAvailable = (update != null)
    };
    return Json(result, JsonRequestBehavior.AllowGet);
  }

  return View(installationState);
}
```

The logic here is pretty straightforward. We grab a project manager. We
then grab a reference to the current installed package representing this
application. And then we check to see if there’s an update available. If
there isn’t an update, the `GetUpdate` method returns false. There’s a
couple of methods here that I wrote we need to look at.

The first method very simply retrieves a project manager. I encapsulated
it into a method since I call it in a couple different places.

```csharp
private WebProjectManager GetProjectManager() {
  string feedUrl = @"D:\dev\hg\AutoUpdateDemo\test-package-source";
  string siteRoot = Request.MapPath("~/");

  return new WebProjectManager(feedUrl, siteRoot);
}
```

There’s a couple things to note here. I hard coded the `feedUrl` for
demonstration purposes to point to a directory on my machine. This is a
nice demonstrations that NuGet can simply treat a directory containing
packages as a package source.

For your auto-updating web application, that should point to a custom
feed you host specifically for your website. Or, point it to the
official NuGet feed and put your website up there. It’s up to you.

This method returns an instance of `WebProjectManager`. This is a class
that I had to copy from the *System.Web.WebPages.Administration.dll*
assembly because it’s marked internal. I don’t know why it’s internal,
so I’ll see if we can fix that. It’s not my fault so please direct your
hate mail elsewhere.
![Smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Building-a-Self-Updating-Site-Using-NuGe_88E2/wlEmoticon-smile_2.png)

What is the web project manager? Well the [WebMatrix
product](http://www.asp.net/WebMatrix "WebMatrix") which includes the
ASP.NET Web Pages framework includes a web-based NuGet client for simple
web sites. This allows packages to be installed into a running website.
I’m just stealing that code and re-purposing it for my own needs.

Now, we just need to use the project manager to query the package source
to see if there’s an update available. This is really easy.

```csharp
private IPackage GetInstalledPackage(WebProjectManager projectManager,     string packageId) {
  var installed = projectManager.GetInstalledPackages("AutoUpdate.Web")    .Where(p => p.Id == packageId);

  var installedPackages = installed.ToList();
  return installedPackages.First();
}
```

What’s really cool is that we can just send a LINQ query to the server
because we’re running OData on the server, it’ll run that query on the
server and send us back the packages that fulfill the query.

That’s all the code necessary to check for updates. The next step is to
write an action method to handle the upgrade. That’s pretty easy too.

```csharp
public ActionResult Upgrade(string packageId) {
  var projectManager = GetProjectManager();
  var installed = GetInstalledPackage(projectManager, packageId);
  var update = projectManager.GetUpdate(installed);
  projectManager.UpdatePackage(update);

  if (Request.IsAjaxRequest()) {
    return Json(new { 
      Success = true, 
      Version = update.Version.ToString()
    }, JsonRequestBehavior.AllowGet);
  }
  return View(update);
}
```

This code starts off the same way that our code to check for the update
does, but instead of simply returning the update, we call
`projectManager.UpdatePackage` on the update. That method call updates
the website to the latest version.

The rest of the method is simply concerned with returning the result of
the upgrade.

Try it Yourself
---------------

If you would like to try it yourself, please keep a one big caveat in
mind. This is rough proof of concept quality code. I hope to shape it
into something more robust over time and publish it in the main package
feed. Until then, I’ll post it here for people to try out. If there’s a
lot of interest, I’ll post the source on
[CodePlex.com](http://codeplex.com/ "CodePlex open source hosting").

So with that in mind, give the *AutoUpdate* package a try

> **install-package AutoUpdate**

and give me some feedback!

UPDATE: I upgraded the project to target ASP.NET MVC 4 and posted the
[source on GitHub](https://github.com/Haacked/AutoUpdate). I have no
idea if it still works, so please do submit pull requests if you find
bugs you would like to have fixed.

