---
title: "Subscribing to cloud events with Abbot"
description: "Subscribe to notifications about important events in your cloud infrastructure using Abbot in a few easy steps!"
tags: [abbot,chatops,csharp]
excerpt_image: https://user-images.githubusercontent.com/19977/110027163-7f009900-7ce6-11eb-8aba-5b1d96ef0b6d.png
---

In [my last post](https://haacked.com/archive/2021/02/19/writing-abbot-skill-in-csharp/), I wrote about writing a sparkly skill in Abbot. That was fun! But Abbot isn't _only_ about fun. After all, our company name is [A Serious Business, Inc.](https://aseriousbusiness.com/) Seriously, that's the name. So it's about time I show you how to get to some serious business with Abbot.

Here's the scenario: We have a [Blue Green deployment](https://martinfowler.com/bliki/BlueGreenDeployment.html) set up for the [Abbot website](https://martinfowler.com/bliki/BlueGreenDeployment.html). In Azure parlance, we use [Deployment Slots](https://docs.microsoft.com/en-us/azure/app-service/deploy-staging-slots) to set this up. This allows us to deploy to stage, and have an automatic cutover to production if everything is fine. But this process can seem opaque in action. It'd be nice to receive a notification when a deployment to stage is complete and the swap is starting. That's where the new Abbot [`cloud-event`](https://ab.bot/packages/aseriousbiz/cloud-event) skill comes in handy.

I [recorded a video](https://www.youtube.com/watch?v=nMKZFzVGutY) so you can watch to see it in action. But I'll write about it here too.

[![YouTube video of the cloud-event skill in action](https://user-images.githubusercontent.com/19977/110027163-7f009900-7ce6-11eb-8aba-5b1d96ef0b6d.png)](https://www.youtube.com/watch?v=nMKZFzVGutY)



The `cloud-event` skill supports the [Cloud Events Web Hooks for Event Delivery](https://github.com/cloudevents/spec/blob/v1.0.1/http-webhook.md) specification. That means it can support any cloud provider that implements the spec, which I assume is most of them.

In our case, we use [Azure Event Grid](https://docs.microsoft.com/en-us/azure/event-grid/overview) to subscribe to events happening in our cloud infrastructure. Here's how it works.

## Create Azure Event Grid Subscription

In the [Azure Portal](https://portal.azure.com/) search up "Event Grid Subscriptions" and click on that to get to the event grid subscriptions page. Note, this page is pre-filtered which can be confusing (I let the team know and they promise to make this UI better).

![Event Grid Subscriptions](https://user-images.githubusercontent.com/19977/110028278-e539eb80-7ce7-11eb-9e71-17c45702bfda.png)

From here click the plus sign next to "Event Subscription" to create a new subscription.

1. For the "Event Schema" choose "Cloud Event Schema 1.0".
2. For the "Topic Types" choose "App Services."
3. For "Endpoint Type" choose "Web Hook"

At this point, it should look something like this. Now we just need a web hook URL. This is where Abbot comes in!

![Screen Shot 2021-03-04 at 12 46 59 PM](https://user-images.githubusercontent.com/19977/110028281-e5d28200-7ce7-11eb-8037-e1d64d6a0483.png)

At this point, I assume you have an Abbot account set up and connected to a chat room. If not, go to [https://ab.bot/](https://ab.bot/) and click "Log In" and use your Slack or Discord login to log in. Follow the instructions to get it set up.

Next, we need to install the `cloud-event` package. On the [`cloud-event`](https://ab.bot/packages/aseriousbiz/cloud-event) package page, click the "Install Package" button. It'll give you a chance to review the code before it actually installs it. Click "Create skill from package" and now you have the skill enabled!

Go to a chat room with Abbot. In our case, we have an `#ops` room for this sort of notification. Then run the following in chat:

```
@abbot attach cloud-event
```

What that does is create an HTTP trigger for the skill attached to the room where you ran it. An HTTP trigger is a secret URL that can be used to call the skill. And the skill will respond to the room it's attached to.

When you run the command, Abbot will respond with something like:

> The skill `cloud-event` is now attached to the channel `ops`. Visit https://ab.bot/skills/cloud-event/triggers to get the secret URL used to call this skill.

Grab that URL, and go back to the Event Grid Subscription page, and click "Select an endpoint" and supply the trigger URL as the web hook endpoint. Click "Create" and you're done! You can trigger a deploy to see it in action. Or just change an App Setting to get an `AppUpdated` notification. If you run into any problems, you can see a log of the last 100 HTTP requests that the skill received.

```
@abbot cloud-event log
```

And you can inspect more details such as the HTTP headers and the request body by specifying which log entry to look at:

```
@abbot cloud-event log 42
```

This is a powerful example of the value of Abbot in a chat ops workflow. If you're interested in learning more about Abbot, check out [my introduction to Abbot](https://haacked.com/archive/2021/02/11/introducing-abbot/), [my .NET Rocks episode on Abbot](https://dotnetrocks.com/?show=1726), or just peruse the [Abbot website](https://ab.bot/). And when you watch the video, don't forget to smash that like button!
