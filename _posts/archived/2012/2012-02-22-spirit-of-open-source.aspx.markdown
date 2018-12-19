---
title: What Is The Spirit of Open Source?
date: 2012-02-22 -0800
disqus_identifier: 18848
categories:
- code
- oss
redirect_from: "/archive/2012/02/21/spirit-of-open-source.aspx/"
---

[In my previous
post](https://haacked.com/archive/2012/02/16/open-source-and-open-source-software-are-not-the-same.aspx "My Previous Post"),
I attempted to make a distinction between Open Source and Open Source Software. Some folks took issue with the post and that’s great! I love a healthy debate. It’s an opportunity to learn. One minor request though. If you disagree with me, I do humbly ask that you read the whole post first before you go and rip me a new one.

It was interesting to me that critics fell into two opposing camps. There were those who felt that it was was disingenuous for me to use the term “open source software” to describe a Microsoft project that doesn’t accept contributions and is developed under a closed model, even if it is licensed under an open source license. Many of them accepted that, yes, ASP.NET MVC is OSS, but I still shouldn’t use the term.

While others felt that the license is the sole determining factor for open source and I wasn’t helping anybody by trying to expand the
definition of “open source.” To my defense, I wasn’t trying to expand it so much as describe how I think a lot of people use the term today, but they have a good point.

Going back to the first camp, a common refrain I heard was that software that meets the [Open Source
Definition](http://opensource.org/docs/osd "Open Source Definition") might “meet the letter of the law, but not the spirit of the law” when it comes to open source.

Interesting. But what is the “spirit of open source” that they speak of? What is the **essential** ingredient?

Looking For The Spirit
----------------------

I assume they mean developing in the open and accepting contributions to be necessary ingredients to qualify a project as being in the spirit of open source. I started to dig into it. I expected that we should probably see references to these things all over the place when we look up the term “open source”.

Oddly enough, Wikipedia doesn’t really talk about those things in its [article on Open
Source](http://en.wikipedia.org/wiki/Open_source "Open Source"), but hey, it’s Wikipedia.

But oddly enough, there’s no mention of accepting contributions in the [Open Source Definition](http://www.opensource.org/docs/osd "Open Source") or pretty much anywhere in [http://opensource.org/](http://opensource.org/) that I could find. It doesn’t really address it.

Neither does any open source license have anything to say about the way the software is developed or whether the project accepts contributions.

Let’s take a look at the Free Software Foundation which is opposed to the term “Open Source” and might have a different [definition of free software](http://www.gnu.org/philosophy/free-sw.html "Free Software"). I’m going to quote a small portion of this.

> Thus, “free software” is a matter of liberty, not price. To understand
> the concept, you should think of “free” as in “free speech,” not as in
> “free beer”.
>
> A program is free software if the program's users have the four
> essential freedoms:
>
> -   The freedom to run the program, for any purpose (freedom 0).
> -   The freedom to study how the program works, and change it so it
>     does your computing as you wish (freedom 1). Access to the source
>     code is a precondition for this.
> -   The freedom to redistribute copies so you can help your neighbor
>     (freedom 2).
> -   The freedom to distribute copies of your modified versions to
>     others (freedom 3). By doing this you can give the whole community
>     a chance to benefit from your changes. Access to the source code
>     is a precondition for this.

I looked for interviews from the pioneers of open source for more information. Richard Stallman reiterates the same points [in this
interview](http://itmanagement.earthweb.com/osrc/article.php/3717476/Interview-with-Richard-Stallman-Four-Essential-Freedoms.htm "Software's Four Essential Freedoms"). What about [Eric Raymond](http://www.catb.org/~esr/ "Eric Raymond")? Well he just links to [http://opensource.org/](http://opensource.org/). As you can see, he’s the [President Emeritus](http://opensource.org/ "Open Source Board") of the Open Source Initiative (OSI) which created the OSD that I’ve been using as my definition.

I then asked [Miguel De Icaza](http://tirania.org/blog/ "Miguel De Icaza's blog") for his thoughts. Miguel is a developer with [a long history in open source](http://en.wikipedia.org/wiki/Miguel_de_Icaza "Miguel on Wikipedia"). He started the GNOME and Mono projects and has more open source experience in his pinky than I have in my entirety. He had some interesting insights.

> In general, I am not sure where the idea came from that for something
> to be open source, the upstream maintainer had to take patches, that
> has never been the case.   Some maintainers are just too protective
> (qmail I believe for a long time did not take patches, or even engage
> in public discussions).   Others are just effectively too hard for
> average developers to get patches in (Linux kernel, C compilers) that
> they are effectively closed.

What gives?
-----------

Isn’t it odd that these concepts that we hold so dearly to as being part of the spirit of open source aren’t mentioned by these stewards of open source?

Maybe it’s because the essential ingredient to open source, the spirit of open source, is not accepting contributions, it’s **freedom**.

The freedom to look at, remix, and redistribute source code without fear of recrimination.

Even so, I still think accepting contributions and developing in the open **are hugely important** to any open source project. If I didn’t believe that, I wouldn’t be working at [GitHub](http://github.com/ "GitHub").

I started to think that perhaps a more apt term to describe that process is [**crowd sourcing**](http://en.wikipedia.org/wiki/Crowdsourcing "Crowd sourcing"). Crowd sourcing can provide many benefits, according to the article I linked to:

> -   Problems can be explored at comparatively little cost, and often
>     very quickly.
> -   Payment is by results or even omitted.
> -   The organization can tap a wider range of talent than might be
>     present in its own organization.
> -   By listening to the crowd, organizations gain first-hand insight
>     on their customers' desires.
> -   The community may feel a brand-building kinship with the
>     crowdsourcing organization, which is the result of an earned sense
>     of ownership through contribution and collaboration.

But Miguel thought a better term is “open and collaborative development.” That’s the process that is so closely associated with
developing open source software that it’s become synonymous with open source in the minds of many people. But it’s not the same thing because it’s possible to conduct open and collaborative development on a non-open source project.

Splitting Hairs?
----------------

I know some folks will continue to think I’m splitting hairs, which is an impressive feat if you think about it as I definitely don’t have the hands of a surgeon and hairs are so thin.

One counter argument might be that perhaps the original framing of “open source” was focused on freedoms, but what we refer to as open source today has evolved to include crowd sourcing as an essential component.

I can see that. It seems obvious to me that collaborative development in the open is a huge part of the culture surrounding open source. But being a core part of the culture doesn’t necessarily mean it’s in the spirit. A lot of people feel that drugs are a big part of the Burning Man culture, for example, but I don’t think it’s an essential part of its spirit. It’s the creativity and expression that forms the spirit of Burning Man.

Who Cares About The License?
----------------------------

Another point someone made is that the community of contributors is more beneficial than having an open source license. I addressed that point in my last post, but Miguel had a great take on this, emphasis mine.

> The reason why the OSI definition was important is because it provided
> a foundation that allowed unlimited redistribution and serviceability
> for the code **for the future**, with the knowledge that there were no
> legal restrictions.  This is the foundation for Debian's policies and
> in general, the test that must be passed for a project to be adopted. 
> Serviceability does not require talking to an upstream maintainer, it
> means having the means and rights to do so, even if the upstream
> distribution goes away, vanishes, dies, or moves on to greener
> pastures.

You could build the most open and collaborative project ever, but if the source code isn’t under a license that meets the open source definition, it may be possible for project to close shop and withdraw all your rights to the code.

With OSS code, that’s just not possible. A copyright holder of the source can close shop and stop giving you access to new addition they write, but they can’t retroactively withdraw the license to code they’ve already released under an OSI certified license.

And this is a real benefit, even with otherwised “closed” projects that are open source. Miguel gave me one example in relation to Mono.

> A perfect example is all the code that we have taken from Microsoft
> that they open sourced and ran with it. In some cases we modify
> it/tweak it (DLR, BigInteger), in others we use as-is, but without it
> being open source, we would be years behind and would have never been
> able to build a lot of extra features that we now depend on.

Prove me wrong?
---------------

By the way, I’m open to be proven wrong and changing my mind. Heck, I’ve done it twice this week as Miguel convinced me that “Open Source” only requires the license. But if you do disagree with me, I’d love to see references that back up your point as opposed to unsubstantiated name calling.

I think this topic is tricky because it’s very easy to discuss whether software is licensed under an open source license. If we agree on the open source definition, or the free software definition, you can easily evaluate that yes, the software gives you the rights and freedoms mentioned earlier.

But it’s trickier to create strict definitions of what encompasses the spirit of anything, because people have different ideas around them. I know some folks that feel that commercial software goes against the spirit of open source. I tend to disagree pointing out that it’s the license that matters, and whether or not you make money from the software is tangential. But I digress.

I know I won’t convince everyone of my points. That’s fine. I enjoy a healthy debate. The only thing I hope to convince you of is that even if you disagree with me, that you can see I’ve provided good reasons for why I believe what I do and it’s not out of being disingenuous.

Collaboration is Good
---------------------

In the end, I think it’s a huge benefit for any open source project to develop in an open collaborative manner. When I think of open source software, that’s the development model that comes first in my mind. But, if you don’t follow that model (perhaps for good reasons, maybe for bad reasons) but do license the software under an open source license, I will still recognize your project as an open source project.
