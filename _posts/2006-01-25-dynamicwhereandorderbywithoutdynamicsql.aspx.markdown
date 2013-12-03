---
layout: post
title: "Dynamic WHERE and ORDER BY Without Dynamic SQL"
date: 2006-01-25 -0800
comments: true
disqus_identifier: 11598
categories: [sql]
---
My friend Jeremy (no blog) pointed me to these two useful articles on
how to perform dynamic WHERE clauses and ORDER BY clauses without using
dynamic SQL. These were written long ago, but I had never thought to use
COALESCE in this way. Very cool!

I will post them here so I can find them later.

-   [Implementing a Dynamic WHERE Clause](http://www.sqlteam.com/item.asp?ItemID=2077)
-   [Dynamic ORDER BY](http://www.sqlteam.com/item.asp?ItemID=2209)

UPDATE: This technique may not be as performant as hoped for. Marty in
the comments noted that he saw table scans in using COALESCE in this
way. Jeremy showed me an example that demonstrated that the execution
plan changed from an index seek to an index scan when using COALESCE. As
always, test, test, test before rolling this out.

