---
title: Unit Testing is a Poor Example to Demonstrate a Complaint About Methodologies
date: 2004-12-06 -0800
tags: [code,tdd,methodologies]
redirect_from: "/archive/2004/12/05/unit-testing-benefits.aspx/"
---

Although I agree in spirit with most of [Joel’s discussion of
methodologies](http://www.joelonsoftware.com/items/2004/12/06.html) and
rock star programmers, I’m in a bit of disagreement over the quote from
Tamir he posts.

> For instance, in software development, we like to have people
> unit-test their code. However, a good, experienced developer is about
> 100 times less likely to write bugs that will be uncovered during unit
> tests than a beginner. It is therefore practically useless for the
> former to write these...

I disagree with this, but in part because I think this is based on a
flawed assumption over the purpose of unit testing. This point assumes
that the only objective for unit tests is to uncover bugs. In reality,
unit tests serve a much larger purpose.

**1. Unit Tests promote better interfaces.**\
 Certainly, if you are truly a rock-star developer, writing a class that
is extremely usable might come intuitively to you. But I think even rock
stars can benefit from writing client code that uses a class the
developer is building. This process helps to make sure that the
interfaces to the class are thought out, and probably doesn’t take much
more time than thinking through the class design before coding.

**2. Unit Tests are a great form of documentation.**\
 When learning a new API, often the first (or second) thing I want to
see is sample code that uses the API. Well written unit tests are a
great source of documentation for how a particular class is meant to be
used.

**3. Unit Tests are great as regression tests**\
 So you’re a rock star who doesn’t write buggy code. Are you sure the
person who is going to maintain your code is also a rock star? What
about the person who wrote the class your code is dependent on? As Code
Complete states, 80% of a project’s timeline is spent after the code is
deployed in the maintenance phase. At some point, someone will come in
and modify the code and perhaps change it in a subtle way that doesn’t
appear to be a bug, but violates an assumption made by the initial
programmer. There are a thousand ways in which a developer can write
perfect code, but have it break either now or later. Well written unit
tests can provide a high degree of confidence that bugs that are
introduced later are discovered quickly. It’s no panacea, but it’s sure
as heck a lot better than having none.

So while I agree that blindly following methodologies is a hindrance to
truly talented developers, I do believe that there are some practices
that are worthwhile across the board. In a team environment,
communication is of the utmost importance. Most developers don’t work in
a vacuum and a writing unit tests is one of those practices that really
help communication within a project and beyond. It helps communications
with current team members as well as future team members to come.

So yes, being a monkey in a methodology is bad, but I think there are
better illustrations of this than Unit Testing.

UPDATE: I’m not the only one who believes this. Seems Roy has a similar
[opinion to
mine](http://weblogs.asp.net/rosherove/archive/2004/12/07/276040.aspx).
As does [Jason Kemp](http://www.jasonkemp.ca/) in [my
comments](https://haacked.com/archive/2004/12/06/1704.aspx#1706).

