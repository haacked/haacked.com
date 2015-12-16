---
layout: post
title: "To String or to string"
date: 2015-12-16 -0800
comments: true
categories: [csharp style code]
---

Like many developers, I have many strong opinions about things that really do not matter. Even worse, I [have the vanity](http://haacked.com/archive/2004/10/08/bloggingispurevanity.aspx/) to believe other developers want to read about it.

For example, a recent [Octokit.net pull request](https://github.com/octokit/octokit.net/pull/1012) changed all instances of `String` to `string`. As a reminder, `String` actually represents the type `System.String` and `string` is the nice C# alias for `System.String`. To the compiler, these are the _exact same thing_. So ultimately, it doesn't really matter.

However, as I just said, __I care__. I care about things that don't matter.

Resharper, a tool we use to maintain our code conventions, by default suggests changing all `String` to `string`. But this doesn't fit the convention we follow. It probably helps to think about why are there aliases in the first place for these types.

I'm not really sure, but types like `string` and `int` feel special to me. They feel like primitives. In C#, [keywords are lowercase](https://msdn.microsoft.com/en-us/library/x53a06bb.aspx). So in my mind, when we're using these types in this manner, they should be lowercased. Any time they're declaring a return type, parameter type, etc.

For example,

```csharp
string foo;
string SomeMethod();
void AnotherMethod(string x);
public string Foo { get; }
```

But when we're using it to call static methods, I want it to look like a normal class.

```csharp
var foo = String.Empty;
String.Format("blah{0}", "blah");
var baz = new String();
```

Maybe I'm being too nitpicky   about this. For those of you who also care about unimportant things, I'm curious to hear what your thoughts on this matter. Is it possible to configure R# or other tools to enforce this convention?
