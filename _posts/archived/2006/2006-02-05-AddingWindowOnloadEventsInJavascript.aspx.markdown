---
title: Adding Window Onload Events In Javascript
date: 2006-02-05 -0800
disqus_identifier: 11686
categories: []
redirect_from: "/archive/2006/02/04/AddingWindowOnloadEventsInJavascript.aspx/"
---

One common approach to having a script method run when a page loads is
to attach a method to the `window.onload` event like so...

` window.onload = function() { someCode; }`

This is a common approach with methods for enhancing structural markup
with Javascript like my [table row
rollover](/archive/2006/02/05/AddingMouseOverRowHighlightingToTables.aspx "Table Row Rollover")
library, but it suffers from one major problem.

What happens when you include more than one script that does this? Only
one of them will be attached to the `onload` event.

It would be nice if there was a syntax like the C\# delegate syntax for
attaching an event handler. For example...

` window.onload += function() {}  //This doesn’t work`

However, there is a better way. In looking at the [Lightbox
script](http://www.huddletogether.com/projects/lightbox/ "Lightbox Script"),
I noticed the script references a method written about by Simon
Willison. He has a method named `addLoadEvent(func);` that will append
the method to the load event, without interfering with any existing
methods set to execute on load.

Read about [this technique
here](http://simon.incutio.com/archive/2004/05/26/addLoadEvent "Executing JavaScript on page load").

