---
title: Switching to MbUnit
date: 2005-10-18 -0800
tags: []
redirect_from: "/archive/2005/10/17/switchingtombunit.aspx/"
---

For the longest time now, I’ve been a fan of MbUnit, but never really
used it on a real project. In part, I stuck with NUnit despite MbUnit’s
superiority because NUnit was the defacto standard.

Well you know what? Pretty much every company or client I end up moving
to has not had unit testing in place before I arrived. I’ve always been
the one to introduce unit testing. So on my latest project, when I
finally meet another developer who writes unit tests, and he is using
MbUnit, I decided to make the switch.

And that, my friends, was a great decision... Why?

**TypeFixture**\
 Say you write an interface `IFoo` (though it could just as easily have
been an abstract class or any base class for that matter). You then
proceed to implement a couple implementations of `IFoo`. Wouldn’t it be
nice to write some unit tests specific to the interface? Here’s how you
do it in MbUnit.

interface IFoo {}

class Bar : IFoo {}

class Baz : IFoo {}

 

[TypeFixture(typeof(IFoo), "Tests the IFoo interface.")]

publicclass IFooTests

{

    [Provider(typeof(Bar))]

    public Bar ProvideBar()

    {

        returnnew Bar();

    }

 

    [Provider(typeof(Baz))]

    public Baz ProvideBaz()

    {

        returnnew Baz();

    }

 

    [Test]

    publicvoid TestIFoo(IFoo instance)

    {

        //Test that the IFoo instance

        //Behaves properly.

    }

}

What you are seeing is a `TypeFixture` which is a type of `TestFixture`
that is useful for testing an interface. There is only one test method
`TestIFoo`. However you should notice that it takes in a parameter of
type `IFoo`.

This deviates from the typical NUnit test which does not allow any
parameters. So just who is passing the test that parameter? The other
methods in the fixture that have been marked with the `Provider`
attribute. The test method is called once for every provider. The
provider methods simply instantiate the concrete instance of the
interface you are testing. So the next time you implement the interface,
you simply add another provider method. Pretty sweet, eh?

**Row Based Testing**\
 I already [wrote a
post](https://haacked.com/archive/0001/01/01/1421.aspx) on the `RowTest`
attribute for MbUnit. It supports a very common test paradigm of using
the same method to test a wide variety of inputs.

**Test Runner**\
 Matt Berther [shows how easy it is to write an
executable](http://www.mattberther.com/2005/10/000677.html) that will
run all your unit tests.

**RollBack Attribute**\
 Attach this attribute to a test method and MbUnit makes sure that any
database transactions are rolled back at the end of the test. There is
an implementation of a [`RollBack` attribute for
NUnit](https://haacked.com/archive/2005/06/10/4580.aspx) out there, but
the extensibility model is tricky, as I found I couldn’t get the
RollBack attribute to work with an `ExpectedException` attribute.

**And More...**\
 MbUnit also has test attributes for repeating a test and repeating a
test on multiple threads. It also has a test fixture designed to test
custom collections that implement `IEnumerable` and `IEnumeration`.

For more information, check out this [Code Project
article](http://www.codeproject.com/gen/design/gunit.asp) and the
[MbUnit wiki](http://www.mertner.com/confluence/display/MbUnit/Home).

In the near future, I’ll be switching the unit tests for Subtext to use
MbUnit. And assuming Dare and Torsten are ok with it, the unit tests for
RSS Bandit.

