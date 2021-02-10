---
title: "Introducing Abbot"
description: "Introducing Abbot, your friendly neighborhood chat bot. It's the best way to automate tasks from chat."
tags: [abbot]
excerpt_image: https://user-images.githubusercontent.com/19977/107439587-26731d00-6ae7-11eb-925c-0f50f09f2969.png
---

Abbot is a hosted chat bot. It lives in your chat room and responds to commands like a champ. Abbot can be a lot of fun, but it can also do a lot of heavy lifting for you and your colleagues. This way of working is often referred to as "chat ops."

The power of chat ops is that a Bot like Abbot becomes a shared command line for your organization. For example, if you tell Abbot to deploy a branch to a lab environment, everyone else in the room can see and search the command and learn together. If you're curious to try it out, head on over to https://ab.bot/ and click [TRY FOR FREE](https://ab.bot/login). We're in Beta right now and it's free to try out. Just authenticate with your Slack or Discord to create an Abbot account. Then install it into your chat platform.

![Abbot in action](https://user-images.githubusercontent.com/19977/107439587-26731d00-6ae7-11eb-925c-0f50f09f2969.png)

## What's the story with Abbot?

Abbot is heavily inspired by [Hubot](https://hubot.github.com/).

> GitHub, Inc., wrote the first version of Hubot to automate our company chat room. Hubot knew how to deploy the site, automate a lot of tasks, and be a source of fun around the office.

When I worked at GitHub I saw the power of chat ops first-hand through Hubot. As did [my co-founder, Paul Nakata](http://pmn.org/). At the same time, setting up a Hubot is tedious. So we decided to form [_A Serious Company, Inc._](https://www.aseriousbusiness.com/) to build Abbot. A key goal with Abbot is its easy to set up and start adding custom skills right away.

## What can Abbot do?

Abbot ships with a few simple built-in skills. Some of them may feel familiar if you've used Hubot such as `rem` for remembering things and `who` for building the story of a person.

The real power lies in writing custom skills and sharing them. Abbot supports writing skills in the browser (using [CodeMirror](https://codemirror.net/)) with C#, JavaScript, and Python. With C#, you get IntelliSense powered by Roslyn and [MirrorSharp](https://github.com/ashmind/mirrorsharp).

![Editing a skill in C#](https://user-images.githubusercontent.com/19977/107440160-0859ec80-6ae8-11eb-9873-31e682850be3.png)

We hope to support many more languages in the future. To learn how to write a skill, check out our [Getting Started Guide](https://ab.bot/help/guides/).

## Try out a skill package!

You can also browse and install skills written by others [from the Abbot Package Manager](https://ab.bot/packages).

For example, the [`tz` skill](https://ab.bot/packages/aseriousbiz/tz) is based on a Hubot script written by [Markus Olsson](https://twitter.com/niik) and reports a specified time in the timezones of all the mentioned people:

![Example of the TZ skill in use](https://user-images.githubusercontent.com/19977/107439487-fdeb2300-6ae6-11eb-8d4a-80a1e514794d.png)

This skill makes use of the timezone as reported by Slack. Discord doesn't provide this information, so you can tell Abbot your timezone via the `my` skill, `@abbot my timezone is America/Los_Angeles` or by telling Abbot your location, `@abbot my location is 98008`.

The [`deploy` skill](https://ab.bot/packages/aseriousbiz/deploy) is based on [`hubot-github-deployments`](https://github.com/stephenyeargin/hubot-github-deployments) and lets you manage deployment via the [GitHub Deployments API](https://docs.github.com/en/rest/reference/repos#deployments).

## Skill Triggers

One interesting feature of Abbot is the ability to trigger a skill on a schedule or via an HTTP request. The HTTP trigger is particularly useful for getting information from third party systems into chat. For example, we have a skill that receives Azure Event Grid notifications and post them into a Slack room. This alerts us when our deployment swap occurs from staging to production.

## Why Abbot

If you mean the name, it's a play on A Bot. It has nothing to do with abs. If you mean the product, we figured we could take care of all the hard work and tedious details of hosting a bot so you don't have to. When you write a skill, that skill can be used in every chat platform we support. We take care of the nitty gritty details of each platform. At the moment, that's just Slack and Discord, but we plan to support more depending on interest.

We've found Abbot to be a fast way to experiment with an idea. It's a lot of work to build and host a full Slack or Discord integration. With Abbot, you can have your idea running in a few minutes.

## What's the Stack?

When evaluating a product, the stack isn't really that important. It's more important that the product solves a need for you and does it well. Having said that, if you're a long time reader of my bot, you're probably a developer interested in the behind the scenes details. I plan to write more about the development in the future. For now I'll give the highlights.

The main website is an ASP.NET Core site written in C#. It's mostly Razor Pages, a few controllers, and a bit of JavaScript. We're not using any JS frameworks as our needs are simple and I'm a fan of using web components. We do [use Bulma](http://bulma.io/) for our CSS framework as that was the preferred choice of the designer (and also a former colleague at GitHub) we contracted with.

The code that runs the skills are Azure Functions. This is why the languages we started with are C#, JavaScript, and Python as those are the languages supported by Azure Functions. For the editor, we use CodeMirror as mentioned earlier. We use MirrorSharp for the C# editor to give us IntelliSense.

To integrate with Slack we use the [Microsoft Bot Framework](https://dev.botframework.com/) hosted with [Azure Bot Service](https://azure.microsoft.com/en-us/services/bot-services/). Unfortunately they don't support Discord so we had to write our own Discord to DirectLine relay. We hope to open source this if there's interest.

## Try it out!

If this sounds interesting to you, please give it a try at https://ab.bot/. We could really use some feedback. In fact, you can send us feedback right from chat using Abbot: `@abbot feedback {Write Your Feedback Here}`. That'll go right into our inbox. Please use responsibly.