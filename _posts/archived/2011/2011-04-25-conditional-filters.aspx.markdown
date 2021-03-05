---
title: Conditional Filters in ASP.NET MVC 3
tags: [aspnet,aspnetmvc,code]
redirect_from: "/archive/2011/04/24/conditional-filters.aspx/"
---

Say you want to apply an action filter to every action except one. How
would you go about it? For example, suppose you want to apply an
authorization filter to every action except the action that lets the
user login. Seems like a pretty good idea, right?

Currently, it takes a bit of work to do this. If you add a filter to the
`GlobalFilters.Filters` collection, it applies to every action, which in
the previous scenario would mean you already need to be authorized to
login. Now *that* is security you can trust!

[![security](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Conditional-Filters-in-ASP.NET-MVC-3_BBA7/security_3.jpg "security")](http://www.sxc.hu/photo/1339522h "Chained door by linder6850 from sxc.hu.")

You can also manually add the filter attribute to every controller
and/or action method except one. This solution is a potential bug magnet
since you would you need to remember to apply this attribute every time
you add a new controller. **Update:** There’s yet another approach you
can try which is to write a custom authorize attribute as described in
this blog post on [Securng your ASP.NET MVC 3
Application](http://blogs.msdn.com/b/rickandy/archive/2011/05/02/securing-your-asp-net-mvc-3-application.aspx "Securing your ASP.NET MVC 3 Application").

Fortunately, ASP.NET MVC 3 introduced a new feature called filter
providers which allow you to write a class that will be used as a source
of action filters. For more details about what filter providers are, I
highly recommend reading [Brad Wilson’s blog post on
filters](http://bradwilson.typepad.com/blog/2010/07/service-location-pt4-filters.html "Filters").

In this case, what I need to write is a conditional action filter. I
actually started writing one during my [ASP.NET MVC 3 presentation at
this past Mix
11](https://haacked.com/archive/2011/04/16/a-look-back-at-mix-11.aspx "ASP.NET MVC 3 Presentation")
but never actually finished the demo. One of the many mistakes that
inspired my [blog post on presentation
tips](https://haacked.com/archive/2011/04/18/presentation-tips.aspx "Presentation Tips").

In this blog post, I’ll finish what I started and walk through an
implementation of a conditional filter provider which will let us
accomplish applying filters to action methods based on any criteria we
can think of.

Here’s the approach I took. First, I wrote a custom filter provider.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

public class ConditionalFilterProvider : IFilterProvider {
  private readonly 
    IEnumerable<Func<ControllerContext, ActionDescriptor, object>> _conditions;

  public ConditionalFilterProvider(
    IEnumerable<Func<ControllerContext, ActionDescriptor, object>> conditions)
  {
        
      _conditions = conditions;
  }

  public IEnumerable<Filter> GetFilters(
      ControllerContext controllerContext, 
      ActionDescriptor actionDescriptor) {
    return from condition in _conditions
           select condition(controllerContext, actionDescriptor) into filter
           where filter != null
           select new Filter(filter, FilterScope.Global, null);
  }
}
```

The code here is fairly straightforward despite all the angle brackets.
We implement the `IFilterProvider` interface, but only return the
filters given that meet the set of criterias represented as a `Func`.
But each Func gets passed two pieces of information, the current
`ControllerContext` and an `ActionDescriptor`. Through the
`ActionDescriptor`, we can get access to the `ControllerDescriptor`.

The ActionDescriptor and ControllerDescriptor are abstractions of
actions and controllers that don’t assume that the controller is a type
and the action is a method. That’s why they were implemented in that
way.

So now, to use this provider, I simply need to instantiate it and add it
to the global filter provider collection (or register it via my
Dependency Injection container like Brad described in his blog post).

Here’s an example of creating a conditional filter provider with two
conditions. The first adds an instance of `MyFilter` to every controller
except `HomeController`. The second adds `SomeFilter` to any action that
starts with “About”. These scenarios are a bit contrived, but I bet you
can think of a lot more interesting and powerful uses for this.

```csharp
IEnumerable<Func<ControllerContext, ActionDescriptor, object>> conditions = 
    new Func<ControllerContext, ActionDescriptor, object>[] { 
    
    (c, a) => c.Controller.GetType() != typeof(HomeController) ? 
      new MyFilter() : null,
    (c, a) => a.ActionName.StartsWith("About") ? new SomeFilter() : null
};

var provider = new ConditionalFilterProvider(conditions);
FilterProviders.Providers.Add(provider);
```

Once we create the filter provider, we add it to the filter provider
collection. Again, you can also do this via dependency injection instead
of adding it to this static collection.

I’ve posted the conditional filter provider as a package in my [personal
NuGet
repository](https://haacked.com/archive/2011/03/31/hosting-simple-nuget-package-feed.aspx "Hosting a simple read-only NuGet feed")
I use for my own little samples located at
[http://nuget.haacked.com/nuget/](http://nuget.haacked.com/nuget/). Feel
free to add that URL as a package source and
`Install-Package ConditionalFilterProvider` in order to get the source.

