---
title: Subtext Notes Around The Web
tags: [subtext]
redirect_from: "/archive/2007/01/04/Subtext_Notes_Around_The_Web.aspx/"
---

While I really enjoyed the holidays, one part was really difficult for
me. There was some great discussions happening about
Subtext in the
mailing list and in various blog posts, but I was too busy to really get
involved.

I’m reading everything, but times are really busy for me right now as
I’ve fallen a bit behind on the book and have to play catch up. Not only
that, work is really picking up.

Unfortunately this means less time to work on Subtext and blog about it.
Fortunately, others have picked up the slack over the holiday weekend. I
wanted to highlight a few of those posts.

### Adding Custom ASPX pages to Subtext

Now that [Barry Dorans](http://idunno.org/ "Barry Doran’s Blog") finally
migrated his blog to Subtext, he’s writing about it. One post he wrote
deals with how to workaround the fact that Subtext intercepts all
requests for \*.aspx pages by default. Thus if you try and add an aspx
page for your own needs, it won’t get rendered. Barry walks you through
how to [add your own .aspx pages to a Subtext
installation](http://idunno.org/archive/2007/01/01/281.aspx "Adding .aspx pages to Subtext").

Barry also provides a quick tip on how to [recalculate view stats in
Subtext](http://idunno.org/archive/2006/12/31/280.aspx "Recalculate Stats").

### Subtext and IIS 7

[Sascha Sertel](http://blog.needforgeek.com/ "NeedForGeek blog.") wrote
a couple of interesting posts that cover how to get Subtext up and
running in IIS 7 on Vista.

His first post covers [Installing Subtext in an IIS 7 virtual
directory with SQL Server
2005](http://blog.needforgeek.com/archive/2006/12/07/InstallingSubtextOnIIS7AndSqlServer2005.aspx "Installing Subtext on IIS 7 and SQL Server 2005").
His guide provides some great troubleshooting advice for getting Subtext
up and running in this scenario. Hopefully the next version of Subtext
will support this scenario much better via improved documentation and
error messaging.

He has a short follow up post that covers [installing Subtext as its own
Website in IIS
7](http://blog.needforgeek.com/archive/2006/12/07/InstallingSubtextAsItsOwnWebSiteInIIS7.aspx "Installing Subtext as its own Website in IIS 7").
In truth, this probably applies to any web application in IIS 7.

His latest post in this series covers [debugging Subtext on Vista using
IIS 7 and Visual Studio
2005](http://blog.needforgeek.com/archive/2006/12/11/DebuggingSubtextOnWindowsVistaUsingIIS7.aspx "Debugging Subtext on Windows Vista using IIS7 and Visual Studio 2005").
While I personally use the built in Webserver.WebDev for debugging, I do
need to test the code using IIS before deployment. This is useful
information to have.

### Merging Blogs

[Keith Elder](http://keithelder.net/blog/ "Keith Elder’s Blog") writes
about his experience [merging two separate
blogs](http://keithelder.net/blog/archive/2007/01/01/Upgrading-and-merging-of-blogs-to-Subtext.aspx "Merging two blogs into Subtext")
into a single blog on Subtext. Once he had the data imported into a
local database, he deployed the Subtext code and ran exported the local
data to BlogML and imported that same BlogML from the server. Another
success story for
[BlogML](http://codeplex.com/Wiki/View.aspx?ProjectName=BlogML "BlogML").

So I may not be as heavily involved in current Subtext development at
the moment as I would like, but development is still moving forward with
or without me. That’s a good feeling.

