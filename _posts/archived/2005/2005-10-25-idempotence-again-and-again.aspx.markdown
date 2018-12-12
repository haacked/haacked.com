---
title: Idempotence Again and Again
date: 2005-10-25 -0800
disqus_identifier: 11021
categories: []
redirect_from: "/archive/2005/10/24/idempotence-again-and-again.aspx/"
---

Eric Lippert does a [great job of defining the
term](http://blogs.msdn.com/ericlippert/archive/2005/10/26/483900.aspx)
*Idempotent*. I’ve used this term many times both to sound smart and
because it is so succinct.

The one place I find idempotence really important is creating update
scripts for a database such as the Subtext database. In an open source
project geared towards other devs, you just have to assume that people
are tweaking and applying various updates to the database. You really
have no idea in what condition the database is going to be in. That’s
where idempotence can help.

For example, if an update script is going to add a column to a table, I
try to make sure the column isn’t already there already, before adding
the column. That way, if I run the script twice, three times, twenty
times, the table is the same as if I ran it once. I don’t end up adding
the column multiple times.

