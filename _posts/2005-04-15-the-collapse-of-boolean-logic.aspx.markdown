---
layout: post
title: "The Collapse of Boolean Logic"
date: 2005-04-14 -0800
comments: true
disqus_identifier: 2711
categories: []
---
What will this code write to the console?

public void SomeMethod()

{

    SqlInt32 x = 1;

 

    if(x == SqlInt32.Null)

    {

        Console.WriteLine("Null.");

    }

    else if(x != SqlInt32.Null)

    {

        Console.WriteLine("Not Null.");

    }

    else

    {

        Console.WriteLine("Neither? WTF!?");

    }

}

If you said "Neither? WTF!?" then you'd be correct.

Doesn't that seem odd since x is either SqlInt32.Null or it isn't? Is
this a case of fuzzy logic? Well not exactly. This is one of those
gotchas with operator overloading. The == operator is overloaded by
SqlInt32 to return a SqlBoolean instead of a boolean.

This caused me a few minutes of pain this week as I was stepping through
code like this and examining the values in the debugger and I thought
perhaps someone had slipped me a very strong hallucinogen because there
I was with a value that was not null, but was null at the same time. It
was one of those mind warping "This sentence is not true" moments.

As typical, I was thinking about rebooting when on a whim I decided to
use intermediate variables and realized that the == was returning
something neither true nor false. Honestly, I think this is a very poor
and unnecessary use of operator overloading (correct me if I'm wrong)
because it hides a real gotcha underneath a very common paradigm. If you
overload the == operator, you should most definitely return a bool.

In this case, it's easy enough to fix. I should have been checking the
IsNull property anyways like so:

public void SomeMethod()

{

    SqlInt32 x = 1;

 

    if(x.IsNull)

    {

        Console.WriteLine("Null.");

    }

    else

    {

        Console.WriteLine("Not Null.");

    }

}

This [post
by](http://blogs.msdn.com/cyrusn/archive/2005/04/15/408689.aspx) Cyrus
motivated me to write about this.

