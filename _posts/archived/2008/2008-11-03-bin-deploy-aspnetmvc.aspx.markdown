---
title: Bin Deploying ASP.NET MVC
tags: [aspnetmvc,aspnet]
redirect_from: "/archive/2008/11/02/bin-deploy-aspnetmvc.aspx/"
---

With the release of [ASP.NET
MVC](http://asp.net/mvc "ASP.NET MVC Website") Beta, the assemblies
distributed with ASP.NET MVC are automatically installed into the GAC.

-   System.Web.Mvc
-   System.Web.Routing
-   System.Web.Abstractions

While developing an application locally, this isn’t a problem. But when
you are ready to deploy your application to a hosting provider, this
might well be a problem if the hoster does not have the ASP.NET MVC
assemblies installed in the GAC.

Fortunately, ASP.NET MVC is still bin-deployable. If your hosting
provider has ASP.NET 3.5 SP1 installed, then you’ll only need to include
the MVC DLL. If your hosting provider is still on ASP.NET 3.5, then
you’ll need to deploy all three. It turns out that it’s really easy to
do so.

Also, ASP.NET MVC **runs in Medium Trust**, so it should work with most
hosting providers’ Medium Trust policies. It’s always possible that a
hosting provider customizes their Medium Trust policy to be draconian.

What I like to do is use the **Publish** feature of Visual Studio to
publish to a local directory and then upload the files to my hosting
provider. If your hosting provider supports FTP, you can often skip this
intermediate step and publish directly to the FTP site.

The first thing I do in preparation is to go to my MVC web application
project and expand the References node in the project tree. Select the
aforementioned three assemblies and in the **Properties** dialog, set
**Copy Local** to **True**.

![copy-local-true\_3](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/BinDeployingASP.NETMVC_F744/copy-local-true_3_3.png "copy-local-true_3") 

Now just right click on your application and select **Publish**.

 ![publish-project\_3](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/BinDeployingASP.NETMVC_F744/publish-project_3_3.png "publish-project_3")

This brings up the following **Publish** wizard.

![Publish-Web](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/BinDeployingASP.NETMVC_F744/Publish-Web_3.png "Publish-Web")

Notice that in this example, I selected a local directory. When I hit
**Publish**, all the files needed to deploy my app are available in the
directory I chose, including the assemblies that were in the GAC.

![bin-assemblies](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/BinDeployingASP.NETMVC_F744/bin-assemblies_3.png "bin-assemblies")

Now I am ready to XCOPY the application to my host, but before I do
that, I really should test the application as a bin deployed app to be
on the safe side.

Ideally, I would deploy this to some staging server, or a virtual
machine that does not have ASP.NET MVC installed. Otherwise, I’m forced
to uninstall ASP.NET MVC on the current machine and then test the
application.

You might be wondering, as I did, why I can’t just use `gacutil` to
temporarily unregister the assembly, test the app, then use it again to
register the assembly. Because it was installed using an MSI, Windows
won’t let you unregister it. Here’s a command prompt window that shows
what I got when I tried.

![gacutil-mvc](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/BinDeployingASP.NETMVC_F744/gacutil-mvc_3.png "gacutil-mvc")

Notice that it says that “assembly is required by one or more
applications”. In general, there shouldn’t be any difference between
running your application with MVC gac’d and it ungac’d. But I wouldn’t
trust me saying this, I’d test it out to be sure.

