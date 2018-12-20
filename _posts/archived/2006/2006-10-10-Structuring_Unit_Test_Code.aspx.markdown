---
title: Structuring Unit Test Code
date: 2006-10-10 -0800
disqus_identifier: 17988
tags:
- code
- tdd
redirect_from: "/archive/2006/10/09/Structuring_Unit_Test_Code.aspx/"
---

UPDATE: I’ve since supplemented this [with another
approach](https://haacked.com/archive/2012/01/01/structuring-unit-tests.aspx "Structuring unit tests").

[Jeremy
Miller](http://codebetter.com/blogs/jeremy.miller/ "The Shade Tree Developer")
[asks the
question](http://codebetter.com/blogs/jeremy.miller/archive/2006/10/07/Week-1-Questions_3A00_--How-do-you-organize-your-NUnit-test-code_3F00_.aspx "Organizing Unit Test Code"),
“How do you organize your NUnit test code?”.  My answer? I don’t,
I organize my [MbUnit](http://mbunit.com/ "MbUnit") test code.

Bad jokes aside, I do understand that his question is more focused on
the structure of **unit** testing code and not the structure of
any particular unit testing framework.

I pretty much follow the same structure that Jeremy does in that I have
a test fixture per class (sometimes more than one per class for special
cases).  I experimented with having a test fixture per method, but gave
up on that as it became a maintenance headache.  Too many files!

One convention I use is to prefix my unit test projects with
"UnitTest".  Thus the unit tests for
[Subtext](http://subtextproject.com/ "Subtext Project Website") are in
the project *UnitTests.Subtext.dll*.  The main reason for this, besides
the obvious fact that it’s a sensible name for a project that contains
unit tests, is that for most projects, the unit test assembly would show
up on bottom in the Solution Explorer because of alphabetic ordering.

So then I co-found a [company](http://veloc-it.com/ "VelocIT") whose
name starts with the letter V.  Doh!

UPDATE: I neglected to point out (as [David Hayden
did](http://codebetter.com/blogs/david.hayden/archive/2006/10/11/Organizing-Unit-Tests-and-Projects-With-Solution-Folders.aspx "Solution Folders"))
that with VS.NET 2005 I can use Solution Folders to group tests. We
actually use Solution Folders within Subtext. Unfortunately, many of my
company work is still using VS.NET 2003, which does not boast such a
nice feature.

One thing I don’t do is separate my unit tests and integration tests
into two separate assemblies.  Currently I don’t separate those tests at
all, though I have plans to start. 

Even when I do start separating tests, one issue with having unit tests
in two separate assemblies is that I don’t know how to produce NCover
reports that merge the results of coverage from two separate assemblies.

One solution I proposed in the comments to Jeremy’s post is to use a
single assembly for tests, but have UnitTests and Integration Tests live
in two separate top level namespaces.  Thus in MbUnit or in TD.NET, you
can simply run the tests for one namespace or another.

Example Namespaces: `Tests.Unit` and `Tests.Integration`

In the root of a unit test project, I tend to have a few helper classes
such as `UnitTestHelper`, which contains static methods useful for unit
tests. I also have a `ReflectionHelper` class, just in case I need to
“cheat” a little. Any other classes I might find useful typically go in
the root, such as my `SimulatedHttpRequest` class as well.



