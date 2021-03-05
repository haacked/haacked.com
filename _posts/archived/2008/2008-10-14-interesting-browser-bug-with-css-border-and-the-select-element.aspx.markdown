---
title: Interesting Browser Bug with CSS border and the Select element
tags: [code]
redirect_from: "/archive/2008/10/13/interesting-browser-bug-with-css-border-and-the-select-element.aspx/"
---

UPDATE: Pretty much disregard this entire post, except as a reminder
that it’s easy to make a mistake with DOCTYPEs and markup. As many
people have told me, I had an error I didn’t notice in the original
HTML. I forgot to close the SELECT tag. I’ll leave the post as-is.

Not only that, the DOCTYPE is not specified in the document, which
causes IE to render in Quirks mode, not standards mode. So I guess the
bug is in IE 7 rendering.

So this is a case of
[PEBKAC](http://en.wikipedia.org/wiki/PEBKAC "PEBKAC in Wikipedia"), the
bug is in the HTML, not the browser.

Here’s an [example of the HTML with the SELECT tag properly closed and
the proper
DOCTYPE](https://haacked.com/code/samples/select-with-doctype-css-border.html "Select with proper HTML").
It renders correctly in FireFox 3 and IE 8.

In testing our helper methods for rendering a \<select /\> element which
has some styling applied to it if the element has a validation error, a
developer on the MVC team found an interesting bug in how browsers
render a select element with a border applied to it via CSS. Check out
the following HTML.

```csharp
<html>
<head>
<style type="text/css">
select {
border: 1px solid #ff0000;
color: #ff0000;
}
</style>
</head>
<body>
 
<select >
  <option>A</option>
<select>
 
</body>
</html>
```

Here’s how IE 8 renders it. Notice there’s no border. UPDATE: According
to people on twitter, this is because I left out the doctype, so IE8
rendered it in old quirks mode, not standards mode.

 ![ie8-select](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/InterestingQuirkwithCSSstylesandtheSelec_D992/ie8-select_3.png "ie8-select")

Here’s Firefox 3. There’s a border, but there’s two drop-down arrows.

![select-ff](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/InterestingQuirkwithCSSstylesandtheSelec_D992/select-ff_3.png "select-ff")

Here’s Google Chrome, which gets it right. Since Google Chrome uses the
Safari Webkit rendering engine, I believe Safari gets it right as well.
I didn’t test it personally, but Opera gets it right too.

![select-chrome](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/InterestingQuirkwithCSSstylesandtheSelec_D992/select-chrome_5.png "select-chrome")

Now if you add the following meta tag to the \<head /\> section of the
HTML.

```csharp
<meta http-equiv='X-UA-Compatible' content='IE=8'>
```

IE 8 now renders correctly.

![select-ie8-meta](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/InterestingQuirkwithCSSstylesandtheSelec_D992/select-ie8-meta_3.png "select-ie8-meta")

You can see for yourself by pointing your browser to an example with
[the meta
tag](https://haacked.com/code/samples/select-with-css-border-and-meta-tag.html "Select Tag with CSS styling")
and [without
it](https://haacked.com/code/samples/select-with-css-border.html "Select Tag with CSS styling").

