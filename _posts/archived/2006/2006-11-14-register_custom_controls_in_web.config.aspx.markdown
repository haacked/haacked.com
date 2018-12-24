---
title: Register Custom Controls In Web.config
date: 2006-11-14 -0800
tags: [aspnet]
redirect_from: "/archive/2006/11/13/register_custom_controls_in_web.config.aspx/"
---

This one is probably old news to many of you, but I just recently ran
across it. Every time I want to add a new control to a new page, I get
annoyed because I have to remember that annoying syntax for registering
a control.

Let’s see...how does it go again? Do I have to add a `TagName`
attribute? No, that’s for user controls. Hmmm, forget it, I’ll just
dynamically add it! Well in the interest of reducing future angst, here
are two examples of the syntax, one for a custom control and one for a
user control.

```csharp
<%@ Register TagPrefix="st" Namespace="Subtext.Web.Controls" 
  Assembly="Subtext.Web.Controls" %>
<%@ Register TagName="SomeControl" TagPrefix="st" 
  Src="~/Controls/SomeControl.ascx" %>
```

The first one registers the tag prefix **st** with the
`Subtext.Web.Controls` namespace in the `Subtext.Web.Controls` assembly.
The second one registers the tag name `SomeControl` with the user
control `SomeControl.ascx`

Add this to the top of your page or user control and you can reference a
control from this assembly like so:

```csharp
<st:HelpToolTip id="blah" runat="server" HelpText="Blah!" />
<st:SomeControl id="foo" runat="server" />
```

A most helpful tooltip!

Fortunately, starting with ASP.NET 2.0, we can register a tag prefix
within the `Web.config` file. This basically makes all the controls
within that namespace and assembly available to all pages without having
to add that ugly `Register` declaration.

```csharp
<system.web>
    <pages>
      <controls>
        <add assembly="Subtext.Web.Controls"
                namespace="Subtext.Web.Controls"
                tagPrefix="st" />
        <add src="~/Controls/SomeControl.ascx"
                tagName="SomeControl"
                tagPrefix="st" />
      </controls>
    </pages>
</system.web>
```

Thanks to the [ASP.NET 2.0 MVP
Hacks](http://www.amazon.com/ASP-NET-2-0-Hacks-David-Yack/dp/0764597663 "ASP.NET Hacks at Amazon")
book for this one.

