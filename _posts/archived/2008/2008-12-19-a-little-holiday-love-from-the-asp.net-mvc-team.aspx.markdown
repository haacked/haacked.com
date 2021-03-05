---
title: A Little Holiday Love From The ASP.NET MVC Team
tags: [aspnet,aspnetmvc]
redirect_from: "/archive/2008/12/18/a-little-holiday-love-from-the-asp.net-mvc-team.aspx/"
---

A while ago [ScottGu](http://weblogs.asp.net/scottgu/ "Scott Guthrie")
mentioned in his blog that we would try and have an [ASP.NET
MVC](http://asp.net/mvc "ASP.NET MVC Website") Release Candidate by the
end of this year. My team worked very hard at it, but due to various
unforeseeable circumstances, I’m afraid that’s not gonna happen. Heck, I
couldn’t even get into the office yesterday because the massive dumping
of snow. I hope to get in today a little later since I’m taking next
week off to be with my family coming in from Alaska.

But do not fret, we’ll have something early next year to release. In the
meanwhile, we gave some rough bits to Scott Guthrie to play with and he
did not disappoint yet again with a a [great detailed
post](http://weblogs.asp.net/scottgu/archive/2008/12/19/asp-net-mvc-design-gallery-and-upcoming-view-improvements-with-the-asp-net-mvc-release-candidate.aspx "ASP.NET MVC Release Candidate")
on some upcoming new features both in the runtime and in tooling.

[![look-ma-no-code-behind](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ALittleHolidayLoveFromTheASP.NETMVCTeam_8267/look-ma-no-code-behind_thumb_1.png "look-ma-no-code-behind")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ALittleHolidayLoveFromTheASP.NETMVCTeam_8267/look-ma-no-code-behind_4.png)
Often, it’s the little touches that get me excited, and I want to call
one of those out, **Views without Code-Behind**. This was always
possible, of course, but due to some incredible hacking down in the
plumbings of ASP.NET , [David
Ebbo](http://blogs.msdn.com/davidebb/ "David Ebbo") with some assistance
from [Eilon Lipton](http://weblogs.asp.net/leftslipper/ "Eilon Lipton"),
figured out how to get it to work with generic types using your language
specific syntax for generic types. If you recall, previously it required
using that complex [CLR Syntax within the Inherits
attribute](http://devlicio.us/blogs/tim_barcz/archive/2008/08/13/strongly-typed-viewdata-without-a-codebehind.aspx "Strongly Typed ViewData Without A Codebehind").

Keep in mind that they had to do this without touching the core ASP.NET
bits since ASP.NET MVC is a bin-deployable out-of-band release. David
provided some hints on how they did this within his blog in these two
posts: [`ProcessGeneratedCode`: A hidden gem for Control Builder
writers](http://blogs.msdn.com/davidebb/archive/2008/11/19/a-hidden-gem-for-control-builder-writers.aspx "ProcessGeneratedCode for Control Builders")
and [Creating a ControlBuilder for the page
itself](http://blogs.msdn.com/davidebb/archive/2008/11/20/creating-a-controlbuilder-for-the-page-itself.aspx "Creating a ControlBuilder for the page itself").

 
-

ASP.NET MVC Design Gallery
--------------------------

In the meanwhile, [Stephen
Walther](http://weblogs.asp.net/StephenWalther/ "Stephen Walther") has
been busy putting together a fun new section of the ASP.NET MVC website
called the [ASP.NET MVC Design
Gallery](http://www.asp.net/mvc/gallery/ "ASP.NET MVC Design Gallery").

In one of my talks about ASP.NET MVC, I mentioned a site that went live
with our default drab blue project template (*the site has since
redesigned its look entirely*). The Design Gallery provides a place for
the community to upload alternative designs for our default template.

Happy Holidays
--------------

[![Cody](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ALittleHolidayLoveFromTheASP.NETMVCTeam_8267/PIC-0092%20(1)_thumb.jpg "Cody")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ALittleHolidayLoveFromTheASP.NETMVCTeam_8267/PIC-0092%20(1)_2.jpg)
Again, I apologize that we don’t have updated bits for you to play with,
but we’ll have something next year! In the meanwhile, as the end of this
year winds down, I hope it was a good one for you. It certainly was a
good one for me. Enjoy your holidays, have a Merry Christmas, Hannukah,
Kwanza, Winter Solstice, Over Consumption Day, whatever it is you
celebrate or don’t celebrate. ;)

