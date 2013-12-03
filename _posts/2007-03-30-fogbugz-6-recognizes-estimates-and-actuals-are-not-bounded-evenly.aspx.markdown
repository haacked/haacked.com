---
layout: post
title: "FogBugz 6 Recognizes Estimates And Actuals Are Not Bounded Evenly On Both Sides"
date: 2007-03-30 -0800
comments: true
disqus_identifier: 18266
categories: []
---
[![Code
Complete](http://ec1.images-amazon.com/images/P/0735605351.01._AA_SCMZZZZZZZ_.jpg)](http://www.amazon.com/gp/product/0735605351?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=9325&creativeASIN=0735605351)A
while ago I read Steve McConnel’s latest book, [Software Estimation:
Demystifying the Black
Art](http://www.amazon.com/gp/product/0735605351?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=9325&creativeASIN=0735605351 "Software Estimation Book"),
which is a fantastic treatise on the “Black Art” of software estimation.

One of the key discoveries the book highlights is just how bad people
are at estimation, especially [single point
estimation](http://www.codinghorror.com/blog/archives/000611.html "Single Point Estimation").

One of several techniques given in the book focuses on providing three
estimation points for every line item.

1.  **Best Case:** If everything goes well, nobody gets sick, the sun
    shines on your face, how quickly could you get this feature
    complete?
2.  **Worst Case:** If your dog dies, your significant other leaves you,
    and your brain turns to mush, what is the absolute longest time it
    would take to get this done? In other words, there is no way on
    Earth it would take longer than this time, unless you were shot.
3.  **Nominal Case:** This is your best guess, based on your years of
    experience with building this type of widget. How long do you really
    think it will take?

The hope is that when development is complete, you’ll find that the
actual time spent is between your best case and worst case. McConnell
[provides a
quiz](http://www.codinghorror.com/blog/archives/000625.html "An Estimation Quiz")
you can try out to discover that this is [harder than it
sounds](http://www.codinghorror.com/blog/archives/000626.html "How Good An Estimator are you").

Over time, as you reconcile your actual times into your past estimates,
you’ll be able to figure out what I call **your estimation**[**batting
average**](http://en.wikipedia.org/wiki/Batting_average "Batting Average"),
a number that represents how accurate your estimates tend to be.

Once you have these three points for a given estimate, you can apply
some formulas and your estimation batting average to create a
probability distribution of when you might complete the project. Here is
a simple example of what that might look like (though in real life there
may be more point values).

-   20% 50 developer days
-   50% 70 developer days
-   80% 90 developer days

So the numbers above show that there’s only a 20% chance the project
will be complete within 50 developer days and an 80% chance of
completion if the development team is given 90 developer days.

**This technique showcases the uncertainty involved in creating
estimates and focuses on the probability that estimates really
represent.**

After reading this book, I fired up Excel and built a nice spreadsheet
with the formulas in the book and columns for these three estimation
points. Now I can simply enter my line items, plug in my best, worst,
and nominal cases, and out pops a probability distribution of when the
project will be complete.

However, as I mentioned before, the crux of this technique relies on
that estimation batting average. But when you’re just starting out, you
have no idea what that average is, so you have to pull it out of the air
(I recommend pulling conservatively).

The reason I bring this all up is that I watched an [interesting
interview](http://www.podtech.net/scobleshow/technology/1414/joel-spolsky-the-famous-blogger-on-software-productivity "Joel Spolsky On Software Productivity")
today on the
[ScobleShow](http://www.podtech.net/scobleshow/ "The Scoble Show").
[Robert Scoble](http://scobleizer.com/ "Scoblelizer") interviewed
FogCreek founder and well known technology blogger, [Joel
Spolsky](http://joelonsoftware.com/ "Joel Spolsky").

Joel let it be known that they are building a new scheduling feature for
FogBugz 6 that reflects the reality of software estimation better than
typical scheduling software.

For example, one key observation he makes is that estimates tend to be
much shorter than the actual time than they are longer.

For example, it’s quite common to estimate that a feature will take two
days, only to have it take four days, or eight days. But it’s rare that
the feature actually ends up taking one day. Obviously it’s impossible
for that feature to take 0 days or -4 days.

This makes obvious sense when you think about it.

**The amount by which you can finish a feature before an estimated time
is constrained, but the amount of time that you can overshoot an
estimate is boundless.**

Yet many software scheduling software completely ignore this fact,
hoping that an underestimation on one item will be offset by an
overestimation of another. They assume these over and under estimates
are balanced, which they are clearly not.

This new feature will attempt to take that into account as well as your
track record for estimates (your batting average if you will), and
provide a probablity of completion for various dates.

Sounds like a brilliant idea! If done well, that would be quite hot and
allow me to chuck my hackish Excel spreadsheet.

