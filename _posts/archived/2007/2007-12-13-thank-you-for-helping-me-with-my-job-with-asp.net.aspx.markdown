---
title: Thank You For Helping Me With My Job With ASP.NET MVC
date: 2007-12-13 -0800
tags:
- aspnet
- code
- aspnetmvc
redirect_from: "/archive/2007/12/12/thank-you-for-helping-me-with-my-job-with-asp.net.aspx/"
---

I have a set of little demos apps I’ve been working on that I want to
release to the public. I need to clean them up a bit (you’d be surprised
how much I swear in comments) and make sure they work with the CTP.
Hopefully I will publish them on my blog over the next few weeks.

In the meanwhile, there’s some great stuff being posted by the community
I want to call out. All these great posts are making my life easier.

-   [Routing
    Revisited](http://myheadsexploding.com/archive/2007/12/13/routing-revisited.aspx "Routing Revisited")
    - Sean Lynch talks about some interesting route scenarios. Currently
    the `Route` object doesn’t support all the scenarios he is
    attempting. This is good feedback and we’re already looking into it.
    He mentioned wanting Subtext-style URLs. You better believe I’m
    going to bring this up. ;) He also brings up a good point clarifying
    which Page templates in the *Add New Item* dialog to select when
    working with master pages. I’m sorry that dialog is crazy busy.
-   [Using script.aculo.us with ASP.NET
    MVC](http://www.chadmyers.com/Blog/archive/2007/12/10/using-script.aculo.us-with-asp.net-mvc.aspx "scriptaculous and mvc")
    - Chad Myers does some fancy schmancy AJAX stuff with ASP.NET MVC
    and the ever so flashy [Script.aculo.us
    framework](http://script.aculo.us/ "Script.aculo.us"). What! No
    [JQuery](http://jquery.com/ "JQuery")?! ;) Hasn’t anyone told Chad
    that Ajax is just a fad? All the interactivity you’ll ever need is
    in the \<blink /\> tag.
-   [ASP.NET MVC Framework - Create your own
    IRouteHandler](http://weblogs.asp.net/fredriknormen/archive/2007/11/18/asp-net-mvc-framework-create-your-own-iroutehandler.aspx "Custom IRouteHandler")
    - Fredik Normén didn’t like the fact that
    `IControllerFactory.GetController` takes in the type of the
    controller (something we’re definitely looking at) because it made
    it more difficult to use
    [Spring.NET](http://www.springframework.net/ "Spring.net app framework").
    So he went an implemented his own `IControllerFactory` and his own
    `IRouteHandler`. This is a great demonstration of how to swap out a
    couple of nodes in the “[snake
    diagram](http://weblogs.asp.net/leftslipper/archive/2007/12/10/asp-net-mvc-design-philosophy.aspx "ASP.NET MVC Design Philosophy")”
    with your own implementation. While it’s a validation of our
    extensibility story that he was able to accomplish this scenario,
    the fact that he needed to do all this also highlights areas for
    improvements.
-   [MvcContrib Open Source Project Call for
    Participation](http://codebetter.com/blogs/jeffrey.palermo/archive/2007/12/09/mvccontrib-open-source-project-call-for-participation.aspx "MvcContrib Open Source Project")
    - Jeffrey Palermo writes about a brand spanking new OSS project to
    build useful tools and libraries for MVC. Gotta give them credit for
    starting an OSS project on a CTP technology the day before it
    launched.

It is really great to see people building demos and applications on top
of ASP.NET MVC. Learing about the rough areas that you run into doing
real-world tasks is immensely valuable feedback.

### Forums!

We have an official [ASP.NET MVC
forum](http://forums.asp.net/1146.aspx "ASP.NET MVC") now for
discussing...surprise surprise...ASP.NET MVC. If you have questions
about ASP.NET MVC, I encourage you ask them there for the benefit of
others. Feel free to comment on my blog if you don’t get a satisfactory
answer in a reasonable amount of time.

Even better, jump in and help answer questions!

