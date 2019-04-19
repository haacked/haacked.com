---
title: Who Owns the Copyright for An Open Source Project
date: 2006-01-26 -0800 9:00 AM
tags: [oss,legal,licensing,copyright]
redirect_from: "/archive/2006/01/25/WhoOwnstheCopyrightforAnOpenSourceProject.aspx/"
---

This is part 3 in this series on copyright law and open source
licensing. If you haven’t already, consider reading [Part
1](https://haacked.com/archive/2006/01/24/TheDevelopersGuideToCopyrightLaw-Part1.aspx "Part 1 of the series")
and [Part
2](https://haacked.com/archive/2006/01/24/DevelopersGuideToOpenSourceSoftwareLicensing.aspx "Part 2 of the series")
of this series for background before tackling this topic.

To properly license open source source code, the license agreement must
be included prominently with the source code. Many simply put a
license.txt file in the root of the source tree and publish the license
on their project website. Others take the extra step to include the
license text in a comment within every source file.

## Assigning Copyright

Take a [look at the license for
DotNetNuke](http://www.dotnetnuke.com/Downloads/tabid/125/Default.aspx)
(DNN), a very popular open source portal software for the .NET platform.
In particular note the very first section:

>     DotNetNuke® - http://www.dotnetnuke.com
>     Copyright (c) 2002-2005
>     by Perpetual Motion Interactive Systems Inc. 
>     ( http://www.perpetualmotion.ca )

Notice that Perpetual Motion Interactive Systems Inc. owns the copywrite
to the DotNetNuke codebase. “How can that be? How can a corporation own
the copyright to code that is open source?” you might ask. Don’t worry,
there is nothing sinister going on here.

In part 1 of this series I stated that when you write code, you own the
copyright to it (with a couple of exceptions such as work for hire). By
default, when you contribute source code to an open source project, you
are agreeing to license the code under the terms of that project, but
you still retain the copyright.

In some cases, this is fine. But it makes it difficult for the project
should they decide they want to relicense or dual-license the project as
they have to get permission from every copyright holder. Or if there is
need to enforce the copyright, the project would need every affected
copyright holder to be involved.

What many projects do is require that contributors assign copyright to a
single legal entity or person which then has the power to enforce the
copyright without requiring everybody get involved. Keep in mind that
although this person or entity then owns the copyright, the code has
been released under a license that allows free distribution. Thus the
fact that the copyright has been assigned to an individual entity does
not make the code any less open.

For example, suppose Perpetual Motion decides they want to exercise
their copyright and make a proprietary version of DotNetNuke. They
certainly have the right to do so, but they cannot stop others from
freely viewing and distributing the code under the pre-existing license
up to the point at which they close the source. At that point,
contributors would be free to fork the project and continue development
under the original license as if nothing had occurred.

## Copyright Assignment Policy

According to a lawyer Fogel talked with...

> For most, simply getting an informal statement from a contributor on
> the public list is enough—something to the effect of “I hereby assign
> copyright in this code to the project, to be licensed under the same
> terms as the rest of the code.”

Is sufficient. Some organizations such as the Free Software Foundation,
on the other hand, apply a very formal process requiring users to sign
and mail in paperwork.

In any case, some open source projects do not have such a copyright
assignment policy in place, but it makes sense to do so. As Fogel points
out, should the terms of the copyright need to be defended, it is much
easier for a single entity to do so rather than relying on the
cooperation of the entire group of contributors, who may or may not be
available.

Having the copyright assigned to a corporation also protects individual
developers from exposure to liability in the case of a copyright
infringement suit.

Public Domain
-------------

*This section added in 2013/07/17*

Another possibility that I did not originally cover is to have nobody
own the copyright by dedicating a work to the [public
domain](http://en.wikipedia.org/wiki/Public_domain "Public Domain").
This effectively surrenders any rights to the code and gives the code to
the public to do with it as they wish.

In some regards, dedicating a work to the public domain is unusual.
Typically, works in the public domain have had their copyright expired
or were inapplicable (such as government works). As Wikipedia points
out:

> Few if any legal systems have a process for reliably donating works to
> the public domain. They may even prohibit any attempt by copyright
> owners to surrender rights automatically conferred by law,
> particularly [moral
> rights](http://en.wikipedia.org/wiki/Moral_rights).

The [Unlicense](http://unlicense.org/ "Unlicense") template is one
approach to dedicating a work to the public domain while serving as a
fallback “license” for legal systems that don’t recognize public domain.
The [CC0 license](http://creativecommons.org/choose/zero/ "CC0") from
Creative Commons is another such effort.

