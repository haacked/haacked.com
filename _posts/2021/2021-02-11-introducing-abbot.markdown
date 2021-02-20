---
title: "Introducing Abbot, a powerful ChatOps tool for collaborative work"
description: "Introducing Abbot, your friendly neighborhood chat bot. It's the best way to automate tasks from chat to work together with others."
tags: [abbot,chatops,remote work]
excerpt_image: https://user-images.githubusercontent.com/19977/107439587-26731d00-6ae7-11eb-925c-0f50f09f2969.png
---

Collaborative work is difficult enough when located together in an office. It can [present new challenges when working remotely](https://haacked.com/archive/2020/03/03/how-to-work-from-home/). When I worked at GitHub, one powerful tool we used that left a lasting impact on me was ChatOps. In fact, GitHub may have created the concept. If not, they were certainly one of the first.

For those unfamiliar with the term, ChatOps is a portmanteau of _Chat_ and _Operations_. That might conjure the idea of operations through posting memes and emojis in chat, and while that's not too far from the truth, it's not the whole picture.

ChatOps is a collaborative way of working together within chat that is connected to all your other tools and systems through a central automation tool, the bot. The bot responds to chat commands and dispatches them to handle tasks and report back into the room. It can deploy the website, coordinate a time for a meeting through multiple time zones, or even display a random image of a pug.

At GitHub, the bot we used for ChatOps was called [Hubot](https://hubot.github.com/).

> GitHub, Inc., wrote the first version of Hubot to automate our company chat room. Hubot knew how to deploy the site, automate a lot of tasks, and be a source of fun around the office.

The power of this approach lies in the two things. First, repeatable processes and tasks can be automated through the chat bot to reduce errors and help people get more done. Second, the visibility of running commands in chat is a powerful collaboration and teaching tool.

Suppose your site goes down and you start to investigate. If you investigate by running commands in a terminal on your own system, nobody else can see what you're doing in order to help or learn. If instead you run commands in chat to bring up graphs and logs in the chat room, anyone else watching that room can follow along and collaborate. They also collectively learn how to troubleshoot issues. As does anyone in the future who happens to browse or search the chat logs. The chat room becomes your organization's shared command line.

Not only that, it's a place to support your colleagues. When GitHub was [hit by the largest DDOS attack ever](https://www.wired.com/story/github-ddos-memcached/), many of us who were not involved in defending the attack still followed along in the `#ops` room (knowing not to get in the way of course). We were able to both watch the impressive troubleshooting and mitigation display by the ops team. I learned a lot about DDOS and networking etc. Not only that, we were able to offer support in other ways as people worked around the clock to keep the site up.

## Introducing Abbot

Hubot is fantastic. However, it can be tedious to set up and keep running. And it doesn't offer much in the way of managing a Hubot via a website, nor audit logs. That's why my [my co-founder, Paul Nakata](http://pmn.org/) and I formed [_A Serious Business, Inc._](https://www.aseriousbusiness.com/) (Yes, that's the real company name) to take what we liked about Hubot and try to improve on it with [Abbot](https://ab.bot/).

Abbot is a hosted chat bot that you can install with a couple of clicks. Like Hubot, it lives in your chat room and responds to commands like a champ. Abbot can be a lot of fun, but it can also do a lot of heavy lifting for you and your colleagues.

If you're curious to try it out, head on over to [https://ab.bot/](https://ab.bot/) and click [TRY FOR FREE](https://ab.bot/login). We're in Beta right now and it's free to try out. Just authenticate with your Slack or Discord account to create an Abbot account. Then follow the instructions to install it into your chat platform. Also, be sure to check out the [lastest episode of .NET Rocks](https://dotnetrocks.com/?show=1726) where I talk about Abbot and ChatOps.

![Image of Abbot, a robot, against a purple background](https://user-images.githubusercontent.com/19977/107439587-26731d00-6ae7-11eb-925c-0f50f09f2969.png "I am ready to do the thing.")

## What can Abbot do?

Abbot ships with a few simple built-in skills. Some of them may feel familiar if you've used Hubot such as `rem` for remembering things and `who` for building the story of a person.

The real power lies in writing custom skills and sharing them. Abbot supports writing skills in the browser (using [CodeMirror](https://codemirror.net/)) with C#, JavaScript, and Python with as little ceremony and boilerplate as possible. With C#, you get IntelliSense powered by Roslyn and [MirrorSharp](https://github.com/ashmind/mirrorsharp). The editing experience also includes a console to test your skill and to run it if chat is down.

![Editing a skill in C#](https://user-images.githubusercontent.com/19977/107440160-0859ec80-6ae8-11eb-9873-31e682850be3.png)

We hope to support many more languages in the future. To learn how to write a skill, check out our [Getting Started Guide](https://ab.bot/help/guides/).

## Try out a skill package!

You can also browse and install skills written by others! Just visit the [Abbot Package Manager](https://ab.bot/packages) to see what's available.

For example, the [`tz` skill](https://ab.bot/packages/aseriousbiz/tz) is based on a Hubot script written by [Markus Olsson](https://github.com/niik) and reports a specified time in the time zones of all the mentioned people:

![Example of the TZ skill in use. It shows 2pm for America/Grand_Turk in 3 different time zones.](https://user-images.githubusercontent.com/19977/107439487-fdeb2300-6ae6-11eb-8d4a-80a1e514794d.png)

This skill makes use of the time zone as reported by Slack. Discord doesn't provide this information, so you can tell Abbot your timezone via the `my` skill, `@abbot my timezone is America/Los_Angeles`, or by telling Abbot your location, `@abbot my location is 98008`.

Another useful package is the [`deploy` skill](https://ab.bot/packages/aseriousbiz/deploy). It's based on [`hubot-github-deployments`](https://github.com/stephenyeargin/hubot-github-deployments) and lets you manage deployments via the [GitHub Deployments API](https://docs.github.com/en/rest/reference/repos#deployments).

## Custom Lists

When I was at GitHub, probably the most common skill that people would write would simply post a random image or text from a pre-configured list of items. For example, my colleague wrote a `haack` skill that would post a [random animated gif of me](https://haacked.com/archive/2016/04/28/thank-you/). I wrote one that returned a random [Deep Thought by Jack Handy](https://www.pinterest.com/chrissymfrey/deep-thoughts-snl/). These were so much fun that we included them as a first-class feature, no code necessary. You can use the `custom-list` skill to create custom lists and then add items to them. When you install Abbot, it comes pre-installed with a `joke` list. Try `@abbot joke` to learn about Abbot's sense of humor.

## Skill Triggers

One powerful feature of Abbot is the ability to trigger a skill on a schedule or via an HTTP request. The HTTP trigger is particularly useful for getting information from third party systems into chat. For example, we have a skill that receives Azure Event Grid notifications and post them into a Slack room. This alerts us when our deployment swap occurs from staging to production.

## Why Abbot?

As you might guess, Abbot is a play on A Bot. It has nothing to do with abs. We built Abbot because we enjoy pain and figured we could take care of all the hard work and tedious details of hosting a bot so you don't have to. For example, skills you write work across every chat platform we support without any additional work on your part. Likewise, any skill packages you install work across multiple chat platforms. We take care of the nitty gritty details of each platform. At the moment, we only support Slack and Discord, but the sky's the limit. As we add more chat platforms based on interest, your skills work across all of them. If your organization moves from Slack to Discord, all your skills continue to work!

We already get a lot of benefit from Abbot. We've found Abbot to be a fast way to experiment with an idea. It's a lot of work to build and host a full Slack or Discord integration. With Abbot, you can have your idea running in a few minutes.

## What's the Stack?

When evaluating a product, the stack isn't really that important. It's more important that the product solves a need for you and does it well. Having said that, if you're a long time reader of my blog, you're probably a developer interested in the behind the scenes details. I plan to write more about the development in the future. For now I'll give the highlights.

The main website is an ASP.NET Core site written in C#. It's mostly Razor Pages, a few controllers, and a bit of JavaScript. We're not using any JS frameworks as our needs are simple and I'm a fan of using web components. We do [use Bulma](http://bulma.io/) for our CSS framework as that was the preferred choice of the designer (and also a former colleague at GitHub) we worked with. For the database we use Azure PostgreSql along with EF Core for data access.

Abbot uses Azure Functions to run skill code, one for each language. This is why the languages we started with are C#, JavaScript, and Python as those are the languages supported by Azure Functions. For the editor, we use CodeMirror as mentioned earlier. We use MirrorSharp for the C# editor to give us IntelliSense.

To integrate with Slack we use the [Microsoft Bot Framework](https://dev.botframework.com/) hosted with [Azure Bot Service](https://azure.microsoft.com/en-us/services/bot-services/). Unfortunately they don't support Discord so we had to write our own Discord to DirectLine relay. We hope to open source this if there's interest.

## Try it out!

If this sounds interesting to you, please give it a try at https://ab.bot/. We could really use some feedback. You can email your feedback to [feedback@aseriousbusinees.com](mailto:feedback@aseriousbusiness.com). Or, send us feedback right from chat using Abbot: `@abbot feedback {Write Your Feedback Here}`. That'll go right into our inbox. Please use responsibly.
