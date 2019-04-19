---
title: Akismet DNS Issues
date: 2006-12-08 -0800 9:00 AM
tags: [tools]
redirect_from: "/archive/2006/12/07/Akismet_DNS_Issues.aspx/"
---

UPDATE: Looks like the DNS issue is starting to get resolved. The fix
may not have fully propagated yet.

If your Akismet Spam Filtering is currently broken, it is probably due
to a DNS issue going around. I reported it to the akismet mailing list
and found that people all over the world are having the same issue. It
is not just a Comcast issue.

The temporary fix is to add the following entry into your hosts file:

`72.21.44.242 rest.akismet.com`

Hopefully the Akismet team will fix this problem shortly.

