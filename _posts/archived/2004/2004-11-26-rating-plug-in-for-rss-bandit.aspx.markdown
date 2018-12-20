---
title: Rating Plug-In for RSS Bandit
date: 2004-11-26 -0800
tags: [rss]
redirect_from: "/archive/2004/11/25/rating-plug-in-for-rss-bandit.aspx/"
---

__NOTE: This plug-in is no longer available or supported__

A while ago I [wrote about AmphetaRate](https://haacked.com/archive/2004/05/06/blog-recommendation-server-amphetarate.aspx/), a blog
recommendation engine that takes user ratings of blog entries and serves
an RSS feed of recommended blog entries.

Later on I wrote an article about how to [build an IBlogExtension
plugin](https://haacked.com/archive/2004/06/19/651.aspx) which was added
the the [RSS Bandit documentation site](http://www.rssbandit.org/docs/).

As a warmup to writing that article I wrote an AmphetaRate plugin to
work through the kinks of writing a plugin for RSS Bandit. However I
never released that plug-in as I wasn't ever sure if it was working
since all my recommendations were "training" recommendations.

This is a classic chicken and egg problem. If there aren't enough users using AmphetaRate, the quality of recommendations are poor.

In any case, I noticed today that
[Dare](http://www.25hoursaday.com/weblog/) updated the [RSS Bandit roadmap](http://www.rssbandit.org/ow.asp?RoadMap) and the version (code
named Nightcrawler) after the next version may include a "Thumbs Up/Thumbs Down" rating system, perhaps intergrated with AmphetaRate.
This would certainly help solve the "Chicken Egg" problem.

In the meantime, I am releasing my plug-in with no warranties. Just copy the AmphetaRatePlugin.dll to the "plugins" subfolder of the RSS Bandit installation (on my computer that's at "C:\\Program Files\\RssBandit\\plugins").

Note that although this plug-in implements the IBlogExtension interface,the configuration form implements a feature specific to RSS Bandit. When you configure this plug-in, I wanted to provide an easy manner to subscribe to a user's recommendation feed. So I added a LinkLabel that when clicked does just that.

However, I had to cheat a bit to do that. Since the IBlogExtension interface doesn't define methods you may want to call on the calling
application, I used Reflection to call a method to bring up a pre-populated "New Feeds" dialog. You can see the source for that call
below.

```csharp
private void lnkSubscribeURL_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
{
    if(IsValidID(this.txtID.Text))
    {
        object banditApp = this.Owner.GetType().InvokeMember("GuiOwner",
BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty,
null, this.Owner, null);
        if(banditApp != null)
        {
            bool result =
(bool)banditApp.GetType().InvokeMember("CmdNewFeed", BindingFlags.Public
| BindingFlags.Instance | BindingFlags.InvokeMethod, null, banditApp,
new object[] {null, this.lnkSubscribeURL.Text, "Personalized AmphetaRate
Feed"});
        }
    }
}
```

At some point, it's possible this will become a fully supported feature of RSS Bandit. For now you're stuck with this hack. If anyone's
interested, I'd love to discuss creating a new plug-in standard that's a little more full featured than IBlogExtension. It works great for simple plug-ins, but doesn't provide much support for a plug-in to interact with the application. I can think of several operations every aggregator pretty much supports that a plug-in might make use of such as "Subscribe To Feed", "Mark Item As Read", etc...

