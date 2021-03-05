---
title: Another Attempt To Reduce Comment Span
tags: [spam,web]
redirect_from: "/archive/2004/07/01/another-attempt-to-reduce-comment-span.aspx/"
---

![Spam](/assets/images/spam.jpg)A little while back, I had a [few
ideas](https://haacked.com/archive/2004/06/05/529.aspx) about how to
combat comment spam. My ideas were more geared towards a trust-based
approach to stopping comment graffiti than spam, but they were a bit
naive in some ways.

Lately, I've been following some conversations on various blogs
attempting to address this problem. Dave Winer [suggest that comments
expire](http://archive.scripting.com/2004/07/01#When:4:59:21PM) unless
the owner does something about it.

[Phil Ringnalda
responds](http://philringnalda.com/blog/2004/07/got_a_piece_of_it.php)
that he doesn't want the comments to ever get indexed. This problem
seems likely solved by [this suggestion in From The
Orient](http://www.dellah.com/orient/2004/05/07/commentspam) that notes
that simply stripping the links out of the text themselves will make
sure Google doesn't index it.

As [Derek Powazek points
out](http://www.powazek.com/2003/11/000273.html), it is Google's
voracious appetite for indexing pages that is the root motivation for
people to comment spam a blog. One question I have about all this is
doesn't Google honor the the
[robots.txt](http://www.robotstxt.org/wc/faq.html) file or the META tag
standard for excluding robots? Adding the following tag:

> ```csharp
> <META NAME="ROBOTS" CONTENT="NOFOLLOW">
> ```

tells Google not to index the links on the given page. Another option is
to add a Robots.txt file and tell Google not to index your archives.
Personally, I think this second option is too draconian. I think it's
great that people find my blog when they search on how to select random
records from SQL Server.

Perhaps what is needed is for us to get together and extend the
Robots.txt standard and then push for Google to honor it. Now, I don't
know exactly how Google indexes a website. I don't know if it parses it
as an HTML tree, but supposing it does. It'd be great to have this
ability.

> ```csharp
> <DIV noindex="false" nofollow="true">
> Welcome to the comments section of this page.The content here will be indexed, but the links will not.Your spam's no good here. 
> DIV> 
> ```

Another option is to just have a comment that indicates everything AFTER
the comment should not be indexed:

> ```csharp
> ```

This is easier for an web crawler to parse.

Combining this with an image verification system (like the one that
comes with the [ASP.NET resource
kit](http://msdn.microsoft.com/asp.net/asprk/) from SAX), hopefully
lowers the real motivation to comment spam a site. If it doesn't
increase their page rank AND they can't automate posting it, why bother?

Another crazy idea I'll mention (and I know this will bog down the
server a bit) is to use a component that converts text to an image. That
way by default, the entire comment will not be indexed. Just thought I'd
throw that out there.

