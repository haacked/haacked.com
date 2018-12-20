---
title: Avoid Using a Database as an API Integration Point
date: 2007-04-23 -0800
tags: []
redirect_from: "/archive/2007/04/22/avoid-using-a-database-as-an-api-integration-point.aspx/"
---

Before I begin, I should clarify what I mean by *using a database as an
API integration point*.

In another life in a distant galaxy far far away, I worked on a project
in which we needed to integrate a partner’s system with our system. The
method of integration required that when a particular event occurred,
they would write some data to a particular table in *our database*,
which would then fire a trigger to perform whatever actions were
necessary on our side (*vague enough for ya?*).

In this case, the data model and the related stored procedures made up
the API used by the partner to integrate into our system.

### So what’s the problem?

I always felt this was ugly in a few ways, I’m sure you’ll think of
more.

1.  First, we have to make our database directly accessible to a third
    party, exposing ourselves to all the security risk that entails.
2.  We’re not really free to make schema changes as we have no
    abstraction layer between the database and any clients to the
    system.
3.  How exactly do you define a contract in SQL? With Web Services, you
    have XSD. With code, you have interfaces.

Personally, I’d like to have some sort of abstraction layer for my
integration points so that I am free to change the underlying
implementation.

### Why am I bringing this up?

A little while ago, I was having a chat with a member of the
Subtext team,
telling him about the custom
[`MembershipProvider`](http://msdn2.microsoft.com/en-us/library/system.web.security.membershipprovider.aspx "MembershipProvider on MSDN")
we’re implementing for Subtext 2.0 to fit in with our data model. His
initial reaction was that developer-users are going to grumble that
we’re not using the “Standard” Membership Provider.

*The “Standard”?*

I question this notion of “*The Standard Membership Provider*”? Which
provider is the standard? Is it the
[`ActiveDirectoryMembershipProvider`](http://msdn2.microsoft.com/en-us/library/system.web.security.activedirectorymembershipprovider.aspx "Membership Provider for the Active Directory on MSDN")?

It is in anticipation of developer grumblings that I write this post to
plead my case and perhaps rail against the wind.

### The point of the Provider Model

You see, it seems that the whole point of the Provider Model is lost if
you require a specific data model. **The whole point of the provider
model is to provide an abstraction to the underlying physical data
store.**

For example, [Rob
Howard](http://weblogs.asp.net/rhoward/ "Rob Howard’s Blog"), one of the
authors of the Provider Pattern wrote this in the second part of [his
introduction to the Provider
Pattern](http://msdn2.microsoft.com/en-us/library/ms972370.aspx "Provider Design Pattern, Part 2")
(emphasis mine).

> A point brought up in the previous article discussed the conundrum the
> ASP.NET team faced while building the Personalization system used for
> ASP.NET 2.0. The problem was choosing the right data model: standard
> SQL tables versus a schema approach. Someone pointed out that the
> provider pattern doesn’t solve this, which is 100% correct. **What it
> does allow is the flexibility to choose which data model makes the
> most sense for your organization. An important note about the pattern:
> it doesn’t solve how you store your data, but it does abstract that
> decision out of your programming interface.**

What Rob and Microsoft realized is that no one data model fits all. Many
applications will already have a data model for storing users and roles.

The idea is that if you write code and controls against the provider
API, **the underlying data model doesn’t matter**. This is emphasized by
the goals of the provider model according to the MSDN introduction...

> The ASP.NET 2.0 provider model was designed with the following goals
> in mind:
>
> -   To make ASP.NET state storage both flexible and extensible \
> -   To insulate application-level code and code in the ASP.NET
>     run-time from the physical storage media where state is stored,
>     and to isolate the changes required to use alternative media types
>     to a single well-defined layer with minimal surface area
> -   To make writing custom providers as simple as possible by
>     providing a robust and well-documented set of base classes from
>     which developers can derive provider classes of their own
>
> It is expected that developers who wish to pair ASP.NET 2.0 with data
> sources for which off-the-shelf providers are not available can, with
> a reasonable amount of effort, write custom providers to do the job.

Of course, Microsoft made it easy for all of us developers by shipping a
full featured
[`SqlMembershipProvider`](http://msdn2.microsoft.com/en-us/library/system.web.security.sqlmembershipprovider.aspx "Sql Membership Provider on MSDN")
complete with database schema and stored procedures. When building a new
implementation from scratch, it makes a lot of sense to use this
implementation. If your needs fit within the implementation, then that
is a lot of work that you don’t have to do.

Unfortunately, many developers took it to be the gospel truth and
standard in how the the data model should be implemented. This is really
only one *possible* database implementation of a Membership Provider.

### An Example Gone Wrong

There is one particular open source application that I recall that
already had a fantastic user and roles implementation at the time that
the Membership Provder Model was released. Their existing implementation
was in all respects, a superset of the features of the Membership
Provider.

Naturally there was a lot of pressure to implement the Membership
Provider API, so they chose to simply implement the
`SqlMembershipProvider`’s tables side by side with their own user
tables.

Stepping through the code in a debugger one day, I watched in disbelief
when upon logging in as a user, the code started copying all users from
the `SqlMembershipProvider`’s stock `aspnet_*` tables to the
application’s internal user tables and vice versa. They were essentially
keeping two separate user databases in synch on every login.

In my view, this was the wrong approach to take. It would’ve been much
better to simply implement a custom `MembershipProvider` class that read
from and wrote to their existing user database tables.

For the features of their existing users and roles implementation that
the Membership Provider did not support, they could have been exposed
via their existing API.

Yes, I’m armchair quarterbacking at this point as there may have been
some extenuating circumstances I am not aware of. But I can’t imagine
doing a full multi-table synch on every login being a good choice,
especially for a large database of users. I’m not aware of the status of
this implementation detail at this point in time.

### The Big But

Someone somewhere is reading this thinking I’m being a bit overly
dogmatic. They might be thinking

> But, but I have three apps in my organization which communicate with
> each other via the database just fine. This is a workable solution for
> our scenario, thank you very much. You’re full of it.

I totally agree on all three counts.

For a set of *internal* applications within an organization, it may well
make sense to integrate at the database layer, since all communications
between apps occurs within the security boundary of your internal
network and you have full control over the implementation details for
all of the applications.

So while I still think even these apps could benefit from a well defined
API or Web Service layer as the point of integration, I don’t think you
should *never* consider the database as a potential integration point.

But when you’re considering integration for external applications
outside of your control, especially applications that haven’t even been
written yet, I think the database is a really poor choice and should be
avoided.

Microsoft recognized this with the Provider Model, which is why controls
written for the `MembershipProvider` are not supposed to assume anything
about the underlying data store. For example, they don’t make direct
queries against the “standard” Membership tables.

Instead, when you need to integrate with a membership database, **use
the API**.

Hopefully future users and developers of Subtext will also recognize
this when we unveil the Membership features in Subtext 2.0 and keep the
grumbling to a minimum. Either that or point out how full of it I am and
convince me to change my mind.

See also: [Where the Provider Model Falls
Short](https://haacked.com/archive/2005/11/01/wheretheprovidermodelfallsshort.aspx "Another post on the Provider Pattern").

