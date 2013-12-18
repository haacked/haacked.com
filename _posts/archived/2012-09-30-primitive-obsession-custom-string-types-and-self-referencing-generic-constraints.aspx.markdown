---
layout: post
title: "Primitive Obsession, Custom String Types, and Self Referencing Generic Constraints"
date: 2012-09-30 -0800
comments: true
disqus_identifier: 18869
categories: [code]
---
I was once accused of [primitive
obsession](http://grabbagoft.blogspot.com/2007/12/dealing-with-primitive-obsession.html "Dealing with primitive obsession").
Especially when it comes to strings. Guilty as charged!

There’s a lot of reasons to be obsessed with string primitives. Many
times, the data really is a just a string and encapsulating it in some
custom type is just software “designerbation.” Also, strings are special
and the .NET Framework heavily optimizes strings through techniques like
string interning and providing classes like the `StringBuilder`.

But in many cases, a strongly typed class that better represents the
domain is the right thing to do. I think `System.Uri` and its
corresponding `UriBuilder` is a prime example. When you work with URLs,
there are security implications that very few people get right if you
treat them just as strings.

But there’s a third scenario that I often run into now that I build
client applications with the Model View ViewModel (MVVM) pattern. Many
properties on a view model correspond to user input. Often these
properties are populated via data binding with input controls on the
view. As such, these properties often need be able to hold invalid
values that represent the input the user entered.

For example, suppose I want a user to type in a URL. I might have a
property like:

```csharp
public string YourBlogUrl {get; set;}
```

I can’t change the type of that property to a `Uri` because as the user
types, the intermediate values bound to the property are not valid
instances of `Uri`. For example, as the user types the first “h” in
“http”, trying to bind the input value to the `Uri` property would fail.

But this sucks because suppose I want to display the host name on the
screen as soon as one becomes can (more or less) be determined based on
the input. I’d love to be able to just bind another control to
`YourBlogUrl.Host`. But alas, the `string` type does not have a Host
property.

Ideally I would have some middle ground where the type of that property
both has structure, but allows me to have invalid values. Perhaps it has
methods to convert it to a more strict type once we validate that the
value is valid. In this case, a `ToUri` method would makes sense.

But `string` is sealed, so can’t derive from it. What’s a hapless coder
to do?

Custom string types through implicit conversions
------------------------------------------------

Well you could use the `StringOr<T>` class written as an April Fool’s
joke. It was a joke, but it might be useful in cases like this! But
that’s not the approach I’ll take.

Or you can follow the advice of [Jimmy
Bogard](http://lostechies.com/jimmybogard/ "Jimmy Bogard") in his post
on [primitive
obsession](http://grabbagoft.blogspot.com/2007/12/dealing-with-primitive-obsession.html "Dealing with Primitive Obsession")
that I linked to at the beginning (I’m sure he’ll love that I dragged
out a post he wrote five years ago) and write a custom class that’s
implicitly convertible to `string`.

In his post, he shows a `ZipCodeString` example which I will include
below, but with one change. The very last method is a conversion
overload and I changed it from `explicit` to `implicit`.

```csharp
public class ZipCodeString
{
    private readonly string _value;

    public ZipCodeString(string value)
    {
        // perform regex matching to verify XXXXX or XXXXX-XXXX format
        _value = value;
    }

    public string Value
    {
        get { return _value; }
    }

    public override string ToString()
    {
        return _value;
    }

    public static implicit operator string(ZipCodeString zipCode)
    {
        return zipCode.Value;
    }

    public static implicit operator ZipCodeString(string value)
    {
        return new ZipCodeString(value);
    }
}
```

This allows you to write code like:

```csharp
ZipCodeString zip = "98008";
```

This provides the ease of a `string` to initialize a `ZipCodeString`
type, while at the same time it provides access to the structure of a
zip code.

In the interest of full disclosure, many people have a strong feeling
against implicit conversions. I asked [Jon
Skeet](http://msmvps.com/blogs/jon_skeet/ "Jon Skeet's Blog"), Number
one dude on [StackOverflow](http://stackoverflow.com/ "StackOverflow")
and perhaps as well versed in C\# as just about anybody in the world, to
review a draft of this post as I didn’t want to propagate bad practices
without due warning. Here’s what he said:

> Personally I really dislike implicit conversions. I don't even like
> *explicit* conversions much - I prefer methods. So if I'm writing XML
> serialization code, I'll usually have a FromXElement static method,
> and a ToXElement instance method. It definitely makes the code longer,
> but I reckon it's ultimately clearer. (It also allows for several
> conversions from the same type with different meanings - e.g.
> Duration.FromHours, Duration.FromMinutes etc.)

I don’t think I’d ever expose an implicit conversion like this in a
public API that’s meant to share with others. But within my own code, I
like it so far. If I get bitten by it, maybe I’ll change my tune and
Skeet can tell me, “I told you so!”

Taking it further
-----------------

I like Jimmy’s approach, but it doesn’t go far enough for my needs. For
example, this works great when you employ this approach from the start.
But what if you already shipped version 1 of a property as a `string`?
And now you want to change that property to a `ZipCodeString`. But you
have existing values serialized to disk. Or maybe you need pass this
`ZipCodeString` to a JSON endpoint. Is that going to serialize ok?

In my case, I often want these types to act as much like strings as
possible. That way, if I change a property from string to one of these
types, it’ll break as little of my code as possible (if any).

What this means is we need to write a lot more boilerplate code. For
example, override the `Equals` method and operator. In other cases, you
may want to override the addition operator. I did this with a
`PathString` class that represents file paths so I could write code like
this:

```csharp
// The code I recommend writing.
PathString somePath = @"c:\fake\path";
somePath = somePath.Combine("subfolder");
// somePath == @"c:\fake\path\subfolder";

// But if you did this by accident:
PathString somePath = @"c:\fake\path";
somePath += "subfolder";
// somePath == @"c:\fake\path\subfolder";
```

`PathString` has a proper `Combine` method, but I see code where people
attempt to concatenate paths all the time. `PathString` overrides the
addition operator creating an idiom where concatenation is equivalent to
path combination.  This may end up being a bad idea, we’ll see. My
feeling is that if you’re already concatenating paths, this can only
make it better.

I also implemented `ISerializable` and `IXmlSerializable` to make sure
that, for example, the serialized representation of `PathString` looks
exactly like a string.

Since I have multiple types like this, I tried to push as much of the
boilerplate into a base class. But it takes some tricky tricky tricks
that might be a little bit evil.

Here’s the signature of [the base class I
wrote](https://github.com/Haacked/CodeHaacks/blob/master/src/MiscUtils/StringEquivalent.cs "StringEquivalent on GitHub"):

```csharp
[Serializable]
public abstract class StringEquivalent<T> 
  : ISerializable, IXmlSerializable where T : StringEquivalent<T>
{
    protected StringEquivalent(string value);
    protected StringEquivalent();
    public abstract T Combine(string addition);
    public static T operator +(StringEquivalent<T> a, string b);
    public static bool operator ==(StringEquivalent<T> a, StringEquivalent<T> b);
    public static bool operator !=(StringEquivalent<T> a, StringEquivalent<T> b);
    public override bool Equals(Object obj);
    public bool Equals(T stringEquivalent);
    public virtual bool Equals(string other)    
    public override int GetHashCode();
    public override string ToString();
    // Implementations of ISerializable and IXmlSerializable
}
```

The full implementation is available in my [CodeHaacks repo on
GitHub](https://github.com/Haacked/CodeHaacks "CodeHaacks") with full
unit tests and examples.

Self Referencing Generic Constraints
------------------------------------

There’s some stuff in here that just seemed crazy to me at first. For
example, taking out the interfaces, did you notice the generic type
declaration?

```csharp
public abstract class StringEquivalent<T> : where T : StringEquivalent<T> 
```

Notice that the generic constraint is self-referencing. This is a
pattern that [Eric Lippert
discourages](http://blogs.msdn.com/b/ericlippert/archive/2011/02/03/curiouser-and-curiouser.aspx "Curiouser and Curiouser"):

> Yes it is legal, and it does have some legitimate uses. I see this
> pattern rather a lot(\*\*). However, I personally don't like it and I
> discourage its use.
>
> This is a C\# variation on what's called the [Curiously Recurring
> Template
> Pattern](http://en.wikipedia.org/wiki/Curiously_recurring_template_pattern)
> in C++, and I will leave it to my betters to explain its uses in that
> language. Essentially the pattern in C\# is an attempt to *enforce*
> the usage of the CRTP.
>
> …snip…
>
> So that's one good reason to avoid this pattern: because it doesn't
> actually enforce the constraint you think it does.
>
> …snip…
>
> **The second reason to avoid this is simply because it**[**bakes the
> noodle**](http://www.youtube.com/watch?v=kWVWNri4IFM)**of anyone who
> reads the code.**

Again, Jon Skeet provided an example of the warning that Lippert states
in regards to the inability to actually enforce the constraint I might
wish to enforce.

> While you're not fulIy enforcing a constraint, the constraint which
> you *have* got doesn't prevent some odd stuff. For example, it would
> be entirely legitimate to write:
>
> `public class ZipCodeString : StringEquivalent<ZipCodeString>`
>
> `public class WeirdAndWacky : StringEquivalent<ZipCodeString>`
>
> That's legal, and we don't really want it to be. That's the kind of
> thing Eric was trying to avoid, I believe.

The reason I chose to against the recommendation of someone much smarter
than me in this case is because my goal isn’t to enforce these
constraints at all. It’s to enable a scenario. This is the only way to
implement these various operator overloads in a base class. Without
these constraints, I’d have to reimplement them for every class class.
If you know a better approach, I’m all ears.

WPF Value Converter and Markup Extension Examples
-------------------------------------------------

As a bonus divergence, I thought I’d throw in one more example of a
self-referencing generic constraint. In WPF, there’s a concept of a
value converter, `IValueConverter`, used to convert values from XAML to
your view model and vice versa. However, the mechanism to declare and
use value converters is really clunky.

Josh Twist [provides a nice
example](http://www.thejoyofcode.com/WPF_Quick_Tip_Converters_as_MarkupExtensions.aspx "Converters as Markup Extensions")
that cleans up the syntax with value converters that are also
`MarkupExtension`. I decided to take it further and write a base class
that does it.

```csharp
public abstract class ValueConverterMarkupExtension<T> 
    : MarkupExtension, IValueConverter where T : class, IValueConverter, new()
{
  static T converter;

  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    return converter ?? (converter = new T());
  }

  public abstract object Convert(object value, Type targetType
    , object parameter, CultureInfo culture);

  // Only override this if this converter might be used with 2-way data binding.
  public virtual object ConvertBack(object value
    , Type targetType, object parameter, CultureInfo culture)
  {
    return DependencyProperty.UnsetValue;
  }
}
```

I’m sure I’m not the first to do something like this.

Now all my value converters inherit from this base class.

Back to Primitives
------------------

Back to the original topic, I used to supplement primitives with loads
of extension methods. I have a set of extension methods of `string` I
use quite a bit. But more and more, I’m starting to prefer dialing that
back a bit in cases like this where I need something to be a string with
structure.

