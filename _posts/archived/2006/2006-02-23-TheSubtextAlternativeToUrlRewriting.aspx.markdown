---
title: The Subtext Alternative To Url Rewriting
tags: [aspnet,subtext]
redirect_from: "/archive/2006/02/22/TheSubtextAlternativeToUrlRewriting.aspx/"
---

![Fountain Pen](https://haacked.com/assets/images/fountain_pen.jpg)For simple
ASP.NET applications that do not employ URL Rewriting, stepping through
the code that handles a request is fairly straightforward. For example,
given a request for `http://localhost/MyProject/Page.aspx`, simply open
up Page.aspx and look at the code-behind file to see what code handles
this request.

But for applications such as
Subtext that
support dynamic URLs, it can be a bit daunting to find the code that
finally responds to the request.

### Common approach to URL Rewriting

Most applications that employ dynamic URLs employ a tactic called “URL
Rewriting” The approach these applications typically take is some
variant of [this
approach](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnaspp/html/urlrewriting.asp "URL Rewriting in ASP.NET")
outlined by [Scott
Mitchell](http://www.scottonwriting.net/sowBlog/ "Scott Mitchel's Blog").

In this approach, a handler maps incoming requests for a dynamic URL to
the actual ASP.NET page that handles the request. As an example,

`http://localhost/CategoryName.aspx`

Might be rewritten to:

`http://localhost/Category.aspx?Cat=CategoryName`

### The .TEXT and Subtext approach {.clear}

Subtext employs a slightly different technique that it inherits from
.TEXT which [Scott Watermasysk wrote about a while
ago](http://scottwater.com/blog/articles/UrlRewrite1.aspx ".TEXT Url Rewriting").
Instead of mapping incoming urls to different pages via a configuration
section within web.config, it pretty much maps every request to a single
page called DTP.aspx, a barebone template file.

Cracking open Subtext's (or .TEXT's) web.config file, you can see a
section named `HandlerConfiguration`. It has an attribute
`defaultPageLocation` with the value DTP.aspx.

Within that section are a set of `HttpHandler` nodes each with a regular
expression pattern. When a request comes in, the handler with the
pattern that matches the URL is invoked. Subtext adds the set of
controls defined in the `controls` attribute of that handler and then
returns a compiled instance of DTP.aspx via a call to
`PageParser.GetCompiledPageInstance`.

### Future Direction

For future versions of Subtext, we may consider changing to a model more
in line with what Scott Mitchell wrote about above for a couple of
reasons. The first is that the current model is not really supported.

According to the MSDN documentation on the `GetCompiledPageInstance`
method

> This method supports the .NET Framework infrastructure and is not
> intended to be used directly from your code.

The second reason is that it is a lot more difficult for people to
understand this method. We may gain a level of simplicity via the other
approach without losing much in flexibility. This decision will be made
when we have enough time to evaluate the differences and tradeoffs
between the two methods.

### Full Request Lifecycle in Subtext

If you are interested in a more detailed discussion of how Subtext
handles incoming requests, please check out the recently added
documentation on this subject, [An In-Depth Look At The Life of a
Subtext
Request](http://subtextproject.com/Docs/Developer/InDepthLookAtTheLifeOfARequest/ "An in-depth look at a request").

