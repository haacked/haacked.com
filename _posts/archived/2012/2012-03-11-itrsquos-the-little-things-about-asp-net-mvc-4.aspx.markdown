---
title: It&rsquo;s The Little Things about ASP.NET MVC 4
tags: [aspnetmvc,aspnet,code]
redirect_from: "/archive/2012/03/10/itrsquos-the-little-things-about-asp-net-mvc-4.aspx/"
---

Conway’s Law states,

> ...organizations which design systems ... are constrained to produce
> designs which are copies of the communication structures of these
> organizations.

Up until recently, there was probably no better demonstration of this
law than the fact that Microsoft had two ways of shipping angle brackets
(and curly braces for that matter) over HTTP – ASP.NET MVC and WCF Web
API.

The reorganization of these two teams under [Scott
Guthrie](http://weblogs.asp.net/scottgu "Scott Guthrie's Blog") (aka
“The GU” which I’m contractually bound to tack on) led to an intense
effort to consolidate these technologies in a coherent manner. It’s an
effort that’s lead to what we see in the [recently released ASP.NET MVC
4
Beta](http://weblogs.asp.net/jgalloway/archive/2012/02/16/asp-net-4-beta-released.aspx "ASP.NET MVC 4 Beta Released")
which includes ASP.NET Web API.

For this reason, this is an exciting release of ASP.NET MVC 4 as I can
tell you, it was not a small effort to get these two teams with
different philosophies and ideas to come together and start to share a
single vision. And this vision may take more than one version to realize
fully, but ASP.NET MVC 4 Beta is a great start!

For me personally, this is also exciting as this is the last release I
had any part in and it’s great to see the effort everyone put in come to
light. So many congrats to the team for this release!

Some Small Things
-----------------

[![small-things](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Its-The-Little-Things-about-AS.NET-MVC-4_A996/small-things_thumb.jpg "small-things")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Its-The-Little-Things-about-AS.NET-MVC-4_A996/small-things_2.jpg)

If you take a look at [Jon
Galloway’s](http://weblogs.asp.net/jgalloway/archive/2012/02/16/asp-net-4-beta-released.aspx "Jon Galloway's Blog Post")
on ASP.NET MVC 4, he points to a lot of resources and descriptions of
the BIG features in this release. I highly recommend reading that post.

I wanted to take a different approach and highlight some of the small
touches that might get missed in the glare of the big features.

### Custom IActionInvoker Injection

I’ve written several posts that add interesting cross cutting behavior
when calling actions via the `IActionInvoker` interface.

-   [Handling Formats Based on Url
    Extension](https://haacked.com/archive/2009/01/06/handling-formats-based-on-url-extension.aspx "Handling formats")
-   [Calling ASP.NET MVC Action Methods from
    JavaScript](https://haacked.com/archive/2011/08/18/calling-asp-net-mvc-action-methods-from-javascript.aspx "Calling MVC Actions from JavaScript")
-   [How a method becomes an
    action](https://haacked.com/archive/2008/08/29/how-a-method-becomes-an-action.aspx "How a method becomes an action")

Ironically, the first two posts are made mostly irrelevant now that
ASP.NET MVC 4 includes ASP.NET Web API.

However, the concept is still interesting. Prior to ASP.NET MVC 4, the
only way to switch out the action invoker was to write a custom
controller factory. In ASP.NET MVC 4, you can now simply inject an
`IActionInvoker` using the dependency resolver.

The same thing applies to the `ITempDataProvider` interface. There’s
almost no need to write a custom `IControllerFactory` any longer. It’s a
minor thing, but it was a friction that’s now been buffed out for those
who like to get their hands dirty and extend ASP.NET MVC in deep ways.

Two DependencyResolvers
-----------------------

I’ve been a big fan of using the Ninject.Mvc3 package to inject
dependencies into my ASP.NET MVC controllers.

[![ninject.mvc3](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Its-The-Little-Things-about-AS.NET-MVC-4_A996/ninject.mvc3_thumb.png "ninject.mvc3")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Its-The-Little-Things-about-AS.NET-MVC-4_A996/ninject.mvc3_2.png)

However, your Ninject bindings do not apply to `ApiController`
instances. For example, suppose you have the following binding in the
*NinjectMVC3.cs* file that the Ninject.MVC3 package adds to your
project’s *App\_Start* folder.

```csharp
private static void RegisterServices(IKernel kernel)
{
    kernel.Bind<ISomeService>().To<SomeService>();
}
```

Now create an `ApiController` that accepts an `ISomeService` in its
constructor.

```csharp
public class MyApiController : ApiController
{
  public MyApiController(ISomeService service)
  {
  }
  // Other code...
}
```

That’s not going to work out of the box. You need to configure a
dependency resolver for Web API via a call to
`GlobalConfiguration.Configuration.ServiceResolver.SetResolver.`

However, you can’t pass in the instance of the ASP.NET MVC dependency
resolver, because their interfaces are different types, even though the
methods on the interfaces look exactly the same.

This is why I wrote [a small adapter
class](https://gist.github.com/2017786 "Adapter for Dependency Resolvers")
and convenient extension method. Took me all of five minutes.

In the case of the Ninject.MVC3 package, I added the following line to
the `Start` method.

```csharp
public static void Start()
{
  // ...Pre-existing lines of code...

  GlobalConfiguration.Configuration.ServiceResolver
  .SetResolver(DependencyResolver.Current.ToServiceResolver());
}
```

With that in place, the registrations work for both regular controllers
and API controllers.

I’ve been pretty busy with my new job to dig into ASP.NET MVC 4, but at
some point I plan to spend more time with it. I figure we may eventually
upgrade [NuGet.org](http://nuget.org/ "NuGet Gallery") to run on MVC 4
which will allow me to get my hands really dirty with it.

Have you tried it out yet? What hidden gems have you found?

