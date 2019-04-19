---
title: New NuGet.org Deployed!
date: 2011-12-05 -0800 9:00 AM
tags: [nuget,code]
redirect_from: "/archive/2011/12/04/nuget_org_update.aspx/"
---

So my [last day at
Microsoft](https://haacked.com/archive/2011/12/05/last-day-at-microsoft.aspx "Last day at Microsoft")
ended up being a very long one as the
[NuGet](http://nuget.org/ "NuGet Gallery") team worked late into the
evening to deployan updated version of NuGet.org. I’m very happy to be a
part of this as my last act as a Microsoft employee. This is complete
re-write of the gallery.

Why a rewrite? We’ve learned a lot since we first launched, and our
needs have evolved to the point where a rewrite made sense. The new
implementation is a vanilla ASP.NET MVC 3 application and highly
optimized to be a gallery with just the features we need.

For example, we made extensive use of [Mvc Mini
Profiler](http://code.google.com/p/mvc-mini-profiler/ "MVC Mini Profiler")
to ensure pages made the least number of database queries as necessary.
Also, the site is now hosted in Azure!

What’s in this new implementation?
----------------------------------

There’s a lot of great improvements. I won’t provide a comprehensive
list, but I will provide a taste. Matthew and others will write about
the improvements in more detail:

-   **Search on every page!** This seems obvious, but we didn’t have
    this in the old gallery. That deficiency is now just a bad memory.
    Also, the search is way faster!
-   **Package owners are displayed more prominently.** In the old
    gallery, the owners of the package weren’t displayed. Anywhere.
    Which was a terrible experience because the owners are the people
    who matter. A package owner is associated with an account. The
    “author” of a package is simply metadata and could be anyone.
-   **Owner profiles.** Click on a package owner to see the package
    owner’s profile. Today, the only thing you see is a
    [gravatar](http://gravatar.com) for the owner and the list of
    packages that person owns. In the future, we might include more
    profile information.
-   **Adding a package owner requires acceptance.** In the past, you
    could add anyone else as an owner of your package and they’d
    immediately become an owner of a package. Now that we show the list
    of owners next to a package, that’s not such a good thing. In the
    new gallery, when you try and add an owner, the gallery sends them
    an email inviting them to become an owner. This way
    *MyCrappyPackage* can’t add you as an owner as a way of boosting
    their reputation at the expense of yours.
-   **Package stats are displayed more prominently.** We wanted to make
    the package stats very prominent.
-   **Package unlisting.** Packages can now be unlisted. This
    effectively hides the package, but the package is still used to
    resolve dependencies.
-   **Cleaner markup and design.** The HTML markup is way cleaner and
    streamlined. For example, we reduced the CSS files from 20 to 1.
-   **Cleaner URLs.**For example, the new package feed URL is now
    [http://nuget.org/api/v1/](http://nuget.org/api/v1/). In the future,
    we’ll probably use content negotiation so we won’t even need
    versioned URLs for the package feed. The NuGet 1.5 client will
    continue to work.
-   **And it’s WAY FASTER!** I almost forgot to mention just how much
    faster the gallery is now than before.

What about NuGet 1.6?
---------------------

There are some features of the Gallery you won’t see until we release
NuGet 1.6. We want to make sure the site works well before we deploy
NuGet 1.6. Once we do that, you’ll also see support for SemVer (Semantic
Versioning) and Prerelease packages in the Gallery.

