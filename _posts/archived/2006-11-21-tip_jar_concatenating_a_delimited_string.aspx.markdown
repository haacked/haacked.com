---
layout: post
title: 'Tip Jar: Concatenating A Delimited String'
date: 2006-11-21 -0800
comments: true
disqus_identifier: 18141
categories: []
redirect_from: "/archive/2006/11/20/tip_jar_concatenating_a_delimited_string.aspx/"
---

Update: I also wrote [a more generic
version](http://haacked.com/archive/2006/11/24/Concatenating_Delimited_Strings_With_Generic_Delegates.aspx "Concatenating Delimited Strings")
using anonymous delegates for .NET 2.0 as a followup to this post.

Here’s one for the tip jar. Every now and then I find myself concatening
a bunch of values together to create a delimited string.  In fact, I
find myself in that very position on a current project. In my case, I am
looping through a collection of objects concatenating together three
separate strings, each for a different property of the object (long
story).

Usually when building such a string, I will append the delimiter to the
end of the string I am building during each loop.  But after the looping
is complete, I have to remember to peel off that last delimiter.  Let’s
look at some code, simplified for the sake of this discussion.

The first thing we’ll define is a fake class for demonstration purposes.
It only has one property.

```csharp
internal class Fake
{
    public Fake(string propValue)
    {
        this.SomeProp = propValue;
    }

    public string SomeProp;
    
    public static Fake[] GetFakes()
    {
        return new Fake[] {new Fake("one")
                , new Fake("two")
                , new Fake("three")
            };
    }
}
```

Now let’s look at one way to create a pipe delimited string from this
array of `Fake` instances.

```csharp
Fake[] fakes = Fake.GetFakes();

string delimited = string.Empty;
foreach(Fake fake in fakes)
{
    delimited += fake.SomeProp + "|";
}

delimited = delimited.Substring(0, delimited.Length - 1);
Console.WriteLine(delimited);
```

I never liked this approach because it is error prone. Do you see the
problem? Yep, I forgot to make sure that delimited wasn’t empty when I
called substring. I should correct it like so.

```csharp
if(delimited.Length > 0)
    delimited = delimited.Substring(0, delimited.Length - 1);
```

When I write code like this, I almost always add a little disclaimer in
the comments because I know someone down the line is going to call me an
idiot for not using the `StringBuilder` class to concatenate the
string. However, if I know that the size of the strings to concatenate
and the number of concatenations will be small, there is no point to
using the StringBuilder.  String Concatenations will win out. It all
[depends on the usage
pattern](http://blogs.msdn.com/ricom/archive/2003/12/02/40778.aspx "StringBuilder vs String").

But for the sake of completeness, let’s look at the StringBuilder
version.

```csharp
Fake[] fakes = Fake.GetFakes();

StringBuilder builder = new StringBuilder();
foreach(Fake fake in fakes)
{
    builder.Append(fake.SomeProp);
    builder.Append("|");
}

string delimited = builder.ToString();
if(delimited.Length > 0)
    delimited = delimited.Substring(0, delimited.Length - 1);
Console.WriteLine(delimited);
```

Aesthetically speaking, this code is even uglier because it requires
more code. And as I pointed out, depending on the usage pattern, it
might not provide a performance benefit. Today, a better approach from a
stylistic point of view came to mind. I don’t know why I didn’t think of
it earlier.

```csharp
Fake[] fakes = Fake.GetFakes();

string[] delimited = new string[fakes.Length];
for(int i = 0; i < fakes.Length; i++)
{
    delimited[i] = fakes[i].SomeProp;
}

string delimitedText = String.Join("|", delimited);
Console.WriteLine(delimitedText);
```

Since I know in advance how many items I am concatenating together
(namely `fakes.Length` number of items), I can fully allocate a string
array in advance, populate it with the property values, and then call
the static `String.Join` method.

From a perf perspective, this is probably somewhere between string
concatenation and `StringBuilder`, depending on the usage pattern. But
for the most part, `String.Join` is quite fast, especially in .NET 2.0
(though my current project is on .NET 1.1.  Boohoo!).

Performance issues aside, this approach just feels *cleaner* to me.  It
gets rid of that extra check to remove the trailing delimiter. 
`String.Join` handles that for me.  To me, this is easier to
understand.  What do you think?

