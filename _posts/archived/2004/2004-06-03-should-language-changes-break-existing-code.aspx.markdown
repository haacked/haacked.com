---
title: Should Language Changes Break Existing Code?
date: 2004-06-03 -0800
tags: [code,languages]
redirect_from: "/archive/2004/06/02/should-language-changes-break-existing-code.aspx/"
---

I received a lot of comments (a lot for me) on my post entitled ["The
Difficulties of Language
Design"](https://haacked.com/archive/2004/05/27/492.aspx).

I wanted to follow up on one interesting comment by a reader named
Jocelyn:

> "Language changes shouldn't break existing code..."Well, yes or: \
> \
> - obsolete features (like the lock keyword) could be flagged\
> - tools could be developped to update existing source code\
> - the language could be versionned:\
> \#version 1.1\
> \#version 2.0\

That's why I qualified my statement with "...Too Much.". There are
certainly cases where you have to take the plunge and risk breaking
existing code. The things she mentioned are certainly great ways to
mitigate the impact of changes, but they aren't enough.

I think the real difficulty is when you slightly change the behavior of
a language feature such as a keyword. This change won't show up when you
recompile your code because you aren't marking the feature as obsolete.
Likewise, it can be quite hard for code analysis tools to check to see
if the semantics of your code relies on the old behavior (though in some
cases this might be possible) and would have problems with the new
behavior. The best it could do is flag the keyword and say "Hey! The
behavior of this keyword has changed." This might be helpful in some
cases, but imagine if the behavior of the lock statement changed
slightly. That's a lot of places you're going to have to check by hand.

The end result is that you recompile your code using the newer language
and everything looks hunky dory. But days, maybe weeks, later you find a
subtle problem with your code that is difficult to track down. In the
end, it may be the end result of a chain of events that started at the
point where your code relied on a certain behavior of the language and
that behavior changed. The point here is that the error might not occur
at the point where you rely on the faulty behavior, but somewhere down
the line.

I'm not advocating that the behavior of language features should never
change, especially if the behavior is wrong to begin with. I'm merely
pointing out the risks and hazards of doing so. It's a heavy cost and
the benefit sure as hell better be worth it. I think this is why you see
so few breaking changes.

