---
title: Tag Your Database - A Data Dictionary Tool
tags: [data]
redirect_from: "/archive/2006/09/27/database_dictionary_tool.aspx/"
---

A few days back [Jon
Galloway](http://weblogs.asp.net/jgalloway/ "Jon Galloway") and I were
discussing a task he was working on to document a database for a
client.  He had planned to use some code generation to initially
populate a spreadsheet and would fill in the details by hand.  I
suggested he store the data with the schema using [SQL extended
properties](http://www.developer.com/db/article.php/3361751 "Using Sql Extended Properties").

We looked around and found some stored procs for pulling properties out,
but no useful applications for putting them in there in a nice, quick,
and easy manner.

A few days later, the freaking guy releases this [Database Dictionary
Creator](http://weblogs.asp.net/jgalloway/archive/2006/09/28/_5B00_Tool_5D00_-Data-Dictionary-Creator-_2D00_-Rapidly-database-documentation.aspx "Document Your Database"),
a nice GUI tool to document your database, storing the documentation as
part of your database schema.

![Database Dictionary Entry
Form](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DatabaseDictionaryTool_9823/254444157_d05bbdf9d34.jpg)

The tool allows you to add your own custom properties to track, which
then get displayed in the data dictionary form grid as seen above. Audit
and Source are custom properties. It is a way to tag our database
schema.

You ask the guy to build a house with playing cards and he comes back
with the Taj Mahal.

[Check it
out](http://weblogs.asp.net/jgalloway/archive/2006/09/28/_5B00_Tool_5D00_-Data-Dictionary-Creator-_2D00_-Rapidly-database-documentation.aspx "Data Dictionary Creator").

