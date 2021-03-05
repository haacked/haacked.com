---
title: Rendering A Single View Using Multiple ViewEngines
tags: [aspnet,aspnetmvc]
redirect_from: "/archive/2008/11/15/composing-multiple-view-engines.aspx/"
---

One of the relatively obscure features of ASP.NET view rendering is that
you can render a single view using multiple view engines.

[Brad Wilson](http://bradwilson.typepad.com/blog/ "Brad Wilson")
actually mentioned this in his monster blog post about [Partial
Rendering and View
Engines](http://bradwilson.typepad.com/blog/2008/08/partial-renderi.html "Partial Rendering")
in [ASP.NET MVC](http://asp.net/mvc "ASP.NET MVC Website"), but the
implications may have been lost amongst all that information provided.

> One of the best features of this new system is that your partial views
> can use a different view engine than your views, and it doesn’t
> require any coding gymnastics to make it happen. It all comes down to
> how the new view system resolves which view engine renders which
> views.

Let’s dig into a brief example of this in action to understand the full
story. Lately, I’ve been playing around with the [Spark view
engine](http://dev.dejardin.org/ "Spark View Engine") lately and really
like what I see there. Unlike
[NHaml](http://andrewpeters.net/2007/12/19/introducing-nhaml-an-aspnet-mvc-view-engine/ "NHaml View Engine")
which pretty gets rid of all angle brackets via a terse DSL for
generating HTML, Spark takes the approach that HTML itself should be the
“language” for defining the view.

This is not to say that one is specifically better than the other, I’m
just highlighting the difference between the two.

Let’s take a look at a small snippet of Spark markup:

```csharp
<ul>
  <li each='var p in ViewData.Model.Products'>
    ${p.Name} ${Html.ActionLink("Edit Product", "Edit")}
  </li>  
</ul>
```

Notice that rather than embedding a for loop in the code, you apply the
`each` attribute to a piece of markup to denote that it should be
repeated. This is much more declarative than using code nuggets to
define a for loop.

As a demonstration, I thought I would take the default ASP.NET MVC
project template and within the `Index.aspx` view, I would render a
partial using the Spark view engine.

After referencing the appropriate assemblies from the Spark project,
`Spark.dll` and `Spark.Mvc.dll`, I registered the spark view engine in
`Global.asax.cs` like so:

```csharp
protected void Application_Start() {
    ViewEngines.Engines.Add(new SparkViewFactory());
    RegisterRoutes(RouteTable.Routes);
}
```

I then created a small spark partial view…

```csharp
<div class="spark-partial">
    <if condition='ViewData.ContainsKey("Title")'>
        <h1>${ViewData["Title"]}</h1>    
    </if>
    
    <p>${ViewData["Message"]}</p>
    <p>${Html.ActionLink("About Page", "About")}</p>
</div>
```

… and added it to the appropriate directory. I also added a bit of CSS
to my default stylesheet in order to highlight the partial.

![views-spark-partial](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/RenderingASingleViewUsingMultipleViewEng_BA82/views-spark-partial_3.png "views-spark-partial")

In my `Index.aspx` view, I added a call to the `Html.RenderPartial`
helper method.

```csharp
<p>This is a WebForm View.</p>
<p>But the bordered box below, is a partial rendered using Spark.</p>
<% Html.RenderPartial("SparkPartial"); %>
```

And the result...

[![spark partial view
result](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/RenderingASingleViewUsingMultipleViewEng_BA82/spark-partial-result_thumb.png "spark partial view result")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/RenderingASingleViewUsingMultipleViewEng_BA82/spark-partial-result_2.png)

When we tell the view to render the partial view named “SparkPartial”,
ASP.NET MVC will ask each registered view engine, “Hey, yous happens to
knows a partial who goes by the name of *SparkPartial*? I have some
unfinished bidness wid this guy.”

Yes, our view infrastructure speaks like a low level mobster thug.

The first view engine that answers yes, gets to render that particular
view or partial view. The benefit of this is that if you create a
partial view using one view engine, you can reuse that partial on
another site that might use a different view engine as its default.

If you want to try out the demo I created, **[download it and give it a
twirl](https://haacked.com/code/SparkViewEngineDemo.zip "SPark View Engine Demo")**.
It is built against ASP.NET MVC Beta.

