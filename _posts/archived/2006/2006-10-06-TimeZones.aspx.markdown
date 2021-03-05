---
title: TimeZones
tags: [dotnet]
redirect_from: "/archive/2006/10/05/TimeZones.aspx/"
---

[![TimeZones](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TimeZones_105BD/timezone_thumb.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TimeZones_105BD/timezone2.jpg)
Right now, there is no easy way to convert a time from one arbitrary
timezone to another arbitrary timezone in .NET.  Certainly you can
convert from UTC to the local system time, or from the local system time
to UTC. But how do you convert from PST to EST?

Well [Scott Hanselman](http://www.hanselman.com/blog/ "Computer Zen")
recently pointed me to some ingenious code in
[DasBlog](http://dasblog.net/ "DasBlog") originally written by [Clemens
Vasters](http://staff.newtelligence.net/clemensv/ "Clemens Vasters' blog") that
does this.  I recently submitted a patch to DasBlog so that this code
properly handles daylight savings and I had planned to blog about it in
more detail later.  Unfortunately, we recently found out that changes in
Vista may break this particular approach.

It turns out that the Orcas release introduces a new [`TimeZone2`
class](http://blogs.msdn.com/bclteam/archive/2006/10/03/System.TimeZone2-Starter-Guide-_5B00_Kathy-Kam_5D00_.aspx "TimeZone2 Starter Guide"). 
This class will finally allow conversions between arbitrary timezones.

[Krzysztof
Cwalina](http://blogs.msdn.com/kcwalina/ "Krzysztof Cwalina's Blog")
(who wins the award for Microsoft blogger with the highest consonants to
vowel ration in a first name) points out that many people are not
thrilled with the "2" suffix and [provides context on the naming
choice](http://blogs.msdn.com/kcwalina/archive/2006/10/06/TimeZone2Naming.aspx "Naming TimeZone2"). 

Kathy Kam of the BCL team points out [some other proposed
names](http://blogs.msdn.com/kathykam/archive/2006/10/06/Naming-Guideline-Discussion.aspx "Naming Guideline")
for the new `TimeZone2` class and the problems with each.

I’m fine with `TimeZone2` or `TimeZoneRegion`.

 

