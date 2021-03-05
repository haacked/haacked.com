---
title: Live Preview jQuery Plugin
tags: [code]
redirect_from: "/archive/2009/12/14/live-preview-jquery-plugin.aspx/"
---

Many web applications (such as this blog) allow users to enter HTML as a
comment. For security reasons, the set of allowed tags is tightly
constrained by logic running on the server. Because of this, it’s
helpful to provide a preview of what the comment will look like as the
user is typing the comment.

[![sneak peek - by ArminH on
sxc.hu](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/LivePreviewjQueryPlugin_875E/preview_3.jpg "sneak peek - by ArminH on sxc.hu")](http://www.sxc.hu/photo/764984 "Sneak Peek - by ArminH from stock.xchng")That’s
exactly what my live preview jQuery plugin does.

[**See it in
action**](http://demo.haacked.com/livepreview/ "LivePreview jQuery Plugin Demo")

This is the first jQuery Plugin I’ve written, so I welcome feedback. I
was in the process of converting a bunch of JavaScript code in
[Subtext](http://subtextproject.com/ "Subtext Blog Engine Project Website")
to make use of jQuery, significantly reducing the amount of hand-written
code in the project. Needless to say, it was a lot of fun. I decided to
take our existing live preview code and completely rewrite it using
JavaScript.

All you need for the HTML is an input, typically a `TEXTAREA` and an
element to use as the preview, typically a `DIV`

```csharp
<textarea class="source"></textarea>

<label>Preview Area</label>
<div class="preview"></div>
```

And the following script demonstrates one way to hook up the preview to
the textarea.

```csharp
$(function() {
    $('textarea.source').livePreview({
        previewElement: $('div.preview'),
        allowed