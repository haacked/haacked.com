---
title: Rollback Attribute for NUnit 2.2.1 and Above
date: 2005-06-10 -0800
disqus_identifier: 4580
categories: []
redirect_from: "/archive/2005/06/09/rollback-attribute-for-nunit-221-and-above.aspx/"
---

Oh man, I have been head deep into “real” work lately, which explains
the relative silence on my blog. In any case, it’s time to jump back in
the fray with some light technical content.

Starting with version 2.2.1, the ever so handy
[NUnit](http://www.nunit.org/ "Nunit") unit-testing framework finally
supports building custom test attributes. This allows you to create your
own attributes that you can attach to tests to allow you to run custom
code before and after a test and handle what to do if an exception is
thrown or not thrown.

[Roy Osherove](http://weblogs.asp.net/rosherove/), a unit-testing
maestro, [wrote up a simple abstract base
class](http://weblogs.asp.net/rosherove/articles/ExntendingNunit221.aspx)
developers can implement that greatly simplifies the process of creating
a custom test attribute.

In an [MSDN
article](http://msdn.microsoft.com/msdnmag/issues/05/06/UnitTesting/default.aspx),
Roy outlines various methods for dealing with database access within
unit tests including a particularly promising method using COM+ 1.5. He
mentions that he’s implemented a Rollback attribute for a custom version
of NUnit he [calls
NUnitX](http://weblogs.asp.net/rosherove/archive/2004/07/12/180189.aspx).

Not wanting to run a custom implementation of NUnitX, I quickly
implemented a Rollback attribute for NUnit 2.2.1. Heck, it was quite
easy considering that Roy did all the fieldwork.

Unfortunately, I ran into a few problems. The custom attribute framework
is not quite fully baked yet. When you apply a custom test attribute,
your attribute may break other attributes. Case in point, my
ExpectedException attributes suddenly stopped working. Looking through
the NUnit codebase, it appears that the first attribute loaded handles
the ProcessNoException and ProcessException method calls.

This is a known issue and an NUnit developer stated that he’s working on
it. In the meanwhile, I worked around this issue with a beautiful
kludge. I simply extended my Rollback attribute to absorb the
functionality of an ExpectedException attribute. This is really ugly,
but it does the job. So if you use this Rollback attribute, you can also
specify an expected exception like so:

[Rollback(typeof(InvalidOperationException))]

I know, “ewwww!”. This is essentially an attribute doing double work.
It’s probably better to name it "RollbackExpectedException" attribute.
But I hope to remove this functionality at a later date when the custom
attribute support in NUnit is more full baked.

The second problem I ran into is that this approach enlists the native
Distributed Transaction Manager for SQL Server (in my situation). In one
project, I’m testing against a remote database and native transactions
are turned off for security purposes. The solution in this case is to
use TIP, or Transaction Internet Protocol. This would require modifying
the "BeforeTestRun" method to (notice the added line in bold):

ServiceConfig config = new ServiceConfig();

**config.TipUrl = "http://YourTIPUrl/";**

config.Transaction = TransactionOption.RequiresNew;

ServiceDomain.Enter(config);

So far, I haven’t been able to get our system administrator to enable
TIP so I haven’t fully tested this last bit of chicanery.

In any case, make sure to read Roy’s articles noted above before
[downloading this code](https://haacked.com/code/RollbackAttribute.zip).
I’ve made some slight modifications to his base class to reflect
personal preferences. Let me know if you find this useful.

[Listening to: Wake Up - Rage Against The Machine - Rage Against The
Machine (6:04)]

