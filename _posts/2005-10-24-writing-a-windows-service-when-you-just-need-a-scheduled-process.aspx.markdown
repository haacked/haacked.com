---
layout: post
title: "Writing a Windows Service When You Just Need A Scheduled Process"
date: 2005-10-24 -0800
comments: true
disqus_identifier: 10997
categories: []
---
Sometimes someone writes a post that makes you say, “Oh shit!”. For
example, [Jon Galloway](http://weblogs.asp.net/jgalloway/) writes that
writing a [windows service just to run a scheduled
process](http://weblogs.asp.net/jgalloway/archive/2005/10/24/428303.aspx)
is a bad idea.

And he presents a very nice case. Nice enough that I take back the times
I have condescendingly said that Windows Services are easy to write in
.NET. I probably should look through some of the services I have written
in the past. I know of one I could easily convert to a console app and
*gain* functionality.

However, I think the decision sometimes isn’t so easy as that. One
service I have written in the past is a Socket Server that takes in
encrypted connections and communicates back with the client. Now that
obviously needs to run all the time and is best served as a Windows
Service. The problem then was since I had written the Windows Service
code to be generalized, I was able to implement many other services very
quickly, even ones with timers that ran on a schedule.

However, the most challenging ones to write, happened to be the ones
that ran on a schedule, since the scheduling requirements kept changing
and I realized I was going down the path to implementing...well...the
Windows Task Scheduler.

In general, I think Jon’s right. If all you are doing is running a
scheduled task, use Windows Task Scheduler until you reach the point
that your system’s need are no longer met by the scheduler. This follows
the principle of doing only what is necessary and implementing the
simplest solution that works.

In a conversation, Jon mentioned that a lot of developers perceive
Windows Services to be a more “professional” solution than task
scheduling a console app. But one way to think of a service is an
application that responds to requests and system events, not necessarily
a scheduled task. So to satisfy both camps you could consider creating a
service that takes in requests, and a scheduled task to make the
requests. For example, a service might have a file system watcher active
and a scheduled task might write the file. I don’t suggest adding all
this complexity to something that can be very simple.

For me, I also like writing windows services because I have a system for
quickly creating installation packages very quickly. What I need to do
is spend some time creating an installer task for setting up a windows
task scheduler job. That way I can do the same for a scheduled console
app.

