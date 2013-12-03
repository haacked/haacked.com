---
layout: post
title: "Whidbey Update"
date: 2004-10-22 -0800
comments: true
disqus_identifier: 1457
categories: []
---
[Scott Guthrie](http://weblogs.asp.net/scottgu/) has returned to
blogging with a tremendous
[piece](http://weblogs.asp.net/scottgu/archive/2004/10/23/246709.aspx)
on his team's effort towards reaching "ZBB" or Zero Bug Bounce.

I've personally never worked on software project as large as the ASP.NET
2.0 project, so it's fascinating for me to read Scott's description of
the testing and check-in process. Typically, my check-in process is to
get latest on any files I didn't change, build, and run my unit tests.
Assuming everything passes, I check in my files, get latest again build,
and run the the unit tests again. If everything still passes, I'm done
with the check-in. If all went smoothly, it's all done under half an
hour.

For the ASP.NET team, every check-in undergoes peer review and is run
through a few hours of checkin test suites. They then run more
exhaustive nightly tests over the product to catch issues in the latest
builds. That's pretty impressive.

