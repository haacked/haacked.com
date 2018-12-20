---
title: Why Not Convert HTML to XML?
date: 2004-10-25 -0800
disqus_identifier: 1488
tags: []
redirect_from: "/archive/2004/10/24/why-not-convert-html-to-xml.aspx/"
---

Pat Gannon (no blog) makes a great
[point](https://haacked.com/archive/2004/10/25/1471.aspx#1486) in the
comments on my post about [using regular expressions to parse
HTML](https://haacked.com/archive/2004/10/25/1471.aspx). He says:

> Just to play devil's advocate for a minute, it seems like HTML is just
> too darned close to XML to have to parse this way. Isn't there a
> library out there for converting HTML into XHTML? If you can do that,
> you can just read the file in using XmlDocument::LoadXml(). Once
> you've done that, you can find your tags using an XPath query. Sorry,
> I just couldn't let a parsing post go by without tossing in my two
> cents ;)

In fact, there are two approaches to this. The first recognizes that
HTML is really just a subset of SGML. Thus if you have a SGML parser,
you're done. So one option is to try Chris Lovett's
[SgmlReader](http://www.gotdotnet.com/Community/UserSamples/Details.aspx?SampleGuid=B90FDDCE-E60D-43F8-A5C4-C3BD760564BC).

In fact, this is what the current version of RSS Bandit uses for
auto-discovery of RSS feeds within HTML content. However, I recently
replaced it with regular expressions because of some memory use and
performance problems we were having with it. In our case, finding these
tags is a lot faster and uses less memory by just using a regular
expression. (Now you see the motivation for the post).

Another option is to use Simon Mourier's [HTML Agility
Pack](http://blogs.msdn.com/smourier/archive/2003/06/04/8265.aspx). He
takes an interesting approach in that he provides an HtmlDocument class
that implements
[System.Xml.XPath.IXPathNavigable](http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpref/html/frlrfsystemxmlxpathixpathnavigableclasstopic.asp).
Thus his approach provides the same interface as an XmlDocument for
querying nodes, but doesn't change the underlying HTML content as many
other approaches would by converting them to XML.

And just to toot Pat's horn a bit, I used to be his manager at
[Solien](http://www.solien.com/) when he was just starting out in his
career. Now he works at Univision and has inherited reams of code that
parse through Fortran code as well as proprietary database files. He's
also written his own grammar engine and xml syntax for describing
computer languages such as C\#. So he knows a thing or two about parsing
text. He's become quite a top notch developer. I'm just waiting for him
to get off his arse and start a blog.

