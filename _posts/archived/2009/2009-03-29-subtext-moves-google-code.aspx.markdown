---
title: Subtext Is On The Move
tags: [subtext]
redirect_from: "/archive/2009/03/28/subtext-moves-google-code.aspx/"
---

Simo [beat me to the
punch](http://codeclimber.net.nz/archive/2009/03/29/subtext-goes-to-google-code.aspx "Subtext Switches")
in writing about this, After many long years being hosted on
SourceForge, the Subtext submarine is moving into a new project hosting
port.

We’ve finally moved off of SourceForge and onto [Google
Code](http://code.google.com/p/subtext/ "Subtext's Google Code")’s
project hosting. Our main site (primarily for end users) is still at
[http://subtextproject.com/](http://subtextproject.com/ "Subtext Project")
and I’ve hopefully updated every place it points to SourceForge to now
point to Google Code.

![Subtext-moves](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/SubtextSwitchesToGoogleCode_A931/Subtext-moves_3.jpg "Subtext-moves")
Image stolen from Simo’s blog. ;)

This was a very tough decision between CodePlex and Google Code.
CodePlex is a great platform and I really like what they’ve done with
being able to vote on issues etc… They seem to be innovating and adding
new features at a rapid clip. I host Subkismet, a smaller project, on
CodePlex and probably would choose it for a brand new project.

My one big complaint with CodePlex is that we really want native
Subversion access, not a subversion bridge to TFS. For example, I was
able to run the `svnsync` command to get the entire SVN history for
Subtext into Google Code. That’s not something I could do today with
CodePlex.

One other thing I really like with Google Code is that it’s **fast**.
When you go to our project page, and click on the tabs, notice how fast
the transition is. Click on an issue and see how fast you get there.
Make a change and save it and it just snaps back. I spend a lot of time
triaging and organizing issues etc… so this snappiness is really
important to me.

Another great feature I love is how well code review is integrated into
Google Code. For example, you can use the web interface to look at any
revision in our repository. Take a look at [r3406 for
example](http://code.google.com/p/subtext/source/detail?r=3406 "Subtext r3406").

Click on the diff link next to each file that was changed. For example,
the [diff for
AkismetClient.cs](http://code.google.com/p/subtext/source/diff?spec=svn3406&r=3406&format=side&path=/trunk/SubtextSolution/Subtext.Akismet/AkismetClient.cs "Diff for Akismet Client").
You get a nice side-by-side diff of the changes. You can double click on
any line of code to leave a comment. Scroll down to line 160 and take a
look at a comment. Don’t worry, I was the original author of that file
so I’m not offending anybody but myself with that comment.

So there’s a lot I’d love to see improved with Google Code, but I’m
pretty happy with the usability of the site overall. It’s a far
improvement over SourceForge where I started to viscerally hate managing
issues and doing any sort of administrative tasks over there.

We’re now using Google Groups for our [Subtext discussion
list](http://groups.google.com/group/subtext "Subtext Group") and we
have a [separate group for notification
emails](http://groups.google.com/group/subtext-notifications "Subtext Notifications")
such as commit emails. I’m going to leave the tracker and file releases
on at SourceForge for a while longer until we’ve moved everything over.
Unfortunately, there’s no automatic import from SourceForge for bug
reports. But if you’re interested in keeping tabs on the progress
Subtext is making, feel free to join the groups.

