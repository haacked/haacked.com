---
title: "Naming is Hard, Let's Go Shopping"
tags: [oss,nuget]
redirect_from: "/archive/2010/10/21/naming-is-hard.aspx/"
---

> There are only 2 hard problems in Computer Science. Naming things,
> cache invalidation and off-by-one errors.

I’m always impressed with the passion of the open source community and
nothing brings it out more than a naming exercise.
![Smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Naming-is-Hard-Lets-Go-Shopping_7876/wlEmoticon-smile_2.png)

In my last blog post, I posted about our [need to rename
NuPack](https://haacked.com/archive/2010/10/21/renaming-nupack.aspx "Renaming NuPack").
Needless to say, I got a bit of ~~angry~~passionate feedback. There have
been a lot of questions that keep coming up over and over again and I
thought I would try and address the most common questions here.

Why not stay with the NuPack name? It was just fine!
----------------------------------------------------

In the [original
announcement](http://www.outercurve.org/Blogs/EntryId/22/Changing-the-NuPack-Project-Name "Changing the NuPack project name"),
we pointed out that:

> We want to avoid confusion with another software project that just
> happens to have the same name. This other project,
> [NUPACK](http://nupack.org/), is a software suite by a group of
> researchers at Caltech having to do with analysis and design of
> nucleic acid systems.

Now some of you may be thinking, “Why let that stop you? Many projects
in different fields are fine sharing the same name. After all, you named
a blog engine *Subtext* and there’s a [Subtext programming language
already](http://en.wikipedia.org/wiki/Subtext_programming_language "Subtext Programming Language").”

There’s a **profound difference** between *Microsoft* starting an open
source project that *accepts contributions* and some nobody named Phil
Haack starting a little blog engine project.

Most likely, the programming language project has never heard of Subtext
and Subtext doesn’t garner enough attention for them to care.

As Paula Hunter points out in a comment on the Outercurve blog post:

> Sometimes we are victims of our own success, and NuPack has generated
> so much buzz that it caught CalTech's attention. They have been using
> NuPack since 2007 and theoretically could assert their common law
> right of "first use" (and, they recently filed a TM application). Phil
> and the project team are doing the right thing in making the change
> now while the project is young. Did they have to? The answer is
> debatable, but they want to eliminate confusion and show respect to
> CalTech's project team.
>
> Naming is tough, and you can't please everyone, but a year from now,
> most won't remember the old name. How many remember Mozilla
> "Firebird"?

Apparently, we’re in good company when it comes to [open source projects
that have had to pick a new
name](http://www.mozilla.org/projects/firefox/firefox-name-faq.html "Renaming Firefox").
It’s always a painful process. This time around, we’re following
guidelines posted by Paula in a blog post entitled [The Naming Game:
Things to consider when naming an open source
project](http://www.outercurve.org/Blogs/EntryId/21/The-Naming-Game-Things-to-consider-when-naming-an-open-source-project "Things to consider when naming an open source project")
which talks about this concept of “first use” Paul mentioned.

Why not go back to NPack?
-------------------------

There’s already a [project on
CodePlex](http://npack.codeplex.com/ "nPack") with that name.

Why not name it NGem?
---------------------

Honestly, I’d prefer not to use the N prefix. I know one of the choices
we provided had it in the name, but it was one of the better names we
could come up with. Also, I’d like to not simply appropriate a name
associated with the Ruby community. I think that could cause confusion
as well. I’d love to have a name that’s uniquely ours if possible.

Why not name it \*\*\*\*?
-------------------------

In the original announcement, we listed three criteria:

-   Domain name available
-   No other project/product with a name similar to ours in the same
    field
-   No outstanding trademarks on the name that we could find

### Domain name

The reason we wanted to make sure the domain name is available is that
if it is, it’s less likely to be the name of an existing product or
company. Not only that, we need a decent domain name to help market our
project. This is one area where I think the community is telling us to
be flexible. And I’m willing to consider being more flexible about this
just as long as the name we choose won’t run afoul of the second
criteria and we get a decent domain name that doesn’t cause confusion
with other projects.

### Product/Project With Similar Names

This one is a judgment call, but all it takes is a little time with
Google/Bing to assess the risk here. There’s always going to be a risk
that the name we pick will conflict with something out there. The point
is not to eliminate risk but reduce it to a reasonable level. If you
think of a name, try it out in a search engine and see what you find.

### Trademarks

This one is tricky. Pretty much, if your search engine doesn’t pull up
anything, it’s unlikely there is a trademark. Even so, it doesn’t hurt
to put your search through the US Patent office’s [Trademark Basic Word
Mark](http://tess2.uspto.gov/bin/gate.exe?f=searchss&state=4001:qe5d8t.1.1 "Trademark Electronic Search System (TESS)")
Search and make sure it’s clean there. I’m not sure how comprehensive or
accurate it is, but if it is there, you’re facing more risk than if it
doesn’t show up.

I have a name that meets your criteria and is way better than the four options you gave us!
-------------------------------------------------------------------------------------------

Ok, this is not exactly a question, but something I hear a lot. In the
original blog post, we said the following:

> #### Can I write in my own suggestion?
>
> Unfortunately no. Again, we want to make sure we can secure the
> domains for our new project name, so we needed to start with a list
> that was actually attainable. If you really can’t bring yourself to
> pick even one, we won’t be offended if you abstain from voting. And
> don’t worry, the product will continue to function in the same way
> despite the name change.

However, I don’t want to be completely unreasonable and I think people
have found a loophole. We’re conducting voting through [our issue
tracker](http://nupack.codeplex.com/workitem/list/basic "NuPack Issue Tracker")
and voting closes at **10/26 at 11:59 PM PDT**. Our reasoning for not
accepting suggestions was we wanted to avoid domain squatting. However,
one creative individual created a bug to rename NuPack to a name for
which they own the domain name and are willing to assign it over to the
Outercurve foundation.

Right now, NFetch is way in the lead. But if some other name were to
take the lead **and** meet all our criteria, I’d consider it. I reserve
the right of veto power because I know one of you will put something
obscene up there and somehow get a bajillion votes. Yeah, I have my eye
on you [Rob](http://blog.wekeroad.com/ "Rob Conery's Blog")!

So where does that leave us?
----------------------------

We really don’t want to leave naming the project as an open ended
process. So I think it’s good to set a deadline. On the morning of
10/27, for better or worse, you’ll wake up to a new name for the
project.

Maybe you’ll hate it. Maybe you’ll love it. Maybe you’ll be ambivalent.
Either way, over time, hopefully this mess will fade to a distant memory
(much as Firebird has) and the name will start to fit in its new
clothes.

As Paul Castle [stated over
Twitter](http://twitter.com/SleeperPService/status/28384103154 "Paul Castle tweet"):

> @haacked to me the name is irrelevant the prouduct is ace

No matter what the name is, we’re still committed to delivering the best
product we can [with your
help](http://nupack.codeplex.com/documentation?title=Contributing%20to%20NuPack "Contributing to NuPack")!

And no, we’re not going to name it:

![prince-symbol](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Naming-is-Hard-Lets-Go-Shopping_7876/prince-symbol_84a96c0e-8bf9-4fa2-a82d-9a46970810d9.jpg "prince-symbol")

