---
title: How to Have the Benefits of Tables and Eat Your CSS Cake Too
date: 2005-05-02 -0800
disqus_identifier: 2921
tags: []
redirect_from: "/archive/2005/05/01/how-to-have-the-benefits-of-tables-and-eat-your-css-cake-too.aspx/"
---

You've heard CSS purists beat it into you over and over again that
"[Tables are bad](http://www.stopdesign.com/articles/throwing_tables/),
umkaaay?". But man, what a pain when you're trying to do something as
simple as a web form and you want the labels to align to the right and
the controls to align to the left.

So [Dimitri](http://glazkov.com/blog/) here [rides in to the rescue with
TILT](http://glazkov.com/blog/archive/2005/05/02/476.aspx) (Table
Injection for Layout Technique). He showed this to me a few weeks ago
and has finally polished it to the point that he's ready to tell the
world.

Essentially, this technique allows you to mark up your code semantically
like the CSS purist you are, but then with some crafty usage of
Javascript, modify the DOM and inject a TABLE (with all the benefits
therein). Genius.

