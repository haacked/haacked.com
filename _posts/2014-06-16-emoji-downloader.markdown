---
layout: post
title: "Download Emojis With Octokit.NET"
date: 2014-06-16 -0800
comments: true
categories: [github emoji octokit]
---

I ![love](https://github.global.ssl.fastly.net/images/icons/emoji/heart.png) emojis. Recently, I had the fun task to add emoji auto completion to the [latest GitHub for Windows release](http://haacked.com/archive/2014/06/09/ghfw-2/), among other contributions.

In this post, I want to walk through how to use [Octokit.NET](http://octokit.github.io/) to download all the emojis that GitHub supports.

The process is pretty simple, we're going to make a request to the [Emojis API](https://developer.github.com/v3/emojis/) to get the list of emojis, and then download each image.

The first example uses the vanilla `Octokit` package. The second example uses the `Octokit.Reactive` package. Both examples pretty much accomplish the same thing, but the Rx version downloads emojis four at a time in parallel instead of one by one.

All the code for this example is available in the [haacked/EmojiDownloader repository](https://github.com/Haacked/EmojiDownloader/) on GitHub. 

## The Code

To get started, create a console project and install the Octokit.NET package:

```
Install-Package Octokit
```

The first step is to create an instance of the `GitHubClient`. We don't have to provide any credentials to call the Emojis API.

```csharp
var githubClient = new GitHubClient(
    new ProductHeaderValue("Haack-Emoji-Downloader"));
```

The string in the `ProductHeaderValue` is used to form a User Agent for the request. The GitHub API requires a valid user agent.

Now we can request the list of emojis.

```csharp
var emojis = await githubClient.Miscellaneous.GetEmojis();
```

This returns a `IReadOnlyList<Emoji>`.

Now we can iterate through each one and use an `HttpClient` to download each image. We'll use the following code to download the image.

```csharp
public static async Task DownloadImage(Uri url, string filePath)
{
    Console.WriteLine("Downloading " + filePath);

    using (var httpClient = new HttpClient())
    {
        using (var request = new HttpRequestMessage(HttpMethod.Get, url))
        {
            var response = await httpClient.SendAsync(request);

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            using (var writeStream = new FileStream(filePath, System.IO.FileMode.Create))
            {
                await responseStream.CopyToAsync(writeStream);
            }     
        }
    }
}
```

Here's the code of the Console's `Main` method that puts all this together. Note that I wrap everything in a `Task.Run` so I can use the `async` and `await` keywords.

```csharp
static void Main(string[] args)
{
    string outputDirectory = args.Any()
        ? String.Join("", args)
        : Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

    Debug.Assert(outputDirectory != null, "The output directory should not be null.`");

    Task.Run(async () =>
    {
        var githubClient = new GitHubClient(new ProductHeaderValue("Haack-Emoji-Downloader"));
        var emojis = await githubClient.Miscellaneous.GetEmojis();
        foreach (var emoji in emojis)
        {
            string emojiFileName = Path.Combine(outputDirectory, emoji.Name + ".png");
            await DownloadImage(emoji.Url, emojiFileName);
        }

    }).Wait();
}
```

The first part of the method sets up the output directory. By default, it will create the emojis wherever the program EXE is located. But you can also specify a path as the sole argument to the program.

## Let's get Reactive!

If you prefer to use the Reactive version of Octokit.NET, the following example will get you started.

```
Install-Package Octokit.Reactive
```

Instead of the `GitHubClient` we'll create an `ObservableGitHubClient`.

```csharp
var githubClient = new ObservableGitHubClient(
    new ProductHeaderValue("Haack-Reactive-Emoji-Downloader"));            
```

Now we can call the equivalent method, but we have the benefit of using the [`Buffer`](http://msdn.microsoft.com/en-us/library/system.reactive.linq.observable.buffer%28v=vs.103%29.aspx) method.

```csharp
githubClient.Miscellaneous.GetEmojis()
    .Buffer(4) // Downloads 4 at a time.
    .Do(group => Task.WaitAll(group
        .Select(emoji => new
        {
            emoji.Url,
            FilePath = Path.Combine(outputDirectory, emoji.Name + ".png")
        })
        .Select(download => DownloadImage(download.Url, download.FilePath)).ToArray()))
    .Wait();
```

The buffer method groups the sequence of emojis into groups of four so we can then kick off the download for four emojis at a time and then wait for the group to finish before requesting the next four.

The reason we don't just request them all at the same time is we don't want to flood the network card or local network.

UPDATE: My buddy [Paul Betts](https://twitter.com/paulcbetts) suggests an even better more Rx-y approach in the comments.

```csharp
githubClient.Miscellaneous.GetEmojis()
    .Select(emoji => Observable.FromAsync(async () =>
    {
        var path = Path.Combine(outputDirectory, emoji.Name + ".png");
        await DownloadImage(emoji.Url, path);
        return path;
    }))
    .Merge(4)
    .ToArray()
    .Wait();
``` 

We'll use the [`Merge`](http://msdn.microsoft.com/en-us/library/system.reactive.linq.observable.merge%28v=vs.103%29.aspx) method instead of `Buffer` to throttle requests to four at a time.

And with that, you'll have 887 (as of right now) emoji png files downloaded to disk.

## Other Octokit.NET blog posts

* [Introducing Octokit.NET](http://haacked.com/archive/2013/10/30/introducing-octokit-net.aspx/)
* [Use Octokit.net to authenticate your app with GitHub](http://haacked.com/archive/2014/04/24/octokit-oauth/)
