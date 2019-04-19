---
title: Copy Source As HTML
date: 2004-11-02 -0800 9:00 AM
tags: [tools]
redirect_from: "/archive/2004/11/01/copy-source-as-html.aspx/"
---

My main man [Colin](http://www.jtleigh.com/people/colin/blog/) is on
fire with his latest version of [CopySourceAsHtml
add-in](http://www.jtleigh.com/people/colin/blog/archives/2004/11/copysourceashtm_2.html).

As this utility catches on, I think you'll see a huge proportion of .NET
bloggers using it to post source code snippets on their blogs. It now
uses VS.NET's own syntax highlighting to highlight the code. Thus
whatever settings you have in VS.NET are used by the add-in. It's also
much more configurable with word-wrapping, ability to add extra styling
options, etc... Here's a couple of snippets as a demonstration.

According to the example's on Colin's site, it even works with aspx and
css files. Unfortunately, that's not working for me right now as I don't
see the context menu on those pages.

Nice job Colin!

    9 ///

   10 /// This just rocks my world!

   11 ///

   12 public class HtmlSourceTest

   13 {

   14     public void ThisMethodKicksButt()

   15     {

   16         //Yep. It does.

   17         Console.Write("Hello World");

   18     }

   19 }

///

/// This just rocks my world!

///

public class HtmlSourceTest

{

    public void ThisMethodKicksButt()

    {

        //Yep. It does.

        Console.Write("Hello World");

    }

}

