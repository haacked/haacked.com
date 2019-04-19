---
title: "To String or to string"
date: 2015-12-16 -0800
tags: [csharp,design,code]
---

Like many developers, I have many strong opinions about things that really do not matter. Even worse, I [have the vanity](https://haacked.com/archive/2004/10/08/bloggingispurevanity.aspx/) to believe other developers want to read about it.

For example, a recent [Octokit.net pull request](https://github.com/octokit/octokit.net/pull/1012) changed all instances of `String` to `string`. As a reminder, `String` actually represents the type `System.String` and `string` is the nice C# alias for `System.String`. To the compiler, these are the _exact same thing_. So ultimately, it doesn't really matter.

However, as I just said, __I care__. I care about things that don't matter.

Resharper, a tool we use to maintain our code conventions, by default suggests changing all `String` to `string`. But this doesn't fit the convention we follow.

To understand the convention I have in my head, it probably helps to think about why are there aliases in the first place for these types.

I'm not really sure, but types like `string` and `int` feel special to me. They feel like primitives. In C#, [keywords are lowercase](https://msdn.microsoft.com/en-us/library/x53a06bb.aspx). So in my mind, when we're using these types in this manner, they should be lowercase. Thus my convention is to lowercase `string` and `int` any time we're using them in a manner where they feel like a keyword.

For example,

```csharp
int x;
string foo;
string SomeMethod();
void AnotherMethod(string x);
public string Foo { get; }
```

But when we're using it to call static methods or calling constructors on these types, I want it to look like a normal type. These constructs don't look like you're using a keyword.

```csharp
Int32.Parse("42");
var foo = String.Empty;
String.Format("blah{0}", "blah");
var baz = new String();
```

Maybe I'm being too nitpicky about this. For those of you who also care about unimportant things, I'm curious to hear what your thoughts on this matter. Is it possible to configure R# or other tools to enforce this convention?

__UPDATE__

One of the reasons I follow this convention is for readability. When things don't fit my expectations a tiny bit of extra processing is needed. So as I'm scanning code in Visual Studio and I see `keyword.MethodCall` it takes a second.

Having said that, a commenter noted that the corefx [coding guidelines always use lowercase](https://github.com/dotnet/corefx/blob/master/Documentation/coding-guidelines/coding-style.md). And one of my principles with coding conventions is I tend to adopt something I feel is something of a widespread standard even if I disagree with it because at the end of the day, these are all mostly arbitrary and rather than spend a lot of time arguing, I'd rather just point to something and say "We're doing it that way because why not?" That's why I tend to follow the Framework Design Guidelines for example.

Having said that, I still need to let this one sink in.

__DOUBLE UPDATE__

Jared Parson who works on the C# compiler notes that `String` vs `string` is not a style debate. He makes a compelling case for why [you should always use `string`](https://blog.paranoidcoding.com/2019/04/08/string-vs-String-is-not-about-style.html).
