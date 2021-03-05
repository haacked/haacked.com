---
title: Mono Migration Analyzer
tags: [dotnet,xplat]
redirect_from: "/archive/2006/11/26/Mono_Migration_Analyzer.aspx/"
---

Now this is a stroke of genius.  If you want people to consider making
their .NET applications work on
[Mono](http://www.mono-project.com/ "Mono"), **give them a tool that
informs them ahead of time how much trouble (or how easy) it will be to
migrate to Mono**.

That is exactly what Jonathan Pobst did with the [Mono Migration
Analyzer](http://www.mono-project.com/MoMA "Mono Migration Analyzer Page")
(found via [Miguel de
Icaza](http://tirania.org/blog/archive/2006/Nov-27.html "Mono Migration Analyzer")). 
This tool analyzes compiled assemblies and generates a report
identifying issues that might prevent your application from running on
Mono.  This report serves as a guide to porting your application to
Mono.

Having Subtext
run on Mono is a really distant goal for us, but a tool like this could
advance the timetable on such a feature, in theory.

I tried to run the analyzer on every assembly in the bin directory of
Subtext, but the analyzer threw an exception, doh!  That’s my “[Gift
Finger](https://haacked.com/archive/2005/07/11/Debugging-Detective-Stories.aspx "Debugging Detective Stories")”
at work (*I could not find where to submit error reports so I sent an
email to Mr. Pobst. I hope he doesn’t mind*).

[![Moma
Exception](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/MonoMigrationAnalyzer_9B03/MonoAnalyzerException_thumb%5B1%5D.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/MonoMigrationAnalyzer_9B03/MonoAnalyzerException%5B5%5D.png)

I then re-ran the analyzer selecting only the Subtext.\* assemblies.

[![Subtext Moma
Results](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/MonoMigrationAnalyzer_9B03/Moma-Results_thumb%5B6%5D.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/MonoMigrationAnalyzer_9B03/Moma-Results%5B12%5D.png)

As you can see, we call 12 methods that are still missing in Mono, 23
methods that are not yet implemented, and 13 on their to do list. 
Clicking on *View Detail Report* provides a nice report on which methods
are problematic.

In a really smart move, Moma also makes it quite easy to submit results
to the Mono team.

[![Submit
Results](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/MonoMigrationAnalyzer_9B03/Moma-Submit-Results_thumb%5B1%5D.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/MonoMigrationAnalyzer_9B03/Moma-Submit-Results%5B3%5D.png)

This is a great way to help them plan ahead and prioritize their
efforts.  Just for fun, I ran Moma against the [BlogML
2.0](http://codeplex.com/Wiki/View.aspx?ProjectName=BlogML "BlogML on CodePlex")
assembly and it passed with flying colors.   [![Moma Blogml
results](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/MonoMigrationAnalyzer_9B03/Moma-BlogMl-Results_thumb%5B3%5D.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/MonoMigrationAnalyzer_9B03/Moma-BlogMl-Results%5B7%5D.png)

Nice!

