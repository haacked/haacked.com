---
title: Web Browser Security In RSS Bandit
date: 2004-07-23 -0800
disqus_identifier: 841
tags: []
redirect_from: "/archive/2004/07/22/web-browser-security-in-rss-bandit.aspx/"
---

[Dare](http://www.25hoursaday.com/) [asks the
question](http://www.25hoursaday.com/weblog/PermaLink.aspx?guid=f0e81765-a3b9-40d7-ad3d-e0500b7abcc1)
whether or not we should change the browser used by RSS Bandit. He was
greeted by over 30 comments, mostly in favor of the switch. This is
purely anecdotal, but I get the sense alot of people are upset by recent
vulnerabilities in IE. I also get the sense that a lot of people feel
that upstart browsers are toeing the line of innovation while IE has sat
on its fat ass and done nothing lately.

Whether that's true or not, as Dare points out, integrating another
browser into RSS Bandit is a bit of work and could open a whole can of
worms. I'd like to point out that there's something you can do now with
RSS Bandit as a stop-gap. It may not appease the die-hard Firefox or
Gecko users, but hopefully it will help you feel more secure using RSS
Bandit.

A little while ago I wrote up some documentation called [Changing The
Web Browser Security
Settings](http://www.rssbandit.org/docs/html/getting_started/changing_web_browser_security_settings.htm)
which can be found on the [RSS Bandit documentation
site](http://www.rssbandit.org/docs/). There are two important features
the document discusses. One is that you can have HTML links within RSS
Bandit opened by an executable of your choice. This may not integrate
with the nice Tabs within RSS Bandit, but at least you're using the
browser of your choice.

If you decide to stick with IE, I suggest configuring the **Security,
Restrictions** options. You can deactivate ActiveX controls (the source
of most vulnerabilities) and browse in relative safety. The
documentation describes the risk of checking each option.

The Reading Pane (or "Item Detail Pane") is not affected by these
settings. It never allows any script or ActiveX controls. While we
debate removing IE, you can read your feeds with more security. Happy
RSS Reading.

