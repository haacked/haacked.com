---
title: Death to the IF statement
tags: [code,languages]
redirect_from: "/archive/2013/11/07/death-to-the-if-statement.aspx/"
excerpt_image: https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UnconditionalProgrammingInC_BD84/Msi_if_cover_2.jpg
---

Over the past few years I’ve become more and more interested in
functional programming concepts and the power, expressiveness, and
elegance they hold.

But you don’t have to abandon your language of choice and wander the
desert eating moths and preaching the gospel of F\#,  Haskell, or
Clojure to enjoy these benefits today!

In his blog post, [Unconditional
Programming](http://michaelfeathers.typepad.com/michael_feathers_blog/2013/11/unconditional-programming.html "Unconditional Programming"),
Michael Feathers ponders how less control structures lead to better
code,

> Control structures have been around nearly as long as programming but
> it's hard for me to see them as more than an annoyance.  Over and over
> again, I find that better code has fewer if-statements, fewer
> switches, and fewer loops.  Often this happens because developers are
> using languages with better abstractions.  They aren't consciously
> trying to avoid control structures but they do.

We don’t need to try and kill every `if` statement, but perhaps the more
we do, the better our code becomes.

[![Msi_if_cover](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UnconditionalProgrammingInC_BD84/Msi_if_cover_thumb.jpg "Msi_if_cover")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UnconditionalProgrammingInC_BD84/Msi_if_cover_2.jpg)*Photo
from [wikimedia](http://en.wikipedia.org/wiki/File:Msi_if_cover.jpg):
Cover of If by the artist Mindless Self Indulgence*

He then provides an example in Ruby of a padded “take” method.

> …I needed to write a 'take' function to take elements from the
> beginning of an array.  Ruby already has a take function on
> Enumerable, but I needed to special behavior.  If the number of
> elements I needed was larger than the number of elements in the array,
> I needed to pad the remaining space in the resulting array with zeros.

I recommend reading his post. It’s quite interesting. At the risk of
spoiling the punch line, here’s the *before* code which makes use of a
conditional...

```ruby
  def padded_take ary, n
    if n <= ary.length
      ary.take(n)
    else
      ary + [0] * (n - ary.length)
  end
  end
```

… and here is the *after* code without the conditional. In this case, he
pads the source array with just enough elements as needed and then does
the take.

```ruby
  def pad ary, n
    pad_length = [0, n - ary.length].max
    ary + [0] * pad_length
  end

  def padded_take ary, n
    pad(ary, n).take(n)
  end
```

I thought it would be interesting to translate the *after* code to C#.
One thing to note about the Ruby code is that it always allocates a new
array whether it’s needed or not.

Now, I haven’t done any benchmarks on it so I have no idea if that’s bad
or not compared to how often the code is called etc. But it occurred to
me that we could use lazy evaluation in C# and completely circumvent
the need to allocate a new array while still being expressive and
elegant.

I decided to write it as an extension method (*I guess that’s similar to
a Mixin for you Ruby folks?*).

```csharp
public static IEnumerable<T> PaddedTake<T>(
  this IEnumerable<T> source, int count)
{
  return source
    .Concat(Enumerable.Repeat(default(T), count))
    .Take(count);
}
```

This code takes advantage of some `Linq` methods. The important thing to
note is that `Concat` and `Repeat` are lazily evaluated. That’s why I
didn’t need to do any math to figure out the difference in length
between the source array and the the take count.

I just passed the total `count` we want to take to `Repeat`. Since
`Repeat` is lazy, we could pass in `int.MaxValue` if we wanted to get
all crazy up in here. I just passed in `count` as it will always be
enough and I like to play it safe.

Now my Ruby friends at work might scoff at all those angle brackets and
parentheses in the code, but you have to admit that it’s an elegant
solution to the original problem.

Here is a test to demonstrate usage and show it works.

```csharp
var items = new[] {1, 2, 3};

var result = items.PaddedTake(5).ToArray();

Assert.Equal(5, result.Length);
Assert.Equal(1, result[0]);
Assert.Equal(2, result[1]);
Assert.Equal(3, result[2]);
Assert.Equal(0, result[3]);
Assert.Equal(0, result[4]);
```

I also ran some quick perf tests on it comparing `PaddedTake` to the
built in `Take` . `PaddedTake` is a tiny bit slower, but the amount is
like the extra light cast by a firefly at noon of a sunny day. The
performance of this method is way more affected by the number of
elements in the array and the number of elements you are taking. But in
my tests, the performance of `PaddedTake` stays pretty close to `Take`
as we grow the array and the take.

I think it’d be interesting to have a build task that reported back the
number of `if` statements and other control structures per line of
code and see if you can bring that down over time. In any case, I hope
this helps you improve your own code!

