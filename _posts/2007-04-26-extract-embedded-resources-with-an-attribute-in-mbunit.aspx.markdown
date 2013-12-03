---
layout: post
title: "Extract Embedded Resources With An Attribute In MbUnit"
date: 2007-04-26 -0800
comments: true
disqus_identifier: 18294
categories: []
---
UPDATE: This functionality is now rolled into the latest version of
MbUnit.

A long time ago [Patrick
Cauldwell](http://www.cauldwell.net/patrick/ "Patrik Cauldwell’s Blog")
wrote up a [technique for managing external
files](http://www.cauldwell.net/patrick/blog/PermaLink,guid,e9a1451b-108c-4da7-8be9-2b6c2316f7b1.aspx "Testing with External Files")
within unit tests by embedding them as resources and unpacking the
resources during the unit test. This is a powerful technique for making
unit tests [self
contained](http://haacked.com/archive/2004/08/26/creating-a-sane-build-process.aspx "Creating a Sane Build Process").

If you look in our unit tests for
[Subtext](http://subtextproject.com/ "Subtext Project Website"), I took
this approach to heart, writing several different methods in our
`UnitTestHelper` class for extracting embedded resources.

Last night, I had the idea to make the code cleaner and even easier to
use by implementing a custom test decorator attribute for my favorite
unit testing framework, [MbUnit](http://mbunit.com/ "MbUnit Website").

### Usage Examples

The following code snippets demonstrates the usage of the attribute
within a unit test. These code samples assume an embedded resource
already exists in the same assembly that the unit test itself is defined
in.

This first test demonstrates how to extract the resource to a specific
file. You can specify a full destination path, or a path relative to the
current directory.

```csharp
[Test]
[ExtractResource("Embedded.Resource.Name.txt", "TestResource.txt")]
public void CanExtractResourceToFile()
{
  Assert.IsTrue(File.Exists("TestResource.txt"));
}
```

The next demonstrates how to extract the resource to a stream rather
than a file.

```csharp
[Test]
[ExtractResource("Embedded.Resource.Name.txt")]
public void CanExtractResourceToStream()
{
  Stream stream = ExtractResourceAttribute.Stream;
  Assert.IsNotNull(stream, "The Stream is null");
  using(StreamReader reader = new StreamReader(stream))
  {
    Assert.AreEqual("Hello World!", reader.ReadToEnd());
  }
}
```

As demonstrated in the previous example, you can access the stream via
the static `ExtractResourceAttribute.Stream` property. **This is only
set if you don’t specify a destination**.

*In case you’re wondering, the stream is stored in a static member
marked with
the*[*[ThreadStatic]*](http://blogs.msdn.com/jfoscoding/archive/2006/07/18/670497.aspx "Are you familiar with [ThreadStatic]")*attribute.
That way if you are taking advantage of MbUnits ability to*[*repeat a
test multiple times using multiple
threads*](http://weblogs.asp.net/astopford/archive/2006/12/28/mbunit-repeating-tests.aspx "MbUnit, repeating tests")*,
you should be OK.*

What if the resource is embedded in another assembly other than the one
you are testing?

Not to worry. You can specify a type (any type) defined in the assembly
that contains the embedded resource like so:

```csharp
[Test]
[ExtractResource("Embedded.Resource.txt"
  , "TestResource.txt"
  , ResourceCleanup.DeleteAfterTest
  , typeof(TypeInAssemblyWithResource))]
public void CanExtractResource()
{
  Assert.IsTrue(File.Exists("TestResource.txt"));
}

[Test]
[ExtractResource("Embedded.Resource.txt"
  , typeof(TypeInAssemblyWithResource))]
public void CanExtractResourceToStream()
{
  Stream stream = ExtractResourceAttribute.Stream;
  Assert.IsNotNull(stream, "The Stream is null");
  using (StreamReader reader = new StreamReader(stream))
  {
    Assert.AreEqual("Hello World!", reader.ReadToEnd());
  }
}
```

This attribute should go a long way to making unit tests that use
external files cleaner. It also demonstrates how easy it is to extend
MbUnit.

A big Thank You goes to [Jay
Flowers](http://jayflowers.com/joomla/ "Jay Flowers") for his help with
this code. **And before I forget, you can download the code for
this**[**custom test decorator
here**](http://haacked.com/code/ExtractResourceAttribute.zip "ExtractResourceAttribute for MbUnit")**.**

*Please note that I left in my unit tests for the attribute which will
fail unless you change the embedded resource name to match an embedded
resource in your own assembly.*

