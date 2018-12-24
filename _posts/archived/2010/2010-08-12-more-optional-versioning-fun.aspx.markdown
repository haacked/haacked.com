---
title: More Versioning Fun With Optional Arguments
date: 2010-08-12 -0800
tags: [code,versioning,dotnet]
redirect_from: "/archive/2010/08/11/more-optional-versioning-fun.aspx/"
---

In my [last blog
post](https://haacked.com/archive/2010/08/10/versioning-issues-with-optional-arguments.aspx "Versioning Issues with Optional Arguments"),
I covered some challenges with versioning methods that differ only by
optional parameters. If you haven’t read it, go read it. If I do say so
myself, it’s kind of interesting. ;) In this post, I want to cover
another very subtle versioning issue with using optional parameters.

At the very end of that last post, I made the following comment.

> By the way, you **can** add overloads that have additional
> **required**parameters. So in this way, you are in the same boat as
> before.

However, this can lead to subtle bugs. Let’s walk through a scenario.
Imagine that some class library has the following method in version 1.0.

```csharp
public static void Foo(string s1, string s2, string s3 = "v1") {
    Console.WriteLine("version 1");
}
```

And you have a client application which calls this method like so:

```csharp
ClassName.Foo("one", "two");
```

That’s just fine right. You don’t need to supply a value for the
argument *s3* because its optional. Everything is hunky dory!

But now, the class library author decides to release version 2 of the
library and adds the following overload.

```csharp
public static void Foo(string s1, string s3 = "v2") {
    Console.WriteLine("version 2");
}

public static void Foo(string s1, string s2, string s3 = "v1") {
    Console.WriteLine("version 1");
}
```

Notice that they’ve added an overload that only has two parameters. It
differs from the existing method by one required parameter, which is
allowed.

As I mentioned before, you’re always allowed to add overloads and
maintain binary compatibility. So if you don’t recompile your client
application and upgrade the class library, you’ll still get the
following output when you run the application.

    version 1

But what happens when you recompile your client application against
version 2 of the class library and run it again with no source code
changes. The output becomes:

    version 2

Wow, that’s pretty subtle.

It may not seem so bad in this contrived example, but lets contemplate a
real world scenario. Let’s suppose there’s a very commonly used utility
method in the .NET Framework that follows this pattern in .NET 4. And in
the next version of the framework, a new overload is added with one less
required parameter.

Suddenly, when you recompile your application, every call to the one
method is now calling the new one.

Now, I’m not one to be alarmist. Realistically, this is probably very
unlikely in the .NET Framework because of stringent backwards
compatibility requirements. Very likely, if such a method overload was
introduced, calling it would be backwards compatible with calling the
original.

But the same discipline might not apply to every library that you depend
on today. It’s not hard to imagine that such a subtle versioning issue
might crop up in a commonly used 3rd party open source library and it
would be very hard for you to even know it exists without testing your
application very thoroughly.

The moral of the story is, **you do write unit tests dontcha? Well
dontcha?!**If not, now’s a good time to start.

