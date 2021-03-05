---
title: Embedding Unit tests
tags: [tdd]
redirect_from: "/archive/2004/04/06/embedding-unit-tests.aspx/"
---

![](/assets/images/unittests.jpg) I just read Bruce Eckel's [blog
entry](http://mindview.net/WebLog/log-0054) about embedding unit tests
in code. Ideally, he'd like compiler support in programming languages
for unit tests.

> The resulting syntax produced what seems like an obvious Java solution
> for the same problem: embed the essence of the unit test code within
> inline comments in the code for the class to be tested, and then
> automatically generate the JUnit code - which can then be used without
> any examination or intervention on the part of the programmer. To make
> changes, the programmer only needs to change the commented source
> code, and run the JUnit generator again.

It's an intriguing idea and what I like about it is that these embedded
tests have access to test code that you can't otherwise test with an
NUnit or JUnit framework such as private member variables and local
variables within a method. It allows for very tight testing.
Additionally, it places the test code as close to the source code as
possible, directly embedded alongside of it. Perhaps a compiler with the
highest warning level set would run all the tests and break if a test
fails.

There are two things that make me wary of this approach. One, I feel it
could muddy up the code a bit. Maybe this is just me being resistant to
change, but I like the fact that all my test code is in a separate class
library (but part of my solution). When reading the main code, it's just
the main code. I don't have to wade through lines and lines of test code
(which can easily include many more lines than the code being tested).

The second issue is that unit tests are superb for testing how well
factored your classes are. When you write unit tests such that they are
another client of your code (without access to your classes' internals),
it helps uncover usability issues with your code. If you find that you
need access to private members in order to fully test your code, more
times than not, it is a sign that your code needs to be refactored. I
have no problems with refactoring code to make it easier to test because
as I said earlier, unit tests are just another client to your code. If
it's easier to unit test, it's probably easier to use for other clients.

Having said that, I realize that having compiler support for unit tests
won't necessarily stop anyone from writing unit tests as a client. In
fact, it's probably still a good idea despite my two issues with it.
However, I'm not convinced it would lead to much better testing unless
it was used sparingly.

