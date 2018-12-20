---
title: Web Applications and Local Document Storage
date: 2005-11-02 -0800
disqus_identifier: 11098
tags: []
redirect_from: "/archive/2005/11/01/web-applications-and-local-document-storage.aspx/"
---

Rob Howard asks the question *[Is “Smart Client” a “Dumb
Idea”](http://weblogs.asp.net/rhoward/archive/2005/11/03/429355.aspx)*.
Obviously I don’t necessarily think so as I pointed out in my post
*[Overlooked Problem With Web Based
Applications](https://haacked.com/archive/2005/11/01/11075.aspx)*.

However, as I thought about it more, I realized that part of the
excitement over web applications is that they are starting to really
deliver on the failed promises that Java made...

> Write once, run anywhere.

Although closer to the truth is...

> Write once, debug CSS and Javascript quirks everywhere.

The missing piece in my mind is that there is no built in support for
managing local storage of your web based data. As websites get richer
and richer, perhaps Smart clients aren’t the only way to solve this
problem. All that is necessary is to develop an HTML specification for
saving user data to the desktop.

Well we have such a thing now, they’re called “cookies”. But cookies are
limited in size and not very useful for document management. The idea in
mind is to create a specification similar to cookies, but that allow
full structured documents to be stored on the client from the web
server. Javascript running in the browser would have permission to
modify these documents (subject to the same restrictions as modifying a
cookie).

In this scenario, if you are offline and need to read a document or
email, you simply navigate to the Url of the web application. The
browser would then load the site from its internal cache. Or better yet,
rather than loading the site (which might not be very useful), it loads
a list of document “viewer” that the site registers with the browser.
You choose the viewer, which is nothing more than a bit of javascript
that is capable of listing and viewing locally stored documents.

When you reconnect, your browser sends the document to the site which
merges your changes, giving you the option to resolve conflicts. It
sounds a lot like smart clients, doesn’t it? The obvious difference is
that your application would theoretically run on nearly any machine with
a modern browser that supports these new standards and would not require
the .NET platform nor a Java virtual machine.

In any case, this is my hand waving half-baked view of where we’re
headed with web applications.

