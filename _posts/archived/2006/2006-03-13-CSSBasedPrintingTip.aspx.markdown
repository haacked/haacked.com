---
title: CSS Based Printing Tip
date: 2006-03-13 -0800
disqus_identifier: 12071
tags: []
redirect_from: "/archive/2006/03/12/CSSBasedPrintingTip.aspx/"
---

This is a follow up tip to my post on [Implementing CSS Based
Printing](https://haacked.com/archive/2006/03/09/ImplementingCSSBasedPrinting.aspx "Printing Article").

One technique that served me well on a project recently was to implement
a very simple `print.css` for the print stylesheet. In fact, it looks
like this:

```csharp
.noprint
{
    display: none;
}
```

Make sure you declare the stylesheet properly:

\<link rel="stylesheet" type="text/css" href="print.css" media="print"
/\>

And now simply add the css class `noprint` to any elements in your HTML
you do not want printed as in the simple example below.

```csharp
<html>
<head><title>example</title></head>
<body>
<div id="main">
   <div id="header" class="noprint"></div>
   <div id="sidebar" class="cool noprint"></div>
   <div id="content"></div>
   <div id="footer" class="noprint"></div>
</div>
</body>
</html>
```

This is useful especially when retrofitting a complicated html page to
use CSS based printing.

