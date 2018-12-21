---
title: Versioning Issues With Optional Arguments
date: 2010-08-10 -0800
tags:
- code
redirect_from: "/archive/2010/08/09/versioning-issues-with-optional-arguments.aspx/"
---

One nice new feature introduced in C# 4 is support for [named and
optional
arguments](http://msdn.microsoft.com/en-us/library/dd264739.aspx "Named and Optional Arguments (MSDN)").
While these two features are often discussed together, they really are
orthogonal concepts.

Let’s look at a quick example of these two concepts at work. Suppose we
have a class with one method having the following signature.

```csharp
  // v1
  public static void Redirect(string url, string protocol = "http");
```

This hypothetical library contains a single method that takes in two
parameters, a required `string url` and an optional `string protocol`.

The following shows the six possible ways this method can be called.

```csharp
HttpHelpers.Redirect("https://haacked.com/");
HttpHelpers.Redirect(url: "https://haacked.com/");
HttpHelpers.Redirect("https://haacked.com/", "https");
HttpHelpers.Redirect("https://haacked.com/", protocol: "https");
HttpHelpers.Redirect(url: "https://haacked.com/", protocol: "https");
HttpHelpers.Redirect(protocol: "https", url: "https://haacked.com/");
```

Notice that whether or not a parameter is optional, you can choose to
refer to the parameter by name or not. In the last case, notice that the
parameters are specified out of order. In this case, using named
parameters is required.

### The Next Version

One apparent benefit of using optional parameters is that you can reduce
the number of overloads your API has. However, relying on optional
parameters does have its quirks you need to be aware of when it comes to
versioning.

Let’s suppose we’re ready to make version two of our awesome
`HttpHelpers` library and we add an optional parameter to the existing
method.

```csharp
// v2
public static void Redirect(string url, string protocol = "http",   bool permanent = false);
```

What happens when we try to execute the client without recompiling the
client application?

We get a the following exception message.

    Unhandled Exception: System.MissingMethodException: Method not found: 'Void HttpLib.HttpHelpers.Redirect(System.String, System.String)'....

Whoops! By changing the method signature, we caused a runtime breaking
change to occur. That’s not good.

Let’s try to avoid a runtime breaking change by adding an overload
instead of changing the existing method.

```csharp
// v2.1
public static void Redirect(string url, string protocol = "http");
public static void Redirect(string url, string protocol = "http",   bool permanent = false);
```

Now, when we run our client application, it works fine. It’s still
calling the two parameter version of the method. Adding overloads is
never a *runtime* breaking change.

But let’s suppose we’re now ready to update the client application and
we attempt to recompile it. Uh oh!

    The call is ambiguous between the following methods or properties: 'HttpLib.HttpHelpers.Redirect(string, string)' and 'HttpLib.HttpHelpers.Redirect(string, string, bool)'

While adding an overload is not a *runtime* breaking change, it can
result in a compile time breaking change. Doh!

Talk about a catch-22! If we add an overload, we break in one way. If we
instead add an argument to the existing method, we’re broken in another
way.

### Why Is This Happening?

When I first heard about optional parameter support, I falsely assumed
it was implemented as a feature of the CLR which might allow dynamic
dispatch to the method. This was perhaps very naive of me.

My co-worker Levi (no blog still) broke it down for me as follows. Keep
in mind, he’s glossing over a lot of details, but at a high level, this
is roughly what’s going on.

When optional parameters are in use, the C# compiler follows a simple
algorithm to determine which overload of a method you actually meant to
call. It considers as a candidate \*every\* overload of the method, then
one by one it eliminates overloads that can’t possibly work for the
particular parameters you’re passing in.

Consider these overloads:

```csharp
public static void Blah(int i);
public static void Blah(int i, int j = 5);
public static void Blah(string i = "Hello"); 
```

Suppose you make the following method call: `Blah(0)`.

The last candidate is eliminated since the parameter types are
incorrect, which leaves us with the first two.

```csharp
public static void Blah(int i); // Candidate
public static void Blah(int i, int j = 5); // Candidate
public static void Blah(string i = "Hello");  // Eliminated
```

At this point, the compiler needs to perform a conflict resolution. The
conflict resolution is very simple: if one of the candidates has the
same number of parameters as the call site, it wins. Otherwise the
compiler bombs with an error.

In the case of `Blah(0)`, the first overload is chosen since the number
of parameters is exactly one.

```csharp
public static void Blah(int i); //WINNER!!!
public static void Blah(int i, int j = 5);
public static void Blah(string i = "Hello"); 
```

This allows you to take an existing method that doesn’t have optional
parameters and add overloads that have optional parameters without
breaking anybody (except in Visual Basic which has a slightly different
algorithm).

But what happens if you need to version an API that already has optional
parameters?  Consider this example:

```csharp
public static void Helper(int i = 2, int j = 3);            // v1
public static void Helper(int i = 2, int j = 3, int k = 4); // added in v2
```

And say that the call site is `Helper(j: 10)`. Both candidates still
exist after the elimination process, but since neither candidate has
exactly one argument, the compiler will not prefer one over another.
This leads to the compilation error we saw earlier about the call being
ambiguous.

### Conclusion

The reason that optional parameters were introduced to C# 4 in the
first place was to support COM interop. That’s it. And now, we’re
learning about the full implications of this fact.

If you have a method with optional parameters, you can never add an
overload with additional optional parameters out of fear of causing a
compile-time breaking change. And you can never remove an existing
overload, as this has always been a runtime breaking change.

You pretty much need to treat it like an interface. Your only recourse
in this case is to write a new method with a new name.

So be aware of this if you plan to use optional arguments in your APIs.

**UPDATE:** By the way, you **can** add overloads that have additional
**required** parameters. So in this way, you are in the same boat as
before. However, this can lead to other subtle versioning issues as my
[follow-up post
describes](https://haacked.com/archive/2010/08/12/more-optional-versioning-fun.aspx "More versioning fun with optional arguments").

