---
title: Using jQuery Grid With ASP.NET MVC
tags: [code,aspnetmvc]
redirect_from: "/archive/2009/04/13/using-jquery-grid-with-asp.net-mvc.aspx/"
---

Tim Davis [posted an updated version of this
solution](http://www.timdavis.com.au/code/jquery-grid-with-asp-net-mvc/)
on his blog. His includes the following:

-   jqGrid 3.8.2
-   .NET 4.0 Updates
-   VS2010
-   jQuery 1.4.4
-   jQuery UI 1.8.7

Continuing in my pseudo-series of posts based on my [ASP.NET MVC Ninjas
on Fire Black Belt Tips
Presentation](http://sessions.visitmix.com/MIX09/T44F "Ninjas on Fire Presentation")
at Mix (go watch it!), this post covers a demo I did *not* show because
I ran out of time. It was a demo I held in my back pocket just in case I
went too fast and needed one more demo.

A common scenario when building web user interfaces is providing a
pageable and sortable grid of data. Even better if it uses AJAX to make
it more responsive and snazzy. Since [ASP.NET
MVC](http://asp.net/mvc/ "ASP.NET MVC") includes jQuery, I figured it’d
be fun to use a jQuery plugin for this demo, so I chose [jQuery
Grid](http://www.trirand.com/blog/ "jQuery Grid Plugin").

After creating a standard ASP.NET MVC project, the first step was to
download the plugin and to unzip the contents to my scripts directory
per the [Installation
instructions](http://www.secondpersonplural.ca/jqgriddocs/_2eb0enwgd.htm "Installation").

![jquery-grid-scripts](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UsingjQueryGridWithASP.NETMVC_B6D6/jquery-grid-scripts_3.png "jquery-grid-scripts")

For the purposes of this demo, I’ll just implement this using the
`Index` controller action and view within the `HomeController`.

With the scripts in place, go to the `Index` view and add the proper
call to initialize the jQuery grid. There are three parts to this:

**First**, make sure to add the required script and CSS declarations.

```csharp
<link rel="stylesheet" type="text/css" href="/scripts/themes/coffee/grid.css" 
  title="coffee" media="screen" />
<script src="/Scripts/jquery-1.3.2.js"></script>
<script src="/Scripts/jquery.jqGrid.js"></script>
<script src="/Scripts/js/jqModal.js"></script>
<script src="/Scripts/js/jqDnR.js"></script>
```

Notice that the first line contains a reference to the “coffee” CSS
file. There are multiple themes included and when you choose a theme,
you need to be sure to include the theme’s CSS file. I chose coffee,
because I drink a lot of it.

The **Second** step is to initialize the grid with a bit of JavaScript.
This looks a bit funky if you’re not used to jQuery, but I assure you,
it’s pretty straightforward.

```html
<script>
    jQuery(document).ready(function(){ 
      jQuery("#list").jqGrid({
        url:'/Home/GridData/',
        datatype: 'json',
        mtype: 'GET',
        colNames:['Id','Votes','Title'],
        colModel :[
          {name:'Id', index:'Id', width:40, align:'left' },
          {name:'Votes', index:'Votes', width:40, align:'left' },
          {name:'Title', index:'Title', width:200, align:'left'}],
        pager: jQuery('#pager'),
        rowNum:10,
        rowList:[5,10,20,50],
        sortname: 'Id',
        sortorder: "desc",
        viewrecords: true,
        imgpath: '/scripts/themes/coffee/images',
        caption: 'My first grid'
      }); 
    }); 
</script>
```

There are a few things you’ll have to be sure to configure here. First
is the *url* property which points to the URL that will provide the JSON
data. Notice that the value is */Home/GridData* which means we’ll be
implementing an action method named *GridData* soon. During the course
of this post, we’ll change that property to point to different action
methods.

The *colNames* property contains the display names for each column
separated by columns. Ideally it should match up with the items in the
*colModel* property.

The *colModel* property is an array that is used to configure each
column of the grid, allowing you to specify the width, alignment, and
sortability of a column. The *index* property of a column is an
important one as that is the value that is sent to the server when
sorting on a column.

See [the
documentation](http://www.secondpersonplural.ca/jqgriddocs/_2eb0ez973.htm "jGrid HTML documentation")
for more details on the HTML and JavaScript used to configure the grid.

The **Third** step is to add a bit of HTML to the page which will house
the grid.

```csharp
<h2>My Grid Data</h2>
<table id="list" class="scroll" cellpadding="0" cellspacing="0"></table>
<div id="pager" class="scroll" style="text-align:center;"></div>
```

With this in place, it’s time to implement the `GridData` action method
to return the JSON in the proper format.

But first, let’s take a look at the JSON format expected by the grid.
[From the
documentation](http://www.secondpersonplural.ca/jqgriddocs/_2eb0f6jhe.htm "JSON Data Documentation"),
you can see it will look something like:

```csharp
{ 
  total: "xxx", 
  page: "yyy", 
  records: "zzz",
  rows : [
    {id:"1", cell:["cell11", "cell12", "cell13"]},
    {id:"2", cell:["cell21", "cell22", "cell23"]},
      ...
  ]
}
```

The documentation I linked to also provides some gnarly looking PHP code
you can use to generate the JSON data. Fortunately, you won’t have to
deal with that. By using the `Json` helper method with an anonymous
object, we can write some relatively clean looking code which looks
*almost just like the spec*. Here’s my first cut of the action method,
just to get it to display some fake data.

```csharp
public ActionResult GridData(string sidx, string sord, int page, int rows) {
  var jsonData = new {
    total = 1, // we'll implement later 
    page = page,
    records = 3, // implement later 
    rows = new[]{
      new {id = 1, cell = new[] {"1", "-7", "Is this a good question?"}},
      new {id = 2, cell = new[] {"2", "15", "Is this a blatant ripoff?"}},
      new {id = 3, cell = new[] {"3", "23", "Why is the sky blue?"}}
    }
  };
  return Json(jsonData, JsonRequestBehavior.AllowGet);
}
```

A couple of things to point out. The arguments to the action methods are
named according to the query string parameter names that jQuery grid
sends via the Ajax request. I didn’t choose those names.

By naming the arguments to the action method exactly the same as what is
in the query string, we have a very convenient way to retrieve these
values. **Remember, arguments passed to an action method should be
treated with care.**[**Never trust user
input**](https://haacked.com/archive/2008/07/08/user-input-in-sheep-clothing.aspx "User Input in Sheep's Clothing")**!**

In this example, we statically create some JSON data and use the `Json`
helper method to return the data back to the grid and Voila! It works!

![jquery-grid-demo](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UsingjQueryGridWithASP.NETMVC_B6D6/jquery-grid-demo_3.png "jquery-grid-demo")

*Yeah, this is great for a simple demo, but I use a real database to
store my data!* Understood. It’s time to hook this up to a real
database. As you might guess, I’ll use the [HaackOverflow
database](https://haacked.com/archive/2008/11/07/haackoverflow-vs-stackoverflow.aspx "HaackOverflow")
for this demo as well as LinqToSql.

I’ll assume you know how to add a database and create a LinqToSql model
already. If not, look at the source code I’ve included. Once you’ve done
that, it’s pretty easy to transformat the data we get back into the
proper JSON format.

```csharp
public ActionResult LinqGridData    (string sidx, string sord, int page, int rows) {
  varnew HaackOverflowDataContext();

  var jsonData = new {
    total = 1, //todo: calculate
    page = page,
    records = context.Questions.Count(),
    rows = (
      from question in context.Questions
      select new {
        id = question.Id,
        cell = new string[] { 
          question.Id.ToString(), question.Votes.ToString(), question.Title 
        }
      }).ToArray()
  };
  return Json(jsonData, JsonRequestBehavior.AllowGet);
}
```

Note that the method is a tiny bit busier, but it follows the same basic
structure as the JSON data. After changing the JavaScript code in the
view to point to this action instead of the other, we can now see the
first ten records from the database in the grid.

But we’re not done yet. At this point, we want to implement paging and
sorting. Paging is pretty easy, but sorting is a bit tricky. After all,
what we get passed into the action method is the name of the sort
column. At that point, we want to dynamically create a LINQ expression
that sorts by that column.

One easy way to do this is to use the [Dynamic Linq Query
library](http://msdn2.microsoft.com/en-us/vcsharp/bb894665.aspx "Dynamic Linq Query Library")
which [ScottGu wrote
about](http://weblogs.asp.net/scottgu/archive/2008/01/07/dynamic-linq-part-1-using-the-linq-dynamic-query-library.aspx "Dynamic Linq Query Library")
a while back. This library adds extension methods which make it easy to
create more dynamic Linq queries based on strings. Of course, with great
power comes great responsibility. **Make sure to validate the strings
before you pass them into the methods**. With this in place, we rewrite
the action method to be (warning, **DEMO CODE AHEAD**!):

```csharp
public ActionResult DynamicGridData
    (string sidx, string sord, int page, int rows) {
  var context = new HaackOverflowDataContext();
  int pageIndex = Convert.ToInt32(page) - 1;
  int pageSize = rows;
  int totalRecords = context.Questions.Count();
  int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

  var questions = context.Questions
    .OrderBy(sidx + " " + sord)
    .Skip(pageIndex * pageSize)
    .Take(pageSize);

  var jsonData = new {
    total = totalPages,
    page = page,
    records = totalRecords,
    rows = (
      from question in questions
      select new {
        id = question.Id,
        cell = new string[] {
          question.Id.ToString(), question.Votes.ToString(), question.Title 
        }
    }).ToArray()
  };
  return Json(jsonData, JsonRequestBehavior.AllowGet);
}
```

Some things to note: The first part of this method does some initial
calculations to figure out the number of pages we’re dealing with based
on the page size (passed in) and the total record count.

Then given that info, we use the Dynamic Linq extension methods to do
the actual paging and sorting via the line:

`   `

var questions = context.Questions.OrderBy(…).Skip(…).Take(…);

Once we have that, we can simply transform that into the array that
jQuery grid expects and place that in the larger JSON payload
represented by the `jsonData` variable.

With all this in place, you now have a pretty snazzy approach to paging
and sorting data using AJAX. Now go forth and wow your customers. ;)

And before I forget, [here’s the sample
project](http://code.haacked.com/mvc-1.0/JQueryGridDemo.zip "JQueryGridDemo sample project")
that uses all three approaches.

