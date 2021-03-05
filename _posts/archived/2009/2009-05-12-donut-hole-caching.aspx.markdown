---
title: Donut Hole Caching in ASP.NET MVC
tags: [aspnetmvc,caching]
redirect_from: "/archive/2009/05/11/donut-hole-caching.aspx/"
---

A while back, I wrote about [Donut Caching in ASP.NET
MVC](https://haacked.com/archive/2008/11/05/donut-caching-in-asp.net-mvc.aspx "Donut Caching")
for the scenario where you want to cache an entire view except for a
small bit of it. The more technical term for this technique is probably
“cache substitution” as it makes use of the `Response.WriteSubstitution`
method, but I think “Donut Caching” really describes it well — you want
to cache everything but the hole in the middle.

However, what happens when you want to do the inverse. Suppose you want
to cache the donut hole, instead of the donut?

[![House of Sims
Photostream](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/PartialCachinginASP.NETMVC_131B3/2534011147_283339d6c1_3.jpg "House of Sims Photostream")](http://www.flickr.com/photos/houseofsims/2534011147/ "Creative Commons By Attribution")

I think we should nickname all of our software concepts after tasty food
items, don’t you agree?

In other words, suppose you want to cache a portion of the view in a
different manner (for example, with a different duration) than the
entire view? It hasn’t been exactly clear how do to do this with
[ASP.NET MVC](http://asp.net/mvc "ASP.NET Website").

For example, the `Html.RenderPartial` method ignores any `OutputCache`
directives on the view user control. If you happen to use
`Html.RenderAction` from [MVC
Futures](http://aspnet.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=24471 "MVC Futures")
which attempts to render the output from an action inline within another
view, you might [run into this
bug](http://stackoverflow.com/questions/606962/outputcache-and-renderaction-cache-whole-page "RenderAction caches whole page")
in which the entire view is cached if the target action has an
`OutputCacheAttribute` applied.

I did a little digging into this today and it turns out that when you
specify the `OutputCache` directive on a control (or page for that
matter), the output caching is not handled by the control itself.
Rather, it appears that compilation system for ASP.NET pages kicks in
and interprets that directive and does the necessary gymnastics to make
it work.

In plain English, this means that what I’m about to show you will only
work for the default `WebFormViewEngine`, though I have some ideas on
how to get it to work for all view engines. I just need to chat with the
members of the ASP.NET team who really understand the deep grisly guts
of ASP.NET to figure it out exactly.

With the default `WebFormViewEngine`, it’s actually pretty easy to get
partial output cache working. Simply add a `ViewUserControl`
***declaratively*** to a view and put your call to `RenderAction` or
`RenderPartial` ***inside*** of that `ViewUserControl`. If you’re using
`RenderAction`, you’ll need to remove the `OutputCache` attribute from
the action you’re pointing to.

Keep in mind that `ViewUserControl`s inherit the `ViewData` of the view
they’re in. So if you’re using a strongly typed view, just make the
generic type argument for `ViewUserControl` have the same type as the
page.

If that last paragraph didn’t make sense to you, perhaps an example is
in order. Suppose you have the following controller action.

```csharp
public ActionResult Index() {
  var jokes = new[] { 
    new Joke {Title = "Two cannibals are eating a clown"},
    new Joke {Title = "One turns to the other and asks"},
    new Joke {Title = "Does this taste funny to you?"}
  };

  return View(jokes);
}
```

And suppose you want to produce a list of jokes in the view. Normally,
you’d create a strongly typed view and within that view, you’d iterate
over the model and print out the joke titles.

We’ll still create that strongly typed view, but that view will contain
a view user control in place of where we would have had the code to
iterate the model (*note that I omitted the namespaces within the
Inherits attribute value for brevity*).

```csharp
<%@ Page Language="C#" Inherits="ViewPage<IEnumerable<Joke>>" %>
<%@ Register Src="~/Views/Home/Partial.ascx" TagPrefix="mvc" TagName="Partial" 
%>
<mvc:Partial runat="server" />
```

Within *that* control, we do what we would have done in the main view
and we specify the output cache values. Note that the `ViewUserControl`
is generically typed with the same type argument that the view is,
`IEnumerable<Joke>`. This allows us to move the exact code we would have
had in the view to this control. We also specify the `OutputCache`
directive here.

```aspx-cs
<%@ Control Language="C#" Inherits="ViewUserControl<IEnumerable<Joke>>" %>
<%@ OutputCache Duration="10000" VaryByParam="none" %>

<ul>
<% foreach(var joke in Model) { %>
    <li><%= Html.Encode(joke.Title) %></li>
<% } %>
</ul>
```

Now, this portion of the view will be cached, while the rest of your
view will continue to not be cached. Within this view user control, you
could have calls to `RenderPartial` and `RenderAction` to your heart’s
content.

Note that if you are trying to cache the result of `RenderPartial` this
technique doesn’t buy you much unless the cost to render that partial is
expensive.

Since the output caching doesn’t happen until the view rendering phase,
if the view data intended for the partial view is costly to put
together, then you haven’t really saved much because the action method
which provides the data to the partial view will run on every request
and thus recreate the partial view data each time.

In that case, you want to hand cache the data for the partial view so
you don’t have to recreate it each time. One crazy idea we might
consider (thinking out loud here) is to allow associating output cache
metadata to some bit of view data. That way, you could create a bit of
view data specifically for a partial view and the partial view would
automatically output cache itself based on that view data.

This would have to work in tandem with some means to specify that the
bit of view data intended for the partial view is only recreated when
the output cache is expired for that partial view, so we don’t incur the
cost of creating it on every request.

In the `RenderAction` case, you really do get all the benefits of output
caching because the action method you are rendering inline won’t get
called from the view if the `ViewUserControl` is outputcached.

I’ve **[put together a small
demo](http://code.haacked.com/mvc-2/DonutHoleCaching.zip "Partial Cache Demo")**
which demonstrates this concept in case the instructions here are not
clear enough. Enjoy!

