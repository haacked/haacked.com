---
layout: post
title: Where the Provider Model Falls Short
date: 2005-11-01 -0800
comments: true
disqus_identifier: 11066
categories: []
redirect_from: "/archive/2005/10/31/wheretheprovidermodelfallsshort.aspx/"
---

Now that ASP.NET 2.0 is released, a lot of developers will start to
really dig into the [provider model design pattern and
specification](http://msdn.microsoft.com/asp.net/beta2/providers/default.aspx?pull=/library/en-us/dnaspnet/html/asp02182004.asp)
and its various implementations. The provider model is really a
[blending of several design
patterns](http://weblogs.asp.net/asmith/archive/2004/04/13/112076.aspx),
but most closely resembles the [abstract
factory](http://www.dofactory.com/Patterns/PatternAbstract.aspx).

Where the provider model really busts out the flashlight and shines is
when an application (or subset of an application) has a fairly fixed
API, but requires flexibility in the implementation. For example, the
[Membership
Provider](http://msdn2.microsoft.com/en-us/library/f1kyba5e(en-us,vs.80).aspx)
has a fixed API for dealing with users and roles, but depending on the
configured provider, could be manipulating users in a database, in
Active Directory or a 4'x6' piece of plywood. The user of the provider
doesn’t need to know.

**Provider Misuse**\
 However, one common area where I’ve seen providers misused is in an
attempt to abstract the underlying database implementation away from
application. For example, in many open source products such as
[DotNetNuke](http://www.dotnetnuke.com/), an underlying goal is to
support multiple database providers. However, the provider model in
these applications tend to be a free for all data access API. For
example, in the .TEXT (and currently Subtext) codebase, there is one
provider that is responsible for nearly all data access. It has a huge
number of methods which aren’t factored into well organized coherent
APIs.

The other inherent flaw in many of these approaches is they violate a
key principle of good object oriented design...

> Good design seeks to **insulate** code from the impact of changes to
> other code.

Take Subtext as an example. Suppose we want to add a column to a table.
Not only do we have to update the base provider to account for the
change, we also have to update *every single concrete provider* that
implements the provider (*assuming we had some*). Effectively, the
provider model in this case **amplifies** the effect of a schema change.
The result is that It makes your proverbial butt look fat.

This is why you see an appalling lack of concrete providers for
applications such as DotNetNuke, .TEXT, Subtext etc.... Despite the fact
that they all implement the provider model, very few take (nor have) the
time to implement a concrete provider.

**A better way**\
 For most professional web projects, this is not really an issue since
your client probably has little need to switch the database upon which
the application is built. However, if you are building a system (such as
an open source blogging engine) in which the user may want to plug in
nearly any database, this is a much bigger issue.

After a bit of research and using an ORM tool on a project, I’ve stepped
away from being a religious stored procedure fanatic and am much more
open to looking at object/relational mapping tools and dynamic query
engines. ORM tools such as [LLBLGen
Pro](http://www.llblgen.com/pages/secure/entrance.aspx) and
[NHibernate](http://wiki.nhibernate.org/display/NH/Home) make use of
dynamically generated prepared sql statements. Now before you dismiss
this approach, bear with me. Because the statements are prepared, the
performance difference between these queries and a stored procedure are
marginal. In fact, a dynamic sql statement can often even outperform a
stored proc because it is targeted to the specific case, whereas stored
procs tend to support the general case. One example is the dynamic query
that only selects the needed columns from a table and not every column.

**Better Insulation**\
 The key design benefit of such a tool is that they *insulate the main
application from the impact of schema changes*. Add a column to a table
and perhaps you only need to change one class and a mapping file. The
other key benefit is that these tools already support multiple
databases. Every time the ORM vendor spends time implementing support
for a new database system, you’re application supports that database for
free! That’s a lot of upside.

Think about that for a moment. [NHibernate currently
supports](http://wiki.nhibernate.org/display/NH/Supported+Databases)
DB2, Firebird, Access, MySql, PostgreSQL, Sql Server 2000, and SqlLite.
Think about how much time and effort it would take to implement a
provider for each of these databases.

The very fact that you don’t see a plethora of database providers for
DNN, .TEXT, etc... is convincing evidence that the provider model falls
short in being a great solution for abstracting the database layer from
application code. It is great for small well defined APIs, but not well
suited for a generalized data api where there tends to be a lot of code
churn.

To this end, the Subtext developers are investigating the potential for
using NHibernate in a future version of Subtext.

**Referenced Links and other resources**\

-   [Provider Model Design Pattern and Specification, Part
    1](http://msdn.microsoft.com/asp.net/beta2/providers/default.aspx?pull=/library/en-us/dnaspnet/html/asp02182004.asp)
-   [Provider Model Design Pattern, Part
    2](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnaspnet/html/asp02182004.asp)
-   [On Utility of Provider
    Model](http://aspnetresources.com/blog/utility_of_provider_model.aspx)
-   [How the provider model isn't just a rename of a basic design
    pattern](http://weblogs.asp.net/asmith/archive/2004/04/13/112076.aspx)
-   [Abstract Factory
    Pattern](http://www.dofactory.com/Patterns/PatternAbstract.aspx)
-   [NHibernate supported
    databases](http://wiki.nhibernate.org/display/NH/Supported+Databases)


