---
layout: post
title: "Quiz Answer: Watch out for the Eeeevil Thread.Abort."
date: 2004-11-17 -0800
comments: true
disqus_identifier: 1642
categories: []
---
Yesterday I posted a little
[quiz](http://haacked.com/archive/2004/11/17/quiz-what-is-wrong-with-this-code.aspx "What is wrong with this code?")
with an example of an `HttpHandler` implemented as an `ASHX` file.

[Brad Wilson](http://www.dotnetdevs.com/ "Brad's Blog") obviously knew
the answer, but only gave a hint for others to elaborate on. BigJimSlade
(no link given) expanded on the answer. ~~BigJim, I have a GMail account
for you if you want one.~~

Calling `HttpResponse.Redirect(string url)` actually calls an overload
`HttpResponse.Redirect(string url, bool endResponse)` with `endResponse`
set to true. If `endResponse` is set to true, `HttpResponse.Redirect`
will make a call to `HttpResponse.End()`.

That method in turn calls `Thread.CurrentThread.Abort()`. Oh the
depravity! [Once
again](http://haacked.com/archive/2004/11/13/the-depravity-of-thread-abort.aspx "The Depravity of Thread.Abort"),
[Thread.Abort](http://haacked.com/archive/2004/11/12/how-to-stop-a-thread.aspx "How to Stop a Thread in .NET")
[rears](http://www.interact-sw.co.uk/iangblog/2004/11/12/cancellation "How to stop a Thread in .NET")
its ugly head.

So as you see, the code sample will ALWAYS redirect to /default.aspx
because the `HandleRedirect` method throws a `ThreadAbortException`
every time. To fix this, I merely need to change the `HandleRedirect`
method to call `ctx.Response.Redirect("/special.aspx", false);`.

The fact that this week seems to be “`Thread.Abort` Week” isn't why I
posted this quiz. I ran into this problem the other day in my
carelessness. It’s a result of my old ASP 3.0 habits resurfacing after
years of suppressing them. It took me a few minutes to realize why my
code never made it to *special.aspx*.

