---
title: Making Heads or Tails of Microformats
date: 2005-07-28 -0800
tags: []
redirect_from: "/archive/2005/07/27/making-heads-or-tails-of-microformats.aspx/"
---

I had an email discussion with [Dimitri](http://glazkov.com/blog/) about
[Microformats](http://microformats.org/about/) a little while back,
trying to understand the purpose of Microformats and what they intend to
solve.

At the time, the potential benefit I saw was that it might allow CSS
writers to share stylesheets for marking up certain types of content.
For example, suppose we standardize the markup for a calendar event
(say, using the [hCalendar](http://microformats.org/wiki/hcalendar)
format). Now if I write some seriously sweet CSS that makes calendar
events explode off the page, I could send that CSS to you and it would
be immediately useful. No need to reformat it to reflect the structure
of the HTML used to render your calendar event, assuming you followed
the standard.

At the time, I was focused on the fact that according to the
microformats about page, microformats are designed for humans first and
machines second.

However, the fact that microformats are machine readable lends itself to
other potential uses. For example, the Microformats blog [recently
highlighted](http://www.microformats.org/blog/2005/07/27/greasemonkey-and-microformats/)
a [Greasemonkey](http://greasemonkey.mozdev.org/) script that parses out
hCalendar events and provides links to import them into a calendar
application.

Now while I try to keep an open mind, I find it odd that Microformat
proponents are [attacking the use of
XML](http://www.25hoursaday.com/weblog/PermaLink.aspx?guid=70e31efd-d296-4708-af71-6499ce524afe)
on the web.

This is where I find the goals of Microformats to be a bit far reaching.
As far as my understanding goes, they present Microformats as a means to
have your website be the API, attempting to make technologies such as
RSS obsolete. The problem I have with this idea is that data exchange
and presentation are often at odds.

For example, suppose I want my presentation to only display calendar
events for the current week, but I want users to be able to import
calendar events for the month. However, I never want to display a month
calendar, for aesthetic reasons. It seems the Microformats method would
be for me to have a month’s worth of calendar events on the page, but
use CSS to hide those I don’t want displayed. Or, I could allow a query
string parameter to specify how many entries to display, but how would I
make that parameter discoverable without messing with my presentation
(i.e. without placing a link to it)?

Instead, I might choose a standard XML format for calendar entries and
provide a auto-discoverable reference to the calendar entries much in
the same way that HTML pages add auto-discoverable references to RSS
feeds. What’s so wrong with that?

It seems the Microformats user might say that the separate XML feed is
not necessary. Why duplicate content? This is a fairly good point worth
considering. The goal of Microformats is to provide a information in a
machine readable format as well as human readable. Part of fulfilling
that goal is to ensure that the presentation degrades well in a normal
browser.

For example, a competing approach to avoid duplication of content might
be to simply specify a calendar event namespace in an XHTML file and
embed that within the markup. The problem with this approach is that
[many browsers and web
authors](http://www.mezzoblue.com/archives/2003/09/03/markup_bulle/) do
not truly support XHTML properly. Thus, tags for alternate namespaces do
not show up, violating the Microformats goal of degrading gracefully.
Not only that, but most XHTML pages end up as being served as [tag
soup](http://www.mezzoblue.com/archives/2003/09/03/markup_bulle/)
because they are sent using the mime type `text/html`. [See *[Sending
XHTML as text/html Considered
Harmful](http://www.hixie.ch/advocacy/xhtml)*].

However, therein lies the problem with Microformats when compared to a
non-presentational XML format like RSS. If you recall, RSS stands for
Really Simple Syndiication. It’s not just that it is simple to syndicate
content, but that (in theory) it is simple to parse such a feed since it
relies on strict XML. Parsing HTML is much more difficult to do because
of the inconsistencies and all the effort that goes into understanding
malformed HTML. Unfortunately, that is exactly what a consumer of
Microformats is essentially forced to do, since Microformats are
intended to degrade gracefully. Microformats aren’t limited to XHTML and
can be placed in valid HTML documents, making it much more difficult to
validate a Microformat snippet.

In any case, it’ll be interesting to see how the use of Microformats
unfold. As Greasemonkey becomes more prevalent, I imagine the popularity
of Microformats might also take off. If I misunderstood microformats, be
sure to let me know.

