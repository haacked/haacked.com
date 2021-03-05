---
title: RSS Bandit Synchronization Using GMail Drive Shell Extension
tags: [rss]
redirect_from: "/archive/2004/10/26/rss-bandit-synchronization-using-gmail-drive-shell-extension.aspx/"
---

If you haven't heard, RSS Bandit can synchronize its state (feedlist,
read/unread, etc...) across multiple machines. I wrote about it in the
[RSS Bandit docs](http://www.rssbandit.org/docs/).

So far, there are four means for synchronizing feeds: Ftp, dasBlog,
local or network file, and webDav. For the average user, these options
might not be always be available.

However, using [GMail Drive Shell
Extension](http://www.viksoe.dk/code/gmail.htm), you can create a local
drive letter that maps to your GMail account. Then in RSS Bandit, open
up the properties dialog, click on the Remote Storage Tab, choose the
File Share protocol and enter the GMail drive in the UNC directory path
(it doesn't have to be UNC). In the screenshot below, I have the e:
drive mapped to my GMail account.

![Remote Storage Tab](/assets/images/RemoteStorageTab.jpg)

Now you can use your GMail account for synchronizing your RSS Bandit
state between multiple machines. Note that this usage of GMail is not
supported by [Google](http://www.google.com/) nor the developers of RSS
Bandit. So if Google suddenly decides to disrupt this usage of GMail,
you've been warned.

As you can see in the RSS Bandit
[Roadmap](http://www.rssbandit.org/ow.asp?RoadMap), there will be
support for more synchronization sources in the next major release.

