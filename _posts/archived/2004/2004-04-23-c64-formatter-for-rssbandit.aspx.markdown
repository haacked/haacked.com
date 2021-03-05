---
title: New RSS Bandit Formatter Stylesheet - C64
tags: [rssbandit]
redirect_from: "/archive/2004/04/22/c64-formatter-for-rssbandit.aspx/"
---

![C64](https://haacked.com/assets/images/C64.gif)I was fooling around with
building a custom stylesheet for formatting RSS items within RSS Bandit
and I created one that tries to emulate the Commodore 64 look and feel.
While it is possible to download the exact font used by the 64, I chose
not to require a font download and instead chose *Terminal*. It is close
enough and exists on most machines. If you want this style for yourself,
[download the template
here](https://haacked.com/xslt/C64.xslt "XSLT for C64") and save it to
the following directory (assuming a default installation):

`C:\Program Files\RssBandit\templates`

If you're designing your own templates, I have a little tip for you.

-   Grab the XML for an RSS from your cache located at
    `C:\Documents and Settings\PHaack\Application Data\RssBandit\Cache`
    on my computer.
-   Drop it in your templates directory.
-   Open it up in a text editor (such as Notepad) and add the following
    line just underneath the XML declaration
-   `<?xml:stylesheet type="text/xsl" href="PathToYourFormatter.xslt"?>`

    Now when you open the XML file in Internet Explorer, it
    automatically applies the stylesheet to the XML allowing you to
    quickly test changes to your formatter stylesheet. Note that this
    will not work if you are using the `AppStartupPath` or
    `AppUserDataPath` variables as these will not be in context.



