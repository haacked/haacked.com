---
title: ASP.NET MVC 3 Release Candidate
tags: [aspnet,aspnetmvc,code,nuget]
redirect_from: "/archive/2010/11/08/asp-net-mvc-3-release-candidate.aspx/"
---

Today we’re releasing the release candidate for ASP.NET MVC 3. We’re in
the home stretch now so it’ll mostly be bug fixes and small tweaks from
here on out.

There are two ways to install ASP.NET MVC 3:

-   [Via the Web Platform Installer (Web
    PI)](http://www.microsoft.com/web/gallery/install.aspx?appid=MVC3 "Install MVC 3 via Web PI")
-   Or by [downloading the installer
    directly](http://go.microsoft.com/fwlink/?LinkID=191797 "Download the MVC 3 installer")

Also, be sure to check out the [ASP.NET MVC 3 web
page](http://www.asp.net/mvc/mvc3 "ASP.NET MVC 3") for information and
content about ASP.NET MVC 3 as well as the [release notes for this
release](http://www.asp.net/learn/whitepapers/mvc3-release-notes "ASP.NET MVC 3 RC Release Notes").

Also, don’t miss Scott Guthrie’s [blog post on ASP.NET MVC
3](http://weblogs.asp.net/scottgu/archive/2010/11/09/announcing-the-asp-net-mvc-3-release-candidate.aspx "Announcing Release Candidate of ASP.NET MVC 3")
which provides the usual level of detail on the release.

Razor Intellisense. Ah Yeah!
----------------------------

Probably the most frequently asked question I received when we released
the Beta of ASP.NET MVC 3 was “When are we going to get Intellisense for
Razor?” Well I’m happy to say the answer to that question is **right
now**!

Not only Intellisense, but syntax highlighting and colorization also
works for Razor views. ScottGu’s blog post I mentioned earlier has some
screenshots of the Intellisense in action as well as details on some of
the other improvements included in ASP.NET MVC 3 RC.

NuGet
-----

As [I wrote
earlier](https://haacked.com/archive/2010/11/09/nuget-ctp2-released.aspx "NuGet CTP 2 released"),
this release of ASP.NET MVC includes an updated version of NuGet, a free
and open source Package Manager that integrates nicely into Visual
Studio.

What’s Next?
------------

Well if all goes well, we’ll land this plane nicely with an RTM release,
and then it’s time to start thinking about ASP.NET MVC 4. There, I said
it. Well, actually, I should probably already be thinking about 4, but
seriously, can’t a guy catch a break once in a while to breathe for a
moment?

Well, since I’m lazy, I’ll probably be asking you very soon for your
thoughts on what you’d like to see us focus on for the next version of
ASP.NET MVC. Then I can present your best ideas as my own in the next
executive review. You don’t mind that at all, do you? ![Winking
smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ASP.NET-MVC-3-Release-Candidate_955/wlEmoticon-winkingsmile_2.png)

Seriously though, please do provide feedback and I’ll keep you posted on
our planning.

Now that we have Nuget in place, one thing we’ll be focusing on is
looking at building packages for features that we would have liked to
include in ASP.NET MVC, but maybe didn’t have the time to implement
them. Or perhaps simply for experimental features that we’d like
feedback on. I think building NuGet packages will be a great way to try
out new feature ideas and for the ones we think belong in the product,
we can always roll them into ASP.NET MVC core.

