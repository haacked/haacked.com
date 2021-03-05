---
title: MonkeySpace shines a light on the future of .NET OSS
tags: [oss,code,community]
redirect_from:
- "/archive/2012/10/19/monkeyspace-dotnet-oss.aspx/"
- "/archive/2012/10/20/monkeyspace-dotnet-oss.aspx/"
---

At the end of last year, I [wrote a
blurb](https://haacked.com/archive/2011/12/26/oss-net-2011.aspx "OSS and .NET Year in Review")
about the Open Source Fest event at Mix 2011. Imagine the typical
exhibition hall, but filled with around 50 open source projects. Each
project had a station in a large room where project members presented
what they were working on to others. You could see the gleam of
inspiration in the smiles of developers as they exchanged ideas and
suggestions. I left this event completely fired up.

This is the spirit we tried to capture with the
[MonkeySpace](http://monkeyspace.org "MonkeySpace") conference this
year. And at least for me, it succeeded. I’m fired up! There was much
sharing of ideas, some of which I’ll talk about in subsequent blog
posts. One such idea is I hope we add something like the open source
exhibition hall in a future MonkeySpace.

[![monkeyspace-spaker-dinner](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/MonkeySpace-and-the-Futu.NET-Open-Source_121F1/monkeyspace-spaker-dinner_thumb.jpg "monkeyspace-spaker-dinner")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/MonkeySpace-and-the-Futu.NET-Open-Source_121F1/monkeyspace-spaker-dinner_2.jpg)*MonkeySpace
speakers dinner. Lots of idea sharing going on.*

MonkeySpace is a rebranded and refocused Monospace conference. While
MonoSpace dealt mostly with Mono, the goal of Monkeyspace is to put the
spotlight on .NET open source everywhere, not just on Mono. Obviously
Mono is a big part of that. But so is Microsoft. But most of all, the
many small “labor of love” projects from those in the .NET OSS community
are a big part of this.

MonkeySquare
------------

Before I go further, I want to briefly describe the relationship of
MonkeySpace to me, Mono, and
[MonkeySquare](http://monkeysquare.org "MonkeySquare") (*the website is,
as the cliché goes, under construction*).

As I mentioned already, MonkeySpace is a rebranded Monospace conference,
but with a new focus. [Dale Ragan](http://ragan.io/ "Dale Ragan") and
others created a non-profit called MonkeySquare with the goal to
“evangelize cross platform and open-source development in .NET.”

I got involved when Dale asked me to be on the board of directors for
the organization. I admit, I was a bit hesitant at first as I tend to
overcommit and I need my afternoon naps, but the mission resonated with
me. I suggested he invite [Scott
Hanselman](http://hanselman.com/blog "Hanselman's Blog") because he’s a
force to be reckoned with and a fountain of ideas. And he has a big
forehead which has to count for something.

As we started to have board meetings, it became clear that we wanted the
next MonoSpace to become MonkeySpace. Due to the herculean efforts of
Dale Ragan and others like Joseph Hill of Xamarin, this became a
reality. We kept the first MonkeySpace small, but we hope to grow the
conference as a premier place for .NET open source developers to share
ideas and grow the ecosystem.

Pessimism turns to optimism
---------------------------

In recent times, there’s been some pessimism around .NET open source.
There’s the occasional rustle of blog posts declaring that someone is
“leaving .NET”. There’s also this perception that with Windows 8, the
Windows team is trying its best to relegate .NET into the dustbin of
legacy platforms. I don’t necessarily believe that to be the case
(*intentionally*), but I do know that many .NET developers feel
disillusioned.

But here’s the thing. **The .NET ecosystem is becoming less and less
solely dependent on Microsoft and this is a good thing.**

OSS Fills the Gaps
------------------

An example I like to point to is when WPF was first released, support
for presentation separation patterns (such as MVP or MVC) was
non-existent and there was much gnashing of teeth. It didn’t take long
before small open source projects such as [Caliburn
Micro](http://caliburnmicro.codeplex.com "Caliburn Micro") sprung into
existence to fill in the gaps. This by no means excuses some of the
design choices that still plague developers who want to write testable
WPF applications, but it certainly makes a bad situation much better.

In his keynote at MonkeySpace, [Miguel de
Icaza](http://tirania.org/blog/ "Miguel de Icaza") told a story that is
another dramatic illustration of this phenomena. Microsoft XNA is a
toolkit for building PC and X-Box games using .NET. But for whatever
reasons, Windows RT does not support running XNA applications. You can
still write an XNA game for Windows 8, but it won’t run on the Windows
RT devices.

Once again, the open source community comes to the rescue with
[MonoGame](http://monogame.codeplex.com/ "MonoGame on CodePlex"). Here’s
a blurb from their project page, emphasis mine:

> MonoGame is an Open Source implementation of the Microsoft XNA 4
> Framework. Our goal is to allow XNA developers on Xbox 360, Windows &
> Windows Phone to port their games to the iOS, Android, Mac OS X, Linux
> and **Windows 8 Metro**.  PlayStation Mobile development is currently
> in progress.

Interesting! MonoGame makes it possible to take your existing XNA based
X-Box game and with a tiny bit of effort, port it on Windows RT. Think
of the implications.

A cornerstone of the Windows 8 strategy is to entice a new developer
audience, web developers, to build client Windows applications with a
development experience that allows them to leverage their existing
JavaScript and HTML skills. *Nevermind the fact that they ignore the
culture and idioms of these communities, replacing such idioms with
Windows specific conventions that are awkward at best.*

Ironically, something like MonoGame may be better positioned to realize
this strategy for Microsoft in the short term than Microsoft’s own
efforts!

As an example, Miguel talked about the developers of Bastion, an X-Box
and PC game, and [how they used MonoGame to port the game to the
iPad](http://blog.xamarin.com/2012/08/30/supergiant-games-uses-xamarin-for-bastion-ipad-app/ "Bastion ported to iPad").
Now that same developer could easily port the game to Windows RT should
they choose to. Before MonoGame, they might not have considered this
option.

Miguel suggested that the majority of applications in the Windows 8 app
store are C# applications. This only makes sense because the attempt to
bring over web developers is a long lead strategy. Meanwhile, C#
developers have been Microsoft’s bread and butter for a while now and
are Microsoft’s to lose. They should not take them lightly and one would
hope, if these numbers are true, that this has the attention of the
Windows folks.

It certainly makes me excited about the potential for C# to become, as
Miguel calls it, the lingua franca for cross device applications.

So despite the pessimism, my discussions at MonkeySpace leave me
optimistic that .NET and .NET OSS is here to stay. There’s a lot of good
OSS stuff coming from Xamarin, independents, and various teams at
Microsoft such as the efforts from the MS Open Tech initiative and all
the stuff that the Windows Azure team is churning out. But even if
Microsoft started to deemphasize .NET, I believe .NET would endure
because the community will continue to fill in the gaps so that the
ecosystem abides.

