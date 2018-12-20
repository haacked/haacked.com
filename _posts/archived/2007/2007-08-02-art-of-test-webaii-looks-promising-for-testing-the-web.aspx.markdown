---
title: Art Of Test WebAii Looks Promising For Testing The Web
date: 2007-08-02 -0800
disqus_identifier: 18377
tags: [tdd]
redirect_from: "/archive/2007/08/01/art-of-test-webaii-looks-promising-for-testing-the-web.aspx/"
---

A coworker of mine ran into some problems using
[WATIN](http://watin.sourceforge.net/ "WATIN web app testing") to test
our website. Specifically issues with Javascript and AJAX. Yes, I know
there’s [Selenium](http://www.openqa.org/selenium/ "Selenium") out
there, but I hoped for something we could run from within NUnit/MbUnit,
so that it’s nicely and easily integrated into our build process and
development environment.

He did a bit of digging and found this free web automation
infrastructure product by [Art Of Test
Inc.](http://www.artoftest.com/ "Art of Test") called
[WebAii](http://www.artoftest.com/Products.aspx "WebAii Web Testing") (they
also have [a blog](http://artoftestinc.blogspot.com/ "ArtOfTest Blog")).
It seems to do everything we want and more. Check out some of these
features (the full feature list is much longer).

-   Supports IE and Firefox
-   Supports DOM actions and pure UI Mouse/Keyboard actions for Ajax
    Testing
-   For ASP.NET testing, you can use an In-Process Host for fast
    browser-less testing without a webserver
-   Use ASP.NET Development Server (aka Cassini) to run tests without
    IIS
-   Use name, id, xpath identifications. (*Need to see if they support
    Regex too*)
-   Unit test your JavaScript by calling JS methods from your .NET code.
-   Extensions for NUnit and VS Team Test (*I’ll ping them to build one
    for MbUnit*)

These are just a small sampling of the many features. It sounds like
this would be a killer web testing app. Does anyone have experience with
it?

We’re going to evaluate it and I’ll keep you posted, but I thought I’d
mention it here because it sounds great.

**Features I’d Like To See In A Web Unit Testing Tool**

-   Ability to set the UserAgent and Referrer
-   Ability to set request headers
-   Ability to hook into and verify the status of a redirect. For
    example, if a page redirects, I want to be able to assert that the
    HTTP Status code for the redirect

