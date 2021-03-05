---
title: The State of NuGet
tags: [nuget,oss]
redirect_from: "/archive/2012/02/07/the-state-of-nuget.aspx/"
---

I’ve seen a few recent tweets asking about what’s going on with NuGet
since I left Microsoft. The fact is that the NuGet team has been hard at
work on the release and have been discussing it in various public
forums. I think the feeling of “quiet” might be due to the lack of
blogging, which I can easily correct right now!

In this post, I want to highlight a few things:

-   What the NuGet team has been working on
-   How you can track what we’re doing
-   And how you can get involved in the discussion

Just to clarify, I have not left the NuGet project. Until my name is
removed from [this
page](http://www.outercurve.org/Galleries/ASPNETOpenSourceGallery/NuGet "NuGet on Outercurve"),
I will be involved.
![Smile](https://haacked.com/assets/images/haacked_com/WindowsLiveWriter/The-State-of-NuGet_A57C/wlEmoticon-smile_2.png)However,
as I’ve been ramping up in my new job at GitHub (loving it!), I have
been less involved as I would like simply because there’s only so much
of me to go around. Once we get the project I’m working on at work
shipped, I hope to divide my time a little better so that I don’t
neglect NuGet. But at the same time, everyone else has stepped up so
much that I don’t think they’ve missed me much.

The team and I are working through how to best keep me involved and we
are starting to improve lines of communication.

NuGet Status Page
-----------------

The NuGet team is currently working on NuGet 1.7, but in the meantime,
we’ve shipped a status page at
[**http://status.nuget.org/**](http://status.nuget.org/).

> This state is design to provide the community with information about
> the overall state of NuGet as well as information about the future
> direction and plan of the NuGet team. Notifications about planned
> maintenance as well as outages will be posted in the “State of NuGet
> address” section and can be followed using the RSS feed. Additionally
> during these times the team with use this section to communicate any
> pertinent information.

NuGet is more than an add-in to Visual Studio. It’s an important service
to multiple different clients and partners and we’re working on ways to
improve that communication. The status page is a start, but we’re open
to other ideas for improving communication.

NuGet Issue Tracker
-------------------

As always, if you’re curious about the progress being made towards NuGet
1.7, just visit [the issue
tracker](http://nuget.codeplex.com/workitem/list/advanced?keyword=&status=Open%20%28not%20closed%29&type=All&priority=All&release=NuGet%201.7&assignedTo=All&component=All&sortField=LastUpdatedDate&sortDirection=Descending&page=0 "NuGet Issue Tracker").
The link I just provided shows a filtered view of issues that are still
open for NuGet 1.7. You can select the Fixed or Closed status to see
what issues have already been implemented for 1.7.

So even if our blogs get a little quite, the issue tracker is the source
of truth about the activity.

NuGet JabbR
-----------

The NuGet team is moving more and more of our design discussions to [our
JabbR room](http://jabbr.net/#/rooms/nuget "NuGet JabbR Room"). Don’t
know what JabbR is? It’s a real-time chat site built on top of ASP.NET
and SignalR with a lot of nice features. It’s similar to CampFire, but
has great features such as tab expansions for user names as well as
emojis! JabbR itself has a very active community surrounding it and they
accept pull requests!

In fact, last night starting at around 11 PM we had a big design
discussion around capability filtering. For example, if you are on a
client that doesn’t support a feature of the package (such as PowerShell
scripts), can we filter that out for you. If you have something to say
about this, don’t respond here, go to the JabbR room!

What’s Next?
------------

A big focus for us is getting the community more and more involved in
NuGet. We hope the move towards leveraging JabbR more helps in that
regard. We’re still hashing out some of the details in how we do this.
For example, what should be discussed in JabbR vs our [discussions
site](http://nuget.codeplex.com/discussions "NuGet Discussions site.")?
I think JabbR is a great place to hash out a design and then perhaps
summarize the results in a discussion item for posterity. What do you
think?

