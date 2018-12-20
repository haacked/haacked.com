---
title: Redirect Routes and other Fun With Routing And Lambdas
date: 2008-12-15 -0800
tags:
- aspnetmvc
- aspnet
redirect_from: "/archive/2008/12/14/redirect-routes-and-other-fun-with-routing-and-lambdas.aspx/"
---

ASP.NET Routing is useful in many situations beyond [ASP.NET
MVC](http://asp.net/mvc "ASP.NET MVC Website"). For example, I often
need to run a tiny bit of custom code in response to a specific request
URL. Let’s look at how routing can help.

First, let’s do a quick review. When you define a route, you specify an
instance of `IRouteHandler` associated with the route which is given a
chance to figure out what to do when that route matches the request.
This step is typically hidden from most developers who use the various
extension methods such as `MapRoute`.

Once the route handler makes that decision, it returns an instance of
`IHttpHandler` which ultimately processes the request.

There are many situations in which I just have a tiny bit of code I want
to run and I’d rather not have to create both a custom route handler and
http handler class.

So I started experimenting with some base handlers that would allow me
to pass around delegates.

> **Warning:** the code you are about to see may cause permanent
> blindness. This was done for fun and I’m not advocating you use the
> code as-is.
>
> It works, but the readability of all these lambda expressions may
> cause confusion and insanity.

One common case I need to handle is permanently redirecting a set of
URLs to another set of URLs. I wrote a simple extension method of
`RouteCollection` to do just that. In my `Global.asax.cs` file I can
write this:

```csharp
routes.RedirectPermanently("home/{foo}.aspx", "~/home/{foo}");
```

This will permanently redirect all requests within the *home* directory
containing the .aspx extension to the same URL without the extension.
That’ll come in pretty handy for my blog in the future.

When you define a route, you have to specify an instance of
`IRouteHandler` which handles matching requests. That instance in turn
returns an `IHttpHandler` which ultimately handles the request. Let’s
look at the simple implementation of this `RedirectPermanently` method.

```csharp
public static Route RedirectPermanently(this RouteCollection routes, 
    string url, string targetUrl) {
  Route route = new Route(url, new RedirectRouteHandler(targetUrl, true));
  routes.Add(route);
  return route;
}
```

Notice that we simply create a route using the `RedirectRouteHandler`.
As we’ll see, I didn’t even need to do this, but it helped make the code
a bit more readable. To make heads or tails of this, we need to look at
that handler. **Warning: Some funky looking code ahead!**

```csharp
public class RedirectRouteHandler : DelegateRouteHandler {
  public RedirectRouteHandler(string targetUrl) : this(targetUrl, false) { 
  }

  public RedirectRouteHandler(string targetUrl, bool permanently)
    : base(requestContext => {
      if (targetUrl.StartsWith("~/")) {
        string virtualPath = targetUrl.Substring(2);
        Route route = new Route(virtualPath, null);
        var vpd = route.GetVirtualPath(requestContext, 
          requestContext.RouteData.Values);
        if (vpd != null) {
          targetUrl = "~/" + vpd.VirtualPath;
        }
      }
      return new DelegateHttpHandler(httpContext => 
        Redirect(httpContext, targetUrl, permanently), false);
    }) {
    TargetUrl = targetUrl;
  }

  private void Redirect(HttpContext context, string url, bool permanent) {
    if (permanent) {
      context.Response.Status = "301 Moved Permanently";
      context.Response.StatusCode = 301;
      context.Response.AddHeader("Location", url);
    }
    else {
      context.Response.Redirect(url, false);
    }
  }

  public string TargetUrl { get; private set; }
}
```

I bolded a portion of the code within the constructor. The bolded
portion is a delegate (via a lambda expression) being passed to the base
constructor. Rather than creating a custom `IHttpHandler` class, I am
telling the base class what code I would want that http handler (if I
had written one) to execute in order to process the request. I pass that
code as a lambda expression to the base class.

I think in a production system, I’d probably manually implement
`RedirectRouteHandler`. But while I’m having fun, let’s take it further.

Let’s look at the `DelegateRouteHandler` to understand this better.

```csharp
public class DelegateRouteHandler : IRouteHandler {
  public DelegateRouteHandler(Func<RequestContext, IHttpHandler> action) {
    HttpHandlerAction = action;
  }

  public Func<RequestContext, IHttpHandler> HttpHandlerAction {
    get;
    private set;
  }

  public IHttpHandler GetHttpHandler(RequestContext requestContext) {
    var action = HttpHandlerAction;
    if (action == null) {
      throw new InvalidOperationException("No action specified");
    }
    
    return action(requestContext);
  }
}
```

Notice that the first constructor argument is a
`Func<RequestContext, IHttpHandler>`. This is effectively the contract
for an `IRouteHandler`. This alleviates the need to write new
`IRouteHandler` classes, because we can instead just create an instance
of `DelegateRouteHandler` directly passing in the code we would have put
in the custom `IRouteHandler` implementation. Again, in this
implementation, to make things clearer, I inherited from
`DelegateRouteHandler` rather than instantiating it directly. To do that
would have sent me to the 7^th^ level of Hades immediately. As it
stands, I’m only on the 4^th^ level for what I’ve shown here.

The last thing we need to look at is the `DelegateHttpHandler`.

```csharp
public class DelegateHttpHandler : IHttpHandler {

  public DelegateHttpHandler(Action<HttpContext> action, bool isReusable) {
    IsReusable = isReusable;
    HttpHandlerAction = action;
  }

  public bool IsReusable {
    get;
    private set;
  }

  public Action<HttpContext> HttpHandlerAction {
    get;
    private set;
  }

  public void ProcessRequest(HttpContext context) {
    var action = HttpHandlerAction;
    if (action != null) {
    action(context);
    }
  }
}
```

Again, you simply pass this class an `Action<HttpContext>` within the
constructor which fulfills the contract for `IHttpHandler`.

The point of all this is to show that when you have interfaces with a
single methods, they can often be transformed into a delegate by writing
a helper implementation of that interface which accepts delegates that
fulfill the same contract, or at least are close enough.

This would allow me to quickly hack up a new route handler without
adding two new classes to my project. Of course, the syntax to do so is
prohibitively dense that for the sake of those who have to read the
code, I’d probably recommend just writing the new classes.

In any case, however you implement it, the `Redirect` and
`RedirectPermanent` extension methods for handling routes is still quite
useful.

