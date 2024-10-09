---
title: "Custom config sections using static virtual members in interfaces"
description: "C# 11 introduced static virtual members in interfaces. The primary motivation for this feature is to support generic math algorithms. But it turns out, this feature is useful in other scenarios."
tags: [csharp dotnet aspnetcore]
---

C# 11 introduced a new feature - [static virtual members in interfaces](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/static-virtual-interface-members). The primary motivation for this feature is to support generic math algorithms. The mention of math might make some ignore this feature, but it turns out it can be useful in other scenarios.

For example, I was able to leverage this feature to clean up how I register and consume custom config section types.

## Custom Config Section

As a refresher, let's look at custom config sections. Suppose you want to configure an API client in your `appSettings.json`. You can map the config section to a type. For example, here is an `appSettings.json` file in one of my projects.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "OpenAI": {
    "ApiKey": "Set this in User Secrets",
    "OrganizationId": "{Set this to your org id}",
    "Model": "gpt-4",
    "EmbeddingModel": "text-embedding-3-large"
  }
}
```

Rather than going through the `IConfiguration` API to read each of the "OpenAI" settings one at a time, I prefer to map this to a type.

```csharp
public class OpenAIOptions {
    public string? ApiKey { get; init; }
    public string? OrganizationId { get; init; }
    public string Model { get; init; } = "gpt-3.5-turbo";
    public string EmbeddingModel { get; init; } = "text-embedding-ada-002";
}
```

In `Program.cs`, I can configure this mapping.

```csharp
builder.Configuration.Configure<OpenAIOptions>(builder.Configuration.GetSection("OpenAI"));
```

With this configured, I can inject an `IOptions<OpenAIOptions>` into any class that's resolved via Dependency Injection and access the config section properties in a strongly typed manner.

```csharp
using Microsoft.Extensions.Options;

public class OpenAIClient(IOptions<OpenAIOptions> options) {
    string? ApiKey => options.Value.ApiKey;
    string? Model => options.Value.Model;
    // ...
}
```

Sometimes, you're in a situation where you can't inject `IOptions<T>` for whatever reason. You can grab it from `IConfiguration` like so.

```csharp
Configuration.GetSection("OpenAI").Get<OpenAIOptions>()
```

## Static Virtual Interfaces Come To Clean Up

This is all fine, but a little repetitive when you have multiple configuration classes. I'd like to build a more convention based approach. This is where static virtual members in interfaces come in handy.

First, let's define an interface for all my configuration sections.

```csharp
public interface IConfigOptions
{
    static abstract string SectionName { get; }
}
```

Notice there's a static abstract string property named `SectionName`. This is the static virtual member. Any type that implements this interface has to implement a static `SectionName` property.

Now I'm going to implement that interface in my configuration class.

```csharp
public class OpenAIOptions : IConfigOptions {
    public static string SectionName => "OpenAI";

    public string? ApiKey { get; init; }
    public string? OrganizationId { get; init; }
    public string Model { get; init; } = "gpt-3.5-turbo";
    public string EmbeddingModel { get; init; } = "text-embedding-ada-002";
}
```

With that in place, I can implement an extension method to access the `SectionName` when registering a configuration section type.

```csharp
public static class OptionsExtensions {
    public static IHostApplicationBuilder Configure<TOptions>(this IHostApplicationBuilder builder)
        where TOptions : class, IConfigOptions
    {
        var section = builder.Configuration.GetSection(TOptions.SectionName);
        builder.Services.Configure<TOptions>(section);
        return builder;
    }

    public static TOptions? GetConfigurationSection<TOptions>(this IHostApplicationBuilder builder)
        where TOptions : class, IConfigOptions
    {
        return builder.Configuration
            .GetSection(TOptions.SectionName)
            .Get<TOptions>();
    }
}
```

Now, with this method, I can register a configuration section like so:

```csharp
builder.Configure<OpenAIOptions>();
```

When you have several configuration sections to configure, the registration code looks nice and clean.

For example, in one project I have a section like this:

```csharp
builder.Configure<OpenAIOptions>()
    .Configure<GitHubOptions>()
    .Configure<GoogleOptions>()
    .Configure<WeatherOptions>()
```

## Conclusion

The astute reader will notice I didn't need to use static virtual members here. I could have built a convention-based approach by using reflection to extract the configuration section name from the type name. It's true, but the code isn't as tight as this approach. Also, there may be times where you want the type name to be different from the section name.
