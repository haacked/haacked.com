---
title: The meaning of Impedance Mismatch
date: 2004-06-15 -0800
tags:
- code
redirect_from: "/archive/2004/06/14/impedance-mismatch.aspx/"
---

You've probably heard the term *Impedance Mismatch* thrown around when
discussing object relational mapping. I'm sure it comes up every morning
at the water cooler. Maybe you've even thrown it around yoursef a few
times. Do you know what the term means?

Object relational mapping refers to the process of mapping your
relational data model to your object model. Object Spaces is a highly
publicized framework for doing just that. The mismatch I refer to is a
result of the differences in structure between a normalized relational
database and a typical object oriented class hierarchy. One might say
Databases are from Mars and Objects are from Venus. Databases do not map
naturally to object models. It's alot like trying to push the north
poles of two magnets together.

Interestingly enough, the term "Impedance", now bandied about in
software engineering circles, is borrowed from electronics. I'm going to
do a disservice to electrical engineers all over the world by offering
a very simple explanation. (My aplogies to you EEs out there).

> **Impedance** is the measure of the amount that some object impedes
> (or obstructs) the flow of a current. Impedance might refer to
> *resistance*, *reactance*, or some complex combination of the two.

Perhaps an example is in order to illustrate impedance mismatching:

Imagine you have a low current flashlight that normally uses AAA
batteries. Don't try this at home, but suppose you could attach your car
battery to the flashlight. The low current flashlight will pitifully
output a fraction of the light energy that the high current battery is
capable of producing. Likewise, if you attached the AAA batteries to
Batman's spotlight, you'll also get low output. However, match the AAA
batteries to the flashlight and they will run with maximum efficiency.

So taking this discussion back to software engineering, if you imagine
the flow of data to be analogous to a current, then the impedance of a
relational data model is not matched with the impedance of an object
hierarchy. Therefore, the data will not flow with maximum efficiency, a
result of the impedance mismatch.

