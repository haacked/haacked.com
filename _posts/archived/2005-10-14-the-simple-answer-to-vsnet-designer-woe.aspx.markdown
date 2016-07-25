---
layout: post
title: The Simple Answer To VS.NET Designer Woe
date: 2005-10-14 -0800
comments: true
disqus_identifier: 10809
categories: []
redirect_from: "/archive/2005/10/13/the-simple-answer-to-vsnet-designer-woe.aspx/"
---

It’s happened to all of us. You are happily coding along (in Visual
Studio .NET 1.x), minding your own business when you decide to switch
from the code view to the designer and back to the code view. That’s
when you experience...**The Woe**.

Now to prevent the woe, this post has [some great
tips](http://spaces.msn.com/members/mwadams/Blog/cns!1pAMOzaH98ZfHK1uhQS5Bd5g!111.entry).

However, I discovered something quite by accident, and I’m not sure if
it works in all cases. But I was working on an ASPX page and switched to
design mode and then switched back and noticed that the HTML was
completely messed up. Various tags had been upper-cased (for god knows
what reason) and my indenting was kicked in the nuts.

**So I hit `CTRL+Z` twice.**

It appears that VS.NET took two steps to fubar my code, but both steps
were still in the command stack. So undoing twice restored my ASPX
markup to its beautiful pristine state.

