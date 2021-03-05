---
title: Changing Base Type Of A Razor View
tags: [aspnet,aspnetmvc,razor]
redirect_from: "/archive/2011/02/20/changing-base-type-of-a-razor-view.aspx/"
---

Within a Razor view, you have access to a base set of properties (such
as `Html`, `Url`, `Ajax`, etc.) each of which provides methods you can
use within the view.

For example, in the following view, we use the `Html` property to access
the `TextBox` method.

<pre class="csharpcode"><code.
<span class="asp">@</span>Html.TextBox(<span class="str">"SomeProperty"</span>)
</code></pre>

`Html` is a property of type `HtmlHelper` and there are a large number
of useful extension methods that hang off this type, such as  `TextBox`.

But where did the `Html` property come from? It’s a property of
`System.Web.Mvc.WebViewPage`, the default base type for all razor views.
If that last phrase doesn’t make sense to you, let me explain.

Unlike many templating engines or interpreted view engines, Razor views
are dynamically compiled at runtime into a class and then executed. The
class that they’re compiled into derives from `WebViewPage`. For long
time ASP.NET users, this shouldn’t come as a surprise because this is
how ASP.NET pages work as well.

Customizing the Base Class
--------------------------

HTML 5 (or is it simply “HTML” now) is a big topic these days. It’d be
nice to write a set of HTML 5 specific helpers extension methods, but
you’d probably like to avoid adding even more extension methods to the
`HtmlHelper` class because it’s already getting a little crowded in
there.

![html-extensions](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Advanced-Razor-View-Extensibility_12651/html-extensions_3.png "html-extensions")

Well perhaps what we need is a new property we can access from within
Razor. Well how do we do that?

What we need to do is change the base type for all Razor views to
something we control. Fortunately, that’s pretty easy. When you create a
new ASP.NET MVC 3 project, you might have noticed that the *Views*
directory contains a *Web.config* file.

Look inside that file and you’ll notice the following snippet of XML.

```xml
<system.web.webPages.razor>
    <host factoryType="System.Web.Mvc.MvcWebRazorHostFactory, 
    System.Web.Mvc, Version=3.0.0.0, 
    Culture=neutral, PublicKeyToken=31BF3856AD364E35" />
  <pages pageBaseType="System.Web.Mvc.WebViewPage">
    <namespaces>
      <add namespace="System.Web.Mvc" />
      <add namespace="System.Web.Mvc.Ajax" />
      <add namespace="System.Web.Mvc.Html" />
      <add namespace="System.Web.Routing" />
    </namespaces>
  </pages>
</system.web.webPages.razor>
```

The thing to notice is the `<pages>` element which has the
`pageBaseType` attribute. The value of that attribute specifies the base
page type for all Razor views in your application. But you can change
that value by simply replacing that value with your custom class. While
it’s not strictly required, it’s pretty easy to simply write a class
that derives from `WebViewPage`.

Let’s look at a simple example of this.

```csharp
public abstract class CustomWebViewPage : WebViewPage {
  public Html5Helper Html5 { get; set; }

  public override void InitHelpers() {
    base.InitHelpers();
    Html5 = new Html5Helper<object>(base.ViewContext, this);
  }
}
```

Note that our custom class derives from `WebViewPage`, but adds a new
Html5 property of type Html5Helper. I’ll show the code for that helper
here. In this case, it pretty much follows the pattern that `HtmlHelper`
does. I’ve left out some properties for brevity, but at this point, you
can add whatever you want to this class.

```csharp
public class Html5Helper {
  public Html5Helper(ViewContext viewContext, 
    IViewDataContainer viewDataContainer)
    : this(viewContext, viewDataContainer, RouteTable.Routes) {
  }

  public Html5Helper(ViewContext viewContext,
     IViewDataContainer viewDataContainer, RouteCollection routeCollection) {
    ViewContext = viewContext;
    ViewData = new ViewDataDictionary(viewDataContainer.ViewData);
  }

  public ViewDataDictionary ViewData {
    get;
    private set;
  }

  public ViewContext ViewContext {
    get;
    private set;
  }
}
```

Let’s write a simple extension method that takes advantage of this new
property first, so we can get the benefits of all this work.

```csharp
public static class Html5Extensions {
    public static IHtmlString EmailInput(this Html5Helper html, string name,       string value) {
        var tagBuilder = new TagBuilder("input");
        tagBuilder.Attributes.Add("type", "email");
        tagBuilder.Attributes.Add("value", value);
        return new HtmlString(tagBuilder.ToString());
    }
}
```

Now, if we change the `pageBaseType` to `CustomWebViewPage`, we can
recompile the application and start using the new property within our
Razor views.

![Html5Helpers](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/Advanced-Razor-View-Extensibility_12651/Html5Helpers_3.png "Html5Helpers")

Nice! We can now start using our new helpers. Note that if you try this
and don’t see your new property in Intellisense right away, try closing
and re-opening Visual Studio.

What about Strongly Typed Views
-------------------------------

What if I have a Razor view that specifies a strongly typed model like
so:

<pre class="csharpcode"><code>
<span class="asp">@</span>model Product
<span class="asp">@</span>{
    ViewBag.Title = <span class="str">"Home Page"</span>;
}

&lt;p&gt;<span class="asp">@</span>Model.Name&lt;/p&gt;</code></pre>

The base class we wrote wasn’t a generic class so how’s this going to
work? Not to worry. This is the part of Razor that’s pretty cool. We can
simply write a generic version of our class and Razor will inject the
model type into that class when it compiles the razor code.

In this case, we’ll need a generic version of both our
`CustomWebViewPage` and our `Html5Helper` classes. I’ll follow a similar
pattern implemented by `HtmlHelper<T>` and `WebViewPage<T>`.

```csharp
public abstract class CustomWebViewPage<TModel> : CustomWebViewPage {
  public new Html5Helper<TModel> Html5 { get; set; }

  public override void InitHelpers() {
    base.InitHelpers();
    Html5 = new Html5Helper<TModel>(base.ViewContext, this);
  }
}

public class Html5Helper<TModel> : Html5Helper {
  public Html5Helper(ViewContext viewContext, IViewDataContainer container)
    : this(viewContext, container, RouteTable.Routes) {
  }

  public Html5Helper(ViewContext viewContext, IViewDataContainer container, 
      RouteCollection routeCollection) : base(viewContext, container,
      routeCollection) {
    ViewData = new ViewDataDictionary<TModel>(container.ViewData);
  }

  public new ViewDataDictionary<TModel> ViewData {
    get;
    private set;
  }
}
```

Now you can write extension methods of `Html5Helper<TModel>` which will
have access to the model type much like `HtmlHelper<TModel>` does.

As usual, if there’s a change you want to make, there’s probably an
extensibility point in ASP.NET MVC that’ll let you make it. The tricky
part of course, in some cases, is finding the correct point.

