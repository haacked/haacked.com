---
layout: post
title: "Razor Donut Caching"
date: 2011-11-25 -0800
comments: true
disqus_identifier: 18827
categories: [asp.net,asp.net mvc,code]
---
Donut caching, the ability to cache an entire page except for a small
region of the page (or set of regions) has been conspicuously [absent
from ASP.NET MVC since version
2](http://haacked.com/archive/2008/11/05/donut-caching-in-asp.net-mvc.aspx "ASP.NET MVC Donut Caching").

[![mmm-donuts](http://haacked.com/images/haacked_com/Windows-Live-Writer/Donut-Caching-in-ASP.NET-MVC_A1EE/mmm-donuts_thumb.jpg "mmm-donuts")](http://haacked.com/images/haacked_com/Windows-Live-Writer/Donut-Caching-in-ASP.NET-MVC_A1EE/mmm-donuts_2.jpg)
\
*Mmmmm, donuts! – [Photo by Pzado at
sxc.hu](http://www.sxc.hu/photo/880587 "Donut")*

This is something that’s on our Roadmap for ASP.NET MVC 4, but we have
yet to flesh out the design. In the meanwhile, there’s a new
[NuGet](http://nuget.org/ "NuGet") package written by Paul Hiles that
brings [donut caching to ASP.NET MVC
3](http://www.devtrends.co.uk/blog/donut-output-caching-in-asp.net-mvc-3 "Donut Caching").
I haven’t tried it myself yet, so be forewarned, but judging by the blog
post, Paul has done some extensive research into how output caching
works.

One issue with his approach is that to create “donut holes”, you need to
call an action from within your view. That works for ASP.NET MVC, but
not for ASP.NET Web Pages. What if you simply want to carve out a region
in your view that isn’t cached?

Well to implement such a thing requires that we make changes to Razor
pages itself to support substitution caching. I’ve been tasked with the
design of this, but I’ve been so busy that I’ve fallen behind. So I’m
going to sketch some thoughts here and get your input, and then turn in
your work as if I had done it. Ha!

Ideally, Razor should have first class support for carving out donut
holes. Perhaps something like:

```csharp
<h1>This entire view is cached</h1>
@nocache {
  <div>But this part is not. @DateTime.Now</div>
}
```

As this seems to be the most common scenario for donut holes, I like the
simplicity of this approach. However, there may be times when you do
want the hole cached, but at a different interval than the rest of the
page.

```csharp
<h1>The entire view is cached for a day</h1>
@cache(TimeSpan.FromSeconds(10)) {
  <div>But this part is cached for 10 seconds. @DateTime.Now</div>
}
```

If we have the second `cache` directive, we probably don’t really need
the `nocache` directive as its redundant. But since I think it’s the
most common scenario, I’d want to keep it anyways.

The final question is whether these should be actual Razor directives or
simply methods. I haven’t dug into Razor enough to know the answer, but
my gut feel is that it would require changes to Razor itself to support
it and can’t be added on as method calls as method calls run too late.

What do you think of this approach?

