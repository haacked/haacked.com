---
layout: post
title: Blog Upgrade
date: 2006-08-06 -0800
comments: true
disqus_identifier: 14688
categories: []
redirect_from: "/archive/2006/08/05/Blog_Upgrade.aspx/"
---

If you read this blog outside of an aggregator, you might notice a few
minor new tweaks. I am dogfooding Subtext 1.9 which runs on ASP.NET 2.0.
We are very close to preparing a release, so I figured I would beta test
this one on my own blog and see if everything works well.

A couple of new things you might notice is that there is now a simple
search field on the left hand sidebar that displays its results in an
overlaying div. Also, when you view an individual post, there are links
to the next and previous post. I have also added
[gravatar](http://www.gravatar.com/ "Gravatar") support to the comments.

It took me a while to warm up to the idea, but I really like the
gravatars. I have participated in many various message boards and sites
(such as [flickr](http://flickr.com "flickr")) in which users choose an
avatar to represent themselves. It is a small thing, but adds to the fun
and identity for the visually focused. However in most cases, you have
to set up a separate avatar for each site.

With gravatars, you register an avatar with their site and in any system
that supports it, your avatar is displayed when you supply your email
address to the software. Subtext takes your email address, creates a
one-way MD5 hash of it, and then requests your gravatar from
gravatar.com. If none is found, then a default placeholder is displayed.
I will post a comment to this post as a demonstration.

