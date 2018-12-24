---
title: Laptop Warmer Anecdote
date: 2006-09-22 -0800
tags: [humor]
redirect_from: "/archive/2006/09/21/laptop_warmer_anecdote.aspx/"
---

I saw [this story](http://www.qdb.us/53151) on the [debugging section of
Anecdota](http://www.anecdota.org/debugging/) and thought it was funny,
though I find it hard to believe.

> ### Laptop warmer {.post-title}
>
> In 1998, I made a C++ program to calculate pi to a billion digits. I
> coded it on my laptop (Pentium 2 I think) and then ran the program.
> The next day I got a new laptop but decided to keep the program
> running. It’s been over seven years now since I ran it. and this
> morning it finished calculating. The output:
>
> “THE VALUE OF PI TO THE BILLIONTH DIGIT IS = ”
>
> Mindblowing eh? I looked in the code of my program, and I found out
> that I forgot to output the value.

You would think he’d do a test run for smaller digits of PI, but I’ve
done things like that.  You make a small test run. It works. You make a
tiny tweak that shouldn't affect anything and then start it running
because you're in a hurry.  Seven years later...

Of course, most (if not all) algorithms for calculating PI aren’t all or
nothing.  Usually they start calculating digits immediately, so
there ought to be immediate output as you calculate PI to further and
further digits, unless this person decided to store all billion digits
in a string before displaying it.

