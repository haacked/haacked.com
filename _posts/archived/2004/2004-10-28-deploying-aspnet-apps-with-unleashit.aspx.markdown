---
title: Deploying ASP.NET Apps With UnleashIt
tags: [aspnet]
redirect_from: "/archive/2004/10/27/deploying-aspnet-apps-with-unleashit.aspx/"
---

I answered a question about ASP.NET deployment in a
[newsgroup](http://msdn.microsoft.com/newsgroups/default.aspx?dg=microsoft.public.dotnet.framework.aspnet)
recently where the person asked which files should he deploy when moving
his site to a production server.

As a followup to my answer, [Jon
Galloway](http://weblogs.asp.net/jgalloway/) pointed the person to a
neat deployment utility called
[UnleashIt](http://www.eworldui.net/UnleashIt/).

![UNLEASHit](/assets/images/UnleashIt.jpg) \
Ready to deploy, Sir!

UnleashIt provides integration with VS.NET 2003 as an add-in. You can
create deployment profiles and share them with other team members. I
plan to use this for any customization of my .TEXT blog I plan to do.

So why not just use Visual Studio's copy project option? I've never used
it but Jon had this to say:

> Visual Studio has a copy project option for web projects, but it
> depends on your setup and you may miss files (javascript, css,
> images).

As usual, I have a few minor complaints as I'm just a nitpicky person.
The first is that the application is not resizeable. The fonts on the
main screen seem smaller than in other applications.

More problematic is that the application doesn't seem to support adding
file masks. Currently the application is missing \*.asmx and \*.ashx,
but more importantly it would be nice to create a deployment profile
using this tool that could handle Word docs (for example) if they were a
part of the site.

