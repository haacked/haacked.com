---
title: Making The Factory Pattern More Discoverable
date: 2005-06-28 -0800
tags: [software,design]
redirect_from: "/archive/2005/06/27/making-the-factory-pattern-more-discoverable.aspx/"
---

[Steven Clarke](http://blogs.msdn.com/stevencl/) has [an interesting
post](http://blogs.msdn.com/stevencl/archive/2005/06/21/431230.aspx)
about the usability (or lack thereof) of the Factory Pattern.

In simple terms, the usability issue strikes when a developer knows she
needs an instance of object `Foo`. So she tries to new one up like so...

```csharp
Foo foo = new Foo();
```

Unfortunately Foo looks like this...

```csharp
public class Foo
{
  private Foo() {}
}
```

Notice the private constructor? VS.NETs intellisense dutifully tells
her that she cant create an instance of Foo in this way. So now how
is she supposed to create her beloved Foo? The answer is that theres
probably a `FooFactory` laying around somewhere thatll do just that
for her. So now she has to go rooting around looking for that class, her
rhythm and flow being disturbed in the process.

So is the answer to simply throw out the Factory pattern? Dear god no!
This is one of those cases where perhaps the IDE could be a bit more
helpful. Imagine if we could markup the class like so...

```csharp
public class Foo
{
    /// <summary >
    /// Try using the FooFactory to create this class.
    /// </summary >
    private Foo() {}
}
```

And that comment would show up when trying to directly create an
instance of Foo. Wouldnt that be wonderful? Or for you attribute
lovers, maybe an attribute would be a better option.

```csharp
[Factory(typeof(FooFactory))]
public class Foo
{
    private Foo() {}
}
```

Either way, the goal is to give the forlorn developer some help via
Intellisense. All that API creators need to do is to add a bit of
information to their classes and voila! Intellisense to the rescue.
You've rescued the usability of the factory pattern.
