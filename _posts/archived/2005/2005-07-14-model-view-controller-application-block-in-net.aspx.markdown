---
title: Model View Controller Application Block in .NET
date: 2005-07-14 -0800 9:00 AM
tags: [dotnet]
redirect_from: "/archive/2005/07/13/model-view-controller-application-block-in-net.aspx/"
---

Ok, one of you forgot to send me the memo about the MVC application
block released by Microsoft. Fess up. Who forgot to send the memo?

Perhaps I missed this because they chose the name “User Interface
Process Application Block” (“UIP” for short). A name that means very
little to me and would not catch my attention. Not a big deal, but it
seems to me that “MVC Application Block” would catch developers
attentions more to its real use, unless there really is more to it than
just MVC (which I have not yet investigated).

Just recently I was working on a UI that could have benefitted from the
MVC pattern. I decided not to roll my own at the moment since I was
trying to rapdily prototype the UI. I was implementing this UI in
ASP.NET, but with the idea that a WinForms version could also be useful
at some point.

Fortunately, Mark Seeman comes to the rescue with this article, “[Easy
UI Testing - Isolate Your UI Code Before It Invades Your Business
Layer](http://msdn.microsoft.com/msdnmag/issues/05/08/UIPApplicationBlock/default.aspx)”.
Mark succinctly outlines how to implement the Application Controller
pattern using the UIP and completes the picture with UI agnostic unit
tests (using NUnit) of the controller logic. You know how I loves me
some unit tests!

Mark, I owe you a beer for highlighting the real potential underneath
this hidden gem of an application block.

