---
title: The Cost Of Breaking Changes
tags: [code,versioning]
redirect_from: "/archive/2008/03/02/the-cost-of-breaking-changes.aspx/"
---

[![broken-glass](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TheCostOfBreakingChanges_A4A1/broken-glass_3.jpg)](http://www.sxc.hu/photo/296133 "Broken Glass on Stock Xchng")
One interesting response to my series on versioning of interfaces and
abstract base classes is the one in which someone suggested that we
should go ahead and break their code from version to version. They’re
fine in requiring a recompile when upgrading.

In terms of recompile, it’s not necessarily recompiling *your*code that
is the worry. It’s the third party libraries you’re using that poses the
problem. Buying a proper third-party library that meets a need that your
company can be a huge cost-saving measure in the long run, especially
for libraries that perform non-trivial calculations/tasks/etc... that
are important to your application. Not being able to use them in the
event of an upgrade could be problematic.

Let’s walk through a scenario. You’re using a third-party language
translation class library which has a class that implements an interface
in the framework. In the next update of the framework, a new property is
added to the interface. Along with that property, some new classes are
added to the Framework that would give your application a competitive
edge if it could use those classes.

So you try and recompile your app, fix any breaks in your app, and
everything seems fine. But when you test your app, it breaks because the
third party library is not implementing the new interface. The third
party library is broken by this change.

Now you’re in a bind. You could try to write your own language
translation library, but that’s a huge and difficult task and your
company is not in the business of writing language translators. You
could simply wait for the third party to update their library, but in
the meanwhile, your competitors are passing you by.

So in this case, the customer has no choice but to wait. It is my hunch
that this is the sort of bind we’d like to limit as much as possible.
This is not to say we never break, as we certainly do. But we generally
limit breaks to the cases in which there is a security issue.

Again, as I’ve said before, the constraints in building a framework, any
framework, are not the same constraints of building an app. Especially
when the framework has a large number of users. When Microsoft ships an
interface, it has to expect that there will be many many customers out
there implementing that interface.

This is why there tends to be an emphasis on Abstract Base Classes as I
[mentioned
before](https://haacked.com/archive/2008/02/21/versioning-issues-with-abstract-base-classes-and-interfaces.aspx "Abstract Base Class")
within the .NET Framework. Fortunately, when the ABC keeps most, if not
all, methods abstract or virtual, then testability is not affected. It’s
quite trivial to mock or fake an abstract base class written in such a
manner as you would an interface.

Personally, I tend to like interfaces in my own code. I like the way I
can quickly describe another concern via an interface and then mock out
the interface so I can focus on the code I am currently concerned about.
A common example is persistence. Sometimes I want to write code that
does something with some data, but I don’t want to have to think about
how the data is retrieved or persisted. So I will simply write up an
interface that describes what the interaction with the persistence layer
will look like and then mock that interface. This allows me to focus on
the real implementation of persistence later so I can focus on the code
that does stuff with that data now.

Of course I have the luxury of breaking my interfaces whenever I want,
as my customers tend to be few and forgiving. ;)

