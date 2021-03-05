---
title: ASP.NET MVC Diagnostics Using NuGet
tags: [aspnet,aspnetmvc,nuget,code]
redirect_from: "/archive/2010/12/04/asp-net-mvc-diagnostics-using-nuget.aspx/"
---

Sometimes, despite your best efforts, you encounter a problem with your
ASP.NET MVC application that seems impossible to figure out and makes
you want to pull out your hair. Or worse, it makes you want to pull out
*my* hair. In some of those situations, it ends up being a [PEBKAC
issue](http://ars.userfriendly.org/cartoons/?id=19980506 "PEBKAC"), but
in the interest of avoiding physical harm, I try not to point that out.

[![pebkac-comic](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/62903db4eb83_8C22/pebkac-comic_3.gif "pebkac-comic")](http://ars.userfriendly.org/cartoons/?id=19980506 "UserFriendly comic")

Thankfully, in the interest of saving my hair, [Brad
Wilson](http://bradwilson.typepad.com/ "Brad Wilson's Blog")
(*recently*[*featured on This Developer’s
Life*](http://thisdeveloperslife.com/post/1620026288/1-0-8-motivation "This Developer's Life - 1.0.8 Motivation")*!*)
wrote a simple [diagnostics web page for ASP.NET
MVC](http://bradwilson.typepad.com/blog/2010/03/diagnosing-aspnet-mvc-problems.html "Diagnosing ASP.NET MVC Problems")
that you can drop into any ASP.NET MVC application. When you visit the
page in your browser, it provides diagnostics information that can help
discover potential problems with your ASP.NET application.

To make it as easy as possible to use it, I created a
[NuGet](http://nuget.codeplex.com/ "NuGet project on CodePlex") package
named “MvcDiagnostics”. If you’re not familiar with NuGet, check out [my
announcement of
NuGet](https://haacked.com/archive/2010/10/06/introducing-nupack-package-manager.aspx "Introducing NuGet")
as well as our [Getting Started
guide](http://nuget.codeplex.com/documentation?title=Getting%20Started "Getting Started with NuGet")
written by [Tim Teebken](http://blogs.msdn.com/b/timlee/ "Tim's Blog").

With NuGet, you can use the Add Package Library Dialog to install
MvcDiagnostics. Simply type in “MVC” in the search dialog to filter the
online entries. Then locate the *MvcDiagnostics* entry and click
“Install”.

[![add-package-dialog](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/62903db4eb83_8C22/add-package-dialog_thumb.png "add-package-dialog")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/62903db4eb83_8C22/add-package-dialog_2.png)

Or you can use the Package Manager Gonsole and simply type:

> `install-package MvcDiagnostics`

Either way, this will add the `MvcDiagnostics.aspx` page to the root of
your web application.

[![mvcdiagnostics-viewinbrowser](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/62903db4eb83_8C22/mvcdiagnostics-viewinbrowser_thumb.png "mvcdiagnostics-viewinbrowser")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/62903db4eb83_8C22/mvcdiagnostics-viewinbrowser_2.png)

You can then visit the page with your browser to get diagnostics
information.

[![mvc-diagnostics](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/62903db4eb83_8C22/mvc-diagnostics_thumb.png "mvc-diagnostics")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/62903db4eb83_8C22/mvc-diagnostics_2.png)

With NuGet, it’s much easier to make use of this diagnostics page.
Hopefully you’ll rarely need to use it, but it’s nice to know it’s
there. Let us know if you have ways to improve the diagnostics page.

