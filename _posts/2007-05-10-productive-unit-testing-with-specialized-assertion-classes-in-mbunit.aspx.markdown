---
layout: post
title: "Productive Unit Testing with Specialized Assertion Classes in MbUnit"
date: 2007-05-10 -0800
comments: true
disqus_identifier: 18306
categories: [code,tdd]
---
If you’ve worked with unit test frameworks like
[NUnit](http://nunit.com/ "NUnit") or
[MbUnit](http://mbunit.com/ "MbUnit") for a while, you are probably all
too familiar with the set of assertion methods that come built into
these frameworks. For example:

```csharp
Assert.AreEqual(expected, actual);
Assert.Between(actual, left, right);
Assert.Greater(value1, value2);
Assert.IsAssignableFrom(expectedType, actualType);
// and so on...
```

While the list of methods on the `Assert` class is impressive, it leaves
much to be desired. For example, I needed to assert that a string value
was a member of an array. Here’s the test I wrote.

```csharp
[Test]
public void CanFindRole()
{
  string[] roles = Roles.GetRolesForUser("pikachu");
  bool found = false;
  foreach (string role in roles)
  {
    if (role == "Pokemon")
      found = true;
  }
  Assert.IsTrue(found);
}
```

Ok, so that’s not all *that* terrible (*and yes, I could write my own
array contains method, but bear with me).* But still, if only there was
a better way to do this.

Well I obviously wouldn’t be writing about this if there wasn’t. **It
turns out that MbUnit has a rich collection of specialized assertion
classes that help handle the grudge work of writing unit tests**. These
classes aren’t as well known as the straightforward `Assert` class.

As an example, here is the previous test rewritten using the
`CollectionAssert` class.

```csharp
[Test]
public void CanFindRole()
{
  string[] roles = Roles.GetRolesForUser("pikachu");
  CollectionAssert.Contains(roles, "pokemon");
}
```

How much cleaner is that? CollectionAssert has many useful assertion
methods. Here’s a small sampling.

```csharp
CollectionAssert.AllItemsAreNotNull(collection);
CollectionAssert.DoesNotContain(collection, actual);
CollectionAssert.IsSubsetOf(subset, superset);
```

Here is a list of some of the other useful specialized assert classes.

-   **`CompilerAssert`** - Allows you to compile source code
-   **`ArrayAssert`** - Methods to compare two arrays
-   **`ControlAssert`** - Tons of methods for comparing Windows controls
-   **`DataAssert`** - Methods for comparing data sets and the like
-   **`FileAssert`** - Compare files and assert existence
-   **`GenericAssert`** - Compare generic collections
-   **`ReflectionAssert`** - Lots of methods for using reflection to
    compare types, etc...
-   **`SecurityAssert`** - Assert security properties such as whether
    the user is authenticated
-   **`StringAssert`** - String specific assertions
-   **`SerialAssert`** - Assertions for serialization
-   **`WebAssert`** - Assertionns for Web Controls
-   **`XmlAssert`** - XML assertions

Unfortunately, the [MbUnit
wiki](http://www.mertner.com/confluence/display/MbUnit/Assertions "MbUnit wiki assertions")
is sparse on documentation for these classes (volunteers are always
welcome to flesh out the docs!). But the methods are very well named and
using Intellisense, it is quite easy to figure out what each method of
these classes does.

Using these specialized assertion classes can dramatically cut down the
amount of boilerplate test code you write to test your methods.

Keep in mind, that if you need the option to port your tests to NUnit in
the future (not sure why you’d want to once you have a taste of MbUnit)
you are better off sticking with the Assert class, as it has parity with
the NUnit implementation. These specialized assertion classes are
specific to MbUnit (and one good reason to choose MbUnit for your unit
testing needs).

