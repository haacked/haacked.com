---
title: "Lessons From a Startup Pivot"
description: "Depending on SAAS, PAAS, and IAAS is great, but it can hurt when you should have turned left at Albuquerque if you haven't insulated yourself from some of those choices."
tags: [aspnetcore, azure, cloud]
excerpt_image: https://user-images.githubusercontent.com/19977/180076597-6fbdb672-6539-43fd-b10d-1a1e667c8d8b.png
---

Building a startup is easy. You file some paperwork and bam! You're a startup!

Building a start-up that's sustainable and can pay everyone a nice salary, on the other hand, is very tough. Today, as my company, [A Serious Business, Inc.](https://www.aseriousbusiness.com/) launches [Abbot on Product Hunt](https://www.producthunt.com/posts/abbot), I thought I'd reflect on some of the lessons learned in the past couple years.

[Abbot](https://ab.bot/) is a Slack App (and bot) that helps teams keep track of customer conversations in Slack.

Some of you might be thinking, "Wait! I thought Abbot was [ChatOps as a service](https://news.ycombinator.com/item?id=27974077)?" Indeed, that was our original goal, but we found it tough to to sell. Conversations often would go like this:

> them: What problem do you solve?
> us: What problems do you have? Abbot can help with them all!
> them: *snoring*

Yeah, not too effective as a sales pitch.

## Lesson: Articulate a specific problem your product solves

I know this isn't new or groundbreaking advice. In fact, it's one of the first things you hear when people give advice about building a company. But it's easy to fall into the trap of thinking that *your* product is going to be the exception.

It's not that Abbot wasn't interesting, useful, and wonderful. A lot of folks told us, "That's very cool! There's so much I could do with that! I hope to find some time to experiment with it." And there's the rub. People are busy and they don't have time to solve their own problems for them. They want your product to solve their problems. And it needs to be a concrete specific problem that matters, not something hypothetical. This leads us right to the next lesson.

## Lesson: Start with selling a Product not a Platform

Abbot started off as an automation platform. With Abbot, we could quickly write and host new capabilities of the bot from within Abbot. We used it internally to solve whole classes of problems and run experiments. Platforms give people the tools to solve whole classes of problems, but they don't, on their own, solve any specific problems. And this is why platforms are hard to sell. For example, Amazon didn't begin with selling their Amazon Web Services (AWS) platform. They started with an online bookstore. A concrete product. And over time, they reached the point where they could sell the underlying platform (AWS) the bookstore was built on.

## Lesson: A Platform does make it easy to experiment and pivot

One thing we did do well during this time was to listen to our current and prospective customers. We noticed a theme. Many of them struggled to support their own customers in Slack. Slack has a neat feature called Slack Connect that lets you invite other companies into a shared Slack channel. We discovered that a lot of companies used this feature to provide support for their own customers. Their customers liked the immediacy of being in a shared chat room. It's more responsive than getting on a phone or dealing with email. And it makes hand-off of discussions easy.

But supporting customers in chat brings its own set of problems. With so many conversations coming in, it can be tough to keep track of them all. Did we respond to every conversation? Is there any conversations waiting on a response? What's our average response time? There are a ton of tools to answer those questions for email support, but not so much for chat.

This sounds like a very specific problem to solve! Fortunately, we had a flexible platform we could leverage and build upon to solve this specific problem. So we pivoted.

But as we did so, it didn't take us long to run into other problems.

## Lesson: Balance your early engineering and infrastructure with achieving Product Market Fit

Before I get into the problems we ran into, it's important to understand some context, lest you think we're running a clown shoes operation. When we joined the YCombinator startup program, one of the key lessons drilled into us is that Product Market Fit is our top priority. And the reasoning is simple, if you don't achieve product market fit, then you don't have a business and your company runs out of money and dies. And it's hard to build great software with a dead company. So while good engineering and infrastructure is important, if you spend too much time on that, but don't achieve product market fit, then all that exquisite infrastructure goes to waste.

At the same time, if you wait too long to address your infrastructure needs, your product may fail under the weight of all that juicy product market fit. It's definitely a delicate balancing act and one that you have to feel out for yourself. I hope that sharing some of our failures can inpspire some ideas on how to strike that balance for your own product and company. That balance is going to be different for every product and company.

## Lesson: All Services Are Leaky

Around the time dinosaurs roamed the wold hosting their own server racks, I was a fresh-faced college graduate starting my first job at a custom development shop. We had a full server rack in the closet where we hosted all our clients web sites. This put us in control of our own hardware, software, and destiny. Which as it turns out, isn't always a good thing. I remember the time my boss wanted to demonstrate how our new hard-drive array was hot-swappable by pulling out a drive. He pulled out the drive and everything continued to work flawlessly. Just kidding. It all came crashing down like a soccer player who feels a gentle tug on the shirt in the penalty box (if the sport metaphor doesn't work for you, just trust me on this one).

At the time, this was the way you built web software. You hosted everything.

Fast forward a bunch of years, and the pendulum swung to the other side. I didn't want to host a damn thing. And with tools like Heroku, Azure Web Sites, AWS, Google Cloud, etc. I really didn't need to. Much of my later career at Microsoft and GitHub was spent building developer tools and client applications. Occasionally, I'd build a website on the side for fun. I'd just deploy it to a cloud provider, take advantage of all their services, and not worry about it too much.

And this approach worked well! It worked so well, when I started a company with my colleague, Paul, we were able to [get a site up](https://ab.bot/) and running in no time. And this all worked great, everyone was happy, and it all just worked out...Until we needed to pivot.

![Driver telling a police office he should have turned left at Albuquerque](https://user-images.githubusercontent.com/19977/180076597-6fbdb672-6539-43fd-b10d-1a1e667c8d8b.png "Free Public Domain image from https://freesvg.org/1535673080")

You may have heard that [all abstractions are leaky](https://www.joelonsoftware.com/2002/11/11/the-law-of-leaky-abstractions/). In a similar manner, all services are leaky. Whether it be Software as a Service (SAAS), Platform as a Service (SAAS), Infrastructure as a Service (IAAS), or whatever the next thing is.

The first "abstraction" to break was relying on Azure Bot Service for our bot. The service provides a single API to make it possible to write a bot once for multiple chat services. Where have we heard that promise before, right?

As you might expect, it meant our bot had access to the lowest common denominator of what chat platforms have to offer. This was fine for our initial product idea, [ChatOps as a service](https://news.ycombinator.com/item?id=27974077). With ChatOps, the interface is primarily textual, so we didn't need all the bell and whistles. But as we pivoted to a Customer Success product, it became imperative that we supported all the rich features that Slack has to offer.

This meant bypassing the bot service and writing our code to directly interact with the Slack API. I don't regret that we started with the bot service as it got us far fast, but I do regret when we configured our Slack Events Endpoints (the URLs that Slack sends its events to), with the bot service URLs. Something like `https://slack.botframework.com/api/messages/events` (or whatever it is). The reason this was a problem is that we submitted our Slack App to the Slack App Directory. So every change we make to the app's configuration can take up to six weeks to be approved by Slack. This meant that even though we were ready to go with our new approach, we had to wait for the Slack approval process to start serving Slack requests directly.

## Lesson: Own Your Traffic With A Level of Indirection built in

A little sprinkling of indirection would have helped a lot here. We should have set up a DNS entry under our control, pointed it to Azure Bot Service, and configured Slack with that URL. It would have allowed us to point Slack to our own web server when we needed to switch. It just wasn't a consideration in the beginning. At the time, we felt we wouldn't need it. But it's cheap to set up a DNS entry to provide a bit of indirection just in case.

As a corollary to this, I recommend managing DNS as code. We have several DNS providers and managing DNS across them can be a pain. But we now use [OctoDNS](https://github.com/octodns) to manage all our DNS in some YAML files that are versioned within a GitHub repository.

Alright, back to the story. So what do you think happened once Slack approved our change to direct Slack traffic to our own server at https://ab.bot? We found a bug that broke everything.

Now a minute ago I was talking about all the benefits of indirection that using our own Domain Name afforded us. Well, I made another mistake here. I pointed Slack directly to https://ab.bot/, which the rest of our site also ran on. So we couldn't just point this DNS entry back to Bot Service. And to revert the change might take another six weeks to go through Slack's approval process. So we just knuckled down to fix it.

Today, we have a separation where https://ab.bot/ points to our marketing site and login page, but the app runs on https://app.ab.bot/. And for each service that needs to be publicly exposed, we use a unique host name in case we need to redirect traffic.

## Lesson: Balance Convenience With The Right Tools

Having been a Microsoft employee in the past, most of my experience is with Azure. When in doubt, I tend to choose Azure tools because they're familiar to me and it's convenient to have everything in one place with the Azure Portal. However, this sometimes means I end up choosing tools that aren't up to snuff. For example, we looked at Azure Front Door and Azure Application Gateway to provide a proxy that would let us better control how traffic reaches our site. Together, they supported the features we needed, but each one was missing something. And using them together seemed complex and overkill. So we ended up using Cloudflare which did everything we need.

## Lesson: Observability and Monitoring With Alerts is Essential

As we started to build out our new product offering, we started to notice connection timeouts. Our database was getting hammered periodically and we had no understanding of why. Fortunately, we were able to figure it out by adding better instrumentation.

See, we had a lot of logging, but not enough. Even worse, we didn't have enough alerts set up early on. So some errors would occur and we wouldn't notice them. This wasn't really a problem at first, but as our pivot started getting traction with real customers, being able to diagnose problems quickly rose up in priority.

## Lesson: Code Your Infrastructure

We ended up figuring out that some problematic code was the cause of some of our error spikes. But we were also on a pretty low-tier plan for the database and web server. As we got closer and closer to shipping our 1.0, we realized we needed to scale up. Now *this* is where the cloud shines, right?

Well no, we happened to choose a plan that didn't allow us to automatically scale up. We had to migrate to a new plan. This meant a lot of manual work and scripting to migrate our web servers and data to new servers and databases in the cloud.

And that helped a lot! So now we're on a plan that will allow us to automatically scale up our infrastructure! Except we happened to choose a region that's at capacity due to recent supply chain issues. So in order to scale up our hardware even more, we need to do yet another migration.

It wouldn't be so bad if all our infrastructure was scripted using something like Terraform or Pulumi, but we're not quite there yet. We've made a lot of progress with automating our infrastructure lately, but we're still not at the point where we can just migrate to a new data center with a few commands.

I used to believe that being Cloud Provider agnostic was a waste of engineering effort, but as we build out our product, we can see that even if you stick with a single cloud provider, it has benefits.

## Lesson: Hire great people

One of our saving graces is we've been very fortunate in our early hiring. In the areas where I'm weak, we have folks who are superstars. At this stage of our company, every person we hire has a huge impact on the company. If we hadn't brought in these amazing people, I think we'd have failed already.
