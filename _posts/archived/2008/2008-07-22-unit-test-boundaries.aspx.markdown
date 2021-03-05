---
title: Unit Test Boundaries
tags: [tdd]
redirect_from: "/archive/2008/07/21/unit-test-boundaries.aspx/"
---

One principle to follow when writing a unit test is that a unit test
should ideally [not cross
boundaries](http://www.williamcaputo.com/archives/000019.html "TDD Pattern: Do not Cross Boundaries").

[![965948\_51171615](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UnitTestYourOwnCode_A67B/965948_51171615_3.jpg "965948_51171615")](http://www.sxc.hu/photo/965948 "Barbed Wire")

Michael Feathers takes a harder stance in saying…

> A test is not a unit test if:
>
> -   It talks to the database
> -   It communicates across the network
> -   It touches the file system
> -   It can’t run at the same time as any of your other unit tests
> -   You have to do special things to your environment (such as editing
>     config files) to run it
>
> Tests that do these things aren’t bad. Often they are worth writing,
> and they can be written in a unit test harness. However, it is
> important to be able to separate them from true unit tests so that we
> can keep a set of tests that we can run fast whenever we make our
> changes.

Speed isn’t the only benefit of following these rules. In order to make
sure your tests don’t reach across boundaries, you have to make sure the
unit under test is easily decoupled from code across its boundary, which
provides benefits for the code being tested.

Suppose you have a function that pulls a list of coordinates from the
database and calculated the best fit line for those coordinates. Your
unit test for this method should ideally not make an actual database
call, as that is reaching across a boundary and coupling your method to
a specific data access layer.

Reaching across a boundary is not the only sin of this method. Data
access is an *[orthogonal
concern](http://codebetter.com/blogs/jeremy.miller/archive/2007/01/08/Orthogonal-Code.aspx "Orthogonal Code")*
to calculating the best-fit line of a series of points. In *[The
Pragmatic
Programmer](http://www.amazon.com/gp/product/020161622X?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=9325&creativeASIN=020161622X "The Pragmatic Programmer: From Journeyman to Master")*,
Andrew Hunt and Dave Thomas tout *Orthogonality* as a key trait of well
written code. In an [interview on
artima.com](http://www.artima.com/intv/dryP.html "Artima interview with the Pragmatic Programmers"),
Andy describes orthogonality like so:

> The basic idea of orthogonality is that things that are not related
> conceptually should not be related in the system. Parts of the
> architecture that really have nothing to do with the other, such as
> the database and the UI, should not need to be changed together. A
> change to one should not cause a change to the other. Unfortunately,
> we've seen systems throughout our careers where that's not the case.

Ideally, you would refactor the method so that the data the method needs
is provided to it via some other means (another method passing the data
via arguments, dependency injection, whatever). That other means,
whatever it is, can perform the necessary data access: that’s not your
concern at this moment. You aren’t testing that other means (*right now
at least, you might later*), you’re focused on testing this unit.

This isolation enforced by unit test can be challenging, as it’s easy to
get distracted by these other orthogonal concerns. For example, if this
method doesn’t do data access, which one does? However, having the
discipline to focus on the unit being tested can help shape your code so
that it follows the [single responsibility
principle](http://en.wikipedia.org/wiki/Single_responsibility_principle "Single Responsibility Principle")
(SRP for short). If your test needs to access an external resource, it
might just be violating SRP.

This provides several key benefits.

-   Your function is no longer tightly coupled to the current system.
    You could easily move it to another system that happened to have a
    different data access layer.
-   Your unit test of this function no longer needs to access the
    database, helping to keep execution of your unit tests extremely
    fast.
-   It keeps your unit test from being too fragile. Changes to the data
    access layer will not affect this function, and therefore the unit
    test of this function.

All this decoupling will provide long term benefits for the
maintainability of your code.
