---
title: Model Binding To A List
tags: [aspnetmvc]
redirect_from: "/archive/2008/10/22/model-binding-to-a-list.aspx/"
---

The **[sample project code is on GitHub](https://github.com/haacked/CodeHaacks/tree/main/src/ListModelBindingDemo "ListModelBinding Demos")**. You can play with the code as you read this blog post.

Using the `DefaultModelBinder` in ASP.NET MVC, you can bind submitted form values to arguments of an action method. But what if that argument is a collection? Can you bind a posted form to an `ICollection<T>`?

Sure thing! It’s really easy if you’re posting a bunch of primitive types. For example, suppose you have the following action method.

```csharp
public ActionResult UpdateInts(ICollection<int> ints) {
  return View(ints);
}
```

You can bind to that by simply submitting a bunch of form fields each having the same name. For example, here’s an example of a form that would bind to this, assuming you keep each value a proper integer.

```csharp
<form method="post" action="/Home/UpdateInts">
    <input type="text" name="ints" value="1" />
    <input type="text" name="ints" value="4" />
    <input type="text" name="ints" value="2" />
    <input type="text" name="ints" value="8" />
    <input type="submit" />
</form>
```

If you were to take fiddler and look at what data actually gets posted
when clicking the submit button, you’d see the following.

```
**ints**=1&**ints**=4&**ints**=2&**ints**=8
```

The default model binder sees all these name/value pairs with the same name and converts that to a collection with the key **`ints`**, which is then matched up with the `ints` parameter to your action method. Pretty simple!

Where it gets trickier is when you want to post a list of complex types. Suppose you have the following class and action method.

```csharp
public class Book {
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime DatePublished { get; set; }
}

//Action method on HomeController
public ActionResult UpdateProducts(ICollection<Book> books) {
    return View(books);
}
```

You might think we could simply post the following to that action method:

> `Title=title+one&Author=author+one&DateTime=1/23/1975&Title=author+two&Author=author+two&DateTime=6/6/2007…`

Notice how we simply repeat each property of the book in the form post data? Unfortunately, that wouldn’t be a very robust approach. One reason is that we can’t distinguish from the fact that there may well be another `Title` input unrelated to our list of books which could throw off our binding.

Another reason is that the *checkbox* input does not submit a value if it isn’t checked. Most input fields, when left blank, will submit the field name with a blank value. With a checkbox, neither the name nor value is submitted if it’s unchecked! This again can throw off the ability of the model binder to match up submitted form values to the correct object in the list.

To bind complex objects, we need to provide an index for each item, rather than relying on the order of items. This ensures we can unambiguously match up the submitted properties with the correct object.

Here’s an example of a form that submits three books.

```csharp
<form method="post" action="/Home/Create">

    <input type="text" name="[0].Title" value="Curious George" />
    <input type="text" name="[0].Author" value="H.A. Rey" />
    <input type="text" name="[0].DatePublished" value="2/23/1973" />
    
    <input type="text" name="[1].Title" value="Code Complete" />
    <input type="text" name="[1].Author" value="Steve McConnell" />
    <input type="text" name="[1].DatePublished" value="6/9/2004" />
    
    <input type="text" name="[2].Title" value="The Two Towers" />
    <input type="text" name="[2].Author" value="JRR Tolkien" />
    <input type="text" name="[2].DatePublished" value="6/1/2005" />
    
    <input type="submit" />
</form>
```

Note that the index must be an unbroken sequence of integers starting at 0 and increasing by 1 for each element.

The new expression based helpers in ASP.NET MVC 2 will produce the correct format within a for loop. Here’s an example of a view that outputs this format:

```aspx-cs
<%@ Page Inherits="ViewPage<IList<Book>>" %>

<% for (int i = 0; i < 3; i++) { %>

  <%: Html.TextBoxFor(m => m[i].Title) %>
  <%: Html.TextBoxFor(m => m[i].Author) %>
  <%: Html.TextBoxFor(m => m[i].DatePublished) %> 

<% } %>
```

It also works with our templated helpers. For example, we can take the part inside the for loop and put it in a ***Books.ascx*** editor template.

```aspx-cs
<%@ Control Inherits="ViewUserControl<Book>" %>

<%: Html.TextBoxFor(m => m.Title) %>
<%: Html.TextBoxFor(m => m.Author) %>
<%: Html.TextBoxFor(m => m.DatePublished) %> 
```

Just add a folder named ***EditorTemplates*** within the Views/Shared folder and add ***Books.ascx*** to this folder.

Now change the original view to look like:

```aspx-cs
<%@ Page Inherits="ViewPage<IList<Book>>" %>

<% for (int i = 0; i < 3; i++) { %>

  <%: Html.EditorFor(m => m[i]) %>

<% } %>
```

### Non-Sequential Indices

Well that’s all great and all, but what happens when you can’t guarantee that the submitted values will maintain a sequential index? For example, suppose you want to allow deleting rows before submitting a list of
books via JavaScript.

The good news is that by introducing an extra hidden input, you can allow for arbitrary indices. In the example below, we provide a hidden input with the *.Index* suffix for each item we need to bind to the list. The name of each of these hidden inputs are the same, so as described earlier, this will give the model binder a nice collection of
indices to look for when binding to the list.

```csharp
<form method="post" action="/Home/Create">

    <input type="hidden" name="products.Index" value="cold" />
    <input type="text" name="products[cold].Name" value="Beer" />
    <input type="text" name="products[cold].Price" value="7.32" />
    
    <input type="hidden" name="products.Index" value="123" />
    <input type="text" name="products[123].Name" value="Chips" />
    <input type="text" name="products[123].Price" value="2.23" />
    
    <input type="hidden" name="products.Index" value="caliente" />
    <input type="text" name="products[caliente].Name" value="Salsa" />
    <input type="text" name="products[caliente].Price" value="1.23" />
    
    <input type="submit" />
</form>
```

Unfortunately, we don’t have a helper for generating these hidden inputs. However, I’ve hacked together an extension method which can render this out for you.

When you’re creating a form to bind a list, add the following hidden input and it will add the appropriate hidden input to allow for a broken sequence of indices. **Use at your own risk!**I’ve only tested this in a couple of scenarios. I’ve **[included sample code](https://github.com/haacked/CodeHaacks/tree/main/src/ListModelBindingDemo "ListModelBinding Demos")**
with multiple samples of binding to a list which includes the source code for this helper.

```aspx-cs
<%: Html.HiddenIndexerInputForModel() %>
```

This is something we may consider adding to a future version of ASP.NET MVC. In the meanwhile, give it a whirl and let us know how it works out for you.
