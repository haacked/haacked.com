---
title: The Design Is Never Right The First Time
tags: [aspnet,aspnetmvc,code,methodologies]
redirect_from: "/archive/2008/05/30/the-design-is-never-right-the-first-time.aspx/"
---

![tacoma narrows](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TheDesignIsNeverRightTheFirstTime_CC01/tacoma_narrows_3.jpg)
The design is never right the first time. Nor is it usually right the
second time. This is one of those key lessons you learn over time
building real software, and is the source of one of the chief complaints
leveled at the Waterfall methodology for building software, which puts
heavy emphasis on getting the design right up front.

We have to define “right” in this case. If by “right” you mean
*perfect*, then I don’t think the design is ever right, as I’ve argued
in the past that [there is no perfect
design](https://haacked.com/archive/2005/05/31/ThereIsNoPerfectDesign.aspx "Blog post on perfect design").

Recently, this lesson was driven home as my feature team (ASP.NET MVC)
were hashing out some tricky design issues. Many times, we find
ourselves in a tight spot in which we have two different audiences
demanding a different approach to a design problem. The tough part is
that the solutions we come up with inevitable satisfy one camp at the
expense of the other.

Let me be clear, it’s not our goal and intention to try and satisfy
everyone as that is a sure means to satisfying nobody. As Bill Cosby
once said...

> I don’t know the key to success, but the key to failure is to trying
> to please everybody.

At the same time, I also think it’s a copout to simply give up
immediately when a thorny design problem comes up with two disagreeing
camps. It’s worth a good faith effort to try and gather ideas you
haven’t considered before and see if you can’t just solve the problem in
a manner that both camps are happy with. Or at least in a manner that
one camp is happy, and the other is merely less happy.

An illustration of this point happened recently at work. After many
iterations, I sent out our design meeting notes internally with our
solution to a particular issue.
[ScottGu](http://weblogs.asp.net/scottgu/ "Scott Guthrie") noticed the
write-up and had some questions about the design (questions like WTF?)
and called me and
[Eilon](http://weblogs.asp.net/leftslipper/ "Eilon Lipton") (the lead
dev) into his office for a “short” meeting to discuss these recent
design changes.

I made sure to call my wife before the meeting to let her know I would
be late for dinner. Once we get a talking about MVC, I know it won’t be
short. We spent two hours hashing out the design, going over options,
shooting them down, trying out other ideas, and so on.

At some point, Scott proposed something my team had rejected earlier due
to a major flaw. However this time, as he was proposing the idea, it
suddenly occurred to me that there were two different ways to implement
this idea. The one we thought of wouldn’t work, but a slight adjustment
to the idea, the very one Scott was proposing, would make it workable.

Eilon and I left the meeting happy with the improved design, but now
concerned with the fact that we had already implemented another
solution, had a bunch of unit tests and automated web functional tests.
Not to mention having to deal with explaining it to the team and getting
buy in so close to our release deadline.

So we did the cowboy thing (which I generally don’t recommend) and
decided instead of telling the rest of the team about it, we’d follow
the old maxim: “[Show, don’t
tell](http://en.wikipedia.org/wiki/Show,_don’t_tell "Show, don't tel")”.
So that night, we went and coded the whole implementation, updated all
the unit and web tests, and had something to demo in the morning rather
than talk about. Since we had updated all the tests, QA was much more
amenable to signing off on making this change so close to the deadline.

In retrospect, the part that causes me pain in the *missed
opportunities* sense was just how tantalizingly close we were to having
implemented this very idea from the beginning. All that time spent
prototyping, app building, etc... we could have saved by getting it
right the first time.

But that’s the way design goes. Sometimes, you are so close to a great
solution that if the wind blows a certain way, you’ll hit upon it, but
if it blows another way, you dismiss your line of thought and move onto
other plans. Try as you might, it is very difficult to get the design
just right the very first time *all of the time*.

Luckily for us, our team had the agility to respond to a better design.
Our suite of unit and integration tests gave us confidence that the
changes we were making were not breaking the framework in other areas.
To me, this is a real testament to the benefits of applying lean and
agile principles in software development, as well as the benefits of
having good unit test coverage.

