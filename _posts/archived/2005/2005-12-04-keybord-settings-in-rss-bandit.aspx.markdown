---
title: Keybord Settings in RSS Bandit
tags: [rss]
redirect_from: "/archive/2005/12/03/keybord-settings-in-rss-bandit.aspx/"
---

I haven’t confirmed this myself, but I believe [Scott when he says
that](http://www.scottcreynolds.com/PermaLink.aspx?guid=86c13378-d2b2-4e6f-953c-6ae732662775)
the latest version of [RSS Bandit](http://www.rssbandit.org/) changed
some of the keyboard settings. In particular he states that to mark a
post as read changed from CTRL+M to CTRL+Q.

The reason I didn’t notice is that I am using customized keyboard
settings. It isn’t too hard if you use my (unsupported) keyboard setting
tool for RSS Bandit.

I wrote up a two-part article about how keyboard settings are
implemented, but the only [part you really need is part
2](https://haacked.com/archive/2005/03/22/2427.aspx) in which I describe
a quick and dirty tool for modifying keyboard settings.

This tool simply provides a GUI for modifying the ShortcutSettings.xml
file (an embedded resource file available with the source code) and
making a copy of it to the following directory: C:\\Documents and
Settings\\YOUR\_USER\_NAME\\Application
Data\\RssBandit\\ShortcutSettings.xml. Once there, new installations
shouldn’t override your custom keyboard settings.

