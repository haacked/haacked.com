---
title: "Introducing Abbot"
description: "Introducing Abbot, your friendly neighborhood chat bot. It's the best way to automate tasks from chat."
tags: [abbot]
excerpt_image: https://user-images.githubusercontent.com/19977/107439587-26731d00-6ae7-11eb-925c-0f50f09f2969.png
---

Abbot is a hosted chat bot. It lives in your chat room and responds to commands like a champ. Abbot can be a lot of fun, but it can also do a lot of heavy lifting for you and your colleagues. The beauty of working within chat is it becomes a shared command line for your organization. For example, if you tell Abbot to deploy a branch to a lab environment, everyone else in the room can see and search the command and learn together. And look at how friendly it looks. If you're curious to try it out, head on over to https://ab.bot/ and click Sign Up. We're in Beta right now and it's free to try out. Just authenticate with your Slack or Discord to create an Abbot account. Then install it into your chat platform.

![Abbot in action](https://user-images.githubusercontent.com/19977/107439587-26731d00-6ae7-11eb-925c-0f50f09f2969.png)

## What's the story with Abbot?

Abbot is heavily inspired by [Hubot](https://hubot.github.com/).

> GitHub, Inc., wrote the first version of Hubot to automate our company chat room. Hubot knew how to deploy the site, automate a lot of tasks, and be a source of fun around the office.

When I worked at GitHub I saw the power of chat ops first-hand through Hubot. As did [my co-founder, Paul Nakata](http://pmn.org/). At the same time, setting up a Hubot is tedious. So we decided to form _A Very Serious Company, Inc._ to build Abbot. A key goal with Abbot is its easy to set up and start adding custom skills right away.

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
