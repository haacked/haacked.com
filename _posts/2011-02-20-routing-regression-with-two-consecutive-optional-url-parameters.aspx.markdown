---
layout: post
title: "Routing Regression With Two Consecutive Optional Url Parameters"
date: 2011-02-19 -0800
comments: true
disqus_identifier: 18763
categories: []
---
It pains me to say it, but ASP.NET MVC 3 introduces a minor regression
in routing from ASP.NET MVC 2. The good news is that there’s an easy
workaround.

The bug manifests when you have a route with two consecutive *optional*
URL parameters and you attempt to use the route to generate an URL. The
incoming request matching behavior is unchanged and continues to work
fine.

For example, suppose you have the following route defined:

```csharp
routes.MapRoute("by-day", 
        "archive/{month}/{day}",
        new { controller = "Home", action = "Index", 
            month = UrlParameter.Optional, day = UrlParameter.Optional }
);
```

Notice that the `month` and `day` parameters are both optional.

```csharp
routes.MapRoute("by-day", 
        "archive/{month}/{day}",
        new { controller = "Home", action = "Index", 
            month = UrlParameter.Optional, day = UrlParameter.Optional }
);
```

Now suppose you have the following view code to generate URLs using this
route.

```csharp
@Url.RouteUrl("by-day", new { month = 1, day = 23 })
@Url.RouteUrl("by-day", new { month = 1 })
@Url.RouteUrl("by-day", null)
```

In ASP.NET MVC 2 the above code (well actually, the equivalent to the
above code since Razor didn’t exist in ASP.NET MVC 2) would result in
the following URLs as you would expect:

-   /archive/1/23
-   /archive/1
-   /archive

But in ASP.NET MVC 3, you get:

-   /archive/1/23
-   /archive/1
-   

In the last case, the value returned is *null*because of this bug. The
bug occurs when two or more consecutive optional URL parameters don’t
have values specified for URL generation.

Let’s look at the workaround first, then we’ll dive deeper into why this
bug occurs.

The Workaround
--------------

The workaround is simple. To fix this issue, change the existing route
to not have any optional parameters by removing the default values for
`month` and `day`. This route now handles the first URL where `month`
and `day` was specified.

We then add a new route for the other two cases, but this route only has
one optional `month` parameter.

Here are the two routes after we’re done with these changes.

```csharp
routes.MapRoute("by-day", 
        "archive/{month}/{day}",
        new { controller = "Home", action = "Index"}
);

routes.MapRoute("by-month", 
        "archive/{month}",
        new { controller = "Home", action = "Index", 
            month = UrlParameter.Optional}
);
```

And now, we need to change the last two calls to generate URLs to use
the *by-month* route.

```csharp
@Url.RouteUrl("by-day", new { month = 1, day = 23 })
@Url.RouteUrl("by-month", new { month = 1 })
@Url.RouteUrl("by-month", null)
```

Just to be clear, this bug affects all the URL generation methods in
ASP.NET MVC. So if you were generating action links like so:

```csharp
@Html.ActionLink("sample", "Index", "Home", new { month = 1, day = 23 }, null)
@Html.ActionLink("sample", "Index", "Home", new { month = 1}, null)
@Html.ActionLink("sample", "Index", "Home")
```

The last one would be broken without the workaround just provided.

The workaround is not too bad if you happen to follow the practice of
centralizing your URL generation. For example, the developers building
[http://forums.asp.net/](http://forums.asp.net/) ran into this problem
as well during the upgrade to ASP.NET MVC 3. But rather than having
calls to `ActionLink` all over their views, they have calls to methods
that are specific to their application domain such as `ForumDetailUrl`.
This allowed them to workaround this issue by updating a single method.

The Root Cause
--------------

For the insanely curious, let’s look at the root cause of this bug.
Going back to the original route defined at the top of this post, we
never tried generating an URL where only the *second* optional parameter
was specified.

```csharp
@Url.RouteUrl("by-day", new { day = 23 })
```

This call really should fail because we didn’t specify a value for the
first optional parameter, month. If it’s not clear why it should fail,
suppose we allowed this to succeed, what URL would it generate?
*`/archive/23`*?  Well that’s obviously not correct because when a
request is made for that URL, 23 will be interpreted to be the month,
not the date.

In ASP.NET MVC 2, if you made this call, you ended up with
`/archive/System.Web.Mvc.UrlParameter/23`. `UrlParameter.Optional` is a
class introduced by ASP.NET MVC 2 which ships on its own schedule
outside of the core ASP.NET Framework. What that means is we added this
new class which provided this new behavior in ASP.NET MVC, but core
routing didn’t know about it.

The way we fixed this in ASP.NET MVC 3 was to make the `ToString` method
of `UrlParameter.Optional` return an empty string. That solved *this*
bug, but uncovered a bug in *core routing* where a route with optional
parameters having default values behaves incorrectly when two of them
don’t have values specified during URL generation. Sound familiar?

In hindsight, I think it was a mistake to take this fix because it
caused a regression for many applications that had worked around the
bug. The bug was found very late in our ship cycle and this is just one
of the many challenging decisions we make when building software that
sometimes don’t work out the way you hoped or expected. All we can do is
learn from it and let the experience factor into the next time we are
faced with such a dilemma.

The good news is we have bugs logged against this behavior in core
ASP.NET Routing so hopefully this will all get resolved in the next core
.NET framework release.

