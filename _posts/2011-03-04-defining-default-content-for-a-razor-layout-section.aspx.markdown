---
layout: post
title: "Defining Default Content For A Razor Layout Section"
date: 2011-03-04 -0800
comments: true
disqus_identifier: 18767
categories: [asp.net,asp.net mvc]
---
Layouts in Razor serve the same purpose as Master Pages do in Web Forms.
They allow you to specify a layout for your site and carve out some
placeholder sections for your views to implement.

For example, here’s a simple layout with a main body section and a
footer section.

```csharp
<!DOCTYPE html>
<html>
<head><title>Sample Layout</head>
<body>
    <div>@RenderBody()</div>
    <footer>@RenderSection("Footer")</footer>
</body>
</html>
```

In order to use this layout, your view might look like.

```csharp
@{
    Layout = "MyLayout.cshtml";
}
<h1>Main Content!</h1>
@section Footer {
    This is the footer.
}
```

Notice we use the `@section` syntax to specify the contents for the
defined Footer section.

But what if we have other views that don’t specify content for the
Footer section? They’ll throw an exception stating that the “Footer”
section wasn’t defined.

To make a section optional, we need to call an overload of
`RenderSection` and specify `false` for the `required` parameter.

```csharp
<!DOCTYPE html>
<html>
<head><title>Sample Layout</head>
<body>
    <div>@RenderBody()</div>
    <footer>@RenderSection("Footer", false)</footer>
</body>
</html>
```

But wouldn’t it be nicer if we could define some default content in the
case that the section isn’t defined in the view?

Well here’s one way. It’s a bit ugly, but it works.

```csharp
<footer>
  @if (IsSectionDefined("Footer")) {
      RenderSection("Footer");
  }
  else { 
      <span>This is the default yo!</span>   
  }
</footer>
```

That’s some ugly code. If only there were a way to write a version of
`RenderSection` that could accept some Razor markup as a parameter to
the method.

[**Templated Razor
Delegates**](http://haacked.com/archive/2011/02/27/templated-razor-delegates.aspx "Templated Razor Delegates")**to
the rescue!**See, I told you these things would come in handy.

We can write an extension method on `WebPageBase` that encapsulates this
bit of ugly boilerplate code. Here’s the implementation.

```csharp
public static class Helpers {
  public static HelperResult RenderSection(this WebPageBase webPage, 
      string name, Func<dynamic, HelperResult> defaultContents) {
    if (webPage.IsSectionDefined(name)) {
      return webPage.RenderSection(name);
    }
    return defaultContents(null);
  }
}
```

What’s more interesting than this code is how we can use it now. My
Layout now can do the following to define the Footer section:

```csharp
<footer>
  @this.RenderSection("Footer", @<span>This is the default!</span>)
</footer>
```

That’s much cleaner! But we can do even better. Notice how there’s that
ugly `this` keyword? That’s necessary because when you write an
extension method on the current class, you have to call it using the
`this` kewyord.

Remember when I wrote about [how to change the base type of a Razor
view](http://haacked.com/archive/2011/02/21/changing-base-type-of-a-razor-view.aspx "Changing the base type of a Razor view")?
Here’s a case where that really comes in handy.

What we can do is write our own custom base page type (such as the
`CustomWebViewPage` class I used in that blog post) and add the
`RenderSection` method above as an instance method on that class. I’ll
leave this as an exercise for the reader.

The end result will let you do the following:

```csharp
<footer>
  @RenderSection("Footer", @<span>This is the default!</span>)
</footer>
```

Pretty slick!

You might be wondering why we didn’t just include this feature in Razor.
My guess is that we wanted to but just ran out of time. Hopefully this
will make it in the next version of Razor.

