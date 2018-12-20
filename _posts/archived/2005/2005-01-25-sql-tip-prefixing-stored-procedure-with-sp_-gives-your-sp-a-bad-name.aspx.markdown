---
title: 'SQL TIP: Prefixing Stored Procedure With &quot;sp_&quot; Gives Your SP a Bad
  Name'
date: 2005-01-25 -0800
disqus_identifier: 2013
tags:
- sql
redirect_from: "/archive/2005/01/24/sql-tip-prefixing-stored-procedure-with-sp_-gives-your-sp-a-bad-name.aspx/"
---

Found this [interesting
article](http://www.winnetmag.com/Article/ArticleID/23011/23011.html "article on stored proc naming")
via [Hassan
Voyeau](http://haveworld.blogspot.com/2005/01/sql-tip-sp-prefix.html "Hassan's Blog")
that details the performance penalty when naming your stored procedure
with an *sp\_* prefix in a database other than the master database.

Personally, I hate adding extraneous and unecessary prefixes and
suffixes to names. Sometimes they’re useful and necessary, like when
programming in Fortran 77. But I hate naming tables with a *tbl* prefix
and stored procs with an *sp* prefix (I’m forced to at my current
position). Sql Enterprise Manager does a nice job of separating tables
from stored procedures when they are being displayed. I’m never going to
get the fact confused that that square looking thing on my database
diagram is a table and not a user defined function.

Anyways, Hassan, how’s the weather in Trinidad?

