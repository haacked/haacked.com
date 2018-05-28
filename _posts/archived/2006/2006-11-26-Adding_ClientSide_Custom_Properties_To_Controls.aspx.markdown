---
layout: post
title: Adding Client-Side Custom Properties To Controls
date: 2006-11-26 -0800
comments: true
disqus_identifier: 18147
categories:
- asp.net
- code
redirect_from: "/archive/2006/11/25/Adding_ClientSide_Custom_Properties_To_Controls.aspx/"
---

One of the benefits of [writing an
ASP.NET book](https://haacked.com/archive/2006/11/19/Writing_A_Book.aspx "Writing A Book")
is that it forces me to spend a lot of time spelunking deep in the
bowels of ASP.NET uncovering all sorts of little gems I never noticed
the first time around.

Many of these little morsels should end up in the book, but I thought I
would blog about a few of them as I go along. 

This is all part of the weird situation I find myself in while writing
this book. I thought I would just sit down and all the words would flow.
Instead, no matter how motivated I am, everytime I sit down to write I
spend two hours procrastinating for every one hour of writing.  What
gives!?

In any case, one of the gems I discovered is the
[ClientScriptManager.RegisterExpandoAttribute](http://msdn2.microsoft.com/en-US/library/system.web.ui.clientscriptmanager.registerexpandoattribute(VS.80).aspx "MSDN Documentation on ClientScriptManager.RegisterExpandoAttribute")
method.  This method allows you to add custom properties to a
control.  These properties are not rendered in the HTML as attributes,
but simply attached to the control in the DOM via javascript.

This is nice for control authors who want to make a custom control
client scriptable, but still maintain XHTML compliance, since XHTML
compliance doesn’t allow arbitrary attributes for tags.

The following is a really simple example.  I present here a custom
control that inherits from `Label`.

```csharp
public class ExpandoControl : Label
{
    //Code to be filled in.
}
```

The `AddAttributesToRender` method is the appropriate place to call
`RegisterExpandoAttribute`.

```csharp
protected override 
    void AddAttributesToRender(HtmlTextWriter writer)
{
    Page.ClientScript.RegisterExpandoAttribute(this.ClientID
                , "contenteditable", "true");
    base.AddAttributesToRender(writer);
}
```

Now we can access the contenteditable property of this control via
client script. The following javascript demonstrates.

```csharp
var expando = document.getElementById('expando');
alert('Content editable: ' + expando.contenteditable);
```

This is a good approach to take to develop a client-side api for your
custom controls.

