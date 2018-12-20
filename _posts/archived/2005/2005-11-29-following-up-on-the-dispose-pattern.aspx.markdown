---
title: Following Up On the Dispose Pattern
date: 2005-11-29 -0800
tags: []
redirect_from: "/archive/2005/11/28/following-up-on-the-dispose-pattern.aspx/"
---

Alright. Enough about vacations, it’s time to get back to work, so let’s
dig our teeth into the dispose pattern again. In [a recent
post](https://haacked.com/archive/2005//11/18/ACloserLookAtDisposePattern.aspx),
I wrote up a potential error I saw in the [Framework Design
Guidelines](http://www.amazon.com/gp/product/0321246756/103-9411210-6787060?v=glance&n=283155&n=507846&s=books&v=glance).

In a [follow
up](http://blogs.msdn.com/brada/archive/2005/11/11/492036.aspx#495384)
on Brad Abram’s blog, [Joe Duffy](http://www.bluebytesoftware.com/blog/)
confirms that if they neglected to mention something about chaining a
dispose call to the base class, that would indeed be a book
bug/omission. He then points out how in the typical pattern, not
chaining the dispose call could end up being very bad and difficult to
track down.

To my second question, he points out that in general, it is not a good
idea to call a virtual method from your Finalize method, but they made
an exception for the Dispose pattern because it is carefully controlled.
Virtual calls during destruction are not as dangerous as virtual calls
during construction, which in general is a no no.

So suffice to say, the general (potential) enhancement to the Dispose
pattern I suggested with the additional virtual methods for clarity
won’t introduce any new problems. It’s really a matter of style and a
question of whether or not it makes the code more readable or not. That
is for you to decide.

