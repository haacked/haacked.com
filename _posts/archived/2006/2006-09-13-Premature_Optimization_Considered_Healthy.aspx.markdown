---
title: Premature Optimization Considered Healthy
tags: [code,performance]
redirect_from: "/archive/2006/09/12/Premature_Optimization_Considered_Healthy.aspx/"
---

Some computer scientist by the name of [Donald
Knuth](http://en.wikipedia.org/wiki/Donald_Knuth) once said,

> Premature optimization is the root of all evil (or at least most of
> it) in programming.

Bah! What did he know?

Of course we all know what he meant, but when you take his statement at
face value, the claim is a bit vague.  **What exactly is it that is
being optimized?**

[![Image From
http://www.galcit.caltech.edu/awt/](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/PrematureOptimizationConsideredHealthy_2A49/WindTunnel_thumb%5B2%5D.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/PrematureOptimizationConsideredHealthy_2A49/WindTunnel%5B4%5D.jpg)

Well **speed** of course! At least that is the optimization that Knuth
refers to and it is what developers typically mean when they use the
term *optimize*.  But there are many factors in software that can be
optimized, not all of which are evil to optimize prematurely.

The key positive optimization that comes to mind is optimizing developer
productivity.  I hardly see anything evil about optimizing productivity
early in a project.  It is most certainly a healthy thing to do, hence
the misleading title of this post.

However as with all things, optimizations bring with them tradeoffs. 
Optimizing for developer productivity often comes at the price of
optimizing code execution speed.  Likewise optimizing for speed will
come at the cost of developer productivity.

Security is another example of an optimization that bears with it
various trade-offs.

**The point of all this is to keep in mind that at all times within a
software project, whether explicitely or implicitely, you are optimizing
for something**.  It is important to be intentional about what exactly
you wish to optimize.

If you start [optimizing for
performance](http://www.joelonsoftware.com/items/2006/09/12.html) early,
keep in mind Knuth’s forewarning.  If you are optimizing for
productivity early, then you are on the right track.  This does not mean
that you should *never *consider performance. On the contrary, a good
developer should definitely [design for
performance](http://blogs.msdn.com/ricom/archive/2003/12/12/43245.aspx)
and [measure measure
measure](http://blogs.msdn.com/ricom/archive/2003/12/02/40779.aspx).

The danger to avoid is diverting too much optimization attention to
areas that provide too little gain, as discussed in my [last post on
Ruby
Performance](https://haacked.com/archive/2006/09/12/Joel_On_Ruby_Performance.aspx).

