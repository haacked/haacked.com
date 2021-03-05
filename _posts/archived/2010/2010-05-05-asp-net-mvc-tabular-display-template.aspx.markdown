---
title: ASP.NET MVC Tabular Display Template
tags: [aspnetmvc,aspnet,code]
redirect_from: "/archive/2010/05/04/asp-net-mvc-tabular-display-template.aspx/"
---

The ASP.NET MVC2 templates feature is a pretty nice way to quickly
scaffold objects at runtime. Be sure to read [Brad
Wilson](http://bradwilson.typepad.com/blog/ "Brad Wilson's blog")’s
fantastic series on this topic starting at [ASP.NET MVC 2 Templates,
Part 1:
Introduction](http://bradwilson.typepad.com/blog/2009/10/aspnet-mvc-2-templates-part-1-introduction.html "ASP.NET MVC 2 Templates").

As great as this feature is, there is one template that’s conspicuously
missing. ASP.NET MVC does not include a template for displaying a list
of objects in a tabular format. Earlier today,
[ScottGu](http://weblogs.asp.net/scottgu/ "ScottGu's Blog") forwarded an
email from Daniel Manes (what?! no blog! ;) with a question on how to
accomplish this. Daniel had much of it implemented, but was trying to
get over the last hurdle.

With Brad’s help, I was able to give him a boost over that hurdle. Let’s
walk through the scenario.

First, we need a model.

![zoolander](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TabularDisplayTemplate_12625/zoolander_3.jpg "zoolander")

No, not that kind of model. Something more along the lines of a C#
variety.

```csharp
public class Book
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    [DisplayName("Date Published")]
    public DateTime PublishDate { get; set; }
}
```

Great, now lets add a controller action to the default `HomeController`
which will create a few books and pass them to a view.

```csharp
public ActionResult Index()
{
    var books = new List<Book>
    {
        new Book { 
            Id = 1, 
            Title = "1984", 
            Author = "George Orwell", 
            PublishDate = DateTime.Now 
        },
        new Book { 
            Id = 2, 
            Title = "Fellowship of the Ring", 
            Author = "J.R.R. Tolkien", 
            PublishDate = DateTime.Now 
        },
        //...
    };
    return View(books);
}
```

Now we’ll create a strongly typed view we’ll use to display a list of
such books.

```aspx-cs
<% @Page MasterPageFile="~/Views/Shared/Site.Master"
  Language="C#"
  Inherits="ViewPage<IEnumerable<TableTemplateWeb.Models.Book>>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>All Books</h2>
    <p>
        <%: Html.DisplayForModel("Table") %>
    </p>
</asp:Content>
```

If you run the code right now, you won’t get a very useful display.
Also, notice that we pass in the string “Table” to the `DisplayForModel`
method. That’s a hint to the template method which tells it, “Hey! If
you see a template named ‘Table’, tell him he owes me money! **Oh, and
use it to render the model**. Otherwise, if he’s not around fallback to
your normal behavior.”

Since we don’t have a *Table* template yet, this code is effectively the
same as if we didn’t pass anything to `DisplayForModel`.

What we need to do now is create the *Table* template. To do so, create
a *DisplayTemplates* folder within the *Views/Shared* directory. Then
right click on that folder and select *Add | View.*

This brings up the Add View dialog. Enter *Table* as the view name and
make sure check *Create a partial view*. Also, check *Create a
strongly-typed view* and type in `IList` as the *View Data Class.*

![Add View
Dialog](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TabularDisplayTemplate_12625/Add%20View_3.png "Add View Dialog")

When you click *Add*, you should see the new template in the
*DisplayTemplates* folder like so.

![solution
explorer](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TabularDisplayTemplate_12625/solution-explorer_5.png "solution explorer")

Here’s the code for the template. Note that there’s some code in here
that I could refactor into a helper class in order to clean up the
template a bit, but I wanted to show the full template code here in one
shot.

```aspx-cs
<% @Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList>" %>
<script runat="server">
  public static bool ShouldShow(ModelMetadata metadata,       ViewDataDictionary viewData) {
    return metadata.ShowForDisplay
      && metadata.ModelType != typeof(System.Data.EntityState)
      && !metadata.IsComplexType
      && !viewData.TemplateInfo.Visited(metadata);
  }
</script>
<%
  var properties = ModelMetadata.FromLambdaExpression(m => m[0], ViewData)
    .Properties
    .Where(pm => ShouldShow(pm, ViewData));
%>
<table>
  <tr>
    <% foreach(var property in properties) { %>        
    <th>
      <%= property.GetDisplayName() %>
    </th>
    <% } %>
  </tr>
    <% for(int i = 0; i < Model.Count; i++) {
    var itemMD = ModelMetadata.FromLambdaExpression(m => m[i], ViewData); %>
    <tr>
      <% foreach(var property in properties) { %>
      <td>
        <% var propertyMetadata = itemMD.Properties
              .Single(m => m.PropertyName == property.PropertyName); %>  
          <%= Html.DisplayFor(m => propertyMetadata.Model) %>
        </td>
      <% } %>
    </tr>
    <% } %>
</table>
```

### Explanation {.clear}

There’s a lot going on in here, but I’ll try to walk through it bit by
bit. If you’d rather skip this part and just take the code and run, I
won’t hold it against you.

In the first section, we define a `ShouldShow` method which is pulled
right out of the logic for our default *Object* template. You’ll notice
there’s mention of `System.Data.EntityState` (defined in the
*System.Data.Entity.dll*) which is used to filter out certain Entity
Framework properties. **If you aren’t using Entity Framework you can
safely delete that line.**You’ll know you don’t need that line if you
aren’t referencing *System.Data.Entity.dll* which will cause this code
to blow up like aluminum foil in a microwave.

In the next code block, we grab all the property `ModelMetadata` for the
first item in the list. Remember, the current model in this template is
a list, but we need the metadata for an item in this list, not the list
itself. That’s why we have this odd bit of code here. Once we grab this
metadata, we can iterate over it and display the column headers.

In the final block of code, we iterate over every item in the list and
use this handy dandy `FromLambdaExpression` method to grab the
`ModelMetadata` for an individual item.

Then we grab the property metadata for that item and iterate over that
so that we can display each property in its own column. Notice that we
call `DisplayFor` on each property rather than simply spitting out
`propertyMetadata.Model`.

### Usage

Now that you’ve created this *Table.ascx* template and placed it in the
*Shared/DisplayTemplates* folder, it is available any time you’re using
a display template to render a list. Simply supply a hint to use the
table template. For example:

`<%: Html.DisplayForModel("Table") %>`

or

`<%: Html.DisplayFor(m => m.SomeList, "Table") %>`

### Download the sample

As I typically do, I’ve written up a sample project you can try out in
case you run into problems getting this to work. Note this sample was
built for Visual Studio 2010 targetting ASP.NET 4. If you are running
ASP.NET MVC 2 on Visual Studio 2008 SP1, just copy the *Table.ascx* into
your own project but replace the [Html encoding code
nuggets](https://haacked.com/archive/2009/09/25/html-encoding-code-nuggets.aspx "Html Encoding Code Blocks with ASP.NET 4") `<%: … %>`
to `<%= Html.Encode(…) %>`.

Here’s the [**link to the
sample**](http://code.haacked.com/mvc-2/TableTemplateDemo.zip "Download TableTemplateDemo").

