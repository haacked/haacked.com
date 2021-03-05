---
title: Proposed Extensions To The XFN Microformat
tags: [microformats]
redirect_from: "/archive/2006/09/13/Proposed_Extensions_To_The_XFN_Microformat.aspx/"
---

[![Source:
http://www.hollywoodjesus.com/movie/friends/20.jpg](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ProposedExtensionsToTheXFNMicroformat_121EE/CastOfFriends_thumb.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ProposedExtensionsToTheXFNMicroformat_121EE/CastOfFriends2.jpg)
If you’ve read my blog you know I have a bit of a [thing for
Microformats](https://haacked.com/archive/2006/05/11/IntroductionToMicroformatsArticle.aspx). 
I once wrote a little [special effect
script](https://haacked.com/archive/2006/04/05/MakingMicroformatsMoreVisibleAnnouncingTheXFNHighlighterScript.aspx)
to highlight links to your friends when marked up using the [XFN (XHTML
Friends Network) Microformat](http://gmpg.org/xfn/) used to denote
relationships to people you link to.

Ever since I wrote and started using this script, I ran into a bit of
interpersonal angst everytime I would link to someone.  Every
link spurred the following internal dialog.

*Do I mark so-and-so as a friend or acquaintance?  Well we’ve never met
but I think he’d consider me a friend. But would it be presumptuous if I
classified him as a friend.  What if I mark him as a friend and he links
to me as an acquaintance?  I would be crushed!  But what if I link to
him as an acquaintance and he considers me a friend.  **Some feelings
could be hurt!***

By now you probably think I have some serious issues (very true) and am
being overly paranoid.  But check out [Scott
Hanselman’s](http://computerzen.com/)
[response](https://haacked.com/archive/2006/04/05/MakingMicroformatsMoreVisibleAnnouncingTheXFNHighlighterScript.aspx#12277)
when I metadata’d him as an acquaintance. He called me a
dick!  \*sniff\* \*sniff\* Ouch!  Well technically he used well formed
markup (no namespace declared) to make that point, which softened the
impact, but only slightly.

I have since realized that the standard XFN relationships are not
granular enough to capture the nuances of real world relationships.  To
save others from such social insecurity and XFN relationship angst, I
humbly propose some new relationships I think should be added to the
format.  For your reference, the current list is [located
here](http://gmpg.org/xfn/11).  I will group by proposed additions in
the appropriate existing categories.

### Friendship

homie 
:   This helps distinguish someone who is just a friend to someone you
    actually hang out and throw down beers with. Possible alternative
    would be *buddy*. Often Symmetric.
friend-with-benefits 
:   As a [very happily married
    man](https://haacked.com/archive/2006/09/12/Four_Good_Years_And_The_Prospect_Of_Forty_More.aspx),
    I have no use for this, but maybe you do, tiger. Hopefully
    Symmetric.
frend-4-evers 
:   My market research indicates that Microformat usage is not very
    popular among the pre-teen Myspace crowd. This one is an attempt to
    reach that market. The format would have to allow alternate
    capitalizations and spellings for this one such as *fReNd-4-eVA*.
    Usually Symmetric or it gets really ugly.

### Physical

fought 
:   Currently the XFN profile only contains one value for the *Physical*
    category. I figured you need at least two to make it its own
    category (FxCop rule \#37142). Let everyone know the two of you got
    into a fist fight over the merits of Ruby on Rails. Symmetric.

### Romantic

dumped 
:   Let everyone know, that regardless of what that filthy **\*\#@!!**
    said, it was *you* who dumped *him/her!* Inverse of *dumped-by*.
~~dumped-by~~ 
:   Update: [Ben Ward](http://ben-ward.co.uk/) points out that dumped-by
    is not necessary. To indicate a reverse relationship, use
    `rev="dumped"` instead. This usage is popular among whiny
    songwriters and adolescents who love to live in that moment of pain.
    Inverse of *dumped*.

### Identity

split-personality 
:   Again, invoking FxCop rule \#37142, The *Identity* category needs
    another value. This lets others know that you are linking to a
    website created by your other personality. Symmetric and Transitive.

I hope to submit this [Tantek](http://tantek.com/log/),
[Matthew](http://photomatt.net/), and [Eric](http://meyerweb.com/) for
their consideration. Unfortunately I have a few strikes against this
proposal becoming accepted.

For example, there’s this point on the [background
page](http://gmpg.org/xfn/background) of the XFN site.

> Negative relationship terms have been omitted from XFN by design. The
> authors think that such values would not serve a positive ends and
> thus made the deliberate decision to leave them out. Such terms (we
> won't even bother naming them here) while mildly entertaining in a
> dark humor sort of way, only serve to propagate negativity.

There’s also this point on the same page.

> XFN values are by implication present tense.
>
> We have chosen to omit any temporal component for the sake of
> simplicity.

So yes, it appears I have my work cut out for me as many of my proposed
additions completely violate the spirit and guidelines of XFN.  But that
is a minor quibble I’m sure we can resolve with your help. Thank you and
good night.

