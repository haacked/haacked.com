---
title: How a Website Can Track Your Movements
tags: [privacy]
redirect_from: "/archive/2005/01/02/how-a-website-can-track-your-movements.aspx/"
---

![map of Japan](/assets/images/MapJapan.jpg) No, I haven't become a paranoid
privacy freak ready to purchase a cabin in Montana. This is just
something that struck me as I opened my browser today. My default home
page is [http://my.yahoo.com/](http://my.yahoo.com/). Thus when I open
my browser, the following information is sent to a Yahoo! server via
HTTP (HyperText Transfer Protocol. The rules for sending and receiving
data between a browser and website) (note: some data omitted for
brevity).

> GET / HTTP/1.1
>  Accept: */*
>  Accept-Language: en-us
>  Accept-Encoding: gzip, deflate 
> User-Agent: Mozilla/4.0 ... 
> Host: my.yahoo.com 
> Connection: Keep-Alive 
> Cookie: B=1note6p0p3843&b=2;...

Notice that the last line is labelled **Cookie** and there's a bunch of
data that comes after it (which I omitted). That data is the infamous
cookie data you no doubt have heard about. It probably contains some
sort of identifier which Yahoo!'s servers use to look up my personalized
information in a database, thus rendering a page just for me using my
settings (hence the name *my.yahoo.com* and not *your.yahoo.com*).

So far so good, it's really quite benign. But what you don't see in the
HTTP request is the TCP/IP data. Simply put, TCP/IP is the underlying
protocol used to send and receive HTTP messages across the web. As you
know, every computer connected to the internet has an IP address (the IP
of TCP/IP) which uniquely identifies that computer. When joining a
network, your computer will often have an IP address dynamically
assigned to it. Right now, my IP address is **61.125.193.68**.

Without getting into the nitty gritty, it's enough to know that blocks
of IP addresses are assigned to ISPs in huge blocks. Different blocks
also tend to be allocated to various geographic regions. Thus Yahoo! can
lookup my IP address in some database and figure out that I'm in Japan.
In fact, that's exactly what they did as when I opened my browser, I
noticed that the ads were in Japanese.

When I saw those ads, it occurred to me that any website I visited via
my laptop using cookies could corroborate the fact that I'm in Japan. Of
course, it might be easier to discover that fact by just reading [my
blog](https://haacked.com/archive/2005/01/01/1791.aspx).

As far as I know, this isn't a perfect means to obtain your whereabouts.
There are anonymizer services out there that can hide your true IP,
though the anonymizer service itself will know your IP.

