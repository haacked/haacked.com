---
title: Database Maintenance Of Your Blog
date: 2006-02-06 -0800
tags: [sql,dba]
redirect_from: "/archive/2006/02/05/databasemaintenanceofyourblog.aspx/"
---

Lately I have been spending a little bit of time performing maintenance
tasks on my blog’s SQL Server. I noticed that OdeToCode’s [Scott
Allen](http://odetocode.com/Blogs/scott/ "Scott Allen's Blog") was in
[the same
mood](http://odetocode.com/Blogs/scott/archive/2006/02/06/2839.aspx "Care and Feeding of Community Server").

In looking to free up some database space, he took the somewhat drastic
step of deleting all referrals and urls before a certain date. Since he
doesn’t care about this data, it isn’t really all that drastic. But it
makes a data packrat like me shudder. I wanted to free up some space as
well so I created an approach that frees up a lot of space, but keeps
the data I care about.

Around 90% to 99% of my referrals are from web searches and online blog
readers. As a matter of fact, nearly all of these are from Google. Since
those referrals are not that important to me as
[WebHost4Life](http://webhost4life.com/ "WebHost4Life Hosting Company Website")
also tracks that data, I wrote a script to delete them for Subtext. Note
that the following SQL script is pretty aggressive, so use at your own
risk. You might even think of some search strings that I missed.

```sql
DELETE FROM subtext_Referrals
WHERE UrlID IN
(
  SELECT UrlID

  FROM subtext_URLs
   WHERE Url LIKE 'http://google.%'
   OR Url LIKE 'http://%.yahoo.%'
   OR Url LIKE 'http://yahoo.%'
   OR Url LIKE '%/Search/%'
   OR Url LIKE '%/Search?%'
   OR Url LIKE 'http://search.%'
    OR Url LIKE 'http://bloglines.%'
)

DELETE FROM subtext_URLs
WHERE Url LIKE 'http://google.%'
  OR Url LIKE 'http://%.yahoo.%'
  OR Url LIKE 'http://yahoo.%'
  OR Url LIKE '%/Search/%'
  OR Url LIKE '%/Search?%'
  OR Url LIKE 'http://search.%'
  OR Url LIKE 'http://bloglines.%'
```

I then ran the same commands that Scott did after reading his post.

```sql
DBCC DBREINDEX(subtext_URLs)
DBCC DBREINDEX(subtext_Referrals)
DBCC SHRINKDATABASE(SubtextData)
```

In order to run those commands on .TEXT, just replace the “subtext_” prefix with “blog_” and you are set.

Now I haven’t tested this, but I imagine the corresponding script for
Community Server would be the following based on its published schema.

```sql
DELETE FROM cs_Referrals
WHERE UrlID IN
(
  SELECT UrlID FROM cs_Urls
  WHERE Url LIKE 'http://google.%'
    OR Url LIKE 'http://%.yahoo.%'
    OR Url LIKE 'http://yahoo.%'
    OR UrlLIKE '%/Search/%'
    OR Url LIKE '%/Search?%'
    OR Url LIKE 'http://search.%'
    OR Url LIKE 'http://bloglines.%'
)

DELETE FROM cs_Urls
WHERE Url LIKE 'http://google.%'
  OR Url LIKE 'http://%.yahoo.%'
  OR Url LIKE 'http://yahoo.%'
  OR UrlLIKE '%/Search/%'
  OR UrlLIKE '%/Search?%'
  OR UrlLIKE 'http://search.%'
  OR UrlLIKE 'http://bloglines.%'
```