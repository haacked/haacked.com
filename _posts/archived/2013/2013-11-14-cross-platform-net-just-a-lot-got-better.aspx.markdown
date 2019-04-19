---
title: Cross Platform .NET Just A Lot Got Better
tags: [code]
redirect_from: "/archive/2013/11/13/cross-platform-net-just-a-lot-got-better.aspx/"
---

Not long ago I wrote a blog post about how [platform restrictions harm
.NET](https://haacked.com/archive/2013/06/24/platform-limitations-harm-net.aspx "Platform Restrictions harm .NET").
This led to a lot of discussion online and on Twitter. At some point
David Kean [suggested a more productive
approach](https://twitter.com/davkean/status/383280597566648320 "Suggested on Twitter")
would be to create a UserVoice issue. [So I
did](http://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/4494577-remove-the-platform-restriction-on-microsoft-nuget "User voice to remove platform restriction")
and it quickly gathered a lot of votes.

I’m [visiting
Toronto](https://github.com/blog/1687-join-us-for-a-drinkup-in-toronto-on-november-14th "Visiting Toronto")
right now so I’ve been off of the Internet all day and missed all the
hubbub when it happened. I found out about it when I logged into Gmail
and I saw I had an email that the user voice issue I created was closed.
My initial angry knee-jerk reaction was “What?! How could they close
this without addressing it?!” as I furiously clicked on the subject to
read the email and follow the [link to this
post](http://blogs.msdn.com/b/dotnet/archive/2013/11/13/pcl-and-net-nuget-libraries-are-now-enabled-for-xamarin.aspx "PCL and NuGet Libraries are now enabled for Xamarin").

Bravo!

Serious Kudos to the .NET team for this. It looks like most of the
interesting PCL packages are now licensed without platform restrictions.
As an example of how this small change sends out ripples of goodness, we
can now make [Octokit.net depend on portable
HttpClient](https://github.com/octokit/octokit.net/pull/219) and make
Octokit.net itself more cross platform and portable without a huge
amount of work.

I’m also excited about the partnership between Microsoft and Xamarin
this represents. I do believe C# is a great language for cross-platform
development and it’s good to see Microsoft jumping back on board with
this. This is a marked change from the situation I [wrote about in
2012](https://haacked.com/archive/2012/10/21/monkeyspace-dotnet-oss.aspx "MonkeySpace").

