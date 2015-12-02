---
layout: post
title: "HttpWebRequest and the Expect: 100-continue Header Problem"
date: 2004-05-15 -0800
comments: true
disqus_identifier: 449
redirect_from: "/archive/2004/05/15/449.aspx"
categories: [asp.net,code]
---
Apparently I’m not the only one to run into this annoying problem. When using the HttpWebRequest to POST form data using HTTP 1.1, it ALWAYS adds the following HTTP header "Expect: 100-Continue". Fixing the problem has proved to be quite elusive.

According to the HTTP 1.1 protocol, when this header is sent, the form data is not sent with the initial request. Instead, this header is sent to the web server which responds with 100 (Continue) if implemented correctly. However, not all web servers handle this correctly, including the server to which I am attempting to post data. I sniffed the headers that Internet Explorer sends and noticed that it does not send this header, but my code does.

Looking through the newsgroups, several people have had problems with this, but nobody with a solution (apart from going back to HTTP 1.0 which doesn’t work for me).

At this point, I thought I would fire up [Lutz Roeder’s](http://www.aisto.com/roeder/dotnet/) Reflector 4.0 and look through the source code for the `System.Net.HttpWebRequest` class. Aha! There it is. Within a private method named `MakeRequest()` are the
following lines of code:

```csharp
if (this._ExpectContinue && ((this._HttpWriteMode == HttpWriteMode.Chunked) || (this._ContentLength > ((long) 0))))
{
    this._HttpRequestHeaders.AddInternal("Expect", "100-continue");
}
```

So even if you try to remove the Expect header from the `_HttpRequestHeaders` collection, the header will get added back when the request is actually made.

Unfortunately, fixing this is not easy since MakeRequest is a private method. Walking up the callee graph, I found that the method I would have to override is `BeginGetRequestStream` (there are other ancestors aside from this one). Unfortunately this method relies on several internal and private objects to which I do not have access. I hoped to re-use the existing code base and only make a slight tweak.

I even started down the path of building my own `HttpWebRequest` class using the Rotor source code but ran into several problems there as well.

In any case, I think the easiest way to get this fixed is to find the right person at Microsoft and ask them very nicely to try to get this in SP2. Pretty Please?

UPDATE: [Lance Olson](http://blogs.msdn.com/lanceo/) points me to the solution in my comments section. The `System.Net.ServicePointManager` class has a static property named `Expect100Continue`. Setting this to false will stop the header "Expect: 100-Continue" from being sent.

Thanks Lance!!!
