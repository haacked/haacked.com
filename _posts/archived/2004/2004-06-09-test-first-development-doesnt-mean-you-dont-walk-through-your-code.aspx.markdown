---
title: Test First Development Doesn't Mean You Don't Walk Through Your Code
date: 2004-06-09 -0800
disqus_identifier: 588
tags:
- code
- tdd
redirect_from: "/archive/2004/06/08/test-first-development-doesnt-mean-you-dont-walk-through-your-code.aspx/"
---

Test First Development, the process of writing unit tests to test the
code you are about to write, is one of my favorite software practices
that has an impact on producing better written code. However, it's no a
panacea. It is true that I use the debugger much less often because of
TDD, but there are still occasions where it's important to manually step
through code line by line.

Personally, I use
[NCover](http://www.gotdotnet.com/Community/Workspaces/Workspace.aspx?id=3122ee1a-46e7-48a5-857e-aad6739ef6b9)
as my first line of defense. It highlights the lines of code that
haven't been executed by my unit tests. A lot of these turn out to be
non-issues such as the last "}" in a method or an assertion that this
line should never happen (for example in the **default:** section of a
switch statement when I believe the default should never be reached).

There are those times, however, when you don't have time to write a unit
test to excercise a particular line of code. Stepping through it is a
wise idea.

Also, unit tests won’t uncover errors of omission. Stepping through your
code will often jog your memory and remind you that, *Hey, I forgot to
make the code jump rope here AND I forgot the jump rope test fixture.*

