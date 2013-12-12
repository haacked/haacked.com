---
layout: post
title: "Grouping Routes Part 2"
date: 2011-01-08 -0800
comments: true
disqus_identifier: 18754
categories: [asp.net mvc,asp.net,code]
---
In [part 1 of this
series](http://haacked.com/archive/2010/12/02/grouping-routes-part-1.aspx "Grouping Routes"),
we looked at the scenario for grouping routes and how we can implement
matching incoming requests with a grouped set of routes.

In this blog post, I’ll flesh out the implementation of URL Generation.

### Url Generation Implementation

URL generation for a group route is tricky, especially when using named
routes because the individual routes that make up the group aren’t in
the main route collection.

As I noted before, the only route that’s actually added to the route
table is the `GroupRoute`. Thus if you supply a route name for one of
the child routes (such as “r1”) during URL generation, you’ll get a null
URL.

Interestingly enough, in this case, if you don’t use named routes when
using URL generation, everything works just fine. However, since I
[heartily recommend using named routes all the
time](http://haacked.com/archive/2010/11/21/named-routes-to-the-rescue.aspx "Named Routes to the rescue"),
I should cover that situation.

So what we need to do here is supply two route names during URL
generation. One for the group route, and one for the child route. How do
we supply the child route name? We’re going to have to supply it in the
route values. Here’s an example of generating an URL in this manner:

```csharp
@Html.RouteLink("Hello World Child", "group", new { __RouteName = "hello-world3" }) 
```

Note that the second parameter, “group”, refers to the route name for
the `GroupRoute` that we registered. The route value *\_\_RouteName* is
passed into the `GroupRoute` so that it can look in its own collection
of routes for the matching child route.

In the following code sample, I’ve highlighted the essential part of the
URL generation logic within the `GroupRoute` class.

```csharp
public override VirtualPathData GetVirtualPath(RequestContext 
    requestContext, RouteValueDictionary values) {
  string routeName = values.GetRouteName();
  var virtualPath = ChildRoutes.GetVirtualPath(requestContext, 
    routeName as string, values.WithoutRouteName());
  if (virtualPath != null) {
    string rewrittenVirtualPath = 
      virtualPath.VirtualPath.WithoutApplicationPath(requestContext);
    string directoryPath = VirtualPath.WithoutTildePrefix(); // remove tilde
    rewrittenVirtualPath = rewrittenVirtualPath.Insert(0, 
    directoryPath.WithoutTrailingSlash());
    virtualPath.VirtualPath = rewrittenVirtualPath.Remove(0, 1);
  }

  return virtualPath;
}
```

The code grabs the route name for the child route from the supplied
route values. Notice that I’m using an extension method I wrote in my
[last blog
post](http://haacked.com/archive/2010/11/28/getting-the-route-name-for-a-route.aspx "Get the route name for a route").

The block of code after the highlighted portion rewrites the virtual
path back to the *full* virtual path for the parent *GroupRoute*. This
ensures that the virtual path that’s eventually returned to the caller
will actually work, since the individual routes within the group don’t
have a clue that they’re within a group.

In a follow-up blog post, I’ll wrap up this series and provide access to
the full source code.

