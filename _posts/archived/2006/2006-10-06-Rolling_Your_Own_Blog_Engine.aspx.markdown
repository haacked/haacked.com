---
title: Rolling Your Own Blog Engine
tags: [meta]
redirect_from: "/archive/2006/10/05/Rolling_Your_Own_Blog_Engine.aspx/"
---

![Hello
World](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WhyRollYourOwnBlogEngine_FAC2/HelloWorld5.jpg)

[Jeff Atwood](http://codinghorror.com/blog/ "Coding Horror") asks [the
question](http://www.codinghorror.com/blog/archives/000696.html "On Frameworkitis")
in a recent post if writing your own blog software is a form of
procrastination (no, blogging is).

I remember reading something where someone equated rolling your own blog
engine is the modern day equivalent of the *Hello World* program.  I
wish I could remember where I heard that so I can give proper credit.
UPDATE: [Kent
Sharkey](http://www.acmebinary.com/blogs/kent/ "Kent Sharkey") reminds
me that I [read it on his
blog](http://www.acmebinary.com/blogs/kent/archive/2006/09/04/811.aspx "AlexBarn has left the building").
It was a quote from [Scott
Wigart](http://blog.swigartconsulting.com/ "Tech Blender"). Thanks for
the memory refresh Kent!

Obviously, as an Open Source project founder building a blog engine, I
have a biased opinion on this topic (*I can own up to that*).  My
feeling is that for most cases (not all) rolling your own blog engine is
a waste of time given that there are several good open source blog
engines such as [Dasblog](http://www.dasblog.net/ "DasBlog"),
[SUB](http://codeplex.com/Wiki/View.aspx?ProjectName=SUB "Single User Blog"),
and [Subtext](http://subtextproject.com/ "Subtext Project Site").

It isn’t so much that writing a rudimentary blog engine is hard.  It
isn’t.  To get a basic blog engine up and running is quite easy. 
The challenge lies in going beyond that basic engine.

The common complaint with these existing solutions (and motivation for
rolling your own) is that they contain more features than a person
needs.  Agreed.  There’s no way a blog engine designed for mass
consumption is going to only have the features needed by any given
individual.

However, there are a lot of features these blog engines support that you
wouldn’t realize you want or need till you get your own engine up and
running.  And in implementing these common features, a developer can
spend a lot of time playing catch-up by reinventing the kitchen sink. 
Who has that kind of time?

**Why reinvent the sink, when the sink is there for the taking?**

For example, let’s look at fighting comment spam.

Implementing comments on a blog is quite easy. But then you go live with
your blog and suddenly you’re overwhelmed with insurance offers. 
Implementing comments is easy, implementing it well takes more time.

If you are going to roll your own blog engine, at least “steal” the
Subtext Akismet API library in our [Subversion
repository](http://subtextproject.com/Home/About/ViewTheCode/tabid/116/Default.aspx "View The Source Code For Subtext"). 
[Dasblog
did](http://flimflan.com/blog/AkismetSupportInDasBlog.aspx "Akismet Support In Dasblog"). 
However, even with that library, you still ought to build a UI for
reporting false positives and false negatives back to Akismet etc... 
Again, not difficult, but it is time consuming and it has already been
done before.

Some other features that modern blog engines provide that you might not
have thought about (not all are supported by Subtext yet, but by at
least one of the blogs I mentioned):

- [RFC3229 with
  Feeds](http://bobwyman.pubsub.com/main/2004/09/using_rfc3229_w.html "RFC3229 For Feeds")
- [BlogML](http://www.codeplex.com/Wiki/View.aspx?ProjectName=BlogML "BlogML")
  - So you can get your posts in there.
- Email to Weblog
- [Gravatars](http://gravatar.com/ "Gravatars")
- Multiple Blog Support (more useful than you think)
- Timezone Handling (for servers in other timezone)
- Windows Live Writer support
- Metablog API
- Trackbacks/Pingbacks
- Search
- Easy Installation and Upgrade
- XHTML Compliance
- Live Comment Preview

My point isn’t necessarily to dissuade developers from rolling their own
blog engine.  It’s fun code to write, I admit.  My point is really this
(actually two points):

​1. If you plan to write your own blog engine, take a good hard look at
the code for existing Open Source blog engines and ask yourself if your
needs wouldn’t be better served by contributing to one of these
projects.  They could use your help and it gets you a lot of features
for free. Just don’t use the ones you don’t need.

[![Jerry
Maguire](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WhyRollYourOwnBlogEngine_FAC2/jerry00_thumb.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/WhyRollYourOwnBlogEngine_FAC2/jerry002.jpg)
2. If you still want to write your own, at least take a look at the code
contained in these projects and try to avail yourself of the gems
contained therein.  It’ll help you keep your wheel reinventions to a
minimum.

That’s all I’m trying to say.  Help us... help you.

