``---
layout: post
title: "The Siren Song of Backwards Compatibility"
date: 2015-05-24 -0800
comments: true
categories: [aspnet]
---

## The Importance of Backwards Compatibility

If anyone tells you that backwards compatibility isn't important, they're wrong. You can prove it to them by downgrading their operating system (to simulate breaking changes) when they're not looking and see how they complain about some of their applications suddenly failing.

As a former purveyor of a web framework, I've seen the most vocal "go ahead and break things" proponent hine like a little baby when we introduced a breaking change during a pre-release cycle!

Microsoft is famous for its dedication to backwards compatibility. In his post, [How Microsoft Lost the API War](http://www.joelonsoftware.com/articles/APIWar.html) Spolsky highlights this comment from Raymond Chen, famous for his [stories](http://blogs.msdn.com/b/oldnewthing/archive/2003/12/23/45481.aspx) of the [crazy lengths](http://blogs.msdn.com/b/oldnewthing/archive/2003/10/15/55296.aspx) they went to maintain backwards compat.

> Look at the scenario from the customer's standpoint. You bought programs X, Y and Z. You then upgraded to Windows XP. Your computer now crashes randomly, and program Z doesn't work at all. You're going to tell your friends, "Don't upgrade to Windows XP. It crashes randomly, and it's not compatible with program Z." Are you going to debug your system to determine that program X is causing the crashes, and that program Z doesn't work because it is using undocumented window messages? Of course not. You're going to return the Windows XP box for a refund. (You bought programs X, Y, and Z some months ago. The 30-day return policy no longer applies to them. The only thing you can return is Windows XP.)

I've had situations where I've written one-off applications for someone and hope to never have to touch that code again. This is where I love Microsoft's adherence to backwards compatibility.

## Backwards Compatibility is a Tax

But there's another side to the backwards compatibility story. Backwards compatibility is a tax that creates significant drag on a team's agility and its ability to innovate. A long time ago I wrote a post suggesting that this [blind adherence to backwards compatibility was holding Microsoft back](http://haacked.com/archive/2006/10/01/Is_Backward_Compatibility_Holding_Microsoft_Back.aspx/).

Which is an interesting position to be in. One one hand, I believe backwards compatibility is one of Microsoft's key selling points and a source of its strength. On the other hand I believe it's holding Microsoft back. So which is it? Well, nobody said competing in the software industry would be easy and straightforward.

Many arguments miss another key aspect of backwards compatibility. Technical compatibility isn't the only aspect to backwards compatibility. For example, if a company isn't able to innovate and have its product stay relevant, then it removing investment in that product or going out of business breaks backwards compatibility.

In this case, compatibility isn't broken by new versions of the product. It's broken by the world changing around the now stagnant unsupported version of the product. We've seen this happen not long after Microsoft pulled support for Windows XP, a [zero-day exploit was discovered](http://krebsonsecurity.com/2014/05/microsoft-issues-fix-for-ie-zero-day-includes-xp-users/). Microsoft relented and patched it, but makes no promise to patch the next zero day.

At this point, users of the product have to make a decision, switch, or stay and risk the exploits.

## ASP.NET vNext

I believe this is the situation that ASP.NET found itself in. When I was part of the team several years ago, I worked on a new product called ASP.NET MVC. Though it was "new", it still built on the existing 13 year-old, at the time, System.Web stack.

This code had accumulated a LOT of cruft and any change to it was a slow and tedious process requiring all sorts of testing on multiple operating systems etc. There were compatibility fixes upon fixes, some of which I'm pretty sure nobody actually understood the code.

Around this time, my manager, Scott Hunter (heretofore known as "The Hu" to complement Scott Guthrie who is "The Gu") and I started day dreaming about completely redoing the stack. I coined the name "ASP2.NET" as a joke. At the time, this was an impossible dream. The disruption to existing customers would be too great. Backwards compatibility is monarch!

But the world changed around Microsoft. Node.js exploded on the scene. ASP.NET was starting to show its age and become irrelevant. And as I [mentioned in my last post](http://haacked.com/archive/2014/05/17/microsofts-new-running-shoes/), with Azure, Microsoft's business model started to change.

> Azure provides an environment that is not limited to hosting .NET web applications. Azure makes money whether you host ASP.NET, NodeJs, or whatever. This is analogous to how the release of Office for iPad is a sign that Office will no longer help prop up Windows. Windows must live or die on its own merits.

ASP.NET found itself becoming increasingly irrelevant with new developers. It reached a crossroads where it had two possible strategies:

1. Continue to invest in the existing stack and try to slow the bleeding as much as possible.
2. Disrupt everything and build something new.

The first strategy is very appealing. It keeps existing customers feeling comfortable and happy. It would be profitable for a very long time. But it's ultimately not sustainable. What it means is Microsoft would be hamstrung in growing new business from newer more progressive developers and businesses. Also, eventually, even the old rank and guard, with their older developers replaced by newer developers, would reach the point where they're ready to move to a new platform. What would they choose then?

The ASP.NET team is focused on providing that next platform. Businesses often go through technology cycles and the ASP.NET team wants to be there with a modern platform when they do. It might rattle existing customers a bit, but that's a calculated risk. After all, if you're an existing customer, you know you have 10 years of support on the current stack. It goes by fast, but it's still a long time.

I left Microsoft a little under two years ago, but Scott Hunter stayed on and continued to execute on the impossible dream with his team and folks like David Fowler, Louis De Jardin, and others. That's why I'm pretty excited about ASP.NET vNext. It's not just another flavor of the day framework from Microsoft. It represents a new modular approach that makes it easier to swap out the parts you don't like and keep the parts you do.