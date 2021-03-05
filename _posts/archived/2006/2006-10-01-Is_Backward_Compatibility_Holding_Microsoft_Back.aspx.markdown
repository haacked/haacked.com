---
title: Is Backward Compatibility Holding Microsoft Back
tags: [versioning,microsoft]
redirect_from: "/archive/2006/09/30/Is_Backward_Compatibility_Holding_Microsoft_Back.aspx/"
---

[![Atlas With The Weight Of The
Codebase](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IsBackwardCompatibilityHoldingMicrosoftB_ECEC/602762_a_thumb.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IsBackwardCompatibilityHoldingMicrosoftB_ECEC/602762_a2.jpg)
I read [this article
recently](http://www.informationweek.com/news/showArticle.jhtml?articleID=192501131&pgno=1&queryText= "Windows After Vista Demands Radical Rethinking")
that describes the mind frying complexity of the Windows development
process.  With Vista sporting around 50 million lines of code, it’s no
wonder Vista suffers from delays.  Quick, what does line \#37,920,117
say?

Microsoft has acknowledged the need to release more often (as in
sometime this millenia), but that agility is difficult to achieve with
the current codebase due to its immense complexity as well as
Microsoft’s (stubbornly?) heroic efforts to maintain backward
compatibilty.  The author of the article labels this the **Curse of
Backward Compatibility**.

I don’t think anyone doubts that maintaining backwards compatibility can
be a Herculean effort because it goes beyond supporting legacy
specification (which is challenging enough).  Just look at how Microsoft
[supports old code that broke the
rules](http://blogs.msdn.com/oldnewthing/archive/2006/04/10/572491.aspx "Changing the rules").  Additionally,
the fact that [old code poses a security
threat](http://news.com.com/Old+code+in+Windows+is+security+threat/2100-1001_3-934363.html) requires
even more code to patch those security threats.  Ideally alot of that
code would be removed outright, but it is challenging to remove or
rewrite any of it in fear of breaking too many applications.

Of course there are very good business reasons for Microsoft to maintain
this religious adherence to backwards compatibility (starts with an *m*
ends with a *y* and has *one* in the middle).  The primary one
being they have a huge user base when compared to Apple, which does not
give Microsoft the luxury of a “Do Over” as Apple has done with OSX.

[A
different article](http://www.informationweek.com/windows/showArticle.jhtml;jsessionid=ZIDGQ1GFU33X0QSNDLRCKHSCJUNN2JVN?articleID=192503689&pgno=1&queryText= "Microsoft must turn to virtual OS after Vista")
(same magazine) points to virtualization technology as the answer.  This
article talks suggests a virtualization layer that is core to the
operating system.  I think we are already seeing hints of this in
play with [Microsoft’s
answer](http://weblogs.asp.net/pwilson/archive/2006/09/27/Vista-will-NOT-support-Developers.aspx#588303 "Scott Guthrie Responds")
to [developers
angry](http://weblogs.asp.net/fbouma/archive/2006/09/27/So_2C00_-VB6-is-more-important-than-VS.NET-2003-I-suppose_3F00_.aspx "So VB6 Is More Important Than VS.NEt 2003 I Suppose?")
that Vista is [not going to support Visual Studio.NET
2003](http://blogs.msdn.com/somasegar/archive/2006/09/26/772250.aspx "Visual Studio Support For Vista").

> The big technical challenge is with enabling scenarios like advanced
> debugging. Debuggers are incredibly invasive in a process, and so
> changes in how an OS handles memory layout can have big impacts on it.
> Vista did a lot of work in this release to tighten security and lock
> down process/memory usage - which is what is affecting both the VS
> debugger, as well as every other debugger out there. Since the VS
> debugger is particularly rich (multi-language, managed/native interop,
> COM + Jscript integration, etc) - it will need additional work to
> fully support all scenarios on Vista. That is also the reason we are
> releasing a special servicing release after VS 2005 SP1 specific to
> Vista - to make sure everything (and especially debugging and
> profiling) work in all scenarios. It is actually several man-months of
> work (we’ve had a team working on this for quite awhile). Note that
> the .NET 1.1 (and ASP.NET 1.1) is fully supported at runtime on Vista.
> VS 2003 will mostly work on Vista. What we are saying, though, is that
> there will be some scenarios where VS 2003 doesn’t work (or work well)
> on Vista - hence the reason it isn’t a supported scenario. Instead, we
> recommend using a VPC/VM image for VS 2003 development to ensure 100%
> compat.

This answer did not satisfy everyone (which answer does?), many seeing
it as a copout as it pretty much states that to maintain backward
compatibility, use Virtual PC.

Keep in mind that this particular scenario is not going to affect the
average user.  Instead, it affects developers, who are notorious for
being early adopters and, one would think, would be more amenable to
adopting virtualization as an answer, because hey! It’s cool new
technology!

Personally I am satisfied by this answer because I have no plans to
upgrade to Vista any time soon (my very own copout).  Sure, it’s not the
best answer I would’ve hoped for if I was planning an impending
upgrade.  But given a choice between a more secure Vista released
sooner, or a several months delay to make sure that developers with
advanced debugging needs on VS.NET 2003 are happy, I’m going to have to
say go ahead and break with backward compatibility.  But at the same
time, push out the .NET 2.0 Framework as a required update to Windows
XP.

With Windows XP, Microsoft finally released a consumer operating system
that was good enough.  Many users will not need to upgrade to Vista for
a looong time.  I think it is probably a good time to start looking at
cleaning up and modularizing that 50 million line rambling historical
record they call a codebase.

If my DOS app circa 1986 stops working on Vista, so be it.  If I’m still
running DOS apps, am I really upgrading to Vista?  Using a virtual
operating system may not be the best answer we could hope for, but I
think it’s good enough and should hopefully free Microsoft up to really
take Windows to the next level.  **It may cause some difficulties, but
there’s no easy path to paying off the immense [design
debt](https://haacked.com/archive/2005/09/24/GoingIntoDesignDebt.aspx "Going Into Design Debt")
that Microsoft has accrued with Windows.**

