---
title: Installing ASP.NET MVC 2 RC 2 on Visual Studio 2010 RC
tags: [aspnetmvc]
redirect_from: "/archive/2010/02/09/installing-asp-net-mvc-2-rc-2-on-visual-studio.aspx/"
---

As many of you have probably heard, the [release candidate for Visual
Studio 2010 was recently
released](http://weblogs.asp.net/scottgu/archive/2010/02/08/vs-2010-net-4-release-candidate.aspx "VS 2010/.NET 4 Release Candidate")
containing immense performance improvements and tons of bug fixes.

Another thing that release contains is the release candidate for ASP.NET
MVC 2. However, that release is not the latest release of ASP.NET MVC 2
as we recently [released a second release candidate for ASP.NET MVC
2](https://haacked.com/archive/2010/02/04/aspnetmvc2-rc2.aspx "ASP.NET MVC 2 RC 2 Released")
in response to customer feedback.

I apologize for the confusion this may have caused, but we really felt
it was important to have another release candidate for ASP.NET MVC to
help verify that we were responding to feedback in the correct manner.

If you wish to have Visual Studio 2010 RC and ASP.NET MVC 2 RC 2
installed at the same time, it’s not a problem.

### If you installed ASP.NET MVC 2 RC 2 *Before* installing VS 2010 RC

Believe it or not, you’re all set if you install them in this order.

When installing VS 2010 RC, the installation will detect that a newer
version of the ASP.NET MVC runtime (aka the `System.Web.Mvc` assembly)
is already installed and will not overwrite it with the older version
included with VS 2010 RC.

Keep in mind that the project templates for VS 2010 will still be the
slightly older ASP.NET MVC 2 RC project templates and not the RC 2
templates. Fortunately those templates haven’t changed too much between
release candidates.

In this configuration, when you create a project using VS 2010 RC, even
though the templates may be slightly older, the project will reference
the newer `System.Web.Mvc` assembly.

### If you are installing ASP.NET MVC 2 RC 2 *After* installing VS 2010 RC

In this case, there’s a tiny bit of work to do. The installer for
ASP.NET MVC 2 RC 2 will block if an older version of the ASP.NET MVC 2
runtime is installed.

To remedy the situation, all you need to do is uninstall the ASP.NET MVC
2 *runtime.* In *Add/Remove Programs dialog*(also known as the *Program
and features dialog*), this would be the entry named “*Microsoft ASP.NET
MVC 2*”.

If you have an older version of MVC tooling/project templates for Visual
Studio 2008 installed (named “*Microsoft ASP.NET MVC 2 – Visual Studio
2008 Tools*”), you’ll also need to uninstall that, but do not uninstall
the MVC tooling for VS 2010.

[![add-remove-dialog](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/GettingAS.NETMVC2RC2onVisualStudio2010RC_13EBF/add-remove-dialog_thumb_1.png "add-remove-dialog")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/GettingAS.NETMVC2RC2onVisualStudio2010RC_13EBF/add-remove-dialog_4.png) 

At this point, you should only have *Microsoft ASP.NET MVC 2 – Visual
Studio Tools for VS 2010* installed. You may now run the installer for
ASP.NET MVC 2 RC 2, which will put the runtime on your machine as well
as tooling/project templates for VS 2008.

Hopefully this clears up some of the confusion and gets you going with
VS 2010 RC and ASP.NET MVC 2 RC 2.

