---
title: Perception Vs Reality Regarding The .NET Framework Source Code
tags: [dotnet]
redirect_from: "/archive/2007/10/04/perception-vs-reality-regarding-the-.net-framework-source-code.aspx/"
---

I think [Miguel de Icaza](http://tirania.org/blog/ "Miguel de Icaza")
[nails
it](http://tirania.org/blog/archive/2007/Oct-05-2.html "A Journey Into the Dumbo-o-Sphere")
regarding some of the FUD being written about [Microsoft’s latest
move](http://weblogs.asp.net/scottgu/archive/2007/10/03/releasing-the-source-code-for-the-net-framework-libraries.aspx "Releasing the Source Code for the .NET Framework Libraries")
to make the source code to the .NET Framework available under the
[Microsoft Reference License
(Ms-RL)](http://www.microsoft.com/resources/sharedsource/licensingbasics/referencelicense.mspx "Ms-RL").

In fact, his post inspired me to try my hand at creating a comic. I have
no comic art skills (nor comic writing skills), so please forgive me for
my lack of talent (click for full size)...

[![Microsoft opens the
code](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/PerceptionVsReali.NETFrameworkSourceCode_13213/Microsoft%20opens%20the%20code_thumb_2.png)](https://haacked.com/assets/images/Microsoft-opens-the-code.png "Reality vs Perception")

I know some of the people involved who made this happen and I find it
hard to believe that there were nefarious intentions involved. You have
to understand that while Bill Gates and Steve Ballmer are known for
playing hardball, they aren’t necessarily personally involved in every
initiative at Microsoft (as far as I know).

Some things start from the grassroots with motives as simple as trying
to give developers a better experience than they’ve had before.

**Before**: the original code, complete with helpful comments, original
variable names, etc... was closed. You could use Reflector (and possibly
violate EULAs in the process), but it wasn’t as nice as having the
actual code.

**After:** The source is available to be seen. This is certainly not
more *closed* than before. **It is clearly *better* because you now have
*more choice*.** You can choose to view the code, or chose *not to*.
Before, you only had one choice - no lookie lookie here!

But It’s Not Open Source!
-------------------------

Many pundits have pointed out that this is not Open Source. That is
correct and as far as I can tell, nobody at Microsoft (at least in an
official position) is claiming that.

The Ms-RL is **not** an open source license, so there is reason to be
cautious should you be contributing to the Mono project, or plan to
write a component that is similar to something within the framework. As
Miguel wrote in his post, these precautions have been in place within
the Open Source community for a very long time.

So yes, it’s not open source. But it’s a step in the right direction. As
I’ve written before, we’re [seeing steady
progression](https://haacked.com/archive/2007/07/26/microsoft-and-open-source.aspx "Microsoft and Open Source")
within Microsoft regarding Open Source, albeit with the [occasional
setback](https://haacked.com/archive/2007/05/13/is-fighting-open-source-with-patents-a-smart-move-by.aspx "Is Fighting Open Source with Patents a Smart Move By Microsoft?").

My hope, when I start at Microsoft, is to be involved with that progress
in one form or another as I see it as essential and beneficial to
Microsoft. But I will be patient.

Should You Look At The Code?
----------------------------

So should you look at the source code? Frans Bouma [says
no](http://weblogs.asp.net/fbouma/archive/2007/10/04/don-t-look-at-the-sourcecode-of-net-licensed-under-the-reference-license.aspx "Don’t look at the sourcecode of .NET licensed under the 'Reference License'")!

> Take for example the new ReaderWriterLockSlim class introduced in .NET
> 3.5. It’s in the System.Threading namespace which will be released in
> the pack of sourcecode-you-can-look-at. This class is a replacement
> for the flawed ReaderWriterLock in the current versions of .NET. This
> new lock is based on a patent, which (I’m told) is developed by
> Jeffrey Richter and sold to MS. This new class has its weaknesses as
> well (nothing is perfect). If you want to bend this class to meet your
> particular locking needs by writing a new one based on the ideas in
> that class' sourcecode, you’re liable for a lawsuit as your code is a
> derivative work based on a patented class which is available in
> sourcecode form.

However I think the advice in Miguel’s post addresses this to some
degree.

> If you have a vague recollection of the internals of a Unix program,
> this does not absolutely mean you can’t write an imitation of it, but
> do try to organize the imitation internally along different lines,
> because this is likely to make the details of the Unix version
> irrelevant and dissimilar to your results.

My advice would be to use your head and not veer towards one extreme or
another. If you’re planning to ship a `ReaderWriterLockSlim` class, then
I probably wouldn’t look at their implementation.

But that shouldn’t stop you from looking at code that you have no plans
to rewrite or copy.

And what do you do if you happen to look at the `ReaderWriterLockSlim`
class on accident and were planning to write one for your internal data
entry app? Either have another member of your team write it, or follow
the above advice and implement it along different lines.

> For example, Unix utilities were generally optimized to minimize
> memory use; if you go for speed instead, your program will be very
> different ...
>
> Or, on the contrary, emphasize simplicity instead of speed. For some
> applications, the speed of today’s computers makes simpler algorithms
> adequate.
>
> Or go for generality. For example, Unix programs often have static
> tables or fixed-size strings, which make for arbitrary limits; use
> dynamic allocation instead.

Just don’t copy the existing implementation.

For many developers, their code is never distributed because it is
completely internal, or runs on a web server. In that case, I think the
risk is very low that anyone is going to prove you infringed on a patent
because you happened to look at a piece of code, unless the code is a
very visible UI element.

Please don’t misunderstand me on this point. I’m not recommending you
violate any software patents (even though I think most if not all
[software patents are
dubious](http://www.codinghorror.com/blog/archives/000902.html "The Coming Software Patent Apocalypse")),
I’m just saying the risk of patent taint for many developers who look at
the .NET source code is not as grave as many are making it out to be.
When in doubt, you’d do well to follow the advice in Miguel’s post.

UPDATE: Upon further reflection, I realized there is one particular risk
with what I’ve just said.

In the case of the ReaderWriteLockSlim, I believe the particular
algorithm for high performance is patented. But what if the idea of a
reader write lock in general (one that allows simultaneous reads unless
blocking for a write) was patented.

Then you could get in trouble for implementing a reader write lock even
if you never look at the source code. Patent infringement is a whole
different beast than copyright infringement. This scenario is not so far
fetched and is something Bill Gates has warned against in the past and
has [come to pass many times in the
present](http://www.nytimes.com/2007/06/09/opinion/09lee.html?ex=1339041600&en=a2f3d8f1f3cfcb61&ei=5090&partner=rssuserland&emc=rss "A Patent Lie").

Of course, this risk is present whether or not Microsoft makes the
source available. By using Reflector, for example, you’d have the same
risk of being exposed to patented techniques.

I should point out I’m not a lawyer so follow any of this advice at your
own risk.

Having said that, I think a [follow-up
post](http://weblogs.asp.net/fbouma/archive/2007/10/05/more-on-the-net-sourcecode-and-its-reference-license.aspx "More on the .NET sourcecode and its 'Reference License'")
on Frans’s blog proposes a solution I think Microsoft should jump on to
clear things up. It comes from the JRL (Java Research License).

> The JRL is not a tainting license and includes an express 'residual
> knowledge' clause which says you’re not contaminated by things you
> happen to remember after examining the licensed technology. The JRL
> allows you to use the source code for the purpose of JRL-related
> activities but does not prohibit you from working on an independent
> implementation of the technology afterwards.

It’d be nice if Microsoft added a similar clause to the Ms-RL so much of
this FUD can just go away. Or even better, take the next step and look
at putting this code (at least some of it) under the
[Ms-PL](http://www.microsoft.com/resources/sharedsource/licensingbasics/permissivelicense.mspx "Microsoft Permissive License").

*Disclaimer: Starting on October 15, I will be a Microsoft Employee, but
the opinions expressed in this post are mine and mine only. I do not
speak for Microsoft on these matters.*

*I’m also the leader of a couple OSS projects, so I will be very careful
about separating what I learn on the job vs what I contribute to
Subtext et all.
But I’ll be a PM so I hear I won’t be looking at much code anyways. ;)*

