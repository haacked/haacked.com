---
title: An Alternative Approach To Strongly Typed Helpers
date: 2009-06-02 -0800
tags: [aspnetmvc]
redirect_from: "/archive/2009/06/01/alternative-to-expressions.aspx/"
---

One of the features contained in the [MVC
Futures](http://aspnet.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=24471 "ASP.NET MVC 1.0 Source")
project is the ability to generate action links in a strongly typed
fashion using expressions. For example:

```aspx-cs
<%= Html.ActionLink<HomeController>(c => c.Index()) %>
```

Will generate a link to to the `Index` action of the `HomeController`.

It’s a pretty slick approach, but it is not without its drawbacks.
First, the syntax is not one you’d want to take as your prom date. I
guess you can get used to it, but a lot of people who see it for the
first time kind of recoil at it.

The other problem with this approach is performance as seen in this
[slide
deck](http://www.slideshare.net/rudib/aspnet-mvc-performance "ASP.NET MVC Performance")
I learned about from [Brad
Wilson](http://bradwilson.typepad.com/ "Brad Wilson"). One of the pain
points the authors of the deck found was that the compilation of the
expressions was very slow.

I had thought that we might be able to mitigate these performance issues
via some sort of caching of the compiled expressions, but that might not
work very well. Consider the following case:

```aspx-cs
<% for(int i = 0; i < 20; i++) { %>

  <%= Html.ActionLink<HomeController>(c => c.Foo(i)) %>

<% } %>
```

Each time through that loop, the expression is the same: `c => c.Foo(i)`

But the value of the captured “i” is different each time. If we try to
cache the compiled expression, what happens?

So I started thinking about an alternative approach using code
generation against the controllers and circulated an email internally.
One approach was to code gen action specific action link methods. Thus
the about link for the home controller (assuming we add an id parameter
for demonstration purposes) would be:

```aspx-cs
<%= HomeAboutLink(123) %>
```

Brad had mentioned many times that while he likes expressions, he’s no
fan of using them for links and he tends to write specific action link
methods just like the above. So what if we could generate them for you
so you didn’t have to write them by hand?

A couple hours after starting the email thread, [David
Ebbo](http://blogs.msdn.com/davidebb/ "David Ebbo's Blog") had an
[implementation of this ready to show
off](http://blogs.msdn.com/davidebb/archive/2009/06/01/a-buildprovider-to-simplify-your-asp-net-mvc-action-links.aspx#comments "A build provider to simplify action links").
He probably had it done earlier for all I know, I was stuck in meetings.
Talk about the best kind of declarative programming. I declared what I
wanted roughly with hand waving, and a little while later, the code just
appears! ;)

David’s approach uses a BuildProvider to reflect over the Controllers
and Actions in the solution and generate custom action link methods for
each one. There’s plenty of room for improvement, such as ensuring that
it honors the `ActionNameAttribute` and generating overloads, but it’s a
neat proof of concept.

One disadvantage of this approach compared to the expression based
helpers is that there’s no refactoring support. However, if you rename
an action method, you will get a compilation error rather than a runtime
error, which is better than what you get without either. One advantage
of this approach is that it performs fast and doesn’t rely on the funky
expression syntax.

These are some interesting tradeoffs we’ll be looking closely at for the
next version of [ASP.NET MVC](http://asp.net/mvc "ASP.NET MVC Website").

