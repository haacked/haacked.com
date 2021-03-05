---
title: Better String Input Handling
tags: [code,humor]
redirect_from: "/archive/2009/03/31/introducing-stringor.aspx/"
---

I’ve been relatively quiet on my blog lately in part because of all the
work on [ASP.NET MVC](http://asp.net/mvc "ASP.NET Website"). However,
the ASP.NET team is a relatively small team so we often are required to
work on multiple features at the same time. So part of the reason I’ve
been so busy is that while we were wrapping up ASP.NET MVC, I was also
busy working on a core .NET Framework feature we plan to get into the
next version (*it was a feature that originated with our team, but we
realized it belongs in the BCL*).

The goal of the feature is to help deal with the very common task of
handling string input. In many cases, the point is to convert the input
into another type, such as an `int` or `float`. However, how do you deal
with the fact that the string might not be convertible to the other
type.

We realized we needed a type to handle this situation. A type that would
represent the situation after the user has submitted input, but before
you attempt the conversion. At this point, you have a string or another
type.

![clip\_image004\_thumb](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/HandlingStringInput_12658/clip_image004_thumb_3.jpg "clip_image004_thumb")

For more details on the `StringOr<T>` Community Technology Preview
(CTP), please see details on lead developer [Eilon Lipton's
Blog](http://weblogs.asp.net/leftslipper/archive/2009/04/01/the-string-or-the-cat-a-new-net-framework-library.aspx "StringOr")
(*he’s a big fan of cats as you can see*). He provides source code and
unit tests for download. As always, please do provide feedback as your
feedback is extremely important in helping shape this nascent
technology.

### Related Blog Posts

-   [The String or the Cat (original source) - Eilon
    Lipton](http://weblogs.asp.net/leftslipper/archive/2009/04/01/the-string-or-the-cat-a-new-net-framework-library.aspx#7020406 "StringOr")
-   [.NET 4.1 Preview -
    Hanselman](http://www.hanselman.com/blog/NET41PreviewNewBaseClassLibraryBCLExtensionMethodsRFC.aspx ".NET 4.1 Preview - New Base Class Library (BCL) Extension Methods - RFC ")
-   [Subsonic Extensions Methods for StringOr&lt;T&gt; - Rob
    Conery](http://blog.wekeroad.com/blog/cool-extension-methods-for-new-stringor/ "Cool Extensions Methods")
-   [String Input Handling + Quantum Mechanics - Jonathan
    Carter](http://lostintangent.com/2009/04/01/string-input-handling-quantum-mechanics/ "String Input Handling")
-   [Quantum Computing in .NET
    “Looflirpa”](http://blog.andrewnurse.net/2009/04/01/QuantumComputingInNetLdquoLooflirpardquo.aspx "Quantum Computing in .NET Looflirpa")

