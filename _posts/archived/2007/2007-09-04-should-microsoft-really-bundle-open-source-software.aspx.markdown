---
title: Should Microsoft Really Bundle Open Source Software?
tags: [oss,microsoft]
redirect_from: "/archive/2007/09/03/should-microsoft-really-bundle-open-source-software.aspx/"
---

[Ayende](http://www.ayende.com/ "Ayende") recently wrote about
Microsoft’s “annoying” tendency [to duplicate the
efforts](http://www.ayende.com/Blog/archive/2007/09/01/Duplication-of-Efforts.aspx "Duplication of Efforts") of
perfectly capable Open Source Software already in existence. In the
post, he references [this post by Scott
Bellware](http://codebetter.com/blogs/scott.bellware/archive/2007/08/31/167354.aspx "How Long Before Microsoft Releases a Mock Object Framework") which
lists several cases in which Microsoft duplicated the efforts of OSS
software.

Fear Factor
-----------

Ayende is not convinced by the fear factor argument around issues of
software pedigree, patents, and legal challenges. [Jon
Galloway](http://weblogs.asp.net/jgalloway/ "Jon Galloway’s Blog") wrote
about this argument a while ago in his post *[Why Microsoft can’t ship
open source
code](http://weblogs.asp.net/jgalloway/archive/2007/05/02/why-microsoft-can-t-ship-open-source-code.aspx "Why Microsoft can’t ship open source code")*.

In his post, Ayende dismisses this argument as “lawyer-paranoia”. While
I agree to some extent that it is paranoia, not all paranoia is bad. I
think this point bears more thoughtful responses than simply dismissing
it as FUD.

Microsoft really is a huge fat target with a gigantic bullseye on its
forehead in the form of lots and lots of money. At that size, the rules
of engagement changes when compared to smaller companies.

Nobody is going after small fries who release open source code such as
Ayende or myself. But as soon a big fry like Microsoft starts bundling
open source code, watch out for the armies of patent trolls, lawyers in
tow, coming out of the woodwork.

[![notld1](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ShouldMicrosoftReallyBundleOpenSourceSof_807C/notld1_thumb.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ShouldMicrosoftReallyBundleOpenSourceSof_807C/notld1.jpg)

As an aside, some commenters mention the “commercial friendliness” of
the licenses of the projects they would like bundled such as
the [BSD](http://www.opensource.org/licenses/bsd-license.php "BSD License")
and
[MIT](http://www.opensource.org/licenses/mit-license.php "MIT License") licenses.
However, as far as I know, none of these licenses have any patent
protection in the same way that the
[GPL](http://www.gnu.org/licenses/gpl.html "GPL License") does. Perhaps
Microsoft should require bundled OSS software to be licensed with the
GPL. I kid! I kid! We’d probably see Steve Ballmer grooving to an IPod
in a pink leotard before that happens.

Back to the point at hand. Having said all that, while I think this is a
difficult challenge, I don’t think it is an insurmountable
challenge. Microsoft can afford an army of lawyers and hopefully some of
them are extremely bright and can come up with creative solutions that
might allow Microsoft to leverage and even bundle Open Source software
in a safe manner. After all, they already face the same risk by allowing
*any* employee to write and ship code. Employees are not immune to
lapses of judgement.

We already see progress happening in regards to Microsoft and Open
Source. The
[IronRuby](http://www.iunknown.com/2007/04/introducing_iro.html "Introducing IronRuby")
project [will accept source code
contributions](http://www.iunknown.com/2007/07/a-first-look-at.html "A First Look At IronRuby"),
but most likely with some strict limitations and with required paperwork
like the Free Software Foundation does. Progress can be made on this
front, but it won’t happen overnight.

## How Should They Choose?

For the sake of argument, suppose that Microsoft deals with all the
legal issues and does decide to start bundling OSS software. How should
they choose *which* software to bundle?

For mock object frameworks, Scott Bellware mentions [Rhino
Mocks](http://ayende.com/projects/rhino-mocks.aspx "Rhino Mocks"), a
mock framework I’ve [written about a few times and would agree with this choice. But what about [NMock](http://nmock.org/ "NMock") which has been around longer as
far as I know. I think Scott and Ayende would both agree that popularity
or seniority should not trump technical quality in choosing which
project to bundle. I personally would choose Rhino Mocks over NMock any
day of the week.

Bellware’s post also lists
[NUnit](http://nunit.com/ "NUnit Test Framework"). While NUnit has been
around longer than
[MbUnit](http://mbunit.com/ "MbUnit generative test framework"), in my
opinion I think it is pretty clear that MbUnit is technically a much
better testing framework. Naturally, I’m sure there are many fans of
NUnit who would disagree vehemently. Therein lies the conflict. No
matter which framework Microsoft chooses, there will be many who are
unhappy with the choice.

If Microsoft had chosen to not write its own test framework, I fear they
would have chosen NUnit over MbUnit simply because it’s more well known
or for political reasons. Such a choice would have the potential to hurt
a project like MbUnit in the never ending competition for users and
contributors.

The fact that the MS Test sucks so much is, in a way, a boon to NUnit
and MbUnit. Please understand I’m not saying that “Because choosing a
project is hard, it shouldn’t or can’t be done”. I’m merely suggesting
that if we’re clamoring for Microsoft to start bundling instead of
duplicating, we ought to offer ideas on how that should happen and be
prepared for the ramifications of such choices.

So what do I think they should do?
----------------------------------

Let’s look at one situation in particular that appears to be an annoying
duplication of efforts. A while back, Microsoft identified a business
opportunity to create an integrated development IDE suite which included
code coverage, bug tracking, unit testing, etc... They came out with
Team System which included a unit testing framework that wasn’t even
near par with NUnit or MbUnit.

This is a situation in which many have argued that Microsoft should have
bundled NUnit with Team System rather than writing their own.

While we can continue to argue the merits of whether Microsoft should or
shouldn’t bundle Open Source software, the official stance currently
appears to be that it is too much of a liability to do so. So rather
than keep arguing that point, let’s take a step back and for the sake of
argument, accept it as a given.

*So given that Microsoft couldn’t bundle NUnit, what should have they
done?*

**They should have given developers a choice.**

What I would have liked to have seen is for Team System to provide
extensibility points which make it extremely easy to swap out MS Test
for another testing framework. MS Test isn’t the money maker for
Microsoft, it’s the whole integrated suite that brings in the moolah, so
being able to replace it doesn’t hurt the bottom line.

Given the inability to bundle NUnit, I can understand why Microsoft
would write their own test framework. They wanted a *complete*
integrated suite. It wouldn’t work to ship something without a test
framework so they provided a barely adequate one. Fine. But why not
allow me to switch that out with MbUnit and still have the full
non-degraded integrated experience?

Microsoft could have then worked with the OSS communities to provide
information and maybe even some assistance with integrating with Team
System.

This is not unprecedented by any means. It’s very similar to how
Microsoft cooperates with control vendors who build WinForms and ASP.NET
widgets and controls.

Microsoft doesn’t provide a GridView, tells us developers that’s all
we’ll ever need for displaying data, and then closes the door on other
control vendors who might want to provide developers with an alternative
grid control. Hell no.

Instead, they make it easy for control vendors to provide their own
controls and have a first-class integrated experience (with design time
support etc...) within the Visual Studio IDE because they recognize they
don’t have the bandwidth to build *everything* top shelf. This sort of
forward thinking should apply anytime they plan to ship a crappy stopgap
implementation.
