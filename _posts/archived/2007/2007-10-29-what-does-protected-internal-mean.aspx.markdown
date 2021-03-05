---
title: What Does Protected Internal Mean?
tags: [dotnet,code]
redirect_from: "/archive/2007/10/28/what-does-protected-internal-mean.aspx/"
---

Pop quiz for you C# developers out there. Will the following code
compile?

```csharp
//In Foo.dll
public class Kitty
{
  protected internal virtual void MakeSomeNoise()
  {
    Console.WriteLine("I'm in ur serverz fixing things...");
  }
}

//In Bar.dll
public class Lion : Kitty
{
  protected override void MakeSomeNoise()
  {
    Console.WriteLine("LOL!");
  }
}
```

If you had asked me that yesterday, I would have said hell no. You can’t
override an internal method in another assembly.

Of course, I would have been WRONG!

Well the truth of the matter is, I **was** wrong. This came up in an
internal discussion in which I was unfairly complaining that certain
methods I needed to override were internal. In fact, they were
`protected internal`. Doesn’t that mean that the method is both
`protected` *and* `internal`?

Had I simply tried to override them, I would have learned that my
assumption was wrong. For the record...

**`protected internal` means `protected` *OR* `internal`**

It’s very clear when you think of the keywords as the union of
accessibility rather than the intersection. Thus `protected internal`
means the method is accessible by anything that can access the
`protected` method UNION with anything that can access the `internal`
method.

[![A Donkey Named Lester - Creative Commons By Attribution -
ninjapoodles](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WhatDoesProtectedInternalMean_1354B/donkey-named-lester_3.jpg)](http://www.flickr.com/photos/ninjapoodles/136704951/ "A Donkey Named Lester - Creative Commons By Attribution - ninjapoodles")As
the old saying goes, when you assume, you make an *ass* out of *u* and
*me*. I never understood this saying because when I assume, I only make
an ass of me. I really think the word should simply be **assme**. As
in... 

> Never assme something won’t work without at least trying it.

UPDATE: [Eilon](http://weblogs.asp.net/leftslipper/ "Eilon Lipton"),
sent me an email to point out that...

> BTW the CLR does have the notion of `ProtectedANDInternal`, but C#
> has no syntax to specify it. If you look at the CLR’s
> `System.Reflection.MethodAttributes` enum you’ll see both
> `FamANDAssem` as well as `FamORAssem` (“Family” is the CLR’s term for
> C#’s `protected` and “Assem” is C#’s `internal`).

If you don’t know Eilon, he’s a freaking sharp developer I get to work
with on the MVC project and was the one who kindly disabused me of my
ignorance on this subject. He keeps a blog at
[http://weblogs.asp.net/leftslipper/](http://weblogs.asp.net/leftslipper/ "Left Slipper").

Apparently he’s the one with the clever idea of using a [C# 3.0
anonymous
type](http://weblogs.asp.net/leftslipper/archive/2007/09/24/using-c-3-0-anonymous-types-as-dictionaries.aspx "Using C# 3.0 Anonymous Types as Dictionaries")
as a dictionary, that many of you saw in ScottGu’s ALT.NET Conference
talk. Very cool.

