http://blog.maartenballiauw.be/post/2014/04/11/Building-NET-projects-is-a-world-of-pain-and-heres-how-we-should-solve-it.aspx

## Import MSBuild targets and props files into project

http://docs.nuget.org/docs/creating-packages/creating-and-publishing-a-package#Import_MSBuild_targets_and_props_files_into_project_(Requires_NuGet_2.5_or_above)

## Development only dependencies.

http://docs.nuget.org/docs/release-notes/nuget-2.7#Development-Only_Dependencies

This feature was contributed by Adam Ralph and it allows package authors to declare dependencies that were only used at development time and don't require package dependencies. By adding a developmentDependency="true" attribute to a package in packages.config, nuget.exe pack will no longer include that package as a dependency.

Octokit.net packages.config

<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="DocPlagiarizer" version="0.1.1" targetFramework="net45" developmentDependency="true" />
  <package id="SimpleJson" version="0.34.0" targetFramework="net45" developmentDependency="true" />
</packages>

## Related dependencies feature

http://nuget.codeplex.com/wikipage?title=Related%20Dependencies