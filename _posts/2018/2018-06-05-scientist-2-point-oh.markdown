---
title: "Scientist.NET 2.0 Release"
date: 2018-06-05 -0800
tags: [github,csharp,dotnet,scientist,refactoring]
---

I have some big news! Scientist.NET 2.0 is now available on NuGet.

`Install-Package Scientist`

This release includes two main features:

* A fire and forget result publisher
* Better support for IoC/DI scenarios.

## Fire and Forget

Result publishers should be very fast in order to avoid delaying code under experimentation. However, if a result publisher needs to talk to a service, it might have a noticeable impact on code execution times. With this release, a result publisher (class that implements `IResultPublisher`) can be wrapped in a `FireAndForgetResultPublisher` so that result publishing avoids any delays in running experiments and is delegated to another thread.

```csharp
Scientist.ResultPublisher = new FireAndForgetResultPublisher(new MyResultPublisher());
```

## Better support for IoC/DI scenarios

Scientist is primarily for short-lived experiments such as when you are refactoring code and need to test the old code and the new code together. With that in mind, it is designed to require minimal configuration. You can add an experiment in without the need to configure a dependency injection container. This is why it's designed as a static API.

While an individual experiment may be short lived, it's possible that you always have an experiment running somewhere in your code. In that case, you may want to use dependency injection to configure and acquire an instance of Scientist. In [this PR](https://github.com/github/Scientist.net/pull/108), [Martin Costello](https://martincostello.com/) added support for these scenarios.

The old static approach should still work if you're just playing around with Scientist, but if you need this DI/IoC approach, it's now possible.

## Thanks!

Once again, thank you to all those who have contributed to Scientist! Without you, this release would not be possible.
