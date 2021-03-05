---
title: Joel On Ruby Performance
tags: [performance]
redirect_from: "/archive/2006/09/11/Joel_On_Ruby_Performance.aspx/"
---

![Ruby](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/JoelOnRubyPerformance_B8DA/ruby6.jpg)
Joel Spolsky follows up on his [earlier
remarks](http://www.joelonsoftware.com/items/2006/09/01.html) about
scaling out a Ruby on Rails site with this post on [Ruby
performance](http://www.joelonsoftware.com/items/2006/09/12.html).  I’m
afraid it is a thoroughly unconvincing and surprising argument.  He
states...

> I understand the philosophy that developer cycles are more important
> than cpu cycles, but frankly that’s just a bumper-sticker slogan and
> not fair to the people who are complaining about performance.

A *bumper-sticker slogan?*  That’s a surprising statement considering
that FogBugz is not written entirely in C.  Is it because Wasabi
compiled to PHP or VBScript is saving CPU cycles?  Hardly.

As one might expect from a well designed application, FogBugz is written
in a productive high-level language for the very reason that Ruby
advocates push ruby - it saves developer cycles and thus money.

Also as one would expect from a well written application, in the few
cases where performance *is* a problem, those particular features were
written with a lower-level high performance language.

So why wouldn’t this approach apply to Ruby?  From the tenor of his
post, Joel seems to indicate that those who choose to implement their
enterprise applications on Ruby are so religiously blinded by the
benefits of Rails that they would never dare allow the impurity of
non-Ruby code to enter the boundary of their architecture.

Really now?

To his credit, Joel states at the end...

> In the meantime I stand by my claim that it's not appropriate for
> every situation.

And this is true. It may not work well for that computation intensive
Bayesian filter.  But is anyone making the claim that Ruby is
appropriate for *every situation*?  The claim I’ve heard is that it is
certainly appropriate for many more situations than Joel gives credit
for.  I believe that.

Update: **Related Links**

-   [Ruby and Strongtalk](http://smallthought.com/avi/?p=16) - Avi
    Bryant points out that Ruby is slow due to its currently naive
    implementation of method dispatch but that poor performance is not a
    necessary curse of dynamic languages when implemented properly.
-   [Has Joel Spolsky Jumped the
    Shark?](http://www.codinghorror.com/blog/archives/000679.html) -
    Jeff Atwood of [CodingHorror.com](http://www.codinghorror.com/blog/)
    fame has the same reaction I did - WTF?

