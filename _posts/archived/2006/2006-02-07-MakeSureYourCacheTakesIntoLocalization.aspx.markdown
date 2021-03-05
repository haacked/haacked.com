---
title: Make Sure Your Cache Takes Into Localization
tags: [aspnet,caching,localization]
redirect_from: "/archive/2006/02/06/MakeSureYourCacheTakesIntoLocalization.aspx/"
---

Recently I added some seemingly innocent code to Subtext within the `Application_BeginRequest` method of Global.asax.cs that I adapted from this [blog post by Darren Neimke](http://markitup.com/Posts/Post.aspx?postId=52252561-f83d-4463-82f0-769fce82fd82 "Displaying dates and times in a local users time zone").
The purpose of the code is to provide culture aware formatting of dates,
times, and numbers specific to the user reading the blog.

```csharp
// Set the user culture.  Got the idea from
// http://markitup.com/Posts/Post.aspx?postId=52252561-f83d-4463-82f0-769fce82fd82
// but modified it to catch specific exceptions.
//TODO: Make sure we store dates in UTC etc and do the conversion when we pull them.
//      I assume we do this but haven't checked.
try
{
   if (Request.UserLanguages != null
         && Request.UserLanguages.Length \> 0
         && Request.UserLanguages[0] != null)
   {
      Thread.CurrentThread.CurrentCulture =
CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);
   }
}
catch(NotSupportedException nse)
{
   log.Error("Error while attempting to set CurrentCulture to '" +
Request.UserLanguages[0] + "'", nse);
}
```

Such a simple snippet of code, yet it introduced a couple of bugs, both which can be seen in this screen capture that [Jayson Knight](http://jaysonknight.com/blog/ "Jayson Knight's Blog") kindly sent me.

![Archive Links](https://haacked.com/assets/images/AchiveLinksCapture.Jpg)

The first error, the fact that you see the same month over and over, has
been fixed in our codebase, but I have yet to deploy it to my blog. That
has to do with how different cultures format dates differently when
ordering the month and the day.

The second error is the fact that Jayson is seeing the months in what
appears to be Portugese in the first place. At one time, he saw the
months in Spanish. I started wondering if somehow a thread from the
thread pool with a `CurrentCulture` already set was servicing his
request, but that didn't make any sense since it should be reset for
him.

Then it occurred to me today. Subtext makes extensive use of caching on
multiple levels from data caching to output caching. When I added this
code, I didn’t go through and update all code that caches data to vary
the cache by user language. Doh!

Once again, the law of unintended consequences smacks me upside the
forehead.

UPDATE: [Jon
Galloway](http://weblogs.asp.net/jgalloway/ "Jon Galloway's Blog")
points out that [Scott Hanselman wrote about
this](http://www.hanselman.com/blog/CachingInASPNETVaryByParamMayNeedVaryByHeader.aspx "Cache Vary by param")
very thing recently. I read that post and totally forgot about it when I
made my change. Blogs are good, umm'kay.
