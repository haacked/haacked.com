---
title: "Calling internal ctors in your unit tests"
description: "What do you do when you need to construct a class with an internal constructor for a unit test? This is what you do."
tags: [csharp tdd]
excerpt_image: https://user-images.githubusercontent.com/19977/235283338-9c406f6d-77b6-4669-9273-4c90bf821487.png
---

One of my pet peeves is when I'm using a .NET client library that uses internal constructors for its return type. For example, let's take a look at the [`Azure.AI.OpenAI` nuget package](https://www.nuget.org/packages/Azure.AI.OpenAI). Now, I don't mean to single out this package, as this is a common practice. It just happens to be the one I'm using at the moment. It's an otherwise lovely package. I'm sure the authors are lovely people.

![Inside a room looking outside at a construction site](https://user-images.githubusercontent.com/19977/235283338-9c406f6d-77b6-4669-9273-4c90bf821487.png)


Here's a method that calls the Azure Open AI service to get completions. Note that this is a simplified version of the actual method for demonstration purposes:

```csharp
public async Task<Completions> GetCompletionsAsync() {
    var endpoint = new Uri("https://wouldn't-you-like-to-know.openai.azure.com/");
    var client = new Azure.AI.OpenAI.OpenAIClient(endpoint, new DefaultAzureCredential());
    var response = await client.GetCompletionsAsync("text-davinci-003", new CompletionsOptions
    {
        Temperature = (float)1.0,
        Prompts = { "Some prompt" },
        MaxTokens = 2048,
    });
    return response?.Value ?? throw new Exception("We'll handle this situation later");
}
```

This code works fine. But I have existing code that calls Open AI directly using the [`OpenAI`](https://www.nuget.org/packages/OpenAI) library. While I work to transition over to Azure, I need to be able to easily switch between the two libraries. So what I really want to do is change this method to return a `CompletionResult` from the `OpenAI` library. This is easy enough to do with an extension method to convert a `Completions` into a `CompletionResult`.

```csharp
public static CompletionResult ToCompletionResult(this Completions completions)
{
    return new CompletionResult
    {
        Completions = completions.Choices.Select(c => new Choice
        {
            Text = c.Text,
            Index = c.Index.GetValueOrDefault(),
        }).ToList(),
        Usage = new CompletionUsage
        {
            PromptTokens = completions.Usage.PromptTokens,
            CompletionTokens = (short)completions.Usage.CompletionTokens,
            TotalTokens = completions.Usage.TotalTokens,
        },
        Model = completions.Model,
        Id = completions.Id,
        CreatedUnixTime = completions.Created,
    };
}
```

But how do I test this? Well, it'd be nice to just "new" up a `Completions`, call this method on it, and make sure all the properties match up. But you see where this is going. As the beginning of this post foreshadowed, the `Completions` type only has `internal` constructors for no good reason I can see. So I can't easily create a `Completions` object in my unit tests. Instead, I have to use one of my handy-dandy helper methods for dealing with this sort of paper cut.

```csharp
public static T Instantiate<T>(params object[] args)
{
    var type = typeof(T);
    Type[] parameterTypes = args.Select(p => p.GetType()).ToArray();
    var constructor = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, parameterTypes, null);

    if (constructor is null)
    {
        throw new ArgumentException("The args don't match any ctor");
    }

    return (T)constructor.Invoke(args);
}
```

With this method, I can now write a unit test for my extension method.

```csharp
[Fact]
public void CreatesCompletionResultFromCompletions()
{
    var choices = new[]
    {
        Instantiate<Choice>(
            "the resulting text",
            (int?)0.7,
            Instantiate<CompletionsLogProbability>(),
            "stop")
    };
    var usage = Instantiate<CompletionsUsage>(200, 123, 323);
    var completion = Instantiate<Completions>(
        "some-id",
        (int?)123245,
        "text-davinci-003",
        choices,
        usage);

    var result = completion.ToCompletionResult();

    Assert.Equal("the resulting text", result.Completions[0].Text);
    Assert.Equal("text-davinci-003", result.Model);
    Assert.Equal("some-id", result.Id);
    Assert.Equal(200, result.Usage.CompletionTokens);
    Assert.Equal(123, result.Usage.PromptTokens);
    Assert.Equal(323, result.Usage.TotalTokens);
}
```

If you're wondering how I call the method without having to declare the type the method belongs to, recall that you can import methods with the `using static` declaration. So this method is part of my `ReflectionExtensions` class (so original, I know), so I have a `using static Serious.ReflectionExtensions;` at the top of my unit tests.

With this all in place, I can update my original method now:

```csharp
public async Task<CompletionResult> GetCompletionsAsync() {
    var endpoint = new Uri("https://wouldn't-you-like-to-know.openai.azure.com/");
    var client = new Azure.AI.OpenAI.OpenAIClient(endpoint, new DefaultAzureCredential());
    var response = await client.GetCompletionsAsync("text-davinci-003", new CompletionsOptions
    {
        Temperature = (float)1.0,
        Prompts = { "Some prompt" },
        MaxTokens = 2048,
    });
    return response?.Value.ToCompletionResult()
      ?? throw new Exception("We'll handle this situation later");
}
```

So yeah, I can work around the internal constructor pretty easily, but in my mind it's unnecessary friction. Also, I know a lot of folks are going to tell me I should wrap the entire API with my own data types. Sure, but that doesn't change the fact that I'm going to want to test the translation from the API's types to my own types. Not to mention, I wouldn't have to do this if the data types returned by the API were simple constructable DTOs. For my needs, this is also unnecessary friction.

I hope this code helps you work around it the next time you run into this situation.
