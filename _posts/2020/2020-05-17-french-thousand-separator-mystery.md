---
title: "Mystery of The French Thousands Separator "
description: "A detective story involving a failing unit test. A test that passes the build, but fails on my machine. And how the Unicode Consortium is at the center of it all."
tags: [dotnet,csharp,testing]
excerpt_image: https://user-images.githubusercontent.com/19977/82157531-dce6f180-9836-11ea-9a33-6cc5069e339f.png
---

I enjoy writing silly chat bots. To indulge my silliness, I've been exploring the Microsoft Bot Framework. Overall, it's a pretty good framework, but I've had some weird bugs here and there. In order to dig into them, I cloned the [microsoft/botbuilder-dotnet](https://github.com/microsoft/botbuilder-dotnet/) to my machine and ran all the unit tests. It's what I do.

One of the tests failed with the message:

> Assert.AreEqual failed. Expected:<12 000,3000>. Actual:<12 000,3000>.

Can you spot the difference?

It's a little hard to see, let's write some code to take a closer look. I've [posted the code on dotnetfiddle](https://dotnetfiddle.net/YUGELH) if you want to play with it.

```csharp
using System;
using System.Linq;
using System.Globalization;
					
public class Program
{
	public static void Main()
	{
        char nbsp = (Char)160;
		var expected = string.Join(",",
			$"12 000,3000"
			.Select(c => (int)c));
		var actual = string.Join(",",
			"12 000,3000"
			.Select(c => (int)c));
Console.WriteLine($"12{nbsp}000,3000");
		Console.WriteLine(expected);
		Console.WriteLine(actual);
	}
}
```

This results in:

> 49,50,160,48,48,48,44,51,48,48,48
> 49,50,8239,48,48,48,44,51,48,48,48

Would you look at that?!

The third character is different! In the `expected` string it's value is `160` which translates to `U+00A0` in unicode, or what we would know as the `nbsp` (aka the [`NO-BREAK SPACE`](https://www.fileformat.info/info/unicode/char/00a0/index.htm)).

But on my machine, I get `8239` there, which is `U+202F` which is the lesser known cousin of `nbsp`, the `nnbsp` (aka [`NARROW NO-BREAK SPACE`](https://www.fileformat.info/info/unicode/char/202f/index.htm)).

I dug into it a little more and the code that's being tested is formatting a number for the `fr-FR` locale. The space character there is determined by the [`NumberFormatInfo.CurrencyGroupSeparator` property](https://docs.microsoft.com/en-us/dotnet/api/system.globalization.numberformatinfo.currencygroupseparator?view=netcore-3.1) for the locale.

![A French beret](https://user-images.githubusercontent.com/19977/82157531-dce6f180-9836-11ea-9a33-6cc5069e339f.png "I'm unimaginative so a French beret is the best I could do for representing the French locale")

So I wrote a little code to test this out. Again, [it's on dotnetfiddle](https://dotnetfiddle.net/YDLsGS).

```csharp
using System;
using System.Globalization;
					
public class Program
{
	public static void Main()
	{
		var separator = new CultureInfo("fr-FR", false)
			.NumberFormat
			.CurrencyGroupSeparator;
		
		Console.WriteLine(
			$"Currency Group Separator for fr-FR is {(int)separator[0]}");
		Console.WriteLine(
			$"As HEX {((int)separator[0]).ToString("X")}");
	}
}
```

Important note, I made sure to call the [`CultureInfo` constructor](https://docs.microsoft.com/en-us/dotnet/api/system.globalization.cultureinfo.-ctor?view=netcore-3.1#System_Globalization_CultureInfo__ctor_System_Int32_System_Boolean_) that lets us ignore user-selected culture settings from the system. Otherwise this test might be flaky for those who have customized settings on their machine.

The result on my machine and in dotnetfiddle is:

> Currency Group Separator for fr-FR is 8239
> As HEX 202F

To be nice, I thought I'd fix the test and submit a PR. It's the scouting rule. However, my PR failed the build. On the projects machines, that group separator is `160`. What gives? So I dug into it more and discovered the [Unicode CLDR project](http://cldr.unicode.org/).

CLDR does not stand for "Certainly Long, Didn't Read." Rather, it's the Unicode Common Locale Data Repository. It's a project by the Unicode Consortium to provide locale data in an XML format.

> The Unicode CLDR provides key building blocks for software to support the world's languages, with the largest and most extensive standard repository of locale data available.

It turns out that sometime in October 2018, the Unicode Consortium changed the thousands separator character for the French locale.

You can read it in their [CLDR 34 release notes](http://cldr.unicode.org/index/downloads/cldr-34). And yes, it's a bit TL;DR so I'll quote the relevant section.

> The French locale now uses narrow no-break space U+202F is [sic] several places: as the numeric grouping separator, in many short unit patterns, and in the locale display name patterns. It also changed normal space to no-break space U+00A0 in the wide unit patterns.

According to this [JavaMoney issue](https://github.com/JavaMoney/jsr354-ri/issues/193#issuecomment-457941385)...

> And even more: they have some intentions to change this for other locales:

Unfortunately, all the TRAC links are broken so I couldn't follow up to verify, but it seems reasonable.

So what does this all mean? Programming is hard. And programming for multiple locales is even harder. Be safe out there.