---
title: IHttpContext And Other Interfaces For Your Duck Typing Benefit
tags: [code]
redirect_from: "/archive/2007/09/08/ihttpcontext-and-other-interfaces-for-your-duck-typing-benefit.aspx/"
---

Not too long ago I wrote a blog post on some of the [benefits of Duck Typing](https://haacked.com/archive/2007/08/19/why-duck-typing-matters-to-c-developers.aspx "How Duck Typing Benefits C# Developers")
for C# developers. In that post I wrote up a simplified code sample demonstrating how you can cast the `HttpContext` to an interface you
create called `IHttpContext`, for lack of a better name.

![Is it a duck or a rabbit?](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WhyDuckTypingMattersInC_919F/duckrabbitphil_thumb.png)Well I couldn’t just sit still on that one so I used Reflector and a lot of patience and created a set of interfaces to match the Http intrinsic classes. Here is a full list of interfaces I created along with the concrete existing class (all in the `System.Web` namespace except where otherwise stated) that can be cast to the interface (ones in bold are the most commonly used.

-   `ICache` - *Cache*
-   `IHttpApplication` - *HttpApplication*
-   `IHttpApplicationState` - *HttpApplicationState*
-   `IHttpCachePolicy` - *CachePolicy*
-   `IHttpClientCertificate` - *HttpClientCertificate*
-   **`IHttpContext` -***HttpContext*
-   `IHttpFileCollection` - *HttpFileCollection*
-   `IHttpModuleCollection` - *HttpModuleCollection*
-   **`IHttpRequest`** - *HttpRequest*
-   **`IHttpResponse`** - *HttpResponse*
-   **`IHttpServerUtility`** - *HttpServerUtility*
-   **`IHttpSession`**- *System.Web.SessionState.HttpSessionState*
-   `ITraceContext` - *TraceContext*

As an aside, you might wonder why I chose the name `IHttpSession` instead of `IHttpSessionState `for the class `HttpSessionState`. It
turns out that there already is an `IHttpSessionState` interface, but `HttpSessionState` doesn’t inherit from that interface. Go figure. Now
that’s a juicy tidbit you can whip out at your next conference cocktail party.

Note that I focused on classes that don’t have public constructors and are sealed. I didn’t want to follow the *entire* object graph!

I also wrote a simple `WebContext` class with some helper methods. For example, to get the current `HttpContext` duck typed as `IHttpContext`, you simply call...

```csharp
IHttpContext context = WebContext.Current;
```

I also added a bunch of `Cast` methods specifically for casting http intrinsic types. Here’s some demo code to show this in action. Assume
this code is running in the code behind of your standard ASPX page.

```csharp
public void HelloWorld(IHttpResponse response)
{
  response.Write("<p>Who’s the baddest!</p>");
}

protected void Page_Load(object sender, EventArgs e)
{
  //Grab it from the http context.
  HelloWorld(WebContext.Current.Response);
  
  //Or cast the actual Response object to IHttpResponse
  HelloWorld(WebContext.Cast(Response));
}
```

The goal of this library is to make it very easy to refactor existing code to use these interfaces (should you so desire), which will make
your code less tied to the `System.Web` classes and more mockable.

Why would you want such a thing? Making classes mockable makes them easier to test, that’s a worthy goal in its own right. Not only that,
this gives control over dependencies to you, as a developer, rather than having your code tightly coupled to the System.Web classes. One
situation I’ve run into is wanting to write a command line tool to administer Subtext on my machine. Being able to substitute my own
implementation of `IHttpContext` will make that easier.

UPDATE: The stack overflow problem mentioned below has since been fixed within the Duck Typing library.

One other note as you look at the code. You might notice I’ve had to create extra interfaces (commented with a //Hack). This works around a
bug I found with the Duck Casting library reproduced with this code...

```csharp
public class Foo
{
  public Foo ChildFoo
  {
    get { return new Foo();}
  }
}

public interface IFoo
{
  //Note this interface references itself
  IFoo ChildFoo { get;}
}

public static class FooTester
{
  public static void StackOverflowTest()
  {
    Foo foo = new Foo();
    IFoo fooMock = DuckTyping.Cast<IFoo>(foo);
    Console.WriteLine(fooMock);
  }
}
```

`Calling FooTester.StackOverflowTest` will cause a stack overflow
exception. The fix is to do the following.

```csharp
public interface IFoo2 : IFoo {}

public class IFoo
{
  IFoo2 ChildFoo { get; }
}
```

In any case, I hope some of you find this useful. Let me know if you find any bugs or mistakes. No warranties are implied. [Download the
code](https://haacked.com.nyud.net/code/HttpInterfaces.zip "Http Interfaces Code") from here which includes the `HttpInterfaces` class library with all the interfaces, a Web Project with a couple of tests, and a unit test
library with more unit tests.
