---
title: How the IIS SEO Toolkit Saved My Butt
tags: [tools,tech]
redirect_from: "/archive/2009/12/13/seo-toolkit-saves-the-day.aspx/"
---

Ok, it wasn’t necessarily my ass that was saved, but it was years worth
of images which were important to me!

As [I wrote
yesterday](https://haacked.com/archive/2009/12/14/back-in-business-again.aspx "Back in Business"),
my blog’s hosting server had a hard-drive failure effectively wiping out
my virtual machine, taking my blog down with it. Fortunately, I was able
to get back up with a static archive of my site provided by Rich
Skrenta, but I was missing all my images and other content (code
samples).

As Jeff mentions,

> I have learned the hard way that there are almost no organizations
> spidering and storing images on the web.

Keep in mind that the images are not just mere eye candy. In many cases,
they serve to illustrate key concepts: “*As you can see in the
screenshot above, if the screenshot were still to exist, but through the
sheer ineptitude of Phil you’ll have to guess at the knowledge it would
have conveyed.*”

As I was lamenting the loss of these files, I started poking around my
browser cache (finding [this great tool for exporting the Google Chrome
cache](http://www.nirsoft.net/utils/chrome_cache_view.html "Chrome Cache Viewer")
by the way which can retain directory structure) looking for stray
images.

I then started thinking about alternative tools that might cache web
content such as a client RSS reader, etc. Then it occurred to me, didn’t
I run the [**IIS SEO
Toolkit**](http://www.microsoft.com/web/page.aspx?templang=en-us&chunkfile=seo.html "IIS SEO Toolkit")
against my site recently (*click to enlarge*)?

[![SEO Toolkit
Screenshot](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/HowtheIISSEOToolkitSavedMyButt_13016/iis-seo-toolkit_thumb.png "SEO Toolkit Screenshot")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/HowtheIISSEOToolkitSavedMyButt_13016/iis-seo-toolkit_2.png)The
first time I saw the SEO Toolkit was when [Carlos
Aguilar](http://blogs.msdn.com/carlosag/ "Carlos Aguilar") gave me a
demo a long while ago. Back then it was just something he put together
over the weekend. As soon as I saw it I begged him (quite annoyingly I
must say) to let me have a private build to try out. Eventually it was
released and I was able to run it against Haacked.com to see how much I
sucked.

Well it’s a good thing I ran it back in August! This tool stores a local
cache of images. Oddly enough, it appends a .txt extension to every
cached file.

[![cached-images](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/HowtheIISSEOToolkitSavedMyButt_13016/cached-images_thumb.png "cached-images")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/HowtheIISSEOToolkitSavedMyButt_13016/cached-images_2.png)
No worries! Using a bit of DOS command magic, I was able to strip off
the .txt extension from every file (note I ran this from the command
line. If you put this in a batch file, you’ll need to double up on the %
character).

> `     `
>
> for /r %x in (\*.c) do ren "%x" "%\~nx"

That stripped off the extensions. Afterwards I uploaded the images and
am now only missing images for 18 blog posts, the posts written in
August, September, and October of 2009. Those shouldn’t be too hard to
recreate manually (***though if you happen to have those images in your
browser or RSS cache, I do appreciate you sending them to me! My email
at Microsoft is philha***)

Looks like I caught a lucky break this time in finding and leveraging
the previously undocumented “Back up a website for dummies” feature in
the IIS SEO Toolkit. Carlos, I owe you one! :)

UPDATE: If you were wondering why the cached files were stored with a
.txt extension appended, Carlos revealed the mystery to me in an email.

> Oh I forgot to explain that silly thing we do with file extensions is
> a “naïve-silly attempt” to reduce the accident of double-clicking a
> javascript, exe, or let the shell try to display ‘malign image’ that
> might come from external sites. Since in theory we consider that a
> ‘private’ cache we decided to do this silly trick to prevent any funky
> games with somebody generating an
> ‘brittney-spears-nude-picture.jpg.exe’ with the icon of a JPG file
> that lured someone into running it.
>
> Agreed its pretty silly, but it was a simple ‘cheesy security feature’
> easy to add for our ‘hidden cache’.

Basically it helps protect the double-click happy folks out there from
hurting themselves accidentally.

