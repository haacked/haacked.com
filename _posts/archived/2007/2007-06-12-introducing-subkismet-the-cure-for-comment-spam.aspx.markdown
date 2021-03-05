---
title: Introducing Subkismet-The Cure For Comment Spam
tags: [spam]
redirect_from: "/archive/2007/06/11/introducing-subkismet-the-cure-for-comment-spam.aspx/"
---

*Update: I’ve created a new [NuGet
Package](https://haacked.com/archive/2010/10/06/introducing-nupack-package-manager.aspx "NuGet Package")
for Subkismet (Package Id is “subkismet”) which will make it much easier
to include this in your own projects.*

*Been a short break from blogging, but I’m ready to get back to writing
about*[*Cody*](https://haacked.com/archive/2007/06/06/introducing-cody-yokoyama-haack.aspx "Introducing Cody Yokoyama Haack")*,
I mean code!*

My philosophy towards Open Source Software is that the more sharing that
goes on between projects, the better off for everyone. As my friend
[Micah](http://micahdylan.com/ "Micah Dylan’s Blog") likes to say, *A
rising tide lifts all boats*.

Towards that end, I’ve tried to structure
[Subtext](http://subtextproject.com/) as much as possible into distinct
reusable libraries. The danger in that, of course, is the specter of
[premature
generalization](https://haacked.com/archive/2005/09/19/avoid_premature_generalization.aspx "Avoid Premature Generalization").

I haven’t always been successful at avoiding premature generalization
which has led me to focus on consolidating code into less assemblies
rather than more. **My focus now is to let actual reuse guide when code
gets pulled into its own library**.

However, there is some useful reusable code I’ve written that is already
in use by many others in the wild. This is code included in Subtext as
part of its defense system against comment spam. For example:

-   [Subtext Akismet
    API](https://haacked.com/archive/2006/09/26/Subtext_Akismet_API.aspx "Akismet Client for C#")
    - A .NET client for the
    [Akismet](http://akismet.com/ "Akismet by WordPress") comment spam
    service.
-   [Lightweight Invisible CAPTCHA Validator
    Control](https://haacked.com/archive/2006/09/26/Lightweight_Invisible_CAPTCHA_Validator_Control.aspx "Invisible CAPTCHA")
    - An invisible CAPTCHA.
-   Visible CAPTCHA - Your standard CAPTCHA control.

I contributed the Akismet code to the
[DasBlog](http://dasblog.info/ "Dasblog Blog Engine") team who I am sure
have made adjustments specific to their blog engine. **The challenge I
face now is how do I get any improvements they may have made back into
my own implementation?**

![a can of
no-spam](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IntroducingSubkismetTheCureForCommentSpa_13B94/no-spam_1.jpg)
To answer that, I created the [Subkismet
project](http://www.codeplex.com/subkismet/ "Subkismet Project"). It’s
more than just an Akismet client for .NET, it’s a library of SPAM
squashing code meant to be useful to developers who are building web
applications that require user input such as Blogs, Forums, etc...

So far it has the three mean features I mentioned, but these alone go a
long way to beating comment SPAM. In the future, I hope to incorporate
even more tricks for beating comment spam as part of this library.

Hopefully I can convince DasBlog (and others such as BlogEngine.NET and
ScribeSonic) to switch to Subkismet for their comment fighting support
and help me craft a great API useful to many. This falls in line with my
goal to have Subtext be an incubator for useful open source library code
that other projects will want to take advantage of.

### What’s With The Name?

I thought I should just use a nonsensical word that’s a play off of
Subtext and Akismet. Besides, the domain name was available (not yet
pointing anywhere).

### Hosting

I’ve decided to host [Subkismet on
CodePlex](http://www.codeplex.com/subkismet/ "Subkismet, the cure for comment spam"),
but with grave trepidations. Not too long ago, they had a major server
issue and lost the source code for the [.NET Identicon Handler
project](http://www.codeplex.com/Identicon/ "Identicon Handler for .NET")
I started with [Jeff Atwood](http://codinghorror.com/ "CodingHorror")
and [Jon Galloway](http://weblogs.asp.net/jgalloway/ "Jon Galloway").

Fortunately I had the source code on my machine so I was not terribly
affected, but this is a serious blow to my confidence in their service.
However, I do believe that CodePlex is great for small open source
projects (though [not yet convinced for large
ones](https://haacked.com/archive/2007/03/02/A_Comparison_of_TFS_vs_Subversion_for_Open_Source_Projects.aspx "A Comparison of TFS vs Subversion for Open Source Projects")
like Subtext) and I like their issue voting and wiki.

I’ll give them one more chance to impress me. Besides, this allow me to
really try out their [Subversion
bridge](https://haacked.com/archive/2007/05/21/codeplex-to-roll-out-tortoisesvn-support.aspx "CodePlex to roll out TortoiseSVN Support")
when they release it.

### Release Schedule

I’ve currently prepared a BETA release in order to get people using it
and to provide feedback. It should be stable code as I pulled it from
Subtext and cleaned it up a bit so it could be reused by others.

However, my next step is to refactor Subtext to reference this library
and see if any API usability issues come up. If you implement it
yourself, please let me know if you have any suggestions for
improvements.

Once I complete the refactoring and convince others to use it and
provide feedback, I’ll create a 1.0 release.

**Please try out**[**the latest
release**](http://www.codeplex.com/subkismet/Release/ProjectReleases.aspx "Subkismet Releases")
and give me feedback!

