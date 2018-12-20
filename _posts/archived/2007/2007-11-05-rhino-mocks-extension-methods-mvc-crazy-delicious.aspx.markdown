---
title: Rhino Mocks + Extension Methods + MVC == Crazy Delicious
date: 2007-11-05 -0800
disqus_identifier: 18418
tags:
- aspnet
- code
- aspnetmvc
- tdd
redirect_from: "/archive/2007/11/04/rhino-mocks-extension-methods-mvc-crazy-delicious.aspx/"
---

UPDATE: This content is a bit outdated as these interfaces have changed
in ASP.NET MVC since the writing of this post.

One task that I relish as a PM on the ASP.NET MVC project is to build
code samples and sample applications to put the platform through its
paces and try to suss out any problems with the design or usability of
the API.

Since testability is a key goal of this framework, I’ve been trying to
apply a Test Driven Development (TDD) approach as I build out the sample
applications. This has led to some fun discoveries in terms of using new
language features of C\# to improve my tests.

For example, the MVC framework will include interfaces for the ASP.NET
intrinsics. So to mock up the HTTP context using Rhino Mocks, you might
do the following.

```csharp
MockRepository mocks = new MockRepository();
      
IHttpContext context = mocks.DynamicMock<IHttpContext>();
IHttpRequest request = mocks.DynamicMock<IHttpRequest>();
IHttpResponse response = mocks.DynamicMock<IHttpResponse>();
IHttpServerUtility server = mocks.DynamicMock<IHttpServerUtility>();
IHttpSessionState session = mocks.DynamicMock<IHttpSessionState>();

SetupResult.For(context.Request).Return(request);
SetupResult.For(context.Response).Return(response);
SetupResult.For(context.Server).Return(server);
SetupResult.For(context.Session).Return(session);

mocks.ReplayAll();
//Ready to use the mock now
```

Kind of a mouthful, no?

Then it occurred to me. I should use C\# 3.0 Extension Methods to create
a mini DSL (to abuse the term) for building HTTP mock objects. First, I
wrote a simple proof of concept class with extension methods.

```csharp
public static class MvcMockHelpers
{
  public static IHttpContext 
    DynamicIHttpContext(this MockRepository mocks)
  {
    IHttpContext context = mocks.DynamicMock<IHttpContext>();
    IHttpRequest request = mocks.DynamicMock<IHttpRequest>();
    IHttpResponse response = mocks.DynamicMock<IHttpResponse>();
    IHttpSessionState session = mocks.DynamicMock<IHttpSessionState>();
    IHttpServerUtility server = mocks.DynamicMock<IHttpServerUtility>();

    SetupResult.For(context.Request).Return(request);
    SetupResult.For(context.Response).Return(response);
    SetupResult.For(context.Session).Return(session);
    SetupResult.For(context.Server).Return(server);

    mocks.Replay(context);
    return context;
  }

  public static void SetFakeHttpMethod(
    this IHttpRequest request, string httpMethod)
  { 
    SetupResult.For(request.HttpMethod).Return(httpMethod);
  }
}
```

And then I rewrote the setup part for the test (the rest of the test is
omitted for brevity).

```csharp
MockRepository mocks = new MockRepository();
IHttpContext context = mocks.DynamicIHttpContext();
context.Request.SetFakeHttpMethod("GET");
mocks.ReplayAll();
```

That’s much cleaner, isn’t it?

Please note that I call the `Replay` method on the `IHttpContext` mock.
That means you won’t be able to setup any more expectations on the
context. But in most cases, you won’t need to.

This is just a proof-of-concept, but I could potentially add a bunch of
`SetFakeXYZ` extension methods on the various intrinsics to make setting
up expectations and results much easier. I chose the pattern of using
the `SetFake` prefix to help differentiate these test helper methods.

Note that this technique isn’t specific to ASP.NET MVC. As you start to
build apps with \#C 3.0, you can build extensions for commonly used
mocks to make it easier to write unit tests with mocked objects. That
takes a lot of the drudgery out of setting up a mocked object.

Oh, and if you’re lamenting the fact that you’re writing ASP.NET 2.0
apps that don’t have interfaces for the HTTP intrinsics, you should read
my post on [IHttpContext and Duck
Typing](https://haacked.com/archive/2007/09/09/ihttpcontext-and-other-interfaces-for-your-duck-typing-benefit.aspx "IHttpContext and other interfaces for your Duck Typing Benefit")
in which I provide such interfaces.

Happy testing to you!

I have a [follow-up post on testing
routes](https://haacked.com/archive/2007/12/17/testing-routes-in-asp.net-mvc.aspx "unit testing routes").
The project includes a slightly more full featured version of the
`MvcMockHelpers class. `

