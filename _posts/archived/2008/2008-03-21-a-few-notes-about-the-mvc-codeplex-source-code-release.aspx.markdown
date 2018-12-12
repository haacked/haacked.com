---
title: A Few Notes About The MVC CodePlex Source Code Release
date: 2008-03-21 -0800
disqus_identifier: 18466
categories: [aspnetmvc]
redirect_from: "/archive/2008/03/20/a-few-notes-about-the-mvc-codeplex-source-code-release.aspx/"
---

Whew! I’ve held off writing about MVC until I could write [a non-MVC
post](https://haacked.com/archive/2008/03/21/is-pizza-brain-food.aspx "Is Pizza Brain Food?")
in response to some [constructive
criticism](http://lostechies.com/blogs/sean_chambers/archive/2008/02/26/what-happened-to-you-ve-been-haacked.aspx "What happened to my blog?")
(It’s not just
[Sean](http://lostechies.com/blogs/sean_chambers/ "Sean Chambers"),
[Jeff](http://codinghorror.com/ "Jeff Atwood") mentioned something to me
as well). Now that I’ve posted that, perhaps I’ve bought myself a few
MVC related posts in a row before the goodwill runs dry and I have to
write something decidedly not MVC related again. ;)

As [ScottGu](http://weblogs.asp.net/scottgu/ "Scott Guthrie") [recently
posted](http://weblogs.asp.net/scottgu/archive/2008/03/21/asp-net-mvc-source-code-now-available.aspx "Source Available"),
the ASP.NET MVC source code is [now available via
CodePlex](http://www.codeplex.com/aspnet/ "ASP.NET MVC CodePlex"). A
move like this isn’t as simple as flipping a switch and \*boom\* it
happens. No, it takes a lot of effort behind the scene. On the one hand
is all the planning involved, and [Bertrand Le
Roy](http://weblogs.asp.net/bleroy/ "Bertrand") and my boss Simon played
a big part in that.

Along with planning is the execution of the plan which requires
coordination among different groups such as the Devs, PMs, QA and the
legal team. For that, we have our newest PM [Scott
Galloway](http://www.mostlylucid.co.uk/ "Scott Galloway") to thank for
that effort. I helped a little bit with the planning and writing the
extremely short
[readme](javascript:__doPostBack(’ctl00$ctl00$Content$TabContentPanel$Content$ReleaseFiles$FileList$ctl01$FileNameLink’,’’) "Readme file")
(I didn’t know what to say) and
[roadmap](http://www.codeplex.com/aspnet/Wiki/View.aspx?title=Road%20Map&referringTitle=Home "ASP.NET MVC RoadMap").
One part of this experience that went surprisingly well was the person
from our legal department we worked with. I was expecting a battle but
this guy just got it and really understood what we were trying to do and
was easy to work with.

With that said, I’ve seen a lot of questions about this so I thought I
would answer a few here.

**Is this the live source repository?**

No, the MVC dev team is not committing directly into the CodePlex source
code repository for many reasons. One practical reason is that we are
trying to reduce interruptions to our progress as much as possible.
Changing source code repositories midstream is a big disruption. For
now, we’ll periodically ship code to CodePlex when we feel we have
something worth putting out there.

**Where is the source for Routing?**

As I [mentioned
before](https://haacked.com/archive/2008/03/10/thoughts-on-asp.net-mvc-preview-2-and-beyond.aspx "Thoughts on Preview 2"),
routing is not actually a feature of MVC which is why it is not
included. It will be part of the .NET Framework and thus its source will
eventually be available much like the rest of the [.NET Framework
source](http://weblogs.asp.net/scottgu/archive/2008/01/16/net-framework-library-source-code-now-available.aspx ".NET Framework Source is now available").
It’d be nice to include it in CodePlex, but as I like to say, *baby
steps*.

**Where are the unit tests?**

Waitaminute! You mean they’re not there?! I better have a talk with
Scott. I kid. I kid.  We plan to put the unit tests out there, but the
current tests have some dependencies on internal tools which we don’t
want to distribute. We’re hoping to rewrite these tests using something
we feel comfortable distributing.

**When’s the next update to CodePlex?**

As I mentioned, we’ll update the source when we have something to show.
Hopefully pretty often and soon. We’ll see how it goes.

As a team, we’re pretty excited about this. I wondered if the devs would
feel a bit antsy about this level of transparency. Sure, anyone can see
the source code for the larger .NET Framework, but that code has already
*shipped*. This is all early work in progress. Can you imagine at your
work place if your boss told *you* to publish all your work in progress
for all the world to critique (if you’re a full time OSS developer,
don’t answer that). ;) I’m not sure I’d want anyone to see some of my
early code using .NET.

Fortunately, the devs on my team buy into the larger benefit of this
transparency. It leads to a closer collaboration with our customers and
creates a tighter feedback cycle. I am confident it will pay off in the
end product. Of course they do have their limits when it comes to
transparency. I tried suggesting we take it one step further and publish
our credit card numbers in there, but that was a no go.

