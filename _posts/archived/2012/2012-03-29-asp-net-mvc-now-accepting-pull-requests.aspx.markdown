---
title: ASP.NET MVC Now Accepting Pull Requests
date: 2012-03-29 -0800
disqus_identifier: 18854
categories:
- code
- oss
- asp.net
- asp.net mvc
redirect_from: "/archive/2012/03/28/asp-net-mvc-now-accepting-pull-requests.aspx/"
---

Changing a big organizations is a slow endeavor. But when people are
passionate and persistent, change does happen.

Three years ago, the ASP.NET MVC source code was released under an open
source license. But at the time, the team could not accept any code
contributions. In [my blog
post](https://haacked.com/archive/2009/04/01/aspnetmvc-open-source.aspx "ASP.NET MVC released as OSS")
talking about that release, I said the following (emphasis added):

> Personally (and this is totally my own opinion), I’d like to reach the
> point where we could accept patches. There are many hurdles in the
> way, but if you went back in time several years and told people that
> Microsoft would release several open source projects ([Ajax Control
> Toolkit](http://www.codeplex.com/AjaxControlToolkit), MEF, DLR,
> [IronPython](http://www.codeplex.com/Wiki/View.aspx?ProjectName=IronPython)
> and [IronRuby](http://www.ironruby.net/), etc….) you’d have been
> laughed back to the present.**Perhaps if we could travel to the future
> a few years, we’ll see a completely different landscape from today.**

Well my friends, we have travelled to the future! Albeit slowly, one day
at a time.

As everyone and their mother knows by now, yesterday [Scott Guthrie
announced](http://weblogs.asp.net/scottgu/archive/2012/03/27/asp-net-mvc-web-api-razor-and-open-source.aspx "ASP.NET Web Stack Open Sourced")
that the entire ASP.NET MVC stack is being released under an open source
license ([Apache
v2](http://www.apache.org/licenses/LICENSE-2.0.html "Apache v2")) and
will be developed under an open and collaborative model:

-   ASP.NET MVC 4
-   ASP.NET Web API
-   ASP.NET Web Pages with Razor Syntax

Note that ASP.NET MVC and Web API have been open source for a long time
now. The change that Scott announced is that ASP.NET Web Pages and
Razor, which until now was not open source, will also be released under
an open source license.

Additionally, the entire stack of products will be developed in the open
in a [Git repository in
CodePlex](http://aspnetwebstack.codeplex.com/ "ASP.NET Web Stack in Git on CodePlex")
and the team will accept external contributions. This is indeed exciting
news!

Hard Work
---------

It’s easy to underestimate the hard work that the ASP.NET MVC team and
Web API team did to pull this off. In the middle of an aggressive
schedule, they had to completely re-work their build systems, workflow,
etc… to move to a new source control system and host. Not to mention
integrate two different teams and products together into a single team
and product. It’s a real testament to the quality people that work on
this stack that this happened so quickly!

I also want to take a moment and credit the lawyers, who are often
vilified, for their work in making this happen.

One of my favorite bits of wisdom Scott Guthrie taught me is that the
lawyers’ job is to protect the company and reduce risk. **If lawyers had
their way, we wouldn’t do anything because that’s the safest choice.**

But it turns out that the biggest threat to a company’s long term
well-being is doing nothing. Or being paralyzed by fear. And
fortunately, there are some lawyers at Microsoft who get that. And
rather than looking for reasons to say NO, they looked for reasons to
say YES! And looked for ways to convince their colleagues.

I spent a lot of time with these lawyers poring over tons of legal
documents and such. Learning more about copyright and patent law than I
ever wanted to. But united with a goal of making this happen.

These are the type of lawyers you want to work with.

Submitting Contributions
------------------------

For those of you new to open source, keep in mind that this doesn’t mean
open season on contributing to the project. Your chances of having a
contribution accepted are only **slightly better than before**.

Like any good open source project, I expect submissions to be reviewed
carefully. To increase the odds of your pull request being accepted,
don’t submit unsolicited requests. Read the [contributor
guidelines](http://aspnetwebstack.codeplex.com/wikipage?title=Contributing&referringTitle=Home "Contributor Guidelines.")
(*I was happy to see their similarity to the*[*NuGet
guidelines*](http://docs.nuget.org/docs/contribute/contributing-to-nuget "NuGet guidelines"))
first and start a discussion about the feature. It’s not that an
unsolicited pull request won’t ever be accepted, but the more that
you’re communicating with the team, the more likely it will be.

Although their guidelines don’t state this, I highly recommend you do
your work in a feature branch. That way it’s very easy to pull upstream
changes into your local master branch without disturbing your feature
work.

Many kudos to the ASP.NET team for this great step forward, as well as
to the CodePlex team for adding Git support. I think Git has a bright
future for .NET and Windows developers.

