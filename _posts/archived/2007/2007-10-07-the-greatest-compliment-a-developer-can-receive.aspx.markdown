---
title: The Greatest Compliment A Developer Can Receive
date: 2007-10-07 -0800
tags: [code]
redirect_from: "/archive/2007/10/06/the-greatest-compliment-a-developer-can-receive.aspx/"
---

Here’s the dirty little secret about being a software developer. No
matter how good the code you write is, it’s crap to another developer.

It doesn’t matter if the code is so clean you could eat sushi off of it.
Doesn’t matter if both John Carmack and Linus Torvalds bow down in
respect every time the code is shown on the screen. Some developer out
there will call it crap, and it’s usually the developer who inherits the
code when you leave.

The reasons are many and petty:

-   Your code uses string concatenation in that one method rather than
    using a `StringBuilder`. So what if in this one situation, that was
    a conscious decision because on average that method only
    concatenates three or four strings together. The next guy doesn’t
    care.
-   You put your curly braces on the same line rather than its own line
    as God intended (*or vice versa)*.
-   You used a switch statement when *everyone* (including the next
    developer) knows you’re supposed to replace that with the State or
    Strategy pattern, always! Didn’t you read *[Design
    Patterns](http://www.amazon.com/gp/product/0201633612?ie=UTF8&tag=youvebeenhaac-20&linkCode=as2&camp=1789&creative=9325&creativeASIN=0201633612 "Gang of Four Design Patterns")*?
    Never mind the fact that there’s only one switch statement and thus
    no code duplication.
-   You’re using Spring.NET for dependency injection, but the next guy
    loves Windsor. Only idiots choose Spring.NET (*or vice versa,
    again*).
-   Or perhaps you used dependency injection at all. What the hell is
    dependency injection? I don’t understand the code now! :(

While we strive for perfect code, it is unattainable on real projects
because real code is weighed down by the pressure of constraints such as
time pressure. Unfortunately, these constraints aren’t reflected in the
code, just the effect of the constraints. The next developer reading
your code didn’t know that code was written with one hour left to
deliver the project.

Although I admit, having been burned by misguided criticism before, it’s
hard not to be tempted to take a pre-emptive strike at criticism by
trying to embed the constraints in the code via comments.

For example,

```csharp
public void SomeMethod()
{
  /*
  At most, there will only be 4 to 5 foos, so string concatenation 
  is just fine in this situation. Here are links to five blog posts that 
  talk about the perf implications. Give me a break, it’s 
  3 AM, I’m hopped up on Jolt, this project is 3 months
  late, and I have no social life anymore. Cut me some slack!
  ...
  */
  string result = string.Empty;
  foreach(Foo foo in Foos)
  {
    result += foo;
  }
  return result;
}
```

Seems awful defensive, no? There’s nothing wrong with leaving a comment
to highlight why a particular non-obvious design decision is made. **In
fact, that’s exactly what comments are for, rather than simply
reiterating what the code does**.

The problem though, is that developers sometimes cut each other so
little slack, you start writing a treatise in green (or whichever color
you have comments set to in your IDE) to justify every line of code
because you have no idea what is going to be *obvious* to the next
developer.

That’s why I was particularly pleased to receive an email the other day
from a developer who inherited some code I wrote and said that the
solutions were, and I quote, “really well written”.

Seriously? Am I being Punk’d? Ashton, where the hell are you hiding?

This is quite possibly the highest compliment you can receive from
another developer. And I don’t think it’s because I’m such a great
developer. I really think the person who deserves credit here is the one
giving the compliment.

I mean, my reaction when I’ve inherited code was typically, *why the
hell did they write this this way!? Did they learn to code from the back
of a Cracker Jack Box!? *Who better to serve as the scapegoat than the
developer who just left?

Fortunately I had enough tact to keep those thoughts to myself. In the
future, I’ll work harder on the empathy side of things. When I inherit
code, I’ll assume the developer wrote it in a 72 hour straight coding
binge, his World of Warcraft character held hostage, bees all over his
body, with only an hour to finish the code on a 386 before everything
really starts to go south.

Given those circumstances, it’s no wonder the idiot didn’t use a `using`
block around that `IDisposable` instance.

