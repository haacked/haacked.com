---
layout: post
title: "Live Preview jQuery Plugin"
date: 2009-12-15 -0800
comments: true
disqus_identifier: 18667
categories: [code]
---
Many web applications (such as this blog) allow users to enter HTML as a
comment. For security reasons, the set of allowed tags is tightly
constrained by logic running on the server. Because of this, it’s
helpful to provide a preview of what the comment will look like as the
user is typing the comment.

[![sneak peek - by ArminH on
sxc.hu](http://haacked.com/images/haacked_com/WindowsLiveWriter/LivePreviewjQueryPlugin_875E/preview_3.jpg "sneak peek - by ArminH on sxc.hu")](http://www.sxc.hu/photo/764984 "Sneak Peek - by ArminH from stock.xchng")That’s
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
        allowedTags: ['p', 'strong', 'br', 'em', 'strike'],
        interval: 20
    });
});
```

One thing that’s different between this implementation and others I’ve
seen is you can specify a set of allowed tags. When typing in the
textbox, the preview will render any tags in that list. If the user
types in tags which are *not* in that list, the preview will HTML encode
the tags.

Keep in mind that this plugin is for previewing what comments will look
like and should not be used as validation! The preview might not exactly
match your server-side logic.

Also for fun, I’m **[hosting the source code on
GitHub](http://github.com/Haacked/jQuery-Live-Preview "jQuery Live Preview on GitHub")**
as a way to force myself to learn what all the fuss is about GIT.

*Thanks to Bohdan Zograf, this blog post is also [available in
Belarusian](http://www.webhostinghub.com/support/by/edu/live-preview-be "translated to Belarusian").*

