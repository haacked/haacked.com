---
title: "What are your ASP.NET Core users up to? Find out with PostHog."
description: "PostHog is a product analytics platform that helps you build better products faster. It contains a set of tools to help you capture analytics and leverage feature flags."
tags: [career, work]
excerpt_image: https://github.com/user-attachments/assets/b7c64431-0232-4707-b016-c0161ea6b0eb
---

PostHog helps you build better products. It tracks what users do. It controls features in production. And now it works with .NET!

[I joined PostHog at the beginning of the year](https://haacked.com/archive/2025/01/07/new-year-new-job/) as a Product Engineer on [the Feature Flags team](https://posthog.com/teams/feature-flags). Feature flags are just one of the many tools PostHog offers to help product engineers build better products.

Much of my job will consist of writing Python and React with TypeScript. But when I started, I noticed they didn't have a .NET SDK. It turns out, I know a thing or two about .NET!

![Hedgehog giving two thumbs up in front of a computer showing the .NET logo](https://github.com/user-attachments/assets/b7c64431-0232-4707-b016-c0161ea6b0eb)

So if you've been wanting to use PostHog in your ASP.NET Core applications, yesterday is your lucky day! The 1.0 version of the PostHog .NET SDK for ASP.NET Core is [available on NuGet](https://www.nuget.org/packages/PostHog.AspNetCore).

```bash
dotnet add package PostHog.AspNetCore
```

You can find documentation for the library on [the PostHog docs site](https://posthog.com/docs/libraries/dotnet), but I'll cover some of the basics here. I'll also cover non-ASP.NET Core usage later in this post.

## Configuration

To configure the client SDK, you'll need:

1. Project API Key - _from the [PostHog dashboard](https://us.posthog.com/settings/project)_
2. Personal API Key - _for local evaluation (Optional, but recommended)_

> [!NOTE]
> For better performance, enable local feature flag evaluation by adding a personal API key (found in Settings). This avoids making API calls for each flag check.

By default, the PostHog client looks for settings in the `PostHog` section of the configuration system such as in the `appSettings.json` file:

```json
{
  "PostHog": {
    "ProjectApiKey": "phc_..."
  }
}
```

Treat your personal API key as a secret by using a secrets manager to store it. For example, for local development, use the `dotnet user-secrets` command to store your personal API key:

```bash
dotnet user-secrets init
dotnet user-secrets set "PostHog:PersonalApiKey" "phx_..."
```

In production, you might use Azure Key Vault or a similar service to provide the personal API key.

## Register the client

Once you set up configuration, register the client with the dependency injection container.

In your `Program.cs` file, call the `AddPostHog` extension method on the `WebApplicationBuilder` instance. It'll look something like this:

```csharp
using PostHog;

var builder = WebApplication.CreateBuilder(args);

builder.AddPostHog();
```

Calling `builder.AddPostHog()` adds a singleton implementation of `IPostHogClient` to the dependency injection container. Inject it into your controllers or pages like so:

```csharp
public class MyController(IPostHogClient posthog) : Controller
{
}

public class MyPage(IPostHogClient posthog) : PageModel
{
}
```

## Usage

Use the `IPostHogClient` service to identify users, capture analytics, and evaluate feature flags.

Use the `IdentifyAsync` method to identify users:

```csharp
// This stores information about the user in PostHog.
await posthog.IdentifyAsync(
    distinctId,
    user.Email,
    user.UserName,
    // Properties to set on the person. If they're already
    // set, they will be overwritten.
    personPropertiesToSet: new()
    {
        ["phone"] = user.PhoneNumber ?? "unknown",
        ["email_confirmed"] = user.EmailConfirmed,
    },
    // Properties to set once. If they're already set
    // on the person, they won't be overwritten.
    personPropertiesToSetOnce: new()
    {
        ["joined"] = DateTime.UtcNow 
    });
```
Some things to note about the `IdentifyAsync` method:

- The `distinctId` is the identifier for the user. This could be an email, a username, or some other identifier such as the database Id. The important thing is that it's a consistent and unique identifier for the user. If you use PostHog on the client, use the same `distinctId` here as you do on the client.
- The `personPropertiesToSet` and `personPropertiesToSetOnce` are optional. You can use them to set properties about the user.
- If you choose a `distinctId` that can change (such as username or email), you can use the `AliasAsync` method to alias the old `distinctId` with the new one so that the user can be tracked across different `distinctIds`.

To capture an event, call the `Capture` method:

```csharp
posthog.Capture("some-distinct-id", "my-event");
```

This will capture an event with the distinct id, the event name, and the current timestamp. You can also include properties:

```csharp
posthog.Capture(
    "some-distinct-id",
    "user signed up",
    new() { ["plan"] = "pro" });
```

The `Capture` method is synchronous and returns immediately. The actual batching and sending of events is done in the background.

## Feature flags

To evaluate a feature flag, call the `IsFeatureEnabledAsync` method:

```csharp
if (await posthog.IsFeatureEnabledAsync(
    "new_user_feature",
    "some-distinct-id")) {
    // The feature flag is enabled.
}
```

This will evaluate the feature flag and return `true` if the feature flag is enabled. If the feature flag is not enabled or not found, it will return `false`.

Feature Flags can contain filter conditions that might depend on properties of the user. For example, you might have a feature flag that is enabled for users on the pro plan.

If you've previously identified the user and are NOT using local evaluation, the feature flag is evaluated on the server against the user properties set on the person via the `IdentifyAsync` method.

But if you're using local evaluation, the feature flag is evaluated on the client, so you have to pass in the properties of the user:

```csharp
await posthog.IsFeatureEnabledAsync(
    featureKey: "person-flag",
    distinctId: "some-distinct-id",
    personProperties: new() { ["plan"] = "pro" });
```

This will evaluate the feature flag and return `true` if the feature flag is enabled and the user's plan is "pro".

## .NET Feature Management

[.NET Feature Management](https://learn.microsoft.com/en-us/azure/azure-app-configuration/feature-management-dotnet-reference) is an abstraction over feature flags that is supported by ASP.NET Core. With it enabled, you can use the `<feature />` tag helper to conditionally render UI based on the state of a feature flag.

```csharp
<feature name="my-feature">
    <p>This is a feature flag.</p>
</feature>
```

You can also use the `FeatureGateAttribute` in your controllers and pages to conditionally execute code based on the state of a feature flag.

```csharp
[FeatureGate("my-feature")]
public class MyController : Controller
{
}
```

If your app already uses .NET Feature Management, you can switch to using PostHog with very little effort.

To use PostHog feature flags with the .NET Feature Management library, implement the `IPostHogFeatureFlagContextProvider` interface. The simplest way to do that is to inherit from the `PostHogFeatureFlagContextProvider` class and override the `GetDistinctId` and `GetFeatureFlagOptionsAsync` methods. This is required so that .NET Feature Management can evaluate feature flags locally with the correct `distinctId` and `personProperties`.

```csharp
public class MyFeatureFlagContextProvider(
    IHttpContextAccessor httpContextAccessor)
    : PostHogFeatureFlagContextProvider
{
    protected override string? GetDistinctId() =>
       httpContextAccessor.HttpContext?.User.Identity?.Name;
    
    protected override ValueTask<FeatureFlagOptions> GetFeatureFlagOptionsAsync()
    {
        // In a real app, you might get this information from a database or other source for the current user.
        return ValueTask.FromResult(
            new FeatureFlagOptions
            {
                PersonProperties = new Dictionary<string, object?>
                {
                    ["email"] = "some-test@example.com",
                    ["plan"] = "pro"
                },
                OnlyEvaluateLocally = true
            });
    }
}
```

Then, register your implementation in `Program.cs` (or `Startup.cs`):

```csharp
using PostHog;

var builder = WebApplication.CreateBuilder(args);

builder.AddPostHog(options => {
    options.UseFeatureManagement<MyFeatureFlagContextProvider>();
});
```

This registers a feature flag provider that uses your implementation of `IPostHogFeatureFlagContextProvider` to evaluate feature flags against PostHog.

## Non-ASP.NET Core usage

The `PostHog.AspNetCore` package adds ASP.NET Core specific functionality on top of the core `PostHog` package. But if you're not using ASP.NET Core, you can use the core `PostHog` package directly:

```bash
dotnet add package PostHog.AspNetCore
```

And then register it with your dependency injection container:

```csharp
builder.Services.AddPostHog();
```

If you're not using dependency injection, you can still use the registration method:

```csharp
using PostHog;
var services = new ServiceCollection();
services.AddPostHog();
var serviceProvider = services.BuildServiceProvider();
var posthog = serviceProvider.GetRequiredService<IPostHogClient>();
```

For a console app (or apps not using dependency injection), you can also use the `PostHogClient` directly, just make sure it's a singleton:

```csharp
using System;
using PostHog;

var posthog = new PostHogClient(
  Environment.GetEnvironmentVariable("PostHog__PersonalApiKey"));
```

## Examples

To see all this in action, the [`posthog-dotnet` GitHub repository](https://github.com/posthog/posthog-dotnet) has a [samples directory](https://github.com/PostHog/posthog-dotnet/tree/main/samples) with a growing number of example projects. For example, the [HogTied.Web](https://github.com/PostHog/posthog-dotnet/tree/main/samples/HogTied.Web) project is an ASP.NET Core web app that uses PostHog for analytics and feature flags and shows some advanced configuration.

## What's next?

With this release done, I'll be focusing my attention on the Feature Flags product. Even so, I'll continue to maintain the SDK and fix any reported bugs.

If anyone reports bugs, I'll be sure to fix them. But I won't be adding any new features for the moment.

Down the road, I'm hoping to add a `PostHog.Unity` package. I just don't have a lot of experience with Unity yet. My game development experience mostly consists of getting shot in the face by squaky voiced kids playing Fortnite. I'm hoping someone will contqribute a Unity sample project to the repo which I can use as a starting point.

If you have any feedback, questions, or issues with the PostHog .NET SDK, please reach file an issue at https://github.com/PostHog/posthog-dotnet.
