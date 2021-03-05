---
title: Default Templated Views
tags: [aspnetmvc]
redirect_from: "/archive/2009/08/03/default-templated-views.aspx/"
---

*Note, this blog post is based on Preview 1 of ASP.NET MVC 2 and details
are subject to change. I’ll try to get back to normal ASP.NET MVC 1.0
content soon. :)*

While in a meeting yesterday with “[The
Gu](http://weblogs.asp.net/scottgu/ "Scott Guthrie's Blog")”, the topic
of automatic views came up. Imagine if you could simply instantiate a
model object within a controller action, return it to the “view”, and
have ASP.NET MVC provide simple scaffolded edit and details views for
the model automatically.

That’s when the light bulb went on for Scott and he briefly mentioned an
idea for an approach that would work. I was excited by this idea and
decided to prototype it tonight. Before I discuss that approach, let me
lead in with a bit of background.

One of the cool features of ASP.NET MVC is that any views in our
*\~/Views/Shared folder*are shared among all controllers. For example,
suppose you wanted a default *Index* view for all controllers. You could
simply add a view named *Index* into the *Shared* views folder.

![shared-index-view](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DefaultViewsforASP.NETMVC2_13B12/shared-index-view_3.png "shared-index-view")

Thus any controller with an action named *Index*would automatically use
the Index in the Shared folder unless there was also an Index view in
the controller’s view folder.

Perhaps, we can use this to our advantage when building simple CRUD
(Create, Read, Update, Delete) pages. What if we included default views
within the *Shared* folder named after the basic CRUD operations? What
would we place in these views? Well calls to our new [Templated
Helpers](http://msdn.microsoft.com/en-us/library/ee308450(VS.100).aspx "Templated Helpers on MSDN")
of course! That way, when you add a new action method which follows the
convention, you’d automatically have a scaffolded view without having to
create the view!

I prototyped this up tonight as a demonstration. The first thing I did
was add three new views to the Shared folder, *Details*, *Edit*, and
*Create*.

[![crud-views](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DefaultViewsforASP.NETMVC2_13B12/crud-views_thumb.png "crud-views")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DefaultViewsforASP.NETMVC2_13B12/crud-views_2.png)
Let’s take a look at the *Details* view to see how simple it is.

```aspx-cs
<%@ Page Inherits="System.Web.Mvc.ViewPage"%>
<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    Details for <%= Html.Encode(ViewData.Eval("Title")) %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <fieldset class="default-view">
        <legend><%= Html.Encode(ViewData.Eval("Title")) %></legend>
    
        <% ViewData["__MyModel"] = Model; %>
        <%= Html.Display("__MyModel") %>
    </fieldset>
</asp:Content>
```

What we see here is a non-generic `ViewPage`. Since this View can be
used for multiple controller views and we won’t know what the model type
is until runtime, we can’t use a strongly typed view here, but we can
use the non-generic `Html.Display` method to display the model.

One thing you’ll notice is that this required a hack where I take the
model and add it to `ViewData` using an arbirtrary key, and then I call
`Html.Display` using the same view data key. This is due to an apparent
bug in Preview 1 in which `Html.Display("")` doesn’t work against the
current model. I’m confident we’ll fix this in a future preview.

`Html.DisplayFor(m => m)` also doesn’t work here because the expression
works against the declared type of the Model, not the runtime type,
which in this case, is object.

With these views in place, I now have the basic default CRUD (*well
Create, Edit, Details to be exact*) views in place. So the next time I
create an action method named the same as these templates, I won’t have
to create a view.

Let’s see this in action. I love
[NerdDinner](http://nerddinner.codeplex.com/ "NerdDinner on CodePlex"),
but I’d like to use another domain for this sample for a chain. Let’s
try Ninjas!

First, we create a simple `Ninja` class.

```csharp
public class Ninja
{
    public string Name { get; set; }
    public int ShurikenCount { get; set; }
    public int BlowgunDartCount { get; set; }
    public string  Clan { get; set; }
}
```

Next we’ll add a new `NinjaController` using the *Add Controller* dialog
by right clicking on the *Controllers* folder, selecting *Add*, and
choosing *Controller*.

![add-controller](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DefaultViewsforASP.NETMVC2_13B12/add-controller_5.png "add-controller")

This brings up a dialog which allows you to name the controller and
choose to scaffold some simple action methods ([completely configurable
of course using T4
templates](https://haacked.com/archive/2009/01/31/t4-templates-in-asp.net-mvc.aspx "T4 Templates in ASP.NET MVC")).

![Add
Controller](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DefaultViewsforASP.NETMVC2_13B12/Add%20Controller_3.png "Add Controller")

Within the newly added Ninja controller, I create sample `Ninja` (as a
static variable for demonstration purposes) and return it from the
`Details` action.

```csharp
static Ninja _ninja = new Ninja { 
    Name = "Ask a Ninja", 
    Clan = "Yokoyama", 
    BlowgunDartCount = 23, 
    ShurikenCount = 42 };

public ActionResult Details(int id)
{
  ViewData["Title"] = "A Very Cool Ninja";
  return View(_ninja);
}
```

Note that I also place a title in `ViewData` since I know the view will
display that title. I could also have created a `NinjaViewModel` and
passed that to the view instead complete with `Title` property, but I
chose to do it this way for demo purposes.

Now, when I visit the Ninja details page, I see:

![Details for One awesome Ninja - Windows Internet Explorer
(2)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DefaultViewsforASP.NETMVC2_13B12/Details%20for%20One%20awesome%20Ninja%20-%20Windows%20Internet%20Explorer%20(2)_3.png "Details for One awesome Ninja - Windows Internet Explorer (2)")

With these default templates in place, I can quickly create other action
methods without having to worry about the view yet. I’ll just get a
default scaffolded view.

If I need to make minor customizations to the scaffolded view, I can
always apply data annotation attributes to provide hints to the
templated helper on how to display the model. For example, let’s add
some spaces to the fields via the `DisplayNameAttribute`.

```csharp
public class Ninja
{
    public string Name { get; set; }
    [DisplayName("Shurikens")]
    public int ShurikenCount { get; set; }
    [DisplayName("Blowgun Darts")]
    public int BlowgunDartCount { get; set; }
    public string  Clan { get; set; }
}
```

If it concerns you that I’m adding these presentation concerns to the
model, let’s pretend this is actually a view specific model for the
moment and set those concerns aside. Also, in the future we hope to
provide means to provide this meta-data via other means so it’s doesn’t
have to be applied directly to the model but can be stored elsewhere.

Now when I recompile and refresh the page, I see my updated labels.

![updated-ninja-details](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DefaultViewsforASP.NETMVC2_13B12/updated-ninja-details_6.png "updated-ninja-details")

Alternatively, I can create a display template for Ninjas. All I need to
do is add a folder named *DisplayTemplates* to the *Shared* views folder
and add my *Ninja* template there.

Then I right click on that folder and select the *Add View* dialog,
making sure to check *Create a strongly-typed view*. In this case, since
I know I’m making a template specifically for Ninjas, I can create a
strongly typed partial view and select `Ninja` as model type.

![Add-Partial-View](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DefaultViewsforASP.NETMVC2_13B12/Add-Partial-View_3.png "Add-Partial-View")

When I’m done, I should see the following template in the
`DisplayTemplates` folder. I can go in there and make any edits I like
now to provide much more detailed customization.

![DisplayTemplates](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DefaultViewsforASP.NETMVC2_13B12/DisplayTemplates_3.png "DisplayTemplates")

Now I just recompile and then refresh my details page and see:

![scaffolded-details](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DefaultViewsforASP.NETMVC2_13B12/scaffolded-details_6.png "scaffolded-details")

Finally, if I need even more control, I can simply add a *Details* view
to the *Ninja* views folder, which provides absolute control and
overrides the default *Details* view in the Shared *folder*.

![Ninja-Details-View](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/DefaultViewsforASP.NETMVC2_13B12/Ninja-Details-View_3.png "Ninja-Details-View")

So that’s the neat idea which I’m calling “default templated views” for
now. This walkthrough not only shows you the idea, but how to implement
it yourself! You can easily take this idea and have it fit your own
conventions.

At the time that he mentioned this idea, Scott exclaimed “*Why didn’t I
think of this before, it’s so obvious*.” (or something to that effect, I
wasn’t taking notes).

I was thinking the same thing until I just realized, we didn’t *have*
Templated Helpers before, so having default CRUD views would not have
been all that useful in ASP.NET MVC 1.0. ;)

But ASP.NET MVC 2 Preview 1 *does* have Templated Helpers and this post
provides a neat means to provide scaffolded views while you build your
application.

And before I forget, here’s a [**download containing my sample Ninja
project**](https://haacked.com/code/DefaultViewsDemo.zip "Default Views Demo").

