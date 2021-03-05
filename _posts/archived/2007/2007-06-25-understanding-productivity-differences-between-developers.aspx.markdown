---
title: 10 Developers For The Price Of One
tags: [code,developers,methodologies]
redirect_from: "/archive/2007/06/24/understanding-productivity-differences-between-developers.aspx/"
---

Update: For an interesting counterpoint to the myth of the 10x engineer,
check out this [blog post by
Shanley](https://medium.com/about-work/6aedba30ecfe). My post is more
focused on what makes a good developer than the 10x myth.

In the *[The Mythical
Man-Month](http://www.amazon.com/gp/product/0201835959?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=9325&creativeASIN=0201835959 "The Mythical Man Month")*,
Fred Brooks highlights an eye opening disparity in productivity between
good and poor programmers (*emphasis mine*).

> Programming managers have long recognized wide productivity variations
> between good programmers and poor ones. But the actual measured
> magnitudes have astounded all of us. In one of their studies, Sackman,
> Erickson, and Grant were measuring performance of a group of
> **experienced** programmers. **Within just this group the ratios
> between the best and worst performances averaged about 10:1 on
> productivity measurements and an amazing 5:1 on program speed and
> space measurements!**

[![Tortoise and Hare:
http://users.cwnet.com/xephyr/rich/dzone/hoozoo/toby.html](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/GoodDevelopers10xProductivityOverAverage_A82B/toby2_thumb.gif)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/GoodDevelopers10xProductivityOverAverage_A82B/toby2.gif)

Robert Glass cites research that puts this disparity even higher in his
book *[Facts and Fallacies of Software
Engineering](http://www.amazon.com/gp/product/0321117425?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=9325&creativeASIN=0321117425 "Facts and Fallacies")*.

> **The best programmers are up to 28 times better than the worst
> programmers**, according to “individual differences” research. Given
> that their pay is never commensurate, they are the biggest bargains in
> the software field.

In other words, the [best developers are generally
underpaid](http://codecraft.info/index.php/archives/78/ "Why great coders get paid far too little")
and the worst developers overpaid.

But don’t leave your job just yet. This is not to say that there should
be a 1 to 1 correlation between productivity and pay. People should be
paid by the value they bring and productivity is only part of the value
proposition, albeit a big part of it. Even so, we’d expect to see some
amount of correlation in pay with such a drastic productivity
difference. But in general, we don’t. Why is that?

It’s because **most managers don’t believe this productivity disparity
despite repeated verification by multiple studies**. Why should they let
facts get in the way of their beliefs? That would only mean the
factonistas have won.

Kidding aside, why is this productivity difference so hard to believe?
Allow me to put words in the mouth of a straw-man manager.

*Well how in the world can one developer write code 28 times faster than
another developer?*

This sort of thinking represents a common fallacy when it comes to
measuring developer productivity. Productivity [is not about the lines
of
code](http://www.developer.com/java/other/article.php/988641 "It’s Not About Lines Of Code").
A huge steaming pile of code that doesn’t get the job done is not
productive. There are many aspects to developer productivity, but they
all fall under one main principle (*borrowing a term from the finance
industry*), TCO.

## TCO - Total Cost Of Ownership.

In general, I’ve tried to always [hire the best developers I can
find](https://haacked.com/archive/2007/01/27/On_Hiring_Bloggers_and_Open_Source_Developers.aspx "Hiring Bloggers and open Source Developers").
But I’ve made mistakes before. Yes, even me.

One situation that comes to mind was with a developer I had hired (under
a lot of pressure to staff up I might add) at a former company. I handed
off a project to this erstwhile coworker to take over. A few days go by
and I don’t hear anything from the guy, so I assume things are humming
along nicely.

Fast forward another few days and I swing by to see how it’s going and
the developer tells me he doesn’t understand a few requirements and has
been spinning his wheels trying to figure it out this whole time.

## Good Developers take Ownership so You Don’t Have To

This is one of the first ways that good developers are more productive
than average developers. They take ownership of a project. Rather than
spend a week spinning wheels because they don’t understand a
requirement, a good developer will go and grab the decision maker and
squeeze out some clarity.

Likewise, a good developer doesn’t require you to prod them every few
moments to make sure they are progressing. If they get overly stuck on a
problem, they’ll come to you or their coworkers and resolve the problem.

A developer who can write code fast, but doesn’t take ownership of their
projects is not very productive because they end up wasting
**your**time.

## Good Developers Write Code With Less Bugs

I once worked with a developer who was praised by my boss for being
extremely fast at writing code. He sure was fast! He was also fast at
introducing bugs into code. His code was sloppy and hard to understand.

The key measure that wasn’t figured into his productivity measurement
was the amount of productivity lost by the QA team attempting to
reproduce bugs introduced by his code, along with the time spent fixing
those bugs by this developer or other developers.

Everyone focused on his time to "completion", but not on the total cost
of ownership of that code. Code is not complete when a developer says it
is complete. That is *not* the time to stop the stopwatch. It’s when QA
has had its say that you can put the stopwatch away for the moment.

As I like to say, productivity is not about speed. It’s about velocity.
You can be fast, but if you’re going in the wrong direction, you’re not
helping anyone.

## Good Developers Write Maintainable Code

Hand in hand with writing less bugs is
writing understandable maintainable code. As soon as a line of code is
laid on the screen, you’re in maintenance mode on that piece of code.

Code that is brittle and difficult to change wastes hours and hours of
developer cycles when trying to amend a system with updates and new
features. By writing maintainable code, a good developer can make these
changes more quickly and also improves the productivity of his or her
team members who later have to work on such code.

## Good Developers Do More With Less Code

Another hallmark of a good developer is that they know when not to write
code. As a friend always tells me

> Why build what you can buy? Why buy what you can borrow? Why borrow
> what you can steal?

With a few exceptions, the NIH (Not Invented Here) syndrome is a
pathological productivity killer. I’ve seen developers start out to
write their own form validation framework until I point out that there
is already one built in to ASP.NET that does the job (It’s not perfect,
but it’s better than the one I saw being written).

All of that time spent reinventing the wheel is wasted because someone
else has already written that code for you. And in many cases, did a
better job as it was their only focus. In such a situation, finding an
existing library that gets the job done can provide a huge productivity
boost.

The caveat in this case is to be careful to avoid non-extensible and
rigid 3rd party libraries, especially for very specialized requirements.
You might a lot of time trying to fit a round peg in a square box.

Even when you must invent here, good developers tend to write less (but
still readable) code that does more. For example, rather than build a
state machine to parse out text from a big string, a good developer
might use a regular expression (*ok, some will say that a regex is not
readable. Still more readable than hundreds of lines of text parsing
code)*.

**Back to TCO**

Each of these characteristics I’ve listed keeps the total cost of
ownership of a good developer low. Please don’t let the term *ownership*
distract you. What I mean here is the cost to the company for having
such a developer on the payroll.

By writing less code that does more, and by writing maintainable code
that has fewer bugs, a good developer takes pressure off of the QA
staff, coworkers, and management, increasing productivity for everyone
around. This is why numbers such as 28 times productivity are possible
and might even seem low when you look at the big picture.

Hopefully seeing this perspective will convince managers that good
developers really are as productive as the studies show. Negotiating a
28x pay increase on the other hand, is an exercise left to the reader.

