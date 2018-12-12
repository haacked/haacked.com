---
title: Dynamic Language DSL vs Xml Configuration
date: 2008-04-18 -0800
disqus_identifier: 18477
categories: [dsl]
redirect_from: "/archive/2008/04/17/dynamic-language-dsl-vs-xml-configuration.aspx/"
---

*Disclaimer: My opinions only, not anyone else’s. Nothing official here.
I shouldn’t have to say this, but past history suggests I should. P.S.
I’m not an expert on DSLs and Dynamic Languages ;)*

This week I attended a talk by [John
Lam](http://www.iunknown.com/ "John Lam on Software") on
[IronRuby](http://www.ironruby.net/ "IronRuby") in which he trotted out
the Uncle Ben line, *with great power comes great responsibility*. He
was of course referring to the power in a dynamic language like Ruby.

Another quip he made stuck with me. He talked about how his brain
sometimes gets twisted in a knot reading Ruby code written using
[metaprogramming](http://en.wikipedia.org/wiki/Metaprogramming "Metaprogramming")
techniques for hours at a time. It takes great concentration
comprehending code on a meta and meta-meta level in which the code is
manipulating and even rewriting code at runtime. Perhaps this is why C\#
will remain my primary language in the near term while I try and expand
my brain to work on a higher level. ;)

However, the type of code I think he is referring to is the code for
implementing a DSL itself. Once a DSL is written though, the code on top
of that DSL ought to be quite readable. This is the nook where I see
myself adopting IronRuby prior to using it as my primary language.I can
see myself creating and using mini-DSLs (Domain Specific Languages) here
and there as replacement for configuration.

Ahhh... configuration. I sometimes think this is a misnomer. At least in
the way that the Java and .NET community have approached config in
practice. We’ve had this trend in which we started jamming everything
into XML configuration.

So much so, we often get asked to provide XML to configure features I
think ought to be set in code along with unit tests. We’ve turned XML
into a programming language, and a crappy one at that. Ayende [talks
about one
issue](http://www.ayende.com/Blog/archive/2008/04/17/Source-control-is-not-a-feature-you-can-postphone-to.aspx "Cannot postpone -Source Control")
with sweeping piles of XML configuration under a tool. This is not an
intractable problem, but it highlights the fact that XML is code, but it
is code with a lot of *ceremony* compared to the amount of *essence*. To
understand what I mean by ceremony vs essence read [Ending Legacy Code
In Our
Lifetime](http://blog.thinkrelevance.com/2008/4/1/ending-legacy-code-in-our-lifetime "Ceremony vs Essence").

With the ASP.NET MVC project, we’ve taken the approach of ***Code First,
Config Second***. You can see this with our URL Routing feature. You
define routes in code, and we might provide configuration for this
feature in a future version.

With this approach, you can write unit tests for your route definitions
**which is a good thing**! Routes basically turn the URL into a method
invocation, why wouldn’t you want to have tests for that?

The reason I write about this now is that I’ve been playing around with
IronRuby lately and want to post on some of the interesting stuff I’ve
been doing in my own time. This post sets the context for why I am
looking into this, apart from it just being plain fun and coming from a
haacker ethic of wanting to see how things work.

