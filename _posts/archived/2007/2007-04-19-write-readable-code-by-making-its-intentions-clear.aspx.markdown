---
title: Write Readable Code By Making Its Intentions Clear
tags: [code,patterns]
redirect_from:
- "/archive/2007/04/20/write-readable-code-by-making-its-intentions-clear.aspx"
- "/archive/2007/04/18/write-readable-code-by-making-its-intentions-clear.aspx/"
---

I don’t think it’s too much of a stretch to say that the hardest part of
coding is not writing code, but reading it. As Eric Lippert points out,
[Reading code is hard.](http://blogs.msdn.com/ericlippert/archive/2004/06/14/155316.aspx "Eric Lippert writes on Reading Code Is Hard")

> First off, I agree with you that there are very few people who can
> read code who cannot write code themselves. It’s not like written or
> spoken natural languages, where **understanding what someone else says
> does not require understanding why they said it that way.**

[![Screenshot of
code](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/APIDesignMakeYourIntentionsClear_AF1C/324180_numbers_and_letters_my_mac_pu%5B1%5D.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/APIDesignMakeYourIntentionsClear_AF1C/324180_numbers_and_letters_my_mac_pu.jpg "Code")*Hmmm,
now why did Eric say that in that particular way?*

This in part is why reinventing the wheel is so common (*apart from the
need to prove you can build a better wheel*). It’s easier to write new
code than try and understand and use existing code.

It is crucial to try and make your code as easy to read as possible.
**Strive to be the Dr. Seuss of writing code.** Making your code easy to
read makes it easier to use.

The basics of readable code include the usual advice of following code
conventions, formatting code properly, and choosing good names for
methods and variables, among other things. This is all included within
[Code
Complete](http://www.amazon.com/Code-Complete-Second-Steve-McConnell/dp/0735619670/ref=pd_bbs_sr_1/104-5216050-0709506?ie=UTF8&s=books&qid=1177055583&sr=1-1 "Code Complete")
which should be your software development bible.

Aside from all that, **a key tactic to improve code readibility and
usability is make your code’s intentions crystal clear.**

Oftentimes it’s paying attention to the little things that can really
help your code along this path. Let’s look at a few examples.

## out vs ref

A while ago I encountered some code that looked something like this
contrived example:

```csharp
int y = 7;
//...
bool success = TrySomething(someParam, ref y);
```

Ignore the terrible names and focus on the parameters. At a glance, what
is your initial expectation of this code regarding its parameter?

When I encountered this code, I assumed that that the `y` parameter
value passed in to this method is important somehow and that the method
probably changes the value.

I then took a look at the method (keep in mind this is all extremely
simplified from the actual code).

```csharp
public bool TrySomething(object something, ref int y)
{
  try
  {
    y = resultOfCalculation(something);
  }
  catch(SomeException)
  {
    return false;
  }
  return true;
}
```

Now this annoyed me. Sure, this method is perfectly valid and will
compile. But notice that the value of y is never used. It is immediately
assigned to something else.

The intention of this method is not clear. It’s intent is not to ever
use the value of y, but to merely set it. But since the method uses the
`ref` keyword, you are required to set the value of the parameter before
you call it. You can’t do this:

```csharp
int y;
bool success = TrySomething(someParam, ref y);
```

In this case, using the `out` keyword expresses the intentions much
better.

```csharp
public bool TrySomething(object something, out int y)
{
  try
  {
    y = resultOfCalculation(something);
  }
  catch(SomeException)
  {
    return false;
  }
  return true;
}
```

It’s a really teeny tiny thing, something you might accuse me of being
nitpicky even bringing it up, but anything you can do so that the reader
of the code doesn’t have to interrupt her train of thought to figure out
the meaning of the code will make your code more readable and the API
more usable.

## Boolean Arguments vs Enums

Brad Abrams [touched upon
this](http://blogs.msdn.com/brada/archive/2004/01/12/57922.aspx "Enums vs Boolean Arguments")
one a while ago. Let’s look at an example.

```csharp
BlogPost p = CreatePost(post, true, false);
```

What exactly is this code doing? Well it’s obvious it creates a blog
post. But what is that `true` indicate? Hard to say. I better pause,
look up the method, and then move on. What a pain!

```csharp
BlogPost p = CreatePost(post
  , PostStatus.Published, CommentStatus.CommentsDisabled);
```

In the second case, the intentions of the code is much clearer and there
is no interruption for the reader to figure out the context of the true
or false as in the first method.

## Assigning a Value You Don’t Use

Another common example I’ve seen is where the result of a method is
assigned to the value of a variable, but the variable is never used. I
think this often happens because some developers falsely believe that if
a method returns a value, that value has to be assigned to something.

Let’s look at an example that uses the TrySomething method I wrote
earlier.

```csharp
int y;
bool success = TrySomething(something, out y);
/*success is never used again.*/
```

Fortunately,
[Resharper](http://www.jetbrains.com/resharper/ "Resharper") makes this
sort of thing stick out like a sore thumb. The problem here is that as a
code reader, I’m left wondering if you meant to use the variable and
forgot, or if this is an unecessary declaration. Do this instead.

```csharp
int y;
TrySomething(something, out y);
```

Again, these are very small things, but they make a big difference.
Don’t worry about coming across as anal (you will) because the payout is
worth it in the end.

**What are some examples that you can think of to make code more
readable and usable?**

UPDATE: Lesson learned. If you oversimplify your code examples, your
main point is lost. Especially on the topic of code readability. Touche!
I’ve updated the sample code to better illustrate my point. The comments
may be out of synch with what you read here as a result.

UPDATE AGAIN: I found another great blog post about writing concise code
that adds a lot to this discussion. It is part of the Fail Fast and
Return Early school of thought. [Short, concise and readable code -
invert your logic and stop nesting
already!](http://javathink.blogspot.com/2006/10/short-concise-and-readable-code-invert.html "Concise Code")

