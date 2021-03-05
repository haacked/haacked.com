---
title: Bin Deploying ASP.NET MVC 3
tags: [aspnet,aspnetmvc,code]
redirect_from: "/archive/2011/05/24/bin-deploying-asp-net-mvc-3.aspx/"
---

When you build an ASP.NET MVC 3 application and are ready to deploy it
to your hosting provider, there are a set of assemblies you’ll need to
include with your application for it to run properly, unless they are
already installed in the Global Assembly Cache (GAC) on the server.

In previous versions of ASP.NET MVC, this set of assemblies was rather
small. In fact, it was only one assembly, *System.Web.Mvc.dll,*though in
the case of ASP.NET MVC 1.0, if you didn’t have SP1 of .NET 3.5
installed, you would have also needed to deploy
*System.Web.Abstractions.dll* and *System.Web.Routing.dll*.

But ASP.NET MVC 3 makes use of technology shared with the new ASP.NET
Web Pages product such as Razor. If you’re not familiar with ASP.NET Web
Pages and how it fits in with Web Matrix and ASP.NET MVC, read David
Ebbo’s blog post, [How WebMatrix, Razor, ASP.NET Web Pages, and MVC fit
together](http://blogs.msdn.com/b/davidebb/archive/2010/07/07/how-webmatrix-razor-asp-net-web-pages-and-mvc-fit-together.aspx "Ebbo clears it all up").

If your server doesn’t have ASP.NET MVC 3 installed, you’ll need to make
sure the following set of assemblies are deployed in the bin folder of
your web application:

-   Microsoft.Web.Infrastructure.dll
-   System.Web.Helpers.dll
-   System.Web.Mvc.dll
-   System.Web.Razor.dll
-   System.Web.WebPages.Deployment.dll
-   System.Web.WebPages.dll
-   System.Web.WebPages.Razor.dll

In this case, it’s not as simple as looking at your list of assembly
references and setting **Copy Local** to **True** as [I’ve instructed in
the
past](https://haacked.com/archive/2008/11/03/bin-deploy-aspnetmvc.aspx "Bin Deploy ASP.NET MVC").

As you can see in the following screenshot, not every assembly is
referenced. Not all of these assemblies are meant to be programmed
against so it’s not necessary to actually *reference*****each of these
assemblies. They just need to be available on the machine either from
the GAC or in the bin folder.

![referenced-assemblies](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Deploying-ASP.NET-MVC-3-Assemblies_12045/referenced-assemblies_3.png "referenced-assemblies")

But the Visual Web Developer team has you covered. They added a feature
specifically for adding these deployable assemblies. Right click on the
project and select **Add Deployable Assemblies** and you’ll see the
following dialog.

![add-deployable-assemblies](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Deploying-ASP.NET-MVC-3-Assemblies_12045/add-deployable-assemblies_3.png "add-deployable-assemblies")

When building an ASP.NET MVC application, you only need to check the
first option. Ignore the fact that the second one says “Razor”. “ASP.NET
Web Pages with Razor syntax”was the official full name of the product we
simply call ASP.NET Web Pages now. Yeah, it’s confusing.

Note that there’s also an option for SQL Server Compact, but that’s not
strictly necessary if you’ve installed SQL Server Compact via NuGet.

So what happens when you click “OK”?

![bin-deployable-assemlies](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Deploying-ASP.NET-MVC-3-Assemblies_12045/bin-deployable-assemlies_3.png "bin-deployable-assemlies")

A special folder named **\_bin\_deployableAssemblies** is created and
the necessary assemblies are copied into this folder. Web projects have
a built in build task that copies any assemblies in this folder into the
**bin** folder when the project is compiled.

Note that this dialog **did not** add any assembly references to these
assemblies. That ensures that the types in these assemblies don’t
pollute Intellisense, while still being available to your deployed
application. If you actually need to use a type in one of these
assemblies, you’re free to reference them.

So here’s the kicker. If you’re building a web application, and you need
an assembly deployed but don’t want it referenced and don’t want it
checked into the bin directory, you can simply add this folder yourself
and put your own assemblies in here.

If you’ve ever run into a problem where an ASP.NET MVC site you
developed locally doesn’t work when you deploy it, this dialog may be
just the ticket to fix it.

