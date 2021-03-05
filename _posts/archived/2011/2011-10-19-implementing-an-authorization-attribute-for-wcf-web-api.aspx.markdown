---
title: Implementing an Authorization Attribute for WCF Web API
tags: [code,aspnet]
redirect_from: "/archive/2011/10/18/implementing-an-authorization-attribute-for-wcf-web-api.aspx/"
---

If you’re not familiar with [WCF Web
API](http://wcf.codeplex.com/wikipage?title=WCF%20HTTP "WCF Web API"),
it’s a framework with nice HTTP abstractions used to expose simple HTTP
services over the web. Its focus is targeted at applications that
provide HTTP services for various clients such as mobile devices,
browsers, desktop applications.

In some ways, it’s similar to ASP.NET MVC as it was developed with
testability and extensibility in mind. There are some concepts that are
similar to ASP.NET MVC, but with a twist. For example, where ASP.NET MVC
has filters, WCF has operation handlers.

[![security](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Conditional-Filters-in-ASP.NET-MVC-3_BBA7/security_3.jpg "security")](http://www.sxc.hu/photo/1339522h "Chained door by linder6850 from sxc.hu.")

One question that comes up often with Web API is how do you authenticate
requests? Well, you run Web API on ASP.NET (Web API also supports a
self-host model), one approach you could take is to write an operation
handler and attach it to a set of operations (an operation is analogous
to an ASP.NET MVC action).

However, some folks like the ASP.NET MVC approach of slapping on an
`AuthorizeAttribute`. In this blog post, I’ll show you how to write an
attribute, `RequireAuthorizationAttribute`, for WCF Web API that does
something similar.

One difference is that in the WCF Web API case, the attribute simply
provides metadata, but not the the behavior, for authorization. If you
wanted to use the existing ASP.NET MVC `AuthorizeAttribute` in the same
way, you could do that as well, but I leave that as an exercise for the
reader.

I’ll start with the easiest part, the attribute.

```csharp
[AttributeUsage(AttributeTargets.Method)]
public class RequireAuthorizationAttribute : Attribute
{
    public string Roles { get; set; }
}
```

For now, it only applies to methods (operations). Later, we can update
it to apply to classes as well if we so choose. I’m still learning the
framework so I didn’t want to go bite off too much all at once.

The next step is to write an operation handler. When properly
configured, the operation handler runs on every request for the
operation that it applies to.

```csharp
public class AuthOperationHandler 
      : HttpOperationHandler<HttpRequestMessage, HttpRequestMessage>
{
  RequireAuthorizationAttribute _authorizeAttribute;

  public AuthOperationHandler(RequireAuthorizationAttribute authorizeAttribute)
    : base("response")
  {
    _authorizeAttribute = authorizeAttribute;
  }

  protected override HttpRequestMessage OnHandle(HttpRequestMessage input)
  {
    IPrincipal user = Thread.CurrentPrincipal;
    if (!user.Identity.IsAuthenticated)
    {
      throw new HttpResponseException(HttpStatusCode.Unauthorized);
    }

    if (_authorizeAttribute.Roles == null)
    {
      return input;
    }

    var roles = _authorizeAttribute.Roles.Split(new[] { " " }, 
      StringSplitOptions.RemoveEmptyEntries);
    if (roles.Any(role => user.IsInRole(role)))
    {
      return input;
    }

    throw new HttpResponseException(HttpStatusCode.Unauthorized);
  }
}
```

~~Notice that the code accesses `HttpContext.Current`. This restricts
this operation handler to only work within ASP.NET applications. Hey, I
write what I know!~~ Many folks replied to me that I should use
`Thread.CurrentPrincipal`. My brain must have been off when I wrote this
to not think of it. :)

Then all we do is ensure that the user is authenticated and in one of
the specified roles if any role is specified. Very simple
straightforward code at this point.

The final step is to associate this operation handler with some
operations. In general, when you build a Web API application, the
application author writes a configuration class that derives from
`WebApiConfiguration` and either sets it as the default configuration,
or passes it to a service route.

Within that configuration class, the author can specify an action that
gets called on every request and gives the configuration class a chance
to map a set of operation handlers to an operation.

For example, in a sample Web API app, I added the following
configuration class.

```csharp
public class CommentsConfiguration : WebApiConfiguration
{
    public CommentsConfiguration()
    {
        EnableTestClient = true;

        RequestHandlers = (c, e, od) =>
        {
            // TODO: Configure request operation handlers
        };

        this.AppendAuthorizationRequestHandlers();
    }
}
```

The `RequestHandlers` is a property of type
`Action<Collection<HttpOperationHandler>, ServiceEndpoint, HttpOperationDescription>`

In general, it would be up to the application author to wire up the
authentication operation handler I wrote to the appropriate actions. But
I wanted to provide a method that helps with that. That’s the
`AppendAuthorizationRequestHandlers` method in there, which is an
extension method I wrote.

```csharp
public static void AppendAuthorizationRequestHandlers(
  this WebApiConfiguration config)
{
  var requestHandlers = config.RequestHandlers;
  config.RequestHandlers = (c, e, od) =>
  {
    if (requestHandlers != null)
    {
      requestHandlers(c, e, od); // Original request handler
    }
    var authorizeAttribute = od.Attributes.OfType<RequireAuthorizationAttribute>()
      .FirstOrDefault();
    if (authorizeAttribute != null)
    {
      c.Add(new AuthOperationHandler(authorizeAttribute));
    }
  };
}
```

Since I didn’t want to stomp on the existing request handlers, I set the
`RequestHandlers` property to a new action that calls the existing
action (if any) and then does my custom registration logic.

I’ll admit, I couldn’t help thinking that if `RequestHandlers` was an
event, rather than an action, that sort of logic could be handled for
me. ![Winking
smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Implementing-an-Authorization-Attribute-_12EA4/wlEmoticon-winkingsmile_2.png)
Have events fallen out of favor? They do work well to decouple code in
this sort of scenario, but I digress.

The interesting part here is that the action’s third parameter, `od`, is
an `HttpOperationDescription`. This is a description of the operation
that includes access to such things as the attributes applied to the
method! I simply look to see if the operation has the
`RequireAuthorizationAttribute` applied and if so, I add the
`AuthOperationHandler` I wrote earlier to the operation’s collection of
operation handlers.

With this in place, I can now write a service that looks like this:

```csharp
[ServiceContract]
public class CommentsApi
{
    [WebGet]
    public IQueryable<Comment> Get()
    {
        return new[] { new Comment 
        { 
            Title = "This is neato", 
            Body = "Ok, not as neat as I originally thought." } 
        }.AsQueryable();
    }

    [WebGet(UriTemplate = "auth"), RequireAuthorization]
    public IQueryable<Comment> GetAuth()
    {
        return new[] { new Comment 
        { 
            Title = "This is secured neato", 
            Body = "Ok, a bit neater than I originally thought." } 
        }.AsQueryable();
    }
}
```

And route to the Web API service like so:

```csharp
public class Global : HttpApplication
{
  protected void Application_Start(object sender, EventArgs e)
  {
    RouteTable.Routes.MapServiceRoute<CommentsApi>("comments",
      new CommentsConfiguration());
  }
}
```

With this in place, a request for */comments* allows anonymous, but a
request for */comments/auth* requires authentication.

If you’re interested in checking this code out, I pushed it to my
[CodeHaacks Github
repository](https://github.com/Haacked/CodeHaacks "CodeHaacks") as a
sample. I won’t make this into a NuGet package until it’s been
thoroughly vetted by the WCF Web API team because it’s very likely I
have no idea what I’m doing. I’d rather one of those folks make a NuGet
package for this.
![Smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Implementing-an-Authorization-Attribute-_12EA4/wlEmoticon-smile_2.png)

And if you’re wondering why I’m writing about Web API, we’re all part of
the same larger team now, so I figured it’s good to take a peek at what
my friends are up to.

