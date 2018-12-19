---
title: Subtext Skinning Changes
date: 2006-08-26 -0800
disqus_identifier: 16090
categories: []
redirect_from: "/archive/2006/08/25/Subtext_Skinning_Changes.aspx/"
---

With the Subtext 1.9 release just around the corner, this is probably a
good time to highlight some minor, but important, changes to skinning in
Subtext.

We made some breaking changes to `Skins.config` file format to make the
naming more consistent with the purpose.  There was a lot of confusion
before.  The following is a snippet from a pre-Subtext 1.9 Skins.config
file.

```csharp
<?xml version="1.0"?>
<SkinTemplates xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Skins>
    <SkinTemplate SkinID="RedBook" 
                    Skin="RedBook" 
                    SecondaryCss="Red.css">
      <Scripts>
        <Script Src="~/Admin/Resources/Scripts/niceForms.js" />
      </Scripts>
      <Styles>
        <Style href="niceforms-default.css" />
        <Style href="print.css" media="print" />
      </Styles>
    </SkinTemplate>
  </Skins>
</SkinTemplates>
```

And here is how that snippet will change in Subtext 1.9.

```csharp
<?xml version="1.0"?>
<SkinTemplates xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Skins>
    <SkinTemplate Name="RedBook" 
                  TemplateFolder="RedBook" 
                  StyleSheet="Red.css">
      <Scripts>
        <Script Src="~/Admin/Resources/Scripts/niceForms.js" />
      </Scripts>
      <Styles>
        <Style href="niceforms-default.css" />
        <Style href="print.css" media="print" />
      </Styles>
    </SkinTemplate>
  </Skins>
</SkinTemplates>
```

The key differences are in the `SkinTemplate` element. The following
attributes have been renamed:

-   **`SkinID`** was changed to **`Name`**
-   **`Skin`** was changed to **`TemplateFolder`**
-   **`SecondaryCss`** was changed to **`StyleSheet`**

Another new change is that the `Style` element now supports a new
attribute named `conditional`. If specified, Subtext will wrap the
stylesheet declaration with an [IE specific conditional
comment](http://www.quirksmode.org/css/condcom.html "Conditional Comments").
This is commonly used for stylesheets that contain IE specific CSS
hacks. For example...

```csharp
<Style href="IEHacks.css" conditional="if ie" />
```

Gets rendered as...

```csharp
<!--[if ie]>
<Style href="IEHacks.css" conditional="if IE" />
<![endif]-->
```

Thus only IE will see that style declaration.

