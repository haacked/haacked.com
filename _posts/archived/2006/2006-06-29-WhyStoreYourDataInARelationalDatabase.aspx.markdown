---
title: Why Store Your Data In A Relational Database?
date: 2006-06-29 -0800 9:00 AM
tags: [sql,data,patterns]
redirect_from: "/archive/2006/06/28/WhyStoreYourDataInARelationalDatabase.aspx/"
---

With Ted Neward’s recent post on the [morass that is Object-Relational
mapping](http://blogs.tedneward.com/2006/06/26/The+Vietnam+Of+Computer+Science.aspx "The Vietnam of Computer Science"),
there has been a lot of discussion going around on the topic. In the
comments on [Atwood’s
post](http://www.codinghorror.com/blog/archives/000621.html "Object Relational Mapping")
on the subject, some commenters ask why put data in a relational
database. Why not use an object database?

The Relational Model is a general theory of data management created by
[Edgar F.
Codd](http://en.wikipedia.org/wiki/Edgar_F._Codd "Wikipedia - Edgar F. Codd")
based on predicate logic and set theory. As such, it has a firm
mathematical foundation for storing data with integrity and for
efficiently pulling data using set based operations. Also, as a timeless
mathematical theory it has no specific ties to any particular framework,
platform, or application.

Now enter object databases which I am intrigued by, but have yet to
really dig into. From what I have read (and if I am off base, please
enlighten me) these databases allow you to store your object instances
directly in the database, probably using some sort of serialization
format.

Seems to me this could introduce several problems. First, it potentially
makes set based operations that are not based on the object model
inefficient. For example, to build a quick ad-hock report, I would have
to write some code to traverse the object hierarchy, which might not be
an efficient means to obtaining the particular data. Perhaps an object
query language would help mitigate or even solve this. I don’t know.

Another issue is that your data is now more opaque. There are all sorts
of third party tools that work with relational data almost without
regards to the database platform. It is quite easy to take Access and
generate a report against an existing SQL database or to use other tools
for exporting data out of a relational database. But since object
oriented databases lack a formal mathematical foundation, it may be
difficult to create a standard for connecting to and querying object
databases that every vendor will agree on.

One last issue is more big picture. It seems to me it ties the data too
much to the current code implementation. I have worked on a project that
was originally written in classic ASP with no objects. The code I wrote
used .NET and objects to access the same data repository. Fortunately,
since the data was in a normalized relational database, it was not a
problem to understand the data simply from looking at a schema and load
it into my new objects.

How would that work with an object database? If I stored my Java objects
in an OO database today, would I be able to load that data into my .NET
objects tomorrow without having to completely change the database? What
about in the future when I move on from .NET objects to message oriented
programming or agent oriented programming?

Ultimately, the choice between an OO database and a relational database
really depends on the particular requirements of the project at hand.
However the thought of tying an application to an OO database at this
point in time gives me reason to pause. This could lock me into a
technology that works today, but is superseded tomorrow. On several
projects I have worked on, we totally revamped the core technology
(typically ASP to ASP.NET), but we rarely scrapped and recreated the
database. The database engine might change over the years (Sql 6.5 to
Sql 7 to Sql 2000 to Sql 2005), but the data model survives.

