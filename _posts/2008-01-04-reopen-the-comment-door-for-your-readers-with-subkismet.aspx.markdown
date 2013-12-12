---
layout: post
title: "Reopen The Comment Door For Your Readers With Subkismet"
date: 2008-01-03 -0800
comments: true
disqus_identifier: 18444
categories: [code]
---
Six months ago and six days after the birth of my son, [Subkismet was
also
born](http://haacked.com/archive/2007/06/12/introducing-subkismet-the-cure-for-comment-spam.aspx "Introducing Subkismet")
which I introduced as the cure for comment spam. The point of the
project was to be a useful class library containing multiple spam
fighting classes that could be easily integrated into a blog platform or
any website that allows users to comment.

One of my goals with the project was to make it safe to enable
trackbacks/pingbacks again.

I’ve been very happy that others have joined in the Subkismet efforts.
[Mads Kristensen](http://blog.madskristensen.dk/ "Mads Kristensen"),
well known for
[BlogEngine.NET](http://www.dotnetblogengine.net/ "BlogEngine.NET blog engine"),
contributed some code.

[Keyvan Nayyeri](http://nayyeri.net/blog/ "Keyvan Nayyeri") has also
been hard at work putting the finishing touches on an implementation of
a client for the [Google Safe Browsing
API](http://code.google.com/apis/safebrowsing/ "Safe Browsing") service.

Keyvan just announced the [release of Subkismet
1.0](http://nayyeri.net/blog/subkismet-1-0-released/ "Subkismet 1.0 Released!").
Here is a list of what is included in this release. I’ve bolded the
items that are new since the first release of Subkismet.

> For the current version (1.0) we’ve included following tools in
> Subkismet core library:
>
> -   Akismet service client: Akismet is an online web service that
>     helps to block spam. This service client implementation for .NET
>     is done by Phil and lets you send your comments to the service to
>     check them against a huge database of spammers.
> -   Invisible CAPTCHA control: A clever CAPTHCA control that hides
>     from real human but appears to spammers. This control is done by
>     Phil and you can read more about it
>     [here](http://haacked.com/archive/2006/09/26/Lightweight_Invisible_CAPTCHA_Validator_Control.aspx).
> -   CAPTCHA control: An excellent implementation of traditional image
>     CAPTCHA control for ASP.NET applications.
> -   **Honeypot CAPTCHA**: Another clever CAPTCHA control that hides
>     form fields from spam bots to stop them. This control is done by
>     Phil and you can read more about it
>     [here](http://haacked.com/archive/2007/09/11/honeypot-captcha.aspx).
> -   **Trackback HTTP Module**: An ASP.NET Http Module that blocks
>     trackback spam. This module is done by [Mads
>     Kristensen](http://blog.madskristensen.dk/) and he has explained
>     his method in a [blog
>     post](http://blog.madskristensen.dk/post/Trackback-spam-fighting.aspx)
>     and [this Word
>     document](http://www.codeplex.com/subkismet/WorkItem/AttachmentDownload.ashx?WorkItemId=3188&FileAttachmentId=311).
> -   **Google Safe Browsing API service client**: This is done by me
>     and you saw its [Beta
>     2](http://nayyeri.net/blog/google-safe-browsing-library-for-net-beta-2/)
>     version last week. Google Safe Browsing helps you check URLs to
>     see if they’re phishing or malware links.

Since it is always fun for us developers to write code with the latest
technologies, we’re planning to have the next release target .NET 3.5.

Part of the reason for that is that over the holidays, I wrote a little
something something just for fun as a way of getting up to speed with
C\# 3.0. It was a lot of fun and I think my secret project will make a
nice addition to Subkismet. I’ll write about it when it is ready for
action.

Unfortunately, this move has made it difficult to fully complete the
implementation and test it out on real data.

[Subkismet is hosted on
CodePlex](http://www.codeplex.com/subkismet/ "Subkismet on CodePlex").
Many thanks go out to Mads and others for their contributions and to
Keyvan for all the implementation work as well as release work he did.
And for the record, Keyvan mentioned that I *ordered* him to prepare the
release. While I do have dictatorial tendencies, I think this is too
harsh a description. I’d like to say I *requested* that he prepare the
release. ;)

Technorati Tags:
[Subkismet](http://technorati.com/tags/Subkismet),[Spam](http://technorati.com/tags/Spam)

