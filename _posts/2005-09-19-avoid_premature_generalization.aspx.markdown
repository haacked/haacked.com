---
layout: post
title: "Avoid Premature Generalization"
date: 2005-09-19 -0800
comments: true
disqus_identifier: 10231
categories: []
---
You’ve no doubt heard me rant against premature optimization in the
past, but [Eric Gunnerson](http://blogs.msdn.com/ericgu/) points out
another “Premature” action to be avoided, “[Premature
Generalization](http://blogs.msdn.com/ericgu/archive/2005/09/19/471327.aspx)”.

His discussion centers around a very specific question of whether to use
private properties to access private fields, or just allow access to the
field. Note this discussion pertains to fields that are not publicly
accessible via property nor direct access.

The place you’ll often see premature generalization is when
inexperienced developers start applying [Design Patterns
everywhere](http://haacked.com/archive/2005/05/31/3935.aspx). If you
need to instantiate a factory, implement an adapter class and use a
bridge to the toilet just to take a dump, then you probably live with a
developer with a premature generalization problem.

Like optimization, generalization is good when it is applied judiciously
in the right places. With optimization, one should measure measure
measure before applying optimizations. With generalization, I typically
suggest that a developer must **feel the pain** first before
generalizing. That simply means that the lack of generalization is
starting to cause more work than it saves. In my experience, this often
boils down to the rule of threes. If you have to implement something a
third time, refactor it.

For example, suppose you have an import tool for some system and as far
as you know, you’ll only have to support one import client. By all means
write an importer specific to that client. Now your boss tells you to
implement an importer for another client. Write that one specific to
that client. Once again your boss tells you to implement an importer for
yet another client. At this point a pattern has been established. Your
boss is a liar and you’ll probably need to implement importers for many
clients. Now is the time to refactor the code and generalize the concept
of importers. Maybe create a plug-in model or an Import Provider.

[Listening to: Cass & Slide / Perception - Sasha - Sasha: Global
Underground: Ibiza [2 of 2] (9:27)]

