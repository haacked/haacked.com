---
title: New Subtext Release and Notes On Subtext 2.0
date: 2007-02-11 -0800
tags: [subtext]
redirect_from: "/archive/2007/02/10/New_Subtext_Release_and_Notes_On_Subtext_2.0.aspx/"
---

[Steve Harman](http://stevenharman.net/blog/ "Steve Harman") just
announced the release of *[Subtext version 1.9.4 Windward
Edition](http://stevenharman.net/blog/archive/2007/02/11/Subtext_v1.9.4_quotWindwardquot_Edition_Released.aspx "Windward edition release")*.
**This one comes with a lot of bug fixes, so be sure to upgrade**.

Just so you know things work, I add bugs to Subtext, and Steve and the
other developers fix the bugs. It’s a rather efficient ecosystem and
is working quite well for us. It keeps everyone on their toes.

Steve’s post has the full list of bug fixes and such. The most
interesting addition is that we’ve implemented a Google Sitemap, which
was submitted as a patch, if I remember correctly.

This release is notable because of the increased number of patch
submissions. I greatly appreciate the contributions by all the new
contributors along with the stalwarts. Working on Subtext is a joy
because of these people.

**As for Subtext 2.0, Progress has been slow, but steady**. One
challenge we’re dealing with is how to cleanly handle the following two
multi blog scenarios as summarized by
[Simo](http://www.codeclimber.net.nz/ "CodeClimber") in our mailing
list.

> ​1. The multi blog site is a communiy site: so user registered to a
> blog, are already registered for all blogs.
>
> ​2. The multi blog site is just a host for many different non related
> blogs: here all users are different, and even if 2 blogs are on the
> same system, the user shouldn’t know about that.

We pretty much can already handle case 1 which is nice because it means
the same user can be the owner of more than one blog, rather than
requiring a user account per blog. This is useful for me personally as I
host three blogs on a single installation, and right now, the admin is
technically three different accounts.

Strictly speaking, this implementation makes implementing the isolation
requested in case 2 difficult, because usernames must be unique. So if
there’s ever a name conflict between two users attempting to register
with the same username, the two blogs will affect each other and not be
completely isolated.

However, if we make the user’s email address their login, we can
implement case 2 in spirit. First of all, there won’t be two different
users using the same email address, so the naming conflict issue is
resolved.

Secondly, if you wish to register for blog 2, why should we make you
fill in your information again if we already know who you are? We
should simply present a message that says, *Hey, we know who you are, if
you want to register for this site, click here and we’ll auto register
you*.

That way, the user is in control over who which blogs get to see their
registration information, yet they only have one user account in the
system. Certainly there are some improvements we can make later, for
example, some blogs may want more information than others to register.

But for now, we only ask for the minimal amount of information and will
keep things simple and consistent across the board.

In any case, enough blabbing. If you want to download the latest
release, you can find it on [our SourceForge
site](https://sourceforge.net/projects/subtext/ "Subtext on SourceForge")
or just use this
[[DOWNLOAD](https://sourceforge.net/project/showfiles.php?group_id=137896 "Download Subtext 1.9.4")]
link.

