---
title: Speed Up Your Pages And Improve Your YSlow Score With The Coral Content Distribution
  Network
tags: [web,performance,code]
redirect_from: "/archive/2007/08/12/speed-up-your-pages-and-improve-your-yslow-score-with.aspx/"
---

UPDATE: Using Coral CDN to serve up my images and stylesheets ended up
being a mistake and actually *slowed* down my site. I’d recommend using
Amazon S3 instead if you need high bandwidth fast serving of static
content. Coral CDN is probably better for cases when you want to serve
up a large file (mp3, mpeg, etc...) and save on your bandwidth usage. It
doesn't seem ready to be a general purpose CDN for speeding up your
site. I’ll add the ability to this code to use S3. In the meanwhile,
this code is still useful by simply restricting the extensions in the
config file to perhaps this list "mpg,mp3,mpeg,wmv,avi,zip". Hat tip to
Jon Galloway for pointing that out.

Yahoo recently released the ever so popular [YSlow add-on for
Firebug](http://developer.yahoo.com/yslow/ "Speed up your web pages with YSlow") used
to help locate bottlenecks for web pages. From their developer network
site we learn...

> YSlow analyzes web pages and tells you why they’re slow based on the
> rules for high performance web sites. YSlow is a [Firefox
> add-on](https://addons.mozilla.org/en-US/firefox/addon/5369 "Firefox Add-On")
> integrated with the popular
> [Firebug](http://www.getfirebug.com/ "Firebug") web development tool.
> YSlow gives you:\
>
>     \* Performance report card\
>      \* HTTP/HTML summary\
>      \* List of components in the page\
>      \* Tools including
> [JSLint](http://jslint.com/ "jslint javascript analysis tool")

YSlow provides a nice report card for your site. Here you can see the
unfortunate grade my blog gets at the time of this writing.

![Haacked.com YSlow
Score](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CoralDistributionNetworkPlugin_FAA8/Firebug%20-%20youve%20been%20HAACKED_2.png)

Naturally I couldn’t just sit there while some unknown Yahoo
disdainfully gives my blog an F. I decided to start digging into it and
start attacking specific items.

I decided to start with \#2 Use a CDN which stands for Content
Distribution Network. The Yahoo YSlow help page has [this to say about
CDNs](http://developer.yahoo.com/performance/rules.html#cdn "Yahoo Yslow Help for CDN").

> A content delivery network (CDN) is a collection of web servers
> distributed across multiple locations to deliver content more
> efficiently to users. The server selected for delivering content to a
> specific user is typically based on a measure of network proximity.
> For example, the server with the fewest network hops or the server
> with the quickest response time is chosen.

That certainly sounds useful, but the CDNs listed by Yahoo include
Akamai, Mirror Image Internet, and LimeLight Networks. These might be
great for big companies like Yahoo, but they’re a bit cost prohibitive
for small fries like us bloggers.

Coral to the Rescue
-------------------

That’s when I remembered the [Coral Content Distribution
Network](http://www.coralcdn.org/ "Coral CDN"). [Jon
Galloway](http://weblogs.asp.net/jgalloway/ "friend met") wrote about
this [a long time
ago](http://weblogs.asp.net/jgalloway/archive/2005/11/26/431592.aspx "Use the Coral Distribution Network to save bandwidth") as
a means to save on bandwidth. The one fatal flaw at the time was that
the network only worked over port 8090. Fortunately, that little issue
has been corrected and Coral now works over port 80.

And the wonderful thing about Coral is that it’s trivially easy to use
it. All you have to do is append your domain name with ****.

So this:

**http://example.com/my/really/big/file.mpeg**

becomes

**http://example.com.nyud.net/my/really/big/file.mpeg**

And now your really big file is being served up by hundreds of
geographically distributed servers. You just need to keep that file on
your server at the original location so Coral can find it when adding it
to its network.

Tell YSlow about Coral
----------------------

By default, YSlow doesn’t recognize Coral as a CDN, which means
implementing Coral CDN won’t affect your YSlow grade. YSlow only
recognizes the CDNs in use by Yahoo. Fortunately, it’s pretty easy to
add Coral to the list. Just follow these steps:

1.  Go to `about:config` in Firefox. You’ll see the current list of
    preferences.
2.  Right-click in the window and choose New and String to create a new
    string preference.
3.  Enter `extensions.firebug.yslow.cdnHostnames` for the preference
    name.
4.  For the string value, enter the hostname of your CDN, for example,
    **`nyud.net`**. Do not use quotes. If you have multiple CDN
    hostnames, separate them with commas.

Here’s a screenshot of the domains I added to YSlow. I’m sure I’ll think
of more to add later.

![YSlow CDN
Configuration](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/CoralDistributionNetworkPlugin_FAA8/Enter%20string%20value_1.png)

How can I automate this?
------------------------

You knew I wasn’t going to write about this without providing some means
for automating this conversion, did ya? There are two approaches I could
take:

1.  Rewrite URLs to static files on incoming posts.
2.  Rewrite URLs on the way out.

The first approach rewrites the URL as you are posting content to your
blog. This has the distinct disadvantage that should you decide to
change the distribution network, you need to go through and rewrite
those URLs.

The second approach rewrites the URLs as they are being output as part
of the the HTTP response. The issue there is to do it properly requires
buffering up the entire output (rather than letting IIS and ASP.NET
stream it) so you can perform your regex replacements and whatnot. This
can impair performance on a large page.

I decided to go with option \#1 for now for performance reasons, though
option \#2 would be quite easy to implement. I wrote an HttpModule in
the same style as my [Windows Live Writer crap
cleaner](https://haacked.com/archive/2007/07/29/cleanup-the-crap-that-windows-live-writer-injects-with-this.aspx "Cleanup the crap that WLW injects") which
rewrites an incoming MetaWeblog API post to append **nyud.net** to the
domain.

The code here only works with Windows Live Writer and BlogJet (*untested
in the latter case*) but can be easily modified to allow posts for any
blog client (I just got lazy here) by modifying the user agent within
the method `IsMetaweblogAPIPost`.

The reason I didn’t write this as a WLW plugin is that it’s not yet
possible to hook into pipeline and rewrite content just before WLW posts
it to the blog. That may be coming in the future though, according
to [this
comment](http://jcheng.wordpress.com/2007/08/10/new-plugin-dynamic-template/#comment-6083 "A comment on hooking into the pipeline")
by [Joe Cheng](http://jcheng.wordpress.com/ "Joe Cheng") of the WLW
team.

Download and Use It
-------------------

You can [download the code
here](https://haacked.com/code/HtmlScrubber.zip "HtmlScrubber Source and Binaries") (*binaries
are included in the bin dir*) in a project called `HtmlScrubber`. I
simply added this `HttpModule` to the same code as the WLW Crap Cleaner
module mentioned earlier. To use it simply add the following to your
web.config.

```csharp
<httpModules>
  <add type="HtmlScrubber.CoralCDNModule, HtmlScrubber" 
    name="CoralCDNModule" />
</httpModules>
```

This filter works by looking at the file extension of a referenced file.
If you’d like to change the list of extensions, you can add the
following configuration.

```csharp
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="CoralCDNConfigSection" 
      type="HtmlScrubber.CoralCDNConfigSection, HtmlScrubber" 
      allowDefinition="Everywhere" 
      allowLocation="true" />
  </configSections>
  
  <CoralCDNConfigSection 
    extensions="mpg,mp3,mpeg,wmv,avi,zip" />
</configuration>
```

The list of extensions shown are the default, so you don’t need to add
this configuration section unless you want to change that list. Please
enjoy and let me know if you put this to good use. Hope this makes your
blog faster than mine.

As for me, I’m moving on to looking into
using [JSMin](http://crockford.com/javascript/jsmin "A tool for compressing javascript"),
[JSLint](http://www.jslint.com/lint.html "A tool for analyzing common errors in javascript"), and
merging CSS files etc...
