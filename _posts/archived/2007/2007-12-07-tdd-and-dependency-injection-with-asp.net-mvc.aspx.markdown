---
title: TDD and Dependency Injection with ASP.NET MVC
tags: [aspnet,code,aspnetmvc,tdd]
redirect_from: "/archive/2007/12/06/tdd-and-dependency-injection-with-asp.net-mvc.aspx/"
---

One of the guiding principles in the design of the new [ASP.NET MVC
Framework](http://asp.net/mvc "ASP.NET MVC Page") is enabling TDD (Test
Driven Development) when building a web application. If you want to
follow along, this post makes use of ASP.NET MVC CodePlex Preview 4
which [you’ll need to
install](http://codeplex.com/aspnet "ASP.NET on CodePlex") from
CodePlex. I’ll try and keep this post up to date with the latest
releases, but it may take me time to get around to it.

This post will provide a somewhat detailed walk-through of building a
web application in a Test Driven manner while also demonstrating one way
to integrate a [Dependency
Injection](http://www.martinfowler.com/articles/injection.html "Inversion of Control Containers and the Dependency Injection Pattern")
(DI) Framework into an ASP.NET MVC app. At the very end, you can
download the code.

I chose [StructureMap
2.0](http://structuremap.sourceforge.net/Default.htm "StructureMap homepage")
for the DI framework simply because I’m familiar with it and it requires
very little code and configuration. If you’re interested in an example
using [Spring.NET](http://www.springframework.net/ "Spring.NET"), check
out [Fredrik Normen’s
post](http://weblogs.asp.net/fredriknormen/archive/2007/11/17/asp-net-mvc-framework-create-your-own-icontrollerfactory-and-use-spring-net.aspx "ASP.NET MVC Framework Create your own IControllerFactory").
I’ll try and post code examples in the future using [Castle
Windsor](http://www.castleproject.org/container/index.html "Caste Windsor")
and
[ObjectBuilder](http://www.codeplex.com/ObjectBuilder "ObjectBuilder on CodePlex").

## Start Me Up! *with apologies to The Rolling Stones*

Once the CTP is released and you have it installed, open Visual Studio
2008 and select the *File* | *New Project*menu item. In the dialog,
select the *ASP.NET MVC Web Application*project template.

![New
Project](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TDDandDependencyInjectionwithASP.NETMVC_EFA7/New%20Project_3.png "New Project")

At this point, you should see the following unit test project selection
dialog.

![Select Unit Test
Project](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TDDandDependencyInjectionwithASP.NETMVC_EFA7/unit-test-project-selection_3.png "Select Unit Test Project")

In a default installation, only the Visual Studio Unit Test project
option is available. But MbUnit, xUnit.NET and others have installers
available to get their test frameworks in this dialog.

As you might guess, I’ll start off building the canonical blog demo. I
am going to start without using a database. We can always add that in
later.

The first thing I want to do is add a few classes to the main project. I
won’t add any implementation yet, I just want something to compile
against. I’m going to add the following:

-   **BlogController.cs** to the *Controllers* directory
-   **IPostRepository.cs** to the *Models* directory
-   **Post.cs** to the *Models* directory
-   **BlogControllerTests** to the *MvcApplicationTest* project.

After I’m done, my project tree should look like this.

 ![Project
Tree](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DependencyInjection_F366/TddDI-Project-Tree_3.png)

At this point, I want to implement just enough code so we can write a
test. First, I define my repository interface.

```csharp
using System;
using System.Collections.Generic;

namespace MvcApplication.Models
{
  public interface IPostRepository
  {
    void Create(Post post);

    IList<Post> ListRecentPosts(int retrievalCount);
  }
}
```

Not much of a blog post repository, but it’ll do for this demo. When
you’re ready to write the next great blog engine, you can revisit this
and add more methods.

Also, I’m going to leave the Post class empty for the time being. We can
implement that later. Let’s implement the blog controller next.

```csharp
using System;
using System.Web.Mvc;

namespace MvcApplication.Controllers 
{
  public class BlogController : Controller 
  {
    public ActionResult Recent() 
    {
      throw new NotImplementedException("Wait! Gotta write a test first!");
    }
  }
}
```

Ok, we better stop here. We’ve gone far enough without writing a unit
test. After all, I’m supposed to be demonstrating TDD. Let’s write a
test.

## Let’s Get Test Started, In Here. *with apologies to the Black Eyed Peas*

Starting with the simplest test possible, I’ll make sure that the
`Recent` action does not specify a view name because I want the default
behavior to apply (this snippet assumes you’ve imported all the
necessary namespaces).

```csharp
[TestClass]
public class BlogControllerTests 
{
  [TestMethod]
  public void RecentActionUsesConventionToChooseView() 
    {
    //Arrange
    var controller = new BlogController();

    //Act
    var result = controller.Recent() as ViewResult;

    //Assert
    Assert.IsTrue(String.IsNullOrEmpty(result.ViewName));
  }
}
```

When I run this test, the test fails.

![failed-test](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TDDandDependencyInjectionwithASP.NETMVC_EFA7/failed-test_3.png "failed-test")

This is what we expect, after all, we haven’t yet implemented the
`Recent `method. This is the RED part of the RED, GREEN, REFACTOR rhythm
of TDD.

Let’s go and implement that method.

```csharp
public ActionResult Recent() 
{
  //Note we haven’t yet created a view
  return View();
}
```

Notice that at this point, we’re focusing on the behavior of our app
first rather than focusing on the UI first. This is a stylistic
difference between ASP.NET MVC and ASP.NET WebForms. Neither one is
necessarily better than the other. Just a difference in approach and
style.

Now when I run the unit test, it passes.

![passing-test](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TDDandDependencyInjectionwithASP.NETMVC_EFA7/passing-test_3.png "passing-test") 

Ok, so that’s the GREEN part of the TDD lifecycle and a very very simple
demo of TDD. Let’s move to the REFACTOR stage and start applying
Dependency Injection.

## It’s Refactor Time! *with apologies to the reader for stretching this theme too far*

In order to obtain the recent blog posts, I want to provide my blog
controller with a “service” instance it can request those posts from. 

At this point, I’m not sure how I’m going to store my blog posts. Will I
use SQL? XML? Stone Tablet?

Dunno. Don’t care... yet.

We can delay that decision till the [last responsible
moment](http://www.codinghorror.com/blog/archives/000705.html "The Last Responsible Moment").
For now, I’ll create a repository abstraction to represent how I will
store and retrieve blog posts in the form of an `IPostRepository`
interface. We’ll update the blog controller to accept an instance of
this interface in its constructor.

This is the *dependency* part of Dependency Injection. My controller now
has a dependency on `IPostRepository`. The *injection* part refers to
the mechanism you use to pass that dependency to the dependent class as
opposed to having the class create that instance directly and thus
binding the class to a specific implementation of that interface.

Here’s the change to my `BlogController` class.

```csharp
public class BlogController : Controller 
{
  IPostRepository repository;

  public BlogController(IPostRepository repository) 
  {
    this.repository = repository;
  }

  public ActionResult Recent() 
  {
    //Note we haven’t yet created a view
    return View();
  }
}
```

Great. Notice I haven’t changed `Recent` yet. I need to write another
test first. This will make sure that we pass the proper data to the
view.

***Note:** If you're following along, you’ll notice that the first test
we wrote won't compile. Comment it out for now. We can fix it later.*

I’m going to use a mock framework, so before I write this test, I need
to reference *Moq.dll* in my test project, downloaded from the [MoQ
downloads](http://code.google.com/p/moq/downloads/list "MoQ Downloads Page")
page.

***Note:** I’ve included this assembly in the example project at the end
of this post.*

Here’s the new test.

```csharp
[TestMethod]
public void BlogControllerPassesCorrectViewData() 
{
  //Arrange
  var posts = new List<Post>();
  posts.Add(new Post());
  posts.Add(new Post());

  var repository = new Mock<IPostRepository>();
  repository.Expect(r => r.ListRecentPosts(It.IsAny<int>())).Returns(posts);

  //Act
  BlogController controller = new BlogController(repository.Object);
  var result = controller.Recent() as ViewResult;

  //Assert
  var model = result.ViewData.Model as IList<Post>;
  Assert.AreEqual(2, model.Count);
}
```

What this test is doing is dynamically stubbing out an implementation of
the `IPostRepository` interface. We then tell it that no matter what
argument is passed to `ListRecentPosts`, return two posts. We can then
pass that stub to our controller.

***Note:** We haven’t yet needed to implement this interface. We don’t
need to yet. We’re interested in isolating our test to only test the
logic in the action method, so we fake out the interface for the time
being.*

At this point, the test fails as we expect. We need to refactor `Recent`
to do the right thing now.

```csharp
public ActionResult Recent() 
{
  IList<Post> posts = repository.ListRecentPosts(10); //Doh! Magic Number!
  return View(posts);
}
```

Now when I run my test, it passes!

## Inject That Dependency

But we’re not done yet. When I load up a browser and try to navigate to
this controller action (on my machine,
http://localhost:64701/blog/recent/), I get the following error page.

![No parameterless constructor defined for this object. - Mozilla
Firefox](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DependencyInjection_F366/No%20parameterless%20constructor%20defined%20for%20this%20object.%20-%20Mozilla%20Firefox_3.png)

Well of course it errors out! By default, ASP.NET MVC requires that
controllers have a public parameter-less constructor so that it can
create an instance of the controller. But *our* constructor requires an
instance of `IPostRepository`. We need someone, anyone, to pass such an
instance to our controller.

**StructureMap (or DI framework of your choice) to the rescue!**

***Note:** Make sure
to*[*download*](http://sourceforge.net/project/showfiles.php?group_id=104740&package_id=112685&release_id=498179 "Download StructureMap")*and
reference the StructureMap.dll assembly if you’re following along. I've
included the assembly in the source code at the end of this post.*

The first step I’m going to do is create a *StructureMap.config* file
and add it to the root of my application. Here are the contents of the
file.

```csharp
<?xml version="1.0" encoding="utf-8" ?>
<StructureMap>
  <Assembly Name="MvcApplication" />
  <Assembly Name="System.Web.Mvc
              , Version=1.0.0.0
              , Culture=neutral
              , PublicKeyToken=31bf3856ad364e35" />

  <PluginFamily Type="System.Web.Mvc.IController" 
                Assembly="System.Web.Mvc
                  , Version=1.0.0.0
                  , Culture=neutral
                  , PublicKeyToken=31bf3856ad364e35">
    <Plugin Type="MvcApplication.Controllers.BlogController" 
            ConcreteKey="blog" 
            Assembly="MvcApplication" />
  </PluginFamily>

  <PluginFamily Type="MvcApplication.Models.IPostRepository" 
                Assembly="MvcApplication" 
                DefaultKey="InMemory">
    <Plugin Assembly="MvcApplication" 
            Type="MvcApplication.Models.InMemoryPostRepository" 
            ConcreteKey="InMemory" />
  </PluginFamily>

</StructureMap>
```

I don’t want to get bogged down in describing this file in too much
detail. If you want a deeper understanding, check out the [StructureMap
documentation](http://structuremap.sourceforge.net/Default.htm "StructureMap site").

The bare minimum you need to know is that each `PluginFamily` node
describes an interface *type* and a *key* for that type. A `Plugin` node
describes a concrete type that will be used when an instance of the
family type needs to be created by the framework.

For example, in the second `PluginFamily` node, the interface type is 
`IPostRepository` which we defined. The concrete type is
`InMemoryPostRepository`. So anytime we use StructureMap to construct an
instance of a type that has a dependency on `IPostRepository`,
StructureMap will pass in an instance of `InMemoryPostRepository`.

Well if that’s true, we better then create that class. Normally, I would
use a `SqlPostRepository`. But for purposes of this demo, we’ll store
blog posts in memory using a static collection. We can always implement
the SQL version later.

*Note: This is where I would normally write tests for
`InMemoryPostRepository` but this post is already long enough, right?
Don’t worry, I included unit tests in the downloadable code sample.*

```csharp
public class InMemoryPostRepository : IPostRepository
{
  //simulates database storage
  private static IList<Post> posts = new List<Post>();

  public void Create(Post post)
  {
    posts.Add(post);
  }

  public System.Collections.Generic.IList<Post> 
    ListRecentPosts(int retrievalCount)
  {
    if (retrievalCount < 0)
      throw new ArgumentOutOfRangeException("retrievalCount"
          , "Let’s be positive, ok?");

    IList<Post> recent = new List<Post>();
    int recentIndex = posts.Count - 1;
    for (int i = 0; i < retrievalCount; i++)
    {
      if (recentIndex < 0)
        break;
      recent.Add(posts[recentIndex--]);
    }
    return recent;
  }

  public static void Clear()
  {
    posts.Clear();
  }
}
```

## Quick, We Need A Factory {.clear}

We’re almost done. We now need to hook up StructureMap to ASP.NET MVC by
writing implementing `IControllerFactory`. The controller factory is
responsible for creating controller instances. We can replace the built
in logic with our own factory.

```csharp
public class StructureMapControllerFactory : DefaultControllerFactory
{
  protected override 
    IController CreateController(RequestContext requestContext, string controllerName) 
  {
    try
    {
      string key = controllerName.ToLowerInvariant();
      return ObjectFactory.GetNamedInstance<IController>(key);
    }
    catch (StructureMapException)
    {
      //Use the default logic.
      return base.CreateController(requestContext, controllerName);
    }
  }
}
```

Finally, we wire it all up together by adding the following method call
within the `Application_Start` method in *Global.asax.cs*.

```csharp
protected void Application_Start() {
  ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

  RegisterRoutes(RouteTable.Routes);
}
```

And we’re done! Now that we have hooked up the dependency injection
framework into our application, we can revisit our site in the browser
(after compiling) and we get...

![View Not Found
Message](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TDDandDependencyInjectionwithASP.NETMVC_EFA7/view-not-found_8.png "View Not Found Message")

**Excellent!** Despite the Yellow Screen of Death here, this is a good
sign. We know our dependency is getting injected because this is a
different error message than we were getting before. This one in
particular is informing us that we haven’t created a view for this
action. So we need to create a view.

*Sorry! Out of scope. Not in the spec.*

I leave it as an exercise for the reader to create a view for the page,
or you can look at the silly simple one included in the source download.

Although this example was a ridiculously simple application, the
principle applies in building a larger app. Just take the techniques
here and rinse, recycle, repeat your way to TDD nirvana.
