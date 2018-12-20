---
title: Haacked.com Is Back Online
date: 2005-05-08 -0800
tags: [blogging,subtext]
redirect_from: "/archive/2005/05/07/haackedcom-is-back-online.aspx/"
---

If there's one thing I've learned as a professional developer, it is
that "TIP" is bad. Never Test In Production! Unfortunately in my case,
it was past midnight, I was tired, and I had two query analyzer windows
open, one to my local host, and one to my website's database, both with
the same database name

I was testing an installation script that would drop and recreate the
Subtext database, and I just happened to run it in the wrong Query
Analyzer window. You can imagine my distress as I visited my site to
find it pretty much gone.

Fortunately, my [hosting provider](http://webhost4life.com/) takes
regular backups and they had a backup from four days ago. Once the
backup was restored, I went in and carefully recreated four days worth
of blog posts with help from my [RSS Bandit](http://www.rssbandit.org/)
cache. You see, it's [more than
vanity](https://haacked.com/archive/2004/10/08/1322.aspx) that I
subscribe to my own feed.

In case your curious, I used Query Analyzer to reconstruct the posts
since the URLs were generated using the ID column (identity) and the
DateAdded column. With liberal use of the

> DBCC CHECKIDENT ('blog\_content', RESEED, 3073)

command, I recreated the proper IDs so that existing links to these
posts would not break. Unfortunately I lost all comments.

[Listening to: Duke Pearson (With Airto And Stella Mars) / O Amor Em Paz
(Once I Loved) - Blue Note Plays Jobim (5:24)]

