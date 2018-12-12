---
title: Writing Testable Code Is About Managing Complexity
date: 2007-11-14 -0800
disqus_identifier: 18423
categories: [code tdd aspnetmvc]
redirect_from: "/archive/2007/11/13/writing-testable-code-is-about-managing-complexity.aspx/"
---

When discussing the upcoming [ASP.NET MVC
framework](http://weblogs.asp.net/scottgu/archive/2007/11/13/asp-net-mvc-framework-part-1.aspx "ASP.NET MVC Framework Part 1"),
one of the key benefits I like to tout is how this framework will
improve testability of your web applications.

The response I often get is the same question I get when mention
patterns such as Dependency Injection, IoC, etc...

> Why would I want to do XYZ just to improve testability?

I think to myself in response

> *Just to improve testability?* *Isn’t that enough of a reason!*

That’s how excited I am about test driven development. Testing seems
enough of a reason for me!

Of course, when I’m done un-bunching my knickers, I realize that despite
all the [benefits of unit testable
code](https://haacked.com/archive/2004/12/06/unit-testing-benefits.aspx "Unit Testing Benefits"),
**the *real* benefit of testable code is how it helps handle the
software development’s biggest problem since time immemorial, *managing
complexity***.

There are two ways that testable code helps manage complexity.

​1. It directly helps manage complexity assuming that you not only write
testable code, but also write the unit tests to go along with them. With
decent code coverage, you now have a nice suite of regression tests,
which helps manage complexity by alerting you to potential bugs
introduced during code maintenance in a large project long before they
become a problem in production.

​2. It indirectly helps manage complexity because in order to write
testable code, you have to employ the principle of [separation of
concerns](http://en.wikipedia.org/wiki/Separation_of_concerns "Separation of Concerns in Wikipedia")
to really write testable code.

Separating concerns within an application is an excellent tool for
managing complexity when writing code. And writing code *is* complex!

The MVC pattern, for example, separates an application into three main
components: the Model, the View, and the Controller. Not only does it
separate these three components, it outlines the loosely coupled
relationships and communication between these components.

### Key Benefits of Separating Concerns

This separation combined with loose coupling allows a developer to
manage complexity because it allows the developer to focus on one aspect
of the problem at a time.

Martin Fowler writes about this benefit in his paper, [Separating User
Interface Code
(pdf)](http://martinfowler.com/ieeeSoftware/separation.pdf "Fowler on Separation of Concerns"):

> A clear separation lets you concentrate on each aspect of the problem
> separately—and one complicated thing at a time is enough. It also lets
> different people work on the separate pieces, which is useful when
> people want to hone more specialized skills. \

The ability to divide work into parallel tracks is a great benefit of
this approach. In a well separated application, if Alice needs time to
implement the controller or business logic, she can quickly stub out the
model so that Bob can work on the view without being blocked by Alice.
Meanwhile, Alice continues developing the business layer without the
added stress that Bob is waiting on her.

### Bring it home

The MVC example above talks about separation of concerns on a large
architectural scale. But the same benefits apply on a much smaller scale
outside of the MVC context. And all of these benefits can be yours as a
side-effect of writing testable code.

So to summarize, when you write testable code, whether it is via Test
Driven Development (TDD) or Test After Development, you get the
following side effects.

1.  A nice suite of regression tests.
2.  Well separated code that helps manage complexity.
3.  Well separated code that helps enable concurrent development.

Compare that list of side effects with the list of side effects of the
latest pharmaceutical wonder drug for curing restless legs or whatever.
What’s not to like!?
