---
title: What To Do When CVS Fails to Create Lock Directory
tags: [code]
redirect_from: "/archive/2005/05/03/what-to-do-when-cvs-fails-to-create-lock-directory.aspx/"
---

I'm posting this in the hopes that it helps someone out there with the
same problem because I just KNOW how often you're adding files using
CVS. It's the little spark that gets your juices flowing while sitting
at work daydreaming about how much you can't wait to use CVS again.

Anyways, I was creating a new CVS module and adding the contents of some
source code to the repository when I heard the sound of breaking glass
that [TortoiseCVS](http://www.tortoisecvs.org/) uses to indicate a
problem (it's a rather lovely sound).

It seems I had some sort of network issue while adding files. No
problem, I thought, I'll just add the files that haven't been added and
then commit all the files.

Unfortunately, every time I tried commiting files I heard that dreaded
breaking glass. My error message was something like:

> cvs commit: failed to create lock directory for
> \`/cvsroot/MyProject/SomeDirectory'\
>  (/cvsroot/MyProject/SomeDirectory/\#cvs.lock):\
>  No such file or directory\
>  cvs commit: lock failed - giving up\
>  cvs [commit aborted]: lock failed - giving up

Well that's strange, the error message is having trouble creating a lock
for the directory "SomeDirectory" because it doesn't exist. But when I
use Tortoise CVS to add contents, it shows that everything has been
added.

What I discovered is that CVS stores information about the state of the
repository in local hidden folders named, you guessed it, "CVS". If
something very bad happens, it's quite possible that the local
information will get out of synch with the information on the server.
That's exactly what I ran into.

To fix it, I copied my source tree to a new directory, deleted the old
source tree, and ran a *Checkout* command to get the latest version that
actually made it into the repository. Then I copied that set aside
source tree over the one I had just checked out so that all the new
files made it into the tree. That then showed that I still needed to add
some files and directories to the CVS repository, which I did. Finally,
a commit of the source tree worked flawlessly.

By the way, I'm a relative CVS rookie, though I've used it quite a bit.
So if there was a better way to do this, let me know.

For a great tutorial on source control, check out Eric Sink's series
[Source Control
HOWTO](http://software.ericsink.com/scm/source_control.html).

[Listening to: Chinese Burn (Forbidden City Remix) - Paul Van Dyk -
Perspective CD2 (10:36)]

