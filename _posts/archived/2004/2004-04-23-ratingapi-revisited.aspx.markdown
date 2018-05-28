---
layout: post
title: RatingAPI Revisited
date: 2004-04-23 -0800
comments: true
disqus_identifier: 359
categories: []
redirect_from: "/archive/2004/04/22/ratingapi-revisited.aspx/"
---

After reading up on the [CommentAPI](http://wellformedweb.org/story/9),
if I were to write the RatingAPI, I'd pretty much plagiarize the
CommentAPI but only make the following changes:

Remove the title and link elements (they are unnecessary).

Change the html discovery to:

\<link rel="service.rating" type="text/xml" href="url goes here"
title="Rating Interface" min="0" max="5"/\>

Change the new comment element to a rating element:

\<wfw:rating xmlns:wfw="http://wellformedweb.org/RatingAPI/"\>\

    \<wfw:endpoint\>http://bitworking.org/news/ratings/52\</wfw:endpoint\>\
     \<wfw:min\>1\</wfw:min\>\
     \<wfw:max\>5\</wfw:max\>\
 \</wfw:rating\>\

What do you think?

