---
layout: post
title: Collection Initializers
date: 2008-01-06 -0800
comments: true
disqus_identifier: 18446
categories: [csharp initializers]
redirect_from: "/archive/2008/01/05/collection-initializers.aspx/"
---

File this in my *learn something new every day* bucket. I received an
email from [Steve Maine](http://hyperthink.net/blog/ "Brain.Save()")
after he read a blog post in which I discuss the anonymous object as
dictionary trick that [Eilon came up
with](http://weblogs.asp.net/leftslipper/archive/2007/09/24/using-c-3-0-anonymous-types-as-dictionaries.aspx "Anonymous types as Dictionary").

He mentioned that there is an object initializer syntax for collections
and dictionaries.

```csharp
var foo = new Dictionary<string, string>()
{
  { "key1", "value1" },
  { "key2", "value2" }
};
```

That’s pretty nice!

Here is a post by [Mads Torgersen about collections and collection
initializers](http://blogs.msdn.com/madst/archive/2006/10/10/What-is-a-collection_3F00_.aspx "What is a collection").

Naturally someone will mention that the Ruby syntax is even cleaner (I
know, because I was about to say that).

However, suppose C\# introduced such syntax:

```csharp
var foo = {"key1" => "value1", 2 => 3};
```

What should be the default type for the key and values? Should this
always produce `Dictionary<object, object>`? Or should it attempt type
inference for the best match? Or should there be a funky new syntax for
specifying types?

```csharp
var foo = <string, object> {"key1" => "value1", "2" => 3};
```

My vote would be for type inference. If the inferred type is not the one
you want, then you have to resort to the full declaration.

Then again, the initializer syntax that *does* exist is much better than
the old way of doing it, so I’m happy with it for now. In working with a
statically typed language, I don’t expect that all idioms for dynamic
languages will translate over in as terse a form.

Check out this [use of lambda
expressions](http://blog.bittercoder.com/PermaLink,guid,d1831805-dbf7-4b74-a6fd-2e9ed437c3d9.aspx "Mucking About with Hashes")
by Alex Henderson to create a hash that is very similar in style to what
ruby hashes look like. Thanks Sergio for pointing that out in the
comments.

