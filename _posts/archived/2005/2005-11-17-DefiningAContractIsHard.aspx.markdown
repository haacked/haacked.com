---
title: Defining a Contract Is Hard
date: 2005-11-17 -0800
disqus_identifier: 11211
tags: []
redirect_from: "/archive/2005/11/16/DefiningAContractIsHard.aspx/"
---

As soon as I saw the code sample on [K. Scott Allen’s latest blog
post](http://odetocode.com/Blogs/scott/archive/2005/11/17/2479.aspx), I
knew he was talking about the Membership Provider.

His example nails it when describing the complexities of defining a
contract via an interface. It just isn’t expressive enough.
Documentation is always necessary.

I ran into this recently when trying to implement a custom Membership
Provider for DotNetNuke. It turns out that DotNetNuke maintains its own
user and role tables. These “satellite” tables map to the ASP.NET
membership tables (but do not have any foreign key constraints) since
they provide more information specific to DotNetNuke.

So how do the satellite DNN tables stay in synch with your provider data
store? Well when you login and DNN doesn’t have a record of the current
user, DNN will call into your provider to get a list of users and then
it *iterates through each one* adding the user to its own user tables if
the user doesn’t already exist.

I couldn’t even get the roles to synchronize properly, but I didn’t
spend enough time. What I discovered is that following the Membership
Provider contract wasn’t enough. I actually needed to know what the
consumer was doing with my provider to understand why it would take so
long to login a user.

