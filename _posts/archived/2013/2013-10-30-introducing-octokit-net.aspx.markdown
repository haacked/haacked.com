---
title: Introducing Octokit.NET
tags: [github]
redirect_from: "/archive/2013/10/29/introducing-octokit-net.aspx/"
---

Today on the GitHub blog, we announced [the first release of
Octokit.net](https://github.com/blog/1676-introducing-octokit-net "Introducing Octokit.net").

> Octokit is a family of client libraries for the GitHub API. [Back in
> May](https://github.com/blog/1517-introducing-octokit), we released
> Octokit libraries for [Ruby](https://github.com/octokit/octokit.rb)
> and [Objective-C](https://github.com/octokit/octokit.objc).
>
> Today we're releasing the third member of the Octokit family,
> [Octokit.net](https://github.com/octokit/octokit.net), the GitHub API
> toolkit for .NET developers.

[![octokit-dotnet](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IntroducingOctokit.NET_D8EF/octokit-dotnet_thumb.png "octokit-dotnet")](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/IntroducingOctokit.NET_D8EF/octokit-dotnet_2.png)

GitHub provides a powerful set of tools for developers who build amazing
software together. But these tools extend way beyond the website and Git
clients.

The [GitHub API](http://developer.github.com/v3/ "GitHub API") provides
a rich web based way to leverage GitHub.com within your own
applications. The [Octokit
family](http://octokit.github.io/ "Octokit Website") of libraries makes
it easy to call into the API. I can’t wait to see what you build with
it.

The Project
-----------

Octokit.net is an [open source project on
GitHub](https://github.com/octokit/octokit.net "Octokit.net GitHub") so
feel free to contribute with pull requests, issues, etc. You’ll notice
that we call it a 0.1.0 release. As of today, it doesn’t implement every
API endpoint that GitHub.com supports.

We wanted to make sure that it was in use by a real application so we
focused on the endpoints that [GitHub for
Windows](http://windows.github.com/ "GitHub for Windows") needs. If
there’s an endpoint that is not implemented, please do log an issue. Or
even better, send a pull request!

Our approach in implementing this library was to avoid being overly
speculative. We tried to implement features as we needed them based on
developing a real production application.

But now that it’s in the wild, we’re curious to see what other types of
applications will need from the library.

Platform and Licensing Details
------------------------------

Octokit.net is licensed under [the MIT
license](https://github.com/octokit/octokit.net/blob/master/LICENSE.txt "MIT License").

As of today, Octokit.net requires .NET 4.5. We also have a WinRT library
for .NET 4.5 Core. This is because we build on top of HttpClient is not
available in .NET 4.0.

There is a [Portable
HttpClient](https://www.nuget.org/packages/Microsoft.Net.Http "Portable HttpClient on NuGet")
package that does work for .NET 4.0, but we won’t distribute it because
it has platform limitations that are incompatible with our license.

I had hoped that its platform limitations would have been removed by
now, but that sadly is not the case. If you’re wondering why that
matters, read [my post
here](https://haacked.com/archive/2013/06/24/platform-limitations-harm-net.aspx "Platform limitations harm .net").

However, if you check the repository out, you’ll notice that there’s a
branch named *haacked/portable-httpclient*. If you only plan to deploy
on Windows, you can build that branch yourself and make use of it.

Go Forth And Build!
-------------------

I’ve had great fun working with my team at GitHub on Octokit.net the
past few weeks. I hope you have fun building amazing software that
extends GitHub in ways we never imagined. Enjoy!

