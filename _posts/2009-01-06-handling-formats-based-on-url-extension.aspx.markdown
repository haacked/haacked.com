---
layout: post
title: "Handling Formats Based On Url Extension"
date: 2009-01-06 -0800
comments: true
disqus_identifier: 18571
categories: [asp.net,asp.net mvc]
---
[Rob](http://blog.wekeroad.com/ "Rob Conery") pinged me today asking
about how to respond to requests using different formats based on the
extension in the URL. More specifically, he’d like to respond with HTML
if there is no file extension, but with JSON if the URL ended with .json
etc...

> /home/index –\> HTML
>
> /home/index.json –\> JSON

The first thing I wanted to tackle was writing a custom action invoker
that would decide based on what’s in the route data, how to format the
response.

This would allow the developer to simply return an object (the model)
from an action method and the invoker would look for the format in the
route data and figure out what format to send.

So I wrote a custom action invoker:

```csharp
public class FormatHandlingActionInvoker : ControllerActionInvoker {
  protected override ActionResult CreateActionResult(
      ControllerContext controllerContext, 
      ActionDescriptor actionDescriptor, 
      object actionReturnValue) {
    if (actionReturnValue == null) {
      return new EmptyResult();
    }

    ActionResult actionResult = actionReturnValue as ActionResult;
    if (actionResult != null) {
      return actionResult;
    }

    string format = (controllerContext.RouteData.Values["format"] 
        ?? "") as string;
      switch (format) {
        case "":
        case "html":
          var result = new ViewResult { 
            ViewData = controllerContext.Controller.ViewData, 
            ViewName = actionDescriptor.ActionName 
          };
          result.ViewData.Model = actionReturnValue;
          return result;
          
        case "rss":
          //TODO: RSS Result
          break;
        case "json":
          return new JsonResult { Data = actionReturnValue };
    }

    return new ContentResult { 
      Content = Convert.ToString(actionReturnValue, 
       CultureInfo.InvariantCulture) 
    };
  }
}
```

The key thing to note is that I overrode the method
`CreateActionResult`. This method is responsible for examining the
result returned from an action method (which can be any type) and
figuring out what to do with it. In this case, if the result is already
an `ActionResult`, we just use it. However, if it’s something else, we
look at the format in the route data to figure out what to return.

For reference, here’s the code for the `HomeController` which simply
returns an object.

```csharp
[HandleError]
public class HomeController : Controller {
  public object Index() {
    return new {Title = "HomePage", Message = "Welcome to ASP.NET MVC" };
  }
}
```

In order to make sure that all my controllers replaced the default
invoker with this invoker, I wrote a controller factory that would set
this invoker. I won’t show the code here, but I will include it in the
download.

So at this point, we have everything in place, except for the fact that
I haven’t dealt with how we get the format in the route data in the
first place. Unfortunately, it ends up that this isn’t quite so
straightforward. Consider the default route:

```csharp
routes.MapRoute(
    "Default",
    "{controller}/{action}/{id}",
    new { controller = "Home", action = "Index", id = "" }
);
```

Since this route allows for default values at each segment, this single
route matches all the following URLs:

-   /home/index/123
-   /home/index
-   /home

So the question becomes, if we want to support optional format
extensions in the URL, would we have to support it for every segment?
Making up a fictional syntax, maybe it would look like this:

```csharp
routes.MapRoute(
    "Default",
    "{controller{.format}}/{action{.format}}/{id{.format}}",
    new { controller = "Home", action = "Index", id = "", format = ""}
);
```

Where the {.format} part would be optional. Of course, we don’t have
such a syntax available, so I needed to put on my dirty ugly hacking hat
and see what I could come up with. I decided to do something we strongly
warn people not to do, inheriting `HttpRequestWrapper` with my own
`HttpRequestBase` implementation and stripping off the extension before
I try and match the routes.

**Warning! Don’t do this at home!** This is merely experimentation while
I mull over a better approach. This approach relies on implementation
details I should not be relying upon

```csharp
public class HttpRequestWithFormat : HttpRequestWrapper
{
  public HttpRequestWithFormat(HttpRequest request) : base(request) { 
  }

  public override string AppRelativeCurrentExecutionFilePath {
    get
    {
      string filePath = base.AppRelativeCurrentExecutionFilePath;
      string extension = System.IO.Path.GetExtension(filePath);
      if (String.IsNullOrEmpty(extension)) {
        return filePath;
      }
      else {
        Format = extension.Substring(1);
        filePath = filePath.Substring(0, filePath.LastIndexOf(extension));
      }
      return filePath;
    }
  }

  public string Format {
    get;
    private set;
  }
}
```

I also had to write a custom route.

```csharp
public class FormatRoute : Route
{
  public FormatRoute(Route route) : 
    base(route.Url + ".{format}", route.RouteHandler) {
    _originalRoute = route;
  }

  public override RouteData GetRouteData(HttpContextBase httpContext)
  {
    //HACK! 
    var context = new HttpContextWithFormat(HttpContext.Current);

    var routeData = _originalRoute.GetRouteData(context);
    var request = context.Request as HttpRequestWithFormat;
    if (!string.IsNullOrEmpty(request.Format)) {
      routeData.Values.Add("format", request.Format);
    }

    return routeData;
  }

  public override VirtualPathData GetVirtualPath(
    RequestContext requestContext, RouteValueDictionary values)
  {
    var vpd = base.GetVirtualPath(requestContext, values);
    if (vpd == null) {
      return _originalRoute.GetVirtualPath(requestContext, values);
    }
    
    // Hack! Let's fix up the URL. Since "id" can be empty string,  
    // we want to check for this condition.
    string format = values["format"] as string;
    string funkyEnding = "/." + format as string;
    
    if (vpd.VirtualPath.EndsWith(funkyEnding)) { 
      string virtualPath = vpd.VirtualPath;
      int lastIndex = virtualPath.LastIndexOf(funkyEnding);
      virtualPath = virtualPath.Substring(0, lastIndex) + "." + format;
      vpd.VirtualPath = virtualPath;
    }

    return vpd;
  }

  private Route _originalRoute;
}
```

When matching incoming requests, this route replaces the
`HttpContextBase` with my faked up one before calling `GetRouteData`. My
faked up context returns a faked up request which strips the format
extension from the URL.

Also, when generating URLs, this route deals with the cosmetic issue
that the last segment of the URL has a default value of empty string.
This makes it so that the URL might end up looking like
*/home/index/.json*when I really wanted it to look like
/home/index.json.

I’ve omitted some code from this blog post, but you can download the
project here and try it out. Just navigate to */home/index* and then try
*/home/index.json* and you should notice the response format changes.

This is just experimental work. There’d be much more to do to make this
useful. For example, would be nice if an action could specify which
formats it would respond to. Likewise, it might be nice to respond based
on accept headers rather than formats. I just wanted to see how
automatic I could make it.

In any case, I was just having fun and didn’t have much time to put this
together. The takeaway from this is really the `CreateActionResult`
method of `ControllerActionInvoker`. That makes it very easy to create
interesting default behavior so that your action methods can return
whatever they want and you can implement your own conventions by
overriding that method.

[**Download the hacky experiment
here**](http://haacked.com/code/handlesformat.zip "Handles Format Demo")****and
play with it at your own risk. :)

