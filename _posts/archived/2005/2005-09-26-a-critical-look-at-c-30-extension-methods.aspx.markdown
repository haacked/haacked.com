---
title: A Critical Look at C# 3.0 Extension Methods
date: 2005-09-26 -0800
tags: [csharp]
redirect_from: "/archive/2005/09/25/a-critical-look-at-c-30-extension-methods.aspx/"
---

[Ian Griffiths](http://www.interact-sw.co.uk/iangblog/) takes [an
in-depth look at C# 3.0 Extension
methods](http://www.interact-sw.co.uk/iangblog/2005/09/26/extensionmethods)
and the potential problems with it. Of particular note is his
philosophy, which directly follows from the idea that code should be
written for humans, which he summarizes whe he say...

> I’m a big fan of code that does what it looks like it does.

Amen brother!

As an example, he highlights the `ToUpper` method on a `System.String`
instance, which often misleads new developers. He would prefer the more
honest and less misleading static method on the String class that would
be called like so:

`String.ToUpper(input);`

I agree wholeheartedly that ToUpper (which sort of follows the Java
convention I guess) is misnamed, but (and this really is a minor niggle)
I probably would prefer that it still be an instance method, but renamed
`GetUpperCase`. I think that would do a good enough job of being honest
and being discoverable.

In any case, if you’re interested in C# 3.0, be sure to read Ian’s take
on extension methods.

