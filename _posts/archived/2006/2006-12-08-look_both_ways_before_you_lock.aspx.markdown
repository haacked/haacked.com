---
title: Look Both Ways Before You Lock
tags: [concurrency]
redirect_from: "/archive/2006/12/07/look_both_ways_before_you_lock.aspx/"
---

[![Green Light from
http://www.sxc.hu/photo/669003](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/LookBothWaysBeforeYouLock_15150/669003_green_traffic_light_thumb1.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/LookBothWaysBeforeYouLock_15150/669003_green_traffic_light3.jpg "Traffic Light")Google
Code Search is truly the search engine for the uber geek, and
potentially a great source of sublime code and sublime comments.  [K.
Scott Allen](http://odetocode.com/Blogs/scott/ "OdeToCode"), aka Mr.
OdeToCode, posted a few [choice
samples](http://odetocode.com/Blogs/scott/archive/2006/12/08/9386.aspx "Words to Live By")
of prose he found while searching through code (*Scott, exactly what
were you searching for?)*.

One comment he quotes strikes me as a particularly good point to
remember about using locks in multithreaded programming.

> **Locks are analogous to green traffic lights**: If you have a green
> light, that does not prevent the idiot coming the other way from
> plowing into you sideways; it merely guarantees to you that the idiot
> does not also have a green light at the same time. (from
> [File.pm)](http://www.google.com/codesearch?hl=en&q=+idiot+show:LRTHNX6IRG0:kkKVkiFeBEE:GaknSvlB5XA&sa=N&cd=14&ct=rc&cs_p=http://search.cpan.org/CPAN/authors/id/J/JH/JHI/perl-5.8.1.tar.gz&cs_f=perl-5.8.1/lib/Tie/File.pm#a0).

The point the comment makes is that a lock does not prevent another
thread from going ahead and accessing and changing a member.  Locks only
work when every thread is "obeying the rules".

Unfortunately, unlike an intersection, there is no light to tell you
whether or not the coast is clear. When you are about to write code to
access a static member, you don't necessarily know whether or not that
member might require having a lock on it. To really know, you'd have to
scan every access to that member.

This is why concurrency experts such as Joe Duffy recommend [following a
locking
model](http://www.bluebytesoftware.com/blog/PermaLink,guid,f8404ab3-e3e6-4933-a5bc-b69348deedba.aspx "Concurrency and the impact on reusable libraries"). 
A locking model can be as simple or as complex as needed by your
situation.

In any case, have fun with Google Code Search. You might find yourself
reviewing the code of a [Star Trek text-based sex
adventure](http://www.google.com/codesearch?hl=en&q=show:D8fEcE7V1N0:x6wWpWtR_xM:jlz7MZyB40k&sa=N&ct=rd&cs_p=http://www.geocities.com/abomire/files/chick.zip&cs_f=/beverly.t "Beverly.t").

