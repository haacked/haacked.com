---
title: Donut Caching in ASP.NET MVC
tags: [aspnetmvc,caching]
redirect_from: "/archive/2008/11/04/donut-caching-in-asp.net-mvc.aspx/"
---

UPDATE: Due to differences in the way that ASP.NET MVC 2 processes
request, data within the substitution block can be cached when it
shouldn’t be. Substitution caching for ASP.NET MVC is not supported and
has been removed from our ASP.NET MVC Futures project.

This technique is NOT RECOMMENDED for ASP.NET MVC 2.

With [ASP.NET MVC](http://asp.net/mvc "ASP.NET MVC Website"), you can
easily cache the output of an action by using the `OutputCacheAttribute`
like so.

```csharp
[OutputCache(Duration=60, VaryByParam="None")]
public ActionResult CacheDemo() {
  return View();
}
```

One of the problems with this approach is that it is an all or nothing
approach. What if you want a section of the view to not be cached?

[![mmmm-doughnut](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DonutCachinginASP.NETMVC_E52F/mmmm-doughnut_thumb.jpg "mmmm-doughnut")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DonutCachinginASP.NETMVC_E52F/mmmm-doughnut_2.jpg)
Well ASP.NET does include a `<asp:Substitution …/>` control which allows
you to specify a method in your Page class that gets called every time
the page is requested.
[ScottGu](http://weblogs.asp.net/scottgu/ "Scott Guthrie") wrote about
this way back when in his [post on Donut
Caching](http://weblogs.asp.net/scottgu/archive/2006/11/28/tip-trick-implement-donut-caching-with-the-asp-net-2-0-output-cache-substitution-feature.aspx "Donut Caching").

However, this doesn’t seem very MVC-ish, as pointed out by Maarten
Balliauw in this post in which he [implements his own means for adding
output cache
substitution](http://blog.maartenballiauw.be/post/2008/07/01/Extending-ASPNET-MVC-OutputCache-ActionFilterAttribute-Adding-substitution.aspx "Adding ASP.NET MVC OutputCache Subsitution").

However, it turns out that the `Substitution` control I mentioned
earlier makes use of an existing API that’s already publicly available
in ASP.NET. The `HttpResponse` class has a `WriteSubstitution` method
which accepts an `HttpResponseSubstitutionCallback` delegate. The method
you supply is given an `HttpContext` instance and allows you to return a
string which is displayed in place of the substitution point.

I thought it’d be interesting to create an Html helper which makes use
of this API, but supplies an `HttpContextBase` instead of an
`HttpContext`. Here’s the source code for the helper and delegate.

```csharp
public delegate string MvcCacheCallback(HttpContextBase context);

public static object Substitute(this HtmlHelper html, MvcCacheCallback cb) {
    html.ViewContext.HttpContext.Response.WriteSubstitution(
        c => HttpUtility.HtmlEncode(
            cb(new HttpContextWrapper(c))
        ));
    return null;
}
```

The reason this method returns a null object is to make the usage of it
seem natural. Let me show you the usage and you’ll see what I mean.
Referring back to the controller code at the beginning of this post,
imagine you have the following markup in your view.

```aspx-cs
<!-- this is cached -->
<%= DateTime.Now %>

<!-- and this is not -->
<%= Html.Substitute(c => DateTime.Now.ToString()) %>
```

On the first request to this action, the entire page is rendered
dynamically and you’ll see both dates match. But when you refresh, only
the lambda expression is called and is used to replace that portion of
the cached view.

I have to thank Dmitry, our PUM and the first developer on ASP.NET way
back in the day who pointed out this little known API method to me. :)

We will be looking into hopefully including this in v1 of ASP.NET MVC,
but I make no guarantees.

