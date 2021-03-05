---
title: Scripting ASP.NET MVC Views Stored In The Database
tags: [code,aspnetmvc,languages]
redirect_from: "/archive/2009/04/21/scripted-db-views.aspx/"
---

Say you’re building a web application and you want, against your better
judgment perhaps, to allow end users to easily customize the look and
feel – a common scenario within a blog engine or any hosted application.

With ASP.NET, view code tends to be some complex declarative markup
stuck in a file on disk which gets compiled by ASP.NET into an assembly.
Most system administrators would first pluck out their own toenail
rather than allow an end user permission to modify such files.

It’s possible to store such files in the database and use a
`VirtualPathProvider` to load them, but that requires your application
(and thus their views) to run in full trust. Is there a way you could
safely store such views in the database in an application running in
medium trust where the code in the view is approachable?

[![](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/HostingASP.NETMVCViewsInTheDatabase_134EA/fun-scripting_thumb.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/HostingASP.NETMVCViewsInTheDatabase_134EA/fun-scripting_2.jpg)
At the ALT.NET conference a little while back, [Jimmy
Schementi](http://blog.jimmy.schementi.com/ "Jimmy Schementi") and [John
Lam](http://www.iunknown.com/ "John Lam") [gave a
talk](https://haacked.com/archive/2009/03/01/altnetseattle-day-three.aspx "ALT.NET Seattle Day 3")
about the pattern of hosting a scripting language within a larger
application. For example, many modern 3-D Games have their high
performance core engine written in C++ and Assembly. However, these
games often use a scripting language, such as
[Lua](http://www.lua.org/ "Lua programming language"), to write the
scripts for the behaviors of characters and objects.

An example that might be more familiar to more people is the use of VBA
to write macros for Excel. In both of these cases, the larger
application hosts a scripting environment that allow end users to
customize the application using a simpler lighter weight language than
the one the core app is written in.

A long while back, I wrote a blog post about [defining ASP.NET MVC Views
in
IronRuby](https://haacked.com/archive/2008/04/22/defining-asp.net-mvc-routes-and-views-in-ironruby.aspx "Defining Views in IronRuby")
followed by a full [IronRuby ASP.NET MVC
stack](https://haacked.com/archive/2009/02/17/aspnetmvc-ironruby-with-filters.aspx "IronRuby ASP.NET MVC").
While there was some passionate interest by a few, in general, I was met
with the thunderous sound of crickets. Why the huge lack of interest?
Probably because I didn’t really sell the benefit and the explain the
pain it solves. I’m sure many of you were asking, *Why bother? What’s in
it for me?*

After thinking about it some more, I realized that my prototypes
appeared to suggest that if you want to take advantage of IronRuby, you
would need to make some sort of wholesale switch to a new foreign
language, not something to be undertaken lightly.

This is why I really like Jimmy and John’s recent efforts to focus on
showing the benefits of hosting the DLR for scripting scenarios like the
ones mentioned above. It makes total sense to me when I look at it in
this perspective. The way I see it, most developers spend a huge bulk of
their time in a single core language, typically their “language of
choice”. For me, I spend the bulk of my time writing C# code.

However, I don’t think twice about the fact that I also write tons of
JavaScript when I do web development, and I’ll write the occasional VB
code when I need a new Macro for Visual Studio or Excel. I also write
SQL when I need to. I’m happy to pick up and use a new language when it
will enable me to do the job at hand more efficiently and naturally than
C# does. I imagine many developers feel this way. The occasional use of
a scripting languages is fine when it gets the job done and I can still
spend most of my time in my favorite language.

So I started thinking about how that might work in a web application.
What if you could write all your business logic and controller logic in
your language of choice, but have your views written in a light weight
scripting language. If my web application were to host a scripting
engine, I could actually store the code in any medium I want, such as
the database. Having them in the database makes it very easy for end
users to modify it since it wouldn’t require file upload permissions
into the web root.

This is where hosting the DLR is a nice fit. I put together a proof of
concept for these ideas. This is just a prototype intended to show how
such a workflow might work. In this prototype, you go about creating
your models and controllers the way you normally would.

For example, here’s a controller that returns some structured data to
the view in the form of an anonymous type.

```csharp
public ActionResult FunWithScripting()
{
  var someData = new { 
    salutation = "Are you having fun with scripting yet?", 
    theDate = DateTime.Now,
      numbers = new int[] { 1, 2, 3, 4 } 
  };

  return View(someData);
}
```

Once you write your controller, but before you create your view, you
compile the app and then go visit the URL.![View does not exist
view](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/HostingASP.NETMVCViewsInTheDatabase_134EA/view-does-not-exist_11.png "View does not exist view")

We haven’t created the view yet, so let’s follow the instructions and
login. Afterwards, we this:

![view
editor](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/HostingASP.NETMVCViewsInTheDatabase_134EA/view-editor_3.png "view editor")

Since the view doesn’t exist, I hooked in and provided a temporary view
for the controller action which contains a view editor. Notice that at
the bottom of the screen, you can see the current property names and
values being passed to the view. For example, there’s an enumeration of
integers as one property, so I was able to use the Ruby `each` method to
print them out in the view.

The sweet little browser based source code editor is named [Edit Area
created by Christophe
Dolivet](http://www.cdolivet.com/index.php?page=editArea&sess=d7189c4b90423ed1b1aff26ec520caba "Edit Area").
Unfortunately, at the time I write this, it doesn’t yet have support for
ERB style syntax highlighting schemes. That’s why the \<% and %\> aren’t
highlighted in yellow.

When I click *Create View*, I get taken back to the request for the same
action, but now I can see the view I just created (click to enlarge).

[![Fun with scripting
view](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/HostingASP.NETMVCViewsInTheDatabase_134EA/fun-with-scripting-view_thumb.png "Fun with scripting view")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/HostingASP.NETMVCViewsInTheDatabase_134EA/fun-with-scripting-view_2.png)

In the future, I should be able to host C# views in this way. Mono
already has a tool for dynamically compiling C# code passed in as a
string which I could try and incorporate.

I’m seriously thinking of making this the approach for building skins in
a future version of Subtext. That would make skin installation drop dead
simple and not require any file directory access. Let me know if you
make use of this technique in your applications.

If you try and run this prototype, please note that there are some
quirky caching issues with editing existing views in the prototype.
It’ll seem like your view is not being edited, but it’s a result of how
views are being cached. It might take a bit of time before your edits
show up. I’m sure there are other bugs I’m still in the process of
fixing. But for the most part, the general principle is sound.

You can **[download the prototype
here](http://code.haacked.com/mvc-1.0/IronRubyViews.zip "IronRuby Views in Db")**.

