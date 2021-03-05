---
title: A Few Questions For Subtext Users
tags: [subtext]
redirect_from: "/archive/2006/09/14/A_Few_Questions_For_Subtext_Users.aspx/"
---

![Subtext
Logo](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/AFewQuestionsForSubtextUsers_194/SubtextLogo6.png)
If you are using Subtext, or are using .TEXT and plan to use Subtext I
need to ask you a few questions.  Please answer as your answers may
determine whether or not some features are **removed** for the sake of
simplification.

These questions revolve around the **Advanced Options** when creating or
editing a post in the admin tool.

**1. Do you use the Title URL field?**

Now before you answer, let me explain what this field is used for. You
might have values in that column, but that doesn’t necessarily mean it
is in use. 

The **Title Url** field is used to specify an ALTERNATE URL for the
title of a blog post.  Ordinarily, the title of the blog post links to
the blog post itself.  Older versions of .TEXT and Subtext would update
that field with the URL to the blog post, which was unnecessary since we
could generate that URL on the fly.

In my humble opinion, it is a bad idea to have the title of a blog post
link elsewhere as it is confusing to users. Unless there are large
numbers of users who have specific needs for this feature, I would like
to remove it.

**2. Do you ever enter values for Source Name and Source Url for a blog
post?**

[Scott Watermasysk](http://scottwater.com/blog/), the original creator
of .TEXT (*Subtext, I am your father*) graciously pointed out the use of
the `Source` and `SourceUrl` fields in the comments of this post. These
are used for the optional RSS `<source>` element. It’s for properly
attributing credit for a link when republishing a post from somewhere
else (see the [RSS
spec](http://blogs.law.harvard.edu/tech/rss#ltsourcegtSubelementOfLtitemgt "RSS 2.0 Specification")).
I’ve never seen any aggregators make use of this unfortunately and most
people simply attribute others in the body of the post, so it is still a
candidate for removal if nobody makes use of it. ~~As far as I can tell,
these fields are intended for comments, not for blog posts.  However the
admin section does have text fields for entering these values.  But~~
These values are NEVER displayed for blog posts.

I’m 99.9% sure I’ll be removing these fields for blog posts so in part,
this question is a warning.  However if someone has an extremely
compelling reason to keep them for blog posts, speak up now or forever
hold your peace.

**3. Would you find the ability to run Subtext off of another database
such as MySql or Firebird very important?**

I know some users might like to save a few bucks and go with .NET and
MySql hosting.  I’ve thought about implementing multiple database
support, but don’t want to undertake such a big task if there is no
demand. 

Even if there is demand, it’d have to be overwhelming for me to consider
doing it sooner rather than later.

**Thanks!** That’s all for now. I appreciate your responses.

