---
title: Clickable Background Images Via CSS
tags: [code,css]
redirect_from: "/archive/2006/01/12/ClickableBackgroundImagesViaCSS.aspx/"
---

On a recent project, my team pursued a CSS based design as we had two
sites to build that were similar in layout, but different in look and
feel. We were brought in after the schematics and design had pretty much
been worked out, but we felt we could work with the agreed upon design.

The site had a typical corporate layout: a header, a body, and a footer.
The body might have two or three columns. We started off writing markup
that had a structure like so (not including the body columns).

```html
<div id="main">
    <div id="header"></div>
    <div id="body">Body</div>
    <div id="footer">Footer</div>
</div>
```

We set the logo using CSS by applying a background image to the header
div.

```css
#header
{
    background: url(logoExample.jpg);
    width: 180px;
    height: 135px;
}
```

Which produces something that might look like this (*Trust me, the real
thing looks a lot better*)...

![](https://haacked.com/assets/images/PageExample.jpg)

So everything is fine and dandy till we place the site on a staging
server and the client asks that the header logo link back to the main
page. This wasn’t in any of the requirements or design spec, but it is
perhaps something we could have guessed as it is quite common.

So how do we make the logo image be a clickable link to the main page?
My first inclination was to abandon using a background image and make
the logo a regular image. The markup would look like (changes in
bold)...

```html
<div id="main">
    <div id="header">
        <a href="/"><img src="images/logoExample.jpg"></a>
    </div>
    <div id="body">Body</div>
    <div id="footer">Footer</div>
</div>
```

But I found a better way to do this based on a technique I saw in
“[Bulletproof Web
Design](http://www.amazon.com/exec/obidos/redirect?link_code=as2&path=ASIN/0321346939&tag=youvebeenhaac-20&camp=1789&creative=9325 "Bulletproof Web Design")”.
I changed the markup to be like so...

```html
<div id="main">
    <div id="header">
        <a href="/" title="Home"><h1>Title</h1></a>
    </div>
    <div id="body">Body</div>
    <div id="footer">Footer</div>
</div>
```

I then changed the css for the anchor tag to have the same dimensions as
the logo image. I positioned it so that it would fit exactly over the
image.

```css
#header
{
    background: url(logoExample.jpg);
    width: 180px;
    height: 135px;
    position: relative;
}

#header a
{
    position: absolute;
    top: 0;
    left: 0;
    width: 180px;
    height: 135px;
}

#header a h1
{
    display: none;
}
```

Notice that I had to add `position: relative;` to the header element .
That ensures that the absolute positioning applied to the header link is
relative to the header and not the entire document.

Now the header logo image appears to be a clickable link. Problem
solved. I am pretty sure that others have pioneered this trick, but I
hadn’t seen anything written up. What I read applied to making clickable
tabs.

UPDATE: As [Klevo](http://klevo.aspweb.cz/) mentioned in the comments, I
really shouldn’t have an anchor tag without any text. Including text
would be good for search engine optimization and for those who view the
site without CSS. Shame on me, especially after reading the bulletproof
book.

But to my defense, it was peripheral to the main point I was making.
However that doesn’t excuse it as bad samples have a way of
proliferating. So I corrected the sample above. The anchor tag now
includes the title of the blog, but sets the title to be invisible.
