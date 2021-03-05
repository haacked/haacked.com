---
title: Unit Test Web Code Without A Web Server Using HttpSimulator
tags: [aspnet,tdd]
redirect_from: "/archive/2007/06/18/unit-tests-web-code-without-a-web-server-using-httpsimulator.aspx/"
---

Testing code written for the web is challenging. Especially code that makes use of the ASP.NET intrinsic objects such as the `HttpRequest` object. **My goal is to make testing such code easier**.

[![Spider Web (c) FreeFoto.com](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WriteUnitTestsForTheWebWithoutAWebServer_13D45/01_17_8---Spiders-Web_web_1.jpg)](http://www.freefoto.com/preview/01-17-8?ffid=01-17-8 "Spider Web (c) FreeFoto.com")

A while ago, I wrote some code to [simulate the `HttpContext`](https://haacked.com/archive/2005/06/11/simulating_httpcontext.aspx/ "Simulate HttpContext for Unit Tests Without Using Cassini nor IIS") in order to make writing such unit tests easier. My goal wasn’t to replace web testing frameworks such as [Selenium](http://www.openqa.org/selenium/ "Selenium Web Testing Tool"), [Watin](http://watin.sourceforge.net/ "Watin"), or [AspUnit](http://aspunit.sourceforge.net/ "AspUnit"). Instead, I’m a fan of the [Pareto principle](http://en.wikipedia.org/wiki/Pareto_principle "Pareto Principle on Wikipedia")
and I hoped to help people easily reach the 80 of the 80/20 rule before reaching out to one of these tools to cover the last mile.

I’ve spent some time since then refactoring the code and improving the API. I also implemented some features that were lacking such as being able to call MapPath and setting and getting Session and Application
variables.

**To that end, I introduce the `HttpSimulator` class**. To best demonstrate how to use it, I will present some unit test code.

The following code simulates a simple GET request for the web root with the physical location *c:\\inetpub*. The actual path passed into the simulator doesn’t matter. It’s all simulated. This tests that you can set a session variable and then retrieve it.

```csharp
[Test]
public void CanGetSetSession()
{
  using (new HttpSimulator("/", @"c:\inetpub\").SimulateRequest())
  {
    HttpContext.Current.Session["Test"] = "Success";
    Assert.AreEqual("Success", HttpContext.Current.Session["Test"]);
  }
}
```

The following test method demonstrates two different methods for simulating a form post. The second using block shows off the fluent interface.

```csharp
[Test]
public void CanSimulateFormPost()
{
  using (HttpSimulator simulator = new HttpSimulator())
  {
    NameValueCollection form = new NameValueCollection();
    form.Add("Test1", "Value1");
    form.Add("Test2", "Value2");
    simulator.SimulateRequest(new Uri("http://localhost/Test.aspx"), form);

    Assert.AreEqual("Value1", HttpContext.Current.Request.Form["Test1"]);
    Assert.AreEqual("Value2", HttpContext.Current.Request.Form["Test2"]);
  }

  using (HttpSimulator simulator = new HttpSimulator())
  {
    simulator.SetFormVariable("Test1", "Value1")
      .SetFormVariable("Test2", "Value2")
      .SimulateRequest(new Uri("http://localhost/Test.aspx"));

    Assert.AreEqual("Value1", HttpContext.Current.Request.Form["Test1"]);
    Assert.AreEqual("Value2", HttpContext.Current.Request.Form["Test2"]);
  }
}
```

The `SimulateRequest` method is always called last once you’ve set your form or query string variables and whatnot. For read and write values such as session, you can set them after the call. If you download the code, you can see other usage examples in the unit tests.

One area I’ve had a lot of success with this class is in unit testing custom `HttpHandlers`. I’ve also use it to test custom control rendering code and helper methods for ASP.NET.

This code can be found in the `Subtext.TestLibrary` project in our Subversion repository. This project contains code I’ve found useful within my unit tests such as a [test SMTP
server](https://haacked.com/archive/2006/05/30/ATestingMailServerForUnitTestingEmailFunctionality.aspx "A Testing Mail Server for Unit Testing Email Functionality") and a [test Web Server using WebServer.WebDev](https://haacked.com/archive/2006/12/12/Using_WebServer.WebDev_For_Unit_Tests.aspx "Using WebServer.WebDev for Unit Tests").

**To make it easy for you to start using the `HttpSimulator`, I’ve packaged the [relevant files in a zip file](https://haacked.com/code/HttpSimulator.zip "HttpSimulator Code") including the unit tests.**

I must make one confession. I originally tried to do all this by using the public APIs. Unfortunately, so many classes are internal or sealed that I had to get my hands dirty and resort to using reflection. Doing so freed me up to finally get certain features working that I could not before.

**And now, for some preemptive answers to expected criticism.**

​1. *You shouldn’t access the `HttpContext` anyways. You should abstract away the `HttpContext` by creating your own `IContext` and using IoC and Dependency Injection.*

You’re absolutely right. Next criticism.

​2. *This isn’t "unit testing", this is "integration testing".*

Very astute observation. Well said. Next?

​3. *You’re not taking our criticisms seriously!*

Au contraire! I take such criticisms very seriously. Even if you write a bunch of code to abstract away the web from your code throwing all sorts of injections and inversions at it, you still have to test your abstraction. `HttpSimulator` to the rescue!

Likewise, whether this is *unit testing* or *integration testing* is splitting semantic hairs. Before TDD came along, *unit testing* meant testing a *unit* of code. It usually meant walking through the code line by line and executing a single function. If you want to call these integration tests, fine. `HttpSimulator` to the rescue!

Not to mention that in the real world, you sometimes don’t get to write code from scratch using sound TDD principles. A lot of time you inherit legacy code and the best you can do is try to write tests after-the-fact as you go before you refactor the code. Again, `HttpSimulator` to the rescue!

**Here is the link to**[**download the source files**](http://code.haacked.com/util/HttpSimulator.zip "HttpSimulator source files")** in case you missed it the first time.**

