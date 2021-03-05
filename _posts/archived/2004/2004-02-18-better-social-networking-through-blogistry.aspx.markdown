---
title: Better Social Networking through Blogistry
tags: [blogging]
redirect_from:
  - "/archive/2004/02/17/better-social-networking-through-blogistry.aspx/"
---

![agent](/assets/images/agent.jpg)Lately it seems that everybody is playing “six-degrees of separation” by sprouting a social networking service.
Some of the most prominent are [Friendster.com](http://www.friendster.com/ "Now Defunct Friendster Website"),
[Tribe.com](http://www.tribe.com/ "Tribe website"), [MySpace.com](http://www.myspace.com/ "MySpace"), and Google’s
[Orkut](http://www.orkut.com/ "Orkut"). The promise these services make
is that by joining, you’ll be where all your friends are and in the
process make new friends and business contacts, though in reality they
tend to look like glorified dating services. Look around the blogosphere
and you’ll find several opinions concerning the problems and weaknesses
of these services. For example, Dare’s [take on social networking
services](http://www.25hoursaday.com/weblog/PermaLink.aspx?guid=ad4e2abb-9893-4d17-ab93-33046b7a7d3e "dare's opinion of social software"),
[Warren Ellis’s
evaluation](http://www.diepunyhumans.com/archives/006968.html) of Orkut,
and Don Park’s
[suggestion](http://www.docuverse.com/blog/donpark/EntryViewPage.aspx?guid=6080f857-4784-4679-8e41-c6881ed933ce "Don Park's Suggestion")
for improving existing services. Robert Scoble, a popular blogger,
points out in this [blog
entry](http://radio.weblogs.com/0001011/2004/02/07.html#a6481 "Social Networks Don't Interoperate"),
that none of these networks interoperate. A person ends up entering his
data into each site over and over again.

A better model for social networking is the blogging community itself.
Most blogs these days contain a list of other blogs called a *blogroll*.
Typically a blogroll is a list of other blogs that the blogger finds
interesting. Click on a link within a blogroll and you’re instantly
transported to another blog with its own blogroll. Continue navigating
blogrolls and you may cover the entire blogging community, or just end
up with a headache and a strong desire never to hear about another
person’s opinion of Janet Jackson’s Super Bowl boob.

These blogrolls create a de-facto network, though I hesitate to call it
a “friendship” network as the social networking services do, but it is a
network nonetheless. Many call the larger network of blogs, the
“blogosphere”.

It wouldn’t be too difficult to create a software component that could
index your blogging network and provide features similar to Friendster:
“Haacked is in your network, would you like to kick him out?”, or “You
have 13,237 bloggers in your network. You spend too much time online.”
Perhaps this would be a web control hosted by a blog, or a new feature
for an RSS aggregator.

Many of these blogs are hosted by some sort of blogging back-end
software whether it is
[http://www.blogger.com](http://www.blogger.com/ "Blogger"),
[http://radio.userland.com/](http://radio.userland.com/ "Userland"), or
my personal favorite [Subtext](http://subtextproject.com/ "Subtext").
With some of these tools, there are more powerful ways to establish
networks than simply following links in a blogroll. For example, after
reading a blog entry about the definition of a [track
back](http://scottwater.com/blog/archive/2004/02/06/Trackbacks.aspx "definition of a track back"),
I realized that social networking occurs both ways. Not only are the
owners of the blogs you link to (and those they link to and so on and so
on) members of your network, but those that link to you (referrals) and
those that comment on your site and create trackbacks to your site are
also members.

Trackbacks are particularly interesting in that they are a form of
machine communication between your blog and the blog of another author.
“Hi, I'm Haacked's blog. He recently wrote a post that referred to an
entry in your blog and trashes you and your family. Here's the URL. Have
a nice day!” This communication provides more information than a simple
link. Not only does the recipient of this trackback know that I read her
blog, but she also knows that I was motivated enough to refer to her
site in a post on my own blog.

In such an exchange, your blog is acting as a software agent on your
behalf. Think about that for a second. In Subtext for example, the
simple act of posting a blog entry with a link to another person’s blog
grants your blog permission to contact her blog (on your behalf) and
communicate your acknowledgment of her blog entry.

Now imagine if the next generation of these blogging tools contained new
features focused on this idea of a blog as social networking tool and
personal agent. Your blog could have many capabilities to act as an
agent on your behalf. It’s nice to know that your blog is looking out
for you.

For example, you might give your blog several pieces of personal
information and create some rules about who is allowed to gain access
this information in sum or in part. Taking the concept of the blogroll
one step further and creating various new “rolls”, a rule might be,
“Anyone that is in my “close-friend-roll” may have access to all of this
information. Anyone in my “colleague-roll” may have access to my
business information. And anyone who trackbacks to me may have my email
address.” Now, the next time I link to your blog entry, your blog might
volunteer some information about your blog to my blog, at which point my
blog may refuse or accept this information (depending on my
preferences).

Taken further, blogs may move past being a simple journal of your
everyday thoughts, but become your representative on the web. Tired of
filling out your billing and shipping information every time you shop
online? Add the selected merchant sites to your “secured-merchant-roll”
and the next time you purchase something from Amazon.com, just give them
your URL and let them get your billing and shipping information.

This rich communication between blogs would occur through XML standards
built into the next generation of blogging software. The current crop of
blogging tools already supports a variety of XML standards such as the
[MetaWeblog API](http://www.xmlrpc.com/metaWeblogApi "Metaweblog API"),
[TrackBack
API](http://www.movabletype.org/docs/mttrackback.html "Trackback API"),
[Comment API](http://wellformedweb.org/story/9 "Comment API"), and
[Blogger
API](http://www.blogger.com/developers/api/1_docs/ "Blogger API").
Perhaps these new communication abilities would be built on top of these
existing APIs or require new APIs altogether.

The beauty of this approach is the fact that these APIs are open and not
proprietary. Not to beat a TLA to death, but this architecture is an
example of Microsoft’s favorite TLA these days, SOA or Service Oriented
Architecture. It doesn’t matter which platform your blog is implemented
on. As long as it supports these various API’s and can make an http
request, it can participate in this rich communication.
