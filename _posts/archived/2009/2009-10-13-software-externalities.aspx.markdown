---
title: Software Externalities
tags: [software,patterns,methodologies]
redirect_from: "/archive/2009/10/12/software-externalities.aspx/"
---

If you’re a manufacturing plant, one way to maximize profit is to keep costs as low as possible. One way to do that is to cut corners. Go ahead and dump that toxic waste into the river and pollute the heck out of the air with your smoke stacks. These options are much cheaper than installing smoke scrubbers or trucking waste to proper disposal sites.

![pollutions](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/SoftwareExternalities_80FB/pollutions_3.jpg "pollutions")

Of course, economists have long known that this does not paint the entire picture. Taking these shortcuts incur other costs, it’s just that these costs are not borne by the manufacturing plant. The term [externalities](http://en.wikipedia.org/wiki/Externality "Externalities on Wikipedia") describes such spillover costs.

> In economics an externality or spillover of an economic transaction is
> an impact on a party that is not directly involved in the transaction.
> In such a case, prices do not reflect the full costs or benefits in
> production or consumption of a product or service.

Thus the full cost of manufacturing includes the hospital bills of those who get sick by drinking the tainted water, the cost of the crops damaged by the acid rain, etc.

Software is the same way. I got to thinking about this after reading Ted’s latest post that [Agile is treating the symptoms not the disease](http://blogs.tedneward.com/CommentView,guid,53f9b658-3b27-4f1a-b93e-14d3a57a8ec1.aspx#commentstart "Ted's blog post") where the complexity that Agile introduces is disparaged and Access is held up as one example of a great “simple” way to develop applications.

I agree that Access is great when you’re building a little database to track Billy’s baseball cards. However, the real world doesn’t stay that simple. As the [second law of thermodynamics](http://blogs.tedneward.com/CommentView,guid,53f9b658-3b27-4f1a-b93e-14d3a57a8ec1.aspx#commentstart "Second Law of Thermodynamics") states (paraphrasing here), **entropy tends to increase over time**, which is something that Ted doesn’t address in his discussion.

I’m all for simplicity in our tools and methodologies as I think we still have a lot of room for improvement in reducing [accidental complexity](http://en.wikipedia.org/wiki/Accidental_complexity "Accidental Complexit in Wikipedia"). Unfortunately, the business processes for which we build software are not simple at all and full of [inherent complexity](http://en.wikipedia.org/wiki/Essential_complexity "Inherent Complexity"). Oh sure, they may start off as a simple Access database, but they never stay that simple. Every business I’ve ever interacted had very complex sets of business processes, some seemingly [cargo cultish](http://en.wikipedia.org/wiki/Cargo_cult "Cargo Cult") in origin, which led to major complexity in business processes.

Ted mentions friends of his who’ve made a healthy living using simpletools to build simple line-of-business apps for customers. And I’m sure they did a fine job of it. But I also made a healthy living in the past coming in to clean up the externalities left by such applications.

I remember one rescue operation for a company drowning in the complexity of a “simple” Access application they used to run their business. It was simple until they started adding new business processes they needed to track. It was simple until they started *emailing copies around*and were unsure which was the “master copy”. Not to mention all the data integrity issues and difficulty in changing the monolithic procedural application code.

I also remember helping a teachers union who started off with a simple attendance tracker style app (to use an example Ted mentions) and just scaled it up to an atrociously complex Access database with stranded data and manual processes where they printed excel spreadsheets to paper, then manually entered it into another application. *I have to wonder, why is that little school district in western Pennsylvania engaging in custom software development in the first place? I don’t engage in developing custom school curricula. An even simpler option is to buy some off the shelf software or simply use a Wiki, but I digress.* 

These were apps that would make The Daily WTF look like paragons of good software development in comparison.

The core problem here is that while it’s fine to push for simpler tools to reduce accidental complexity, at one point or another we are going to have to deal with inherent complexity caused by entropy. Business processes are inherently complex, usually more than they need to be, and **this is not a problem that will be solved by any software**. Most are not only inherently complex, but chock-full of accidental complexity as well. Your line of business app won’t solve that. It takes systemic change in the organization to make that happen.

Not only that, but business processes get more complex over time as entropy sets in. The applications I mentioned dealt with this entropy and reached a point where the current solution could not scale to meet that new level of complexity (a different sort of scaling up), so they started to drown in it, the original authors of the applications long gone off to create new apps with new externalities.

Fortunately, the externalities of these applications didn’t cause cancer, but rather kept guys like me employed. Of course, it was a negative externality for the company who kept pumping cash to fix these applications.

Ted paraphrases Billy suggesting that Agile requires even more complex tools, story cards, continuous integration servers, etc. This is an unfair characterization and misses the point of Agile. **Agile is less about managing the complexity of an application itself and more about managing the complexity of building an application**.

A higher principle of agile is YAGNI (You ain’t gonna need it) until you need it. For example, the 1 to 2 guys in a garage probably don’t need a continuous integration server, stand up meetings, etc and any real agilist worth his or her salt would recognize that and not try to force unnecessary procedures on a team that didn’t need it.

However, as the two garage dwellers start to grow the business and need to coordinate with more developers, such tools come in handy. As you grow a team beyond two people, the lines of communication start to grow exponentially, thus creating inherent complexity.  Looking at the cost of developing and maintaining an application over time is where you start to get a full picture of the true cost of building an application.

As Robert Glass pointed out in [Facts and Fallacies of Software Engineering](http://www.amazon.com/gp/product/0321117425?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=9325&creativeASIN=0321117425 "Facts and Fallacies of Software Engineering at Amazon"), research shows that **maintenance typically consumes from 40 to 80 percent of software costs**, typically making it [the dominant life cycle phase of a software project](https://haacked.com/archive/2007/01/09/Writing_Maintainable_Code.aspx/ "Writing Maintainable Code"). Thus these so called “simple” solutions need to factor that in, or the customers will continually be left with the clean-up duty while the polluters have long since moved on.
