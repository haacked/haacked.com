---
title: Good Design Minimizes The Impact Of Changes
tags: [patterns,code]
redirect_from: "/archive/2006/11/12/Good_Design_Minimizes_The_Impact_Of_Changes.aspx/"
---

[![Blue Skies from
Stock.xchng](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/GoodDesignMinimizesTheImpactOfChanges_1198/blue_skies_thumb1.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/GoodDesignMinimizesTheImpactOfChanges_1198/blue_skies3.jpg)
We’ve all been there.  Your project stakeholder stands in your doorway
with a coffee mug in hand and asks for one more teeny tiny change.

> Yeeeaaah. It’d be great if you could just change the display to
> include the user’s middle name.  That’s pretty easy, right?

No problem!  Let’s see.  I’ll just need to modify the database schema to
add the column, update several stored procedures to reflect the schema
change, add a new property to the User class, update the data access
code to reflect the new property, and finally update the various user
controls that render or take in input for this information.

That’s quite a number of changes to the codebase for one measly little
change.

**The goal of good software design is to minimize the impact of changes
in the code. ** Many of you might be having the same reaction to this
that you would if I just told you the sky is blue.  Well no duh!  Even
so, I think this bears repeating again and again, because this principle
is violated in subtle ways, which I will discuss in a follow-on post.

This is one reason that duplicate code is considered such an odoriferous
[code smell](http://en.wikipedia.org/wiki/Code_smell).  When a snippet
of code is repeated, a change to the code affects every location in
which that snippet is located.

Many [Design
Patterns](http://en.wikipedia.org/wiki/Design_Patterns) focus
on minimizing the impact of changes by attempting to **look at what
varies in a system and encapsulate it**. 

For example, suppose you develop a class that monitors the power level
of your Universal Power Supply (UPS) device.  When a power level change
occurs, several UI widgets need to be updated.

A naïve implementation might have the UPS class keep a reference to each
widget that needs to be updated and directly makes a call to various
methods or properties of each widget to update the widget’s state.

The downside of this approach becomes apparent when you need to add
a new widget or change a widget.  You now need to update the UPS class
because of changes to the UI.  **The UPS class is not insulated to
changes in the UI**. 

The [Observer pattern](http://en.wikipedia.org/wiki/Observer_pattern)
addresses this issue by changing the direction of the dependency so that
the UPS class (the observed) does not have direct knowledge of the UI
widgets (the observers).  The widgets all implement a comment observer
interface and the UPS class only needs to know about that one
interface.  Add a new widget and the code for the class does not need to
be updated.  Now the UPS class is insulated from changes to the UI.

Another example of code that is **not** resilient to change is a class
with several methods that contain a similar switch statement.  Going
back to the example of the UPS class, suppose the class has several
operations it must do every few seconds.  But how it implements each
operation is dependent on the current power state.

A naïve implementation might have a switch statement in each method that
contains a case for each possible power state.  The problem with this
approach is that when we need to add a new power state or edit how an
existing state behaves, we have to update multiple existing methods. 
The [State pattern](http://en.wikipedia.org/wiki/State_pattern)
addresses this problem by encapsulating the behavior of a state in a
class.  Thus each power state would be encapsulated in a class and the
UPS class would simply delegate calls to its member state instance.

So where is the downside in all this? Seems like these patterns provide
a win-win situation for us.  Well in these contrived examples it sure
does, but not in every situation.  When used improperly, a pattern in
one scenario can actually increase the impact of change in another. 
[Stay
tuned](https://haacked.com/archive/2006/11/16/Tradeoffs_When_Minimizing_The_Impact_Of_Changes.aspx "Tradeoffs When Minimizing the Impact of Changes").

