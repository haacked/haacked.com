---
title: Concatenating Delimited Strings With Generic Delegates
date: 2006-11-24 -0800
tags: [tips,csharp]
redirect_from:
  - "/archive/2006/11/23/concatenating_delimited_strings_with_generic_delegates.aspx/"
  - "/archive/2006/11/24/Concatenating_Delimited_Strings_With_Generic_Delegates.aspx/"
---

UPDATE: In my original example, I created my own delegate for converting
objects to strings. [Kevin
Dente](http://weblogs.asp.net/kdente "Kevin Dente") pointed out that
there is already a perfectly fine delegate for this purpose, the
`Converter` delegate. I updated my code to use that instead. Thanks
Kevin!  Just shows you the size and depth of the Framework libraries.

My [recent post on
concatenating](https://haacked.com/archive/2006/11/21/Tip_Jar_Concatenating_A_Delimited_String.aspx "Delimited String Concatenation")
a delimited string sparked [quite a bit of
commentary](https://haacked.com/archive/2006/11/21/Tip_Jar_Concatenating_A_Delimited_String.aspx#feedback "Comments"). 
The inspiration for that post was some code I had to write for a
project.  One constraint that I neglected to mention was that I was
restricted to .NET 1.1.  Today, I revisit this topic, but with the power
of .NET 2.0 in my pocket.

Let’s make our requirements a bit more interesting, shall we?

In this scenario, I have a new class creatively named `SomeClass`.  This
class has a property also creatively named, `SomeDate` (*how do I come
up with these imaginative names*?!). 

```csharp
class SomeClass
{
    public SomeClass(DateTime someDate)
    {
        this.SomeDate = someDate;
    }

    public DateTime SomeDate;
}
```

Suppose I want to concatenate instances of this class together, but this
time I want a pipe delimited list of the number of days between now and
the `SomeDate` value.  For example, given the date *11/23/2006*, the
string should have a “1” since that date was one day ago.  Yes, this is
a contrived example, but it will do.

Now I’ll define a new `Join` method that can take in a delimiter, an
enumeration, and an instance of the `Converter` delegate.  The Converter
delegate has the following signature.

```csharp
delegate TOutput Converter<TIn,TOutput> (TIn input)
```

As an argument to my Join method, I specify that `TOutput` should be a
string, leaving the input to remain generic.

```csharp
public static string Join<T>(string delimiter
                             , IEnumerable<T> items
                             , Converter<T, string> converter)
{
    StringBuilder builder = new StringBuilder();
    foreach(T item in items)
    {
        builder.Append(converter(item));
        builder.Append(delimiter);
    }
    if (builder.Length > 0)
        builder.Length = builder.Length - delimiter.Length;

    return builder.ToString();
}
```

Now with this method defined, I can concatenate an array or collection
of `SomeClass` instances like so:

```csharp
SomeClass[] someClasses = new SomeClass[]
{
  new SomeClass(DateTime.Parse("1/23/2006"))
  , new SomeClass(DateTime.Parse("12/25/2005"))
  , new SomeClass(DateTime.Parse("5/25/2004"))
};

string result = Join<SomeClass>(’|’, someClasses
  , delegate(SomeClass item)
    {
        TimeSpan ts = DateTime.Now - item.SomeDate;
      return ((int)ts.TotalDays).ToString();
    });

Console.WriteLine(result);
```

Notice that I make use of an anonymous delegate that examines an
instance of `SomeClass` and calculates the number of days that
`SomeDate` is in the past.  This returns a string that will be
concatenated together.

This code produces the following output.

```bash
305|334|913
```

This gives me a nice reusable method to concatenate collections of
objects into delimited strings via the `Converter` generic delegate.
This follows a common pattern in .NET 2.0 embodied by such methods as
the `List.ForEach` method which uses the `Action` generic delegate and
the `Array.Find` method which uses the `Predicate` generic delegate.
