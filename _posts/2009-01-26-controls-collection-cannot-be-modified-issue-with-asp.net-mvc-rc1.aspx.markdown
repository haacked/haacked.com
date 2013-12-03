---
layout: post
title: "Controls Collection Cannot Be Modified Issue with ASP.NET MVC RC1"
date: 2009-01-26 -0800
comments: true
disqus_identifier: 18580
categories: [asp.net mvc,asp.net]
---
In my last post, I announced the happy news that the [Release Candidate
for ASP.NET MVC is
available](http://haacked.com/archive/2009/01/27/aspnetmvc-release-candidate.aspx "ASP.NET MVC Release Candidate").
In this post, I say mea culpa for a known bug within this release.

This bug is a consequence of a change we made in our default template.
We know have a content placeholder in the `<head>` section of the
Site.master page.

```csharp
<head runat="server">
    <asp:ContentPlaceHolder ID="head" runat="server">
        <title></title>
    </asp:ContentPlaceHolder>
    <link href="../../Content/Site.css" rel="stylesheet" type="text/css" />
</head>
```

The benefit here is that it makes it easy to specify view specific
scripts, style sheets, and title tags from the specific view, like so.

```csharp
<asp:Content ID="indexHead" ContentPlaceHolderID="head" runat="server">
    <title>Home Page</title>
    <style type="text/css">
        /* Some style specific to this view */
    </style>
</asp:Content>
```

Long story short, if the Header control (aka `<head runat="server" />`)
doesn’t see a title tag as a direct child of itself, it renders an empty
title tag in order to ensure the page has valid HTML. This results in
having two title tags, the one you intended and an empty one.

We put in a fix for this whereby we modify the controls collection of
the header control, but the fix itself causes a problem when you have
code nuggets within the `<head>` section. For example, the following
causes an exception.

```aspx-cs
<head runat="server">
    <script src="<%= Url.Content("~/foo.js") %>" type="text/javascript">
    </script>
    <asp:ContentPlaceHolder ID="head" runat="server">
        <title></title>
    </asp:ContentPlaceHolder>
    <link href="../../Content/Site.css" rel="stylesheet" type="text/css" />
</head>
```

The exception this causes is:

> The Controls collection cannot be modified because the control
> contains code blocks (i.e. \<% ... %\>).

We unfortunately didn’t find this until very recently. The current
workaround is simple. Place the script tag within a normal `PlaceHolder`
control.

```aspx-cs
<head runat="server">
    <asp:PlaceHolder runat="server" id="mainScripts">
        <script src="<%= Url.Content("~/foo.js") %>" type="text/javascript">
        </script>
    </asp:PlaceHolder>
    <asp:ContentPlaceHolder ID="head" runat="server">
        <title></title>
    </asp:ContentPlaceHolder>
    <link href="../../Content/Site.css" rel="stylesheet" type="text/css" />
</head>
```

We have one simple solution to this we are bouncing around, and are
investigating alternative solutions. We apologize for any inconveniences
this may cause.

