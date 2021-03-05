---
title: Interface Inheritance Esoterica
tags: [code,dotnet]
redirect_from: "/archive/2009/11/09/interface-inheritance-esoterica.aspx/"
---

I learned something new yesterday about interface inheritance in .NET as
compared to implementation inheritance. To illustrate this difference,
here’s a simple demonstration.

I’ll start with two concrete classes, one which inherits from the other.
Each class defines a property. In this case, we’re dealing with
*implementation inheritance*.

```csharp
public class Person {
    public string Name { get; set; }
}

public class SuperHero : Person {
    public string Alias { get; set; }
}
```

We can now use two different techniques to print out the properties of
the `SuperHero` type: type descriptors and reflection. Here’s a little
console app that does this. Note the code I’m showing below doesn’t
include a few `Console.WriteLine` calls that I have in the actual app.

```csharp
static void Main(string[] args) {
  // type descriptor
  var properties = TypeDescriptor.GetProperties(typeof(SuperHero));
  foreach (PropertyDescriptor property in properties) {
    Console.WriteLine(property.Name);
  }

  // reflection
  var reflectedProperties = typeof(SuperHero).GetProperties();
  foreach (var property in reflectedProperties) {
    Console.WriteLine(property.Name);
  }
}
```

Let’s look at the output of this code.

![impl-inheritance](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/InterfaceInheritanceEsoterica_8E49/impl-inheritance_3.png "impl-inheritance")

No surprises there.

The `SuperHero` type has two properties, `Alias` defined on `SuperHero`
and the `Name` property inherited from its base type.

But now, let’s change these classes into interfaces so that we’re now
dealing with *interface inheritance*. Notice that `ISupeHero` now
derives from `IPerson`.

```csharp
public interface IPerson {
  string Name { get; set; }
}

public interface ISuperHero : IPerson {
  string Alias { get; set; }
}
```

I’ve also made the corresponding changes to the console app.

```csharp
var properties = TypeDescriptor.GetProperties(typeof(ISuperHero));
foreach (PropertyDescriptor property in properties) {
  Console.WriteLine(property.Name);
}

// reflection
var reflectedProperties = typeof(ISuperHero).GetProperties();
foreach (var property in reflectedProperties) {
  Console.WriteLine(property.Name);
}
```

Before looking at the next screenshot, take a moment to answer the
question, what is the output of the program now?

[![interface-inheritance](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/InterfaceInheritanceEsoterica_8E49/interface-inheritance_thumb.png "interface-inheritance")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/InterfaceInheritanceEsoterica_8E49/interface-inheritance_2.png)
Well it should be obvious that the output is different otherwise I
wouldn’t be writing this blog post in the first place, right?

When I first tried this out, I found the behavior surprising. However,
it’s probably not surprising to anyone who has an encyclopedic knowledge
of the [ECMA-335 Common Language Infrastructure specification
(PDF)](http://www.ecma-international.org/publications/files/ECMA-ST/Ecma-335.pdf "Ecma-335 spec")
such as Levi, one of the ASP.NET MVC developers who pointed me to
section 8.9.11 of the spec when I asked about this behavior:

> **8.9.11 Interface type derivation** Interface types can require the
> implementation of one or more other interfaces. Any type that
> implements support for an interface type shall also implement support
> for any required interfaces specified by that interface. This is
> different from object type inheritance in two ways:
>
> -   Object types form a single inheritance tree; interface types do
>     not.
> -   Object type inheritance specifies how implementations are
>     inherited; required interfaces do not, since interfaces do not
>     define implementation. Required interfaces specify additional
>     contracts that an implementing object type shall support.
>
> To highlight the last difference, consider an interface, `IFoo`, that
> has a single method. An interface, `IBar`, which derives from it, is
> requiring that any object type that supports `IBar` also support IFoo.
> It does not say anything about which methods `IBar` itself will have.

The last paragraph provides a great example of why the code I wrote
behaves as it does. The fact that `ISuperHero` inherits from `IPerson`
doesn’t mean the `ISuperHero` interface type inherits the properties of
`IPerson `because interfaces do not define implementation.

Rather, what it means is that any class that implements `ISuperHero`
must also implement the `IPerson` interface. Thus if I wrote an
implementation of `ISuperHero` such as:

```csharp
public class Groo : ISuperHero {
  public string Name {get; set;}
  public string Alias {get; set;}
}
```

The `Groo` type must implement both `ISuperHero` and `IPerson` and
iterating over its properties would show both properties.

### Implications for ASP.NET MVC Model Binding

You probably could have guessed this part was coming. Let’s say you’re
trying to use model binding to bind the `Name` property of an
`ISuperHero`. Since our model binder uses type descriptors under the
hood, we won’t be able to bind that property for the reasons stated
above.

I learned of this detail investigating a [bug reported in
StackOverflow](http://stackoverflow.com/questions/1676731/asp-net-mvc-v2-debugging-model-binding-issues "Debugging Model Binding Issues").
It turns out this behavior is by design. In the context of sending a
view model to the view, that view model should be a simple carrier of
data. Thus it makes sense to use concrete types on your view model, in
contrast to your domain models which will more likely be interface
based.

