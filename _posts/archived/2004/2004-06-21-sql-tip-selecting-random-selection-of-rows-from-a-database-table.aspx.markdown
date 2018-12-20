---
title: 'SQL TIP: Selecting Random Selection Of Rows From A Database Table'
date: 2004-06-21 -0800
tags:
- sql
redirect_from: "/archive/2004/06/20/sql-tip-selecting-random-selection-of-rows-from-a-database-table.aspx/"
---

I found a [nice tip](http://www.sqlteam.com/item.asp?ItemID=8747) for
selecting random rows from within a SQL Server 2000 database. Well
actually, pseudorandom. Since my undergraduate thesis was on the topic
of pseudorandom number generation, I might as well be precise. For some
reason, my non-geek friends find it awfully funny when I mention
pseudorandom numbers.

I digress. In order to select 10 records from some table at random, try
this:

```sql
SELECT TOP 10 * FROM someTable ORDER By NEWID()
```

Now for my homework, I should find out just how random this is. There's
a whole slew of statistical tests I can run to gauge the randomness of
pseudorandom number generator such as the Chi-square Test, Serial
Correlation Coefficient, and 2-D Random Walk Test to name a few.

**IMPORTANT:** Please note that this will NOT work in SQL 7 on NT4
because the NEWID() function there generates sequential results (Bad
SQL! Bad!).

UPDATE: My friend Erik referred me to [this
article](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnsqlpro04/html/sp04c1.asp)
that has an overview of several methods for random sampling. Thanks e.

