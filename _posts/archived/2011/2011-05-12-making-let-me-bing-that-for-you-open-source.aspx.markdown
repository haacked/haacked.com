---
title: Making Let Me Bing That For You Open Source
date: 2011-05-12 -0800
disqus_identifier: 18788
tags:
- oss
- personal
redirect_from: "/archive/2011/05/11/making-let-me-bing-that-for-you-open-source.aspx/"
---

Almost two years ago, I [announced the
launch](https://haacked.com/archive/2009/10/16/announcing-let-me-bing-that-for-you.aspx "Announcing LetMeBingThatForYou.com")
of [http://letmebingthatforyou.com/](http://letmebingthatforyou.com/), a
blatant and obvious rip-off of the [Let me Google that for
you](http://lmgtfy.com/) website.

The initial site was created by [Maarten
Balliauw](http://blog.maartenballiauw.be/ "Maarten's Blog") and [Juliën
Hanssens](http://hanssens.org/ "Juliën's Website") in response to a call
for help I made. It was just something we did for fun. I’ve been
maintaining the site privately always intending to spend some time to
refresh the code and open source it.

Just recently, I upgraded the site to ASP.NET MVC 3, refactored a bunch
of code, and moved the site to
[AppHarbor](http://appharbor.com/ "AppHarbor").

Why AppHarbor?

I’ve heard such good things about how easy it is to deploy to AppHarbor
so I wanted to try it out firsthand myself, and this small little
project seemed like a perfect fit.

I had been working on the code in a private Mercurial repository so it
was trivially easy for me to push it to a [BitBucket
repository](https://bitbucket.org/haacked/letmebingthatforyou/ "LMBTFY BitBucket").
From there it’s really easy to integrate the [BitBucket account with
AppHarbor](http://support.appharbor.com/kb/getting-started/using-mercurial-on-appharbor "Using BitBucket with AppHarbor").

So now, my deployment workflow is really easy when working on this
simple project:

1.  Make some changes and commit them into my local HG (Mercurial)
    repository. I have my local repository syncing to all my machines
    using Live Mesh.
2.  At some point, when I’m ready to publish the changes, I run the hg
    push command on my repository.
3.  That’s it! AppHarbor builds my project and if all the unit tests
    pass, it deploys it live.

I’m not planning to spend a lot of time on Let Me Bing That For You.
It’s just a fun little side project that allows me to play around with
ASP.NET MVC 3, jQuery, etc. If you want to look at the source, or
contribute a patch, **[check it out on
BitBucket](https://bitbucket.org/haacked/letmebingthatforyou/ "LetMeBingThatForYou on BitBucket")**.

