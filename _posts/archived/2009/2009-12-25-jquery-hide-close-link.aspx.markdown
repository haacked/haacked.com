---
title: jQuery Hide/Close Link
tags: [code]
redirect_from: "/archive/2009/12/24/jquery-hide-close-link.aspx/"
---

UPDATE (12/26): I updated this post to use the `href` instead of the
`rel` attribute

It’s Christmas day, and yes, I’m partaking in the usual holiday fun such
as watching Basketball, hanging out with the family and eating our
traditional Alaskan king crab Christmas dinner. But of course it
wouldn’t be a complete day without writing a tiny bit of code!

[![code](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/jQueryHideCloseButton_C334/code_3.jpg "code")](http://www.sxc.hu/photo/58511 "the source by befresh on sxc.hu")

Today I’ve been working on improving the UI here and there in Subtext.
One common task I run into over and over is using an anchor tag to
trigger the hiding of another element such as a `DIV`. It happens so
often that I get pretty tired of hooking up each and every link to the
element it must hide. Being the lazy bastard that I am, I thought I’d
try to come up with a way to do this once and for all with jQuery and a
bit of convention.

Here’s what I came up with. The following HTML shows a `DIV` element
with an associated link that when clicked, should hide the `DIV`.

```csharp
<div id="hide-this">
    This here DIV will be hidden when you click on 
    the link
</div>
<a href="#hide-this" class="close">This is the link that hides the DIV</a>
```

The convention here is that any anchor tag with a class “`close`” is
going to have its click event hooked up to close another element. That
element is identified by the anchor tag’s `rel` `href` attribute, which
contains the id of the element to hide. This was based on a suggestion
by a couple of commenters to the original version of this post where I
used a rel attribute. I like this much better for two reasons:

-   The `href` value is a hash which is already in the correct format to
    be a CSS selector for an ID.
-   I’m not using the `href` value in the first place, so might as well
    make use of it.

~~Yeah, this is probably an abuse of this attribute, but in this case
it’s one I can live with due to the benefits it produces. The `rel`
attribute is supposed to define the relationship of the current document
to the document referenced by the anchor tag. Browsers don’t do anything
with this attribute, but search engines do as in the case with
the~~[~~`rel` value of
“no-follow”~~](https://haacked.com/archive/2005/01/20/rel-no-follow-important.aspx "Why Rel="no-follow" is important")~~.~~

~~However in this case, I feel my usage is in the spirit of this
attribute as I’m defining the relationship of the anchor tag to another
element in the document. Also, search engines are going to ignore the
value I put in there unless the id happens to match a valid value, so no
animals will be harmed by this.~~

Now I just need a little jQuery script to make the magic happen and hook
up this behavior.

```csharp
$(function() {
    $('a.close').click(function() {
        $($(this).attr('href')).slideUp();
        return false;
    });
});
```

I happened to choose the slide up effect for hiding the element in this
case, but you could choose the `hide` method or `fadeOut` if you prefer.

I put up a **[simple demo
here](http://demo.haacked.com/hide-anchor/ "Hide Anchor Demo")** if you
want to see it in action.

I’m just curious how others handle this sort of thing. If you have a
better way, do let me know. :)

