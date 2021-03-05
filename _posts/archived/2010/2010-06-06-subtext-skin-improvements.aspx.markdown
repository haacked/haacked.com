---
title: Subtext 2.5 Skin Improvements
tags: [subtext]
redirect_from: "/archive/2010/06/05/subtext-skin-improvements.aspx/"
---

Deploying a Subtext skin used to be one of the biggest annoyances with
Subtext prior to version 2.5. The main problem was that you couldn’t
simply copy a skin folder into the Skins directory and just have it work
because the configuration for a given skin is centrally located in the
*Skins.config* file.

[![elephant-skin](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/UpcomingSubtextSkinImprovements_C779/elephant-skin_3.jpg "elephant-skin")](http://www.sxc.hu/photo/1229077 "Skin on sxc.hu by Nick Bradsworth")In
other words, a skin wasn’t self contained in a single folder. With
Subtext 2.5, this has changed. Skins are fully self contained and there
is no longer a need for a central configuration file for skins.

**What this means for you is that it is now way easier to share skins.**
When you get a skin folder, you just drop it into the */skins* directory
and you’re done!

In most cases, there’s no need for any configuration file whatsoever. If
your skin contains a CSS stylesheet named *style.css*, that stylesheet
is automatically picked up. Also, with Subtext 2.5, you can provide a
thumbnail for your skin by adding a file named *SkinIcon.png* into your
skin folder. That’ll show up in the improved Skin picker.

### When To Use A Skin.config File

Each skin can have its own manifest file named *Skin.config.*This file
is useful when you have multiple CSS and JavaScript files you’d like to
include other than *style.css* (though even in this case it’s not
absolutely necessary as you can reference the stylesheets in
*PageTemplate.ascx* directly).

The other benefit of using the *skin.config* file to reference your
stylesheets and script files is you can take advantage of our ability to
merge these files together at runtime using the `StyleMergeMode` and
`ScriptMergeMode` attributes.

Also, in some cases, a skin can have multiple *themes* differentiated by
stylesheet [as described in this blog
post](https://haacked.com/archive/2006/08/26/Mile_High_Overview_Of_Subtext_Skinning.aspx "Mile High Overview of Subtext Skinning").
A *skin.config* file can be used to specify these skin themes and their
associated CSS file.

### Creating a Skin.config file

Creating a *skin.config* file shouldn’t be too difficult. If you already
have a *Skins.User.config* file, it’s a matter of copying the section of
that file that pertains to your skin into a *skin.config* file within
your skin folder and removing some extraneous nodes.

Here’s an example of a new *skin.config* file for my personal skin.

```csharp
<?xml version="1.0" encoding="utf-8" ?>
<SkinTemplates>
    <SkinTemplate Name="Haacked-3.0">
        <Scripts>
            <Script Src="~/scripts/lightbox.js" />
            <Script Src="~/scripts/XFNHighlighter.js" />
        </Scripts>
        <Styles>
            <Style href="~/css/lightbox.css" />
            <Style href="~/skins/_System/csharp.css" />
            <Style href="~/skins/_System/commonstyle.css" />
            <Style href="~/skins/_System/commonlayout.css" />
            <Style href="~/scripts/XFNHighlighter.css" />
            <Style href="IEPatches.css" conditional="if IE" />
        </Styles>
    </SkinTemplate>
</SkinTemplates>
```

If you compare it to the old format, you’ll notice the `<Skins>` element
is gone and there’s no need to specify the *TemplateFolder* since it’s
assumed the folder containing this file is the template folder.

Hopefully soon, we’ll provide more comprehensive documentation on our
wiki so you don’t have to go hunting around my blog for information on
how to skin your blog. My advice is to copy an existing skin and just
tweak it.

