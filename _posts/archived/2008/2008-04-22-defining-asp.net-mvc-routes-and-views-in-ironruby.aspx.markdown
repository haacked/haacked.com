---
title: Defining ASP.NET MVC Routes and Views in IronRuby
date: 2008-04-22 -0800 9:00 AM
tags: [aspnetmvc,languages]
redirect_from: "/archive/2008/04/21/defining-asp.net-mvc-routes-and-views-in-ironruby.aspx/"
---

In a [recent
post](https://haacked.com/archive/2008/04/18/dynamic-language-dsl-vs-xml-configuration.aspx "Dynamic Language DSL vs XML Config")
I expressed a few thoughts on using a DSL instead of an XML config file.
I followed that up with a technical look at [monkey patching CLR objects
using
IronRuby](https://haacked.com/archive/2008/04/18/monkey-patching-clr-objects.aspx "Monkey Patching"),
which explores a tiny bit of interop.

These posts were precursors to this post in which I apply these ideas to
an implementation that allows me to define ASP.NET MVC Routes using
IronRuby. Also included in this download is an incomplete implementation
of an IronRuby view engine. I haven't yet implemented layouts.

[**IronRubyMvcDemo.zip Download (4.93
MB)**](https://haacked.com/code/IronRubyMvcDemo.zip "IronRubyMvcDemo.zip")

This implementation works with the latest [CodePlex drop of
MVC](http://www.codeplex.com/aspnet/Release/ProjectReleases.aspx?ReleaseId=12640 "CodePlex release").

To use routes written in Ruby, reference the IronRubyMvcLibrary from
your MVC Web Application and import the `IronRubyMvcLibrary.Routing`
namespace into your `Global.asax` code behind file. From there, you can
just call an extension method on `RouteCollection` like so...

```csharp
public class GlobalApplication : System.Web.HttpApplication
{
  protected void Application_Start()
  {
    RouteTable.Routes.LoadFromRuby();
  }
}
```

This will look for a `Routes.rb` file within the webroot and use that
file to load routes. Here's a look at mine:

```csharp
$routes.map "products/{action}/{id}"
  , {:controller => 'products', :action => 'categories', :id => ''}
$routes.map "{controller}/{action}/{id}", {:id => ''}
$routes.map "{controller}", {:action => 'index'}, {:controller => '[^\.]*'}
$routes.map "default.aspx", {:controller => 'home', :action => 'index'}
```

That’s it. No other cruft in there. I tried experimenting with lining up
each segment using tabs so it looks like an actual table of data, rather
than simply code definitions.

Also included in this download is a sample web app that makes use of the
`IronRubyViewEngine`. You can see how I applied Monkey Patching to make
referencing view data cleaner. Within an IronRuby view, you can access
the view data via a global variable, \$model. The nice part is, whether
you pass strongly typed data or not to the view, you can always
reference view data via `$model.property_name`.

In the case where the view data is a view data dictionary, this will
perform a dictionary lookup using the property name as the key.

Be sure to check out the unit tests which provide over 95% code coverage
of my code if you want to understand this code and improve on it. Next
stop, Controllers in IronRuby...

