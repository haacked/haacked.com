---
title: "A .NET port of Scientist"
date: 2016-01-20 -0800 9:00 AM
tags: [github,csharp,dotnet,scientist]
excerpt_image: https://cloud.githubusercontent.com/assets/19977/12812812/0e75502c-cae9-11e5-9965-2cf7cf99adfd.jpg
---

Over on the [GitHub Engineering blog](http://githubengineering.com/scientist/) my co-worker Jesse Toth published a fascinating post about the [Ruby library named Scientist](http://githubengineering.com/scientist/) we use at GitHub to help us run experiments comparing new code against the existing production code.

![Photo by tortmaster on flickr - CC BY 2.0](https://cloud.githubusercontent.com/assets/19977/12812812/0e75502c-cae9-11e5-9965-2cf7cf99adfd.jpg)

It's an enjoyable read with a really great analogy comparing this approach to building a new bridge. The analogy feels very relevant to those of us here in the Seattle area as we're in the midst of a major bridge construction project across Lake Washington as they lay a new bridge alongside the existing 520 bridge.

Naturally, a lot of people asked if we were working on a C# version. In truth, I had been toying with it for a while. I had hoped to have something ready to ship on the day that Scientist 1.0 shipped, but life has a way of catching up to you and tossing your plans in the gutter. The release of Scientist 1.0 lit that proverbial fire under my ass to get something out that people can play with and help improve.

Consider this a working sketch of the API. It's very rough, but it works! I don't have a CI server set up yet etc. etc. I'll get around to it.

The plan is to start with [this repository](https://github.com/haacked/scientist.net) and once we have a rock solid battle tested implementation, we can move it to the [GitHub Organization](https://github.com/github/) on GitHub.com. If you'd like to participate, jump right in. There's plenty to do!

I tried to stay true to the Ruby implementation with one small difference. Instead of registering a custom experimentation type, you can register a custom measurement publisher. We don't have the ability to override the `new` operator like those Rubyists and I liked keeping publishing separate. But I'm not stuck to this idea.

Here's a sample usage:

```csharp
public bool MayPush(IUser user)
{
  return Scientist.Science<bool>("may-push", experiment =>
  {
      experiment.Use(() => IsCollaborator(user));
      experiment.Try(() => HasAccess(user));
  });
}
```

As expected, you can install it via NuGet `Install-Package Scientist -Pre`

Enjoy!
