---
title: RestSharp 104.2.0 Released
tags: [code,oss]
redirect_from: "/archive/2013/09/17/restsharp-104-2-0-released.aspx/"
---

Just shipped a new release of [RestSharp to
NuGet](https://www.nuget.org/packages/RestSharp/ "RestSharp on NuGet").
For those who don’t know, RestSharp is a simple REST and HTTP API Client
for .NET.

This release is primarily a bug fix release with a whole lotta bug
fixes. It should be fully compatible with the previous version. If it’s
not, I’m sorry.

**Some highlights:**

-   Added `Task<T>` Overloads for async requests
-   Serialization bug fixes
-   ClientCertificate bug fix for Mono
-   And many more bug fixes…

Full release notes are [up on
GitHub](https://github.com/restsharp/RestSharp/releases "RestSharp 104.2 release notes").
If you’re interested in the nitty gritty, you can see every commit that
made it into this release using the [GitHub compare
view](https://github.com/RestSharp/RestSharp/compare/104.1...104.2 "RestSharp 104.2 commits").

I want to send a big thanks to everyone who contributed to this release.
You should feel proud of your contribution!

Who are you and what did you do to Sheehan?!
--------------------------------------------

Don’t worry! [John
Sheehan](http://john-sheehan.com/ "John Sheehan's blog") is safe and
sound in an undisclosed location. Ha! I kid. I’m beating him senseless
every day.

Seriously though, if you use RestSharp, you should buy John Sheehan a
beer. Though get in line as [Paul
Betts](https://twitter.com/paulcbetts "Paul Betts") owes him at least
five beers.

[![-359](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/RestSharp104.2Released_F7DA/-359_thumb.png "-359")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/RestSharp104.2Released_F7DA/-359_2.png)John
started RestSharp [four years
ago](https://github.com/restsharp/RestSharp/commit/c6fa63e14208cde3243d1176f038da2342a175ab "Initial commit")
and has shepherded it well for a very long time. But a while back he
decided to focus more on other technologies. Even so, he held on for a
long time tending to his baby even amidst a [lot of
frustrations](http://john-sheehan.com/blog/my-net-open-source-project-management-nightmare "Open Source Management Nightmare"),
until he finally stopped contributing and left it to the community to
handle.

And the community did. Various other folks started taking stewardship of
the project and it continued along. This is the beauty of open source.

We at GitHub use RestSharp for the [GitHub for Windows
application](http://windows.github.com/ "GitHub for Windows"). A little
while back, I noticed people stopped reviewing and accepting my pull
requests. Turns out the project was temporarily abandoned. So Sheehan
gave me commit access and I took the helm getting our bug fixes in as
well as reviewing and accepting the many dormant pull requests. That’s
why I’m here.

Why RestSharp when there’s HttpClient?
--------------------------------------

Very good question!
[System.Net.HttpClient](http://msdn.microsoft.com/en-us/library/system.net.http.httpclient.aspx "System.Net.HttpClient on MSDN")
is only available for .NET 4.5. There’s the [Portable Class Library
(PCL)
version](http://blogs.msdn.com/b/bclteam/archive/2013/02/18/portable-httpclient-for-net-framework-and-windows-phone.aspx "Portable HttpClient"),
but that is encumbered by silly platform restrictions. I’ve written
before that [this is harms
.NET](https://haacked.com/archive/2013/06/24/platform-limitations-harm-net.aspx "Platform Limitations harm .NET").
I am hopeful they will eventually change it.

RestSharp is unencumbered by platform restrictions - another beautiful
thing about open source.

So until Microsoft fixes the licensing on HttpClient, RestSharp is one
of the only options for a portable, multi-platform, unencumbered, fully
open source HTTP client you can use in all of your applications today.
Want to build the next great iOS app using Xamarin tools? Feel free to
use RestSharp. Find a bug in using it on Mono? Send a pull request.

The Future of RestSharp
-----------------------

I’m not going to lie. I’m just providing a temporary foster home for
RestSharp. When the HttpClient licensing is fixed, I may switch to that
and stop shepherding RestSharp. I fully expect others will come along
and take it to the next level. Of course it really depends on the
feature set it supplies and whether they open source it.

As they say, open source is about scratching an itch. Right now, I’m
scratching the “we need fixes in RestSharp” itch. When I no longer have
that itch, I’ll hand it off to the next person who has the itch.

But while I’m here, I’m going to fix things up and make them better.

