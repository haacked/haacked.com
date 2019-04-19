---
title: Upcoming Changes In Routing
date: 2008-04-10 -0800 9:00 AM
tags: [aspnet,aspnetmvc,routing]
redirect_from: "/archive/2008/04/09/upcoming-changes-in-routing.aspx/"
---

Made a few corrections on having default.aspx in the root due to a minor
bug we just found. Isn’t preview code so much fun?

We’ve been making some changes to routing to make it more powerful and
useful. But as Uncle Ben says, with more power comes more
responsibility. I’ll list out the changes first and then discuss some of
the implication of the changes.

-   **Routes no longer treat the . character as a separator.**
    Currently, routes treat the . and / characters as special. They are
    separator characters. The upcoming release of routing will only
    treat the / as separator.
-   **Routes may have multiple (non-adjacent) url parameters in a
    segment.** Currently, URL parameters in a route must fill up the
    space between separators. For example,
    *`{param1}/{param2}.{param3}`*. With the upcoming release, a URL
    segment may have more than one parameter as long as they are
    separated by a literal. For example
    *`{param1}.{ext}/{param3}-{param4}`*is now valid*.*

### Passing Parameter Values With Dots

With this change, the dot character becomes just another literal. It’s
no longer “special”. What does this buy us? Suppose you are building a
site that can present information about other sites. For example, you
might want to support URLs like this:

-   http://site-info.example.com**/site/www.haacked.com/rss**
-   http://site-info.example.com**/site/www.haacked.com/stats**

That first URL will display information about the RSS feed at
www.haacked.com while the second one will show general site stats. To
make this happen, you might define a route like this.

```csharp
routes.Add(new Route("site/{domain}/{action}" 
  , new MvcRouteHandler()) 
{ 
  Defaults=new RouteValueDictionary(new {controller="site"})  
});
```

Which routes to the following controller and action method.

```csharp
public class SiteController : Controller
{
  public void Rss(string domain)
  {
    RssData rss = GetRssData(domain);
    RenderView("Rss", rss);
  }

  public void Stats(string domain)
  {
    SiteStatistics stats = GetSiteStatistics(domain);
    RenderView("Stats", stats);
  }
}
```

The basic idea here is that the domain (such as www.haacked.com in the
example URLs above) would get passed to the `domain `parameter of the
action methods. The only problem is, it does not work with the previous
routing system because routing considered the dot character in the URL
as a separator. You would have had to define routes with URLs like
`site/{sub}.{domain}.{toplevel}/{action} `but then that doesn't work for
URLs with two sub-domains or no sub-domain.

Since we no longer treat the dot as special, this scenario is now
possible.

### Multiple URL Segments

What does adding multiple URL segments buy us? Well it continues to
allow using routing with URLs that do have extensions. For example,
suppose you want to route a request to the following action method:
`public void List(string category, string format)`

With both the previous and new routing, you can match the request for
the URL...

**`/products/list/beverages.xml`**

with the route

**`{controller}/{action}/{category}.{format}`**

To call that action method. But suppose you don’t want to use file
extensions, but still want to specify the format in a special way. With
the new routing, you can use any character as a separator. For example,
maybe you want to use the dash character to separate the category from
the format. You could then match the URL

**`/products/list/beverages-xml`**

with the route

**`{controller}/{action}/{category}-{format}`**

and still call that action method.

Note that we now allow any character (allowed in the URL and that is not
a dash) to pretty much be a separator. So if you really wanted to,
though not sure why you would, you could use *`BLAH`* to separate out
the format. Thus you could match the route

**`/products/list/beveragesBLAHxml`**

with the route

**`{controller}/{action}/{category}BLAH{format}`**

and it would still route to the same `List` method above.

### Consequences

This makes routing more powerful, but there are consequences to be aware
of.

For example, using the default routes as defined in the ASP.NET MVC
Preview 2 project template, a request for `“/Default.aspx”` fails
because it can’t find a controller with the name `“Default.aspx”`. Huh?
Well `“/Default.aspx”` now matches the route
*`{controller}/{action}/{id}`* (*because of the defaults for {id} and
{action}*) because we don’t treat the dot as special.

Not only that, what about a request for `/images/jpegs/foo.jpg`?
Wouldn’t routing try to route that to controller="images",
action="jpegs", id="foo.jpg" now?

The decision we made in this case was that **by default, routing should
not apply to files on disk**. That is, routing now checks to see if the
file is on disk before attempting to route (via the Virtual Path
Provider).

If the file is on disk, we pop out and don’t route and let the web
server handle the request normally. If the file doesn’t exist, we
attempt to apply routing. This makes sure we don’t screw around with
requests for static resources on disk. Of course, this default can be
changed by setting the property `RouteTable.Routes.RouteExistingFiles`
to be `true`.

### Why Blog This Now?

The Dynamic Data team is scooping the MVC team on these routing changes.
;) They are [releasing a
preview](http://code.msdn.microsoft.com/dynamicdata "ASP.NET Dynamic Data Preview")
of their latest changes to Dynamic Data which includes using our new
routing dll.

Check out [ScottGu’s post on the
subject](http://weblogs.asp.net/scottgu/archive/2008/04/10/asp-net-dynamic-data-preview-available.aspx "Dynamic Data Preview").
I really feel Dynamic Data is the most underrated new technology coming
out from the ASP.NET team. When you dig into it, it is really cool. The
“scaffolding” part is only the tip of the iceberg.

Installing the Dynamic Data preview requires installing the routing
assembly into the GAC. **If you install this, it may break existing MVC
Preview 2 sites**because the assembly loader favors the GAC when the
assembly is the same version. And the routing assembly is the same
version as the one in Preview 2.

The Dynamic Data Preview readme has the steps to update your MVC Preview
2 project to work with the new routing. You’ll notice that the readme
recommends having a Default.aspx file in the root which redirects to
**/Home**. Technically, the Default.aspx file in the root won’t be
necessary in the final release because of the suggested routing changes
(it is necessary now due to a minor bug). Unfortunately, Cassini doesn’t
work correctly when you make a request for "/" and there is no default
document. It doesn’t run any of the ASP.NET Modules. So we kept the file
in the root, but you ~~can~~ will be able to remove it when deploying to
IIS 7. So to recap this last point, *the Default.aspx in the project
root is to make sure that pre-SP1 Cassini works correctly as well as IIS
6 without star mapping. It's will not be needed for IIS 7 in the future,
but is needed for the time being.*

We will have a new [CodePlex source code
push](http://codeplex.com/ASPNET "CodePlex") in a couple of weeks with
an updated version of MVC that supports the new routing engine.
