---
layout: post
title: "Using a Decorator to Hook Into A WebControl's Rendering for Better XHTML Compliance"
date: 2006-01-17 -0800
comments: true
disqus_identifier: 11537
categories: []
---
Man! What a mouthful of a title, but I think it succinctly describes
what this post is about. I will demonstrate how to hook into the
rendering of a control that inherits from
`System.Web.UI.WebControls.WebControl` using a
[Decorator](http://www.dofactory.com/Patterns/PatternDecorator.aspx). In
particular, I am going to hook into the rendering of a `Button` control
to stop it from emitting the `language="javascript"` attribute.

Why?

Because I am a bit anal about XHTML compliance. The `Button` control
renders an `input` tag with the language attribute. But according to the
XHTML 1.0 transitional spec, this is an invalid attribute.

More than just being anal, I also thought it would serve as a nice
demonstration of this technique in case you want to build custom
controls that modify the rendering of other controls just slightly
without having to rewrite a lot of code.

### First Naive Attempt

My first attempt to handle this was to simply try and remove the
language attribute via the following code placed in the `OnPreRender`
method of my page:

~~~~ {style="margin: 0px;"}
btnSubmit.Attributes.Remove("language");
~~~~

That didn’t work because the button control doesn’t explicitly add the
language attribute to the attributes collection. Instead, the attribute
is added within the `Render` method which is called by the page when it
is time for a control to render its contents to HTML.

### Examining The Rendering Process

The `Render` method is passed an instance of `HtmlTextWriter` used to
render the page. One of the methods on this class is `AddAttribute`
which has several overrides. Using
[Reflector](http://www.aisto.com/roeder/dotnet/) I found that the method
that adds the language attribute has the signature
`AddAttribute(string name, string value);`.

### The Decorator

Now if only I had some way to override that method to discard attributes
with the name “language”. That’s where the decorator pattern comes in.

The class I want to decorate is the `HtmlTextWriter`. Fortunately the
authors of this class did a good job of making it extensible and easy to
decorate. `HtmlTextWriter` has a constructor that takes in an instance
of `TextWriter`. Methods on the `HtmlTextWriter` use the specified
`TextWriter` to write to the underlying stream. The good news is that
`HtmlTextWriter` inherits from `TextWriter`. So if I want to hook into
the rendering process, I just need to implement my own `HtmlTextWriter`
and override the specific methods I need.

### The CompliantButton class

The first step is to create a `CompliantButton` class that inherits from
`Button`. Within that class I created a private internal class named
`CompliantHtmlTextWriter` like so:

private class CompliantHtmlTextWriter : HtmlTextWriter

{

    internal CompliantHtmlTextWriter(HtmlTextWriter writer) :
base(writer)

    {

    }

 

    /// \<summary\>

    /// Ignores the language attribute for the purposes of a submit
button.

    /// \</summary\>

    public override void AddAttribute(string name, string value, bool
fEndode)

    {

        if(String.Compare(name, "language", true,
CultureInfo.InvariantCulture) == 0)

            return;

        base.AddAttribute (name, value, fEndode);

    }

}

This is the decorator. Notice that the constructor takes in another
`HtmlTextWriter` which it will forward method calls to. The
`AddAttribute` method simply forwards calls to the base class unless the
attribute name is “language”.

### Redecorating

Now all that is left is to use the decorator within the render method of
the `CompliantButton` class. Here is the render method:

protected override void Render(System.Web.UI.HtmlTextWriter writer)

{

    base.Render(new CompliantHtmlTextWriter(writer));

}

Notice that I am wrapping (decorating) the `HtmlTextWriter` parameter
with my `CompliantHtmlTextWriter` decorator before passing it along to
the base `Render` method. As far as the base `Render` method is
concerned, it is dealing with an `HtmlTextWriter`. It doesn’t need to
know any specifics about the decorator class. But via decoration, the
behavior has been slightly modified. No more language attribute.

