---
layout: post
title: UrlScan Broke My Blog (And How I Fixed It)
date: 2010-09-26 -0800
comments: true
disqus_identifier: 18722
categories:
- asp.net
- subtext
- code
redirect_from: "/archive/2010/09/25/url-scan-broke-my-blog.aspx/"
---

By now, you’re probably aware of a serious [ASP.NET
Vulnerability](http://weblogs.asp.net/scottgu/archive/2010/09/18/important-asp-net-security-vulnerability.aspx "Security Vulnerability")
going around. The ASP.NET team has been working around the clock to
address this. Quite literally as last weekend, I came in twice over the
weekend (to work on something unrelated) to find people working to
address the exploit.

Recently, Scott Guthrie posted [a follow-up blog
post](http://weblogs.asp.net/scottgu/archive/2010/09/18/important-asp-net-security-vulnerability.aspx "A Follow-Up Post")
with an additional recommended mitigation you should apply to your
servers. I’ve seen a lot of questions about these mitigations, as well
as a lot of bad advice. The best advice I’ve seen is this - if you’re
running an ASP.NET application, follow the advice in Scott’s blog to the
letter. Better to assume your site is vulnerable than to second-guess
the mitigation.

In the follow-up post, Scott recommends installing the handy dandy
[UrlScan IIS Module](http://www.iis.net/download/UrlScan "URL Scan") and
applying a specific configuration setting. I’ve used UrlScan in the past
and have found it extremely useful in [dealing with DOS
attacks](http://haacked.com/archive/2008/08/22/dealing-with-denial-of-service-attacks.aspx "Dealing with DOS").

However, when I installed UrlScan, my blog broke. Specifically, all the
styles were gone and many images were broken. It took me a while to
notice because of my blog cache. It wasn’t till someone commented that
my new site design was a tad bit bland, that I hit CTRL+F5 to hard
refresh my browser to see the changes.

I looked at the URLs for my CSS and I knew they existed physically on
disk, but when I tried to visit them directly, I received a 404 error
with some message in the URL about being blocked by UrlScan.

I opened up the *UrlScan.ini* file located:

`%windir%\system32\inetsrv\urlscan\UrlScan.ini`

And started scanning it. One of the entries that caught my eye was this
one.

>     AllowDotInPath=0         ; If 1, allow dots that are not file
>                              ; extensions. The default is 0. Note that
>                              ; setting this property to 1 will make checks
>                              ; based on extensions unreliable and is
>                              ; therefore not recommended other than for
>                              ; testing.

That’s when I had a hunch. I started digging around and remembered that
I have a custom skin in my blog named “haacked-3.0”. I viewed source and
noticed my CSS files and many images were in a URL that looked like:

`http://haacked.com/skins/haacked-3.0/style/foo.css`

Aha! Notice the dot in the URL segment there?

What I *should* have done next was go and rename my skin. Unfortunately,
I have many blog posts with a dot in the slug (and thus in the blog post
URL). So I changed that setting to be 1 and restarted my web server.
There’s a small risk of making my site slightly less secure by doing so,
but I’m willing to take that risk as I can’t easily go through and fix
every blog post that has a dot in the URL right now.

So if you’ve run into the same problem, it may be that you have dots in
your URL that *UrlScan* is blocking. The best and recommended solution
is to remove the dots from the URL if you are able to.

