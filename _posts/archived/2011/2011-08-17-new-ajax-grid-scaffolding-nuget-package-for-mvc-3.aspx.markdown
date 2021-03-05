---
title: New Ajax Grid Scaffolding NuGet Package for MVC 3
tags: [aspnetmvc,aspnet,code]
redirect_from: "/archive/2011/08/16/new-ajax-grid-scaffolding-nuget-package-for-mvc-3.aspx/"
---

***EDITOR’S NOTE:**Microsoft has an [amazing intern
program](http://careers.microsoft.com/careers/en/us/collegeinternships.aspx).
For a summer, these bright college students work with a feature crew
getting real work done, all the while attending cool events nearly every
week that, frankly, make the rest of us jealous! Just look at [some of
the
perks](http://seattletimes.nwsource.com/html/microsoft/2009630759_microsoftinterns10.html "MS Interns")
listed in this news article!*

*This summer, the ASP.NET MVC is hosting an intern, Stephen Halter, who
while very smart, doesn’t have a blog of his own (booo! hiss!). Being
the nice guy that I am (and also being amenable to bribes), I’m letting
him guest author a post on my blog (first guest post ever!) to talk
about the cool project he’s been doing.*

***Editor Update:****A lot of folks are wondering why we built our own
grid. The plan is to build this on top of the [jQuery UI
Grid](http://blog.jqueryui.com/2011/02/unleash-the-grid/) when it’s
complete. It’s not done yet so we built a scaled down implementation to
allow us to test out the interactions with ASP.NET MVC controllers and
make sure everything is in place. It’s mostly T4 files that extends our
existing Add Controller Scaffolding feature. You can tweak the T4 to
build any kind of grid you want.*

Hello everyone – my name is Stephen Halter, and I am a summer intern on
the ASP.NET MVC team who is excited to show you the new Ajax grid
scaffolder that I have been working on for ASP.NET MVC 4. Phil has
graciously allowed me to use his blog to get the word out about my
project.

We want to get some feedback before releasing the scaffolder as part of
the MVC Framework, and we figured what better way to get feedback than
to release a preview of the scaffolder as a NuGet package?

To install the package, search for “AjaxGridScaffolder” in the NuGet
Package Manager dialog or just enter the following command

`PM> Install-Package AjaxGridScaffolder`

in the Package Manager Console to try it out.

The NuGet package, once installed, registers itself to the Add
Controller dialog as a ScaffolderProvider named “Ajax Grid Controller.”
When run, the scaffolder generates a controller and corresponding views
for an Ajax grid representation of the selected model and data context
class.

The Ajax grid controller scaffolder has many similarities to the current
“Controller with read/write actions and views, using Entity Framework”,
but it provides pagination, reordering and inline editing of the content
using Ajax.

![Add \> Controller menu
selection](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/New-Ajax-Grid-Scaffolding-NuGet-Package-_126BB/clip_image001_3.png "Add > Controller menu selection")

![Template Selection - Ajax Grid
Controller](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/New-Ajax-Grid-Scaffolding-NuGet-Package-_126BB/clip_image002_3.png "Template Selection - Ajax Grid Controller")

Currently, the Ajax Grid Scaffolder only generates C# code, but both
Razor and ASPX views are supported. We’ll add VB.NET support later. If
you selected “None” from the Views drop down in the Add Controller
dialog, only the controller will be generated.

The generated controller has 8 actions. There is `Index`, `Create`
(POST/GET), `Edit` (POST/GET), `Delete` (POST), `GridData`, and
`RowData`. Most of these actions probably sound familiar, but there are
a few that might not be obvious. The GET versions of `Create` and `Edit`
provide editing widgets to the client using `HtmlHelper.EditorFor` or
`HtmlHelper.DropDownList` if the property is a foreign key. The
`GridData` action renders a partial view of a range of rows while
`RowData` renders a partial view a specific row given its primary key.
These \*Data actions aren’t very *verby* (*editor’s note: “verby?”
Seriously?*) and improved naming suggestions are welcome.

![Solution explorer view of the project
structure](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/New-Ajax-Grid-Scaffolding-NuGet-Package-_126BB/clip_image003_3.png "Solution explorer view of the project structure")

The scaffolder also provides three views: `Index`, `GridData`, and
`Edit`. The `Index` view includes JavaScript and small amount of CSS
required to make the Ajax grid usable. Due to all the possible ways a
master or layout page can be done, it isn’t possible to ensure that
JavaScript and CSS will be included in the `<head>` element of the
document. However, we are looking at possible fixes for the MVC 4
release.

[![clip\_image005](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/New-Ajax-Grid-Scaffolding-NuGet-Package-_126BB/clip_image005_thumb.jpg "clip_image005")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/New-Ajax-Grid-Scaffolding-NuGet-Package-_126BB/clip_image005_2.jpg)

The `GridData` and `Edit` views are partial views that are rendered when
retrieving non-editable and editable rows respectively via
`XMLHttpRequest` instances (XHRs) using jQuery. Sending HTML snippets in
response to XHRs (as opposed to JSON for example) makes using
`DisplayFor`, `EditorFor` and `ValidationFor` more natural and
progressive enhancement simpler.

With or without JavaScript, the `Index` view provides pagination and
reordering by column. With JavaScript enabled, rows can be
created/edited/deleted all inside the grid without ever leaving the
index view.

Tell us what your thoughts are on the Ajax Grid Scaffolder. Is it
important to you that we support editing without JavaScript? Do you want
the back button to take you to your previous view of the grid? Do you
have any suggestions on how to clean up the generated code? Did you
encounter any bugs/issues? If so, we want to know.

