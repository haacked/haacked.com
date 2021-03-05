---
title: How Constant Are Your Constants in .NET
tags: [dotnet]
redirect_from: "/archive/2004/12/31/HowConstantAreYourConstants.aspx/"
---

![Code Complete 2](/assets/images/codecomplete2.gif) Back in the day when I was
a wet behind the ears developer a coworker gave me some sage advice. He
told me that if I wanted to become a good developer, I need to read the
bible. He was of course referring to Code Complete, the bible of
software construction. When I was promoted to manager, I made it
required reading for developers. Several years later, I’m reading
through the second edition savoring every page like a fine glass of
sake.

This time around, I have a lot more experience to provide context to
what I’m reading. Around page 270 (Chapter 11 end of section 2) I came
across McConnel’s recommendations about the use of constants and it got
me thinking about how appropriate that advice is in the world of .NET.

McConnel discusses good and bad names for constants. An example of a
poor name for a constant is *FIVE*. If you needed to change it to
another value, it wouldn’t make any sense (*const int FIVE = 6;*).
Instead choose a name that represents the abstract entity the constant
represents. For example, CYCLES_NEEDED.

Another bad example he presents is *BAKERS_DOZEN* which he states would
be better named as *DONUTS_MAX*.

Although I agree with him in principle, his advice might need to be
modified in light of how constants are handled in .NET. For example,
CYCLES_NEEDED probably shouldn’t be a constant if you think you might
change the value later. Secondly, BAKERS_DOZEN might be a fine constant
since it’s a value that will never change.

This boils down to a semantic issue. What exactly is a constant? Is it
simply a variable with a value set at compile time often used to
consolidate a setting in one place? Or is it a variable that holds a
value that never changes, not even from build to build?

Well the answer of course is "it depends". When you look at .NET
however, it seems to favor the latter behavior. Suppose you’re building
a class library that contains a public constant like so:

```csharp
public class Library
{
    public const int CYCLES_NEEDED = 5;
}
```

And you build an application that references this assembly and makes use
of the constant like so.

```csharp
class MyApp
{
    /// 
    /// The main entry point for the application.
    /// 
    [STAThread]
    static void Main(string[] args)
    {
        for(int i = 0; i < Library.CYCLES_NEEDED; i++)
        {
            //Do meaningful work...
            Console.WriteLine(i);
        }
    }
}
```

If you compile and run this simple program, the console will output the
numbers 0 through 4 as you would expect. Yes, this is a complicated
program. The result of many years of experience.

Now suppose it’s several weeks later and your boss storms into your
office. The company is bleeding cash and he wants you to up the cycles
to 6 to increase profit. "Why that’s simple" you say to yourself.

"I’ll just change the value of CYCLES_NEEDED, recompile my library
assembly, and deploy the dll without touching the exe so that the
downtime is minimized. I’m such a genius!"

So what happens when you do that? You get the same output as before.

Huh?

When one assembly references a constant in another assembly, the
compiler will embed the value of that constant into the assembly. For
example, using [Reflector](http://www.aisto.com/roeder/dotnet/) to
decompile the sophisticated console app presented above, the Main method
is compiled as:

```csharp
[STAThread]
private static void Main(string[] args)
{
    for (int num1 = 0; num1 < 5; num1++)
    {
        Console.WriteLine(num1);
    }
}
```

So as you can see, in order to change the value of the constant, both
the library and the consumer of the library have to be recompiled to
reflect the change with the constant. If we anticipate that
CYCLES_NEEDED might ever change, it would be better to make this a
public static read only variable as such:

```csharp
public class Library
{
    public static readonly int CYCLES_NEEDED = 5;
}
```

Now should you deploy a change to the value of CYCLES_NEEDED, the
console application will pick up the change without needing to recompile
it. This is especially important in cases where it’s much easier to
deploy a dll rather than the entire application.

The only drawback to this approach is that the value needs to be
obtained at run-time instead of having the value compiled into the app
which is a slight performance hint. Well if you’re worried about this,
I’d suggest that you’re suffering from a case of premature optimization
and you need to go read [Rico’s blog](http://blogs.msdn.com/ricom/)
where he’ll tell you to measure measure measure. As McConnel states
repeatedly in Code Complete, the greatest impediment to performance is
most likely to be the overall architecture of your system and not minor
code issues.

Of course, if you have full control over your libraries and clients of
the libraries, this may not be as big an issue to you. However, if you
have several production systems deployed, it's nice to apply patches via
deploying the least amount of code as possible.

