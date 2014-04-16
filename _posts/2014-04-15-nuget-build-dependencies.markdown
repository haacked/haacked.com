---
layout: post
title: "A less terrible .NET project build with NuGet"
date: 2014-04-15 13:36 -0800
comments: true
categories: [nuget]
---

According to [Maarten Balliauw](http://blog.maartenballiauw.be/), [Building .NET projects is a world of pain](http://blog.maartenballiauw.be/post/2014/04/11/Building-NET-projects-is-a-world-of-pain-and-heres-how-we-should-solve-it.aspx). He should know, he is a co-founder of [MyGet.org](https://myget.org) which provides private NuGet feeds along with build services for those packages.

He's also a co-author of the [Pro NuGet book](http://amzn.to/1kYrgw0), though I might argue he's most famous for his contribution to [Let Me Bing That For You](http://letmebingthatforyou.com/).

His post gives voice to a frustration I've long had. For example, if you want to build a project library that targets Windows 8 RT, you have to install Visual Studio on your build machine. That's just silly fries! (_By the way, if you have a solution that doesn't require Visual Studio, I'd love to hear it!_)

UPDATE: Nick Berardi writes [about an approach that doesn't require Visual Studio](http://nickberardi.com/a-net-build-server-without-visual-studio/). Of course, there are several caveats with that approach. First, any upgrade requires you re-do the copy. Second, I'm not sure what the licensing implications are. You might still technically need a Visual Studio license for that server to do this. In any case, I opened a User Voice issue asking Microsoft to just [clean this mess up](http://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/5786689-support-net-builds-without-requiring-visual-studi) and make it easier for us to do this. 

Maarten doesn't just rant about this situation, he proposes a solution (emphasis mine):

> I do not think we can solve this quickly and change history. But I do think from now on we have to start building SDK’s differently. Most projects only require an MSBuild .targets file and some assemblies, either containing MSBuild tasks or reference assemblies, to do their compilation work. __What if… we shipped the minimum files required to succesfully build a project as NuGet packages?__

This philosophy aligns well with my personal philosophy on [self-contained builds](http://haacked.com/archive/2004/08/26/creating-a-sane-build-process.aspx/) and was a key design goal with NuGet. One of the guiding principles I wrote about when [we first announced NuGet](http://haacked.com/archive/2010/10/06/introducing-nupack-package-manager.aspx/):

> Works with your source code. This is an important principle which serves to meet two goals: The changes that NuGet makes can be committed to source control and the changes that NuGet makes can be x-copy deployed. This allows you to install a set of packages and commit the changes so that when your co-worker gets latest, her development environment is in the same state as yours. This is why NuGet packages do not install assemblies into the GAC as that would make it difficult to meet these two goals. __NuGet doesn’t touch anything outside of your solution folder.__ It doesn’t install programs onto your computer. It doesn’t install extensions into Visual studio. It leaves those tasks to other package managers such as the Visual Studio Extension manager and the Web Platform Installer.

There's a caveat that NuGet does store packages in a machine specific location outside of the solution, but that's an optimization. The point is, a developer should ideally be able to checkout your code from GitHub or other source hosting repository and build the solution. Bam! Done! If there's too many more steps than that, it's a pain to contribute.

Fortunately, there are some great features in NuGet that can help package authors reach this goal!

## Import MSBuild targets and props files into project

NuGet 2.5 introduces the ability to [import MSBuild targets and prop files into a project](http://docs.nuget.org/docs/creating-packages/creating-and-publishing-a-package#Import_MSBuild_targets_and_props_files_into_project_\(Requires_NuGet_2.5_or_above\)). As more projects take advantage of this feature, we'll hopefully see the demise of required MSIs in order to work on a project. As Maarten points out, MSIs (or Visual Studio Extensions) are still valuable in order to add extra tooling. But they shouldn't be required in order to build a project.

## Development-only dependencies

In tandem with importing MSBuild targets, NuGet 2.7 adds the ability to specify [development-only dependencies](http://docs.nuget.org/docs/release-notes/nuget-2.7#Development-Only_Dependencies).

> This feature was contributed by Adam Ralph and it allows package authors to declare dependencies that were only used at development time and don't require package dependencies. By adding a `developmentDependency="true"` attribute to a package in `packages.config`, `nuget.exe pack` will no longer include that package as a dependency.

These are packages that do not get deployed with your application. These packages might include MSBuild targets, code contract assemblies, or source code only packages.

You can see an example of this in use with [Octokit.net](https://github.com/octokit/octokit.net) in its `packages.config`.

```xml
<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="DocPlagiarizer" version="0.1.1" targetFramework="net45" developmentDependency="true" />
  <package id="SimpleJson" version="0.34.0" targetFramework="net45" developmentDependency="true" />
</packages>
```

My recommendation to package authors is to consider a separate *.Runtime package that contains just the assemblies that need to be deployed and a separate main project that depends on that package that brings in all the build-time dependencies such as MSBuild targets and whatnot. It keeps a nice separation and works well for other non-Visual Studio NuGet consumers such as Web Matrix, ASP.NET Web Pages, Xamarin, etc.

## Related dependencies feature

At the end of his post, Maarten notes that there is good progress towards build sanity.

> P.S.: A lot of the new packages like ASP.NET MVC and WebApi, the OData packages and such are being shipped as NuGet packages which is awesome. The ones that I am missing are those that require additional build targets that are typically shipped in SDK's. Examples are the Windows Azure SDK, database tools and targets, ... I would like those to come aboard the NuGet train and ship their Visual Studio tooling separately from teh artifacts required to run a build.

This reminds me of a feature proposal I wrote a draft specification for a long time ago called [Related Dependencies](http://nuget.codeplex.com/wikipage?title=Related%20Dependencies). You can tell it's old because it refers to the old name for NuGet.

These are basically "optional" dependencies that can bring in tooling from other package managers such as the Visual Studio Extensions gallery. In the spec, I mentioned "prompting" but the goal would be a non-obtrusive way for packages to highlight other tooling related to the package dependency and make it easy for developers to easily install all of them.

In my mind, this would be similar to how you are notified of updates in the Visual Studio Extension Manager (now called "Extensions and Updates" dialog). Perhaps there's another tab that lets you see extensions related to the packages installed in your solution and an easy way to install them all.

But these would have to be __optional__. You should be able to build the solution without them. Installing them just makes the development experience a bit better.
 
