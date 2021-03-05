---
title: Achieving Pixel Perfect Skins in DotNetNuke
tags: [dotnetnuke]
redirect_from: "/archive/2005/11/16/AchievingPixelPerfectSkinsInDotNetNuke.aspx/"
---

One complaint that I’ve had about DotNetNuke (DNN for short) is the
difficulty I've had in creating skins that match client design comps
exactly to the pixel. And believe me, they check every single one.

One key issue is how container skins work. Every module (analagous to a
web part) in DNN is wrapped by a container skin. This is an ascx control
that can be used to apply a border and title around a module should you
so wish.

However, the most important part of a container control is that it
contains placeholders for action controls, controls that present options
to user who has rights to edit the module. Options might include editing
the content of the module, changing its settings, or moving it to
another content pane on the page.

The examples below present a Semantic Link Module I wrote (it uses an
unordered list instead of a table to render the link list) while in
normal content mode and while logged in as an administrator.

![](https://haacked.com/assets/images/BeforeLinkModule.Png) 
 Figure 1: Normal View

![](https://haacked.com/assets/images/ActionOptions.Png) 
 Figure 2: Editable View

The container that wraps this module has to have a placeholder for the
little pencil edit links. The general structure for default container
ascx files that ship with DNN looks like the following.

```html
<table cellpadding ="0" cellspacing ="0" border ="0">
    <tr>
        <td>< dnn:Actions runat="server" id="dnnActions" /></td >
        <td><! -- Maybe another DNN control //-- ></td>
    </tr>
    <tr>
        <td colspan="2" id="ContentPane" runat="server">
            <! -- Your dynamic content is placed here -- >
        </td>
    </tr>
</table>
```

Notice that the Actions control is in its own table row. Ideally when
you are in the normal view, that row would collapse to nothing.
Unfortunately, not all browsers are so kind. IE for example will leave a
one pixel gap. Ideally, you would like to mark that row in such a way
that it isn’t even rendered to the client.

That’s where my new `ContainerOptions` control comes into play. It is a
very simple custom control used to wrap container options such that they
completely removed in the normal view, getting rid of that one pixel
gap. See the following snippet for an example of the usage.

```html
<table>
    <vdn:ContainerOptions runat="server" id="Containeroptions">
        <tr><td><dnn:Actions runat="server" id="actions"/><! -- ... --></td></tr>
    </vdn:ContainerOptions>
    <tr>
        <td id="ContentContainer" runat="server"></td>
    </tr>
</table>
```

This control inherits from the `System.Web.UI.WebControls.PlaceHolder`
control and simply sets its visibility to false unless the module in the
container skin is editable by the current user.

The way it figures this out is it walks up the control hierarchy till it
finds the `DotNetNuke.UI.Containers.Container` control that contains it.
From there it can access the module contained by the container and check
the module’s `IsEditable` property.

Since I pretty much only deal with creating skins using ascx controls, I
did not go to the extra trouble to apply this technique to a skin
object. It is simply a custom control.

The source for this is very small and can be [downloaded here](https://haacked.com/code/ContainerOptions.zip).
