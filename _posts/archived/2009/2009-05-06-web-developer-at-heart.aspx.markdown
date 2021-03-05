---
title: I am a Web Developer At Heart
tags: [web,developers]
redirect_from: "/archive/2009/05/05/web-developer-at-heart.aspx/"
---

A while back a young developer emailed me asking for advice on what it
takes to become a successful developer. I started to respond,

> I don’t know. Why don’t you go ask a successful developer?

[![web - credits:
http://www.sxc.hu/photo/885310](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ImaWebDeveloperAtHeart_1287B/web_thumb.jpg "web - credits: http://www.sxc.hu/photo/885310")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ImaWebDeveloperAtHeart_1287B/web_2.jpg)
But then I thought, that’s kind of snarky, isn’t it? And who am I
kidding with all that false modesty? After all, the concept of a “clever
hack” was named after me, but the person who came up with it didn’t have
1/10 of my awesomeness which is exceedingly apparent given the
off-by-one error that dropped one of the “a”s from the phrase when it
was coined. This, of course, was all before I invented TDD, the
Internet, and breathing air.

(*note to the humor impaired, I didn’t really invent TDD*)

But as I’m apt to do, I digress…

I started thinking about the question a bit and said to myself, “If I
*were* a successful developer, what would have I done to become that?”
As I started brainstorming ideas, one thing that really stood out was
joining an open source project.

If one thing in my career has paid dividends, it was getting involved
with open source projects. It exposed me to such a diverse set of
problems and technologies that I wouldn’t normally get a chance to work
on at work.

Now before I go further, this post is not the post where I answer the
young developer’s question. No, that’s a post for another time. I’ll
probably give it some trite and pompous title like “Advice for a young
developer.” I mean, c’mon! How pathetic and self absorbed is that title?
“Get over yourself!” I’ll say to the mirror. But the guy in the mirror
will probably do it anyways, but in reverse.

No, this is not that post. Rather, this post is a digression from that
post, because if I’m good at one thing, it’s digressing.

As I thought about the open source thing, I got to thinking about the
first open source project I ever worked on – [RSS
Bandit](http://rssbandit.org/ "RSS Bandit") (w00t w00t!). RSS Bandit is
a kick butt RSS aggregator developed by [Dare
Obasanjo](http://www.25hoursaday.com/weblog/ "Dare Obasanjo") and
[Torsten
Rendelmann](http://www.rendelmann.info/blog/ "Torsten Rendelmann"). I
had just started to get into blogging at the time and was really
impressed by Dare’s outspoken and yet very thoughtful blog as well as by
his baby at the time, RSS Bandit (he has a real baby now, congrats
man!).

I hadn’t done much Windows client development back then. I was mostly
building web applications in classic ASP and then early versions of
ASP.NET. I figured that it would be exciting to cut my teeth on RSS
Bandit and learn Winforms development in the process. The idea of a
stateful programming model had me positively giddy with excitement. This
was going to be so cool.

Many new developers approaching an open source project have grand
visions of implementing shiny amazing new features that will have the
crowds roaring, the President naming a holiday after you, and all your
enemies realizing the errors of their ways and naming their children
after you.

But a good contributor swallows his or her pride and starts off slowly
with something smaller in scope, and more grunt work like in nature.
Most OSS projects have a real need for documentation, partly because all
the glamour is in implementing features so nobody wants to write the
documentation.

That’s where I started. I wrote an article for the docs, [getting
started with RSS
Bandit](https://haacked.com/articles/getting-started-with-rss-bandit.aspx "Getting Started With RSS Bandit").
Dare took a notice and asked if I would contribute to the documentation,
which I gladly agreed to do. He gave me commit access (I believe I was
the third after Dare and Torsten to get commit rights) and started
working very hard on the documentation. In fact, much of what I wrote is
still there as you can see in [my narcissistic application screenshots I
used](http://docs.rssbandit.org/v1.8/html/getting_started/posting_comments.htm "Posting Comments").
;)

Over time, I gained more and more trust and was allowed to work on some
bug fixes and features. My first main feature was implementing
configurable keyboard shortcuts, which was really neat to implement.

(*A bit of trivia. I worked with these guys for years on RSS Bandit, but
never met Dare in person until this past Mix conference in Las Vegas.
Seriously! I’ve yet to meet Torsten who lives in Germany.)*

I really loved working on RSS Bandit and it became quite a hobby that
took up what little was left of my free time. I guess you could say it
kept me out of gangs in the hard streets of Los Angeles, not that I
tried to join nor would they accept me. Over time though, I learned
something. Despite all that initial giddiness over finally getting to
program in a stateful environment…

I realized I didn’t like it.

In fact, I found it quite foreign and challenging. I kept running into
weird problems where controls still had the state they had before after
a user clicked a button. I would think to myself, “why do I have to
clear that state myself? Why doesn’t it just go away when the user takes
an action?” I realized my problem was that I was thinking like a web
programmer, not a client programmer who took these things for granted.

As challenging as a client programmer finds the web, where you have to
recreate the state on each request because the web is stateless, a
developer who primarily programs the web sometimes finds client
development challenging because the state is like that ugly sidekick
next to the hot one at a bar -- *it…just…won’t…go…away*.

I realized then, that I’m just a web developer at heart and I’d rather
[make web, not
war](http://www.youtube.com/watch?v=cCApcSq1ke0 "Make Web, not war"). It
was around that time that [I started the Subtext
project](https://haacked.com/archive/2005/05/04/announcing-subtext.aspx "Announcing Subtext")
where I felt more in my element working on a web application.
Eventually, I stopped using RSS Bandit preferring a web based solution
in Google Reader, ironically, because the state of my feeds is always
there, in the cloud, without me needing to synchronize or install an app
when I’m at a new computer.

So while I actually like (or maybe am just accustomed to) the stateless
programming model of the web, I’m also attracted to the statefulness of
web applications as a whole in that the state of my data is not tied to
any one machine but it’s stored centrally where I can easily get to it
from anywhere (which yes, has its own concerns and problems such as when
the net is down).

At the same time, I do check in now and then to see how RSS Bandit is
progressing. There are very cool features that it has that I miss out on
with Google Reader such as the ability to comment directly from the
aggregator via the Comment API and the ability to subscribe to
authenticated feeds. And I think Dare’s taking RSS Bandit into
[compelling new
directions](http://www.25hoursaday.com/weblog/2009/05/05/RSSReadersModeledAfterEmailClientsAreFundamentallyBroken.aspx "RSS Aggregators built like email clients are flawed").

All this is to say that if you want to become a better developer, join
an open source project ([such as this
one](http://subtextproject.com/ "Subtext Project") :) because it might
just show you exactly what type of developer you are at heart. As I
learned, I’m a web developer at heart.

