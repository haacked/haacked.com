---
title: How to Harvest Emails With Google And Protect Yours From Spammers
tags: [code,tech]
redirect_from: "/archive/2007/04/01/how-to-harvest-emails-with-google-and-protect-yours-from.aspx/"
---

Just something I noticed today. A lot of people (I may even be guilty of
this) publish their emails on the web using the following format:

> name at gmail dot com

Substitute *gmail dot com* with your favorite email domain.

**The problem with this approach is that it is trivially easy to harvest
email addresses in this format with Google.**

![Harvest](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/HowtoHarvestEmailsWithGoogle_10AB8/harvest%5B4%5D.jpg)

First, do a search for the following text (*include the quotes*):

> "\* at \* dot com"

Now, all you need to do is run a regular expression over the results.
For example, using your favorite regular expression tool, search for
this:

> (\\w+)\\s+at\\s+(\\w+)\\s+dot\\s+com

and replace with this:

> [\$1@\$2.com](mailto:$1@$2.com)

Now before you blame me for giving the spammers another tool in their
arsenal, I would be very surprised if spammers aren’t already doing
this. I highly doubt I’m the first to think of it.

**So what is a better way to communicate your email address without
making it succeptible to harvesting?** You could try mish-mashing your
email with [HTML entity
codes](http://www.w3schools.com/tags/ref_entities.asp "HTML Entity Codes").
For example, when viewed in a browser, the following looks exactly the
same as *name at gmail dot com*.

> name &\#97;t&nbsp;g&\#109;ail &\#100;ot&nbsp;c&\#111;m

The key is to somewhat randomly replace characters with entity codes, so
that we all don’t use the exact same sequence. If we all replaced every
letter with its corresponding entity code, it would be trivially easy to
farm.

But by introducing some randomness, it becomes a lot more difficult to
farm these emails. It’s possible, but would take more technical chops
and computing power than the technique I just demonstrated.

