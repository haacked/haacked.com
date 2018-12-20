---
title: Improving The CommentAPI And Comment Moderation
date: 2004-09-12 -0800
disqus_identifier: 1198
tags: []
redirect_from: "/archive/2004/09/11/ImprovingTheCommentAPIAndCommentModeration.aspx/"
---

One thing that bothers me about the
[CommentAPI](http://wellformedweb.org/story/9 "CommentAPI Spec") is that
the only response you get is the HTTP status code.

> HTTP/1.1 200 OK

However, there are cases where it would be helpful to return more
information. For example, when I post a comment on a blog that moderates
its comment, the blog should note in the response that comments are
moderated, allowing the application to notify the user as such.

Otherwise I might assume that maybe there was a problem in posting the
comment and then use the web form within the blog itself to repost the
comment, only to discover that comments are moderated.

