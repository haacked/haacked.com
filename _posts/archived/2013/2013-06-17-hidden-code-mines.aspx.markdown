---
title: Hidden Code Mines
date: 2013-06-17 -0800
tags:
- code
redirect_from: "/archive/2013/06/16/hidden-code-mines.aspx/"
---

Code is unforgiving. As the reasonable human beings that we are, when we
review code we both know what the author intends. But computers can’t
wait to [Well,
Actually](http://tirania.org/blog/archive/2011/Feb-17.html "Well, Actually")
all over that code like a lonely Hacker News commenter:

> Well Actually, Dave. I'm afraid I can’t do that.
>
> *Hal, paraphrased from 2001: A Space Odyssey*

*As an aside, imagine the post-mortem review of **that** code!*

Code review is a tricky business. Code is full of hidden mines that lay
dormant while you test just to explode in a debris of stack trace at the
most inopportune time – when its in the hands of your users.

The many times I’ve run into such mines just reinforce how important it
is to write code that is intention revealing and to make sure
assumptions are documented via asserts.

Such devious code is often the most innocuous looking code. Let me give
one example I ran into the other day. I was fortunate to defuse this
mine [while
testing](https://haacked.com/archive/2013/03/04/test-better.aspx "Test Better").

This example makes use of the `Enumerable.ToDictionary` method that
turns a sequence into a dictionary. You supply an expression to produce
a key for each element. In this example, loosely based on the actual
code, I am using the `CloneUrl` property of `Repository` as the key of
the dictionary.

```csharp
IEnumerable<Repository> repositories = GetRepositories();
repositories.ToDictionary(r => r.CloneUrl);
```

It’s so easy to gloss over this line during a code review and not think
twice about it. But you probably see where this is going.

While I was testing I was lucky to run into the following exception:

    System.ArgumentException: 
    An item with the same key has already been added.

Doh! There’s an implicit assumption in this code – that two repositories
cannot have the same `CloneUrl`. In retrospect, it’s obvious that’s not
the case.

Let’s simplify this example.

```csharp
var items = new[]
{
    new {Id = 1}, 
    new {Id = 2}, 
    new {Id = 2}, 
    new {Id = 3}
};
items.ToDictionary(item => item.Id);
```

This example attempts to create a dictionary of anonymous types using
the `Id` property as a key, but we have a duplicate, so we get an
exception.

**What are our options?**

Well, it depends on what you need. Perhaps what you really want is a
dictionary that where the value contains every item with the given key.
The `Enumerable.GroupBy` method comes in handy here.

Perhaps you only care about the first value for a given key and want to
ignore any others. The `Enumerable.GroupBy` method comes in handy in
this case.

In the following example, we use this method to group the items by `Id`.
This results in a sequence of `IGrouping` elements, one for each `Id`.
We can then take advantage of a second parameter of `ToDictionary` and
simply grab the first item in the group.

```csharp
items.GroupBy(item => item.Id)
  .ToDictionary(group => group.Key, group => group.First());
```

This feels sloppy to me. There is too much potential for this to cover
up a latent bug. Why should the other items be ignored? Perhaps, as in
my original example, it’s fully normal to have more than one element for
the key and you should handle that properly. Instead of grabbing the
first item from the group, we retrieve an array.

```csharp
items.GroupBy(item => item.Id)
  .ToDictionary(group => group.Key, group => group.ToArray());
```

In this case, we end up with a dictionary of arrays.

***UPDATE:** Or, as Matt Ellis points out [in the
comments](https://haacked.com/archive/2013/06/17/hidden-code-mines.aspx#comment-933184518 "Comment on ToLookup"),
you could use
the*[*Enumerable.ToLookup*](http://msdn.microsoft.com/en-us/library/bb460184.aspx "Enumerable.ToLookup on MSDN")*method.
I should have known such a thing would exist. It’s exactly what I need
for my particular situation here.*

What if having more than one element with the same key is not expected
and should throw an exception. Well you could just use the normal
`ToDictionary` method since it will throw an exception. But that
exception is unhelpful. It doesn’t have the information we probably
want. For example, you just might want to know, *which* key was already
added as the following demonstrates:

```csharp
items.GroupBy(item => item.Id)
    .ToDictionary(group => group.Key, group =>
    {
        try
        {
            return group.Single();
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException("Duplicate
  item with the key '" + group.First().Id + "'");
        }
    });
```

In this example, if a key has more than one element associated with it,
we throw a more helpful exception message.

    System.InvalidOperationException: Duplicate item with the
    key '2'

In fact, we can encapsulate this into our [own better extension
method](https://gist.github.com/Haacked/5793124 "ToDictionaryBetter extension method on gist").

```csharp
public static Dictionary<TKey, TSource>
  ToDictionaryBetter<TSource, TKey>(
    this IEnumerable<TSource> source,
    Func<TSource, TKey> keySelector)
{
  return source.GroupBy(keySelector)
    .ToDictionary(group => group.Key, group =>
    {
      try
      {
        return group.Single();
      }
      catch (InvalidOperationException)
      {
        throw new InvalidOperationException(
            string.Format("Duplicate item with the key
          '{0}'", keySelector(@group.First())));
      }
    });
}
```

Code mine mitigated!

This is just one example of a potential code mine that might go
unnoticed during a code review if you’re not careful.

Now, when I review code and see a call to `ToDictionary`, I make a
mental note to verify the assumption that the key selector must never
lead to duplicates.

When I write such code, I’ll use one of the techniques I mentioned above
to make my intentions more clear. Or I’ll embed my assumptions into the
code with a debug assert that proves that the items cannot have a
duplicate key. This makes it clear to the next reviewer that this code
will not break for this reason. This code still might not open the
hatch, but at least it won’t have a duplicate key exception.

If I search through my code, I will find many other examples of
potential code mines. What are some examples that you can think of? What
mines do you look for when reviewing code?

