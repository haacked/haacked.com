---
title: DotNetKicks IBlogExtension Plugin For RSS Bandit (And Others)
tags: [oss,blogging]
redirect_from: "/archive/2006/06/04/DotNetKicksIBlogExtensionPluginForRSSBanditAndOthers.aspx/"
---

Recently [I
highlighted](https://haacked.com/archive/2006/06/02/KickItToEarnPayola.aspx "DotNetKicks")
a site named
[DotNetKicks](http://dotnetkicks.com "Like Digg, but for .NET") which is
like [Digg.com](http://digg.com/ "Digg"), but targetted to .NET
technology. In particular I thought it was a smart move for them to
share in their advertising revenue with those who submit stories.

Well to make it easier to kick stories from the convenience of your
favorite [RSS Aggregator](http://www.rssbandit.org/ "RSS Bandit Home"),
I wrote an IBlogExtension plugin so that you can submit/kick/unkick
stories from RSS Bandit or any RSS Aggregator that supports the
IBlogExtension plugin model (.NET 1.1 must also be on the machine).

Just
[download](https://haacked.com/code/BlogExtension.DotNetKicks.zip "DotNetKicks Plugin")
and unzip the extension to your plugins directory. The default location
for RSS Bandit would be *c:\\Program Files\\RssBandit\\plugins*.

Once you have it installed, restart RSS Bandit and right click on any
feed item and select the *DotNetKick This - Configure...* menu option.

![Context Menu](https://haacked.com/assets/images/SNAG-0036.gif)

This will bring up the plugin configuration dialog. You should leave the
URLs as they are. I made left them to be configurable in case the URL
ever changes. Just enter your DotNetKicks username and password and
click *OK*.

![](https://haacked.com/assets/images/SNAG-0037.gif)

This will save your username and password in an xml settings file with
the password heavily encrypted.

Now you can right click on a story to submit it to DotNetKicks. If the
story hasn’t been submitted, you will get the following dialog.

![Submit a Story Dialog](https://haacked.com/assets/images/SNAG-0038.gif)

This form is pretty self-explanatory.

If a story has already been submitted, you will see the following dialog
which allows you to kick it or unkick it (essentially adding your vote
to the story or removing it (*editor’s note: At the time of this
writing, the unkick function was not working*).

![Kick/Unkick a story dialog](https://haacked.com/assets/images/SNAG-0039.gif)

The API for DotNetKicks was published today on [Gavin Joyce’s
wiki](http://public.gavinjoyce.com/default.aspx/Public/KickApi.html "Gavin Joyce").
This was quite a turnaround as I emailed him on friday asking if there
was a web-based API. We went back and forth formulating the API and he
said he would work on it over the weekend. This morning, he sent me the
URL to his wiki page describing the API! Much of the API was inspired by
the [del.icio.us
API](http://www.programmableweb.com/api/del.icio.us "Del.icio.us API").

If you want to learn to write an IBlogExtension from scratch, check out
my [tutorial
here](http://www.rssbandit.org/docs/html/advanced/building_and_using_bandit_plugins.htm "IBlogExtension tutorial").
In this case, I was able to get a jumpstart by using
[Dare’s](http://www.25hoursaday.com/weblog/ "Dare Obasanjo") excellent
[del.icio.us
plugin](http://www.25hoursaday.com/weblog/PermaLink.aspx?guid=d2ad1435-f213-4e34-886f-52bfb0efa7b3 "Post to del.icio.us")
as a starting point.

Another plugin I wrote a while ago is the [improved w.bloggar
plugin](https://haacked.com/archive/2004/12/04/1697.aspx "Improved w.bloggar plugin")
for RSS Bandit that should hopefully be included in the next release of
RSS Bandit.

Once again, in case you missed the first link to this DotNetKicks
plugin,
[[DOWNLOAD](https://haacked.com/code/BlogExtension.DotNetKicks.zip "DotNetKicks Plugin")]
it.

