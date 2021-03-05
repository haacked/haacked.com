---
title: Finding Code On Your Machine
tags: [code,tools]
redirect_from: "/archive/2007/06/03/finding-code-on-your-machine.aspx/"
---

As I [mentioned
before](https://haacked.com/archive/2007/05/11/my-last-day-before-starting-a-new-career.aspx "Starting a new career"),
I am the Product Manager for the [Koders.com
website](http://www.koders.com/ "Koders Code Search Engine"). I am
responsible for the search engine, the source code index, the forums,
the blog and the Content Management System.

![magnifying
glass](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/SearchingForCodeOnYourMachine_EC44/magnifying-glass_1.jpg)
My counterpart at Koders, [Ben
McDonald](http://beebe4.blogspot.com/ "Ben McDonald’s blog"), is
responsible for our client editions of the search engine which include
the [Enterprise
Edition](http://www.koders.com/corp/products/enterprise-code-search/ "Search Code on the Enterprise")
and the recently announced [Pro
Edition](http://www.koders.com/corp/products/pro/ "Desktop Code Search"),
which makes him one very busy fella.

He [just recently
blogged](http://www.koders.com/blog/?p=78 "Teaching an old dog new tricks")
about a private beta we have going on for Pro Edition. The Pro Edition
allows you to index and search code on your desktop. As far as I know,
the initial beta only searches the file system, but future versions
might index source control repositories just like the Enterprise
Edition.

If you’re interested in trying it out and providing feedback, go ahead
and [sign up
here](http://www.koders.com/corp/products/pro/ "Pro Edition Beta Sign-up").

The interesting part about this product for me is the tech:

> Oh yeah, in case any of you are wondering we ended up with the
> following responses to the initial requirements laid out before us:
>
> \* 6.2 Mb installer\
> \* SQLite embedded database\
> \* Cassini Personal Web Server from Microsoft\
> \* To make sure developers have something to search immediately after
> installation, we’ve bundled the indexed source code of our
> implementation of a [Amazon A9 OpenSearch
> client](http://opensearch.a9.com/-/company/opensearch.jsp "A9 OpenSearch Client"),
> broken down into two projects, the business layer and the web UI layer
>
I believe that’s a heavily customized version of the Cassini web server.
The product works similarly to how Google Desktop works in that you
search via the browser. This allows you to let other developers search
code on your machine, should you so choose.

**So what makes the Pro Edition different from just using a normal
Desktop search?** I'll let Ben answer that in more detail. But I’m
betting he’ll talk about how we provide some degree of semantic analysis
of the code, allowing you to search specifically for a method or class
for example.

