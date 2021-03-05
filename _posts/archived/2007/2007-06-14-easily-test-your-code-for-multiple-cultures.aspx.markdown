---
title: Easily Test Your Code For Multiple Cultures
tags: [code,tdd]
redirect_from: "/archive/2007/06/13/easily-test-your-code-for-multiple-cultures.aspx/"
---

[![Globe from the
stock.xchng](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/EasilyTestYourCodeForMultipleCultures_1390E/439027_around_the_world_5_1.jpg)](http://www.sxc.hu/photo/439027 "Photo from the stock.xchng")
Most of the time when I’m testing my code, I only test it using the
`en-US` culture since, ...well..., I speak English and I live in the
U.S. Isn’t the U.S. the only country that matters anyway? ;)

Fortunately, there are [Subtext](http://subtextproject.com/ "Subtext")
team members living in other countries ready to smack such nonsensical
thoughts from my head and keep me honest about Localization and
Internationalization issues.

[Simone](http://www.codeclimber.net.nz/ "CodeClimber - Simo's English Blog"),
who is an Italian living in New Zealand, pointed out that a particular
unit test that [works on my
machine](http://www.codinghorror.com/blog/archives/000818.html "Works on my machine certification program")
always fails on his machine. Here’s the test.

```csharp
[RowTest]
[Row("4/12/2006", "04/12/2006 00:00:00 AM")]
[Row("20070123T120102", "01/23/2007 12:01:02 PM")]
[Row("12 Apr 2006 06:59:33 GMT", "04/12/2006 06:59:33 AM")]
[Row("Wed, 12 Apr 2006 06:59:33 GMT", "04/12/2006 06:59:33 AM")]
public void CanParseUnknownFormatUTC(string received, string expected)
{
  DateTime expectedDate = DateTimeHelper.ParseUnknownFormatUTC(received);
  Assert.AreEqual(expected, expectedDate.ToString("MM/dd/yyyy HH:mm:ss tt"));
}
```

The method being tested simply takes in a date string in an unknown
format and performs a few heuristics in order to parse the date.

The way I test this method is very U.S. centric. I call `ToString()` and
then match it to the expected string defined in the `Row` attributes (I
can’t use actual `DateTime` values in the attributes).

So for the very first row, I expect that date to match *04/12/2006
00:00:00 AM*. But when Simo runs the test over there in New Zealand, he
gets*12/04/2006 00:00:00 a.m.*

Makes you wonder how anyone over there can keep an appointment with the
month and date all backwards like that. ;)

**Testing In Another Culture**

At this point, I start thinking of convincing my wife to take a vacation
in New Zealand so I can test this method properly. Hmmm... that’s
probably not going to fly, with the newborn and all.

Another option is to go into my regional settings and change my locale
to test temporarily, but that sort of defeats the purpose of automated
tests once I change it back. What to do?

**MbUnit to the rescue!**

Once again, I discover a feature I hadn’t known about in
[MbUnit](http://mbunit.com/ "MbUnit Generative Unit Test Framework")
that solves this problem (*Jeff and Jon, feel free to snicker*).

Looking at the MbUnit [TestDecorators
page](http://www.mertner.com/confluence/display/MbUnit/TestDecorators "MbUnit Test Decorators"),
I noticed there is a [MultipleCultureAttribute] decorator! Hmmm, I bet
that could end up being useful.

Unfortunately, at the time, this decorator was not documented (I’ve
since documented it), so I looked up [the code on
Koders](http://www.koders.com/csharp/fidF9B0E8E7D2CAE89E7844833CD37A3017F2B5FE56.aspx "MultipleCultureAttribute on Koders.com")
real quick to see the documentation and saw that I simply need to pass
in a comma delimited string of cultures. **This allows me to run a
single test multiple times, once for each culture listed.**

Here is the updated test with my code correction.

```csharp
[RowTest]
[Row("4/12/2006", "04/12/2006 00:00:00 AM")]
[Row("20070123T120102", "01/23/2007 12:01:02 PM")]
[Row("12 Apr 2006 06:59:33 GMT", "04/12/2006 06:59:33 AM")]
[Row("Wed, 12 Apr 2006 06:59:33 GMT", "04/12/2006 06:59:33 AM")]
[MultipleCulture("en-US,en-NZ,it-IT")]
public void CanParseUnknownFormatUTC(string received, string expected)
{
  DateTime expectedDate = DateTimeHelper.ParseUnknownFormatUTC(received);
  Assert.AreEqual(DateTime.ParseExact(expected
    , "MM/dd/yyyy HH:mm:ss tt"
    , new CultureInfo("en-US")), expectedDate);
}
```

*One cool note about how decorators like this work in MbUnit is the way
it composes with the `RowTest`’s `Row` attributes. For example, in the
above test, the test method will get called once per culture per Row for
a grand total of 12 times.*

So now my friends in faraway places will have the pleasure of unit tests
that pass in their respective locales and I can feel like a better
citizen of the world.

