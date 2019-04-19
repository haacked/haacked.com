---
title: Possible Bug With DateTime.Parse?
tags: [dotnet]
redirect_from: "/archive/2006/09/30/Possible_Bug_With_DateTime.Parse.aspx/"
---

UPDATE: I think a good measure of a blog is the intelligence and quality
of the comments. This comments in response to this post makes my blog
look good (not [all
do](https://haacked.com/archive/2005/02/20/GetYourPimpName.aspx "Get Your Pimp Name")).

As several commenters pointed out, the function returns a local DateTime
adjusted from the specified UTC date. By calling `ToUniversalTime()` on
the result, I get the behavior I am looking for. That’s why I ask you
smart people before making an ass of myself on the bug report site.

Before I post this as a bug, can anyone tell me why this test fails when
I think it should pass?

```csharp
[Test]
public void ParseUsingAssumingUniversalReturnsDateTimeKindUtc()
{
  IFormatProvider culture = new CultureInfo("en-US", true);
  DateTime utcDate = DateTime.Parse("10/01/2006 19:30", culture, 
    DateTimeStyles.AssumeUniversal);
  Assert.AreEqual(DateTimeKind.Utc, utcDate.Kind, 
    "Expected AssumeUniversal would return a UTC date.");
}
```

What is going on here is I am calling the method `DateTime.Parse`
passing in a `DateTimeStyle.AssumeUniversal` as an argument. My
understanding is that it should indicate to the `Parse` method that the
passed in string denotes a Coordinated Univeral Time (aka UTC).

But when I check the `Kind` property of the resulting `DateTime`
instance, it returns `DatTimeKind.Local` rather than `DatTimeKind.Utc`.

The unit test demonstrates what I think **should** happen. Either this
really is a bug, or I am wrong in my assumptions, in which case I would
like to know, how **are** you supposed to parse a string representing a
date/time in the UTC timezone?

