---
title: Online Games Are Written By Humans
date: 2005-08-29 -0800 9:00 AM
tags: [gaming,security]
redirect_from: "/archive/2005/08/28/online-games-written-by-humans.aspx/"
---

Remember that online games are written by humans and thus are subject to
the bugs and flaws that humans are so good at introducing.

This was made quite evident by an article for the [current issue of
2600: The Hacker Quarterly](http://store.2600.com/summer2005.html) that
a former coworker of mine wrote. It’s an interesting read and I
encourage you to check it out, though it is only in print on dead trees.

In this article he describes a flaw that became apparent to him within a
newly released BlackJack game on the Paradise Poker website. In
BlackJack, when the dealer is showing an ace, the dealer offers the
players the option to purchase insurance. This is a way for the players
to pay to cut their losses should the dealer have ten (10, Jack, Queen,
or King) in the hole.

On this particular online game, he noticed that when the dealer did have
a pocket ten, there would be a noticeable pause before he was prompted
with the Insurance request. When there wasn’t a pocket ten, the prompt
appeared immediately.

After doing some quick calculations, he realized this bit of information
gave him an edge over the house. He ended up playing the next seven
hours exploiting this bug and made a nice chunk of change during that
time.

Obviously I don’t know what caused the flaw in the game, but my guess is
that there was some calculation the system needed to make to determine
whether or not to offer insurance. That calculation may have taken more
time to perform in the situation the dealer had a ten.

Let’s pretend I am right (not a huge stretch as I am always right) and
think about that for a sec. The code itself may have been completely
correct in the sense that it did what it was supposed to do. It was the
amount of time the code needed to execute that ended up being the tell.
No different than when a poker player twitches when holding a great
hand.

The fix may have been to change the **execution profile** of the code so
that it made the same pause no matter what was in the hole. Talk about a
challenge for game developers. Not only does the code need to be bug
free in syntax and semantics, but they now need to worry about the
execution profile for their games.

Who knows if there are several other timing flaws like this in other
games. It didn’t even require my friend to hack into anything. He simply
observed the timing disparity. Now imagine if he was running a timing
program specifically designed to look for other timing flaws. Something
that would notice discrepancies down to the millisecond.

