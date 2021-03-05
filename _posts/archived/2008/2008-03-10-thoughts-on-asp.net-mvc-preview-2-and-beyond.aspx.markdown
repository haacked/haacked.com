---
title: Thoughts on ASP.NET MVC Preview 2 and Beyond
tags: [aspnet,aspnetmvc,code]
redirect_from: "/archive/2008/03/09/thoughts-on-asp.net-mvc-preview-2-and-beyond.aspx/"
---

At this year’s [Mix
conference](http://visitmix.com/2008/default.aspx "Mix08"), we announced
the availability of the second preview for ASP.NET MVC which you can
[**download from
here**](http://www.microsoft.com/downloads/details.aspx?FamilyId=38CC4CF1-773A-47E1-8125-BA3369BF54A3&displaylang=en "ASP.NET MVC Preview 2 Download Page").
Videos highlighting MVC are [also
available](http://www.asp.net/learn/3.5-extensions-videos/default.aspx "ASP.NET MVC Videos").

Now that I am back from Mix and have time to breathe, I thought I’d
share a few (non-exhaustive) highlights of this release as well as my
thoughts on the future.

### New Assemblies and Routing

Much of the effort and focus of this release was put into routing. If
you’ve installed the release, you’ll notice that MVC has been factored
into three assemblies:

-   System.Web.Mvc
-   System.Web.Routing
-   System.Web.Abstractions

The key takeaway here is that *MVC* depends on *Routing* which depends
on *Abstractions*.

> MVC =\> Routing =\> Abstractions

Routing is being used by another team here at Microsoft so we worked on
making it an independent feature to MVC. MVC relies heavily on routing,
but routing doesn’t have any knowledge of MVC. I’ll write a follow up
post that talks about the implications of that and how you might use
Routing in a non-MVC context.

Because of the other dependencies on Routing, we spent a lot of time
trying to make sure we got the API and code correct and making sure the
quality level of routing meets an extremely high bar. Unfortunately,
this investment in routing did mean that we didn’t implement everything
we wanted to implement for MVC core, but hey, it’s a preview right? ;)

### CodePlex Builds

At Mix this year [Scott
Hanselman’s](http://hanselman.com/blog/ "Scott Hanselman's Blog") gave a
[great talk (IMHO) on
MVC](http://sessions.visitmix.com/?selectedSearch=T22 "ASP.NET MVC Talk").
One thing he announced during that talk is the vehicle by which we will
be making the MVC source code available. Many of you might recall
[ScottGu’s](http://weblogs.asp.net/scottgu/ "Scott Guthrie") recent
[roadmap for
MVC](http://weblogs.asp.net/scottgu/archive/2008/02/12/asp-net-mvc-framework-road-map-update.aspx "ASP.NET MVC Framework RoadMap")
in which he mentioned we would be making the source available. At Mix,
Scottha announced that we would be deploying our source to
[CodePlex](http://www.codeplex.com/ "CodePlex") soon.

Not only that, we hope to push source from our source control to a
CodePlex Project’s source control server on a semi-regular basis. These
builds would only include source (in a buildable form) and would not
include the usual hoopla with associated with a full Preview or Beta
release.

How *regular* a schedule we keep to remains to be seen, but Scott
mentioned in his talk around every four to six weeks. Secretly, between
you and me, I’d love to push even more regularly. Just keep in mind that
this is an experiment in transparency here at Microsoft, so we’ll start
slow (baby steps!) and see how it progresses. In spirit, this would be
the equivalent to the daily builds that are common in open source
projects, just not daily.

### Unit Test Framework Integration

In a recent post, I highlighted some of the work we’re doing around
[integrating third party unit testing
frameworks](https://haacked.com/archive/2008/02/12/asp.net-mvc-update.aspx "ASP.NET MVC Update").

![Unit Testing
Frameworks](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ASP.NETMVCUpdate_C69C/UnitTestingFrameworks_3.png)

I’ve been in contact with various unit testing framework developers
about integrating their frameworks with the MVC project template. I’m
happy to see that [MbUnit released updated
installers](http://weblogs.asp.net/astopford/archive/2008/03/10/microsoft-mvc-and-mbunit.aspx "MbUnit and MVC")
that will integrate [MbUnit](http://mbunit.com/ "MbUnit") into this
dropdown. Hopefully the others will follow suit soon.

One interesting approach I should point out is that this is a great way
to integrate your own totally tricked out unit testing project template
complete with your favorite mock framework and your own private assembly
full of your useful unit test helper classes and methods.

If you’re interested in building your own customized test framework
project which will show up in this dropdown, Joe Cartano of the Web
Tools team posted an updated [streamlined
walkthrough](http://blogs.msdn.com/webdevtools/archive/2008/03/06/asp-net-mvc-test-framework-integration-demo.aspx "ASP.NET MVC Test Framework Integration Walkthrough")
on how to do so using [NUnit](http://nunit.com/ "NUnit") and [Rhino
Mocks](http://www.ayende.com/projects/rhino-mocks.aspx "Rhino Mocks") as
an example.

### Upcoming improvements

One area in this preview we need to improve is the testability of the
framework. The entire MVC dev team had a pause in which we performed
some app building and uncovered some of the same problems being reported
in various forums. Problems such as testing controller actions in which
a call to `RedirectToAction` is made. Other problems include the fact
that you need to mock `ControllerContext` even if you’re not using it
within the action. There are many others that have been reported by
various people in the community and we are listening. We ourselves have
encountered many of them and definitely want to address them.

Experiencing the pain ourselves is very important to understanding how
we should fix these issues. One valuable lesson I learned is that **a
framework that is testable *does not mean* that applications built with
that framework are testable**. We definitely have to keep that in mind
as we move forward.

To that end, we’ll be applying some suggested improvements in upcoming
releases that address these problems. One particular refactoring I’m
excited about is ways to make the controller class itself more
lightweight. Some of us are still recovering from Mix so as we move
forward, I hope to provide even more details on specifically what we
hope to do rather than this hand-waving approach. Bear with me.

During this next phase, I personally hope to have more time to
continuously do app building to make sure these sort of testing problems
don’t crop up again. For the ones that are out there, I take
responsibility and apologize. I am a big a fan of TDD as anyone and I
hate making life difficult for my brethren. ;)

### RTM

When do we RTM? This is probably the most asked question I get and right
now, I don’t have a good answer. In part, because I’m still trying to
sort through massive amounts of feedback regarding this question. Some
questions I’ve been asking various people revolve around the very notion
of RTM for a project like this :

-   How important is RTM to you?
-   Right now, the license allows you to go-live and we’ll provide the
    source, is that good enough for your needs?
-   Is it really RTM that you want, or is it the assurance that the
    framework won’t churn so much?
-   What if we told you what areas are stable and which areas will
    undergo churn, would you still need the RTM label?
-   Is there a question I should be asking regarding this that I’m not?
    ;)

I do hope to have a more concrete answer soon based on this feedback. In
general, what I have been hearing from people thus far is they would
like to see an RTM release sooner rather than later, even if it is not
feature rich. I look forward to hearing from more people on this.

### Closing Thoughts

In general, we have received an enormous amount of interest and feedback
in this project. Please do keep it coming as the constructive feedback
is really shaping the project. Tell us what you like as well as what is
causing you problems. We might not respond to every single thing
reported as quickly as we would like to, but we are involved in the
forums and I am still trying to working through the massive list of
emails accrued during Mix.

