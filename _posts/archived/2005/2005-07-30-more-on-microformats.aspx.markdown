---
title: More on Microformats
date: 2005-07-30 -0800
tags: [web]
redirect_from: "/archive/2005/07/29/more-on-microformats.aspx/"
---

[Kevin Marks](http://epeus.blogspot.com/) says in the comments to my
[last post](https://haacked.com/archive/2005/07/28/9085.aspx) that my
example of how a format designed for both machine and human readability
might run into problems was a little contrived. My dear Mr. Marks how
silly of you to say so. I must vehemently disagree with your lack of
insight. That example was *a lot* contrived. I blame Jet Lag for the
lack of a better example, but the point that I was trying to make
(albeit unconvincingly), is that...

> *A format designed for both data exchange and data presentation cannot
> excel at both.*

Keep in mind that this doesn’t necessarily mean that Microformats cannot
do well *enough* for both tasks. I am simply attempting to examine the
consequences of using Microformats over other approaches.

There are three particular areas in which I see significant differences
between a data interchange format and a presentation format.

1.  Presentation
2.  Authoring
3.  Consumption

### Presentation

Suppose you have a large set of data to send from one machine to
another. Perhaps the statistical summaries for every soccer game played
in a given month. Since we’re playing make believe here, also assume
you’ve chosen XML as the format to represent that data. Are you
concerned about the fact that the receiving machine is going to suffer
from sensory overload trying to making sense such a large set of data?

Probably not.

When you choose a format designed for data interchange, you typically
have no problems sending the entire data set. Machines don’t generally
suffer from sensory overload.

But if you take the same set of data and want to present it to a human
(using say, (X)HTML), you’d probably want to break it up into multiple
pages, perhaps one per day, since that would be more readable for your
soccer afficionado than simply cramming everything on one page (though
many would-be web designers have committed such a crime in the past).
Unlike machines, we humans do suffer from sensory overload.

Now it certainly is possible to send the entire data set to the browser
and use DOM manipulation via Javascript to simulate paging, but that
seems like a hack to me. First of all, why not do the simplest thing
that works? It makes more sense to send only the data needed. Secondly,
if you need to use DOM manipulation to present the data well, it just
reinforces the idea that the format is not good for presentation.

### Authoring

Have you ever written poetry? And no, I am not referring to your code,
though I do see code as poetry at times. The difference between code and
poetry is that there are no strict rules to poetry. Certainly there are
some rules to some forms of poetry, but the only standard rule of poetry
is that to make great poetry, you have to break the rules.

Now how many machines do you know of that appreciate poetry (If you know
of one, let’s talk). Probably not any at this point in time. This is due
to the fact that computers cannot understand free form input. They
require input formatted accordirng to very precise rules. This is one of
the difficulties inherent in having a human author create content using
aformat for data interchange, humans in general are not precise enough.
Let me give you a more concrete example.

XHTML attempts to merge the presentation format of HTML with the data
interchange format for XML. In many ways, this is a praiseworthy goal as
consuming (we’ll get to that) XML is much easier than HTML. And, XML is
easy to validate as it has very strict rules. Unfortunately, *strict* is
not something humans do well.

I spent some time not too long ago attempting to make my blog XHTML 1.0
Transitional compliant. I succeeded in getting the front page to
validate, but would often post content that would break validation.
Imagine how bad it would be if I allowed HTML comments from visitors.

Now suppose I am writing a blog entry where I want to add a
microformatted item in my blog entry. There are an infinite number of
ways I could make a mistake and thus render the microformat useless and
unreadable by a machine. Writing for machines takes a lot of discipline
(think programmer), and humans don’t naturally do it well. We require
debuggers, compilers, unit tests, etc....

As you read this, you’re probably thinking that better tools will
ameliorate this problem, and you are probably right. When such tools
appear, they may make this point somewhat obsolete. But I still want to
contrast this approach to how my RSS feed is authored. I simply post
content to my blog. The content gets stored in a structured table. An
RSS generator (a machine if you wil), authors the structured data. No
extra work on my part. Yes, it is a little less flexible than the
alternative, since any new piece of information I want to include in the
feed has to have some persistent storage associated with it and code
written to author it. This is a classic trade-off. I don’t have to worry
about messing around with tools or proper formatting when I create
content, I merely cram it into my blog and the machine does the rest.

### Consumption

The other issue I have with Microformats is the Screen Scraping issue.
Some Microformat proponents propose that we start building aggregators
using Microformats instead of RSS. The problem with this approach as I
see it is that these aggregators have to resort to screen scraping to
find the appropriate markup within an HTML page. With RSS you have the
benefit that the entire document is well structured, not just small
sections within the document. You can use mature technologies to
validate the document before you spend time trying to parse it. How well
can you validate an HTML file that contains microformats alongside tag
soup?

### What Happens to Microformats When XHTML is widespread?

Microformats is designed to present data in a structured manner that
works in current browsers. In a way, it is a compromise. XHTML promises
the ability to add any kind of structured data (properly scoped by a
namespace) to an HTML document, but XHTML is not yet well supported. So
Microformats were created as a hybrid of XML and HTML. But what happens
when XHTML is supported well. Are we going to settle for a bunch of divs
to represent our data, or will we use more meaningful XML tags?

### Microformats Benefits

Ok, now that I’m done being critical of Microformats (and I’m only
critical because it is the new kid on the block and I want to know why I
should support it, if at all), I would like to point out some benefits
that occurred to me, apart from the ones I mentioned previously.

The first benefit is that it can work now. To the previous question I
raised about what happens when XHTML arrives, one can say, “Who cares,
it isn’t here now!” Touche! As the Greasemonkey script illustrates,
people are already starting to take advantage of the presense of
microformats to enhance user experience.

In part, this is an illustration of the Betamax principle. Although, I
may think that RDF or XML feeds are a better technical solution to the
problem Microformats tries to solve, the better technical solution
doesn’t always win. Often, the more convenient solution wins. That’s why
VHS won.

Second, it is flexible. By flexible, I mean that I can add microformat
data by simply publishing the format. I don’t have to add a new table in
my database to store hCalendar entries and then create a proper UI to
add those entries. Instead, I just post a blog post and carefully format
the post with the hCalendar format. Sure, I might screw it up, but I can
do this now, without a recompile.

A third benefit I have heard mentioned is that it is more easily
indexed. I don’t necessarily see why that is so as I have seen many RSS
feeds indexed by Google.

### Conclusion

So at this point, I am still a bit ambivalent about Microformats. I’ll
probably sit back and watch how well it is adopted and see if there is a
groundswell of support. We’ll definitely incorporate it into Subtext if
it becomes adopted in a widespread manner. Just don’t expect to see me
cheerleading a push to create Microformat aggregators. I still think RSS
does a great job of that and I believe it does a better job than
Microformats would. It is well known and just starting to gain
mainstream recognition. There’s no point in splintering the aggregation
industry at this point, confusing the layperson as a result. Besides, I
have a significant time investment in RSS technologies I am not willing
to give up just yet. ;)

