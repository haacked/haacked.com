---
title: An Improved Plugin for Using w.bloggar with RSS Bandit
date: 2004-12-04 -0800
disqus_identifier: 1697
tags: []
redirect_from: "/archive/2004/12/03/an-improved-plugin-for-using-wbloggar-with-rss-bandit.aspx/"
---

Recently I received some [kind words from Scott
Reynolds](http://www.scottcreynolds.com/PermaLink.aspx?guid=a5cfb397-c353-4b50-a68c-5617a1bc7bdb)
regarding my "[excellent
tutorial](https://haacked.com/archive/2004/06/19/651.aspx)" on writing
IBlogExtensions for RSS Bandit. Using my tutorial, he wrote a plug-in to
send items in RSS Bandit to One Note. (There's another plug-in that does
the same thing
[here](http://www.furrygoat.com/2004/06/onenote_sp1_man.html)).

The same day I noticed [this
post](http://channel9.msdn.com/ShowPost.aspx?PostID=31137#31137) on
[Channel9](http://channel9.msdn.com/) in which a user wishes that using
the "Blog This" plug-in that comes with RSS Bandit wouldn't paste the
entire text of the entry into w.bloggar. Instead, this user would prefer
just the link to the entry.

Well, inspired by the kind words, I figured I could do this in a few
minutes. So I built a new version of the IBlogExtension plugin that
comes with RSS Bandit. This plug-in is configurable to allow posting the
full text, the link only, or some generic text with the link. The
generic text is something along the lines of

> {Author} wrote this interesting post entitled "{entry title}" on
> {title of blog}.

The {entry title} is a link to the actual post while the {title of blog}
is a link to the main blog. If some of this information is not available
in the feed, the text is even more generic.

You can grab the [plug-in dll
here](https://haacked.com/code/BlogThisUsingWBloggarPlugin.zip). Just
copy it to the plugins directory within the RSS Bandit installation
directory. For me, that's in "c:\\Program Files\\RssBandit\\plugins".
You might want to back up the old plug-in before overwriting it with
this new one.

The Visual Studio.NET 2003 C\# project file is also [available
here](https://haacked.com/code/BlogThisUsingWBloggarPluginProject.zip).

The plug-in makes use of embedded XSLT files for each type of post. In
the future, it would be quite easy to allow user defined XSLT files that
are not embedded. Please let me know if you find this useful. I'll see
if [Dare](http://www.25hoursaday.com/weblog/) and
[Torsten](http://www.rendelmann.info/blog/) would like to include this
in future releases of RSS Bandit. If you make improvements, please send
them to me so I may update my files.

[Listening to: Feel So Good - Jamiroquai - A Funk Odyssey (5:21)]

