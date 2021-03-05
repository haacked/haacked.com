---
title: An Easier Way To See The Rotor Code
tags: [dotnet]
redirect_from: "/archive/2007/07/11/an-easier-way-to-see-the-rotor-code.aspx/"
---

Have you ever wanted to take a look at the internals of the .NET
Framework? Sure you can (and should) fire up
[Reflector](http://www.aisto.com/roeder/dotnet/ "Lutz Roeder's Reflector")
and see the Base Class Libraries, but what about the fully commented
source code? What about the parts implemented in C++?

A while back, Microsoft released the Shared Source Common Language
Infrastructure (aka the SSCLI aka Rotor). This is a fully working
implementation of the ECMA CLI standard and ECMA C# language
specification. So it's not quite the entire framework, but it is still
quite a bit of code.

Traditionally, to look at this code you would [download the compressed
archive](http://www.microsoft.com/downloads/details.aspx?FamilyId=8C09FD61-3F26-4555-AE17-3121B4F51D4D&displaylang=en "Download the compressed archive")
and play around with it locally.

![Development-Cost](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/AnEasierWayToSeeTheRotorCode_898F/Development-Cost_thumb.png)But
if you want to just quickly browse the code, you can view all of its
nearly three million lines of code on its [project page on
Koders.com](http://www.koders.com/info.aspx?c=ProjectInfo&pid=CRXEFWSLGTWAKTH3BSPNDHXQ1A "Project Page for the SCCLI").

*Wouldn't it be fun to compare that development cost estimate with the
real number? I doubt Microsoft is interested in disclosing that
information.*

To search within this project, you just need to set the search scope.

For example, here are the [search results for
DateTime](http://www.koders.com/default.aspx?s=DateTime&btn=Search&scope=CRXEFWSLGTWAKTH3BSPNDHXQ1A&la=*&li=* "Search Results")
and here is the page with [the DateTime
implementation](http://www.koders.com/csharp/fid3AFDF2DE50D30EDD084B83CB2F4E3037CF527C3B.aspx?s=DateTime "DateTime source code")
complete with comments.

[![Koders - datetime.cs - Windows Internet
Explorer](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/AnEasierWayToSeeTheRotorCode_898F/Koders%20-%20datetime.cs%20-%20Windows%20Internet%20Explorer_thumb.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/AnEasierWayToSeeTheRotorCode_898F/Koders%20-%20datetime.cs%20-%20Windows%20Internet%20Explorer.png "DateTime.cs Source Code")

Other Great Projects to Look At
-------------------------------

I mentioned a [couple of great
projects](http://www.koders.com/blog/?p=86 "Indexing 700,000,000") to
look at over on the Koders blog, but here are some other great projects
of interest to me now included in the Source Code Index.

-   [DotNetNuke](http://www.koders.com/info.aspx?c=ProjectInfo&pid=FEQ2YQS4KLK7N1RXC8PYB8RH2H "DotNetNuke project Page")
    - One of the largest open source projects on the .NET Framework.
-   [Subsonic](http://www.koders.com/info.aspx?c=ProjectInfo&pid=M9BYER1UPZ4LDY97R3W9VC54VF "Subsonic Project Page")
    - Some think this project should be called *Sublime* for how it
    brings fun back to ASP.NET development.
-   [Subtext](http://www.koders.com/info.aspx?c=ProjectInfo&pid=DFV7667WQ72FL9EV6BL8TGSE3G "Subtext Project Page")
    - Of course I'm going to mention this!
-   [PSP Development
    Tools](http://www.koders.com/info.aspx?c=ProjectInfo&pid=ZQXQ6G8KHWFRMN2HCQGE9AQRLG "PSP Development Tools Page")
    - Because we all want to write the next great game for the Play
    Station Portable.


