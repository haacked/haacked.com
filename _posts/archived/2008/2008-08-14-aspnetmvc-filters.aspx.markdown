---
layout: post
title: Filters in ASP.NET MVC CodePlex Preview 4
date: 2008-08-14 -0800
comments: true
disqus_identifier: 18522
categories: [aspnetmvc]
redirect_from: "/archive/2008/08/13/aspnetmvc-filters.aspx/"
---

In Preview 2 or Preview 3 of ASP.NET (I forget which), we introduced the
concept of *Action Filters*. Sounds much more exciting than your
run-of-the-mill `LayOnTheCouchMunchingChipsWatchingInfomercialsFilter`,
that I originally proposed to the team. Thankfully, that was rejected.

An action filter is an attribute you can slap on an action method in
order to run some code before and after the action method executes.
Typically, an action filter represents a [cross-cutting
concern](http://en.wikipedia.org/wiki/Cross-cutting_concern "Cross-Cutting Concern on Wikipedia")
to your action method. Output caching is a good example of a
cross-cutting concern.

In [CodePlex Preview 4 of ASP.NET
MVC](In%20CodePlex%20Preview%204%20of%20ASP.NET%20MVC "ASP.NET MVC on CodePlex"),
we split out our action filters into four types of filters, each of
which is an interface.

-   `IAuthorizationFilter`
-   `IActionFilter`
-   `IResultFilter`
-   `IExceptionFilter`

### IAuthorizationFilter

Authorization filters run before any of the action filters and allow you
to cancel the action. If you cancel the action, you can set the
`ActionResult` instance you want rendered in response to the current
request.

There should be very few cases (hopefully) that you need to write such a
filter of your own. In those rare cases when you do, you’ll be glad to
have this interface around.

### IActionFilter

Action filters allow you to run code before and after an action method
is called, but *before* the result of the action method is executed.
This effectively allows you to hook into the rendering of the view, for
example.

In the “before” method (`OnActionExecuting`), you can cancel the action
and even supply an action result of your own instead. If you cancel the
action, no other filters higher up the stack will be executed and the
invoker starts executing the “after” method for any action filter that
had its “before” method called (except for the filter that canceled the
action).

In the after method (`OnActionExecuted`) you can’t cancel the action (it
already ran and we don’t have a `ITimeMachineFilter` implemented yet),
but you *can* replace or modify the action result before it gets called.

If an exception was thrown by another action filter or by the action
method itself, you can examine the exception thrown from your filter.
Your filter can specify that it can handle the exception (*seriously,
only do this if your filter really can do this as it’s generally a bad
thing to handle an exception you shouldn’t be handling*), in which case
the action result will still get executed. If the exception propagates
up, the result will not get executed.

### IResultFilter

Result filters are pretty much similar to action filters, except they
run after the action method has executed, but before the result returned
from the action method has been executed. The “before” method is called
`OnResultExecuting` and the “after” method is called `OnResultExecuted`.

### IExceptionFilter

The exception filters are all guaranteed to run after all of the action
filters and result filters have run. Even if an exception filter
indicates that it can handle the exception, it will still run. This is
useful for logging scenarios in cases where you want a filter to always
run no matter what happens so it can log exceptions etc…

One interesting thing to note is that exception filters run after result
filters. So what can you do from an exception filter? Well we give you
one last ditch chance to render something to the user by allowing you to
set the action result in the exception filter. If *that* action result
throws an exception, you’re
[SOL](http://www.urbandictionary.com/define.php?term=S.O.L. "SOL from Urban Dictionary")
and the exception filter does not handle that exception. Well, you’re
not totally SOL. The normal ASP.NET [web.config settings for custom
errors](http://msdn.microsoft.com/en-us/library/h0hfz6fc.aspx "web.config customErrors")
will kick in if you set them.

### Writing Custom Filters

To write a custom filter, you simply need to create an attribute (aka a
class that inherits from `FilterAttribute`) that also implements one of
the four interfaces I mentioned.

It turns out that we think the most common case for custom filters will
be those that implement `IActionFilter` and/or `IResultFilter. `To
support the common case, we included a base attribute
`ActionFilterAttribute`, which inherits **both** of these interfaces.
Yeah, the name isn’t *exactly* accurate, but we tend to think of action
filters as really action and action result filters.

For the other two filter types, we did not include a base attribute type
for these. To write your own authorization filter, you simply implement
`IAuthorizationFilter`. For example, here’s a filter I wrote the other
day which we will probably include in the `MvcFutures` assembly. Apply
this filter to an action and it will perform request validation of
potentially insecure input. (*Side Note: This validation is on by
default in ASP.NET WebForm applications, but not in ASP.NET MVC
applications because it’s implemented by the Page class, which runs too
late.*)

```csharp
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, 
  Inherited = true, AllowMultiple = false)]
public sealed class ValidateInputAttribute : FilterAttribute
    , IAuthorizationFilter {
  public void OnAuthorization(AuthorizationContext filterContext) {
    filterContext.HttpContext.Request.ValidateInput();
  }
}
```

While we did not include a base attribute for these filters, we did
include concrete implementations of these interfaces. For example, the
`AuthorizeAttribute` is a concrete implementation of an authorization
filter. You can (*er…will be able to*) inherit from this attribute if
you want, but you can also simply implement `IAuthorizationFilter`
yourself.

### Why Four Filter Types?

We debated this a long time. We could have stuck with just the two
interfaces: `IActionFilter` and `IResultFilter` and handled all cases.

The problem we ran into is that for attributes that perform some sort of
authentication check, you want to be absolutely sure it runs before any
of the action filters. And it’s very easy to get this wrong by accident
even if you know what you are doing.

The type of thing we wanted to avoid was accidentally running the output
cache filter before the the authorization filter. That’s a recipe for an
information disclosure bug, potentially displaying information to
someone who shouldn’t have access to see it such as photos of your hair
piece collection (*Why do you have so man?*). So we decided that there
ought to be four distinct filter phases in the life of a controller
action: Authorization, Action Execution, Result Execution, Exception
Handling.

If you write an authorization filter, it is guaranteed to run before any
other action filters.

Keep in mind though, that these phases merely help guide filter writers
into doing the right thing. Because the MVC framework is all about
leaving you in control, it is still possible to get it all wrong. For
example, I could write a custom output caching filter that implements
`IAuthorizationFilter` and thus runs at the wrong time. **Please don’t
do this.**Code responsibly.
