---
title: "All Services Are Leaky"
description: "Depending on SAAS, PAAS, and IAAS is great, but it can hurt when you should have turned left at Albuquerque if you haven't insulated yourself from some of those choices."
tags: [aspnetcore, azure, cloud]
excerpt_image: https://user-images.githubusercontent.com/19977/180076597-6fbdb672-6539-43fd-b10d-1a1e667c8d8b.png
---

At my first job, fresh outta college, I worked at a custom development shop. We had a full server rack in the closet where we hosted all our clients web sites. We were in control of our own hardware, software, and destiny. Which as it turns out, isn't always a good thing. I remember one time my boss wanted to demonstrate how our new hard-drive array was hot-swappable by pulling out a drive and sure enough, it brought all our sites on that server down.

Fast forward a bunch of years, and my personal preference swung all the way to the other side. I didn't want to host a damn thing. And with tools like Heroku, Azure Web Sites, AWS, etc. I really didn't need to. Having worked at Microsoft, I tended to just dump everything to Azure and let it sort it out. I loved that it just worked! If it was built-in, I would use it.

In fact, it worked so well, when I started a company with my colleague, Paul, we were able to [get a site up](https://ab.bot/) and running in no time. We were using:

1. Azure Web Sites - to host our actual web app.
2. Azure Functions - to host our skill runners (for running customer code).
3. Azure PostgreSQL - our hosted database.
4. Azure Bot Service - for managing our bot's communication with Slack, Teams, and Discord.
5. Multiple DNS services because of the various domains we had.
6. And so on…

And this all worked great, until we needed to pivot.

![Driver telling a police office he should have turned left at Albuquerque](https://user-images.githubusercontent.com/19977/180076597-6fbdb672-6539-43fd-b10d-1a1e667c8d8b.png "Free Public Domain image from https://freesvg.org/1535673080")

You may have heard that [all abstractions are leaky](https://www.joelonsoftware.com/2002/11/11/the-law-of-leaky-abstractions/). In a similar manner, I think all services are leaky too, whether it be Software as a Service (SAAS), Platform as a Service (SAAS), Infrastructure as a Service (IAAS), or whatever the nxt thing is.

The first "abstraction" to break was relying on Azure Bot Service. It provides a single API to make it possible to write a bot once that can communicate with multiple chat services. Where have we heard that promise before, right? As you might expect, it meant supporting the lowest common denominator of what chat platforms have to offer. And this worked for our initial product idea, [ChatOps as a service](https://news.ycombinator.com/item?id=27974077). With ChatOps, the interface was primarily textual, so we didn't need all the bell and whistles. But as we started pivoting to providing a Customer Success product to help keep on top of conversations in Slack, it became imperative that we supported all features that Slack had to offer.

This meant bypassing Bot Service and writing our code to directly interact with the Slack API. I don't regret that we started with Bot Service as it got us far fast, but I do regret when we configured our Slack Events Endpoints (the URLs that Slack sends its events to), we configured it with the Bot Service URLs. Something like `https://slack.botframework.com/api/messages/events` (or whatever it is). The reason this was a problem is that we submitted our Slack App to the Slack App Directory. So every change we make to the app's configuration can take up to six weeks to be approved by Slack. This meant that even though we were ready to go with our new approach, we had to wait for the Slack approval process to start serving Slack requests directly.

## Lesson: Own Your Traffic With A Level of Indirection built in

Ideally, we would have set up a DNS entry and configured Slack with that. It would have allowed us to point it to Azure Bot Service in the beginning, and later point it to our own web server when we needed to switch. At the time, we felt we wouldn't need it. But it's inexpensive to set up a DNS entry for a level of indirection.

As a corollary to this, it's really helpful to manage DNS as code. We have several DNS providers, but we now manage all our DNS in a repository using https://github.com/octodns.

Alright, back to the story. So what do you think happened once the approval went through and we were directing Slack traffic directly to our server? Of course, we found a bug and it didn't work. And we made the mistake of pointing Slack directly to https://ab.bot/ which is our main site, so we couldn't just point DNS back to botframework.com. We were really in a bind because it would take another six weeks to change the Slack configuration back to pointing to Bot Service.

## Lesson: Own your Ingress

Yes, we should have used a DNS entry other than our main app's entry. In retrospect it's so obvious. But even so, we also should have had some sort of forward proxy in place such as Azure Front Door or Azure Application Gateway. I should mention, neither of those choices worked out for us, which leads me to the next lesson.

## Lesson: Balance Convenience With The Right Tools

I mentioned before that my background is with Azure, so I tend to choose Azure tools because I know them and it's convenient to have everything in one place with the Azure Portal. However, this sometimes means I end up choosing tools that aren't up to snuff. It turns out that Azure Front Door and Azure Application Gateway each have deficiences. And the only solution that Microsoft could provide us was to use both at the same time, which seemed complex and overkill. Especially when Cloudflare does exactly what we need and is probably the best in breed of this type of thing.

As we started to build out our new product offering, we started to notice connection timeouts. Our database was getting hammered periodically and we had no understanding of why. Fortunately, we were able to figure it out by adding better instrumentation.

## Lesson: Observability and Monitoring With Alerts is Essential

See, we had a lot of logging, but not enough. Even worse, we didn't have alerts set up early on. So errors would occur and we wouldn't notice them. I know I'm making it sound like we're running a clown shoes operation, but our primary focus at the time was product market fit and getting something useful in our customers hands to try out. But as we started getting more people trying it out, being able to diagnose problems quickly rose up in priority.

## Lesson: Code Your Infrastructure

We ended up figuring out that some problematic code was the cause of some of our error spikes. But we were also on a pretty low-tier plan for the database and web server. As we got closer and closer to shipping our 1.0, we realized we needed to scale up. Now *this* is where the cloud shines, right?

Well no, we happened to choose a plan that didn't allow us to automatically scale up. We had to migrate to a new plan. This meant a lot of manual work and scripting to migrate our web servers and data to new servers and databases in the cloud.

And that helped a lot! So now we're on a plan that will allow us to automatically scale up our infrastructure! Except we happened to choose a region, US West 2, that's at capacity due to recent supply chain issues. So in order to scale up our hardware even more, we need to do yet another migration.

It wouldn't be so bad if all our infrastructure was scripted using something like Terraform or Pulumi, but we're not quite there yet. We've made a lot of progress with Pulumi lately, but we're still not at the point where we can just migrate to a new data center with a few commands. That's where we're headed next. I used to believe that being Cloud Provider agnostic was a waste of engineering effort, but as we build out our product, we can see that even if you stick with a single cloud provider, it has benefits.

They say that when you build a startup, you're going to make a lot of mistakes. And like an idiot, I thought I've been around the block and wouldn't make all those rookie mistakes. Yet here we are. I hope that your startup can learn from our mistakes (assuming you're building a similar type of product).