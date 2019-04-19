---
title: NullReferenceException When Deploying DotNetNuke Could Be An ObjectQualifier
  Discrepancy
date: 2006-05-17 -0800
tags: [dotnetnuke]
redirect_from: "/archive/2006/05/16/NullReferenceExceptionWhenDeployingDotNetNukeCouldBeAnObjectQualifierDiscrepancy.aspx/"
---

When starting a new DotNetNuke based website, I like to develop it on my
local machine, and when everything is ready for a first deployment, I
deploy to whatever staging or production server is relevant.

This has worked fine over the years, but I ran into a problem recently
when applying this approach to DNN 4.03. I had everything working just
fine on my local machine, but after deploying to our production server,
I could not get the site to work. It would give me some message about a
`NullReferenceException` when trying to get the portal.

Opening up Query Analyzer, I could select the records from the
`dnn_PortalAlias` table and see that everything matched up. I banged my
head on this for a long time.

I finally had the idea to change the connection string to point to a
brand new database. I thought maybe I would find some discrepancy in the
database records. Perhaps I deleted something or other important. After
the change, I hit the site which invoked the web-based installation
process. Once that was complete I tried to get a list of records from
dnn_PortalAlias and got an error message
`Invalid object name 'dnn_PortalAlias'`. Huh?

Executing `sp_tables` showed there was no *dnn_PortalAlias* table.
Instead, there was a *PortalAlias* table. Aha! I looked in web.config
and indeed the `ObjectQualifier` value was set to the empty string. So
how did that change from my development machine to the production
machine?

Well the source zip archive for DNN 4.0 ships with two config files. One
named *development.config* and one named *release.config*. Before
deploying, you are supposed to rename *release.config* to *web.config*.
However, I had assumed that on my local machine, I could simply rename
*development.config* to web.config for development purposes. I assumed
that the only differences were in some debug settings. Boy was I wrong!

It turns out that the `ObjectQualifier` setting was set to *dnn_* in
*development.config*. This is the value I would expect as this was the
typical installation I used in previous versions. In any case, I hope
this saves you time if you happen to run into it. The fix on my
production server was simply to change the ObjectQualifier value to be
*dnn_*.

