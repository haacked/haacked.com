---
layout: post
title: "Making The Factory Pattern More Discoverable"
date: 2005-06-28 -0800
comments: true
disqus_identifier: 7346
categories: []
---
[Steven Clarke](http://blogs.msdn.com/stevencl/) has [an interesting
post](http://blogs.msdn.com/stevencl/archive/2005/06/21/431230.aspx)
about the usability (or lack thereof) of the Factory Pattern.

In simple terms, the usability issue strikes when a developer knows she
needs an instance of object `Foo`. So she tries to new one up like so...

Foo foo = new Foo();

Unfortunately Foo looks like this...

public class Foo

{

ÂÂÂÂprivate Foo() {}

}

Notice the private constructor? VS.NETâ€™s intellisense dutifully tells
her that she canâ€™t create an instance of Foo in this way. So now how
is she supposed to create her beloved Foo? The answer is that thereâ€™s
probably a `FooFactory` laying around somewhere thatâ€™ll do just that
for her. So now she has to go rooting around looking for that class, her
rhythm and flow being disturbed in the process.

So is the answer to simply throw out the Factory pattern? Dear god no!
This is one of those cases where perhaps the IDE could be a bit more
helpful. Imagine if we could markup the class like so...

public class Foo

{

ÂÂÂÂ/// \<summary\>

ÂÂÂÂ/// Try using the FooFactory to create this class.

ÂÂÂÂ/// \</summary\>

ÂÂÂÂprivate Foo() {}

}

And that comment would show up when trying to directly create an
instance of Foo. Wouldnâ€™t that be wonderful? Or for you attribute
lovers, maybe an attribute would be a better option.

[Factory(typeof(FooFactory))]

public class Foo

{

ÂÂÂÂprivate Foo() {}

}

Either way, the goal is to give the forlorn developer some help via
Intellisense. All that API creators need to do is to add a bit of
information to their classes and voila! Intellisense to the rescue.
Youâ€™ve rescued the usability of the factory pattern.

[Listening to: Victorious - TiÃ«sto - Parade of the Athletes (4:38)]

