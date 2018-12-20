---
title: Release Early, Release Often
date: 2011-04-20 -0800
tags:
- code
- oss
redirect_from: "/archive/2011/04/19/release-early-and-often.aspx/"
---

Eric S. Raymond in the famous essay, [The Cathedral and the
Bazaar](http://en.wikipedia.org/wiki/The_Cathedral_and_the_Bazaar "The Cathedral and the Bazaar"),
states,

> Release early. Release often. And listen to your customers.

This advice came from Eric’s experience of managing an open source
project as well as his observations of how the Linux kernel was
developed.

*But why? Why release often? Do I really have to listen to my customers?
They whine all the time!* To question this advice is sacrilege to those
who have this philosophy so deeply ingrained. It’s obvious!

Or is it?

When I was asked this in earnest, it took me a moment to answer. It’s
one of those aphorisms you know is true, but perhaps you’ve never had to
explain it before. It’s hard to answer not because there isn’t a good
answer, but because it’s difficult to know where to begin.

It’s healthy to challenge conventional wisdom from time to time to help
avoid the [cargo
cult](http://en.wikipedia.org/wiki/Cargo_cult "Cargo Cult") mentality
and remind oneself all the *reasons* good advice is, well, good advice.

One great approach is to take a step back and imagine explaining this to
someone who isn’t ingrained in software development, such as a business
or marketing person. **Why is releasing early and often a good
thing?**It helps to clarify that releasing early doesn’t mean waking up
at 3:00 AM to release, though that may happen from time to time.

In this blog post, I’ll look into this question as well as a couple of
other related questions that came to mind as I thought about this topic:

-   If releasing often is a good thing, why not release even more often?
-   What factors affect how often is often enough?
-   What are common objections to releasing often?
-   Why does answering a question always leave you with more questions?

I’ll try answering these questions as best as I can based on my own
experiences and research.

Why is it a good thing?
-----------------------

What are the benefits of releasing early and often? As I thought through
this question and looked at the various responses that I received from
[asking the
Twitterista](http://twitter.com/#!/haacked/status/53127858926788609 "Why release often?")
for their opinions, three key themes kept recurring. These themes became
the summary of my
[TL;DR](http://en.wikipedia.org/wiki/Wikipedia:Too_long;_didn't_read "Too Long, Didn't Read on Wikipedia")
version of the answer:

1.  **It’s results in a better product.**
2.  **It results in happier customers.**
3.  **It fosters happier developers.**

So how does it accomplish these three things? Let’s take a look.

### It provides a rapid feedback loop

[Steve Smith](http://stevesmithblog.com/ "Steve Smith's Blog") had [this
great
observation](http://twitter.com/#!/ardalis/status/53133055837212673 "Tweet")
(emphasis mine):

> The shorter the feedback loop, the faster value is added. **Try
> driving while only looking at the road every 10 secs, vs.
> constantly.**

Driving like that is a sure formula for receiving a [Darwin
Award](http://www.darwinawards.com/ "Darwin Awards").

Every release is an opportunity to stop theorizing about what customers
want and actually put your hypotheses to the test by getting your
product in their hands. The results are often surprising. After all, who
expected Twitter to be so big?

[Matt Mullenweg](http://ma.tt/ "Matt Mullenweg's Blog"), the creator of
WordPress put it best in his blog post, [1.0 is the loneliest
number](http://ma.tt/2010/11/one-point-oh/ "One Point Oh"):

> **Usage is like oxygen for ideas.** You can never fully anticipate how
> an audience is going to react to something you’ve created until it’s
> out there. That means every moment you’re working on something without
> it being in the public it’s actually dying, deprived of the oxygen of
> the real world.

This has played out time and time again in my experience. This happened
recently with NuGet when we released a bug that caused sluggishness in
certain TFS scenarios, something very difficult to discover without
putting the product in real customers hands to use in real scenarios.
Thankfully we didn’t have to wait a year to release a proper fix.

As [Miguel De Icaza](http://tirania.org/blog/ "Miguel's Blog") [points
out](http://twitter.com/#!/migueldeicaza/status/53128416152657921 "Miguel's tweet"),

> Early years of the project, you get to course correct faster, keep up
> with demand/needs

It’s not just customer demands that require course corrections. At
times, changing market conditions and other external factors may require
you to quickly adjust your planned feature sets and come out with a
release in response to these changes. Having short iterations allow more
agility to respond to such events keeping your product relevant.

### It gets features and bug fixes in customers hands faster

This point is closely related to the last point, but worth calling out.
In the Chromium blog, the open source project that makes up the core of
the Google Chrome browser, they point out the following in their blog
post, also titled [Release Early, Release
Often](http://blog.chromium.org/2010/07/release-early-release-often.html "Release Early, Release Often")
(emphasis mine):

> The first goal is fairly straightforward, given our pace of
> development. We have new features coming out all the time and **do not
> want users to have to wait months before they can use them**. While
> pace is important to us, we are all committed to maintaining high
> quality releases — if a feature is not ready, it will not ship in a
> stable release.

Well why not make users wait a few months? As [Nate
Kohari](http://kohari.org/ "Nate Kohari") [points
out](http://twitter.com/#!/nkohari/status/53130649875382272 "tweet"),

> Nothing is real until it's providing value (or not) to your users.
> Having completed work that isn't released is wasteful.

The longer a feature is implemented, but not being used in real
scenarios, the more the context for the feature is lost. By the time
it’s in customers hands, the original reason for the feature may be lost
in the smoky mists of memory. And as feedback on the feature comes in,
it takes time for the team to re-acquaint itself with the code and the
reasons the code was written the way it was. All of that ramp up time is
wasteful.

Likewise, the faster the cycle, the shorter the time the team has to
live with a known bug out there in the product. Sometimes products ship
with bugs that aren’t serious enough for an emergency patch, but
annoying enough that customers are unhappy with having to live with the
bug till the next release. This also makes developers unhappy as well as
they are the ones who hear about it from the customers. A short release
cycle means nobody has to live with these sort of bugs for long.

### It reduces pressure on the development team to “make” a release

This point is also taken from the Chromium blog post as well. You can
probably tell that post really resonated with me.

As a project gets closer and closer to the end of the release cycle, the
team starts to make hard decisions regarding which bugs or features will
get implemented or get punted. The pressure builds as the team realizes,
if they don’t get the fix in this release, customers will have to wait a
long time to get it in the next. As the Chromium blog post states:

> Under the old model, when we faced a deadline with an incomplete
> feature, we had three options, all undesirable: (1) Engineers had to
> rush or work overtime to complete the feature by the deadline, (2) We
> delayed the release to complete that feature (which affected other
> un-related features), or (3) The feature was disabled and had to wait
> approximately 3 months for the next release. With the new schedule, if
> a given feature is not complete, it will simply ride on the the next
> release train when it’s ready. Since those trains come quickly and
> regularly (every six weeks), there is less stress.

The importance of this point can’t be overstated. Releasing often is not
only good for the customers, it’s good for the development team.

### It makes the developers excited!

This point was one of the original observations that Eric Raymond made
in his essay,

> So, if rapid releases and leveraging the Internet medium to the hilt
> were not accidents but integral parts of Linus's engineering-genius
> insight into the minimum-effort path, what was he maximizing? What was
> he cranking out of the machinery?
>
> Put that way, the question answers itself. Linus was keeping his
> hacker/users constantly stimulated and rewarded -- stimulated by the
> prospect of having an ego-satisfying piece of the action, rewarded by
> the sight of constant (even*daily*) improvement in their work.

Contrary to popular depictions, **developers love people!** And we
especially love happy people, which makes us very excited to see
features and bug fixes get into the customers hands because it makes
them happy.

It’s demoralizing to implement a great feature or key bug fix and then
watch it sit and stagnate with nobody using it.

This is especially important when you’re trying to harness the energy of
a community of open source contributors within your project. It’s
important to keep their attention and interest in the project high, or
you will lose them. And nothing makes contributors more excited than
seeing their hard work be released into the public for the world to use
and recognize.

Yes, appeal to your contributors egos! Let them share in the glory now,
and not months from now! Let them receive the recognition they deserve
sooner rather than later!

### It makes the schedule more predictable and easier to scope

Quick! Tell me how many piano tuners there are in Chicago? At first
glance, this is a very difficult task. But if you break it down into
smaller pieces, you can come up with a pretty good estimate for each
small piece which leads to a decent overall estimate.

This type of problem is known as a [Fermi
problem](http://en.wikipedia.org/wiki/Fermi_problem "Fermi Problem")
named after the physicist [Enrico
Fermi](http://en.wikipedia.org/wiki/Enrico_Fermi "Enrico Fermi") who was
renown for his estimation abilities. The story goes that he estimated
the strength of an atomic bomb by measuring the distance pieces of paper
travelled that he ripped up and dropped from his hands.

Breaking down a long product schedule into short iterations is similar
to attacking a Fermi Problem. It’s much easier to scope and estimate a
short iteration than it is a large one.

Again, going back to the Chromium blog post,

> The second goal is about implementing good project management
> practice. Predictable fixed duration development periods allow us to
> determine how much work we can do in a fixed amount of time, and makes
> schedule communication simple. We basically wanted to operate more
> like trains leaving Grand Central Station (regularly scheduled and
> always on time), and less like taxis leaving the Bronx (ad hoc and
> unpredictable).

### Keeps your users excited and happy

Ultimately, all the previous points I made lead to happy customers. When
a customer reports a bug, and a fix comes out soon afterwards, the
customer is happy. When a customer sees new features continually
released that make their lives better, they are happy. When your product
does what they want because of the tight feedback cycle, the customers
are ultimately much happier with the product.

And this doesn’t just benefit your current customers. Potential new
customers will be attracted to the buzz that frequent releases generate.
As [Atley Hunter](http://www.atleyhunter.com/ "Atley Hunter's Blog")
[points
out](http://twitter.com/#!/atleyhunter/status/53128149512368128 "Atley's tweet"),

> Offering software consistently and frequently helps to foster both
> market buzz and continued interested from your installbase.

Continual releases are the sign of an active and vibrant product and
product community. This is great for marketing your product to new
audiences.

So Why Not Release All The Time?
--------------------------------

If releasing often is such a good thing, why not release all the time?
Isn’t releasing more often better than less often?

Releasing every second of the day time might not be possible since it
does take time to implement features, but it’s not unheard of to release
features as soon as they are done. This is the idea behind a technique
called [continuous
deployment](http://radar.oreilly.com/2009/03/continuous-deployment-5-eas.html "Continuous deployment"),
which is particularly well suited to web sites.

When I worked at [Koders](http://koders.com/ "Koders") (now part of
BlackDuck software), we pushed a release every two weeks. We wanted to
move towards a weekly release, but it took a couple of days to build our
source code index. Our plan was to make the index build incrementally so
we could deploy features more often and hopefully reach the ultimate
goal of releasing as soon as a feature was completely done.

I think this is a great idea, but not always attainable without
significant changes in how a product is developed. For example, with
NuGet, we have a [continuous integration
server](http://ci.nuget.org:8080/ "NuGet CI Server"), that produces a
build with every check-in. In a manner of speaking, we do have
continuous deployment because anyone can go and try out these builds.

But I wouldn’t apply the “continuous deployment” label to what we do
because we these CI builds are not release quality. To get to that point
would require changing our development process so that every check-in to
our main branch represented a completely end-to-end tested feature that
we’re ready to ship.

At this point, I’m not even sure that continuous deployment is right for
every product, though I’m still learning and open to new and better
ideas. To understand why I feel this way, let me transition to my next
question.

What factors affect how often is often enough?
----------------------------------------------

I think there are several key factors that determine how often a product
should be released.

### Adoption Cost to the Customers

Some products have a higher adoption cost when a new release is
produced. For example, websites have a very low adoption cost. When
StackOverflow.com produces a release daily or even more than once a day,
the cost to me is very little as long as the changes aren’t too drastic.

I simply visit the site and if I notice the new feature, I start taking
advantage of it.

A product like Google Chrome has a slightly higher adoption cost, but
not much. I’m unlikely to have critical infrastructure completely
dependent on their browser. The browser updates itself and I simply take
advantage of the new features.

But a product like a web framework has a much higher adoption cost.
There’s a steeper learning curve for a new release of a programming
framework than there is for a browser update. Also, authors will want
time to be able to write their training materials, courses, books before
they become obsolete by the next version.

And folks running sites on these framework versions want time to upgrade
to the next version without having that version become obsolete
immediately. Major releases of frameworks allow the entire ecosystem to
congeal around these major release points. Imagine if ASP.NET MVC had 24
official releases in two years. How much harder would it be to hire
developers to support your ASP.NET MVC v18 application when they want to
be on the latest and greatest because we all know v24 is the cats
pajamas.

I believe this is why you see Ruby on Rails releasing every two years
and ASP.NET MVC release yearly. Note that both of these products still
release previews early and often, but the final “blessed” builds come
out relatively infrequently.

### Maturity of the product

The other factor that might affect release cadence is the maturity of
the product. When a product is playing catch-up, it has to release more
often or risk falling further and further behind.

As a product matures, all the low-hanging fruit gets taken and sometimes
a longer release cycle is necessary as they tackle deeper features which
require heavier investments of time. Keep in mind, this is me theorizing
here. I don’t have hard numbers to base this on, but it’s based on my
observations.

### Customer Costs

Sometimes, the customers tolerance for change affects release cadence.
For example, I don’t think customers would tolerate a new iPhone
hardware device every month because they’ll constantly feel left behind.
A year is enough time for many (but not all) consumers to feel ready to
upgrade to the next version.

### Deployment Costs

One last consideration might be the costs to deploy the product. For
hardware, this can be a big factor when the design of the product
changes drastically from one version to the next.

Suddenly new supply chains may need to be set in place. Factories may
need to be retrofitted to support the new or changing components. The
products have to physically be shipped to the stores.

All these things can affect how often a new product can be shipped.

Common objections to Releasing Often
------------------------------------

I think there are three main objections to this model I’ve heard or can
think of. The first is that it forces end users to update their software
more often. I’ve addressed this point already by looking at customer
adoption as a gating factor in how often the product should be released.
Many products can be updated quietly without requiring users to take any
action. If the new features are designed well, customers will naturally
discover them and learn them without too much fuss. In this model, avoid
having these regular releases move everyone’s cheese by re-arranging the
UI and that sort of thing.

Another concern raised is that this leads to more frequent lower quality
releases rather than less frequent releases with higher polish and
quality. After all, releases always contain overhead and by having more
releases, you’re multiplying this overhead over multiple releases.

This is definitely a concern, but one that’s easily mitigated. Before we
address that, but as my co-worker [Drew
Miller](http://twitter.com/#!/anglicangeek "Drew Miller on Twitter")
points out, **long release cycles mask waste**and that waste is far
greater than the cost of more frequest release overhead.

-   The more often you release, the better you are at releasing; release
    overhead decreases over time. With long release cycles, the pain of
    release inefficiency is easy to ignore and justify and the urgency
    and incentive to trim that waste is very low.
-   The sense of urgency for frequent releases drives velocity, more
    than off-setting the cost of release overhead.
-   The rapid feedback with frequent releases reduces the waste we
    always have for course correction within a long release cycle. A
    great example of this is the introduction of `ActionResult` to
    ASP.NET MVC 1.0 between Preview 2 and 3. That was costly, but
    would’ve been more costly if we had made that change much later.
-   The slow start of a long release cycle alone is usually more
    wasteful than the total cost of release overhead over the same
    period.
-   Long release cycles may have milestone overhead that can be as great
    (or greater) than release overhead.

Release as Often as Possible and Prudent
----------------------------------------

There’s probably a lot more that can be written on this topic, but I’m
quickly approaching the TL;DR event horizon (if I haven’t passed it
already). I’m excited to continue to learn more about effective release
strategies so I look forward to your thoughtful comments on this topic.

At the end, my goal was to make it clear why releasing early and often
is a good thing. I don’t currently believe there’s an empirical answer
to the question, how often should you release? Rather, my answer right
now is to suggest as often as possible and prudent.

If you release often, but find that your releases tend to be of a low
quality, then perhaps it’s time to take the dial back a bit. If your
releases are of a very high quality, perhaps it’s worth looking at any
waste that goes into each release and trying to eliminate it so you can
release even more often if doing so would appeal to your customers.

For more reading, I recommend:

-   [The Hardest Lessons for Startups to
    Learn](http://www.paulgraham.com/startuplessons.html "Hardest Lessons")
    This is an essay by Paul Graham with multiple lessons for startups.
    His first point he makes is about releasing early.
-   [The Cathedral and the
    Bazaar](http://www.free-soft.org/literature/papers/esr/cathedral-bazaar/cathedral-bazaar.html "Cathedral and the Bazaar")
    This is the essay by Eric S. Raymond that popularized the philosophy
    of RERO (Release Early, Release Often).
-   [Release Early, Release
    Often](http://blog.chromium.org/2010/07/release-early-release-often.html "Release Early, Release Often")
    This is a blog post by the Chromium project about their increased
    release cadence.
-   [1.0 is the Loneliest
    Number](http://ma.tt/2010/11/one-point-oh/ "One point Oh") This is a
    blog post by Matt Mullenweg, the founder of WordPress where he talks
    about the importance of getting to version 1.0 quickly.


