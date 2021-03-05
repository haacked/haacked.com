---
title: Compiling MVC Views In A Build Environment
tags: [aspnet,aspnetmvc]
redirect_from: "/archive/2011/05/08/compiling-mvc-views-in-a-build-environment.aspx/"
---

ASP.NET MVC project templates include support for precompiling views,
which is useful for finding syntax errors within your views at build
time rather than at runtime.

In case you missed the memo, the following outline how to enable this
feature.

-   Right click on your ASP.NET MVC project in the Solution Explorer
-   Select *Unload Project* in the context menu. Your project will show
    up as
    unavailable![unavailable-project](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/MvcBuildViews-and_799B/unavailable-project_c749a0c0-18ef-4691-9937-2ab42753eecf.png "unavailable-project")
-   Right click on the project again and select *Edit
    ProjectName.csproj*.

This will bring up the project file within Visual Studio. Search for the
entry `<MvcBuildViews>` and set the value to true. Then right click on
the project again and select *Reload Project*.

Compiling in a build environment
--------------------------------

If you search for *MvcBuildViews* on the web, you’ll notice a lot of
people having problems when attempting to build their projects in a
build environment. For example, this StackOverflow question describes an
issue when [compiling MVC on a TFS
Build](http://stackoverflow.com/questions/2566215/allowdefinition-machinetoapplication-error-when-publishing-from-vs2010-but-onl "ASP.NET MVC 1.0 AfterBuilding Views fails on TFS Build").
I [had an
issue](http://twitter.com/#!/haacked/status/67424234015698944 "Deploying to AppHarbor")
when trying to deploy an ASP.NET MVC 3 application to AppHarbor.

It turns out we had a bug in our project templates in earlier versions
of ASP.NET MVC that **[we fixed in ASP.NET MVC 3 Tools
Update](https://haacked.com/archive/2011/04/12/introducing-asp-net-mvc-3-tools-update.aspx "ASP.NET MVC 3 Tools Update")**.

But if you created your project using an older version of ASP.NET MVC
*including ASP.NET MVC 3 RTM* (the one before the Tools Update), your
csproj/vbproj file will still have this bug.

To fix this, look for the following element within your project file:

```xml
<Target Name="AfterBuild" Condition="'$(MvcBuildViews)'=='true'">
  <AspNetCompiler VirtualPath="temp" PhysicalPath="$(ProjectDir)\..\$(ProjectName)" />
</Target>
```

And replace it with the following.

```xml
<Target Name="MvcBuildViews" AfterTargets="AfterBuild" Condition="'$(MvcBuildViews)'=='true'">
  <AspNetCompiler VirtualPath="temp" PhysicalPath="$(WebProjectOutputDir)" />
</Target>
```

After I did that, I was able to deploy my application to AppHarbor
without any problems.

Going back to [the StackOverflow
question](http://stackoverflow.com/questions/755645/asp-net-mvc-1-0-afterbuilding-views-fails-on-tfs-build "MVC TFS Build Fails")
I mentioned earlier, notice that the accepted answer is not the best
answer. [Jim Lamb](http://blogs.msdn.com/jimlamb "Jim Lamb") provided a
better answer and is the one who provided the solution that we use in
ASP.NET MVC 3 Tools Update. Thanks Jim!

