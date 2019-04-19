---
title: 'CSS Question: Two Backgrounds on the Same Element?'
date: 2005-04-04 -0800
tags: [code]
redirect_from: "/archive/2005/04/03/css-question-two-backgrounds-on-the-same-element-again.aspx/"
---

As part of my site's redesign, I wanted to keep the drop shadow effect
on the left and right borders of my main content area. No problem I
naively thought, I'll simply add two background elements to the main
div. I named the div "background" like so:

```csharp
<div id="background">
</div>
```

And in my style sheet, I tried the following:

    #background
    {
        background: url(leftBorder.gif) repeat-y left;
        background: url(rightBorder.gif) repeat-y right;
    }

Unfortunately this did not work as only one of the background images
showed up. What I ended up resorting to was using two nested divs. The
inner div would contain the main content and the right border while the
outer div would display the left border.

```csharp
<div id="backgroundLeft">
   <div id="background">
   </div>
</div>
```

At this point, I needed the two divs to overlap each other just right.
The inner div needed to align over the outer div's right edge. On the
left side, the inner div needed to expose the outer div's left edge so
that the background image would be displayed. Here's the CSS I used.

    #backgroundleft
    {
        margin: 0px;
        background: url(leftBorder.gif) repeat-y left;
        width: 784px;
    }

    #background
    {
        background: url(rightBorder.gif) repeat-y right;
        margin-right: -11px;
        margin-left: 11px;
        position: relative;
        top: 0px;
        left: 0px;
        padding-top: 3px;
        width: 783px;
    }

So my question to you CSS gurus out there (if any), is there a better
way for me to accomplish this?

