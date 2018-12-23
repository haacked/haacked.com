---
title: Must We Put Code In Our Blog Titles and Subtitles?
date: 2005-09-23 -0800
tags: [blogging,humor]
redirect_from: "/archive/2005/09/22/must-we-put-code-in-our-blog-titles-and-subtitles.aspx/"
---

Perhaps it’s a rather innocuous practice, but after seeing one too many
I am compelled to rant a bit, if only to be a total jerk. What do I
speak of? I was going through the blogs in my blog reader the other day
and I started noticing the predilection for geek blogs to have “cute”
titles and/or subtitles with code snippets. I know we’re geeks, but must
we be THAT geeky? It’s become so trendy that I must start the backlash
now.

Since I know he can take some good natured ribbing, let’s start with my
friend, Jon Galloway.

**Title: `JonGalloway.ToString()`**\
 Is the `ToString()` *really* necessary? Seriously. Isn’t the title of
your blog already a string? Hopefully the compiler optimizes this
redundancy away.

**Title: `{public virtual blog}`**\
 Next we have Ryan Farley’s virtual blog. Does a virtual blog imply that
you’re not the one writing it, or that you feel your thoughts aren’t
concrete? Perhaps the text we see really isn’t there, but are mere wisps
of our imagination?

I’m sorry, but the title of your blog just doesn’t compile. Where is the
implementation, the member name, and the getter? Perhaps you meant:

> `public abstract Blog MyBlog {get;}`

Even so, you left yourself open for someone else to override your blog.

**Title: [protected virtual void jaysonBlog {A conduit to the voices in
my head.}](http://jaysonknight.com/blog/default.aspx)**\
 Another virtual blog, but at least this one is protected.
Unfortunately, it doesn’t compiled (yes, I tried). And here I thought
you were spreading better practices for C# coding.

**Title: `` `(joe (@ (version "2.0")) ,(mk-blog)) ``**\
 **Subtitle:
`(define (mk-blog) (lambda () (begin (call/cc brain) (mk-blog))))`** Joe
Duffy couldn’t simply use a language we can all recognize. Nooooooo, he
had to take it to the next level and go all the way to LISP. Does your
mom read your blog? And does she worry that you spend too much time with
the computer after seeing that title?

**Title: [Bob Yexley Net](http://yexley.net/blogs/bob/default.aspx)**\
 **Subtitle:
`-- SELECT * FROM [bobs].[brain] WHERE category IN (SELECT title FROM [blog_Categories])`**\
 And lest we leave out you DBAs, here is some SQL code from the subtitle
of Bob’s blog. Good going Bob!

So as it is now apparent by now, my lashing out is merely a response to
my own feeling of geek inadequacy when faced with true geekdom. In order
to join the geek big leagues, I hereby re-title my blog...

.method public hidebysig static string YouHaveBeen() cil managed

{

    .maxstack 1

    .locals init (

    string title)

    L\_0000: ldstr "Haacked"

    L\_0005: stloc.0

    L\_0006: br.s L\_0008

    L\_0008: ldloc.0

    L\_0009: ret

}

I hope I am worthy.

