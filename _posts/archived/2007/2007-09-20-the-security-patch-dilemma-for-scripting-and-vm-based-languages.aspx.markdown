---
title: The Security Patch Dilemma For Scripting And VM Based Languages
date: 2007-09-20 -0800
tags: [security]
redirect_from: "/archive/2007/09/19/the-security-patch-dilemma-for-scripting-and-vm-based-languages.aspx/"
---

In his book, *[Producing Open Source
Software](http://producingoss.com/ "Producing Open source Software - How to run a successful free software project")*,
Karl Fogel gives sage advice on running an open source project. The
section on how to deal with a security vulnerability was [particularly
interesting](https://haacked.com/archive/2007/09/20/urgent-subtext-security-patch.aspx "Subtext Security Patch")
to me last night.

Upon learning of a potential security hole, Karl recommends the
following:

1.  Don’t talk about the bug publicly until a fix is available.
2.  Make sure to have a private mailing list setup with a small group of
    trusted committers where users can send security reports.
3.  Fix the patch quickly. Time is of the essence.
4.  Don’t commit the fix into your source control lest someone scanning
    for such vulnerabilities find out about it. Wait till after the fix
    is released.
5.  Give well known administrators (and thus likely targets) using the
    software a heads up before announcing the flaw and the fix.
6.  Distribute the fix publicly.

There’s more elaboration in the book, but I think the above list
distills the key points. Karl’s advice is born from his experience
working on CVS and leading the Subversion project and makes a lot of
sense.

But for a project built on Java, .NET, or a scripting language, there is
an interesting dilemma. **The security fix itself announces the
vulnerability**.

When the Subversion team releases a patch, it is generally compiled to
native machine code, which is effectively opaque to the world. Sure with
time and effort, a native executable can be decompiled, but the barrier
is high to discover the actual exploit by examining the binary. It buys
consumers time to patch their installations before exploits start
becoming rampant.

With a language like C\#, Java, or Ruby, the bar to looking at the code
is extremely low. Such languages can raise the bar slightly by using
obfuscators, but that is really not common for an Open Source project
and creates very little delay for the determined attacker.

So no matter how well you keep the flaw private until you’re ready to
announce the fix. The announcement and publication of the fix itself
potentially points attackers to the flaw.

This is one situation in which the increased transparency of such
languages can cause a problem. Consumers of projects built on these
languages have to be extra vigilant about applying patches quickly,
while developers of such code must be extra vigilant in threat modeling
and code review to avoid security vulnerabilities in the first case.
Then again, this doesn't mean that code compiled to a native binary
should be any less vigilant about security.\

If you have a better way of distributing security patches for
VM-based/Scripting language projects than this, please do tell.

