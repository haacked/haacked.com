---
title: Does the ADO.NET DataTable respect the Order By Clause?
date: 2004-02-05 -0800
disqus_identifier: 168
categories: []
redirect_from: "/archive/2004/02/04/does-the-adonet-datatable-respect-the-order-by-clause.aspx/"
---

Can somebody out there point me to a reference that explicitly says
whether or not we can count on the underlying order of a DataTable rows
to be the same order that is returned by a SQL statement or a Stored
Procedure?

I read a posting somewhere where the author states that though it
appears to be the case that the DataTable rows are ordered in the same
order as retrieved from the database, that this ordering is not
guaranteed by ADO.NET and should not be relied upon. For those familiar
with hash tables, you know that a hashtable gives no guarantees about
how elements are sorted.

Now I know all about the DataView class and how that can be used to have
a sorted view of items in a DataTable. But my concern is this. Suppose
the DataTable does respect ordering (for now) and thus my underlying
data is already sorted. If the DataView uses traditional Quicksort to
sort the data, that is the pathological worst case. Now there are new
[variants](http://www.whitsoftdev.com/qsort/) of quicksort that handle
already sorted data just fine. I have yet to run benchmarks to find out
how the DataView performs.

