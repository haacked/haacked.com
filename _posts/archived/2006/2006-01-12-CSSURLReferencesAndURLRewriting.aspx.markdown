---
title: CSS URL References And URL Rewriting
date: 2006-01-12 -0800
disqus_identifier: 11480
tags: []
redirect_from: "/archive/2006/01/11/CSSURLReferencesAndURLRewriting.aspx/"
---

I can get a bit overboard with my virtual paths. I tend to prefer
virtual paths over relative paths since they feel safer to use. For
example, when applying a background image via CSS, I will tend to do
this:

``

body

{

    background: url('/images/bg.gif');

}

My thinking was that since much of the code I write employs URL
rewriting and Masterpages, I never know what the url of the page will be
that references this css. However my thinking was wrong.

One problem I ran into is that on my development box, I tended to run
this code in a virtual directory. For example, I have Subtext running in
the virtual directory `/Subtext.Web`. So I end up changing the CSS like
so:

``

body

{

    background: url('**/Subtext.Web**/images/bg.gif');

}

Thus when I deploy to my web server, which is hosted on a root website,
I have to remove the /Subtext.Web part. Now if I had read the CSS spec
more closely, I would have noticed the following line:

> Partial URLs are interpreted relative to the source of the style
> sheet, not relative to the document.

Thus, the correct CSS in my case (assuming my css file is in the root of
the virtual application) is...

``

body

{

    background: url('images/bg.gif');

}

Now I have true portability between my dev box and my production box.

It turns out that Netscape Navigator 4.x incorrectly interprets partial
URLs relative to the html document that references the css file and not
the css file itself. Perhaps this was where I got the wrongheaded notion
embedded in my head way back in the day.

