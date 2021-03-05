---
title: How Duck Typing Benefits C# Developers
tags: [csharp,code,patterns]
redirect_from: "/archive/2007/08/18/why-duck-typing-matters-to-c-developers.aspx/"
---

[David Meyer](http://www.deftflux.net/blog/ "David Meyer’s Blog - deft flux")
recently published a [.NET class library](http://www.deftflux.net/blog/page/Duck-Typing-Project.aspx "Duck Typing Project")
that enables *[duck typing](http://en.wikipedia.org/wiki/Duck_typing "Duck typing on Wikipedia")* (also sometimes [incorrectly described as *Latent Typing*](http://www.mindview.net/WebLog/log-0051 "Latent Typing") as [Ian Griffiths](http://www.interact-sw.co.uk/iangblog/ "Ian Griffith's Blog") explains in his campaign to [disabuse that notion](http://www.interact-sw.co.uk/iangblog/2005/01/06/notlatent "Not Latent")) for .NET languages.

The term *duck typing* is popularly explained by the phrase

> If it walks like a duck and quacks like a duck, it must be a duck.

For most dynamic languages, this phrase is slightly inaccurate in
describing duck typing. To understand why, let’s take a quick look at
what duck typing is about.

## Duck Typing Explained

[![duck-rabbit-phil](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WhyDuckTypingMattersInC_919F/duckrabbitphil_thumb.png)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WhyDuckTypingMattersInC_919F/duckrabbitphil.png "Rabbit or Duck?")Duck
typing allows an object to be passed in to a method that expects a
certain type even if it doesn’t inherit from that type. All it has to do
is support the methods and properties of the expected type *in use by
the method*.

I emphasize that last phrase for a reason. Suppose we have a method that
takes in a `duck` instance, and another method that takes in a `rabbit`
instance. In a dynamically typed language that supports duck typing, I
can pass in my object to the first method as long as my object supports
the methods and properties of `duck` in use by that method. Likewise, I
can pass my object into the second method as long as it supports the
methods and properties of `rabbit` called by the second method. Is my
object a duck or is it a rabbit? Like the above image, it’s neither and
it’s both.

In many (if not most) dynamic languages, **my object does not have to
support *all* methods and properties of `duck` to be passed into a
method that expects a `duck`**. Same goes for a method that expects
a `rabbit. `It only needs to support the methods and properties of the
expected type that are actually called by the method.

## The Static Typed Backlash

Naturally, static typing proponents have formed a backlash against
dynamic typing, claming that all hell will break loose when you give up
static typing. A common reaction (*and I paraphrase*) to David’s duck
typing project goes something like

> Give me static types or give me death!

Now I love compiler checking as much as the next guy, but I don’t
understand this attitude of completely dismissing a style of programming
that so many are fawning over.

Well, actually I do understand...kinda. So many programmers were burned
by their days of programming C (among other languages) and its type
unsafety which caused many stupid runtime errors that it’s been drilled
into their heads that static types are good, just, and the American way.

And for the most part, it’s true, but making this an absolute starts to
smell like the [monkey cage
experiment](http://www.safetycenter.navy.mil/Articles/a-m/monkeys.htm "Monkey Cage Experiment") in
that we ignore changes in software languages and tooling that might
challenge the original reasons we use static types because *we’ve done
it this way for so long*.

I think Bruce Eckel’s thoughts on [challenging preconceived
notions](http://www.mindview.net/WebLog/log-0053 "I’m over it - analysis of latent typing")
surrounding dynamic languages are spot on (emphasis mine).

> What I’m trying to get to is that in my experience there’s a balance
> between the value of strong static typing and the resulting impact
> that it makes on your productivity. The argument that "strong static
> is obviously better" is generally made by folks who haven’t had the
> experience of being dramatically more productive in an alternative
> language. When you have this experience, you see that the **overhead
> of strong static typing isn’t always beneficial**, because sometimes
> it slows you down enough that it ends up having a big impact on
> productivity.

The key point here is that static typing doesn’t come without a cost.
And that cost has to be weighed on a case by case basis against the
benefits of dynamic languages.

## C# has used duck typing for a long time

Interestingly enough, certain features of C# already use duck typing.
For example, to allow an object to be enumerated via the C# `foreach`
operator, the object only needs to implement a set of methods as
[Krzystof
Cwalina](http://blogs.msdn.com/kcwalina/ "Designing Reusable Frameworks")
of Microsoft points out in [this
post](http://blogs.msdn.com/kcwalina/archive/2007/07/18/DuckNotation.aspx "Duck Notation")...

> Provide a public method `GetEnumerator` that takes no parameters and
> returns a type that has two members: a) a method `MoveNext` that takes
> no parameters and return a Boolean, and b) a property `Current` with a
> getter that returns an `Object`.

You don’t have to implement an interface to make your object enumerable
via the `foreach` operator.

 

## A Very Useful Use Case For When You Might Use Duck Typing

 

If you’ve followed my blog at all, you know that I’ve gone through all
sorts of contortions to try and mock the `HttpContext` object via [the
`HttpSimulator`
class](https://haacked.com/archive/2007/06/19/unit-tests-web-code-without-a-web-server-using-httpsimulator.aspx "Unit Test Web Code").
The problem is that I can’t use a mock framework because `HttpContext`
is a sealed class and it doesn’t implement an interface that is useful
to me.

Not only that, but the properties of `HttpContext` I’m interested in
(such as `Request` and `Response`) are sealed classes (`HttpRequest` and
`HttpResponse` respectively). This makes it awful challenging to mock
these objects for testing. More importantly, it makes it hard to switch
to a different type of context should I want to reuse a class in a
different context such as the command line. Code that uses these classes
have a strong dependency on these classes and I’d prefer looser coupling
to the `System.Web` assembly.

The common approach to breaking this dependency is to create your own
`IContext` interface and then create another class that implements that
interface and essentially forwards method calls to an internal private
instance of the actual `HttpContext`. This is effectively a combination
of the composition and adapter pattern.

The problem for me is this is a lot more code to maintain just to get
around the constraints caused by static typing. Is all this additional
code worth the headache?

With the .NET Duck Typing class, I can reduce the code by a bit. Here’s
some code that demonstrates. First I create interfaces with the
properties I’m interested. In order to keep this sample short, I’m
choosing two interfaces each with one property..

```csharp
public interface IHttpContext
{
  IHttpRequest Request { get;}
}

public interface IHttpRequest
{
  Uri Url { get;}
}
```

Now suppose my code had a method that expects an `HttpContext` to be
passed in, thus tightly coupling our code to `HttpContext`. We can break
that dependency by changing that method to take in an instance of the
interface we created, `IHttpContext,` instead.

```csharp
public void MyMethod(IHttpContext context)
{
  Console.WriteLine(context.Request.Url);
}
```

The caller of `MyMethod` can now pass in the real `HttpContext` to this
method like so...

```csharp
IHttpContext context = DuckTyping.Cast<IHttpContext>(HttpContext.Current);
MyMethod(context);
```

What’s great about this is that the code that contains the `MyMethod`
method is no longer tightly coupled to the `System.Web` code and does
not need to reference that assembly. Also, I didn’t have to write a
class that implements the `IHttpContext` interface and wraps and
forwards calls to the private `HttpContext `instance, saving me a lot of
typing (*no pun intended*).

Should I decide at a later point to pass in a custom implementation of
`IHttpContext` rather than the one in `System.Web`, I now have that
option.

Yet another benefit is that I can now test `MyMethod` using a mock
framework such as
[RhinoMocks](http://www.ayende.com/projects/rhino-mocks.aspx "A dynamic mock object framework") like
so...

```csharp
MockRepository mocks = new MockRepository();
IHttpContext mockContext;
using (mocks.Record())
{
  mockContext = mocks.DynamicMock<IHttpContext>();
  IHttpRequest request = mocks.DynamicMock<IHttpRequest>();
  SetupResult.For(mockContext.Request).Return(request);
  SetupResult.For(request.Url).Return(new Uri("https://haacked.com/"));
}
using (mocks.Playback())
{
  MyMethod(mockContext);
}
```

You might wonder if I can go the opposite direction. Can I write my own
version of `HttpContext` and using duck typing cast it to `HttpContext`?
I tried that and it didn’t work. I believe that’s because `HttpContext`
is a sealed class and I think the Duck Typing Project generates a
dynamic proxy that inherits from the type you pass in. Since we can’t
inherit from a sealed class, we can’t simply cast a compatible type to
`HttpContext`. The above examples work because we’re duck type casting
to an interface.

With C#, if you need a class you’re writing to act like both a `duck`
and a `rabbit`, it makes sense to implement those interfaces. But
sometimes you need a class you didn’t write and cannot change (such as
the Base Class Libraries) to act like a `duck`. In that case, this duck
typing framework is a useful tool in your toolbox.
