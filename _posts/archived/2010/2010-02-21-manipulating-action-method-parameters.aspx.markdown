---
title: Manipulating Action Method Parameters
date: 2010-02-21 -0800
disqus_identifier: 18687
tags:
- asp.net
- asp.net mvc
- code
redirect_from: "/archive/2010/02/20/manipulating-action-method-parameters.aspx/"
---

During the MVP summit, an attendee asked me for some help with a common
scenario common among those building content management systems. He
wanted his site to use human friendly URLs.

  `http://example.com/pages/a-page-about-nothing/`

instead of

  `http://example.com/pages/123/`

Notice how the first URL is descriptive whereas the second is not. The
first URL contains a URL “slug” while the second one contains the ID for
the content, typically associated with the ID in the database.

This is easy enough to set up with routing, but there’s a slight twist.
He still wanted the action method which would respond to the first URL
to have the integer integer ID as the parameter, not the slug. Let’s
look at one possible approach to solving this.

Here’s an example of what the route might look like:

```csharp
routes.MapRoute(
  "Slug", // Route name
  "pages/{slug}", // URL with parameters
  new { controller = "Home", action = "Content" } // Parameter defaults
);
```

Notice that the route URL contains one parameter for “slug” and no “id”
parameter whatsoever. Here’s an example of the controller action that
route should map to.

```csharp
public ActionResult Content(int id)
{
  // Note the argument is an id, not slug
  return View();
}
```

Note that the action method does not accept a parameter named “slug” but
instead expects an integer “id” parameter.

Fortunately, there’s an easy way to do this. Action filters, classes
which derive from `ActionFilterAttribute`, allow hooking into the point
in time after the parameters of action method have been bound, but just
before the action method has been invoked. This gives us a fine
opportunity to muck around with the parameters.

The following is an example of an action filter which converts a slug to
an ID (you can imagine a real one would probably look it up in the
database, not in a static dictionary like the sample does).

```csharp
public class SlugToIdAttribute : ActionFilterAttribute
{
  static IDictionary<string, int> Slugs = new Dictionary<string, int>
  {
    {"this-is-a-slug", 100}, 
    {"another-slug", 101}, 
    {"and-another", 102}
  };

  public override void OnActionExecuting(ActionExecutingContext filterContext)
  {
    var slug = filterContext.RouteData.Values["slug"] as string;
    if(slug != null)
    {
      int id;
      Slugs.TryGetValue(slug, out id);
      filterContext.ActionParameters["id"] = id;
    }
    base.OnActionExecuting(filterContext);
  }
}
```

The filter overrides the `OnActionExecuting` method which is called just
before the action method is called. The filter than grabs the slug from
the route data, and looks up the corresponding id. Now all we need to do
is make sure the id is passed into the action method.

Fortunately the filter context passed into this method allows us to peek
into the parameters that will get passed into the action method via the
`ActionParameters` property. Not only that, *it allows us to change
them!*

In this case, I’m grabbing the slug from the route data, and looking up
the associated id, and adding a parameter named “id” to the action
parameters with the correct id value.

All I need to do now is apply this filter to the action method and when
the action method is called, this id will be passed into the method.

This works whether the argument to the action method is a simple
primitive type as in this example or whether it’s a complex type. I’ve
**[included a sample
project](http://code.haacked.com/mvc-2/ActionParameterManipulationDemo.zip "Sample Demo")**
that demonstrates changing parameters to action methods via an action
filter.

