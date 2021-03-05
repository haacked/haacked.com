---
title: 'Neat VS10 Feature: Pinning A Debugger Watch'
tags: [visualstudio]
redirect_from: "/archive/2009/11/05/pinning-a-debugger-watch.aspx/"
---

I was stepping through some code in a debugger today and noticed a neat
little feature of Visual Studio 2010 that I hadn’t noticed before.

When debugging, you can easily examine the value of a variably by
highlighting it with your mouse. Nothing new there. But then I noticed a
little pin next to it, which I’ve never seen before.

![debugger-value](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/NeatVS10FeaturePinningADebuggerWatch_EB08/debugger-value_3.png "debugger-value")

So what do you see when you see a pin? You click on it!

![pinned-quick-watch](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/NeatVS10FeaturePinningADebuggerWatch_EB08/pinned-quick-watch_3.png "pinned-quick-watch")

As you might expect, that pins the quick watch in place. So now I hit
the play button, continue running my app in the debugger, and the next
time I hit that breakpoint:

![pinne-watch-changed](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/NeatVS10FeaturePinningADebuggerWatch_EB08/pinne-watch-changed_3.png "pinne-watch-changed")

I can clearly see the value changed since the last time. I think this
may come in useful when walking through code as a way of seeing the
value of important variables right next to where they are declared. I
thought that was pretty neat.

