---
title: Creating Copies of Attributes
tags: [code]
redirect_from: "/archive/2010/08/04/copying-attributes.aspx/"
---

UPDATE: A reader named Matthias pointed out there is a flaw in my code.
Thanks Matthias! I’ve [corrected it in my GitHub
Repository](https://github.com/Haacked/CodeHaacks/blob/master/src/MiscUtils/AttributeExtensions.cs).
The code would break if your attribute had an array property or
constructor argument.

I’ve been working on a lovely little prototype recently but ran into a
problem where my code receives a collection of attributes and needs to
change them in some way and then pass the changed collection along to
another method that consumes the collection.

[![reflection](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ReflectingOverAttributesWithoutLoadingTh_ED5C/reflection_3.jpg "reflection")](http://www.sxc.hu/photo/931357 "Abstract creation: by Jamie Woods from sxc.hu")

I  want to avoid changing the attributes directly, because when you use
reflection to retrieve attributes, those attributes may be cached by the
framework. So changing an attribute is not a safe operation as you may
be changing the attribute for everyone else who tries to retrieve them.

What I really wanted to do is create a *copy* of all these attributes,
and pass the collection of copied attributes along. But how do I do
that?

### CustomAttributeData

[Brad Wilson](http://bradwilson.typepad.com/ "Brad Wilson's Blog") and
[David Ebbo](http://blogs.msdn.com/b/davidebb/ "David Ebbo's Blog") to
the rescue! In a game of geek telephone, David told Brad a while back,
who then recently told me, about a little class in the framework called
`CustomAttributeData`.

This class takes advantage of a feature of the framework known as a
[Reflection-Only
context](http://msdn.microsoft.com/en-us/library/ms172331.aspx "Reflection-Only Context on MSDN").
This allows you to examine an assembly without instantiating any of its
types. This is useful, for example, if you need to examine an assembly
compiled against a different version of the framework or a different
platform.

### Copying an Attribute

As you’ll find out, it’s also useful when you need to copy an attribute.
This might raise the question in your head, “if you have an existing
attribute instance, why can’t you just copy it?” The problem is that a
given attribute might not have a default constructor. So then you’re
left with the challenge of figuring out how to populate the parameters
of a constructor from an existing instance of an attribute. Let’s look
at a sample attribute.

```csharp
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class SomethingAttribute : Attribute {
  public SomethingAttribute(string readOnlyProperty) {
      ReadOnlyProperty = readOnlyProperty;
  }
  public string ReadOnlyProperty { get; private set; }
  public string NamedProperty { get; set; }
  public string NamedField;
}
```

And here’s an example of this attribute applied to a class a couple of
times.

```csharp
[Something("ROVal1", NamedProperty = "NVal1", NamedField = "Val1")]
[Something("ROVal2", NamedProperty = "NVal2", NamedField = "Val2")]
public class Character {
}
```

Given an instance of this attribute, I *might* be able to figure out how
the constructor argument should be populated by assuming a convention of
using the property with the same name as the argument. But what if the
attribute had a constructor argument that had no corresponding property?
Keep in mind, I want this to work with arbitrary attributes, not just
ones that I wrote.

### CustomAttributeData saves the day!

This is where `CustomAttributeData` comes into play. An instance of this
class tells you everything you need to know about the attribute and how
to construct it. It provides access to the constructor, the constructor
parameters, and the named parameters used to declare the attribute.

Let’s look at a method that will create an attribute instance given an
instance of `CustomAttributeData`.

```csharp
public static Attribute CreateAttribute(this CustomAttributeData data){
  var arguments = from arg in data.ConstructorArguments
                    select arg.Value;

  var attribute = data.Constructor.Invoke(arguments.ToArray())     as Attribute;

  foreach (var namedArgument in data.NamedArguments) {
    var propertyInfo = namedArgument.MemberInfo as PropertyInfo;
    if (propertyInfo != null) {
      propertyInfo.SetValue(attribute, namedArgument.TypedValue.Value, null);
    }
    else {
      var fieldInfo = namedArgument.MemberInfo as FieldInfo;
      if (fieldInfo != null) {
        fieldInfo.SetValue(attribute, namedArgument.TypedValue.Value);
      }
    }
  }

  return attribute;
}
```

The code sample demonstrates how we use the information within the
`CustomAttributeData` instance to figure out how to create an instance
of the attribute described by the data.

So how did we get the `CustomAttributeData` instance in the first place?
That’s pretty easy, we called the
`CustomAttributeData.GetCustomAttributes()` method. With these pieces in
hand, it’s pretty straightforward now to copy the attributes on a type
or member. Here’s a set of extension methods I wrote to do just that.

NOTE: The following code does not handle array properties and
constructor arguments correctly. Check out my repository [for the
correct
code](https://github.com/Haacked/CodeHaacks/blob/master/src/MiscUtils/AttributeExtensions.cs).

```csharp
public static IEnumerable<Attribute> GetCustomAttributesCopy(this Type type) {
  return CustomAttributeData.GetCustomAttributes(type).CreateAttributes();
}

public static IEnumerable<Attribute> GetCustomAttributesCopy(    this Assembly assembly) {
  return CustomAttributeData.GetCustomAttributes(assembly).CreateAttributes();
}

public static IEnumerable<Attribute> GetCustomAttributesCopy(    this MemberInfo memberInfo) {
  return CustomAttributeData.GetCustomAttributes(memberInfo).CreateAttributes();
}

public static IEnumerable<Attribute> CreateAttributes(    this IEnumerable<CustomAttributeData> attributesData) {
  return from attributeData in attributesData
          select attributeData.CreateAttribute();
}
```

And here’s a bit of code I wrote in a console application to demonstrate
the usage.

```csharp
foreach (var instance in typeof(Character).GetCustomAttributesCopy()) {
  var somethingAttribute = instance as SomethingAttribute;
  Console.WriteLine("ReadOnlyProperty: " + somethingAttribute.ReadOnlyProperty);
  Console.WriteLine("NamedProperty: " + somethingAttribute.NamedProperty);
  Console.WriteLine("NamedField: " + somethingAttribute.NamedField);
}
```

And there you have it, I can grab the attributes from a type and produce
a copy of those attributes.

With this out of the way, I can hopefully continue with my original
prototype which led me down this rabbit hole in the first place. It
always seems to happen this way, where I start a blog post, only to
start writing a blog post to support that blog post, and then a blog
post to support that one. Much like a dream within a dream within a
dream. ;)

