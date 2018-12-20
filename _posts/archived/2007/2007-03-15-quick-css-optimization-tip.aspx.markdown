---
title: Quick CSS Optimization Tip
date: 2007-03-15 -0800
tags: []
redirect_from: "/archive/2007/03/14/quick-css-optimization-tip.aspx/"
---

When you see the following in your CSS

    div
    {
      margin-top: 10px;
      margin-right: 20px;
      margin-bottom: 10px;
      margin-left: 20px;
    }

It makes sense to convert it to this.

    div
    {
      margin: 10px 20px;
    }

It’s cleaner and takes up less space.

There are a lot of ways you can optimize your CSS in this way. I'm not
talking about compression, but optimization.

Today, [The Daily Blog
Tips](http://www.dailyblogtips.com/speed-up-your-site-optimize-your-css/ "The Daily Blog Tips")
site linked to a website called
[CleanCSS](http://www.cleancss.com/ "CleanCSS - Optimize your CSS") that
can perform many of these optimizations for you. For example, feed it
the above CSS and it will make that conversion. Very nice!

