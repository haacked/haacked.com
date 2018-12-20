---
title: A Library For Executing SQL Scripts With GO Separators and Template Parameters
date: 2007-11-04 -0800
tags:
- code
redirect_from: "/archive/2007/11/03/a-library-for-executing-sql-scripts-with-go-separators-and.aspx/"
---

One thing I’ve found with various open source projects is that many of
them contain very useful code nuggets that could be generally useful to
developers writing different kinds of apps. Unfortunately, in many
cases, these nuggets are hidden. If you’ve ever found yourself thinking,
*Man, I wonder how that one open source app does XYZ because I could use
that in this app*, then you know what I mean.

**One goal I have with Subtext is to try and expose code that I think
would be useful to others**. It’s part of the reason I started the
[Subkismet
project](http://www.codeplex.com/subkismet "Subkismet - The Cure for Comment Spam").

Another useful library you might find useful in Subtext is our SQL
Script execution library encapsulated in the
`Subtext.Scripting.dll `assembly.

A loooong time ago, [Jon
Galloway](http://weblogs.asp.net/jgalloway/ "Jon Galloway") wrote a post
entitled [*Handling GO Separators in SQL Scripts - the easy
way*](http://weblogs.asp.net/jgalloway/archive/2006/11/07/Handling-_2200_GO_2200_-Separators-in-SQL-Scripts-_2D00_-the-easy-way.aspx "Handlyng ")
that tackled the subject of executing SQL Scripts that contain GO
separators using SQL Server Management Objects (SMO). SMO handles GO
separators, but it doesn’t (AFAIK) handle [SQL template
parameters](https://haacked.com/archive/2005/07/01/sql_query_analyzer_parameters.aspx "SQL Template Parameters").

So rather than go the easy way, we went the hard way and wrote our own
library for parsing and executing SQL scripts that contain GO separators
(much harder than it sounds) and template parameters. Here’s a code
sample that demonstrates the usage.

```csharp
string script = @"SELECT * FROM <table1, nvarchar(256), Products>
GO
SELECT * FROM <table2, nvarchar(256), Users>";

SqlScriptRunner runner = new SqlScriptRunner(script);
runner.TemplateParameters["table1"] = "Post";
runner.TemplateParameters["table2"] = "Comment";

using(SqlConnection conn = new SqlConnection(connectionString))
{
  conn.Open();
  using(SqlTransaction transaction = conn.BeginTransaction())
  {
    runner.Execute(transaction);
    transaction.Commit();
  }
}            
```

The above code uses the `SqlScriptRunner` class to parse the script into
its constituent scripts (you can access them via a `ScriptCollection`
property) and then sets the value of two template parameters before
executing all of the constituent scripts within a transaction.

Currently, the class only has one `Execute` method which takes in a
`SqlTransaction` instance. This is slightly cumbersome and it would be
nice to have a version that didn’t need all this setup, but this was all
we needed for Subtext.

When I started writing this post, I thought about making some overrides
that would make this class even easier to use, but instead, I will
[provide a copy of the
assembly](https://haacked.com/code/Subtext.Scripting.zip "The Subtext Scripting Assembly")
and point people to our Subversion repository and hope that someone out
there will find this useful and have enough incentive to submit
improvements!

Also, be sure to check out our unit tests for this class to understand
what I mean when I said it was harder than it look. As a hint, think
about dealing with GO statements in comments and quotes. Also, GO
doesn’t have to be the only thing on the line. Certain specific elements
can come before or after a GO statement on a line.

In case you missed the link, [DOWNLOAD IT
HERE](http://code.haacked.com/util/Subtext.Scripting.zip "Subtext Scripting Assembly").

