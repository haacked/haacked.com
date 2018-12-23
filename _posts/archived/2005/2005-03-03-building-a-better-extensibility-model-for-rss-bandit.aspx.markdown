---
title: Building a Better Extensibility Model For RSS Bandit
date: 2005-03-03 -0800
tags: [rss]
redirect_from: "/archive/2005/03/02/building-a-better-extensibility-model-for-rss-bandit.aspx/"
---

Currently, the only plug-in model supported by RSS Bandit is the
[IBlogExtension](http://www.pocketsoap.com/weblog/stories/2003/04/0023.html)
interface. This is a very limited interface that allows developers to
write a plug-in that adds a menu option to allow the user to manipulate
a single feed item.

The ability to interact with the application from such a plug-in is very
limited as the interface doesn't define an interface to the application
other than a handle. (For info on how to write an IBlogExtension
plug-in, see [this
article](https://haacked.com/archive/2004/06/19/651.aspx).)

So last night I was thinking about an email that a user sent me. He
wants to know how to add an intelligent module for filtering and
classifying RSS feeds to RSS Bandit. This is for his thesis project.

Since that might not be a necessary feature for RSS Bandit at this
point, I suggested that he implement it in a private build. RSS Bandit
is open source and he can easily obtain the source code. But it occurred
to me that there are hundreds of feature requests like this that have
the potential to be great solutions in the future, but would be best
implemented as a plug-in in the near term.

So as I sat there thinking about it, Torsten goes ahead and [implements
a
prototype](http://www.rendelmann.info/blog/PermaLink.aspx?guid=d3c8dfd5-c3f7-4e74-bdb0-0168eb4e2d82)
for it. Mind you, I hadn't talked to Torsten nor
[Dare](http://www.25hoursaday.com/weblog/) about this yet, but I'm sure
it's something we've all been thinking about. Especially as the number
of feature requests continues to accumulate.

So what areas of extensibility might we want to support? Well there's
the callback on feed item download that Torsten implemented. That alone
is quite useful. I can imagine building a plug-in that would score items
based on how likely I'd want to see it or not. Thus it could sort items
based on importance in a special feed. This would help with the
increasingly large number of feeds I'm subscribed to.

Dare mentioned (and he'll write more on this) the idea to support new
data sources and formats. For example, it'd be interesting to build NNTP
support as a plug-in.

I'd also like to build the ability to provide an interface for plug-in
data storage. For example, suppose you build a plug-in that keeps
statistics of your RSS feeds. You might want that stored with your
settings so that when you synchronize RSS Bandit, your plug-in settings
are synchronized as well.

]

Thinking far out there, perhaps adding the ability to support IE
toolbars might be an option as I think some users actually use RSS
Bandit as their primary browser.

The important thing is to design a nice interface to expose the RSS
Bandit application to the plug-ins. In tandem we need to prioritize
these extensibility options by looking at current feature requests and
thinking about how many of them could be implemented as plug-ins.

