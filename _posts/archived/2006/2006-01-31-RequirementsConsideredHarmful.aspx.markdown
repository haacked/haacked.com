---
title: Requirements Considered Harmful
date: 2006-01-31 -0800 9:00 AM
tags: [product-management]
redirect_from: "/archive/2006/01/30/RequirementsConsideredHarmful.aspx/"
---

Micah delves into the dark
side of requirement documents. I am glad he is taking on writing about some
of the challenges we’ve faced in starting a new company. I’ve been
planning to write about many of these challenges as well, but I wanted
to give myself more time. Perhaps I was a bit too shell-shocked by all
the difficulties to give it a proper treatment. Perhaps I was afraid I
would violate the one blogging rule we have, don’t be stupid.

Fortunately, Micah has the bravery to delve into the deep dark dirty
recesses of real world project management. Read it at your own risk and
hold tight to your sanity.

> The client came to us with a lot of requiremets. There were catalogs of use cases, user scenarios by user role, and functional requirements. There were system requirements, security requirements, and performance requirements; all carefully documented. Business logic was carefully laid out with workflow diagrams. There was a catalog of business rules. They even had helpful examples of how the rules would apply to different situations and system states.
> 
> The client had been doing business analysis on this problem for almost two years. I swooned at the prospect of working with such a focused and organized client. And that’s about when things went awry.
> 
> Implementing each requirement would yield the usual set of clarification questions. Dear client, R0223 is in conflict with the validation method for business rule BR045. What should we do? Nine times out of ten the answer was something that was not to be found in the 900 pages of requirements, analysis, or design documents. And about one out of every twenty answers required rethinking the design, the architecture, or the requirement itself. Do you really need integration with application X? Who uses that? Oh that person left last year so we don’t need it anymore. But the new guy uses Y – can you support that?
> 
> What was the problem? I had broken one of my own cardinal rules:
> 
>> No complex problem is fully understood until you attempt to solve it.
>
> The only time I feel safe ignoring this rule is if I have indeed fully solved the same problem before. Repeatedly. And even then I would do well to keep my mind an “uncarved block”, as the Taoists say.
> 
> In this case, the basic gist of the solution was in the requirements. The goals and general criteria were helpful. But the detail of all the requirements and the pages and pages of documentation actually hindered our understanding of the real problems and issues the client faced day-to-day. It was too easy for us to assume rather than verify and to guess instead of test.
> 
> This led to some spectacular omissions of the obvious, such as finding out very late in the project that a core part of the data model needed to support many-to-many relations instead of one-to-many. That resulted in a minor implementation detail best left as an exercise for the reader…
> 
> I don’t think all requirements are bad, of course. Every project should start somewhere and written documentation is easy to share and easy for the non-technical folks on a project to understand. But this project has hammered home a simple point – ultra-detailed written requirements usually serve the same purpose as running demo code. They’re just a lot harder to debug.
