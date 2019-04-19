---
title: Should Unit Tests Touch the Database?
date: 2005-10-20 -0800 9:00 AM
redirect_from:
- "/archive/2005/10/21/10941.aspx"
- "/archive/2005/10/19/should-unit-tests-touch-the-database.aspx/"
tags: [tdd,data]
---

_UPDATE: For the most part, I think young Phil Haack is full of shit in these first two paragraphs. I definitely now think unit tests should NOT touch the database. Instead, I do separate those into a separate integration test suite, as I had suggested in the last paragraph. So maybe Young Phil wasn't so full of shit after all._

I know there are unit testing purists who say unit tests by definition should never touch the database. Instead you should use mock objects or some other contraption. And in part, I agree with them. Given unlimited time, I will gladly take that approach.

But I work on real projects with real clients and tight deadlines. So I will secretly admit that this is one area I am willing to sacrifice a bit of purity. Besides, at some point, you just have to test the full interaction of your objects with the database. You want to make sure your stored procedures are correct etc...

However, I do follow a few rules to make sure that this is as pure as possible.

First, I always try and test against a local database. Ideally, I will script the schema and lookup data so that my unit tests will create the database. MbUnit has an attribute that allows you to perform a setup operation on assembly load and teardown when the tested assembly unloads. That would be a good place to set up the database so you don’t have to do it for every test. However, often, I set up the database by hand once and let my tests just assume a clean database is already there.

Except for lookup data, my tests create all the data they will use using whichever API and objects I am testing. Each test runs within a COM+ 1.5 transaction using a [RollBack attribute](https://haacked.com/archive/2005/06/10/4580.aspx) so that no changes are stored after each test. This ensures that each test is testing against the same exact database.

This is the reason I can be a bit lazy and set up the database by hand, since the none of the tests will change the data in the database. Although I would prefer to have a no-touch approach where the unit tests set up the database. For that, there is
[TestFu](http://www.testdriven.com/modules/mylinks/visit.php?cid=4&lid=499&PHPSESSID=868711b3b596c3eeed313b5d7a2cbac7) which is now part of [TestDriven.Net](http://www.testdriven.com/).

From my experience, I think this approach is a good middle ground for many projects. A more purist approach might separate the tests that touch the database into a separate assembly, but still use NUnit or MbUnit to run them. Perhaps that assembly would be called
IntegrationTests.dll instead of UnitTests.dll. It’s your choice.
