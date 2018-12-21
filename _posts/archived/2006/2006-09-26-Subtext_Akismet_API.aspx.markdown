---
title: Subtext Akismet API
date: 2006-09-26 -0800
tags: []
redirect_from: "/archive/2006/09/25/Subtext_Akismet_API.aspx/"
---

[Akismet](http://akismet.com/) is all the rage among the kids these days
for blocking comment spam.  Started by the founder of Wordpress, [Matt
Mullenweg](http://photomatt.net/), Akismet is a RESTful web service used
to filter comment spam.  Simply submit a comment to the service and it
will give you a thumbs up or thumbs down on whether it thinks the
comment is spam.

In order to use Akismet you need to sign up for a [free non-commercial
API key](http://akismet.com/personal/) with WordPress and hope that your
blog engine supports the Akismet API.

There are already two Akismet API implementations for ASP.NET, but they
are both licensed under
the [GPL](http://www.gnu.org/copyleft/gpl.html) which I won’t allow near
[Subtext](http://subtextproject.com/) (for more on open source licenses,
see [my series on the
topic](https://haacked.com/archive/2006/01/24/DevelopersGuideToOpenSourceSoftwareLicensing.aspx)).

So I recently implemented an API for Akismet in C# to share with the
[DasBlog](http://www.dasblog.net/) (*despite the bitter public
mudslinging between blog engines, there is nothing but hugs behind the
scenes.*) folks as part of the Subtext project, thus it is [BSD
licensed](http://www.opensource.org/licenses/bsd-license.php).

You can [download the assembly and source
code](http://tools.veloc-it.com/tabid/58/grm2id/15/Default.aspx) and
take a look.  It is also in the Subtext Subversion repository.

