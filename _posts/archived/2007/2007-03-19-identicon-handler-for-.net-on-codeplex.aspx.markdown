---
title: Identicon Handler For .NET On CodePlex
tags: [dotnet]
redirect_from: "/archive/2007/03/18/identicon-handler-for-.net-on-codeplex.aspx/"
---

*Update: I’ve created a new [NuGet
Package](https://haacked.com/archive/2010/10/06/introducing-nupack-package-manager.aspx "NuGet Package")
for Identicon Handler (Package Id is “IdenticonHandler”) which will make
it much easier to include this in your own projects.*

A while ago, [Jeff
Atwood](http://codinghorror.com/blog/ "Jeff Atwood’s Blog") blogged
[about
Identicons](http://www.codinghorror.com/blog/archives/000774.html "Identicons for .NET")
for .NET. An Identicon is an anonymized visual glyph that can represent
an IP address. I likened it to a [Graphical Digital
Fingerprint](https://haacked.com/archive/2007/01/22/Identicons_as_Visual_Fingerprints.aspx "Identicons as Graphical Digital Fingerprints").

![Identicon
samples](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IdenticonsasVisualFingerprints_CB0/identiconsamples_thumb1.png)

The original concept and Java implementation was [created by Don
Park](http://www.docuverse.com/blog/donpark/2007/01/19/identicon-explained "Don Park explains Identicons").

Afterwards, Jeff and [Jon
Galloway](http://weblogs.asp.net/jgalloway/ "Jon Galloway") became
excited by the idea and ported Don’s code to C# and .NET 2.0 and
released it on his website.

This weekend, we’ve spent some time working out a few kinks and
performance improvements and are proud to [release version 1.1 on
CodePlex](http://www.codeplex.com/Identicon/ "Identicon Handler on CodePlex").

### Why CodePlex?

We chose [CodePlex](http://codeplex.com/ "CodePlex") for this project
because the codebase for this is extremely small, so the patch issue I
mentioned in my critique, [A Comparison of TFS vs Subversion for Open
Source
Projects](https://haacked.com/archive/2007/03/02/A_Comparison_of_TFS_vs_Subversion_for_Open_Source_Projects.aspx "Comparing TFS and Subversion"),
is not quite as large an issue.

We don’t expect this project to grow very large and have a huge number
of releases. This code does one thing, and hopefully, does it well.

So in that respect, CodePlex seems like a great host for this type of
small project. It is really easy to get other developers up and running
if need be.

Having said that, I probably wouldn’t host a large project here yet
based on the critique I mentioned.

### Related Posts

-   [Identicon
    Explained](http://www.docuverse.com/blog/donpark/2007/01/19/identicon-explained "Identicon Explained")
    - Don Park’s explanation of Identicons
-   [Identicons for
    .NET](http://www.codinghorror.com/blog/archives/000774.html "Identicons for .NET")
    - Jeff Atwood's original .NET 2.0 implementation
-   [Identicons - Ported from Java Servlet to
    HttpHandler](http://weblogs.asp.net/jgalloway/archive/2007/01/24/identicons-ported-from-java-servlet-to-httphandler.aspx "Jon Galloway's look at Identicons")
    - Jon Galloway’s introduction to Identicons.
-   [Identicons as Graphical Digital
    Fingerprints](https://haacked.com/archive/2007/01/22/Identicons_as_Visual_Fingerprints.aspx "Digital Fingerprints")
    - My look at Identicons as graphical hashes


