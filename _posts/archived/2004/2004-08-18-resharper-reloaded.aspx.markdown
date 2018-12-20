---
title: ReSharper Reloaded
date: 2004-08-18 -0800
tags: []
redirect_from: "/archive/2004/08/17/resharper-reloaded.aspx/"
---

Ok, I tried
[CodeRush](http://www.devexpress.com/?section=/Products/NET/CodeRush)
(an excellent product) and now I'm back to
[ReSharper](http://www.jetbrains.com/resharper/index.html). Valentin
Kipiatkov, the Chief Scientist at JetBrains pointed out that there are
several options that can fix some of my concerns and make it usable for
me. The first concern was the intellisense delay.

> The pause is intentional because you not necessarily want to use the
> intellisense after typing the dot. So it appears after a small delay
> (unless you continue to type). To change this, go to ReSharper |
> Options Code Completion and find "Delay:" field at the very bottom of
> the page. Change it's value to 0.

I'd recommend that 0 or 100 would be the default. But the fact that it
is configurable is good enough for me. Another concern I had was that
their intellisense listed all overloads when I just want a method list
(like in VS.NET).

> This is actually configured by an option but a weird point is that the
> default setting is exactly as you want (and as it is in VS), maybe you
> changed it occasinally? To configure that, go to ReSharper \> Options
> \> Code completion and uncheck "Show signatures" checkbox.

Excellent! Much better! Finally there's the [pet
peeve](https://haacked.com/archive/2004/08/11/913.aspx) I mentioned
earlier. Valentin points out an option that partially helps the
situation:

> auto-insertion of parens and braces can be configured by options in
> "Editor" page, "Auto-insertion" group box. As for your suggestion
> about braces, we'll think about it.

Awesome! I appreciate the excellent support, and I'm just a trial user.
This is the kind of excellent customer support that will take this
product far. I'm ready to recommend purchasing this for the development
team at work. If you haven't tried it out, really give it a try. The
refactoring and code formatting alone are worth the money. But it goes a
lot further than that.

There are three big factors in this decision. The first is the
refactoring support in ReSharper. That is very important to me.
Secondly, the fact that there is excellent customer support. It's good
to know that if there's an issue with the product, I'll get to hear from
a human in a decent amount of time. Lastly, I have to admit that the
price is a big factor as I plan to buy a license at home of whichever
tool we use at work. ReSharper is more affordable than CodeRush, yet
provides just as much a productivity boost and more code cleanliness
boost (via refactoring support) overall in my opinion.

