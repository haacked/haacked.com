---
title: "Is .NET Aspire NuGet for Cloud Service Dependencies?"
description: ".NET Aspire has similar goals to NuGet in that it aims to streamline the number of steps it takes to incorporate a cloud service into a project. But Aspire has goals that are more far-reaching than NuGet."
tags: [aspire dotnet nuget service-dependencies cloud docker]
excerpt_image: https://github.com/haacked/ai-demo/assets/19977/9ce2bdc0-866e-47a8-bef0-e3bd00b80730
---

Recently [I tweeted](https://twitter.com/haacked/status/1793911877334122835),

> It’s not a perfect analogy, but .Net Aspire is like NuGet for cloud services. We created NuGet to make it easy to pull in libraries. Before, it took a lot of steps. Nowadays, to use a service like Postgres or Rabbit MQ, takes a lot of steps.

![Cloud dependencies](https://github.com/haacked/ai-demo/assets/19977/9ce2bdc0-866e-47a8-bef0-e3bd00b80730)

And I'm not just saying that because [David Fowler](https://twitter.com/davidfowl), one of the creators of .NET Aspire, was also [most definitely one of the creators of NuGet](https://news.ycombinator.com/item?id=40210262). But it is his MO to focus on developer productivity.

To understand why I said that, it helps to look at my initial blog post that introduced NuGet. Specifically the section ["What does NuGet solve?"](https://haacked.com/archive/2010/10/06/introducing-nupack-package-manager.aspx/#what-does-nuget-solve).

> The .NET open source community has churned out a huge catalog of useful libraries. But what has been lacking is a widely available easy to use manner of discovering and incorporating these libraries into a project.

Back in the dark ages before NuGet, adding a .NET library to your project took more steps than a marching band on speed. NuGet drastically reduced the number of steps to find and depend on a library, no stimulants necessary.

In addition to that, NuGet helped support the `clone and F5` workflow of local development. The goal with this workflow is that a new developer can clone a repository and then hit F5 in their editor or IDE to run the project locally. Or at least have as few steps between clone and run as possible. .NET Aspire helps with this too.

## The State of Cloud Service Dependencies

We're in a similar situation when it comes to cloud service dependencies. It takes a lot of steps to incorporate the service into a project. In that way, .NET Aspire is similar to NuGet (and in fact leverages NuGet) as it reduces the number of steps to incorporate a cloud service into a project, and helps support the git clone and F5 development model.

As I mentioned, my analogy isn't perfect because .NET Aspire doesn't stop at the local development story. Unlike a library dependency, a cloud service dependency has additional requirements. It needs to be provisioned, configured, and deployed. Connection strings need to be securely managed. Sacrifices to the old gods need to  be made. Aspire helps with all that except the sacrifices.

## Why Not Just Docker?

My tweet lead to a debate where someone pointed out that Postgres was a bad example because Aspire adds a lot of ceremony compared to just using Docker. This is a fair point. Docker is a great way to package up a service and run it locally. But it doesn't help with the other steps of provisioning, configuring, and deploying a service.

And even for local development, I found .NET Aspire to have the advantage that it supports the clone and run workflow better than Docker alone.

In a follow-up post to this one, I'll walk through setting up two simple asp.net core applications that leverage Postgres via EF Core. One will use Docker alone, the other will use Aspire. This provides a point of comparison so you can judge for yourself.

I know I don't have a great track record with timely follow-up posts, but I usually do follow through! This time, I won't [wait 8 years for the follow-up](https://haacked.com/archive/2012/04/15/The-Real-Pain-Of-Software-Development-2/).
