---
layout: post
title: "Updated Routing With WebForms"
date: 2008-05-19 -0800
comments: true
disqus_identifier: 18487
categories: [asp.net]
---
A while back I wrote a sample that demonstrated how to [use Routing with
WebForms](http://haacked.com/archive/2008/03/11/using-routing-with-webforms.aspx "Using Routing with WebForms").

*If you missed it, you can **[download the
code](http://haacked.com/code/WebFormRoutingDemo.zip "Download the code")**
here*

With the recent announcement that [Routing will be included with .NET
3.5
SP1](http://haacked.com/archive/2008/05/12/sp1-beta-and-its-effect-on-mvc.aspx "Routing in SP1"),
you can see why I wanted to put that demo together.

I have since updated that sample to work with the versions of Routing
that comes with the [April CodePlex build of
MVC](http://www.codeplex.com/aspnet/Release/ProjectReleases.aspx?ReleaseId=12640 "MVC Interim").
This should also work with the SP1 Beta. I’ll verify that when I get a
moment.

As part of this update, I added a new feature which allows applying a
simple substitution. For example, suppose you want URLs such as
*/forms/whatever* to route to a physical file */forms/whatever.aspx*.
You can do the following:

```csharp
routes.Map("forms/{whatever}").To("~/forms/{whatever}.aspx");
```

One mistake a lot of people make when looking at URL Routing is to think
of it as URL Rewriting. The difference is subtle, but important. There
is no URL rewriting going on with Routing.

What happens here is when an incoming request URL matches this route
(aka the URL pattern on the left), the `WebFormRouteHandler` will
instantiate the physical ASPX Page specified in the virtual path on the
right and use that http handler to handle the request (Recall that the
`Page` class implements the `IHttpHandler` interface). As far as the
page is concerned, the URL *is* */forms/whatever/*. For example, this
means that the URL rendered by your form will match the current URL,
unlike what typically happens with URL rewriting.

This is why the above won’t work if you try to map a route to a virtual
path that contains a query string:

```csharp
//WRONG!!!
routes.Map("forms/{whatever}").To("~/{whatever}.aspx?what={whatever}");
```

The reason the above route won’t work is that the virtual path on the
right isn’t valid. The path needs to specify a page we can instantiate,
not a request for an URL.

To save you from having to visit the previous post, here is a link to
**[download the code](http://haacked.com/code/WebFormRoutingDemo.zip)**

Technorati Tags:
[routing](http://technorati.com/tags/routing),[aspnetmvc](http://technorati.com/tags/aspnetmvc)

