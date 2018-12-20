---
title: Watch Out For the Generics Explosion
date: 2005-10-26 -0800
disqus_identifier: 11033
tags: []
redirect_from: "/archive/2005/10/25/watch-out-for-the-generics-explosion.aspx/"
---

By now, every developer and his mother knows that VS.NET 2005 and SQL
Server 2005 [has been
released](http://blogs.msdn.com/somasegar/archive/2005/10/27/485665.aspx).
Prepare for the generics explosion as legions of .NET developers
retrofit code, happily slapping `<T>` wherever it fits.

I predict the number of angle brackets in C\# will initially increase by
250% only to settle over time to around 75% above current numbers. If
you don’t count the angle brackets in C\# comments, could be even
higher.

But before you go too hog wild with generics, do consider that generics
have an overhead associated with them, especially generics involving a
value type. Their benefits do not come completely free.

As [Rico Mariani](http://blogs.msdn.com/ricom/) pointed out in his PDC
talk, generics involve a form of code generation by the run-time. His
rule of thumb was that when your collection contains around ~~50~~ 500
or so items, you’ll the benefits outweigh the overhead. But as always,
measure measure measure.

In general, the strong typing and improved code productivity outweigh
any performance concerns I have with small collections.

UPDATE: Whoops, I mistyped the number of items Rico mentioned. He said
500, not 50. Thanks for the correction
[Rico](http://blogs.msdn.com/ricom/).

