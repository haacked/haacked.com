---
layout: post
title: "The Siren Song of Backwards Compatibility"
date: 2014-05-27 -0800
comments: true
categories: [aspnet]
---

This post is sort of a continuation of my post on [Microsoft's New Running Shoes](http://haacked.com/archive/2014/05/17/microsofts-new-running-shoes/).

## The Importance of Backwards Compatibility

If anyone tells you that backwards compatibility isn't important, they're wrong. And in fact, if they use _any_ software long enough, they'll tell you themselves. Another upgrade of some framework they depend on will break their application and they'll get real care mad about it. I know because I've been on both sides of this river. I've shipped a Framework that broke people who told me we should break compatibility and experienced the heat of their anger. Usually, when someone tells you breaking compatibility is fine, they mean as long as it doesn't affect them.

Microsoft is famous for its tenacious dedication to backwards compatibility. In his post, [How Microsoft Lost the API War](http://www.joelonsoftware.com/articles/APIWar.html) Spolsky highlights this comment from Raymond Chen, famous for his [stories](http://blogs.msdn.com/b/oldnewthing/archive/2003/12/23/45481.aspx) of the [crazy lengths](http://blogs.msdn.com/b/oldnewthing/archive/2003/10/15/55296.aspx) they went to maintain backwards compat.

> Look at the scenario from the customer's standpoint. You bought programs X, Y and Z. You then upgraded to Windows XP. Your computer now crashes randomly, and program Z doesn't work at all. You're going to tell your friends, "Don't upgrade to Windows XP. It crashes randomly, and it's not compatible with program Z." Are you going to debug your system to determine that program X is causing the crashes, and that program Z doesn't work because it is using undocumented window messages? Of course not. You're going to return the Windows XP box for a refund. (You bought programs X, Y, and Z some months ago. The 30-day return policy no longer applies to them. The only thing you can return is Windows XP.)

I've been there. I've written applications for friends that I hope I never have to change again just to move it to a new server. I've also had times where I depended on some 3rd party code where I didn't have the source and the author was long gone. If that code breaks because of an operating system upgrade, I'm in a world of hurt. It's situations like these where Microsoft's adherence to backwards compatibility is a sanity saver.

## Backwards Compatibility is a Tax

But there's another side to the backwards compatibility story. All of those benefits I mentioned have a cost. Backwards compatibility is a tax that creates significant drag on a team's agility and its ability to innovate. A long time ago I wrote a post that suggested this [blind adherence to backwards compatibility was holding Microsoft back](http://haacked.com/archive/2006/10/01/Is_Backward_Compatibility_Holding_Microsoft_Back.aspx/).

Wait, so now I've argued both for and against backwards compatibility. Does it sound like I want to have it both ways? Well of course I do! But good design is a series of trade-offs and good execution is knowing when to make one trade-off vs the other. Nobody said it would be easy and straightforward to compete in the software industry and give users what they need.

For example, many discussions about this topic miss another key consideration. Technical compatibility isn't the sole factor in backwards compatibility. For example, if a company isn't able to innovate and have its product stay relevant, it might need to remove investment in the product, or worse, go out of business. This also breaks backwards compatibility.

In this case, compatibility isn't broken by new versions of the product. It's broken by how the world continues to change around the now stagnant unsupported version of the product. We've seen this happen not long after Microsoft pulled support for Windows XP, a [zero-day exploit was discovered](http://krebsonsecurity.com/2014/05/microsoft-issues-fix-for-ie-zero-day-includes-xp-users/). Microsoft relented and patched it, but makes no promise to patch the next zero day.

At this point, users of the product have to make a decision, switch, or stay and risk the exploits.

## ASP.NET vNext

I believe this is the situation that ASP.NET found itself in recent years. When I was part of the team several years ago, I worked on a new product called ASP.NET MVC. Though it was "new", it was really just a layer on top of the existing 13 year-old, at the time, System.Web stack.

This code had accumulated a LOT of cruft and any change to it was a slow and tedious process that required a huge test effort on multiple operating systems etc. There were compatibility fixes so old we were quite convinced they paradoxically predated the advent of computers. There were even fixes I wasn't sure anyone understood the code, but we were afraid to change it nonetheless.

Around this time, my manager, [Scott Hunter](https://twitter.com/coolcsh) (heretofore known as "The Hu" to complement Scott Guthrie who is known as "The Gu") and I often day dreamed (as one does) about a complete rewrite of the stack. As a joke, I coined the name "ASP2.NET" as the moniker for this new stack. At the time, we were Don Quixotes dreaming the impossible dreams. The disruption to existing customers would be too great. Backwards compatibility is monarch! It could never happen!

![Don Quixote charging the windmills by Dave Winer CC BY-SA 2.0](https://cloud.githubusercontent.com/assets/19977/3078128/f3f0f02c-e45c-11e3-9802-10f188c63934.jpg)

But the world changed around Microsoft. Node.js and many other modern web frameworks, unencumbered by years of compatibility drag, exploded on the scene. These frameworks felt fresh and lightweight. Meanwhile, as I [mentioned in my last post](http://haacked.com/archive/2014/05/17/microsofts-new-running-shoes/), Azure's business model created new incentives.

> Azure provides an environment that is not limited to hosting .NET web applications. Azure makes money whether you host ASP.NET, NodeJs, or whatever. This is analogous to how the release of Office for iPad is a sign that Office will no longer help prop up Windows. Windows must live or die on its own merits.

In this new environment, ASP.NET was starting to show its age. Continue on its current course and it would risk complete irrelevance with the next generation of developers. It reached a crossroads where it had two possible strategies:

1. Continue to invest in the existing stack and try to slow the bleeding as much as possible.
2. Disrupt everything and build something new.

The first strategy is appealing. It makes existing customers feel comfortable and happy. Heck, it would be profitable for a very long time. But it's ultimately not sustainable. It hamstrings Microsoft from making inroads with new developers not already on its stack. Also, eventually, companies switch from old technologies to newer platforms. It might be the result of the fact that they can't hire developers to work on the old platforms. Or it might be that the software that meets their new business needs are on newer platforms. Either way, what happens when they are ready to make this switch? What platform would they choose? 

ASP.NET wants to _be_ that next platform. It _needs_ to be that next modern platform. It might rattle existing customers a bit, but that's a calculated risk. After all, if you're an existing customer, you know you have 10 years of support on the current stack. It goes by fast, but it's still a long time. You might be angry at having to make that switch at that point, but Microsoft's betting that might happen anyways over time and they're hoping to provide the next platform that you switch to. As Steve Jobs [famously said](http://www.businessinsider.com/best-steve-jobs-quotes-from-biography-2011-10),

> If you don't cannibalize yourself, someone else will.

Microsoft wants to be the zombie, not the zombie food.

I left Microsoft a little under two years ago, but Scott Hunter stayed on and continued to execute on the impossible dream with his team and folks like [David Fowler](http://davidfowl.com/), [Louis De Jardin](http://whereslou.com/), and others. That's why I'm pretty excited about ASP.NET vNext. It's not just another flavor of the day framework from Microsoft. It represents a new modular approach that makes it easier to swap out the parts you don't like and keep the parts you do. And it's a sign that Microsoft is more and more taking a page from Steve Jobs playbook and [solve the Innovator's Dilemma](http://blogs.hbr.org/2011/10/steve-jobs-solved-the-innovato/),

> My passion has been to build an enduring company where people were motivated to make great products. The products, not the profits, were the motivation. Sculley flipped these priorities to where the goal was to make money. It’s a subtle difference, but it ends up meaning everything.

