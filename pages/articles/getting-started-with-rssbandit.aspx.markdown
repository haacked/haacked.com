---
title: "Getting Started With RssBandit"
permalink: /articles/getting-started-with-rssbandit.aspx
---

Getting Started With RSSBandit

A beginners guide to RSS feed aggregation.

Phil Haack
February 2004

**Summary:** Outlines the basic steps to getting started with RSSBandit.
Also downloadable as a [Word
document](https://haacked.com/GettingStartedWithRssBandit.zip).

Introduction
============

So what exactly is a blog?  And what does a blog have to do with RSS? 
And now that you mention it, what is RSS?  Slow down there fella. 

Blogs have been getting a lot of press lately due to their adoption by
various campaigns during this election year.  A Blog is simply a weblog,
an online diary. Nothing more, Nothing less.  For an example of a blog
written by a simpleton, visit [http://haack.org/](http://haack.org/).

The reason you read about blogs in Doonesbury is due to the plethora of
tools out there that make it very easy to publish every interesting or
inane thought in you head to the World Wide Web.  A Japanese hip-hopper
can boot up a browser and find out what type of meatloaf Sally in Omaha
cooked for her family last night.  Average citizens can deem themselves
journalists and publish un-edited reports of the events near them.  This
can be very good or bad depending on whether the journalist said
something nice about you.

As the presence of blogs proliferates, how do you keep track of them
all?  One option is to keep a list in and check every one of them each
day to see if something new has been published.  That hardly seems
efficient.

That’s where RSS comes into the picture.  If you took the time to visit
my blog, you’ll notice a big orange “XML” link in the left hand side. 
That’s my RSS feed.

RSS is an XML syntax for syndicating content.  In English, this means
that RSS is used to describe the content that one is publishing.  For
example, I may write a blog entry like so:

Today I sat in front of the computer all day.

Now if you want to know whether or not you’ve already read this entry,
it would be nice to know some information about this specific entry,
such as its title, when the entry was made, etc…

RSS is a format for marking up a news item or blog entry with such
information and it looks a little something like this (note: not
exactly):

```xml
<?xml version="1.0" encoding="utf-8" ?>
<item>
  <title>My Exciting Day</title>
  <link>http://haack.org/</link>
  <description> </description>
  <date>2004-02-08</date>
</item>
```
 

Now to the untrained eye, this mark-up is quite unsightly.  The actual
RSS format is even uglier, and that’s OK because it isn’t intended for
the untrained eye.  It’s intended to be read by other computers.

“Huh?  You mean my computer is interested in reading about Sally’s
Meatloaf?”

Not quite.  The Japanese hip-hopper is still interested in reading about
Sally, but rather than checking in on Sally’s blog every day, he uses an
RSS Aggregator (sometimes called an RSS Reader or News Aggregator) to
aggregate all the various blogs to which he subscribes.  It’s the
aggregator that reads the unslightly mark-up and will notify our
fearless hip-hopper that Sally’s cooked up another fantastic dish.

For more information about RSS, please see the following URL:
[http://www.mnot.net/RSS/tutorial/](http://www.mnot.net/RSS/tutorial/).

So where can you get your hands on such a program?  Visit the following
link
([http://sourceforge.net/project/showfiles.php?group\_id=96589](http://sourceforge.net/project/showfiles.php?group_id=96589)).
 You’ll see a list of the various versions of RSSBandit.  In general,
select the version with the largest number (usually at the top of the
list) and download the installer.  For example, at the time of this
writing, you would click on the link that says
“[RssBandit1.2.0.90installer.zip](http://prdownloads.sourceforge.net/rssbandit/RssBandit1.2.0.90installer.zip?download)”. 

Before you install RssBandit, make sure that you have the Microsoft .NET
Framework installed.  How?  Go to
[http://windowsupdate.microsoft.com/](http://windowsupdate.microsoft.com/)
and scan for the latest updates.  If an option comes up for Microsoft
.NET, install it. If not, you may already have the framework.

Rss Bandit Application
======================

Overview
--------

Now we get to the whole point of the article.  After you’ve downloaded
and installed RSSBandit, you should see a screen that looks something
like this (results may vary)

![](/assets/images/RssBanditImages/image001.jpg)

On the left is a list of feed subscriptions organized into category
folders.  On the right is a list of headlines in the top pane.  In the
bottom right pane is the actual content of a selected headline. 
RssBandit allows you to customize the layout of the reading pane and
headlines list.

One thing to note, the headlines displayed in the list depend on the
feed category or feed selected in the list of feed subscriptions.  When
a category is selected, the headlines for every feed in that category
(and sub-category) are displayed.  When a single feed is selected, then
only the headlines for that feed are displayed.

At the top, you’ll notice a toolbar with multiple buttons.  Most of
these items are self explanatory, but I’ll delve into them a bit.

![](/assets/images/RssBanditImages/image002.jpg)

Subscribing To Your First Feed
------------------------------

When you install RssBandit, you’ll actually notice that the creators of
RssBandit were kind enough to already include a few feeds.  Feel free to
read them.  There are a couple of feeds devoted to RssBandit in there
along with Wired News (a favorite of mine) and Slashdot.

But if you’re looking for really good content, it’s time to learn to
subscribe to a new feed.  Hey! Let’s try mine!

Step 1: Click on “New Feed”.  This will bring up a dialog for adding new
feeds.

![](/assets/images/RssBanditImages/image003.jpg)

**![](/assets/images/RssBanditImages/image004.jpg)**

Step 2: Paste the text
[https://haacked.com/Rss.aspx](https://haacked.com/Rss.aspx)
into the box labelled with “Url”.  Afterwards, click the button “Get
Title From Feed.”  Rss Bandit will look up the title via the web. 
Finally, select a category and then click “Ok”.  You’ve successfully
subscribed to an Rss Feed of my blog.  If you look in the list of
subscribed feeds, you’ll notice a new entry in Bold.  The (19) is number
of unread items.  Go ahead, read them all!

![](/assets/images/RssBanditImages/image005.jpg)

Setting Preferences For A Feed
------------------------------

By default, items within a feed are held for a limited time only.  Since
I’m producing ground breaking content, you may want to change this
setting for my feed.  Right click on the feed and click on the
properties menu item.

![](/assets/images/RssBanditImages/image006.jpg)

This brings up the “Feed Properties” Dialog box.  Please feel free to
change the Max. Item Age option to Unlimited.  Feed items will never
expire with this setting.  You can also change the Update Frequency as
well.  This setting tells RssBandit how often to check the feed for new
headlines.  Since I’m not producing content every 60 minutes, that is
more than a generous setting.  For others like Yahoo News, you might
even consider checking every half hour.

![](/assets/images/RssBanditImages/image007.jpg)

Discovering New Feeds
---------------------

So by now you’re probably hooked and in dire need of new feeds.  You can
visit the following sites to find new and interesting blogs:
[http://www.2rss.com/](http://www.2rss.com/) (Directory, Software and
Articles about RSS, Portal)
[http://www.blogarama.com/](http://www.blogarama.com/) (yet another blog
directory).

But if you’re like me, most of the feeds you will find will be by word
of mouth.  Or should I say “word of blog”?  You see, most blogs often
have what is called a blog roll.  Basically it is a list of blogs read
by the author of the blog.  Also, many blog entries will refer to other
blogs. 

RssBandit has a very neat feature that will automatically discover RSS
Feeds for these blogs.  Say you’re reading the latest installment of my
riveting blog, and I link to David Winer’s blog.  Look up in the toolbar
of RssBandit and you may notice that there’s an icon with an orange XML
with a \#1 in front of it
![](/assets/images/RssBanditImages/image008.jpg).

Click on it and you’ll see a list of the feeds that RssBandit has
recently discovered.

![](/assets/images/RssBanditImages/image009.jpg)

In the screenshot above, you can see that “Scripting News” was
discovered. Clicking a discovered feed opens the Add New Feed dialog box
with its fields pre-populated with the information needed to subscribe
to the feed.

RssBandit Options
-----------------

Now that you’re well on your way to RSS bliss, you should spend some
time playing around with the various options in RssBandit.  I won’t go
into every option, but I will discuss a couple that I find useful.

### Feed Item Formatting

In the menu bar, select the “Tools” menu and click “Preferences.”  That
will bring up a dialog box with several tabs.  Click on the “Feed Items”
tab and you’ll see the following dialog.

![](/assets/images/RssBanditImages/image010.jpg)

This page allows you to change the format for items within the reading
pane.  By clicking “Use a custom formatter”, you can select various
formatters that have been created for RssBandit.  My personal favorite
is outlook2003-orange.  However, have fun trying out the various
formats.

### Reading Pane Position

RssBandit also allows you to change the position of the reading pane in
relation to the list of headlines. Simply select the “View” menu, and
click the “Reading Pane Position” sub-menu item.  A list of options will
appear to the right. Collect them all!

Conclusion
----------

This article attempts to give you an easy to use guide to getting
started with RssBandit.  Ideally it is clear enough for your techno
phobic parents to get started reading your RSS feed using these
instructions. Whether or not they understand anything you write about,
well that’s another matter.

This article is by no means a thorough document of all of RssBandit’s
features.  However, I do hope to write more in the future about this
wonderful piece of software.

More Information
----------------

For more information, please check out the following sites.

Dare Obasanjo
([http://www.25hoursaday.com/](http://www.25hoursaday.com/)) one of the
creators of RssBandit.

RSS Tutorial
([http://www.mnot.net/RSS/tutorial/](http://www.mnot.net/RSS/tutorial/)).

RssBandit Home Page (http://www.rssbandit.org)

David Winer’s blog, the creator of RSS
([http://www.scripting.com](http://www.scripting.com/))

Windows Update Website
([http://windowsupdate.microsoft.com/](http://windowsupdate.microsoft.com/))

 

