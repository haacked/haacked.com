---
layout: post
title: "Potential For A Subtle Bug in RFC3229 Implementations"
date: 2005-06-30 -0800
comments: true
disqus_identifier: 7427
categories: []
---
In a [previous post](http://haacked.com/archive/2005/06/30/7415.aspx) I
mentioned a problem I was having with implementing
[RFC3229](http://bobwyman.pubsub.com/main/2004/09/using_rfc3229_w.html).
Well I got that one fixed, but realized that I had another very subtle
bug. One that is potentially encouraged by the examples in the spec.

Before I begin, please realize that the examples are not an
implementation specification. They are simply examples of how one
**might** implement this RFC. Having said that, many implementors will
probably conform to the examples as I did.

At issue is that all the examples show an integer entity tag value.
Presumably this would be the id of the feed item (or post) within your
backend database. In Subtext, this value maps to an integer primary key
field that is an auto-incrementing identity.

So suppose you have a blog an a reader with an RFC3229-compliant
aggregator makes a request for your feed. It might look like

>     GET /Rss.aspx HTTP/1.1Host: haacked.comIf-None-Match: "100"A-IM: feed, gzip

The "100" represents the last feed item that the aggregator making the
request saw. The server presumably does a lookup to return all feed
items with an ID that is greater than this number, creates a feed with
those items, and sends that to the client. Bandwidth is saved as the
entire feed doesn’t need to be sent, just the items with an id greater
than 100.

Now suppose you had posted five items since the aggregator last made a
request. The response header might look like:

>     HTTP/1.1 226 IM UsedETag: "105"IM: feed, gzipDate: Thu, 30 Jun 2005 23:48:05 GMTCache-Control: no-store, im

Your server is telling the aggregator client, “Hey! The last item you
are getting has an e-tag of 105. Make sure to send the value 105 in your
next request.”

Well at least that’s how I implemented it in
[Subtext](http://subtextproject.com/). The problem is that this
implementation has a very subtle bug. Can you see the problem?

Since the table that stores blog entries has its id field as an
identity, I am assured that a newer element has a higher id than an
older element, so that’s not necessarily the problem. The problem is
that Subtext (and I presume other blogging engines) allows the author to
create a post that is not published.

So suppose a blogger create a post with an ID of 106 that isn’t
published. This same blogger then creates and publishes a post with an
ID of 107. The next request for the feed will get just item 107 and the
server response will look like

>     HTTP/1.1 226 IM UsedETag: "107"IM: feed, gzipDate: Fri, 01 Jul 2005 01:15:03 GMTCache-Control: no-store, im

Now say that the blogger fixes up the oh so important post 106 and
finally publishes it. The next request for the feed from that aggregator
will look like

>     GET /Rss.aspx HTTP/1.1Host: haacked.comIf-None-Match: "107"A-IM: feed, gzip

Your server will send the aggregator all feed items with an id greater
than 107, completely skipping the newly published item 106. This poor
aggregator completely misses out on the beauty and sublime writing that
was surely within post 106.

In order to rectify this situation, I need to add a DatePublished field
to my database table and use that to as the entity tag and to determine
which items an aggregator has and hasn’t seen. That will ensure that
important posts aren’t ignored from RFC3229 compliant aggregators.

