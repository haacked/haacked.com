---
title: Tradeoffs When Minimizing The Impact Of Changes
tags: [software]
redirect_from: "/archive/2006/11/15/Tradeoffs_When_Minimizing_The_Impact_Of_Changes.aspx/"
---

[![Silver Bullet: From
http://www.tejasthumpcycles.com/Parts/LeversGripsctrls/Silver\_Bullet/Silver\_Bullet\_Shift\_Brake.jpg](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TradeoffsWhenMinimizingTheImpactOfChange_1D54/Silver_Bullet_thumb%5B1%5D.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TradeoffsWhenMinimizingTheImpactOfChange_1D54/Silver_Bullet%5B3%5D.jpg)
In a recent post I talked about how [good design attempts to minimize
the impact of
changes](https://haacked.com/archive/2006/11/13/Good_Design_Minimizes_The_Impact_Of_Changes.aspx "Good Design Minimizes Changes")
to a system, often through Design Patterns.

When used appropriately, Design Patterns are a great tool for building a
great design, but there is an important caveat to keep in mind anytime
you apply a pattern. **A Design Pattern might minimize the impact of one
kind of change at the expense of amplifying another type of change.**

What do I mean by this? One common pattern is the [Abstract Factory
pattern](http://en.wikipedia.org/wiki/Abstract_factory_pattern) which is
often manifested in .NET code via the [Provider
Model](http://msdn.microsoft.com/asp.net/downloads/providers/default.aspx?pull=/library/en-us/dnaspnet/html/asp02182004.asp)
pattern. The Provider Model abstracts access to an underlying resource
by providing a fixed API to the resource. This does a bang up job of
insulating the consumer of the provider when changing the underlying
resource.

The
[`MembershipProvider`](http://msdn2.microsoft.com/en-us/library/system.web.security.membershipprovider.aspx)
is one such implementation of the provider model pattern. The consumer
of the `MembershipProvider` API doesn’t need to change if the
[`SqlMembershipProvider`](http://msdn2.microsoft.com/en-us/library/system.web.security.sqlmembershipprovider.aspx)
is swapped in favor of the
[`ActiveDirectoryMembershipProvider`](http://msdn2.microsoft.com/en-us/library/system.web.security.activedirectorymembershipprovider.aspx).
This is one way that the provider pattern attempts to minimize the
impact of changes. It insulates against changes to the underlying data
store.

However there is a hidden tradeoff with this pattern. Suppose the API
itself changes often. Then, the impact of a single API change is
multiplied across every concrete implementation of the provider. In the
case of the `MembershipProvider`, this is pretty much a non-issue
because the likelihood of changing the API is very small.

But the same cannot be said of the data access layer for software such
as a blog (or similar software). A common approach is to implement a
`BlogDataProvider` to encapsulate all data access so that the blog
software can make use of multiple databases. The basic line of thought
is that we can simply implement a concrete provider for each database we
wish to support. So we might implement a `SqlBlogDataProvider`, a
`MySqlBlogDataProvider`, a `FireBirdBlogDataProvider`, and so on.

This sounds great in theory, but it breaks down in practice because
unlike the API to the `MembershipProvider`, the API for a
`BlogDatabaseProvider` is going to change quite often. Pretty much every
new feature one can think of often needs a backing data store.

Everytime we add a new column to a table to support a feature, the
impact of that change is multiplied by the number of providers we
support. I discussed this in the past in my post entitled [Where the
Provider Model Falls
Short](https://haacked.com/archive/2005/11/01/WhereTheProviderModelFallsShort.aspx).

Every **Design Pattern comes with inherent tradeoffs that we must be
aware of**. There is no silver bullet.

The key here when looking to apply patterns is to not follow a script
for applying patterns blindly. Look at what changes often (in this case
the database schema) and figure out how to minimize the impact of that
change. In the above scenario, one option is to simply punt the work of
supporting multiple databases to someone else in a more generic fashion.

For example, using something like
[NHibernate](http://www.hibernate.org/343.html) or
[Subsonic](http://www.codeplex.com/Wiki/View.aspx?ProjectName=actionpack)
in this situation might mean that a schema change only requires changing
one bit of code. Then NHibernate or Subsonic is responsible for making
sure that the code works against its list of supported databases.

One might object to these approaches because they feel these approaches
cannot possibly query every database they support as efficiently as
database specific SQL as one would do in a database specific provider.
But I think the 80/20 rule applies here. Let the dynamic query engine
get you 80% of the way, and use a provider just for the areas that need
it.

So again, this is not an indictment of the provider model. The provider
model is extremely useful when used appropriately. As I mentioned, the
Membership Provider is a great example. But if you really need to
support multiple databases AND your database schema is succeptible to a
lot of churn, then another pattern may be in order.

