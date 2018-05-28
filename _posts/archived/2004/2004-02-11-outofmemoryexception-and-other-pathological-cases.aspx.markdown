---
layout: post
title: OutOfMemoryException and other pathological cases
date: 2004-02-11 -0800
comments: true
disqus_identifier: 189
categories:
- csharp dotnet
redirect_from: "/archive/2004/02/10/outofmemoryexception-and-other-pathological-cases.aspx/"
---

Ok, I admit it. I’m a unit-tester-aholic. I’m compulsive about it.
Sometimes going overboard:

```csharp
string s = "hello world";
Assert.AreEqual(s, "hello world", "s changed. How about that?");
```

[MbUnit](http://mbunit.com/ "MbUnit Unit Testing Framework") is my friend. I feel a great sense of accomplishment when I can write a fully automated end to end unit test of an email Newsletter mailer. Previously, testing such an app required me to run the code, open up Outlook, check my email, and “uh huh, looks good.&8221; Now, by using a
custom SMTP server class running on a separate thread (and on a custom port), my unit test can send a batch of emails and then ask the server if everything looks as its supposed to. “Aye aye. Everything checks out capt’n.”

However, there are certain cases that threaten to cure my obsessive compulsive behavior. For example, unit testing the pathological cases. Now ideally, these cases should happen infrequently enough that perhaps you can let them go. Sweep them under the carpet. Nobody has to know. Besides, who runs out of memory these days?

*raising hand* Well I did today. And I didn’t have a unit test for it. I could probably be forgiven for this one. I’m writing a Windows service that reads data into 10 DataSets. As each DataSet is read, I asynchronously open an SMTP connection and begin performing a mail-merge on that data set. I made sure to cap the number of concurrent connections to 10.

Now this app passed all my unit tests, but when I deployed it to the production server, it ran out of memory. After some analysis with a memory profiler, I discovered that I had violated [Performance Rule #6,](http://www.panopticoncentral.net/PermaLink.aspx/eacfc5e0-42df-44b0-bb9a-94354b689b17#1d6d1f3c-3fd6-4e09-8761-de3dc769a27a "Performance Rule") “90% of performance problems are designed in, not coded in.”

Each chunk of data I read in from the database is wrapped in a `RecipientBundle` class. The service then gives this class to an SmtpPool class which then schedules it to be sent on an available thread. The `SmtpPool` class keeps an `ArrayList` of the scheduled bundles to avoid sending duplicate bundles.

Hopefully you see the problem with this approach. I had a complete brain shutdown when I wrote the code to add the bundle to the `ArrayList`. Since the `ArrayList` is only used to avoid duplicate bundles, all that is necessary is to store the ID of the bundle. By adding the bundle, the `ArrayList` is holding a reference to the bundle, thus making sure it never
gets garbage collected until the entire mailing is complete. Bad move. These bundles should be generation 0 objects. Created, sent, garbage collected.

So in any case, I’m thinking about whether or not I should write a unit case for dealing with `OutOfMemoryException`s. There are a lot of difficulties in doing so. Typically, for a case like this, I will try to write a unit test that fakes it. I will talk about how I do that later.

I already have a unit test for dealing with the `ThreadAbortException`. That’s also a story for another time as there are several difficulties with deaing with the `ThreadAbortException`. For example, it doesn’t seem to get raised in my methods that are running asynchronously. They just seem to die without a whimper. Also, there’s a [slight issue](http://headblender.com/joe/blog/archives/geekness/001084.html "Slight Issue with the Finally Block")
with the CLR such that the finally block isn’t guaranteed to execute.

Till next time
