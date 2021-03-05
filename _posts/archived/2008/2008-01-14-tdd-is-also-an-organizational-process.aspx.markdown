---
title: TDD Is Also An Organizational Process
tags: [tdd,microsoft,methodologies]
redirect_from: "/archive/2008/01/13/tdd-is-also-an-organizational-process.aspx/"
---

After [joining
Microsoft](https://haacked.com/archive/2007/09/17/why-is-microsoft-removing-my-mvp-status.aspx "Why Am I Losing My MVP status?")
and [drinking from the
firehose](https://haacked.com/archive/2007/10/26/drinking-from-the-firehose.aspx "Drinking from the Firehose at Microsoft")
a bit, I’m happy to report that I am still alive and well and now
residing in the Seattle area along with my family. In meeting with
various groups, I’ve been very excited by how much various teams here
are embracing Test Driven Development (and its close cousin, TAD aka
Test After Development). We’ve had great discussions in which we really
looked at a design from a TDD perspective and discussed ways to make it
more testable. Teams are also starting to really apply TDD in their
development process as a team effort, and not just sporadic individuals.

![Bugs](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/BugDrivenDevelopment_851F/1364145387_b8cf994488_4.jpg)TDD
doesn’t work well in a vacuum. I mean, it *can* work to adopt TDD as an
individual developer, but the effectiveness of TDD is lessened if you
don’t consider the organizational changes that should occur in tandem
when adopting TDD.

One obvious example is the fact that TDD works much better when everyone
on your team applies it. If only one developer applies TDD, the
developer loses the regression tests benefit of having a unit test suite
when making changes that might affect code written by a coworker who
doesn’t have code coverage via unit tests.

Another example that was less obvious to me until now was how the role
of a dedicated QA (Quality Assurance) team changes subtly when part of a
team that does TDD. This wasn’t obvious to me because I’ve rarely been
on a team with such talented, technical, and dedicated QA team members.

QA teams at Microsoft invest heavily in test automation from the UI
level down to the code level. Typically, the QA team goes through an
exploratory testing phase in which they try to break the app and suss
out bugs. This is pretty common across the board.

The exploratory testing provides data for the QA team to use in
determining what automation tests are necessary and provide the most
bang for the buck. Engineering is all about balancing constraints so we
can’t write every possible automated test. We want to prioritize them.
So if exploratory testing reveals a bug, that will be a high priority
area for test automation to help prevent regressions.

However, this appears to have some overlap with TDD. Normally, when
someone reports a bug to me, the test driven developer, I will write a
unit test (or tests) that should pass but fails *because of that bug*. I
then fix the bug and ensure my test (or tests) now pass because the bug
is fixed. This prevents regressions.

In order to reduce duplication of efforts, we’re looking at ways of
reducing this overlap and adjusting the QA role slightly in light of TDD
to maximize the effectiveness of QA working in tandem with TDD.

In talking with our QA team, here's what we came up with that seems to
be a good guideline for QA in a TDD environment:

-   QA is responsible for exploratory testing along with non-unit tests
    (system testing, UI testing, integration testing, etc...). TDD often
    focuses on the task at hand so it may be easy for a developer to
    miss obscure test cases.
-   Upon finding bugs in exploratory testing, assuming the test case
    doesn't require external systems (aka it isn't a system or
    integration test), the developer would be responsible for test
    automation via writing unit tests that expose the bug. Once the test
    is in place, the developer fixes the bug.
-   In the case where the test requires integrating with external
    resources and is thus outside the domain of a unit test, the QA team
    is responsible for test automation.
-   QA takes the unit tests and run them on all platforms.
-   QA ensures API integrity by testing for API changes.
-   QA might also review unit tests which could provide ideas for where
    to focus exploratory testing.
-   Much of this is flexible and can be negotiated between Dev and QA as
    give and take based on project needs.

Again, it is a mistake to assume that TDD should be done in a vacuum or
that it negates the need for QA. TDD is only [one small part of the
testing
story](https://haacked.com/archive/2005/10/18/UnitTestingLovesBetaTestingAndViceVersa.aspx "Unit Testing Loves Beta Testing")
and if you don't have testers, [shame on
you](http://www.joelonsoftware.com/articles/fog0000000067.html "Top Five (Wrong) Reason You Don't Have Testers")
;) . One anecdote a QA tester shared with me was a case where the
developer had nice test coverage of exception cases. In these cases, the
code threw a nice informative exception. Unfortunately a component
higher up the stack swallowed the exception, resulting in a very
unintuitive error message for the end user. This might rightly be
considered an integration test, but the point is that relying soley on
the unit tests caused an error to go unnoticed.

TDD creates a very integrated and cooperative relationship between
developers and QA staff. It puts the two on the same team, rather than
simply focusing on the adversarial relationship.

