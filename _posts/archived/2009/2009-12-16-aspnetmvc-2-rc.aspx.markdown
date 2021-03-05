---
title: ASP.NET MVC 2 RC Released
tags: [aspnet,aspnetmvc]
redirect_from: "/archive/2009/12/15/aspnetmvc-2-rc.aspx/"
---

Paternity leave is not all fun and games. Mostly it’s soothing an irate
baby and toddler while dealing with explosive poo episodes. Believe me
when I say the term “blow out” is apt. That’s probably not the imagery
you were hoping for in a technical blog post, but I think you can handle
it.
;)![066](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ASP.NETMVC2RCReleased_14561/066_1.jpg "066")
*What!? It’s already time for an RC?! I think I need to be changed.*

While I’m on leave, the ASP.NET MVC team continues its hard work and is
now ready to announce the release candidate for ASP.NET MVC 2. Go get it
now!

**[Download ASP.NET MVC 2 Release
Candidate](http://go.microsoft.com/fwlink/?LinkID=157071 "ASP.NET MVC 2 Release Candidate")**

As always, the [release notes provide a summary of what’s
changed](http://go.microsoft.com/fwlink/?LinkID=157072 "ASP.NET MVC 2 Release Notes").
Also, stay tuned as I expect we’ll see one of those epic
[ScottGu](http://weblogs.asp.net/scottgu "Scott Guthrie") blog posts on
the release soon.

### Highlights

As you might expect from a release candidate, most of the work focused
on bug fixes and improvements to existing features. We also spent a lot
of time on performance profiling and optimization.

Much of the focus on this release was in the client validation scripts.
For example, the validation script was moved into its own file and can
be included at the top or bottom of the page. Client validation also now
supports globalization.

The other change related to validation is that the ValidationSummary now
supports overloads where only model-level errors are displayed. This is
useful if you are displaying validation messages inline next to each
form field. Previously, these messages would be duplicated in the
validation summary. With these new changes, you can have the summary
display an overall validation message (ex. “There were errors in your
form submission”) as well as a list of validation messages which don’t
apply to a specific field.

### What’s Next?

RTM of course! The RTM release of ASP.NET MVC will be included in the
RTM release of Visual Studio 2010, which is slated for some time in
March. The VS2008 version of ASP.NET MVC 2 might release earlier than
that. We’re still working out those details.

