---
title: The Misuse of the Space Shuttle Analogy
tags: [software,methodologies]
redirect_from: "/archive/2006/10/19/The_Misuse_of_the_Space_Shuttle_Analogy.aspx/"
---

[![Space Shuttle
Landing](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TheMisuseoftheSpaceShuttleAnalogy_C0E0/spaceshuttlelanding1_thumb2.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TheMisuseoftheSpaceShuttleAnalogy_C0E0/spaceshuttlelanding14.jpg)
[Jeff
Atwood](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/TheMisuseoftheSpaceShuttleAnalogy_C0E0/spaceshuttlelanding13.jpg "Jeff Atwood")
writes a great post about [The Last Responsible
Moment](http://www.codinghorror.com/blog/archives/000705.html "How to delay commitments").
Take a second to read it and come back here. I’ll wait.

In the comments, someone named Steve makes this comment:

> This is madness. Today’s minds have been overly affected by short
> attention span music videos, video games, film edits that skip around
> every .4 seconds, etc.
>
> People are no longer able to focus and hold a thought, hence their
> "requirements" never settle down, hence "agile development", extreme
> coding, etc.
>
> I wonder what methodology the space shuttle folks use.
>
> You shouldn’t humor this stuff, it’s a serious disease.

Ahhhh yes. The Space Shuttle. The paragon of software development.
Nevermind the fact that 99.999% of developers are *not* developing
software for the Space Shuttle, we should all write code like the
Shuttle developers. Let’s delve into that idea a bit, shall we?

Invoking the Space Shuttle is common when arguing against iterative or
agile methodologies to building software. Do people really think *hey,
if agile won*’*t work for the Space Shuttle, how the hell is it going to
work for my client*’*s widget tracking application?*Gee, your widget app
is doomed.

The Space Shuttle is a different beast entirely from what most
developers deal with day in and day out. This is not to say that there
aren’t lessons to be learned from how the Shuttle software is built,
[there certainly
are](http://www.fastcompany.com/online/06/writestuff.html "They Write the Right Stuff").
Good lessons. No, great lessons! But in order to make good use of the
lessons, you must understand how your client is very different from the
client to the Shuttle developers.

One reason that the requirements for the Space Shuttle can be more
formally specified beforehand and up front is because *the requirements
have very little need to chang*once the project is underway*.*When was
the last time the laws of gravity changed? The Shuttle code is mostly an
autonomous system, which means the “users” of the code is the Shuttle
itself as well as the various electronic and mechanical systems that it
must coordinate.  These are well specified systems that do not need to
change often, if at all, in the course of a project.

Contrast this to business processes which are constantly evolving and
heavily people centric. Many times, the users of a system aren’t even
sure about how to solve the business problem they are trying to solve
with software.  This is partly why they don’t exactly know what they
want until they have the solution in hand. We can wave activity
diagrams, list of requirements, user stories and use cases in front of
them all day long, but these are rough approximations of what the final
system will do and look like. It’s showing the user a pencil sketch of a
stick figure and hoping they see the Mona Lisa.

Later in the comments, the same guy Steve responds with what we should
do with users to focuse them.

> ​1) what do you want it to do?\
> 2) understand the business as much as you can\
> 3) draw a line in the sand for that which you can safely deliver
> pretty soon\
> 4) build a system that is extensible, something that can be added on
> too fairly easily, because changes are coming (that is agile-ness)\
> 5) charge for changes

And I agree. One of the common misperceptions of agile approaches is
that you never draw a line in the sand. This is flat out wrong. You do
draw a line in the sand, but you do it every iteration.

Unlike the BDUF Waterfall approach which requires that you force the
user to spew requirements until he or she is blue in the face, with
iterative approaches you gather a list of requirements and prioritize
them according to iterations.  This helps a long way to avoiding poor
requirements due to design fatique. The user can change any requirements
for later iterations, but once an iteration has commenced, the line in
the sand is drawn for that iteration.

To me, this sounds like the last responsible moment for deciding on a
set of requirements to implement. You don’t have to decide on the entire
system at the beginning. You get some leeway to trade requirements for
later iterations. You only are forced to decide for the current
iteration.

 

