---
title: AddressInfo Update
tags: [code]
redirect_from: "/archive/2005/08/04/addressinfo-update.aspx/"
---

A long time ago, in a galaxy far away, I wrote a really simple little
class for converting [State Codes to State Names and Vice Versa](https://haacked.com/archive/2005/04/08/2599.aspx).

Essentially, this class contained two enums, one for state codes such as `AK` and `CA`. Another enum contained state names such as `Alaska` and `California`. There were static methods that facilitated converting between the the two as well as string representations.

Simple stuff really, but very helpful if you deal with states all the time. However, just today I received an email from
[Omer](http://weblogs.asp.net/OKloeten/) pointing out that I am trusting the order of the two enums values to be aligned to allow conversions between the two. While it happens to work, it creates a dependency on the order of the values that doesn’t need to be there. You never know when we’ll annex Iraq as our 51st state and need to add a value to the enums.

In any case, I took ~~Omar’s~~ Omer’s suggestion to have one of the enums refer to the other. For example, here’s a snippet of the `StateCode` enum.

```csharp
public enum StateCode
{
  /// <summary\>Alabama</summary\>
  AL = State.Alabama,
  /// <summary\>Alaska\</summary\>
  AK = State.Alaska,
  ///... and so on
}
```

The [code is available on GitHub](https://github.com/Haacked/CodeHaacks/blob/master/src/AddressInfo.cs).
