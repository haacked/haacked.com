---
title: Writing Unit Tests For Controller Actions
date: 2007-12-09 -0800
disqus_identifier: 18434
categories:
- asp.net
- code
- asp.net mvc
- tdd
redirect_from: "/archive/2007/12/08/writing-unit-tests-for-controller-actions.aspx/"
---

UPDATE: Completely ignore the contents of this post. All of this is
out-dated. Test specific subclasses are no longer necessary with ASP.NET
MVC since our [April CodePlex
refresh](http://weblogs.asp.net/scottgu/archive/2008/04/16/asp-net-mvc-source-refresh-preview.aspx "April source refresh")

Just a brief note on writing unit tests for controller actions. When
your action has a call to `RedirectToAction` or `RenderView `(yeah,
pretty much every action) be aware that these methods have dependencies
on various context objects.

If you attempt to mock these objects, you sometimes also have to mock
their dependencies and their dependencies' dependencies and so on,
depending on what you are trying to test. This is why I wrote my post on
[Test Specific
Subclasses](https://haacked.com/archive/2007/12/06/test-specific-subclasses-vs-partial-mocks.aspx "Test Specific Subclasses").
It provides an easier way to test some of these cases.

Some of these challenges are the nature of mocking and some of them are
due to protected methods that we realize we should probably make public.

In this post, I want to demonstrate a couple of unit test techniques for
testing controller actions for the [CTP release of the ASP.NET MVC
Framework](http://www.asp.net/downloads/3.5-extensions/ "ASP.NET 3.5 Extensions").
Remember, this is a CTP so all of this may change in the future. I will
be compiling testing patterns into a longer document on unit testing
patterns for ASP.NET MVC

### Controller with RedirectToAction

Here is the really simple controller we’ll test

```csharp
public class HomeController : Controller
{
  [ControllerAction]
  public void Index()
  {
    RenderView("Index");
  }

  [ControllerAction]
  public void About()
  {
    RedirectToAction("Index");
  }
}
```

We will test the `About` action.

**Test Specific Subclass Approach**

For the most part, when a test calls `RedirectToAction`, you just want
to no-op that method call. But if you want to verify that the action
that is being redirected to is the correct one, here's one way to test
it using a test-specific subclass.

```csharp
[Test]
public void VerifyAboutRedirectsToCorrectActionUsingTestSpecificSubclass()
{
  HomeControllerTester controller = new HomeControllerTester();
  controller.About();
  Assert.AreEqual("Index", controller.RedirectedAction
    , "Should have redirected to 'Index'.");
}

internal class HomeControllerTester : HomeController
{
  public string RedirectedAction { get; private set; }

  protected override void RedirectToAction(object values)
  {
    this.RedirectedAction = (string)values.GetType()
      .GetProperty("Action").GetValue(values, null);
  }
}
```

In this test I inherited from the controller I am testing, following the
[Test Specific Subclass
pattern](https://haacked.com/archive/2007/12/06/test-specific-subclasses-vs-partial-mocks.aspx "Test Specific Subclass")
(*Note: This pattern leaves a bad taste in some TDDers mouths. I am
aware of that. I still like it. But I already know some of you don’t*).

One thing that is really ugly is I had to resort to reflection to get
the `Action` we are redirecting to. This testing scenario will be fixed
in the next release. Just showing you how it is done now.

Mock Framework Approach

In this test, I will use
[RhinoMocks](http://www.ayende.com/projects/rhino-mocks.aspx "Rhino.Mocks")
to test the same thing as above.

```csharp
[Test]
public void VerifyAboutRedirectsToCorrectActionUsingMockViewFactory()
{
  RouteTable.Routes.Add(new Route
  {
    Url = "[controller]/[action]",
    RouteHandler = typeof(MvcRouteHandler)
  });

  HomeController controller = new HomeController();
    
  MockRepository mocks = new MockRepository();
  IHttpContext httpContextMock = mocks.DynamicMock<IHttpContext>();
  IHttpRequest requestMock = mocks.DynamicMock<IHttpRequest>();
  IHttpResponse responseMock = mocks.DynamicMock<IHttpResponse>();
  SetupResult.For(httpContextMock.Request).Return(requestMock);
  SetupResult.For(httpContextMock.Response).Return(responseMock);
  SetupResult.For(requestMock.ApplicationPath).Return("/");
  responseMock.Redirect("/Home/Index");

  RouteData routeData = new RouteData();
  routeData.Values.Add("Action", "About");
  routeData.Values.Add("Controller", "Home");
  ControllerContext contextMock = new 
    ControllerContext(httpContextMock, routeData, controller);
  mocks.ReplayAll();

  controller.ControllerContext = contextMock;
  controller.About();

  mocks.VerifyAll();
}
```

The mock test actually tests the final URL that we would be redirecting
to. You can verify this test is actually testing what I say it will by
changing the line with *"/Home/Index"* to something like
*"/Home/Index2"* and see that the test does fail.

### Controller With RenderView

Using the same controller class above, let’s write a test to make sure
the correct view is rendered.

**Using Test Specific Subclass**

```csharp
[Test]
public void VerifyIndexSelectsCorrectViewUsingTestSpecificSubclass()
{
  HomeControllerTester controller = new HomeControllerTester();
  controller.Index();
  Assert.AreEqual("Index", controller.SelectedViewName
    , "Should have selected 'Index'.");
}

internal class HomeControllerTester : HomeController
{
  public string SelectedViewName { get; private set; }
    
  protected override void RenderView(string viewName
    , string masterName, object viewData)
  {
    this.SelectedViewName = viewName;   
  }
}
```

**Using a Mock Framework**

UPDATE: Sorry, but the following test doesn’t work in the CTP. I had
compiled it against an interim build and not the CTP version. Apologies.
For this scenario, you pretty much have to use the subclass approach. We
will make this better in the next CTP.

```csharp
[Test]
public void VerifyIndexSelectsCorrectViewUsingMockViewFactory()
{
  MockRepository mocks = new MockRepository();
  IViewFactory mockViewFactory = mocks.DynamicMock<IViewFactory>();
  IView mockView = mocks.DynamicMock<IView>();
  IHttpContext httpContextMock = mocks.DynamicMock<IHttpContext>();

  HomeController controller = new HomeController();
  RouteData routeData = new RouteData();

  ControllerContext contextMock = new ControllerContext(httpContextMock
    , routeData, controller);

  Expect.Call(mockViewFactory.CreateView(contextMock, "Index"
    , string.Empty, controller.ViewData)).Return(mockView);
  Expect.Call(delegate { mockView.RenderView(null); }).IgnoreArguments();
    
  mocks.ReplayAll();

  controller.ControllerContext = contextMock;
  controller.ViewFactory = mockViewFactory;
  controller.Index();

  mocks.VerifyAll();
}
```

Please note that while the Rhino Mocks examples look like a lot of code,
on a real project I would build up a custom set of Extension methods to
effectively create a DSL (Domain Specific Language) for testing my
controllers.

I’ve already [started on this a
bit](https://haacked.com/archive/2007/11/05/rhino-mocks-extension-methods-mvc-crazy-delicious.aspx "Rhino Mocks + Extension Methods").
Hopefully together, we can build up a really nice library to make
testing controllers much more fluid.

In the meanwhile, we will also evaluate the sticking points when it
comes to writing tests and do our part to reduce the friction for TDD
scenarios.

 

Tags:
[aspnetmvc](http://technorati.com/tags/aspnetmvc/ "ASP.NET MVC tag") ,
[TDD](http://technorati.com/tags/TDD/ "TDD tag")

