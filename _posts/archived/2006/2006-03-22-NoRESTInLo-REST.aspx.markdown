---
title: No REST In Lo-REST
tags: [rss,patterns]
redirect_from: "/archive/2006/03/21/NoRESTInLo-REST.aspx/"
---

UPDATE: Ok, so being away from RSS Bandit has put me out of touch of
some of the other discussion on this topic. As Dare writes, Don isn’t
the first person to make the [Lo-REST
distinction](http://www.25hoursaday.com/weblog/PermaLink.aspx?guid=473cc14f-4668-43cf-b5b9-0178f9271296 "How Tool Vendors Can Better Support Rest").

![Sleeping Tiger](https://haacked.com/assets/images/sleeping_tiger.jpg)Well it
looks like Dimitri [beat me to the
punch](http://glazkov.com/archive/2006/03/22/2444.aspx "Dimitri Glazkov's Blog").
It also struck me as odd to hear [Don
Box](http://pluralsight.com/blogs/dbox/ "Don Box's Blog") coin the term
[Lo-REST](http://pluralsight.com/blogs/dbox/archive/2006/03/18/20235.aspx "Don Talks About Lo-Rest").
It also struck me to hear it repeated at Mix06 and described as really
just POX over HTTP. I had an interesting conversation with [Steve
Maine](http://hyperthink.net/blog/ "Steve Maine's blog") (who is one
cool cat by the way) about it at Mix06 that got me thinking.

The thing is, REST is an architectural style, and to some degree it is
well defined. What is not well defined is exactly how you build real
world web services using this style. If you perform a search for the
term “xml” within [Roy Fielding’s Dissertation
(pdf)](http://www.ics.uci.edu/%7Efielding/pubs/dissertation/fielding_dissertation.pdf "Roy Fielding REST Dissertation")
you will find zero matches. REST does not require nor really have
anything to do with XML. However, XML is a viable tool that can be used
with a RESTful service.

Now Don is a very smart guy and I have immense respect for his work. I
am more likely to second guess myself before I disagree with him. This
is why I want to give him the benefit of doubt and try to look at what
he was trying to accomplish in this post, though I obviously can’t speak
for him so this is mere conjecture. The question I have is whether Don
is “prescribing” or “describing”.

It seems to me that rather than try and prescribing a new form of REST,
he was merely describing the reality of service oriented implementations
that exist in the real world. Fact of the matter is that many companies
are unveiling RESTful services that end up being nothing more than
attempts to tunnel REST verbs through HTTP GET (Amazon is one example).

The problem is that if this is indeed the case, I can see the usefulness
of the term from an academic viewpoint. Lo-REST can usefully describe
what services that are labelled as REST, but are really not. Rather,
these services are actually POX based services.

Even so, attaching the term REST to a POX service is problematic in two
key ways (if not more). First, it dilutes and obscures what REST is,
which not only gets the RESTafarians all up in a tizzy, but also can
make the conversation around this topic more difficult. Second, it seems
to undercut the significance and usefulness of POX by implicitely
indicating that POX needs to be attached to the term REST in order to be
taken seriously.

Ideally, we should take a step back and realize that significant web
services are being written and will continue to be developed using POX.
Let’s elevate the respectability of the term POX a bit and retire the
term Lo-REST. It is POX, let's leave it at that and call it what it is.

DISCLAIMER: I am not what you would call a RESTafarian. I think I
understand what REST is, but I am on the fence on whether web services
SHOULD all be implemented as REST. I tend to take a more pragmatic
approach and think that some services will benefit from a REST style
approach and others will benefit from using SOAP. What I do care about
though is that for the purposes of the grand debate, that we keep our
terms clean and as well defined as we can and not dilute what one or the
other is.

