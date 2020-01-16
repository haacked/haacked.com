---
title: "My First Xamarin app"
description: "In which I build a simple mobile app built with Xamarin that offers an encouragement based on your mood."
tags: [mobile,dev,xamarin]
excerpt_image: https://user-images.githubusercontent.com/19977/72463290-bf6e2200-3787-11ea-9c83-26e5cf22a7b4.png
---

A few days ago, my daughter was in a real funk. It breaks my heart to see her struggle. Fortunately, I knew exactly what to do, "build a mobile app!"

Before the humorless among you think I'm a callous parent, that's not exactly what I did of course. I gave her money.

I kid. What I _really_ did was listen to her struggles and offered unconditional love, support, and comfort. _Then_ I went and wrote a mobile app.

## The App

The idea for the app is simple. The main screen has a list of moods. She clicks on the mood corresponding to how she feels at the moment, and the app displays an encouraging message with a corresponding funny or cute image.

This might sound familiar to some of you. The idea was inspired by the [Encourage for Visual Studio](https://haacked.com/archive/2014/06/20/encourage-vs/) and [Encourage for Atom](https://haacked.com/archive/2016/05/11/encourage-atom/) plug-ins I wrote in the past.

Let's take a look at it! This is the main screen with a list of moods.

<img src="https://user-images.githubusercontent.com/19977/72462978-2fc87380-3787-11ea-9994-3c15a3da8889.png" title="A screen with a list of mood buttons: Sad, Bored, Angry, Frustrated, Happy" width="400" />

Suppose she clicks "Bored", she'll get a random encouragement which consists of a funny photo and a message.

<img src="https://user-images.githubusercontent.com/19977/72463290-bf6e2200-3787-11ea-9c83-26e5cf22a7b4.png" title="Image of a dog with glasses looking at a book. Underneath is the caption: When I'm bored, I go read a book" width="400" />

For now, the image is stored as a URL and is loaded off the internet, so a connection is required to see the image. Also, all of this is configurable through a janky UI.

<img src="https://user-images.githubusercontent.com/19977/72463503-3a373d00-3788-11ea-97ac-ffdfeb82e27d.png" title="A janky looking user interface for editing a mood and its list of encouragements" width="400" />

So if you wanted to add a mood such as "Missing my Ex" and add encouragements such as "Put down that phone!", you could totally do that!

## Choice of Stack

For most of my career, I've been a web slinger knee deep in HTML, CSS, JavaScript, and all the [agglutinations of Active Server Pages](https://twitter.com/haacked/status/1217512365622644736). There was one brief interlude where [I impersonated a WPF developer](https://haacked.com/archive/2012/05/21/introducing-github-for-windows.aspx/), but most of my career has been web tech.

Since I want to build a cross-platform app, React Native might seem a logical choice. But in the end, I wanted to build it fast and my command of XAML and C# is better than my command of React. Also, this is just an exploration for me. I am not building this with expectations of becoming an App Store millionaire. I just wanted to get it working quick and on my daughter's phone.

## The Experience

The good news is the time it took from code to something usable on the phone was very short. The longest part of it was getting all the tools installed. Since I'm on a mac, used Visual Studio for Mac as many of the tutorials assume it. For asp.net core, I've been using Jetbrains Rider.

You might wonder why I don't use Visual Studio Code. Don't get me wrong, I love VS Code. But for me, it's like the hostel of editors. It's vibrant and fun and perfect for me when I was younger, but as I've gotten older, I've grown accustomed to all the amenities of a proper hotel (IDE). I still use VS Code for a lot though.

Here are a few of the tutorials I followed.

* [Create a Single Page Xamarin.Forms Application](https://docs.microsoft.com/en-us/xamarin/get-started/quickstarts/single-page?pivots=macos)
* [Perform Navigation in a Multi-Page Xamarin.Forms Application](https://docs.microsoft.com/en-us/xamarin/get-started/quickstarts/multi-page?pivots=macos)
* [Store Data in a Local SQLite.NET Database](https://docs.microsoft.com/en-us/xamarin/get-started/quickstarts/database?pivots=macos)
* [Style a Cross-Platform Xamarin.Forms Application](https://docs.microsoft.com/en-us/xamarin/get-started/quickstarts/styling?pivots=macos)
* [Xamarin.Forms Quickstart Deep Dive](https://docs.microsoft.com/en-us/xamarin/get-started/quickstarts/deepdive?pivots=macos)

Most of what I implemented is pretty boilerplate. However, one feature was a bit tricky. In order to edit a mood, I wanted the user to press and hold the button. The long press gesture doesn't come included with Xamarin Forms. Fortunatley I found [a blog post by Alex Dunn where he implements a long press effect with a custom `RoutingEffect`](https://alexdunn.org/2017/12/27/xamarin-tip-xamarin-forms-long-press-effect/).

## Some observations

* I've written a ton of XAML and it's still confusing at times. It's got quite a learning curve. Some things that should be simple [are way more complicated than you'd expect](https://twitter.com/haacked/status/1217183616142176256). Microsoft recently announced some [experimental Blazor bindings for Xamarin Forms](https://docs.microsoft.com/en-us/mobile-blazor-bindings/). I might try and port my app to it for a side-by-side comparison.
* Visual Studio on Mac is beautiful, but pretty rough around the edges. Every now and then it just shits itself and decides it's had enough of Intellisense. A restart usually brings it back. This happens with Rider too.
* Also, I don't like that to do multi-cursor selection, I have to use the mouse or trackpad. There doesn't seem to be block selection fully with keyboard like there is in Visual Studio on Windows.
* Every now and then it can't match up xaml files with their code behind. It just gets in a mood I tell ya.
* It's not clear to me how to build idiomatic data entry pages (such as setting pages) with Xamarin. I wish there was more guidance about building idiomatic apps and common scenarios.
* Despite these complaints, the overall experience of writing the app with C# 8 was a real pleasure. I started off as simple and minimal as I could. For example, I didn't add any view models until I really started to need them. And even then, I am not using any view model frameworks. As the code gets more complicated, I may introduce something.
* Hot reload with Xamarin is so hot!

## Try it out, provide feedback, or get involved

The code is up on GitHub at [https://github.com/haacked/encourage-mobile](https://github.com/haacked/encourage-mobile).

As this is my first mobile app with Xamarin, I would love feedback and help. Especially around these areas:

1. Is my approach idiomatic? Are there things I should be doing better?
2. Can you help me make it look less like ass and more like awesome?
3. What polish should I add?
4. I need an icon.

## Conclusion

I should mention before I end this post, my daughter loved the bespoke app lovingly crafted just for her. She got a good laugh out of it, then went right back to her Youtube videos never to launch it again.
