---
title: 'Tech-Ed 2004: Don Box and Doug Purdy talk about Connected Systems.'
date: 2004-05-25 -0800
disqus_identifier: 484
tags: [conferences]
redirect_from: "/archive/2004/05/24/tech-ed-2004-don-box-and-doug-purdy-talk-about-connected-systems.aspx/"
---

It's probably too early to tell, but [Don Box](http://www.gotdotnet.com/team/dbox) and [Doug
Purdy](http://www.douglasp.com/default.aspx) gave what I bet will be the most interesting talk I will hear at Tech-Ed. This was the opening talk for the Connected Systems track which focuses on that hot TLA, SOA (Service Oriented Architecture).

The implications of The Program is that we are constantly adding code to it while it still runs, but we can't reboot it. We can take parts of it offline and replace parts, but there are parts (and decisions) within The Program that we may have to live with forever.

One of the big revelations they had was that **"There is only one program and it is still being written."** This has pretty profound implications when you think of it. I can attest to this statement as I recently worked on porting Fortran code running on a microcomputer to Fortran 90 code (big upgrade!) that was to run on a modern Intel server. The front end was being replaced by an ASP.NET site that emulated the console currently in use. The ASP.NET site communicates with the Fortran via memory mapped files.

**"Choice is an Illusion"** Darwin is the system admin for The Program. You can't change the program, you only have control over a small sliver of it. Your goal is to make your neighborhood better. This addresses the initiatives in the industry to create grand unification architectures. The physicists are still looking for a Grand Unifying Theory, but for software architects, it's time to give that up. We're not all going to run on a JVM. There will be no language to rule them all. We've tried DCOM, CORBA, etc...

In any case, this keyboard is not ergo so I'm going to have to stop typing and let you research this one on your own. Till next time, over and out.
