---
title: A Really Empty ASP.NET MVC 3 Project Template
tags: [aspnet,aspnetmvc]
redirect_from: "/archive/2012/01/10/a-really-empty-asp-net-mvc-3-project-template.aspx/"
---

In the ASP.NET MVC 3 Uservoice site, one of the most voted up items is a
suggestion to [include an empty project
template](http://aspnet.uservoice.com/forums/41201-asp-net-mvc/suggestions/2386516-the-empty-asp-net-mvc-project-template-should-be-e "Include empty project template").
No, a really empty project template.

You see, ASP.NET MVC 3 includes an “empty” project template, but it’s
not empty enough for many people. So in this post, I’ll give you a much
emptier one. It’s not completely empty. If you really wanted it
completely empty, just choose the ASP.NET Empty Web Application
template.

The Results
-----------

I’ll show you the results first, and then talk about how I made it.
After installing my project template, every time you create a new
ASP.NET MVC 3 project, you’ll see a new entry named “Really Empty”

[![mvc3-empty-proj-template](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/A-Really-Empt.NET-MVC-3-Project-Template_E2FF/mvc3-empty-proj-template_thumb_4.png "mvc3-empty-proj-template")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/A-Really-Empt.NET-MVC-3-Project-Template_E2FF/mvc3-empty-proj-template_10.png)

Select that and you end up with the following directory structure.

![mvc3-proj-template-expanded](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/A-Really-Empt.NET-MVC-3-Project-Template_E2FF/mvc3-proj-template-expanded_3.png "mvc3-proj-template-expanded")

I removed just about everything. I kept the *Views* directory because
the *Web.config* file that’s required is not obvious and there’s special
logic related to the *Views* directory. I also kept the *Controllers*
directory, since that’s where the tooling is going to put controllers
anyways. I also kept the *Global.asax* and *Web.config* files which are
typically necessary for an ASP.NET MVC project.

I debated removing the *AssemblyInfo.cs* file, but decided to trim it
down and keep it.

Building Custom Project Templates
---------------------------------

I wrote about building a custom ASP.NET MVC 3 project template [a long
time
ago](https://haacked.com/archive/2011/06/05/creating-a-custom-asp-net-mvc-project-template.aspx "Building a custom ASP.NET MVC 3 project template").
However, I’ve improved on what I did quite a bit. Now, I have a single
*install.cmd* file you can run and it’ll determine whether you’re on x64
or x86 and run the correct registry script. The *install.cmd* and
*uninstall.cmd* batch files are there for convenience and call into a
PowerShell script that does the real work.

**UPDATE 1/12/2012**: Thanks to [Tim
Heuer](http://timheuer.com/blog/ "Tim Heuer's Blog"), we have an even
better installation experience. He refactored the project to output a
VSIX file. All you need to do is double click the extension file to
install the project template. I’ve uploaded the extension file to
[GitHub
here](https://github.com/Haacked/ReallyEmptyMvc3ProjectTemplate/downloads "VSIX").

I tried uploading it to the gallery, but it wouldn’t let me. I’ll follow
up on that.

History
-------

If you’re wondering why the product team hasn’t included this all along,
it’s for a lot of reasons. There was (at least when I was there)
internal debate about how empty to make it. For example, when you create
a new project with my empty template, and hit F5, you get an error. Not
a great experience for most people.

Honestly, I’m all for it, but there are many other higher priority items
for the team to work on. So I figured I’d do it myself and put it up on
GitHub.

Installation
------------

Installation is really simple. If you like to build things from source,
grab the source from [my GitHub
repository](https://github.com/Haacked/ReallyEmptyMvc3ProjectTemplate "ReallyEmptyProjectTemplate on GitHub")
and run the *build.cmd* batch file. Then double click the resulting VSIX
file. Be sure to read the [README for more
details](https://github.com/Haacked/ReallyEmptyMvc3ProjectTemplate/blob/master/README.md "Readme file").

If you don’t yet know how to use Git to grab a repository, don’t worry,
just navigate to the [downloads
page](https://github.com/Haacked/ReallyEmptyMvc3ProjectTemplate/downloads "Downloads")
and download the VSIX file I’ve conveniently uploaded.

Contribute!
-----------

Hey, if you think you can help me make this better, please go fork it
and send me a pull request. Let me know if I include too little or too
much.

I’ve already posted a few things that could use improvement in the
README. If you'd like to help make this better, consider one of the
following. :)

-   Make script auto-detect whether VS is running or not and do the
    right thing
-   Test this on an x86 machine
-   Write an installer for this

Let me know if you find this useful.

