---
title: "Fun with URL Encodings"
description: "URL Encoding is never as simple as you'd like it to be"
tags: [aspnet, web]
excerpt_image: https://user-images.githubusercontent.com/19977/70470634-5278b580-1a80-11ea-81d1-f0c41a392071.jpg
---

Quick! How many ways are there with .NET Core to encode parts of a URL? Here's a list I came up with.

* [`HttpUtility.UrlEncode`](https://docs.microsoft.com/en-us/dotnet/api/system.web.httputility.urlencode?view=netcore-3.0) - This is part of System.Web, so primarily used within a web application.
* [`WebUtility.UrlEncode`](https://docs.microsoft.com/en-us/dotnet/api/system.net.webutility.urlencode?view=netcore-3.0#System_Net_WebUtility_UrlEncode_System_String_) - Part of System.Net so it can be used outside of a web application.
* [`Uri.EscapeUriString`](https://docs.microsoft.com/en-us/dotnet/api/system.uri.escapeuristring?view=netcore-3.0) - 99 out of 100 developers agree you should pretty much never use this method. Use `EscapeDataString` instead.
* [`Uri.EscapeDataString`](https://docs.microsoft.com/en-us/dotnet/api/system.uri.escapedatastring?view=netcore-3.0) - This method is the jam for encoding a full URL or the path portions of the URL.
* [`HttpUtility.UrlPathEncode`](https://docs.microsoft.com/en-us/dotnet/api/system.web.httputility.urlpathencode?view=netcore-3.0) - The docs literally say "Do not use; intended only for browser compatibility."
* [`UrlEncoder`](https://docs.microsoft.com/en-us/dotnet/api/system.text.encodings.web.urlencoder?view=netcore-3.0) - This class has methods for reading and writing from text writers and spans for high performance low allocation scenarios.

[![Photo of an Enigma machine](https://user-images.githubusercontent.com/19977/70470634-5278b580-1a80-11ea-81d1-f0c41a392071.jpg "Enigma machine - CC BY 2.0")](https://www.flickr.com/photos/manunimaths/44960892745)

Programming is so easy, isn't it!

How did we get in such a situation? I think XKCD summarized it best,

![XKCD comic on how standards proliferate](https://imgs.xkcd.com/comics/standards.png "Standards by XKCD - CC BY-NC 2.5")

Just replace "Standards" with "Ways to encode a URL."

To be fair, some of these options are there because of different scenarios in which you need to encode some or all parts of a URL. And I believe the last option is a result of progress!

I assume `UrlEncoder` is a result of the .NET Core team's [war on allocations](https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-core/) and extreme focus on the performance of the stack.

## How to choose

A while back, Leon Bambrick (aka the SecretGeek) wrote a post with at [table of the various encoding methods](http://www.secretgeek.net/uri_enconding) and the result of the method. It does not include `UrlEncoder.Default.Encode` which didn't exist at the time, so maybe get on that Leon.

## My scenario

This might not be obvious, but I don't randomly write about coding minutiae out of a deep seated desire to be pedantic. Ok, that might not be entirely true, but this time, it is. I'm covering this, of course, because my lack of understanding of these nuances lead to a bug in my code. IN MY CODE! Yeah, I know, it defies imagination.

My scenario involves file uploads, markdown rendering, and a lot of whiskey for debugging purposes. It's a "fun" one.

The asp.net core app I'm building allows user to drag and drop an image over a textarea in order to upload the image. In response, the textarea renders a markdown image reference to the uploaded image. If you've used GitHub issues, you've seen this interaction before.

I wrote the code, shipped it, and celebrated by high-fiving my coworker, which is awkward, because I work alone and high-fiving myself is not as cool as it may sound at first.

Everything was hunky dory until a beta-tester friend of mine noticed her images weren't rendering properly. After some digging, I realized it was because her images have spaces in them. An obvious [PEBKAC](https://www.computerhope.com/jargon/p/pebkac.htm) issue, case closed.

But I like this friend and people have a right to have spaces in their image filenames. So I decided not to be so user hostile and dig into it.

### Markdown rendering

The first issue has to do with Markdown rendering. Say you upload an image named `cool image.png` (`uncool image.png` works as well, but is not as cool).

The resulting markdown might look like (xyzver5 is a random sequence to prevent collisions):

```md
![cool image.png](/assets/images/xyzver5/cool image.png)
```

Unfortunately, [Markdig](https://github.com/lunet-io/markdig) doesn't render that as an image. That's because Markdig is a goody two-shoes and accurately follows the [CommonMark Spec](https://spec.commonmark.org/0.28/#link-destination). The spec specifically which does not allow spaces for link destinations.

No problem, I naively thought, I'll just use my old standby, `HttpUtility.UrlEncode` for the filename.

```md
![cool image.png](/assets/images/xyzver5/cool+image.png)
```

Now it renders fine. On the back end though, I don't allow direct access to these images for security reasons. Instead, I route them through a controller with a wildcard route.

```cs
[Route("[controller]")]
[Authorize]
public class ImagesController {
  [HttpGet("{**name}")]
  public async Task<ActionResult> Get(string name) {
    // name will be `xyzver5/cool+image.png`
    // Some beautiful code to retrieve the image by name from the
    // back-end store which could be S3, Azure Blob Storage, or a
    // junk drawer under your bed.
  }
}
```

Note that I'm using wildcard routing here because I want the entire path after the `/images/` part of the image URL.

I tested it locally, it worked like a charm. Shipped it! I deployed it to Azure App Service and called it a day, spiked my laptop in the endzone, and celebrated another successful deployment.

### Works locally, not in production

Until I later tested it myself in production and realized it was still not working.

I was dumbfounded because my code passed the ["Works on My Machine" certification program](https://blog.codinghorror.com/the-works-on-my-machine-certification-program/).

I dug into it, ruled out possible differences, and discovered that my code wasn't even being called in production for this image, but worked fine for others. So what was the difference between the two environments? Ah! On my local machine, I'm running on Kestrel. But when I deploy, Azure App Service runs my site on IIS in front of Kestrel.

It turns out that the culprit was [IIS Request filtering](https://docs.microsoft.com/en-us/iis/configuration/system.webserver/security/requestfiltering/). By default, request filtering [blocks URLs with plus signs `+` in the non-query portion of the URL](https://blogs.iis.net/thomad/iis7-rejecting-urls-containing) (query string is fine). Why? Because it could result in a security issue:

> Some standards, e.g. the CGI standard require +'s to be converted into spaces. This can become a problem if you have code that implements name-based rules, for example urlauthorization rules that base their decisions on some part of the url.

Probably not an issue for my scenario and I could just modify this rule, but one of my coding philosophies is to try and not work against the system as much as possible. In my younger days, I'd customize the heck out of everything. Now I don't have time for all that malarkey.

The solution was simple, I needed a url encoding method that used `%20` instead of `+` to encode spaces. After some research, I used `Uri.EscapeDataString` and that did the trick! But first, I had to make sure to avoid using its evil sibling, `Uri.EscapeUriString` which by most accounts is pretty useless in comparison to its smarter and better looking sibling.

And now it works correctly! Yay me!

## What about `UrlEncoder`?

I just learned about this class and I bet it would work great too. The place where I'm doing the encoding isn't a hot spot so it's not a big concern for me to optimize that path at the moment.

## The Moral of the Story

There's probably a lot of lessons I could learn from this experience if I paid attention.

1. Test in production after deployment. Even better, minimize differences between dev and production environments. Containers might be useful here.
2. Being a developer requires being a bit of an anthropologist and realizing the libraries you use evolve as everyone gains experience. So it helps to understand why there are six or more ways to url encode a string.
3. Coding is tricky, but still fun when you learn something.

Happy coding!
