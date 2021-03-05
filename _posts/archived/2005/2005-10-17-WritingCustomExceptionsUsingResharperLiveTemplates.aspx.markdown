---
title: Writing Custom Exceptions Using Resharper Live Templates
tags: [tools]
redirect_from: "/archive/2005/10/16/WritingCustomExceptionsUsingResharperLiveTemplates.aspx/"
---

Writing proper custom exceptions can amount to a lot of busy work. Oh
sure, it’s easy to simply inherit from `System.Exception` and stop
there. But try running that baby through FxCop or passing that exception
across AppDomains and you’re in for a world of hurt (hyperbole alert!).

What makes writing a custom exception a pain? First, there are all those
constructors you have to implement. You also need to remember to mark
the class with the `Serializable` attribute. Also, if your exception has
at least one custom property, then you’ll want to implement
`ISerializable`, a special serialization constructor, and more
constructors that accept the new property.

On page 411 of *[Applied .NET Framework
Programming](http://www.amazon.com/gp/product/0735614229/103-9411210-6787060?v=glance&n=283155&v=glance)*,
Jeffrey Richter outlines the steps to write a proper custom exception
class.

If you have this book, you should definitely read and learn these steps.
Or if you are a [ReSharper](http://www.jetbrains.com/resharper/) user,
you can be lazy and just use the Live Template (akin to a Whidbey Code
Snippet) I’ve [created and posted
here](https://haacked.com/assets/images/ExceptionLiveTemplate.zip) for your
exceptional enjoyment.

Unfortunately, I do not know of any way to export and import live
templates within ReSharper, so you’ll have to follow the steps I
[outlined in a previous
post](https://haacked.com/archive/2004/08/20/954.aspx).

I have included two templates. The first is for a full-blown sealed
custom exception with a single custom property. It’s easy enough to add
more properties if you need them. The second is for a simple custom
exception with no custom properties. The ReadMe.txt file included
outlines a couple of settings you need to make for a couple template
variables.

I ended up using the abbreviation `excc` to expand the full exception
class and `excs` for the simple exception class. This ought to save you
a lot of typing. Below is a screenshot of the full template...

![Exception Live
Template](https://haacked.com/assets/images/ExceptionLiveTemplateScreen.gif)

