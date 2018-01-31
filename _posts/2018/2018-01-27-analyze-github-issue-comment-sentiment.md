---
layout: post
title: "Analyzing GitHub Issue Comment Sentiment With Azure"
date: 2018-01-27 -0800
comments: true
categories: [azure azure-functions serverless github sentiment ml ai]
---

![Tragedy and Comedy - Scarbrough Hotel, Bishopgate, Leeds - by Tim Green - CC BY 2.0](https://user-images.githubusercontent.com/19977/35477506-f253e26c-0378-11e8-9eff-5d150fa0ca9f.jpg)

Developers are _real_ passionate about their semi-colons; or lack thereof. Comment threads on [GitHub](https://github.com/) can [get a bit...testy...on this topic](https://github.com/twbs/bootstrap/issues/3057#issuecomment-5135512). What's a beleaguered<sup>1</sup> repository maintainer to do when an issue comment thread gets out of hand?

GitHub provides [community tools](https://github.com/blog/2380-new-community-tools) maintainers can use to define community standards for their projects. For example, it's easy to add a code of conduct to a repository. It's also possible [report offensive comments directly to GitHub](https://github.com/blog/2493-report-content-directly-to-github-support). However, a code of conduct is only a set of words on a page. It's only effective if you enforce it. And face it, enforcing it can be very time consuming.

What if a bot could help? Now I'm not so naïve to think you can take the very human problem of enforcing community standards and just sprinkle a bit of Machine Learning on it and the problem goes away. Clippy taught me that.

But perhaps the [combination of machine learning and human judgement](http://www.bbc.com/future/story/20151201-the-cyborg-chess-players-that-cant-be-beaten) could make the problem more tractable.

### The Idea

This was the idea I had in mind when I decided to explore some new technologies. I learn best by building something so I set out to add sentiment analysis to GitHub issue comments.

Sentiment analysis (also known as opinion mining) is the use of computers to analyze text to try and determine whether a piece of writing is positive, negative, or neutral. It relies on multiple fields related to AI such as natural language processing, computational linguistics, machine learning, and wishful thinking.

To make this work I need to do four things:

0. Drink some whiskey
1. Listen to and respond to GitHub issue comments.
2. Analyze the sentiment of the comment.
3. Update the comment with a note about the sentiment.

The idea is this: when an issue receives a negative issue comment, I'm going to have my "SentimentBot" update the comment with a note to keep things positive.

_DISCLAIMER: I want to be very clear that I chose this behavior as a proof of concept. I don't think it'd be a good idea on a real OSS project to have a bot automatically respond to negative sentiment. If I were doing this for real, I'd probably have it privately flag comments in some manner for follow-up. You'll probably see me make this clarification again because people have short memories._

### The GitHub Listener

[Webhooks](https://developer.github.com/webhooks/) are a powerful mechanism to extend GitHub. There are three key steps to set up a webhook.

1. Set up an application that can receive an HTTP POST from github.com.
2. Register the application as a webhook on a repository.
3. Configure the repository events the webhook listens to in the repository settings page.

That first step is a bit of a pain. I need to write an entire application and host it at a publicly available URL? Ugh! So 2015!

All I really want to do is write a tiny bit of code to respond to a Webhook call. I don't care how its hosted.

[Serverless architecture](https://martinfowler.com/articles/serverless.html) to the rescue! The "Serverless" nomenclature has been the source of a lot of snide comments and jokes. The name may lead one to believe we chucked the server and are hosting our code on gumption and hope. But it's not like that. Of course there's a server! You just don't have to worry about it. You just write some code and the Serverless service handles hosting, scaling, etc. all for you.

[Azure Functions](https://azure.microsoft.com/en-us/services/functions/) and AWS Lambda are the two most well known examples of Serverless services. I decided to play around with Azure Functions because they have specific support for GitHub Webhooks. GitHub Webhooks and Azure Functions go together like Bitters and Bourbon. Mmmm, I'll be right back.

Follow [these instructions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-github-webhook-triggered-function) to set up an Azure Function inside of the [Azure Portal](hthtps://portal.azure.com) that responds to a GitHub webhook in no time. The result is a method with a signature like this.

```csharp
public static async Task<object> Run(
  HttpRequestMessage req,
  TraceWriter log)
{
  string jsonContent = await req.Content.ReadAsStringAsync();
  dynamic data = JsonConvert.DeserializeObject(jsonContent);

  // Your code goes here

  return req.CreateResponse(HttpStatusCode.OK, new {
    body = "Your response"
});
}
```

The shape of the `data` is determined by the event type that the webhook subscribes to. For example, if you subscribe to issue comments like I did, the payload represented by `data` is the [`IssueCommentEvent`](https://developer.github.com/v3/activity/events/types/#issuecommentevent).

In my example, we use a `dynamic` type for ease and convenience (but at the risk of correctness). However, you can deserialize the response into a strongly typed class. The Octokit.net library provides such classes. For example, I could deserialize the request body to an instance of [`IssueCommentPayload`](https://github.com/octokit/octokit.net/blob/master/Octokit/Models/Response/ActivityPayloads/IssueCommentPayload.cs).

### Analyzing Sentiment

The next step is to write code to analyze sentiment. But how do I do that? A naïve approach would search for my favorite colorful words in the text. A more sophisticated approach is to use something like Microsoft's Cognitive Services. They have a [Text Analytics](https://azure.microsoft.com/en-us/services/cognitive-services/text-analytics/) API you can use for analyzing sentiment.

And of course, there's a NuGet package for that.

`Install-Package Microsoft.Azure.CognitiveServices.Language`

I installed the package, wrote a bit of code, and had the sentiment analysis working in short order. The API returns a score between 0 and 1. Scores close to 0 are negative. Close to 1 are positive.

```csharp
static async Task<double?> AnalyzeSentiment(string comment)
{
  ITextAnalyticsAPI client = new TextAnalyticsAPI();
  client.AzureRegion = AzureRegions.Westcentralus;
  client.SubscriptionKey = "YOUR_SUBSCRIPTION_KEY";

  return (await client.SentimentAsync(
    new MultiLanguageBatchInput(
        new List<MultiLanguageInput>()
        {
          new MultiLanguageInput("en", "0", comment),
        })
  )).Documents.First().Score;
}
```

### Updating the comment

Now that all the sentiments are determined, let's do something with that information. For the sake of this proof of concept, I will update overly negative comments with a little reminder to keep it positive. After all, we know how much humans enjoy being [chided by a software robot](https://www.theatlantic.com/technology/archive/2015/06/clippy-the-microsoft-office-assistant-is-the-patriarchys-fault/396653/). Again, I want to reiterate that I wouldn't use this for a real repository. I'd probably just flag the comment for a human to follow-up.

I will also update positive comments with a nice thank you for keeping it positive. Gotta reward the nice people from time to time.

In order to update the comment, I'll [use Octokit.net](https://haacked.com/archive/2013/10/30/introducing-octokit-net.aspx/)! Once again, NuGet to the rescue.

`Install-Package octokit`

The code is pretty straightforward. We use Octokit to post an edit to a comment [using the issue comment API](https://developer.github.com/v3/issues/comments/#edit-a-comment).

```csharp
static async Task UpdateComment(
  long repositoryId,
  int commentId,
  string existingCommentBody,
  string sentimentMessage)
{
  var client = new GitHubClient(
    new ProductHeaderValue("haack-test-bot", "0.1.0"));
  var personalAccessToken = "SECRET PERSONAL ACCESS TOKEN";
  client.Credentials = new Credentials(personalAccessToken);

  await client.Issue.Comment.Update(
    repositoryId,
    commentId,
    $"{existingCommentBody}\n\n_Sentiment Bot Says: {sentimentMessage}_");
}
```

### Deployment

It's possible to build an Azure Function entirely in the Azure Portal via a web browser. But then you're pasting code into a text box. I like to write code with my [favorite editor](https://atom.io). Fortunately, Azure Functions supports [continuous deployment integration with GitHub](https://docs.microsoft.com/en-us/azure/azure-functions/functions-continuous-deployment). It's quick and easy to set up.

I set up [my repo](https://github.com/haacked-demos/azure-sentiment-analysis/) as my deployment source. Every time I merge a change into the `master` branch, my changes are deployed.

### Try it!

The source code is available in my [haacked-demos/azure-sentiment-analysis repository](https://github.com/haacked-demos/azure-sentiment-analysis/)

If you want to try out the end result, I created [a test issue in the repository](https://github.com/haacked-demos/azure-sentiment-analysis/issues/1). I know you're testing out a sentiment bot, but you can still be negative and civil to each other. Please abide by the [code of conduct](https://github.com/haacked-demos/azure-sentiment-analysis/blob/master/CODE_OF_CONDUCT.md).

Also, I don't want to pay a lot of money for this demo, so it might fail in the future if my trial of the text analysis service runs out.

### Future Ideas

My goal in this post is to show you how easy it is to build a GitHub Webhook using Azure Functions. I haven't tried it with AWS Lambda. I hope it's just as easy. If you try it, let me know how it goes!

The possibilities here are legion. With this approach, you can build all sorts of extensions that make GitHub fit into your workflows. For example, you may want to flag first time issue commenters. Or you may want to run static analysis on PRs. All of that is easy to build!

But before you get too wild with this, note that there are a lot of GitHub integrations out there that might already do what you need. For example, [the Probot project](https://github.com/probot/probot) has a [showcase of interesting apps](https://probot.github.io/apps/) that range from managing stale issues to enforcing GPG signatures on pull requests. There's even a sentiment bot in there!

Probot apps are NodeJS apps that can respond to webhooks. I believe they require you host an application, but I haven't tried to see if they're easy to run in a Serverless environment yet. That could be fun to try.

### Resources

* [GitHub Webhooks Documentation](https://developer.github.com/webhooks/)
* [Create a GitHub Webhook triggered function in Azure](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-github-webhook-triggered-function)
* [Continuous Deployment to Azure Functions from GitHub](https://docs.microsoft.com/en-us/azure/azure-functions/functions-continuous-deployment)
* [Microsoft Cognitive Services Text Analytics API](https://azure.microsoft.com/en-us/services/cognitive-services/text-analytics/)
* [Octokit.net documentation](http://octokitnet.readthedocs.io/en/latest/)
* [The haacked-demos/azure-sentiment-analysis with my code](https://github.com/haacked-demos/azure-sentiment-analysis/)

<sup>1</sup> _I admit, I have to look up the spelling of this word every time, but it's so perfect in this context._
