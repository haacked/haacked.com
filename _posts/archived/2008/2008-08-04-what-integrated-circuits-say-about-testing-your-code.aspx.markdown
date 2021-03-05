---
title: What Integrated Circuits Say About Testing Your Code
tags: [tdd,patterns,code,testing]
redirect_from: "/archive/2008/08/03/what-integrated-circuits-say-about-testing-your-code.aspx/"
---

A while back I talked about how [testable code helps manage
complexity](https://haacked.com/archive/2007/11/14/writing-testable-code-is-about-managing-complexity.aspx "Testable code manages complexity").
In that post, I mentioned one common rebuttal to certain design
decisions made in code in order to make it more testable.

> Why would I want to do XYZ just do improve testability?

![integrated-circuit](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DesignForTestability_12B08/integrated-circuit_3.png "integrated-circuit")
Recently, I heard one variation of this comment in the comments to my
post on [unit test
boundaries](https://haacked.com/archive/2008/07/22/unit-test-boundaries.aspx "Unit Test Boundaries").
Several people suggested that it’s fine to have unit tests access the
database, after all, the code relies on data from the database, it
should be tested.

Implicit in this statement is the question, “***Why would I want to
abstract away the data access just to improve testability?**”*

*Keep in mind, I never said you shouldn’t test your code’s interaction
with the database. You absolutely should. I merely categorized that sort
of test as a different sort of test - an integration test. You might
still use your favorite unit testing framework to automate such a test,
but I suggest trying to keep it in a separate test suite.*

The authors of [The Pragmatic Programmer: From Journeyman to
Master](http://www.amazon.com/gp/product/020161622X?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=9325&creativeASIN=020161622X)
have a great answer to this question with their comparison to Integrated
Circuit's, which have features designed specifically to enable
testability.

The “[Design For
Test](http://en.wikipedia.org/wiki/Design_For_Test "Design for Test on Wikipedia")”
Wikipedia entry refers to name as encompassing a range of design
techniques for adding features to microelectronic hardware in order to
make it testable. Examples of these techniques show up as early as the
1940s/50s. So designing for testability is not some whiz-bangy latest
methodology flavor of the day the crazy kids are doing.

One key benefit to these techniques is that components can be tested in
relative isolation. You don’t have to place them into a product in order
to test them, though at the same time, they can be tested while within
the product.

So in answer to the original question, I’d ask, **“Why wouldn’t we
design for testability?”**

I think this analogy illustrates one reason why I don’t want my unit
tests talking to the database (apart from wanting the tests to run
fast). Ideally, someone else down the road, new to the project, should
be able to get the latest code from source control and run the unit
tests immediately without having to go through the pains of setting up
an environment with the correct database.

Another benefit of abstracting away the database so that your code is
testable and doesn’t cross boundaries is that your code is then not so
dependent on a particular database. I used to argue that there’s no need
to insulate your code from the particular database that you are using.
I’ve never been on a project where the customer suddenly switches from
SQL Server to Oracle. That sort of drastic change very rarely happens.

But it turns out that I *have* been on projects where we switched from
SQL Server 6.5 to 7 (and from 7 to 2000 and so on). Upgrades can be
nearly as drastic as choosing a different database vendor. Having your
code isolated from your choice of database provides some nice peace of
mind here.

