---
title: Stop The Window.Onload Madness
tags: [javascript]
redirect_from: "/archive/2006/04/05/StopTheWindow.OnloadMadness.aspx/"
---

![Madness - Image from DC
Comics](https://haacked.com/assets/images/JokerMadness.jpg) There are a lot
[cool](/archive/2006/04/05/MakingMicroformatsMoreVisibleAnnouncingTheXFNHighlighterScript.aspx "XFN Highlighter Script")
[javascript](http://paraesthesia.com/blog/comments.php?id=674_0_1_0_C "Amazon Associates Tooltips")
[libraries](http://www.huddletogether.com/projects/lightbox2/ "Lightbox JS v2.0")
floating around the *Intarweb* these days that add cool behavior to web
pages. My favorites are the ones that you simply add to the `head`
section of your website and [control via
markup](http://weblogs.asp.net/jgalloway/archive/2006/01/18/435857.aspx "Using Markup Based Javascript Effect Libraries").
It is a great way to [enhance structural html markup with
Javascript](http://www.sitepoint.com/print/structural-markup-javascript "Enhancing Structural Markup with Javascript").

Unfortunately many of these attempt to hijack the `window.onload` event.
Ok, a show of hands (and I have been guilty of this as well). How many
of you have written code like this to handle the onload event in
javascript within a .js file?

```js
function init()
{
}

window.onload = init;
```

**Stop it!**

That line of code will completely wipe out any other functions that were
attached and ready to handle the onload event. How arrogant of your
script to do so. Instead, your script should learn to play nicely.

Unfortunately, Javascript doesn’t support the delegate syntax that C#
has. It’d be nice to be able to do this.

```csharp
function init()
{
}

window.onload += init;
```

But that won’t work. One approach I found on [Simon Incutio’s
blog](http://simon.incutio.com/ "Simon Incutio's Blog") (which is used
by the [original LightboxJS
script](http://www.huddletogether.com/projects/lightbox/ "LightboxJS"))
involves using a method that safely attaches an event handling method to
the onload event without overwriting existing event handlers.

It works by checking to see if there any methods are already attached to
the event. If so it attaches a new anonymous method that calls the
original method along with the method you are attempting to attach.

Here is a snippet demonstrating this technique.

```js
function highlightXFNLinks()
{
  // Does stuff...
}

//
// Adds event to window.onload without overwriting currently 
// assigned onload functions.
function addLoadEvent(func)
{    
    var oldonload = window.onload;
    if (typeof window.onload != 'function')
    {
        window.onload = func;
    } 
    else 
    {
        window.onload = function()
        {
            oldonload();
            func();
        }
    }
}

addLoadEvent(highlightXFNLinks);
```

This is pretty nifty, but there appears to be a whole new school of
script libraries that provide this sort of functionality for attaching
to any event, not just the `window.onload` event.

I am sure you Javascript Gurus will expose how out of date and ignorant
I am of this area (it is true) but the few that I have heard of that
seem to be catching on like wildfire are the [Prototype JavaScript
Framework](http://prototype.conio.net/ "Prototype Javascript Framework")
(often just referred to as prototype.js), the [Dojo
Toolkit](http://dojotoolkit.org/ "Dojo v0.2.2"), and
[Behaviour](http://bennolan.com/behaviour/ "Behaviour").

I will probably end up rewriting all my libraries to use one of these
tooltips so that I stop duplicating code. Since each of my javascript
libraries are stand-alone, I make sure to include the `addLoadEvent`
method in each of them. But I think its time to allow a dependency on
another script to avoid this duplication.

