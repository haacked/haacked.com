---
title: Notes on ASP.NET MVC CodePlex Preview 4
date: 2008-07-16 -0800
disqus_identifier: 18504
categories: [aspnetmvc]
redirect_from: "/archive/2008/07/15/aspnetmvc-codeplex-preview4.aspx/"
---

(If you want to skip all the blah blah blah, [**go straight to the
release**](http://www.codeplex.com/aspnet/Release/ProjectReleases.aspx?ReleaseId=15389 "ASP.NET MVC CodePlex Preview 4"))

What I love about working with [The
Gu](http://weblogs.asp.net/scottgu "Scott Guthrie's Blog") (aka ScottGu,
the man with many aliases) is that he makes my life easier with his
gargantuan and detailed blog posts covering the features of each
release. This allows me to follow up and fill in some details with a
much shorter post, as by the time we get a release out the door, I’m
usually too exhausted to write such a detailed post as he does. Yeah,
excuses excuses.

In his latest post, Scott covers the [ASP.NET MVC CodePlex Preview
4](http://weblogs.asp.net/scottgu/archive/2008/07/14/asp-net-mvc-preview-4-release-part-1.aspx "ASP.NET MVC CodePlex Preview 4 Part 1")
release in two parts. This is the next in a continuing series of preview
releases that alternate between full CTP level releases on ASP.NET and
interim releases on CodePlex. Unlike previous CodePlex releases, this
one contains an MSI installer for convenience. There are still some
rough spots with some of the new features as we tried to get a lot into
this release to elicit feedback. In fact, I’ve been doing some app
building today and have already run into some areas for improvements
with the Ajax helpers. I look forward to hearing your feedback as well.

### MVC Futures

One thing you’ll notice in the project template that Scott didn’t cover
in much detail is that we include a new assembly,
`Microsoft.Web.Mvc.dll`. The release notes explain what this is.

> The ASP.NET MVC team builds prototypes for a lot of features during
> the course of normal development. Some of these features will not be
> included in the RTM release, but are very likely to be included in a
> future full release. We’ve moved many of these features into a
> separate assembly, Microsoft.Web.Mvc.dll.

This follows what the ASP.NET’s team intends the term *Futures* to mean
when it comes to products. *Futures* should contain features that we
think has a decent chance of making it into the framework. If we
implement something we are pretty sure we have no intention of including
in the framework, we’ll typically post it as a blog sample or something
to that effect.

### Component Controller

For those that have used this, you’ll notice that we removed the
`ComponentController `class. Instead, we renamed `RenderComponent` to be
`RenderAction` and re-implemented it so that it works against a normal
controller. This code is was moved to the MVC Futures assembly since
we’re not planning to include it in the RTM.

My personal opinion is that this violates the separation of concerns so
important to the MVC pattern. Having a method within your view calling
back into a controller in order to render out a bit more view makes me
feel a wee bit dirty ;). We’re developing prototypes for an alternative
approach. In the meanwhile, I recognize that for a small sacrifice of
pattern purity, this method is very useful, hence its inclusion in the
MVC Futures assembly, but do understand that you use it at your own
risk.

### What’s Next?

The [roadmap for ASP.NET
MVC](http://www.codeplex.com/aspnet/Wiki/View.aspx?title=Road%20Map&referringTitle=Home "ASP.NET MVC Roadmap")
has been up on the CodePlex site for a while. We still have more cleanup
and implementation work to do on the Ajax features, as well as existing
and new helper methods. Likewise, we are tackling the problem of how
should you report validation errors to the user via a common error
reporting mechanism. This might not handle validation itself, but gives
everyone a common place to put validation messages and allows for some
of HTML helpers to be aware of these messages and render themselves
accordingly.

And before I forget, you can [**download ASP.NET MVC CodePlex Preview 4
from the release
page**](http://www.codeplex.com/aspnet/Release/ProjectReleases.aspx?ReleaseId=15389 "Download ASP.NET MVC CodePlex Preview 4").

