---
layout: post
title: The SuperSonic Subtext Build Server
date: 2007-05-17 -0800
comments: true
disqus_identifier: 18316
categories: []
redirect_from: "/archive/2007/05/16/the-supersonic-subtext-build-server.aspx/"
---

Real quickly, check out our [brand spanking new build
server](http://build.subtextproject.com/ccnet/ViewFarmReport.aspx "Subtext Build Server").
Notice anything different? No? Good! Hopefully everything is working
just fine, but faster.

As you know, I’m ever the optimist. What’s that trite phrase, “When the
crap hits the fan, make lemonade”? Or something like that.

So in this *tragedy becomes triumph* story, the bricking of my tiny
little home [built build
server](http://haacked.com/archive/2007/04/24/the-death-of-the-subtext-build-server.aspx "Death of the Subtext Build Server")
caused me to start thinking of a more permanent solution. In steps [Eric
Kemp](http://monk.thelonio.us/Default.aspx "monk.thelonio.us"), [Rob
Conery’s](http://blog.wekeroad.com/ "Rob Conery’s Blog") right hand man
(in the clean sense of the idiom) on the
[Subsonic](http://codeplex.com/actionpack "Subsonic") team.

He converted the VMWare image to run on Virtual Server and is hosting
our virtual build server on a pretty hefty machine. Finally, I can shut
down the virtual machine running on my desktop.

Eric ended up moving the server twice before settling on a final
location. Eric, if you have a moment, remind me what the specs are on
that baby.

So now it is time for me to step in with my part of the bargain, which
is to help the Subsonic team get a continuous integration setup going.
Now that their code is hosted in a Subversion repository, this will be a
lot easier than it would’ve been before.

Even so, I’ll probably look at using
[CIFactory](http://www.cifactory.org/ "CIFactory") and hopefully enlist
the help of an [Software Configuration Management
Ecowarrior](http://jayflowers.com/WordPress/?p=149 "Software Configuraiton Management Ecowarrior"),
aka [Jay Flowers](http://jayflowers.com/joomla/ "Jay Flowers").

