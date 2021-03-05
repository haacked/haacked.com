---
title: MySpace China Blogs Powered By Subtext
tags: [subtext,blogging]
redirect_from: "/archive/2007/10/27/subtext-powers-myspace-china-blogs.aspx/"
---

![MySpace China
Logo](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/NihaoMySpaceChinaHowisSubtextWorkingOutF_6BA/logo_3.png)An
undisclosed source informed me that [MySpace
China](http://myspace.cn/ "MySpace China") is using a modified version
of Subtext for
its blogging engine.

I had to check it out for myself to confirm it and it is true! Check out
my first [MySpace China blog
post](http://blog.myspace.cn/1304049400/archive/2007/10/29/400051092.aspx "Ni Hao Ma!").
How do I know for a fact that this is running on Subtext? I just viewed
source and saw this little bit of javascript...

> `var subtextBlogInfo = new blogInfo('/', '/1304049400/');`

So if anyone is wondering if Subtext can scale, it sure can. MySpace
China gets around 100 million page views, approximately a million of
which go to the blog.

My source tells me the MySpace China developers found some bugs with
Subtext they had to fix that were only exposed when they put a huge load
on it. Although they are under no requirements to do so under [our
license](http://subtextproject.com/Home/Docs/About/License/tabid/110/Default.aspx "Subtext License"),
I hope that they would contribute those fixes as
[patches](http://sourceforge.net/tracker/?group_id=137896&atid=739981 "Subtext Patches on SourceForge")
back to Subtext.

So to all you Chinese users of Subtext (via MySpace China), 你好 to you.

