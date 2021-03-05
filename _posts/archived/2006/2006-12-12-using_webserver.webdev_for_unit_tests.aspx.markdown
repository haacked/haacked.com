---
title: Using WebServer.WebDev For Unit Tests
tags: [code,tdd]
redirect_from: "/archive/2006/12/11/using_webserver.webdev_for_unit_tests.aspx/"
---

[![A Scanning Test - From
http://www.sxc.hu/photo/517386](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UsingWebServer.WebDevForUnitTests_7370/517386_scanning_test_thumb.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UsingWebServer.WebDevForUnitTests_7370/517386_scanning_test2.jpg)

Last night a unit test saved my life (*[with
apologies](http://en.wikipedia.org/wiki/Last_Night_a_DJ_Saved_My_Life_(song) "Last Night A DJ Saved My Life"))*.
Ok, maybe not my life, but the act of writing some unit tests did save
me the embarrasment of an obscure bug which was sure to hit when I least
expected it.  It is cases like this that made me into such a big fan of
writing automated unit tests.

Not too long ago I wrote a C# [Akismet
API](https://haacked.com/archive/2006/09/26/Subtext_Akismet_API.aspx "Subtext Akismet API")
for Subtext. In writing the code, I followed design principles focused
on making the API as testable as possible. For example, I applied
[Inversion of
Control](http://www.martinfowler.com/articles/injection.html "Inversion of Control")
(IOC) by having the `AkismetClient` constructor take in an `HttpClient`
instance as an argument. The `HttpClient` instance is responsible for
making the actual HTTP request.

This allowed me to use [Rhino
Mocks](http://www.ayende.com/projects/rhino-mocks.aspx "Rhino Mocks") to
replace the `HttpClient` with a mock enabling me to build unit
tests that ensured that the Akismet API was doing the right thing
without having to make any actual web requests.

Of course this approach only delays the inevitable. I still want to have
an automated test for the `HttpClient` class.

So last night, I took a step back and [revisited this excellent
post](http://www.hanselman.com/blog/NUnitUnitTestingOfASPNETPagesBaseClassesControlsAndOtherWidgetryUsingCassiniASPNETWebMatrixVisualStudioWebDeveloper.aspx "NUnit Unit Testing of ASP.NET Pages")
by [Scott
Hanselman](http://www.hanselman.com/blog/ "Scott Hanselman’s Blog") in
which he shows how to use Cassini in your unit tests. I decided to
update his pioneering approach to use the latest incarnation of Cassini,
WebServer.WebDev. I also decided to refactor what he did into a reusable
`TestWebServer` class in order to make the barrier to entry in using it
as low as possible.

Setup
-----

WebServer.WebDev is the built in Web Server (formerly known as Cassini)
used by Visual Studio.NET 2005. The main functionality of the server is
located in **WebDev.WebHost.dll**. You can find this assembly in the
GAC. On my machine, it is located in the following directory:

*c:\\WINDOWS\\assembly\\GAC\_32\\WebDev.WebHost\\8.0.0.0\_\_b03f5f7f11d50a3a\\*

Note that the .NET framework installs an explorer extension for the GAC
so you won’t see this directory using Windows Explorer. I navigated to
the directory using the command prompt.

Setting up the test web server is a two step process once you’ve located
the WebDev.WebHost assembly.

1.  **Copy WebDev.WebHost into your unit test project and add a
    reference to it.**
2.  **Add the
    [TestWebServer.cs](http://www.koders.com/csharp/fidD413C8AD118C221918653F02B78C85894EB55263.aspx?s=smtp+server "TestWebServer.cs class")
    file into your unit test project.**

*Note: To make this really reusable, you could drop this class into a
separate Unit Test Helper assembly that you reference in your unit test
projects. If you do go that route, be sure to heed the “//NOTE” I left
for you in the ExtractResources method.*

TestWebServer Usage
-------------------

The following shows a couple of ways you can use this test web server in
your own unit tests. If you have a single test in a fixture that needs
to use the server, you can do something like this:

```csharp
using (TestWebServer webServer = new TestWebServer())
{
    webServer.Start();
    webServer.ExtractResource("ResourcePath.SomePage.aspx"
      , "SomePage.aspx");
    string response = webServer.RequestPage("SomePage.aspx");
    Assert.AreEqual("Done", response);
}
```

If you have a set of tests that need to use the web server, I suggest
using the `[TestFixtureSetUp]` and `[TestFixtureTearDown]` attributes to
start the web server just once for all the tests.

```csharp
private TestWebServer webServer = new TestWebServer();
private Uri webServerUrl;

[TestFixtureSetUp]
public void TestFixtureSetUp()
{
    this.webServerUrl = this.webServer.Start();
}

[TestFixtureTearDown]
public void TestFixtureTearDown()
{
    if(this.webServer != null)
        this.webServer.Stop();
}
```

I added several helper methods to the TestWebServer class based on what
Scott did.

**`ExtractResource`** takes in the full path to an embedded resource and
extracts it into the test webroot. In the first code example above, I
extracted an embedded resource into a file named *SomePage.aspx*. Be
sure to call this method *after* the webserver is started.

**`RequestPage`** has two overloads. One which makes a simple GET
request to the test web server, and the other which makes a POST
request.

Discussion
----------

In the past, I have gone to great lengths to not using a web server to
unit test my code, as that takes us more into the realm of Integration
testing. A while ago I wrote a post on how to [simulate the HttpContext
for unit
tests](https://haacked.com/archive/2005/06/11/Simulating_HttpContext.aspx "Simulating HttpContext")
without using a web server. This approach has been improved upon in the
Subtext unit test codebase and has served me well.

But even that approach can only go so far. As I pointed out in my post
on a [Testing Mail
Server](https://haacked.com/archive/2006/05/30/ATestingMailServerForUnitTestingEmailFunctionality.aspx "Testing Mail Server"),
it’s a good thing to abstract out these extensibility points using an
interface or a provider. But at some point, you have to test the
concrete implementation. I can’t keep delegating functionality endlessly
to another abstraction. Somebody has to make a real HTTP request.

So consider this approach the method of last resort.

Download
--------

The test webserver class can be downloaded [here from my company’s tools
site](http://tools.veloc-it.com/tabid/58/grm2id/21/Default.aspx "Web Server for UnitTesting").

