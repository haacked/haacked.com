---
layout: post
title: "TipJar: Title Tags and Master Pages"
date: 2009-04-02 -0800
comments: true
disqus_identifier: 18607
categories: [asp.net,asp.net mvc]
---
There are a couple of peculiarities worth understanding when dealing
with title tags and master pages within Web Forms and [ASP.NET
MVC](http://asp.net/mvc "ASP.NET MVC"). These assume you are using the
`HtmlHead` control, aka `<head runat="server"` /\>.

The first peculiarity involves a common approach where one puts a
`ContentPlaceHolder` inside of a title tag like we do with the default
template in ASP.NET MVC:

```csharp
<%@ Master ... %>
<html>
<head runat="server">
  <title>
    <asp:ContentPlaceHolder ID="titleContent" runat="server" />
  </title>
</head>
...
```

What’s nice about this approach is you can set the title tag from within
any content page.

```csharp
<asp:Content ContentPlaceHolderID="titleContent" runat="server">
  Home
</asp:Content>
```

But what happens if you want to set part of the title from within the
master page. For example, you might want the title of every page to end
with a suffix, “ – MySite”.

If you try this (notice the – MySite tacked on):

```csharp
<%@ Master ... %>
<html>
<head runat="server">
  <title>
    <asp:ContentPlaceHolder ID="titleContent" runat="server" /> - MySite
  </title>
</head>
...
```

And run the page, you’ll find that the *– MySite* is not rendered. This
appears to be a quirk of the `HtmlHead `control. This is because the
title tag within the `HtmlHead` control is now itself a control. This
will be familiar to those who understand how the
[AddParsedSubObject](http://msdn.microsoft.com/en-us/library/system.web.ui.control.addparsedsubobject.aspx "AddParsedSubObject on MSDN")
method works. Effectively, the only content allowed within the body of
the `HtmlHead` control are other controls.

The fix is pretty simple. Add your text to a `LiteralControl` like so.

```csharp
<%@ Master ... %>
<html>
<head runat="server">
  <title>
    <asp:ContentPlaceHolder ID="titleContent" runat="server" /> 
    <asp:LiteralControl runat="server" Text=" - MySite" />
  </title>
</head>
...
```

**The second peculiarity**has to do with how the `HeaderControl` really
wants to produce valid HTML markup.

If you leave the `<head runat="server"></head>` tag empty, and then view
source at the rendered output, you’ll notice that it renders an empty
`<title>` tag for you. It looked at its child controls collection and
saw that it didn’t contain an `HtmlTitle` control so it rendered one for
you.

This can cause problems when attempting to use a `ContentPlaceHolder` to
render the title tag for you. For example, a common layout I’ve seen is
the following.

```csharp
<%@ Master ... %>
<html>
<head runat="server">
  <asp:ContentPlaceHolder ID="headContent" runat="server"> 
    <title>Testing</title>  
  </asp:ContentPlaceHolder>
</head>
...
```

This approach is neat because it allows you to not only set the title
tag from within any content page, but any other content you want within
the `<head>` tag.

However, if you view source on the rendered output, you’ll see two
`<title>` tags, one that you specified and one that’s empty.

Going back to what wrote earlier, the reason becomes apparent. The
`HtmlHead` control checks to see if it contains a child title control.
When it doesn’t find one, it renders an empty one. However, it doesn’t
look within the content placeholders defined within it to see if they’ve
rendered a title tag.

This makes sense when you consider how the `HtmlHead` tag works. It only
allows placing controls inside of it. However, a `ContentPlaceHolder`
allows adding literal text in there. So while it looks the same, the
title tag within the `ContentPlaceHolder` is not an `HtmlTitle` control.
It’s just some text, and the `HtmlHead` control doesn’t want to parse
all the rendered text from its children.

This is why I tend to take the following approach with my own master
pages.

```csharp
<%@ Master ... %>
<html>
<head runat="server">
  <title><asp:ContentPlaceHolder ID="titleContent" runat="server" /></title>
  <asp:ContentPlaceHolder ID="headContent" runat="server"> 
  </asp:ContentPlaceHolder>
</head>
...
```

Happy Titling!

