---
title: ASP.NET MVC 2 Optional URL Parameters
date: 2010-02-12 -0800
tags: [aspnet,aspnetmvc,code]
redirect_from: "/archive/2010/02/11/asp-net-mvc-2-optional-url-parameters.aspx/"
---

If you have a model object with a property named `Id`, you may have run
into an issue where your model state is invalid when binding to that
model even though you don’t have an “Id” field in your form.

The following scenario should clear up what I mean. Suppose you have the
following simple model with two properties.

```csharp
public class Product {
    public int Id { get; set; }
    public string Name { get; set; }
}
```

And you are creating a view to create a new Product. In such a view, you
obviously don’t want the user to specify the Id.

```aspx-cs
<% using (Html.BeginForm()) {{ "{%" }}>

  <fieldset>
    <legend>Fields</legend>
        
        
    <div class="editor-label">
      <%= Html.LabelFor(model => model.Name) %>
    </div>
    <div class="editor-field">
      <%= Html.TextBoxFor(model => model.Name) %>
      <%= Html.ValidationMessageFor(model => model.Name) %>
    </div>
       
    <p>
      <input type="submit" value="Create" />
    </p>
  </fieldset>

<% } %>
```

However, when you post it to an action method like so:

```csharp
[HttpPost]
public ActionResult Index(Product p)
{
    if (!ModelState.IsValid) {
        throw new InvalidOperationException("Modelstate not valid");
    }
    return View();
}
```

You’ll find that the model state is not valid. What gives!?

Well the issue here is that the `Id` property of Product is being set to
an empty string. Why is that happening when there is no “Id” field in
your form? The answer to that, my friend, is routing.

When you crack open a freshly created ASP.NET MVC 1.0 application,
you’ll notice the following default route defined.

```csharp
routes.MapRoute(
    "Default",
    "{controller}/{action}/{id}",
    new { controller = "Home", action = "Index", id = "" }
);
```

To refresh your memory, that’s a route with three URL parameters
(*controller*, *action*, *id*), each with a default value ("home",
"index", "").

What this means is if you post a form to the URL */Home/Index*, without
specifying an “ID” in the URL, you’ll still have an empty string route
value for the key “id”. And as it turns out, we use route values to bind
to action method parameters.

In the scenario above, it just so happens that your model object happens
to have a property with the same name, “Id”, as that route value, so the
model binder attempts to set the value of the `Id` property to empty
string, and since `Id` is a non-nullable int, we get a type conversion
error.

This wouldn’t be so bad if “Id” wasn’t such a common name for
properties. ;)

In ASP.NET MVC 2 RC 2, we added an MVC specific means to work around
this issue via the new `UrlParameter.Optional` value. If you set the
default value for a URL parameter to this special value, MVC makes sure
to remove that key from the route value dictionary so that it doesn’t
exist.

Thus the fix to the above scenario is to change the default route to:

```csharp
routes.MapRoute(
    "Default",
    "{controller}/{action}/{id}",
    new { controller = "Home", action = "Index", id = UrlParameter.Optional }
);
```

With this in place, if there’s no ID in the URL, there won’t be a value
for ID in the route values and thus we’ll never try to set a property
named “Id” unless you have a form field named “Id”.

Note that this should be the default in the project templates for
ASP.NET MVC 2 RTM.

