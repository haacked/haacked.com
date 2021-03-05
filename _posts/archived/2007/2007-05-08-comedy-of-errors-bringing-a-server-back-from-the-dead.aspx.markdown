---
title: Comedy Of Errors Bringing A Server Back From The Dead
tags: [personal]
redirect_from: "/archive/2007/05/07/comedy-of-errors-bringing-a-server-back-from-the-dead.aspx/"
---

Not too long ago I mentioned that a power surge [bricked the Subtext
Build
Server](https://haacked.com/archive/2007/04/24/the-death-of-the-subtext-build-server.aspx "The Death of the Subtext Build Server").
What followed was a comedy of errors on my part in trying to get this
sucker back to life. Let my sleep deprived misadventures be a cautionary
tale for you.

My first assumption was that the hard drive failed, so I ordered a new
Hard Drive.

**Lesson #1:** *If you think your hard drive has failed, it might not
be a bad idea to actually test it if you can. Don’t just order a new
one*!

I have my main desktop machine I could have used to test the drive, but
due to my sheer and immense laziness, I didn’t just pop the drive in
there as a secondary drive to test it out. I just ordered the drive and
moved on to other tasks.

Days later, the drive arrived and I popped it in and started to install
Ubuntu on the machine. As I got to the disk partitioning part, I noticed
that it found a disk and I went ahead and formatted the drive and
installed Ubuntu. Sweet! But when I rebooted, the server not find the
drive. Huh?

**[![The Scream - Edvard
Munch](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ComedyOfErrorsBringingAServerBackFromThe_781/300pxThe_Scream_thumb.jpg)](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/ComedyOfErrorsBringingAServerBackFromThe_781/300pxThe_Scream2.jpg "The Scream - Edvard Munch")
Lesson #2:** *When installing an Operating System on a machine, make
sure to unplug any external USB or Firewire drives.*

Yep, I formatted my external hard drive and installed Ubuntu on that.
The Ubuntu installation process recognized my firewire drive and offered
that as an available drive to partition and install. Ouch!

At this point, I realized that the machine was not detecting my brand
new hard drive, though I could hear the hard drive spin up when I
powered on the machine. I figure that quite possibly it’s a problem with
the SATA cable. So I order a new one.

**Lesson #3:** *In the spirit of lesson 1, why not just temporarily
pull a SATA cable from your other machine, if you have it.*

I thought the SATA cables were all inaccessible and would be a pain to
pull, but didn’t bother to check. It was in fact easy to grab one. To my
defense, I figured having extra SATA cables on hand wouldn’t be a bad
idea anyways and they are cheap.

So I plugged the SATA cable that I know to be good into the box and
still it won’t recognize the hard drive. At this point it seems pretty
clear to me that the drive controller on the Motherboard is fried. *Any
suggestions on how to fix this are welcome, if it is even possible.*

In any case, after a good night of sleep, I started doing the right
thing. I plugged the old drive into my desktop and sure enough, I can
copy all its files onto my main machine.

I installed VMWare server and the build server is now [up and
running](http://build.subtextproject.com/ccnet/ "Subtext Build Server")
on my main desktop for the time being. Woohoo!

As a side note, I tried to use this [VMDK (VMWare) to VHD (Virtual PC)
Converter](http://vmtoolkit.com/files/folders/converters/entry8.aspx "VMDK to VHD Converter")
(registration required) so I wouldn’t have to install VMWare Server on
my machine, but it didn’t seem to work. Has anyone had good luck
converting a VMWare hard disk into a Virtual PC hard disk?

**Long story short, do not under any circumstances let me anywhere near
your hardware.** At least the build server is back up and working fine.
It is officially time to subscribe to
[mozy.com](http://mozy.com/ "Mozy Online Backup"). I*’*m exhausted. Good
night.

