---
title: Grouping Controllers with ASP.NET MVC
tags: [aspnet,aspnetmvc]
redirect_from: "/archive/2008/11/03/areas-in-aspnetmvc.aspx/"
---

UPDATE: I updated the prototype to work against the ASP.NET MVC 1.0 RTM.
Keep in mind, this is \*NOT\* a backport of the the ASP.NET MVC 2
feature so there may be some differences.

A question that this. The funny part with things like this is that I’ve
probably spent as much time writing this blog post as I did working on
the prototype, if not more!

The scenario that areas address is being able to partition your
application into discrete areas of functionality. It helps make managing
a large application more manageable and allows for creating distinct
applets that you can drop into an application.

For example, suppose I want to drop in a *blogs* subfolder, complete
with its own controllers and views, along with a *forums* subfolder with
its own controllers and views, into a default project. The end result
might look like the following screenshot (area folders highlighted).

![areas-folder-structure](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/36ef165d4ea4_ED92/areas-folder-structure_6.png "areas-folder-structure")

Notice that these folders have their own *Views*, *Content*, and
*Controllers* directories. This is slightly similar to a [solution
proposed by Steve
Sanderson](http://blog.codeville.net/2008/07/30/partitioning-an-aspnet-mvc-application-into-separate-areas/ "Partitioning an ASP.NET MVC Application into separate areas"),
but he ran into a few problems we’d like to resolve.

-   URL generation doesn’t take namespaces into consideration when
    generating a URL. We want to be able to easily generate URLs to
    other areas.
-   When you are within one area, and you call `Html.ActionLink` to link
    to another action in the same area, you’d like to not have to
    specify the area name. You’d also like to not be forced to specify a
    route name.
-   You still want to be able to link to another area by specifying the
    area name. And, you want to be able to have controllers of the same
    name within the same area.
-   You also want to be able to link to the “root” area, aka the default
    `HomeController` that comes with the project template that is not
    located in an area.

The prototype I put together resolves these problems by adopting and
enforcing a few constraints when it comes to areas.

-   The area portion comes first in the URL.
-   Controller namespaces must have a specific format that includes the
    area name in the namespace.
-   The root controllers that are not in any area have a default area
    name of “root”.
-   When resolving a View/Partial View for a controller within an area,
    we search in the area’s *Views* folder first. If not found there, we
    then look in the root *Views* folder.

### Overridable Templating

This last point bears a bit of elaboration. It is a technique that came
about from some experimentation I did on a potential new way of skinning
for [Subtext](http://subtextproject.com/ "Subtext Blog Engine").

In the *Blogs* area, I have a partial view called
`LoginUserControl.ascx`. In the *Forums* area, I don’t have this partial
view. Thus when you go to the *Forums* area, it falls back to the root
*Views* directory in order to render this partial view. But in the
*Blogs* area, it uses the one specified in the area. This is a
convenient way of implementing overridable templating and is reminiscent
of ASP.NET Dynamic Data.

If you run the sample, you’ll see what I mean. When you hit the *Blogs*
area, the login link is replaced by some text saying “Blogs don’t need
no stinking login”, but the *Forums* area still has the login link.

Note that all of these conventions are specifically for this prototype.
It would be very easy to relax these constraints to fit you’re own way
of doing things. I just wanted to show how this could be done using the
current ASP.NET MVC bits.

### Registering Routes

The first thing we do is call two new extension methods I wrote to
register routes for the areas. This call is made in the `RegisterRoutes`
method in `Global.asax.cs`.

```csharp
routes.MapAreas("{controller}/{action}/{id}", 
    "AreasDemo", 
    new[]{ "Blogs", "Forums" });

routes.MapRootArea("{controller}/{action}/{id}", 
    "AreasDemo", 
    new { controller = "Home", action = "Index", id = "" });
```

The first argument to the `MapAreas` method is the Routing URL pattern
you know and love. We will prepend an area to that URL. The second
argument is a root namespace. By convention, we will append
“.Areas.*AreaName.*Controllers” to the provided root namespace and use
that as the namespace in which we lookup controller types.

For example, suppose you have a root namespace of `MyRootNamespace`. If
you have a `HomeController` class within the *Blogs* area, its full type
name would need to be

`MyRootNamespace.Areas.Blogs.Controllers.HomeController`.

Again, this is a convention I made up, it could be easily changed. The
nice thing about following this convention is you don’t really have to
think about namespaces if you follow the directory structure I outlined.
You just focus on your areas.

The last argument to the method is a string array of the “areas” in your
application. Perhaps I could derive this automatically by examining the
file structure, but I put together this prototype in the morning and
didn’t think of that till I was writing this blog post. ;)

The second method, MapRootArea, is exactly the same as MapRoute, except
it adds a default of area = “root” to the defaults dictionary.

### Registering the ViewEngine

I also wrote a very simple custom view engine that knows how to look in
the *Areas* folder first, before looking in the root *Views* folder when
searching for a view or partial view.

I wrote this in such a way that it replaces the default view engine. To
make this switch, I added the following in Global.asax.cs in the
Application\_Start method.

```csharp
ViewEngines.Engines.Clear();
ViewEngines.Engines.Add(new AreaViewEngine());
```

The code for the `AreaViewEngine` is fairly simple. It inherits from
`WebFormViewEngine` and looks in the appropriate Areas first for a given
view or partial view before looking in the default location. The way I
accomplished this was by adding some catch-all location formats such as
`~/{0}.aspx` and formatted those myself in the code.

If that last sentence meant nothing to you, don’t worry. It’s an
implementation detail of the view engine.

### Linking to Areas

In the root view, I have the following markup to link to the
`HomeController` and `Index` action of each area.

```aspx-cs
<%= Html.ActionLink("Blog Home", "Index", new { area="Blogs" } )%>
<%= Html.ActionLink("Forums Home", "Index", new { area="Forums" } )%>
```

However, within an area, I don’t have to specify the area when linking
to another action within the same area. It chooses the current area by
default. For example, here’s the code to render a link to the Blogs
area’s Posts action.

```aspx-cs
<%= Html.ActionLink("Blogs Posts", "Posts") %>
```

That’s no different than if you weren’t doing areas. Of course, if I
want to link to the forums area, I need to specify that. Also, if I want
to link to an action in the root, I need to specify that as well.

```aspx-cs
<%= Html.ActionLink("Forums", "Index", "new {area="forums"}") %>
<%= Html.ActionLink("Root Home", "Index", "new {area="root"}") %>
```

As you click around in the sample, you’ll notice that I changed the
background color when in a different area to highlight that fact.

### Next Step, Nested Areas

One thing my prototype doesn’t address are nested areas. This is
something I’ll try to tackle next. I’m going to see if I can clean up
the implementation later and possibly get them into the MVC Futures
project. This is just some early playing around I did on my own so do
let me know if you have better ideas for improving this.

**[Download the
Sample](http://code.haacked.com/mvc-1.0/areas-for-mvc-1.0.zip "Areas Demo")**

